using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Mock;
using Filantroplanta.Models;

namespace Filantroplanta.Views.Produtor;

public partial class ProdMinhaConta : ContentPage
{
    public ControlePessoa controlePessoa = new ControlePessoa();
    public Pessoa usuarioLogado = new Pessoa();

	public ProdMinhaConta()
	{
		InitializeComponent();

        this.usuarioLogado = controlePessoa.BuscarUsuarioLogado();

        if (this.usuarioLogado != null)
            lblMinhaConta.Titulo = $"Olá, {this.usuarioLogado.Nome}";
    }

    private void btnMeusDados_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Usuario.CadastroPessoa(this.usuarioLogado));
    }

    private void btnAlterarSenha_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Usuario.AlterarSenha());
    }

    private async void btnSair_Clicked(object sender, EventArgs e)
    {
        bool resposta = await DisplayAlert("Saindo", "Realmente deseja sair?", "Sim", "Não");

        if (resposta)
        {
            controlePessoa.Logout();
            App.Current.MainPage = new Login();
        }
    }
}