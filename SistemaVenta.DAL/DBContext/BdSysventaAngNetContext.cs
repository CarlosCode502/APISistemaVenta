using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaVenta.DAL.DBContext;

public partial class BdSysventaAngNetContext : DbContext
{
    public BdSysventaAngNetContext()
    {
    }

    public BdSysventaAngNetContext(DbContextOptions<BdSysventaAngNetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategoria> TblCategorias { get; set; }

    public virtual DbSet<TblDetalleVenta> TblDetalleVenta { get; set; }

    public virtual DbSet<TblMenu> TblMenus { get; set; }

    public virtual DbSet<TblMenuRol> TblMenuRols { get; set; }

    public virtual DbSet<TblNumeroDocumento> TblNumeroDocumentos { get; set; }

    public virtual DbSet<TblProducto> TblProductos { get; set; }

    public virtual DbSet<TblRol> TblRols { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<TblVenta> TblVenta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local); DataBase=BD_SYSVENTA_ANG_NET; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCategoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Tbl_Cate__A3C02A10C2B0EBE8");

            entity.ToTable("Tbl_Categorias");

            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCategoria).HasMaxLength(50);
        });

        modelBuilder.Entity<TblDetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__Tbl_Deta__AAA5CEC2F8BF2BA3");

            entity.ToTable("Tbl_DetalleVenta");

            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TblDetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Tbl_Detal__IdPro__45F365D3");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.TblDetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Tbl_Detal__IdVen__44FF419A");
        });

        modelBuilder.Entity<TblMenu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Tbl_Menu__4D7EA8E1A7136043");

            entity.ToTable("Tbl_Menu");

            entity.Property(e => e.Icono).HasMaxLength(50);
            entity.Property(e => e.NombreMenu).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(50);
        });

        modelBuilder.Entity<TblMenuRol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("PK__Tbl_Menu__F8D2D5B6BB201D4B");

            entity.ToTable("Tbl_MenuRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.TblMenuRols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__Tbl_MenuR__IdMen__2E1BDC42");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TblMenuRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Tbl_MenuR__IdRol__2F10007B");
        });

        modelBuilder.Entity<TblNumeroDocumento>(entity =>
        {
            entity.HasKey(e => e.IdNumeroDocumento).HasName("PK__Tbl_Nume__6DFF4A6C0C7283F4");

            entity.ToTable("Tbl_NumeroDocumento");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TblProducto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Tbl_Prod__09889210308AFF00");

            entity.ToTable("Tbl_Productos");

            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreProducto).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.TblProductos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Tbl_Produ__IdCat__3A81B327");
        });

        modelBuilder.Entity<TblRol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Tbl_Rol__2A49584CCFEFBB30");

            entity.ToTable("Tbl_Rol");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Tbl_Usua__5B65BF977F5DF46D");

            entity.ToTable("Tbl_Usuario");

            entity.Property(e => e.Correo).HasMaxLength(80);
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto).HasMaxLength(110);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TblUsuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Tbl_Usuar__IdRol__31EC6D26");
        });

        modelBuilder.Entity<TblVenta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Tbl_Vent__BC1240BD4C4E74C8");

            entity.ToTable("Tbl_Venta");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NumeroDocumento).HasMaxLength(50);
            entity.Property(e => e.TipoPago).HasMaxLength(50);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
