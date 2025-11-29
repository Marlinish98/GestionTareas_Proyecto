namespace GestionTareas_Proyecto.Models;
using SQLite;

[Table("GestionLista")]
public class GestionLista
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [NotNull]
    public string? Nombre {get;set;}
    public string? Descripcion {get;set;}
    public DateTime FechaLimite{get;set;}
    [NotNull]
    public string? EstadoTarea {get;set;}
    public DateTime CambioFecha{get;set;}

}
