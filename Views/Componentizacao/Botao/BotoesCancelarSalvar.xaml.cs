namespace Filantroplanta.Views.Componentizacao.Botao;

public partial class BotoesCancelarSalvar : ContentView
{
	public BotoesCancelarSalvar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TituloProperty = BindableProperty.Create(nameof(Titulo), typeof(string), typeof(BotoesCancelarSalvar));
    public string Titulo
    {
        get
        {
            return GetValue(TituloProperty) as string;
        }
        set
        {
            SetValue(TituloProperty, value);
        }
    }
}