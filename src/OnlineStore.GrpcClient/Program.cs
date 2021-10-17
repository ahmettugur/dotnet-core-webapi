﻿using Grpc.Net.Client;
using OnlineStore.GrpcService.Protos;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineStore.GrpcClient
{
    class Program
    {
        protected Program()
        {
            
        }
        static async Task Main(string[] args)
        {

            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress("https://localhost:5005", new GrpcChannelOptions { HttpClient = httpClient });
            var client = new ProducController.ProducControllerClient(channel);
            var reply = await client.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty());

            foreach (var item in reply.Products)
            {
                Console.WriteLine($"Ürün Id: {item.Id} Ürün Adı: {item.Name} Fiyat: {item.Price}");
            }

            Console.ReadLine();
        }
    }
}
