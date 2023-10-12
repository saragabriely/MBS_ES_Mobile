using Filantroplanta.Controle.Produtor;
using Filantroplanta.Models;

namespace Filantroplanta.Views.Produtor;

public partial class ProdControleEstoque : ContentPage
{
    public ControleProduto controleProduto = new ControleProduto();

	public ProdControleEstoque()
	{
		InitializeComponent();

        BuscarProdutos();
    }

    private void BuscarProdutos()
    {
        var ctrlProduto = new ControleProduto();
        var listaProdutos = ctrlProduto.MockListaProdutos();

        if(listaProdutos != null && listaProdutos.Count > 0)
        {
            lvControleEstoque.ItemsSource = listaProdutos;

            lvControleEstoque.IsVisible = true;
            lblListaVazia.IsVisible = false;
        }
        else
        {
            lblListaVazia.IsVisible     = true;
            lvControleEstoque.IsVisible = false;
        }
    }

    private void lvControleEstoque_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var produto = (Produto) e.SelectedItem;

        if (produto != null)
            NavegarTelaEstoque(produto);
    }

    private void NavegarTelaEstoque(Produto produto)
    {
        Navigation.PushAsync(new ProdEstoque(produto));
    }
}