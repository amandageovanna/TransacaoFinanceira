namespace TransacaoFinanceira.Models
{
    public class Transacao
    {
        public int CorrelationId { get; }
        public string DataHora { get; }
        public long ContaOrigem { get; }
        public long ContaDestino { get; }
        public decimal Valor { get; }

        public Transacao(int correlationId, string dataHora, long contaOrigem, long contaDestino, decimal valor)
        {
            CorrelationId = correlationId;
            DataHora = dataHora;
            ContaOrigem = contaOrigem;
            ContaDestino = contaDestino;
            Valor = valor;
        }
    }
}