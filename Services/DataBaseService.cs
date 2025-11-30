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

    public Task<int> CreateTask(GestionLista lista)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteProducto(GestionLista lista)
    {
        throw new NotImplementedException();
    }

    public Task<List<GestionLista>> GetAllProductos()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateTask(GestionLista lista)
    {
        throw new NotImplementedException();
    }
}