using System.Collections.Generic;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Repositories;
using TransacaoFinanceira.Services;

namespace TransacaoFinanceira.Tests
{
    public class TransacaoServiceTests
    {
        // Fake dentro do mesmo arquivo
        private class ContaRepositoryFake : IContaRepository
        {
            private readonly Dictionary<long, ContaSaldo> _contas;

            public ContaRepositoryFake(Dictionary<long, ContaSaldo> contas)
            {
                _contas = contas;
            }

            public ContaSaldo BuscarPorConta(long numeroConta)
            {
                return _contas.ContainsKey(numeroConta) ? _contas[numeroConta] : null;
            }
        }

        [Fact]
        public void Deve_Cancelar_Transacao_Quando_Conta_Origem_Nao_Existir()
        {
            var contas = new Dictionary<long, ContaSaldo>
            {
                { 2, new ContaSaldo(2, 100m) }
            };

            var repo = new ContaRepositoryFake(contas);
            var service = new TransacaoService(repo);
            var transacao = new Transacao(1, "09/09/2023 14:15:00", 1, 2, 10m);

            var resultado = service.Transferir(transacao);

            Assert.Equal("Transacao numero 1 foi cancelada por conta inexistente", resultado);
        }

        [Fact]
        public void Deve_Cancelar_Transacao_Quando_Conta_Destino_Nao_Existir()
        {
            var contas = new Dictionary<long, ContaSaldo>
            {
                { 1, new ContaSaldo(1, 100m) }
            };

            var repo = new ContaRepositoryFake(contas);
            var service = new TransacaoService(repo);
            var transacao = new Transacao(2, "09/09/2023 14:15:00", 1, 2, 10m);

            var resultado = service.Transferir(transacao);

            Assert.Equal("Transacao numero 2 foi cancelada por conta inexistente", resultado);
        }

        [Fact]
        public void Deve_Cancelar_Transacao_Quando_Nao_Houver_Saldo_Suficiente()
        {
            var contas = new Dictionary<long, ContaSaldo>
            {
                { 1, new ContaSaldo(1, 100m) },
                { 2, new ContaSaldo(2, 50m) }
            };

            var repo = new ContaRepositoryFake(contas);
            var service = new TransacaoService(repo);
            var transacao = new Transacao(3, "09/09/2023 14:15:00", 1, 2, 150m);

            var resultado = service.Transferir(transacao);

            Assert.Equal("Transacao numero 3 foi cancelada por falta de saldo", resultado);
            Assert.Equal(100m, contas[1].Saldo);
            Assert.Equal(50m, contas[2].Saldo);
        }

        [Fact]
        public void Deve_Realizar_Transferencia_Quando_Houver_Saldo_Suficiente()
        {
            var contas = new Dictionary<long, ContaSaldo>
            {
                { 1, new ContaSaldo(1, 200m) },
                { 2, new ContaSaldo(2, 50m) }
            };

            var repo = new ContaRepositoryFake(contas);
            var service = new TransacaoService(repo);
            var transacao = new Transacao(4, "09/09/2023 14:15:00", 1, 2, 150m);

            var resultado = service.Transferir(transacao);

            Assert.Equal("Transacao numero 4 foi efetivada com sucesso! Novos saldos: Conta Origem:50 | Conta Destino: 200", resultado);
            Assert.Equal(50m, contas[1].Saldo);
            Assert.Equal(200m, contas[2].Saldo);
        }
    }
}