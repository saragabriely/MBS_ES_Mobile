using Filantroplanta.Controle;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Mock;
using Filantroplanta.Models;
using Filantroplanta.Views.Componentizacao;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Filantroplanta.Views.Produtor;

public partial class ProdCadastroProduto : ContentPage
{
    public Produto produto { get; set; }
    public MockGeral mock = new MockGeral();
    public ControleProduto controleProduto  = new ControleProduto();
    public BotaoCancelar   btnCancelar      = new BotaoCancelar();
    public BotaoSalvar     btnSalvar        = new BotaoSalvar();
    public BotaoExcluirRecusar  btnExcluirRecusar = new BotaoExcluirRecusar();
    public LabelInput       labelInput = new LabelInput();
    public ControleComponentizacao controleComponente = new ControleComponentizacao();

    public ProdCadastroProduto()
	{
		InitializeComponent();

        this.BotoesCancelarSalvar("Finalizar");
    }

    private void BotoesCancelarSalvar(string descricaoSalvar)
    {
        hsBotoesSalvarCancelar.Children.Add(btnCancelar);
        hsBotoesSalvarCancelar.Children.Add(btnSalvar);

        var botaoSalvar = btnSalvar.FindByName<Button>(controleComponente.NomeBotaoSalvar);
        if (botaoSalvar != null)
        {
            botaoSalvar.Clicked += this.ButtonSalvar_Clicked;
            btnSalvar.Texto = descricaoSalvar;
        }

        var botaoCancelar = btnCancelar.FindByName<Button>(controleComponente.NomeBotaoCancelar);
        if (botaoCancelar != null)
        {
            botaoCancelar.Clicked += this.ButtonCancelar_Clicked;
            btnCancelar.Texto = "Cancelar";
        }
    }

    private void BotaoExcluir()
    {
        slBotaoExcluir.Children.Add(btnExcluirRecusar);

        var botaoExcluir = btnExcluirRecusar.FindByName<Button>(controleComponente.NomeBotaoExcluirRecusar);
        if (botaoExcluir != null)
        {
            botaoExcluir.Clicked  += this.ButtonExcluir_Clicked;
            botaoExcluir.IsVisible = true;
            btnExcluirRecusar.Texto = "Excluir";
        }
    }

    public ProdCadastroProduto(Produto produto)
    {
        InitializeComponent();

        this.BotoesCancelarSalvar("Salvar");
        this.BotaoExcluir();

        if (produto != null)
        {
            this.produto = produto;

            entNomeProduto.TextoEntry = this.produto.Descricao;
            entQtde.TextoEntry        = this.produto.Quantidade.ToString();
            entValorPorKG.TextoEntry  = this.produto.ValorPorKG.ToString();

            slBotaoExcluir.IsVisible   = true;
        }
    }

    private void ButtonSalvar_Clicked(object sender, EventArgs e)
    {
        RealizarCadastroProduto();
    }

    private async void ButtonExcluir_Clicked(object sender, EventArgs e)
    {
        if(this.produto != null)
        {
            bool resposta = await DisplayAlert("Exclusão", "Confirma a exclusão do produto?", "Sim", "Não");

            if(resposta)
                ExcluirProduto(this.produto);
        }
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
        long pessoaID   = 0;
        var nomeProduto = entNomeProduto.TextoEntry;
        var quantidade  = entQtde.TextoEntry;
        var valorPorKG  = entValorPorKG.TextoEntry;

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