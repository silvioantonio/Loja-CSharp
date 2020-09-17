namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; internal set; }
        public Endereco Endereco { get; internal set; }
    }
}