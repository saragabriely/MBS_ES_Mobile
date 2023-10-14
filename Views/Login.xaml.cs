using System.Windows.Input;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Mock;
using Filantroplanta.Models;
using Filantroplanta.Views.Produtor;
using Filantroplanta.Views.Restaurante;
using Filantroplanta.Views.Usuario;
using LazyCache;

namespace Filantroplanta.Views;

public partial class Login : ContentPage
{
    public readonly IAppCache cache = new CachingService();

    public ControleProduto controleProduto = new ControleProduto();
    public ControlePessoa controlePessoa   = new ControlePessoa();

    public Login()
	{
		InitializeComponent();
	}

    public void RealizarCadastro_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroPessoa());
    }

    public void btnLogin_Clicked(object sender, EventArgs e)
    {
        ValidarLogin();
    }

    private async void ValidarLogin()
    {
        var retorno = ValidarUsuario();

        if(retorno.Equals("Produtor"))
            //await Navigation.PushAsync(new ProdPaginaInicial());
            App.Current.MainPage = new ProdPaginaInicial();
        else if(retorno.Equals("Restaurante"))
            App.Current.MainPage = new RestPaginaInicial();
        //await Navigation.PushAsync(new RestPaginaInicial());
        else
            await DisplayAlert("Falha ao realizar login", retorno, "OK");
    }

    private string ValidarUsuario()
    {
        var login = entUsuario.Text;
        var senha = entSenha.Text;

        if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha))
        {
            var listaPessoa = controlePessoa.BuscarListaPessoaCache();

            if (listaPessoa != null && listaPessoa.Count() > 0)
            {
                var pessoa = listaPessoa.Where(i => i.Email.Equals(login)).FirstOrDefault();

                if (pessoa != null && pessoa.Pessoa_ID > 0)
                {
                    if (!pessoa.Senha.Equals(senha))
                        return "Senha incorreta, digite novamente";

                    controlePessoa.AdicionarSalvarPessoaCache(pessoa, $"Pessoa_{pessoa.Pessoa_ID}");

                    controlePessoa.RegistrarUsuarioLogado(pessoa);

                    controleProduto.CriarListaProdutoCache();

                    if (pessoa.mTipoPessoa.TipoPessoa_ID == TipoPessoa.Produtor)
                        return "Produtor";
                    else
                        return "Restaurante";
                }
                else
                    return "Faça o cadastro para acessar o aplicativo";
            }
            else
                return "Nenhum usuário foi encontrado na base";
        }
        else
            return "Preencha Usuário e Senha";
    }
}