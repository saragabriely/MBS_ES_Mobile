using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class TipoPessoa
    {
        public long TipoPessoa_ID { get; set; }
        public string descricao { get; set; }


        public const int Restaurante = 1;
        public const int Produtor = 2;
    }
}
