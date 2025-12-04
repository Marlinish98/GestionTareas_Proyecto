using GestionTareas_Proyecto.Views;

namespace GestionTareas_Proyecto;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new GestionListaView();
	}
}
