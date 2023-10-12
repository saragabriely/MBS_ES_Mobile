using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class Pessoa
    {
        public long Pessoa_ID { get; set; }
        public string Nome { get; set;}
        public TipoPessoa mTipoPessoa { get; set; }
        public string Documento { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }


        public Pessoa() { }

        public Pessoa(long Pessoa_ID)
        {
            this.Pessoa_ID = Pessoa_ID;
        }

        public Pessoa(string Nome, TipoPessoa mTipoPessoa,string Documento, string CEP, string Endereco, 
            int Numero, string Complemento, string Cidade, string Estado, string Telefone, string Email, string Senha)
        {
            this.Nome        = Nome;
            this.mTipoPessoa = mTipoPessoa;
            this.Documento   = Documento;
            this.CEP         = CEP;
            this.Endereco    = Endereco;   
            this.Numero      = Numero;
            this.Complemento = Complemento;
            this.Estado      = Estado;
            this.Telefone    = Telefone;
            this.Email       = Email;
            this.Senha       = Senha;
        }
    }
}
