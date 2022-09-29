namespace NotasAPI.Entities;

public partial class GrupoConUsuario : Entity
{
    public long IdUsuario { get; set; }
    public long IdGrupo { get; set; }

}

public partial class GrupoConUsuario
{
    public virtual Grupo IdGrupoNavigation { get; set; }
    public virtual Usuario IdUsuarioNavigation { get; set; }
}
