namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Endereco
    {
        public Endereco()
        {
        }

        public Endereco(string cidade, string logradouro)
        {
            Cidade = cidade;
            Logradouro = logradouro;
        }

        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public Cliente Cliente { get; set; }
    }
}