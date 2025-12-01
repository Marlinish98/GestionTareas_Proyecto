using GestionTareas_Proyecto.Models;

public interface IDataBaseService
{
    public Task<List<GestionLista>> GetAllTask();
    public Task<int> CreateTask(GestionLista lista);
    public Task<int> UpdateTask(GestionLista lista);
    public Task<int> DeleteTask(GestionLista lista);
}