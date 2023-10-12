using Filantroplanta.Mock;
using Filantroplanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle.Produtor
{
    public class ControlePedidoProdutor
    {
        public ControlePedidoProdutor() { }

        public List<Pedido> ListaPedidos(long produtorID)
        {
            return BuscarPedidos(produtorID);
        }

        public List<Pedido> BuscarPedidos(long produtorID)
        {
            var lista = new List<Pedido>();

            if (produtorID == 0)
            {
                var mock = new MockGeral();

                lista.Add(mock.MockPedidoPendenteAprovacao());
                lista.Add(mock.MockPedidoPendenteEnvio());
                lista.Add(mock.MockPedidoFinalizado());
                lista.Add(mock.MockPedidoCancelado());
            }
            else
            {
                // consulta na base
            }            

            return lista;
        }


    }
}
