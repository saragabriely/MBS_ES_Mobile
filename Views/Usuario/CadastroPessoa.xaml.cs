using Filantroplanta.Controle;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Models;
using Filantroplanta.Views.Componentizacao;
using Filantroplanta.Views.Produtor;
using LazyCache;
using System.Threading.Tasks;

namespace Filantroplanta.Views.Usuario;

public partial class CadastroPessoa : ContentPage
{
    public Pessoa pessoa { get; set; }
    public ControlePessoa controlePessoa = new ControlePessoa();
    public BotaoCancelar btnCancelar = new BotaoCancelar();
    public BotaoSalvar   btnSalvar   = new BotaoSalvar();
    public ControleComponentizacao controleComponente = new ControleComponentizacao();

    public CadastroPessoa()
	{
		InitializeComponent();

        this.BotoesCancelarSalvar("Finalizar");
    }

    public CadastroPessoa(Pessoa pessoaCadastrada)
    {
        InitializeComponent();

        this.BotoesCancelarSalvar("Salvar");

        if (pessoaCadastrada != null)
        {
            this.pessoa = pessoaCadastrada;
            PopularCamposCadastro(pessoaCadastrada);
        }
        else
        {
            //VoltarTelaLogin();
        }
    }

    private void BotoesCancelarSalvar(string descricaoSalvar)
    {
        var botaoSalvar = btnSalvar.FindByName<Button>(controleComponente.NomeBotaoSalvar);
        if (botaoSalvar != null)
        {
            botaoSalvar.Clicked += this.ButtonSalvar_Clicked;
            btnSalvar.Texto = descricaoSalvar;
        }

        var botaoCancelar = btnCancelar.FindByName<Button>(controleComponente.NomeBotaoCancelar);
        if (botaoCancelar != null)
            botaoCancelar.Clicked += this.ButtonCancelar_Clicked;
    }

    private void ButtonCancelar_Clicked(object sender, EventArgs e)
    {
        Voltar();
    }

    private async void ButtonSalvar_Clicked(object sender, EventArgs e)
    {
        await FinalizarOuSalvarCadastro();
    }

    public async Task FinalizarOuSalvarCadastro()
    {
        var taskPessoa = ValidarCamposPopulados();
        var pessoa     = taskPessoa.Result as Pessoa;

        if(pessoa != null && pessoa.Pessoa_ID == 0)
        {
            var tp = rbRestaurante.IsChecked ? TipoPessoa.Restaurante : TipoPessoa.Produtor;

            pessoa.mTipoPessoa = new TipoPessoa { TipoPessoa_ID = tp };

            await DisplayAlert("Cadastro realizado", "Cadastro realizado com sucesso!", "OK");

            Voltar();
        }
        else if(pessoa != null && pessoa.Pessoa_ID > 0)
        {
            controlePessoa.AdicionarSalvarPessoaCache(this.pessoa, $"Pessoa_{this.pessoa.Pessoa_ID}");

            await DisplayAlert("Cadastro atualizado", "Cadastro atualizado com sucesso!", "OK");

            Voltar();
        }
    }

    public async Task<Pessoa> ValidarCamposPopulados()
    {
        var pessoa = new Pessoa();

        if (this.pessoa != null && this.pessoa.Pessoa_ID > 0)
            pessoa = this.pessoa;

        if (string.IsNullOrEmpty(entNome.TextoEntry))
        {
            LancarExcecaoCampoVazio("NOME");
            return null;
        }
        else
            pessoa.Nome = entNome.TextoEntry;

        if (string.IsNullOrEmpty(entDocumento.TextoEntry))
        {
            LancarExcecaoCampoVazio("DOCUMENTO");
            return null;
        }
        else
            pessoa.Documento = entDocumento.TextoEntry;

        if (string.IsNullOrEmpty(entCep.TextoEntry))
        {
            LancarExcecaoCampoVazio("CEP");
            return null;
        }
        else 
            pessoa.CEP = entCep.TextoEntry;

        if (string.IsNullOrEmpty(entEndereco.TextoEntry))
        {
            LancarExcecaoCampoVazio("ENDEREÇO");
            return null;
        }
        else
            pessoa.Endereco = entEndereco.TextoEntry;

        if (string.IsNullOrEmpty(entNumero.TextoEntry))
        {
            LancarExcecaoCampoVazio("NUMERO");
            return null;
        }
        else
            pessoa.Numero = Convert.ToInt32(entNumero.TextoEntry);

        if (string.IsNullOrEmpty(entBairro.TextoEntry))
        {
            LancarExcecaoCampoVazio("BAIRRO");
            return null;
        }
        else
            pessoa.Bairro = entBairro.TextoEntry;

        if (string.IsNullOrEmpty(entCidade.TextoEntry))
        {
            LancarExcecaoCampoVazio("CIDADE");
            return null;
        }
        else
            pessoa.Cidade = entCidade.TextoEntry;

        if (string.IsNullOrEmpty(entEstado.TextoEntry))
        {
            LancarExcecaoCampoVazio("ESTADO");
            return null;
        }
        else
            pessoa.Estado = entEstado.TextoEntry;

        if (string.IsNullOrEmpty(entTelefone.TextoEntry))
        {
            LancarExcecaoCampoVazio("TELEFONE");
            return null;
        }
        else
            pessoa.Telefone = entTelefone.TextoEntry;

        if (string.IsNullOrEmpty(entEmail.TextoEntry))
        {
            LancarExcecaoCampoVazio("EMAIL");
            return null;
        }
        else
            pessoa.Email = entEmail.TextoEntry;

        if(pessoa.Pessoa_ID == 0)
        {
            if (string.IsNullOrEmpty(entSenha.TextoEntry))
            {
                LancarExcecaoCampoVazio("SENHA");
                return null;
            }
            else
                pessoa.Senha = entSenha.TextoEntry;

            if (string.IsNullOrEmpty(entConfirmaSenha.TextoEntry))
            {
                LancarExcecaoCampoVazio("CONFIRMAÇÃO DE SENHA");
                return null;
            }

            if (!entConfirmaSenha.TextoEntry.Equals(entSenha.TextoEntry))
            {
                await DisplayAlert("Senha", "As senhas não coincidem", "OK");
                return null;
            }
        }

        return pessoa;
    }

    public async void LancarExcecaoCampoVazio(string campo)
    {
        await DisplayAlert("Campo Vazio", $"Popule o campo '{campo}'", "OK");
    }

    private void Voltar()
    {
        Navigation.PopAsync();
    }

    private void PopularCamposCadastro(Pessoa pessoa)
    {
        entNome.TextoEntry       = pessoa.Nome;
        entDocumento.TextoEntry  = pessoa.Documento;
        entCep.TextoEntry        = pessoa.CEP;
        entEndereco.TextoEntry   = pessoa.Endereco;
        entNumero.TextoEntry     = pessoa.Numero.ToString();
        entComplemento.TextoEntry = pessoa.Complemento;
        entBairro.TextoEntry     = pessoa.Bairro;
        entCidade.TextoEntry     = pessoa.Cidade;
        entEstado.TextoEntry     = pessoa.Estado;
        entTelefone.TextoEntry   = pessoa.Telefone;
        entEmail.TextoEntry      = pessoa.Email;

        lblPerfilUsuario.Text = pessoa.mTipoPessoa.TipoPessoa_ID == TipoPessoa.Produtor ? "Produtor" : "Restaurante";

        DesabilitarCampoAlteracaoCadastro();
    }

    private void DesabilitarCampoAlteracaoCadastro()
    {
        stPerfil.IsVisible          = false;
        stSenha.IsVisible           = false;
        stPerfilCadastro.IsVisible  = true;
        lblPrimeiroCadastro.IsVisible = false;
        lblMeuCadastro.IsVisible    = true;
    }
}