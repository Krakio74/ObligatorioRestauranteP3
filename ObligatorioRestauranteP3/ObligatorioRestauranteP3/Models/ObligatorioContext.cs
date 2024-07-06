using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ObligatorioP3.Models;

public partial class ObligatorioContext : DbContext
{
    public ObligatorioContext()
    {
    }

    public ObligatorioContext(DbContextOptions<ObligatorioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Clima> Climas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<FotoMenu> FotoMenus { get; set; }

    public virtual DbSet<FotoRestaurante> FotoRestaurantes { get; set; }

    public virtual DbSet<FotoUsuario> FotoUsuarios { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Orden> Ordens { get; set; }

    public virtual DbSet<OrdenDetalle> OrdenDetalles { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Reseña> Reseñas { get; set; }

    public virtual DbSet<Restaurante> Restaurantes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    //public virtual DbSet<Login> Logins { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=localhost;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC27E645851C");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cliente__ID__3C69FB99");
        });

        modelBuilder.Entity<Clima>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clima__3214EC27D9248E40");

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
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC27B248631C");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Rango)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleado__ID__403A8C7D");
        });

        modelBuilder.Entity<FotoMenu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__FotoMenu__C99ED230FEDE4F1F");

            entity.ToTable("FotoMenu");

            entity.Property(e => e.MenuId).ValueGeneratedNever();

            entity.HasOne(d => d.Menu).WithOne(p => p.FotoMenu)
                .HasForeignKey<FotoMenu>(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FotoMenu__MenuId__4AB81AF0");
        });
        
        modelBuilder.Entity<FotoRestaurante>(entity =>
        {
            entity.HasKey(e => e.RestauranteId).HasName("PK__FotoRest__AAF3667B0EBE0B7E");

            entity.ToTable("FotoRestaurante");

            entity.Property(e => e.RestauranteId).ValueGeneratedNever();

            //entity.HasOne(d => d.Restaurante).WithOne(p => p.FotoRestaurante)
            //    .HasForeignKey<FotoRestaurante>(d => d.RestauranteId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__FotoResta__Resta__45F365D3");
        });

        modelBuilder.Entity<FotoUsuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__FotoUsua__2B3DE7B874FE19B2");
            

            entity.ToTable("FotoUsuario");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.Usuario).WithOne(p => p.FotoUsuario)
                .HasForeignKey<FotoUsuario>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FotoUsuar__Usuar__398D8EEE");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC27A8DA0DF5");

            entity.ToTable("Menu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombrePlato)
                .HasMaxLength(50)
                .IsUnicode(false);
            //entity.Property(e => e.Precio).HasColumnType(("numeric()"));
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mesa__3214EC278A878A88");

            entity.ToTable("Mesa");

            entity.HasIndex(e => new { e.NumeroMesa, e.Restauranteid }, "ukMesaRestaurante").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Restaurante).WithMany(p => p.Mesas)
                .HasForeignKey(d => d.Restauranteid)
                .HasConstraintName("FK__Mesa__Restaurant__4E88ABD4");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orden__3214EC273393CBBB");

            entity.ToTable("Orden");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descuento).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.Total).HasColumnType("numeric(20, 2)");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FK__Orden__ReservaId__5CD6CB2B");
        });

        modelBuilder.Entity<OrdenDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdenDet__3214EC27607CAC7D");

            entity.ToTable("OrdenDetalle");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Menu).WithMany(p => p.OrdenDetalles)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK__OrdenDeta__MenuI__60A75C0F");

            entity.HasOne(d => d.Orden).WithMany(p => p.OrdenDetalles)
                .HasForeignKey(d => d.OrdenId)
                .HasConstraintName("FK__OrdenDeta__Orden__5FB337D6");

            entity.HasOne(d => d.Restaurante).WithMany(p => p.OrdenDetalles)
                .HasForeignKey(d => d.RestauranteId)
                .HasConstraintName("FK__OrdenDeta__Resta__619B8048");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pago__3214EC27A9198358");

            entity.ToTable("Pago");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Monto).HasColumnType("numeric(14, 2)");
            entity.Property(e => e.ReservaId).HasColumnName("ReservaID");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FK__Pago__ReservaID__59FA5E80");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserva__3214EC27B66A03C9");

            entity.ToTable("Reserva");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Clienteid)
                .HasConstraintName("FK__Reserva__Cliente__5165187F");

            entity.HasOne(d => d.Mesa).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.MesaId)
                .HasConstraintName("FK__Reserva__MesaId__534D60F1");

            entity.HasOne(d => d.Restaurante).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.RestauranteId)
                .HasConstraintName("FK__Reserva__Restaur__52593CB8");
        });

        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reseña__3214EC2717D3C5C7");

            entity.ToTable("Reseña");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comentario)
                .HasMaxLength(255)
                .IsUnicode(false);


            entity.HasOne(d => d.Cliente).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Reseña__ClienteI__571DF1D5");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FK__Reseña__ReservaI__5629CD9C");
        });
        //modelBuilder.Entity<Restaurante>()
        //    .HasOne(d => d.FotoRestaurante)
        //    .WithOne(d => d.Restaurante)
        //    .HasForeignKey<FotoRestaurante>(d => d.RestauranteId)
        //    .IsRequired();
        modelBuilder.Entity<Restaurante>(entity =>
        {

            entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC27F8AC1771");
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
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Restaurante)
            .WithMany(r => r.Reservas)
            .HasForeignKey(r => r.RestauranteId);

        modelBuilder.Entity<Restaurante>()
        .HasMany(e => e.Reservas)
        .WithOne(e => e.Restaurante)
        .HasForeignKey(e => e.RestauranteId)
        .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC27A5486A8A");

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
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<ObligatorioP3.Models.Login> Login { get; set; } = default!;


}
