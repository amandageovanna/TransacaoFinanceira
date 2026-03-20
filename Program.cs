using System;
using System.Collections.Generic;
using System.Linq;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Repositories;
using TransacaoFinanceira.Services;

namespace TransacaoFinanceira
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var transacoes = new List<Transacao>
            {
                new Transacao(1, "09/09/2023 14:15:00", 938485762L, 2147483649L, 150m),
                new Transacao(2, "09/09/2023 14:15:05", 2147483649L, 210385733L, 149m),
                new Transacao(3, "09/09/2023 14:15:29", 347586970L, 238596054L, 1100m),
                new Transacao(4, "09/09/2023 14:17:00", 675869708L, 210385733L, 5300m),
                new Transacao(5, "09/09/2023 14:18:00", 238596054L, 674038564L, 1489m),
                new Transacao(6, "09/09/2023 14:18:20", 573659065L, 563856300L, 49m),
                new Transacao(7, "09/09/2023 14:19:00", 938485762L, 2147483649L, 44m),
                new Transacao(8, "09/09/2023 14:19:01", 573659065L, 675869708L, 150m)
            };

            var contaRepository = new ContaRepository();
            var transacaoService = new TransacaoService(contaRepository);

            var transacoesOrdenadas = transacoes
            .OrderBy(transacao => DateTime.Parse(transacao.DataHora));

            foreach (var transacao in transacoesOrdenadas)
            {
                var resultado = transacaoService.Transferir(transacao);
                Console.WriteLine(resultado);
            }
        }
    }
}