using System;
using System.Net.Http;

namespace ChamarAPI
{
    class Program
    {
        private const string ApiUrl = "https://5ra1u9p6ii.execute-api.sa-east-1.amazonaws.com/ArquivoAws";
        private const string LambdaUrl = "https://hhxrsyip28.execute-api.sa-east-1.amazonaws.com/ArquivoAws";
        static void Main(string[] args)
        {
            HttpClient clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri(ApiUrl);
            HttpClient clientLambda = new HttpClient();
            clientLambda.BaseAddress = new Uri(LambdaUrl);
            for(int i = 0; i<100; i++) {
               HttpResponseMessage responseAPI = clientApi.GetAsync(String.Empty).Result;
               if (responseAPI.IsSuccessStatusCode) {
                     Console.WriteLine(String.Concat(i.ToString(),": OK - API"));
                 } else {
                     Console.WriteLine(String.Concat(i.ToString(),": NOK - API"));
                }
                HttpResponseMessage responseLambda = clientLambda.GetAsync(String.Empty).Result;
               if (responseLambda.IsSuccessStatusCode) {
                     Console.WriteLine(String.Concat(i.ToString(),": OK - Lambda"));
                 } else {
                     Console.WriteLine(String.Concat(i.ToString(),": NOK - Lambda"));
                }
            }
        }
    }
}
