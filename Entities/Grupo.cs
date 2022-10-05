namespace NotasAPI.Entities;

public partial class Grupo : Entity
{
    public Grupo()
    {
        GrupoConRecordatorios = new HashSet<GrupoConRecordatorio>();
        GrupoConUsuarios = new HashSet<GrupoConUsuario>();
    }

    public int Codigo { get; set; }
    public long IdMonitor { get; set; }
    public string Nombre { get; set; }

}

public partial class Grupo
{
    public virtual Usuario IdMonitorNavigation { get; set; }
    public virtual ICollection<GrupoConRecordatorio> GrupoConRecordatorios { get; set; }
    public virtual ICollection<GrupoConUsuario> GrupoConUsuarios { get; set; }
}
