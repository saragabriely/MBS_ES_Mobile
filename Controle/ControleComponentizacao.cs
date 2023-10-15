using Filantroplanta.Views.Componentizacao.Botao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle
{
    public class ControleComponentizacao
    {
        public BotoesCancelarSalvar btnCancelarSalvar = new BotoesCancelarSalvar();
        public ControleComponentizacao() { }

        public Button BuscarBotao(string nomeBotao)
        {
            return btnCancelarSalvar.FindByName<Button>(nomeBotao);
        }
    }
}
