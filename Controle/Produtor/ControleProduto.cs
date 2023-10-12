using Filantroplanta.Mock;
using Filantroplanta.Models;
using LazyCache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle.Produtor
{
    public class ControleProduto
    {
        public readonly IAppCache cache = new CachingService(); 
        public MockGeral mock = new MockGeral();

        public ControleProduto() { }

        public List<Produto> MockListaProdutos()
        {
            return BuscarListaProdutoCache();
        }

        //public Produto BuscarProduto(long produtoID)
        //{
        //    return ObterProduto(produtoID);
        //}

        public void SalvarAdicionarProduto(Produto produto)
        {
            long idProduto = 0;
            var lista = BuscarListaProdutoCache();

            if(lista != null && lista.Count > 0)
            {
                idProduto = lista.Max(i => i.Produto_ID);

                if (idProduto > 0)
                    produto.Produto_ID = idProduto + 1;
            }

            AdicionarProdutoCache(produto);

            AdicionarProdutoLista(produto);
        }

        public void ExcluirProduto(Produto produto)
        {
            var listaProdutos = BuscarListaProdutoCache();

            if (listaProdutos != null && listaProdutos.Count > 0)
            {
                if (listaProdutos.Contains(produto))
                {
                    listaProdutos.Remove(produto);
                    AtualizarLista(listaProdutos);
                }
            }
        }

        public void CriarListaProdutoCache()
        {
            AtualizarLista(mock.MockListaProdutos());
        }

        public void AdicionarProdutoLista(Produto produto)
        {
            var lista = BuscarListaProdutoCache();

            if(!lista.Contains(produto))
                lista.Add(produto);

            AtualizarLista(lista);
        }

        public void AtualizarLista(List<Produto> lista)
        {
            cache.GetOrAdd("ListaProduto", () =>
            {
                return lista;
            });
        }

        public List<Produto> BuscarListaProdutoCache()
        {
            return cache.Get<List<Produto>>("ListaProduto");
        }

        public Produto ObterProduto(long produtoID)
        {
            return cache.Get<Produto>($"Produto_{produtoID}");
        }

        public void AdicionarProdutoCache(Produto produto)
        {
            cache.GetOrAdd($"Produto_{produto.Produto_ID}", () =>
            {
                return produto;
            });
        }

        public List<Produto> ExcluirProdutoCache(Produto produto)
        {
            var listaProduto = BuscarListaProdutoCache();

            if(listaProduto.Contains(produto))
            {
                listaProduto.Remove(produto);
                AtualizarLista(listaProduto);
            }

            return listaProduto;
        }
    }
}
