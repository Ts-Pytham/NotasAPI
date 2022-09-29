namespace NotasAPI.Entities;

public partial class Recordatorio : Entity
{
    public Recordatorio()
    {
        GrupoConRecordatorios = new HashSet<GrupoConRecordatorio>();
    }

    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Prioridad { get; set; }
    public DateTime Fecha { get; set; }

}

public partial class Recordatorio
{
    public virtual ICollection<GrupoConRecordatorio> GrupoConRecordatorios { get; set; }
}
