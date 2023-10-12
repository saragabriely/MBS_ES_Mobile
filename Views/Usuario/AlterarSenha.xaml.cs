namespace Filantroplanta.Views.Usuario;

public partial class AlterarSenha : ContentPage
{
	public AlterarSenha()
	{
		InitializeComponent();
	}

	public void Voltar()
	{
		Navigation.PopAsync();
	}

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
		Voltar();
    }

    private void btnSalvar_Clicked(object sender, EventArgs e)
    {
		ValidarSenha();
    }

    private async void ValidarSenha()
    {
        var senha = entSenha.Text;
        var confirmarSenha = entConfirmaSenha.Text;

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