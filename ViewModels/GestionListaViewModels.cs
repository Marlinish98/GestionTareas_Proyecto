namespace GestionTareas_Proyecto.ViewModels;

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionTareas_Proyecto.Models;
using GestionTareas_Proyecto.Services;

public partial class GestionListaViewModels: ObservableObject
{
    private DataBaseService _dbService;

    [ObservableProperty]
    private GestionLista _TareaSeleccionada;

    [ObservableProperty]
    private ObservableCollection<GestionLista> _TaskCollection;

    public GestionListaViewModels()
    {
        _dbService = new DataBaseService();
        TaskCollection = new ObservableCollection<GestionLista>();
        CargarTareasCommand.ExecuteAsync(null);
        TareaSeleccionada = new GestionLista();
    }

    private void Alerta(string mensaje)
    {
        Application.Current!.MainPage!.DisplayAlert("", mensaje, "Aceptar");
    }

     [RelayCommand]
    private async Task CargarTareas()
    {
        List<GestionLista> listas =  await _dbService.GetAllTask();
        TaskCollection.Clear();

        foreach (GestionLista lista in listas)
        {
            TaskCollection.Add(lista);
        }
    }

    [RelayCommand]
    private async Task GuardarActualizarProducto()
    {
        try
        {
            if (TareaSeleccionada.Nombre is null || TareaSeleccionada.Nombre == "")
            {
                Alerta("Escriba el nombre de la Tarea");
                return;
            }

            if(TareaSeleccionada.FechaLimite<DateTime.Now)
            {
               Alerta("La tarea se encuentra Vencida");
               return; 
            }
            
            // aqui parte validacion
            /*if (ProductoSeleccionado.Id == 0)
            {
                await _dbService.CreateProducto(ProductoSeleccionado);
            } else
            {
                await _dbService.UpdateProdcuto(ProductoSeleccionado);
            }
            
            await LoadProductos();
            ProductoSeleccionado = new Producto();
            */


        }
        
        
        catch (Exception ex)
        {
            Alerta($"Ha ocurrido un error: {ex.Message}");
        }
    }
}