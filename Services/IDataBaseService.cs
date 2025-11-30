using GestionTareas_Proyecto.Models;

public interface IDataBaseService
{
    public Task<List<GestionLista>> GetAllProductos();
    public Task<int> CreateTask(GestionLista lista);
    public Task<int> UpdateTask(GestionLista lista);
    public Task<int> DeleteProducto(GestionLista lista);
}