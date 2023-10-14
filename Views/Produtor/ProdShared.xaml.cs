using System.Windows.Input;

namespace Filantroplanta.Views.Produtor;

public partial class ProdShared : ContentView
{
	public static readonly BindableProperty TituloProperty = BindableProperty.Create(nameof(Titulo), typeof(string), typeof(ProdShared));
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

    public ProdShared()
	{
		InitializeComponent();
	}

    public virtual void btnCadastrarSalvarProduto_Clicked(object sender, EventArgs e)
    {

    }

    //public static readonly BindableProperty ChildCommandProperty = BindableProperty.Create(nameof(ChildCommand), typeof(ICommand), typeof(ProdShared));
	//
    //public ICommand ChildCommand
    //{
    //    get => (ICommand)GetValue(ChildCommandProperty);
    //    set => SetValue(ChildCommandProperty, value);
    //}
}