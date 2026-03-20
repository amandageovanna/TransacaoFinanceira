using System.Collections.Generic;
using System.Linq;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly List<ContaSaldo> _contas;

        public ContaRepository()
        {
            _contas = new List<ContaSaldo>
            {
                new ContaSaldo(938485762L, 180m),
                new ContaSaldo(347586970L, 1200m),
                new ContaSaldo(2147483649L, 0m),
                new ContaSaldo(675869708L, 4900m),
                new ContaSaldo(238596054L, 478m),
                new ContaSaldo(573659065L, 787m),
                new ContaSaldo(210385733L, 10m),
                new ContaSaldo(674038564L, 400m),
                new ContaSaldo(563856300L, 1200m)
            };
        }

        public ContaSaldo BuscarPorConta(long numeroConta)
        {
            return _contas.FirstOrDefault(conta => conta.Conta == numeroConta);
        }
    }
}