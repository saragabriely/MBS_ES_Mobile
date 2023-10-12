using Filantroplanta.Models;

namespace Filantroplanta.Views.Produtor;

public partial class ProdPaginaInicial : TabbedPage
{
	public ProdPaginaInicial()
	{
		InitializeComponent();

        Navigation.RemovePage(new Login());
    }
}