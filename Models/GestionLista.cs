namespace GestionTareas_Proyecto.Models;
using SQLite;

[Table("GestionLista")]
public class GestionLista
{
    [PrimaryKey, AutoIncrement]
    public int Id {get; set;}
    [NotNull]
    public string? Nombre {get;set;}
     [NotNull]
    public string? EstadoTarea {get;set;}
    public string? Descripcion {get;set;}
    [NotNull]
    public DateTime FechaLimite{get;set;} //fecha unica que se hara manual 
    public DateTime CambioFechaEstado{get;set;} // fecha automatica

}
