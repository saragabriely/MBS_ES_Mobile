using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Models;
using Filantroplanta.Views.Produtor;
using LazyCache;
using System.Threading.Tasks;

namespace Filantroplanta.Views.Usuario;

public partial class CadastroPessoa : ContentPage
{
    public Pessoa pessoa { get; set; }

    public ControlePessoa controlePessoa = new ControlePessoa();

    public CadastroPessoa()
	{
		InitializeComponent();
        btnFinalizarSalvar.Text = "Finalizar";
    }

    public CadastroPessoa(Pessoa pessoaCadastrada)
    {
        InitializeComponent();

        btnFinalizarSalvar.Text = "Salvar";

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

    private void ButtonCancelar_Clicked(object sender, EventArgs e)
    {
        Voltar();
    }

    private async void ButtonSalvar_Clicked(object sender, EventArgs e)
    {
        await FinallizarOuSalvarCadastro();
    }

    public async Task FinallizarOuSalvarCadastro()
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

        if (string.IsNullOrEmpty(entNome.Text))
        {
            LancarExcecaoCampoVazio("NOME");
            return null;
        }
        else
            pessoa.Nome = entNome.Text;

        if (string.IsNullOrEmpty(entDocumento.Text))
        {
            LancarExcecaoCampoVazio("DOCUMENTO");
            return null;
        }
        else
            pessoa.Documento = entDocumento.Text;

        if (string.IsNullOrEmpty(entCep.Text))
        {
            LancarExcecaoCampoVazio("CEP");
            return null;
        }
        else 
            pessoa.CEP = entCep.Text;

        if (string.IsNullOrEmpty(entEndereco.Text))
        {
            LancarExcecaoCampoVazio("ENDEREÇO");
            return null;
        }
        else
            pessoa.Endereco = entEndereco.Text;

        if (string.IsNullOrEmpty(entNumero.Text))
        {
            LancarExcecaoCampoVazio("NUMERO");
            return null;
        }
        else
            pessoa.Numero = Convert.ToInt32(entNumero.Text);

        if (string.IsNullOrEmpty(entBairro.Text))
        {
            LancarExcecaoCampoVazio("BAIRRO");
            return null;
        }
        else
            pessoa.Bairro = entBairro.Text;

        if (string.IsNullOrEmpty(entCidade.Text))
        {
            LancarExcecaoCampoVazio("CIDADE");
            return null;
        }
        else
            pessoa.Cidade = entCidade.Text;

        if (string.IsNullOrEmpty(entEstado.Text))
        {
            LancarExcecaoCampoVazio("ESTADO");
            return null;
        }
        else
            pessoa.Estado = entEstado.Text;

        if (string.IsNullOrEmpty(entTelefone.Text))
        {
            LancarExcecaoCampoVazio("TELEFONE");
            return null;
        }
        else
            pessoa.Telefone = entTelefone.Text;

        if (string.IsNullOrEmpty(entEmail.Text))
        {
            LancarExcecaoCampoVazio("EMAIL");
            return null;
        }
        else
            pessoa.Email = entEmail.Text;

        if(pessoa.Pessoa_ID == 0)
        {
            if (string.IsNullOrEmpty(entSenha.Text))
            {
                LancarExcecaoCampoVazio("SENHA");
                return null;
            }
            else
                pessoa.Senha = entSenha.Text;

            if (string.IsNullOrEmpty(entConfirmaSenha.Text))
            {
                LancarExcecaoCampoVazio("CONFIRMAÇÃO DE SENHA");
                return null;
            }

            if (!entConfirmaSenha.Text.Equals(entSenha.Text))
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
        entNome.Text        = pessoa.Nome;
        entDocumento.Text   = pessoa.Documento;
        entCep.Text         = pessoa.CEP;
        entEndereco.Text    = pessoa.Endereco;
        entNumero.Text      = pessoa.Numero.ToString();
        entComplemento.Text = pessoa.Complemento;
        entBairro.Text      = pessoa.Bairro;
        entCidade.Text      = pessoa.Cidade;
        entEstado.Text      = pessoa.Estado;
        entTelefone.Text    = pessoa.Telefone;
        entEmail.Text       = pessoa.Email;

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