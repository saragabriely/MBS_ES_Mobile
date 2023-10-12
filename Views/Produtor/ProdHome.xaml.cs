using Filantroplanta.Models;
using Filantroplanta.Mock;
using Filantroplanta.Controle.Produtor;

namespace Filantroplanta.Views.Produtor;

public partial class ProdHome : ContentPage
{
    public ProdHome()
	{
		InitializeComponent();

        BuscarPedidos();
    }

    private void Filantroplanta_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Filantro());
    }

    private void BuscarPedidos()
    {
        long idProdutor = 0;

        var controlePedidoProd = new ControlePedidoProdutor();

        var pedidos = controlePedidoProd.ListaPedidos(idProdutor);

        if(pedidos != null && pedidos.Count > 0)
        {
            var lstPendenteAprovacao = FiltrarPedido(pedidos, StatusPedido.Pendente_Aprovacao);
            var lstPendenteEnvio     = FiltrarPedido(pedidos, StatusPedido.Pendente_Envio);
            var lstFinalizados       = FiltrarPedido(pedidos, StatusPedido.Finalizado);
            var lstCancelados        = FiltrarPedido(pedidos, StatusPedido.Cancelado);

            lblPedidosPendenteAprovacao.Text = lstPendenteAprovacao.Count().ToString();
            lblPendenteEnvio.Text = lstPendenteEnvio.Count().ToString();
            lblFinalizados.Text   = lstFinalizados.Count().ToString();
            lblCancelados.Text    = lstCancelados.Count().ToString();
        }
        else
        {
            lblListaVazia.IsVisible     = true;
            gridResumoPedidos.IsVisible = false;
        }
    }

    public List<Pedido> FiltrarPedido(List<Pedido> pedidos, long statusPedido)
    {
        return pedidos.Where(i => i.mStatusPedido.StatusPedido_ID == statusPedido).ToList();
    }

}