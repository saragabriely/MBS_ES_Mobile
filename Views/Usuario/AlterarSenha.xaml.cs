using Filantroplanta.Controle;
using Filantroplanta.Views.Componentizacao;

namespace Filantroplanta.Views.Usuario;

public partial class AlterarSenha : ContentPage
{
    public BotaoCancelar btnCancelar = new BotaoCancelar();
    public BotaoSalvar   btnSalvar   = new BotaoSalvar();
    public ControleComponentizacao controleComponente = new ControleComponentizacao();

    public AlterarSenha()
	{
		InitializeComponent();

        this.BotoesCancelarSalvar();
    }

    private void BotoesCancelarSalvar()
    {
        var botaoSalvar = btnSalvar.FindByName<Button>(controleComponente.NomeBotaoSalvar);
        if (botaoSalvar != null)
            botaoSalvar.Clicked += this.ButtonSalvar_Clicked;

        var botaoCancelar = btnCancelar.FindByName<Button>(controleComponente.NomeBotaoCancelar);
        if (botaoCancelar != null)
            botaoCancelar.Clicked += this.ButtonCancelar_Clicked;
    }

    public void Voltar()
	{
		Navigation.PopAsync();
	}

    private void ButtonCancelar_Clicked(object sender, EventArgs e)
    {
		Voltar();
    }

    private void ButtonSalvar_Clicked(object sender, EventArgs e)
    {
		ValidarSenha();
    }

    private async void ValidarSenha()
    {
        var senha          = entSenha.TextoEntry;
        var confirmarSenha = entConfirmaSenha.TextoEntry;

        if(string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confirmarSenha))
        {
            await DisplayAlert("Campo vazio", "Preencha todos os campos para confirmar a alteração da senha.", "Ok");
        }
        else if (!senha.Equals(confirmarSenha))
        {
            await DisplayAlert("Campo diferentes", "As senhas não coincidem.", "Ok");
        }
        else
        {
            AlteraSenha();

            await DisplayAlert("Alteração de Senha", "Senha alterada com sucesso!", "Ok");

            Voltar();
        }
    }

    private void AlteraSenha()
    {

    }
}