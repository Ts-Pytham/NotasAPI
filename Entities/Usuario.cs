namespace NotasAPI.Entities;

public partial class Usuario : Entity
{
    public Usuario()
    {
        GrupoConUsuarios = new HashSet<GrupoConUsuario>();
        Grupos = new HashSet<Grupo>();
    }

    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Contraseña { get; set; }
    public int Codigo { get; set; }
    public short IdRol { get; set; }

}

public partial class Usuario
{
    public virtual Rol IdRolNavigation { get; set; }
    public virtual ICollection<GrupoConUsuario> GrupoConUsuarios { get; set; }
    public virtual ICollection<Grupo> Grupos { get; set; }
}
