using Filantroplanta.Controle.Produtor;
using Filantroplanta.Models;

namespace Filantroplanta.Views.Produtor;

public partial class ProdEstoque : ContentPage
{
    public Produto ProdutoSelecionado { get; set; }

	public ProdEstoque()
	{
		InitializeComponent();
	}

    public ProdEstoque(Produto produto)
    {
        InitializeComponent();

        PopularCampos(produto);
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        Voltar();
    }

    private void Voltar()
    {
        Navigation.PopAsync();
    }

    private async void btnSalvarProduto_Clicked(object sender, EventArgs e)
    {
        try
        {
            var quantidadeDigitada = entQtde.Text.Trim();

            if (!string.IsNullOrEmpty(quantidadeDigitada))
            {
                var qtdeNumerica = Convert.ToInt32(quantidadeDigitada);

                if (qtdeNumerica > 0)
                {
                    if (qtdeNumerica != this.ProdutoSelecionado.Quantidade)
                    {
                        var controleEstoque = new ControleEstoque();

                        this.ProdutoSelecionado.Quantidade = qtdeNumerica;

                        controleEstoque.AtualizarEstoque(new Estoque());

                        Voltar();
                    }
                }
            }
            else
            {
                LancarExcecaoCampoVazio("QUANTIDADE");
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Erro - Estoque", ex.Message, "OK");
        }
    }

    public void PopularCampos(Produto produto)
    {
        if(produto != null)
        {
            lblProduto.Text = produto.Descricao;
            entQtde.Text    = produto.Quantidade.ToString();

            ProdutoSelecionado = produto;
        }  
    }

    public async void LancarExcecaoCampoVazio(string campo)
    {
        await DisplayAlert("Campo Vazio", $"Popule o campo '{campo}'", "OK");
    }
}