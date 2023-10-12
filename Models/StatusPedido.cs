using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class StatusPedido
    {
        public long StatusPedido_ID { get; set; }
        public string Descricao { get; set; }

        public const int Pendente_Aprovacao = 1;
        public const int Pendente_Envio     = 2;
        public const int Finalizado         = 3;
        public const int Cancelado          = 4;
    }
}
