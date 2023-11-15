using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeOpenXml;
using NeoCondo.GerarBoleto.Entities;

namespace NeoCondo.GerarBoleto
{
    public class GerarBoletoService
    {
        public async Task<Uri> CreateCobrancaAssync(Cobranca cobranca)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.asaas.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("access_token", "$aact_YTU5YTE0M2M2N2I4MTliNzk0YTI5N2U5MzdjNWZmNDQ6OjAwMDAwMDAwMDAwMDAwNDIwNTg6OiRhYWNoX2VjNGRmOGJhLTkxNWUtNDQ5YS05ZjlmLTNkYmNmNjZjYWQzZA==");

            HttpResponseMessage response = await client.PostAsJsonAsync
                (
                "/api/v3/payments", cobranca);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public List<Cobranca> LerArquivoCobrancas()
        {
            var cobrancas = new List<Cobranca>();

            FileInfo file = new FileInfo(@"C:\Temp\modelo_new.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using(ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row + 1;

                for (int row = 2; row < rowCount; row++)
                {
                    var cobranca = new Cobranca();
                    //Variaveis
                    cobranca.Customer = worksheet.Cells[row, 2].Value.ToString();
                    cobranca.DueDate = Convert.ToDateTime(worksheet.Cells[row, 3].Value);
                    cobranca.Value = Convert.ToDecimal(worksheet.Cells[row, 4].Value);
                    cobranca.Description = worksheet.Cells[row, 5].Value.ToString();
                    cobranca.ExternalReference = worksheet.Cells[row, 2].Value.ToString() + DateTime.Now.ToString("yyyyMMdd");

                    //Valores Fixos
                    cobranca.BillingType = "BOLETO";
                    cobranca.PostalService = false;
                    cobranca.Fine = new CobrancaFine { value = 2};
                    cobranca.Interest = new CobrancaInterest { value = 2};

                    cobrancas.Add(cobranca);
                }
                
            }

                return cobrancas;
        }
    }
}
