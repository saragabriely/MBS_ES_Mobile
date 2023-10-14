using Filantroplanta.Mock;
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
        public MockGeral mock = new MockGeral();

        public void Logout()
        {
            AdicionarSalvarPessoaCache(null, "UsuarioLogado");
        }

        public void RegistrarUsuarioLogado(Models.Pessoa pessoa)
        {
            AdicionarSalvarPessoaCache(pessoa, "UsuarioLogado");
        }

        public Models.Pessoa ObterPessoa(string chave)
        {
            return cache.Get<Models.Pessoa>(chave);
        }

        public void AdicionarSalvarPessoaCache(Models.Pessoa pessoa, string chave)
        {
            cache.GetOrAdd(chave, () =>
            {
                return pessoa;
            });
        }

        public Models.Pessoa BuscarUsuarioLogado()
        {
            return ObterPessoa("UsuarioLogado");
        }

        public List<Models.Pessoa> BuscarListaPessoaCache()
        {
            return new List<Models.Pessoa> 
            {
                mock.MockProdutor(),
                mock.MockRestaurante()
            };
        }
    }
}
