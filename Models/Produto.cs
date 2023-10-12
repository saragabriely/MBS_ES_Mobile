using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class Produto
    {
        public long Produto_ID { get; set; }
        public string Descricao { get; set; }
        public long Quantidade { get; set; }
        public decimal ValorPorKG { get; set; }
        public Pessoa mProdutor { get; set; }


        public Produto() { }

        public Produto(long Pessoa_ID)
        {
            this.Produto_ID = Pessoa_ID;
        }

        public Produto(string Descricao, long Quantidade, decimal ValorPorKG)
        {
            this.Descricao  = Descricao;
            this.Quantidade = Quantidade;
            this.ValorPorKG = ValorPorKG;
        }
    }
}
