using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class Pedido
    {
        public long Pedido_ID { get; set; }
        public Produto mProduto { get; set; }
        public Pessoa mCliente { get; set; }
        public StatusPedido mStatusPedido { get; set; }
        public long Quantidade { get; set; }
        public decimal ValorPorKG { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
