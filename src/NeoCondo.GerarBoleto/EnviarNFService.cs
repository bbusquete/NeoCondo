using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeOpenXml;
using NeoCondo.GerarBoleto.Entities;

namespace NeoCondo.GerarBoleto
{
    public class EnviarNFService
    {
        public async Task<string> GetAuthorization()
        {

            String token = String.Empty;
            try
            {

                using (var client = new HttpClient())
                {
                    //TO-DO: Adicionar URL ao WebConfig
                    client.BaseAddress = new System.Uri("https://orbiadtsbayer.implantait.com/");
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //TO-DO: Adicionar URL ao WebConfig
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/orbia/v1/login", new LoginAutorization { email= "orbiabdts@implantait.com.br", senha = "a4552227cd9ad433829458f54b6514fd" });

                    if (response.IsSuccessStatusCode)
                    {
                        var retorno = await response.Content.ReadFromJsonAsync<LoginAutorizationResponse>();
                        if (retorno != null)
                        {
                            token = retorno.Authorization.ToString().Replace("Token ", "");
                        }
                    }

                    return token;
                }

            }
            catch (Exception ex)
            {
                return String.Empty;
            }

        }

        public async Task<int> PostSendInvoices(IList<SendNF> invoices, string token)
        {
            var returno = 1;
            try
            {

                using (var client2 = new HttpClient())
                {

                    client2.BaseAddress = new System.Uri("https://orbiadtsbayer.implantait.com/");
                    client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client2.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    client2.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client2.DefaultRequestHeaders.Add("Accept", "*/*");
                    client2.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client2.PostAsJsonAsync("api/orbia/v1/SendNF", invoices).ConfigureAwait(true);

                    if (response.IsSuccessStatusCode)
                    {
                        var retorno = await response.Content.ReadFromJsonAsync<SendNFResponse>();
                        returno = 99;
                    }
                }
                returno = 2;
                return returno;
            }
            catch (Exception ex)
            {
                returno = 3;
                return returno;
            }

            returno = 4;
            return returno;
        }

        public List<SendNF> GetInvoicesJson()
        {
            var listaNFs = new List<SendNF>();
            listaNFs.Add(new SendNF { Danfe = "6191", Numero= 6191, Serie= 17, DataEmissao = "2024-02-30T17:20:39", EmissorDocumento = "05429994001151", EmissorNome = "VEGETAL AGRONEGOCIOS LTDA", DestinatarioDocumento= "86813099191", DestinatarioNome = "ALAN CENCI", InfoAdicional = "" });

            return listaNFs;

        }

        public class LoginAutorization
        {
            public string email { get; set; }
            public string senha { get; set; }
        }

        public class LoginAutorizationResponse
        {
            public string Authorization { get; set; }
        }

        public class SendNF
        {
            public string Danfe { get; set; }
            public int Numero { get; set; }
            public int Serie { get; set; }
            public string DataEmissao { get; set; }

            public string EmissorDocumento { get; set; }
            public string EmissorNome { get; set; }
            public string DestinatarioDocumento { get; set; }
            public string DestinatarioNome { get; set; }
            public string InfoAdicional { get; set; }

        }


        public class SendNFResponse
        {
            public string TraceId { get; set; }
            public bool IsSuccess { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
