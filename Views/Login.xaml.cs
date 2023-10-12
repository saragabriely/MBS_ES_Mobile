using System.Windows.Input;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Mock;
using Filantroplanta.Models;
using Filantroplanta.Views.Produtor;
using Filantroplanta.Views.Usuario;
using LazyCache;

namespace Filantroplanta.Views;

public partial class Login : ContentPage
{
    public readonly IAppCache cache = new CachingService();

    public ControleProduto controleProduto = new ControleProduto();

    public Login()
	{
		InitializeComponent();
	}

    public async void btnLogin_Clicked(object sender, EventArgs e)
    {
        var pessoa = BuscarUsuario();

        if(pessoa != null && pessoa.Pessoa_ID > 0)
        {
            cache.GetOrAdd("Pessoa_" + pessoa.Pessoa_ID, () =>
            {
                return pessoa;
            });

            controleProduto.CriarListaProdutoCache();

            await Navigation.PushAsync(new ProdPaginaInicial());
        }
        else
            await DisplayAlert("Dados incorretos", "Usuário e/ou senha incorretos", "OK");
    }

    private Pessoa BuscarUsuario()
    {
        var login = entUsuario.Text;
        var senha = entSenha.Text;

        if(!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha))
        {
            var produtor = new MockGeral().MockProdutor();

            if(login.Equals(produtor.Email) && senha.Equals(produtor.Senha))
                return produtor;
        }

        return null;
    }
}