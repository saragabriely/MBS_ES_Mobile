using Filantroplanta.Models;
using Filantroplanta.Mock;
using Filantroplanta.Controle.Produtor;

namespace Filantroplanta.Views.Produtor;

public partial class ProdProdutos : ContentPage
{
    public ControleProduto controleProduto = new ControleProduto();

	public ProdProdutos()
	{
		InitializeComponent();

        BuscarProdutos();
    }

    public ProdProdutos(List<Produto> produtos)
    {
        InitializeComponent();

        listaProdutos.ItemsSource = produtos;
    }

    private void BuscarProdutos()
    {
        var lista = controleProduto.MockListaProdutos();

        if(lista != null && lista.Count > 0)
            listaProdutos.ItemsSource = lista;
        else
            lblListaVazia.IsVisible = true;
    }

    private void ButtonAdicionarProduto_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProdCadastroProduto());
    }

    private void listaProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var produto = (Produto)e.SelectedItem;
        
        if (produto != null)
            NavegarTelaCadastro(produto);
    }

    private void NavegarTelaCadastro(Produto produto)
    {
        Navigation.PushAsync(new ProdCadastroProduto(produto));
    }
}