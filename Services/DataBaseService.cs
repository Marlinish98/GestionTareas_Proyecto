namespace GestionTareas_Proyecto.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using GestionTareas_Proyecto.Models;
using SQLite;

public class DataBaseService : IDataBaseService
{
    private SQLiteAsyncConnection _db;

    public DataBaseService()
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Listado.db3");
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<GestionLista>().Wait();
        _db.ExecuteAsync("UPDATE GestionLista SET EstadoDB = 'Pendiente' WHERE EstadoDB IS NULL").Wait();

    }

    /// <summary>
    /// Creacion de Items enlistado
    /// </summary>
    /// <param name="lista"> variable para creacion de lista</param>
    /// <returns> Informacion completa que se ingresa </returns>
    public async Task<int> CreateTask(GestionLista lista)
    {
        return await _db.InsertAsync(lista);
    }

    /// <summary>
    /// Borrado unico de una tarea seleccionada
    /// </summary>
    /// <param name="lista"> variable para creacion de lista </param>
    /// <returns> El borrado de la tarea en la base de datos</returns>
    public async Task<int> DeleteTask(GestionLista lista)
    {
        return await _db.DeleteAsync(lista);
    }

    /// <summary>
    /// Obtiene todas las tareas guardadas en la base de datos
    /// </summary>
    /// <returns> Una lista de todas las tareas Guardadas</returns>
    public async Task<List<GestionLista>> GetAllTask()
    {
        return await _db.Table<GestionLista>().ToListAsync();
    }

    /// <summary>
    /// Actualiza el estado de dicha lista en esta caso de Pendiente a Completa
    /// </summary>
    /// <param name="lista">variable para creacion de lista</param>
    /// <returns>El cambio del estado de la tarea</returns>
    public async Task<int> UpdateTask(GestionLista lista)
    {
        return await _db.UpdateAsync(lista);
    }

    /// <summary>
    /// Boorado de manera general de todas las tareas
    /// </summary>
    /// <returns> Limpieza del listado completo </returns>
    public async Task DeleteAllTasks()
{
    await _db.DeleteAllAsync<GestionLista>();
}
}