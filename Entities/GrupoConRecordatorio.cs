namespace NotasAPI.Entities;

public partial class GrupoConRecordatorio : Entity
{
    public long IdGrupo { get; set; }
    public long IdRecordatorio { get; set; }

}

public partial class GrupoConRecordatorio
{
    public virtual Grupo IdGrupoNavigation { get; set; }
    public virtual Recordatorio IdRecordatorioNavigation { get; set; }
}
