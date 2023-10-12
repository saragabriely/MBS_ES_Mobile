using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle.Pessoa
{
    public class ControlePessoa
    {
        public readonly IAppCache cache = new CachingService();

        public Models.Pessoa BuscarPessoa(long pessoaID)
        {
            return ObterPessoa(pessoaID);
        }

        public Models.Pessoa ObterPessoa(long pessoaID)
        {
            var teste = cache.Get<Models.Pessoa>($"Pessoa_{pessoaID}");

            return teste;
        }

        public void SalvarPessoa(Models.Pessoa pessoa)
        {
            cache.GetOrAdd("Pessoa_" + pessoa.Pessoa_ID, () =>
            {
                return pessoa;
            });
        }
    }
}
