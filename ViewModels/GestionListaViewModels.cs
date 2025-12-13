namespace GestionTareas_Proyecto.ViewModels;

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionTareas_Proyecto.Models;
using GestionTareas_Proyecto.Services;

public partial class GestionListaViewModels : ObservableObject
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
        TareaSeleccionada = new GestionLista();
        CargarTareasCommand.ExecuteAsync(null);
    }

    /// <summary>
    /// Alerta para los mensajes en las validaciones 
    /// </summary>
    /// <param name="mensaje"></param>
    private void Alerta(string mensaje)
    {
        Application.Current!.MainPage?.DisplayAlert("", mensaje, "Aceptar");
    }
    /// <summary>
    /// OrderByDescending - Ordena la lista segun la fecha limite
    /// ThenByDescending - Hara uso de la prioridad para ordenarlo
    /// </summary>
    /// <returns> Retorno el orden de lista primero por fecha y luego prioridad</returns>
    [RelayCommand]
    private async Task CargarTareas()
    {
        List<GestionLista> listas = await _dbService.GetAllTask();
        var listasOrdenadas = listas
            .OrderByDescending(t => t.EstaCompleta)
            .ThenByDescending(t => t.Prioridad)
            .ToList();

        TaskCollection.Clear();

        foreach (GestionLista lista in listas)
        {
            TaskCollection.Add(lista);
        }
    }

    /// <summary>
    /// Se ejecuta validacion de que el nombre no este vacio, que la fecha limite sea menor
    /// que la fecha actual, se establece una validacion para que solo acepte los numeros
    /// establecidos en la parte de la prioridad y creando el listado de de las tareas
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task Validaciones()
    {
        try
        {

            if (TareaSeleccionada.Nombre is null || TareaSeleccionada.Nombre == "")
            {
                Alerta("Escriba el nombre de la Tarea");
                return;
            }


            if (TareaSeleccionada.FechaLimite.Date < DateTime.Now.Date)
            {
                Alerta("La tarea se encuentra Vencida");
                TareaSeleccionada.EstadoDB = "Vencida";
            }
            else
            {
                TareaSeleccionada.EstadoDB = "Pendiente";
            }

            if (TareaSeleccionada.Prioridad <= 0 || TareaSeleccionada.Prioridad >= 4)
            {
                Alerta("Numero fuera de Rango");
                return;
            }


            if (TareaSeleccionada.Id == 0)
            {
                TareaSeleccionada.EstaCompleta = false;
                await _dbService.CreateTask(TareaSeleccionada);
            }
            else
            {
                await _dbService.UpdateTask(TareaSeleccionada);
            }


            await CargarTareas();
            TareaSeleccionada = new GestionLista();

        }
        catch (Exception ex)
        {
            Alerta($"Ha ocurrido un error: {ex.Message}");
        }
    }

    /// <summary>
    /// Elimina las tareas de manera general, pero valida si la lista esta vacia y nos da
    /// una alerta que no existen tareas por eliminar
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task EliminarTarea()
    {
        try
        {
            if (TaskCollection.Count == 0)
            {
                Alerta("No hay tareas para eliminar");
                return;
            }

            bool respuesta = await Application.Current!.MainPage!.DisplayAlert(
                "ELIMINAR LISTADO",
                "¿Desea eliminar las tareas enlistadas?",
                "Si", "No"
            );


            if (respuesta)
            {
                await _dbService.DeleteAllTasks();

                TaskCollection.Clear();
                TareaSeleccionada = new GestionLista();

                Alerta("Tareas eliminadas exitosamente");
            }
        }
        catch (Exception ex)
        {
            Alerta($"Ha ocurrido un error: {ex.Message}");
        }
    }


    /// <summary>
    /// Elimina de manera individual cada items dentro de toda la informacion que trae la 
    /// tarea 
    /// </summary>
    /// <param name="tarea"></param>
    /// <returns></returns>
    [RelayCommand]
    private async Task EliminarTareaItem(GestionLista tarea)
    {
        try
        {
            if (tarea == null || tarea.Id == 0)
            {
                Alerta("Tarea inválida");
                return;
            }

            bool respuesta = await Application.Current!.MainPage!.DisplayAlert(
                "ELIMINAR TAREA",
                "¿Desea eliminar esta tarea?",
                "Si", "No"
            );

            if (respuesta)
            {
                await _dbService.DeleteTask(tarea);
                await CargarTareas();
                Alerta("Tarea eliminada exitosamente");
            }
        }
        catch (Exception ex)
        {
            Alerta($"Ha ocurrido un error: {ex.Message}");
        }
    }


    /// <summary>
    /// Hace un Update de la tarea, del cual ayuda a pasar de Pendiente a Completada
    /// </summary>
    /// <param name="tarea"></param>
    /// <returns></returns>
    [RelayCommand]
    private async Task CompletarTarea(GestionLista tarea)
    {
        if (tarea == null) return;

        tarea.EstaCompleta = true;
        tarea.EstadoDB = "Completada";
        tarea.fechaTareaCompleta = DateTime.Now;

        await _dbService.UpdateTask(tarea);
        await CargarTareas();
    }
}