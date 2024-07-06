using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ObligatorioRestauranteP3.Models;

public partial class Op3v5Context : DbContext
{
    public Op3v5Context()
    {
    }

    public Op3v5Context(DbContextOptions<Op3v5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Clima> Climas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<FotoRestaurante> FotoRestaurantes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Orden> Ordens { get; set; }

    public virtual DbSet<OrdenDetalle> OrdenDetalles { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<PermisosUsuario> PermisosUsuarios { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Reseña> Reseñas { get; set; }

    public virtual DbSet<Restaurante> Restaurantes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=OP3V5;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC273891B22C");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(22)
                .IsUnicode(false);
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkClienteID");
        });

        modelBuilder.Entity<Clima>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clima__3214EC27FF2E80F8");

            entity.ToTable("Clima");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DescripcionClima)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Temperatura)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC27C74BB42B");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Modificar).HasColumnName("modificar");
            entity.Property(e => e.ResId).HasColumnName("ResID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkEmpleadoID");
        });

        modelBuilder.Entity<FotoRestaurante>(entity =>
        {
            entity.HasKey(e => e.RestauranteId).HasName("PK__FotoRest__AAF3667BA3052A49");

            entity.ToTable("FotoRestaurante");

            entity.Property(e => e.RestauranteId).ValueGeneratedNever();
            entity.Property(e => e.Foto)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.Restaurante).WithOne(p => p.FotoRestaurante)
                .HasForeignKey<FotoRestaurante>(d => d.RestauranteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FotoResta__Resta__44FF419A");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC27C1AF2A5D");

            entity.ToTable("Menu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombrePlato)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.IdRestaurantes).WithMany(p => p.IdMenus)
                .UsingEntity<Dictionary<string, object>>(
                    "MenuRestaurante",
                    r => r.HasOne<Restaurante>().WithMany()
                        .HasForeignKey("IdRestaurante")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("IdRestaurante"),
                    l => l.HasOne<Menu>().WithMany()
                        .HasForeignKey("IdMenu")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FkMenu"),
                    j =>
                    {
                        j.HasKey("IdMenu", "IdRestaurante").HasName("PK__MenuRest__FFE24EAE75CC1FE9");
                        j.ToTable("MenuRestaurante");
                    });
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mesa__3214EC27097A673C");

            entity.ToTable("Mesa");

            entity.HasIndex(e => new { e.NumeroMesa, e.Restauranteid }, "chkMesaRestaurante").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Restaurante).WithMany(p => p.Mesas)
                .HasForeignKey(d => d.Restauranteid)
                .HasConstraintName("FkMesaRestaurante");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orden__3214EC2758C59AAC");

            entity.ToTable("Orden");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DescCliente).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.DescClima).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.DescTemperatura).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.Total).HasColumnType("numeric(20, 2)");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FkReservaIdOrden");

            entity.HasOne(d => d.Restaurante).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.RestauranteId)
                .HasConstraintName("FkRestauranteIdOrdenDetalle");
        });

        modelBuilder.Entity<OrdenDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdenDet__3214EC276B6D0E31");

            entity.ToTable("OrdenDetalle");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Menu).WithMany(p => p.OrdenDetalles)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FkMenuIdOrdenDetalle");

            entity.HasOne(d => d.Orden).WithMany(p => p.OrdenDetalles)
                .HasForeignKey(d => d.OrdenId)
                .HasConstraintName("FkOrdenIdOrdenDetalle");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pago__3214EC2774B61C1D");

            entity.ToTable("Pago");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Monto).HasColumnType("numeric(14, 2)");
            entity.Property(e => e.ReservaId).HasColumnName("ReservaID");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FkReservaIdPago");
        });

        modelBuilder.Entity<PermisosUsuario>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PageAccess }).HasName("PK__Permisos__C6516914A82265F6");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.PageAccess)
                .HasMaxLength(24)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.PermisosUsuarios)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PermisosU__UserI__403A8C7D");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserva__3214EC27991ABE55");

            entity.ToTable("Reserva");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Clienteid)
                .HasConstraintName("FkClienteIdReserva");

            entity.HasOne(d => d.Mesa).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.MesaId)
                .HasConstraintName("FkMesaIdReserva");

            entity.HasOne(d => d.Restaurante).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.RestauranteId)
                .HasConstraintName("FkRestauranteIdReserva");
        });

        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reseña__3214EC27B3DC7B30");

            entity.ToTable("Reseña");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comentario)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FkClienteIdReseña");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FkReservaIdReseña");
        });

        modelBuilder.Entity<Restaurante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC27883AC825");

            entity.ToTable("Restaurante");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC27BD9EA1BF");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
