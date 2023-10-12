using Filantroplanta.Controle.Produtor;
using Filantroplanta.Models;

namespace Filantroplanta.Views.Produtor;

public partial class ProdPedidos : ContentPage
{
	public ProdPedidos()
	{
		InitializeComponent();

        BuscarListaPedidos();
    }

    public void BuscarListaPedidos()
    {
        //long produtorID = 0;
        //var controlePedido = new ControlePedidoProdutor();
        //
        //var listaPedidos = controlePedido.BuscarPedidos(produtorID);
        //
        //if (listaPedidos.Count > 0)
        //{
        //    lvListaPedidos.ItemsSource = listaPedidos;
        //    lvListaPedidos.IsVisible = true;
        //}
        //else
        //{
        //    lvListaPedidos.IsVisible = false;
        //    lblListaVazia.IsVisible = true;
        //}
    }

    private void ListaPedidos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}