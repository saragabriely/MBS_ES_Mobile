using Filantroplanta.Controle.Produtor;
using Filantroplanta.Mock;
using Filantroplanta.Models;

namespace Filantroplanta.Views.Produtor;

public partial class ProdCadastroProduto : ContentPage
{
    public Produto produto { get; set; }
    public MockGeral mock = new MockGeral();
    public ControleProduto controleProduto = new ControleProduto();

    public ProdCadastroProduto()
	{
		InitializeComponent();
	}

    public ProdCadastroProduto(Produto produto)
    {
        InitializeComponent();
        
        if(produto != null)
        {
            this.produto = produto;

            entNomeProduto.Text = this.produto.Descricao;
            entQtde.Text        = this.produto.Quantidade.ToString();
            entValorPorKG.Text  = this.produto.ValorPorKG.ToString();

            btnExcluir_.IsVisible   = true;
            btnSalvar_.IsVisible    = true;
            btnCadastrar_.IsVisible = false;
        }
    }

    private void ButtonSalvarProduto_Clicked(object sender, EventArgs e)
    {
        RealizarCadastroProduto();
    }

    private void ButtonCadastrarProduto_Clicked(object sender, EventArgs e)
    {
        RealizarCadastroProduto();
    }

    private async void ButtonExcluirProduto_Clicked(object sender, EventArgs e)
    {
        //produto = this.produto;

        if(this.produto != null)
            ExcluirProduto(this.produto);
        else
            await DisplayAlert("Produto vazio", "Ocorreu algum problema com o produto", "OK");
    }

    private void ExcluirProduto(Produto produto)
    {
        if(produto != null && produto.Produto_ID > 0)
        {
            var listaProdutos = controleProduto.ExcluirProdutoCache(produto);

            Voltar(listaProdutos);

            //NavegarListaProdutos();
        }
    }

    private void ButtonCancelar_Clicked(object sender, EventArgs e)
    {
        Voltar();
    }

    public void Voltar(List<Produto> listaProdutos)
    {
        Navigation.PushAsync(new ProdProdutos(listaProdutos));
    }

    public void Voltar()
    {
        Navigation.PopAsync();
        //Navigation.PopModalAsync();
    }

    public void NavegarListaProdutos()
    {
        Voltar();
    }

    private async void RealizarCadastroProduto()
    {
        long pessoaID = 0;
        var nomeProduto = entNomeProduto.Text;
        var quantidade  = entQtde.Text;
        var valorPorKG  = entValorPorKG.Text;

        if (string.IsNullOrEmpty(nomeProduto))
            LancarExcecaoCampoVazio("NOME DO PRODUTO");

        else if (string.IsNullOrEmpty(quantidade))
            LancarExcecaoCampoVazio("QUANTIDADE");

        else if (string.IsNullOrEmpty(valorPorKG))
            LancarExcecaoCampoVazio("VALOR");

        else
        {
            var controleProduto = new ControleProduto();

            if(this.produto != null && this.produto.Produto_ID > 0)
            {
                this.produto.Descricao  = nomeProduto;
                this.produto.Quantidade = Convert.ToInt64(quantidade);
                this.produto.ValorPorKG = Convert.ToDecimal(valorPorKG);

                controleProduto.SalvarAdicionarProduto(this.produto);

                await DisplayAlert("Cadastro atualizado", "Cadastro atualizado com sucesso!", "OK");
            }
            else
            {
                var novo = new Produto();

                novo.Descricao = nomeProduto;
                novo.Quantidade = Convert.ToInt64(quantidade);
                novo.ValorPorKG = Convert.ToDecimal(valorPorKG);
                novo.mProdutor  = new Pessoa { Pessoa_ID = pessoaID };

                controleProduto.SalvarAdicionarProduto(novo);

                await DisplayAlert("Cadastro realizado", "Cadastro realizado com sucesso!", "OK");
            }

            Voltar(controleProduto.BuscarListaProdutoCache());

            //NavegarListaProdutos();
        }
    }

    public async void LancarExcecaoCampoVazio(string campo)
    {
        await DisplayAlert("Campo Vazio", $"Popule o campo '{campo}'", "OK");
    }
}