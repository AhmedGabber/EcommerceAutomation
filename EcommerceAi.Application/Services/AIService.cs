using EcommerceAi.Application.Dtos.AiDtos;
using EcommerceAi.Application.IServices;
using System.Text.Json;

namespace EcommerceAi.Application.Services;

public class AIService : IAIService
{
    private readonly IProductService _productService;
    private readonly IShipmentService _shipmentService;
    private readonly IOllamaService _ollamaService;
    private readonly ICustomerService _customerService;
    private readonly IOrderService _orderService;

    public AIService(
     IProductService productService,
     IShipmentService shipmentService,
     ICustomerService customerService,
     IOrderService orderService,
     IOllamaService ollamaService)
    {
        _productService = productService;
        _shipmentService = shipmentService;
        _customerService = customerService;
        _orderService = orderService;
        _ollamaService = ollamaService;
    }

    public async Task<AiChatResponseDto> ChatAsync(
        AiChatRequestDto request)
    {
        var actionJson =
            await DetectIntentAsync(request.Message);
        var action =
            JsonSerializer.Deserialize<AiActionDto>(
                actionJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (action == null)
        {
            return new AiChatResponseDto
            {
                Response = "Could not determine action."
            };
        }

        object result = null!;

        switch (action.Action)
        {
            case "GetProducts":


                var products =
                    await _productService.SearchProductsAsync(request.Message);


                return new AiChatResponseDto
                {
                    Response = JsonSerializer.Serialize(products)
                };

                break;

            case "TrackShipment":

                var customer =
                    await _customerService
                        .GetByPhoneNumberAsync(
                            request.PhoneNumber);

                if (customer == null)
                    return new AiChatResponseDto
                    {
                        Response = "Customer not found."
                    };

                var order =
                    await _orderService
                        .GetLatestOrderAsync(
                            customer.Id);

                var shipment =
                    await _shipmentService
                        .GetByOrderIdAsync(
                            order.Id);

                return new AiChatResponseDto
                {
                    Response =
        $"Your order is currently {shipment.Status}. " +
        $"Tracking Number: {shipment.TrackingNumber}."
                };

                break;

            default:

                return new AiChatResponseDto
                {
                    Response =
                        "Action not supported."
                };
        }

    }

    private async Task<string> DetectIntentAsync(string message)
    {
        var prompt =
    $$"""
You are an API Router.

Available Actions:

GetProducts
TrackShipment

IMPORTANT:
Return ONLY ONE JSON object.
Do not explain.
Do not use markdown.
Do not return examples.

User Message:
{{message}}

JSON:
""";

        return await _ollamaService.AskAsync(prompt);
    }
}