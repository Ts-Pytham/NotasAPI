namespace NotasAPI.Entities;

public partial class Rol
{
    public Rol()
    {
        Usuarios = new HashSet<Usuario>();
    }

    public short Id { get; set; }
    public string Nombre { get; set; }

}

public partial class Rol
{
    public virtual ICollection<Usuario> Usuarios { get; set; }
}
