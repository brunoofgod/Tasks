using Tasks.CrossCutting.Services.Dtos;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Tasks.CrossCutting.Services
{
    public class BuscaCepServices : IDisposable
    {
        private string Url => "https://viacep.com.br/";

        public ViaCepDto BuscarCep(string cep)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                var query = $"ws/{cep}/json";
                var response = client.GetAsync(query);
                if (!response.Result.IsSuccessStatusCode)
                    return null;

                var resultado = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ViaCepDto>(resultado);
            }
        }

        public void Dispose()
        {
        }
    }
}