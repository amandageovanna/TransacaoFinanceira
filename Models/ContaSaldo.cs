namespace TransacaoFinanceira.Models
{
    public class ContaSaldo
    {
        public long Conta { get; }
        public decimal Saldo { get; private set; }

        public ContaSaldo(long conta, decimal saldo)
        {
            Conta = conta;
            Saldo = saldo;
        }

        public bool TemSaldoSuficiente(decimal valor)
        {
            return Saldo >= valor;
        }

        public void Debitar(decimal valor)
        {
            Saldo -= valor;
        }

        public void Creditar(decimal valor)
        {
            Saldo += valor;
        }
    }
}