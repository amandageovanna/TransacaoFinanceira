using TransacaoFinanceira.Models;
using TransacaoFinanceira.Repositories;

namespace TransacaoFinanceira.Services
{
    public class TransacaoService
    {
        private readonly IContaRepository _contaRepository;

        public TransacaoService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public string Transferir(Transacao transacao)
        {
            {
                var contaOrigem = _contaRepository.BuscarPorConta(transacao.ContaOrigem);
                var contaDestino = _contaRepository.BuscarPorConta(transacao.ContaDestino);

                if (contaOrigem == null || contaDestino == null)
                {
                    return $"Transacao numero {transacao.CorrelationId} foi cancelada por conta inexistente";
                }

                if (!contaOrigem.TemSaldoSuficiente(transacao.Valor))
                {
                    return $"Transacao numero {transacao.CorrelationId} foi cancelada por falta de saldo";
                }

                contaOrigem.Debitar(transacao.Valor);
                contaDestino.Creditar(transacao.Valor);

                return $"Transacao numero {transacao.CorrelationId} foi efetivada com sucesso! Novos saldos: Conta Origem:{contaOrigem.Saldo} | Conta Destino: {contaDestino.Saldo}";
            }
        }
    
}
