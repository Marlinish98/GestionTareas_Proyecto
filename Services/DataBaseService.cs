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
        _db.CreateTableAsync<GestionLista>();
    }

    public async Task<int> CreateTask(GestionLista lista)
    {
        return await _db.InsertAsync(lista);
    }

    public async Task<int> DeleteTask(GestionLista lista)
    {
        return await _db.DeleteAsync(lista);
    }

    public async Task<List<GestionLista>> GetAllTask()
    {
        return await _db.Table<GestionLista>().ToListAsync();
    }

    public async Task<int> UpdateTask(GestionLista lista)
    {
        return await _db.UpdateAsync(lista);
    }
}