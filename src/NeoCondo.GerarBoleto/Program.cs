// See https://aka.ms/new-console-template for more information
using NeoCondo.GerarBoleto;

Console.WriteLine("Iniciando gerações de cobranças...");

var g = new GerarBoletoService();
var cobrancas = g.LerArquivoCobrancas();
Console.WriteLine("Será gerado um total de {0} cobranças", cobrancas.Count);

foreach (var c in cobrancas)
{
    Console.WriteLine("Gerando cobrança da " + c.ExternalReference);
    var ret = await g.CreateCobrancaAssync(c);
}

Console.WriteLine("Processo de Cobrança Finalizado");