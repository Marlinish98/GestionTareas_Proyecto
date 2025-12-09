namespace GestionTareas_Proyecto.Models;
using SQLite;

[Table("GestionLista")]
public class GestionLista
{
    /// <summary>
    /// Creacion de Id para enlistar las diferentes tareas.
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// String de Nombre para poder validar que el campo no pase vacio.
    /// </summary>
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    /// <summary>
    /// Fecha con ingreso Manual para poder determinar la fecha limite de la tarea cargada.
    /// </summary>
    public DateTime FechaLimite { get; set; } 

    /// <summary>
    /// Se hizo de manera numerica con un rango de 1 a 3 para ordenar la lista.
    /// </summary>
    public int Prioridad { get; set; }

    /// <summary>
    /// Booleando para determinar el estado final de la tarea cuando se logro completar, con el uso del boton.
    /// </summary>
    public bool EstaCompleta { get; set; }
    /// <summary>
    /// Se toma en cuenta esta fecha para observar en la lista cuando se creo lista tarea.
    /// </summary>
    public DateTime FechaCreacion { get;set; } = DateTime.Now;
    public bool MostrarBotonCompletar => EstadoDB == "Pendiente";

    /// <summary>
    /// Implementado para uso de IOS, ya que al momento de cargar los datos
    /// en la base de datos no permite cargar los datos enlistado, uso netamente para IOS
    /// pero aplicativo para los distintos dispositvos.
    /// </summary>
    [NotNull]
    public string EstadoDB { get; set; } = "Pendiente";

    /// <summary>
    /// Fecha para actualizar la fecha que se comppleto dicha tarea.
    /// </summary>
    public DateTime? fechaTareaCompleta {get;set;}

    [Ignore]
    public string Estado
    {
        get
        {
            if (EstaCompleta)
                return "Completada";

            if (FechaLimite < DateTime.Now)
                return "Vencida";

            return "Pendiente";
        }
    }

    /// <summary>
    /// Inicializacion de fechas para evitar el colapso en la App, ya que 
    /// estan se reinician a años muy anteriores, lo que dificulta su actualizacion
    /// a las maa reciente
    /// </summary>
    public GestionLista()
    {
        FechaLimite = DateTime.Today; 
        FechaCreacion = DateTime.Now;
    }
    
}
