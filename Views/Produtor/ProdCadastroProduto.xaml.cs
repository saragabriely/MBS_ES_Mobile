using Filantroplanta.Controle;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Mock;
using Filantroplanta.Models;
using Filantroplanta.Views.Componentizacao.Botao;
using System.Windows.Input;

namespace Filantroplanta.Views.Produtor;

public partial class ProdCadastroProduto : ContentPage
{
    public Produto produto { get; set; }
    public MockGeral mock = new MockGeral();
    public ControleProduto controleProduto = new ControleProduto();
    public BotoesCancelarSalvar btnCancelarSalvar = new BotoesCancelarSalvar();

    public ProdCadastroProduto()
	{
		InitializeComponent();

        AssociarComponentesCompartilhados();
    }

    private void AssociarComponentesCompartilhados()
    {
        AssociarBotaoCancelar();
        AssociarBotaoSalvar();
    }

    private void AssociarBotaoCancelar()
    {
        BotaoCancelarVoltar btnCancelar = new BotaoCancelarVoltar();
        hsBotoesSalvarCancelar.Children.Add(btnCancelar);

        Button cancelar = btnCancelar.FindByName<Button>("btnCancelarVoltar");
        cancelar.Clicked += this.ButtonCancelar_Clicked;
        cancelar.Text = "Voltar";
    }

    private void AssociarBotaoSalvar()
    {
        BotaoSalvar btnSalvar = new BotaoSalvar();
        hsBotoesSalvarCancelar.Children.Add(btnSalvar);

        Button salvar = btnSalvar.FindByName<Button>("btnCadastrarSalvar");
        salvar.Clicked += this.ButtonSalvarProduto_Clicked;
        salvar.Text = "Salvar";
    }

    public ProdCadastroProduto(Produto produto)
    {
        InitializeComponent();

        //this.BotoesCancelarSalvar("Cancelar", "Salvar");
        AssociarComponentesCompartilhados();

        if (produto != null)
        {
            this.produto = produto;

            entNomeProduto.Text = this.produto.Descricao;
            entQtde.Text        = this.produto.Quantidade.ToString();
            entValorPorKG.Text  = this.produto.ValorPorKG.ToString();

            btnExcluir_.IsVisible   = true;
        }
    }

    private void ButtonSalvarProduto_Clicked(object sender, EventArgs e)
    {
        RealizarCadastroProduto();
    }

    private async void ButtonExcluirProduto_Clicked(object sender, EventArgs e)
    {
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
        }
    }

    public async void LancarExcecaoCampoVazio(string campo)
    {
        await DisplayAlert("Campo Vazio", $"Popule o campo '{campo}'", "OK");
    }
}