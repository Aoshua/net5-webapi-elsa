﻿using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Workflow.ActivityLibrary.Services
{
    public static class HttpService
    {
        public static HttpClient Client { get; set; }

        static HttpService()
        {
            Client = new HttpClient(
                new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    {
                        return true;
                    },
                }
                , false
            )
            {
                BaseAddress = new Uri("https://localhost:44358/")
            };
        }

        public static async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var obj = JsonSerializer.Deserialize<T>(content);
                return obj;
            }
            else throw new Exception(response.ToString());
        }
    }
}
