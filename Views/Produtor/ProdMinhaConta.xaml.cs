using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Mock;

namespace Filantroplanta.Views.Produtor;

public partial class ProdMinhaConta : ContentPage
{
    public ControlePessoa controlePessoa = new ControlePessoa();

	public ProdMinhaConta()
	{
		InitializeComponent();
	}

    private void btnMeusDados_Clicked(object sender, EventArgs e)
    {
        var mock = new MockGeral();

        var pessoa = controlePessoa.BuscarPessoa(1); //mock.MockProdutor();

        Navigation.PushAsync(new Usuario.CadastroPessoa(pessoa));
    }

    private void btnAlterarSenha_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Usuario.AlterarSenha());
    }

    private void btnSair_Clicked(object sender, EventArgs e)
    {

    }
}