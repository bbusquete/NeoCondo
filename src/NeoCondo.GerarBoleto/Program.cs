// See https://aka.ms/new-console-template for more information
using NeoCondo.GerarBoleto;

Console.WriteLine("Hello, World!");

var g = new GerarBoletoService();
var cobrancas = g.LerArquivoCobrancas();

foreach (var c in cobrancas)
{
    var ret = await g.CreateCobrancaAssync(c);
}

Console.WriteLine("Bye, World!");