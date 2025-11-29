using Entities.Model;
using Microsoft.Extensions.Configuration;
using Service.PaymentServicePayMob.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.PaymentServicePayMob
{
    public class PaymobService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public PaymobService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<string> GeneratePaymentUrl(Order order)
        {
            // 1) AUTH
            var authToken = await Auth();

            // 2) CREATE PAYMOB ORDER
            var paymobOrderId = await CreatePaymobOrder(authToken, order);

            // 3) CREATE PAYMENT KEY
            var paymentKey = await CreatePaymentKey(authToken, paymobOrderId, order);

            // 4) BUILD URL
            var iframeId = Environment.GetEnvironmentVariable("PAYMOB_IFRAME_ID");

            var url = $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentKey}";

            if (paymentKey == null || iframeId == null)
                return null;

            return url;
        }


        private async Task<string> Auth()
        {
            var apiKey = Environment.GetEnvironmentVariable("PAYMOB_API_KEY");

            var response = await _client.PostAsJsonAsync(
                "https://accept.paymob.com/api/auth/tokens",
                new { api_key = apiKey }
            );

            var data = await response.Content.ReadFromJsonAsync<AuthResponse>();

            return data.token;
        }

        private async Task<int> CreatePaymobOrder(string authToken, Order order)
        {
            var body = new
            {
                auth_token = authToken,
                delivery_needed = "false",
                amount_cents = (order.Total * 100).ToString(),
                currency = "EGP",
                merchant_order_id = order.Id.ToString()
            };

            var response = await _client.PostAsJsonAsync(
                "https://accept.paymob.com/api/ecommerce/orders",
                body
            );

            var data = await response.Content.ReadFromJsonAsync<PaymobOrderResponse>();

            return data.id; // Paymob Order ID
        }

        private async Task<string> CreatePaymentKey(string authToken, int paymobOrderId, Order order)
        {
            var integrationId = Environment.GetEnvironmentVariable("PAYMOB_INTEGRATION_ID");

            var body = new
            {
                auth_token = authToken,
                amount_cents = (order.Total * 100).ToString(),
                expiration = 3600,
                order_id = paymobOrderId,
                currency = "EGP",
                integration_id = integrationId,
                billing_data = new
                {
                    apartment = "NA",
                    email = "hs915684@gmail.com",
                    floor = "NA",
                    first_name = "Hamdy",
                    street = "NA",
                    building = "NA",
                    phone_number = "01116727826",
                    shipping_method = "PKG",
                    postal_code = "NA",
                    city = "Giza",
                    country = "EG",
                    last_name = "Saad",
                    state = "NA"
                }
            };

            var response = await _client.PostAsJsonAsync(
                "https://accept.paymob.com/api/acceptance/payment_keys",
                body
            );

            var data = await response.Content.ReadFromJsonAsync<PaymentKeyResponse>();

            return data.token;
        }

    }
}
