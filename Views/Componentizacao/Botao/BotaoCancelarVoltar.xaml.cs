namespace Filantroplanta.Views.Componentizacao.Botao;

public partial class BotaoCancelarVoltar : ContentView
{
	public BotaoCancelarVoltar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TituloProperty = BindableProperty.Create(nameof(Titulo), typeof(string), typeof(BotaoCancelarVoltar));
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