namespace NotasAPI.DataAccess;

public partial class NotesContext : DbContext
{
    public NotesContext()
    {
    }

    public NotesContext(DbContextOptions<NotesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grupo> Grupos { get; set; }
    public virtual DbSet<GrupoConRecordatorio> GrupoConRecordatorios { get; set; }
    public virtual DbSet<GrupoConUsuario> GrupoConUsuarios { get; set; }
    public virtual DbSet<Recordatorio> Recordatorios { get; set; }
    public virtual DbSet<Rol> Rols { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server=apinotas.postgres.database.azure.com;Database=Notes;Port=5432;User Id=Pytham;Password=75322357Johan;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.ToTable("Grupo");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.IdMonitor).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdMonitorNavigation)
                .WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdMonitor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdMonitor");
        });

        modelBuilder.Entity<GrupoConRecordatorio>(entity =>
        {
            entity.ToTable("GrupoConRecordatorio");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.IdGrupo).ValueGeneratedOnAdd();

            entity.Property(e => e.IdRecordatorio).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdGrupoNavigation)
                .WithMany(p => p.GrupoConRecordatorios)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdGrupo");

            entity.HasOne(d => d.IdRecordatorioNavigation)
                .WithMany(p => p.GrupoConRecordatorios)
                .HasForeignKey(d => d.IdRecordatorio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdRecordatorio");
        });

        modelBuilder.Entity<GrupoConUsuario>(entity =>
        {
            entity.ToTable("GrupoConUsuario");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('\"GrupoUsuario_ID_seq\"'::regclass)");

            entity.Property(e => e.IdGrupo).HasDefaultValueSql("nextval('\"GrupoUsuario_IdGrupo_seq\"'::regclass)");

            entity.Property(e => e.IdUsuario).HasDefaultValueSql("nextval('\"GrupoUsuario_IdUsuario_seq\"'::regclass)");

            entity.HasOne(d => d.IdGrupoNavigation)
                .WithMany(p => p.GrupoConUsuarios)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdGrupo");

            entity.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.GrupoConUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdUsuario");
        });

        modelBuilder.Entity<Recordatorio>(entity =>
        {
            entity.ToTable("Recordatorio");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Prioridad)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Recordatorios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdUsuario");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("Rol");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Contraseña)
                .IsRequired()
                .HasMaxLength(512);

            entity.Property(e => e.Correo)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.IdRol).HasDefaultValueSql("nextval('\"Usuario_ID_Rol_seq\"'::regclass)");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.IdRolNavigation)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
