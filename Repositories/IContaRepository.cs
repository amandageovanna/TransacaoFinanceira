using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Repositories
{
    public interface IContaRepository
    {
        ContaSaldo BuscarPorConta(long numeroConta);
    }
}