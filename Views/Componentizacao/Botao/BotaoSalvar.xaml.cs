namespace Filantroplanta.Views.Componentizacao.Botao;

public partial class BotaoSalvar : ContentView
{
	public BotaoSalvar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TituloProperty = BindableProperty.Create(nameof(Titulo), typeof(string), typeof(BotaoSalvar));
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