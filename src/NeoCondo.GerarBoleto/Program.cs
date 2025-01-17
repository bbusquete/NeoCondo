using Microsoft.Extensions.Configuration;
using NeoCondo.GerarBoleto;
using System.Configuration;

Console.WriteLine("Iniciando gerações de cobranças...");

GerarBoletoService g;

try
{
    var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

    string urlArquivo = config["AppSettings:UrlArquivo"];
    string urlAssas = config["AppSettings:UrlAssas"];
    string acessTokenAssas = config["AppSettings:AcessTokenAssas"];

    g = new GerarBoletoService(urlArquivo, urlAssas, acessTokenAssas);
    Console.WriteLine("Processando arquivo {0}", urlArquivo);

}
catch (Exception ex)
{
    Console.WriteLine("Erro ao carregar as configurações!");
    throw ex;
}


var cobrancas = g.LerArquivoCobrancas();
Console.WriteLine("Será gerado um total de {0} cobranças", cobrancas.Count);

foreach (var c in cobrancas)
{
    
    Console.WriteLine("Gerando cobrança da " + c.Customer);
    var ret = await g.CreateCobrancaAssync(c);
}

Console.WriteLine("Processo de Cobrança Finalizado");