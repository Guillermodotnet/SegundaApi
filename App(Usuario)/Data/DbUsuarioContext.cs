using App_Usuario_.Models; // Importo el modelo Usuario que se usará para mapear la tabla
using Microsoft.EntityFrameworkCore; // Importo EF Core para trabajar con bases de datos

namespace App_Usuario_.Data
{
    // Esta clase representa mi contexto de base de datos
    // Aquí defino cómo se conectan mis modelos con las tablas de SQL Server
    public partial class DbUsuarioContext : DbContext
    {
        // Constructor vacío (puede ser usado si no se pasa configuración por inyección)
        public DbUsuarioContext()
        {
        }

        // Constructor principal que recibe las opciones de configuración del contexto
        // Este se usa normalmente con la inyección de dependencias
        public DbUsuarioContext(DbContextOptions<DbUsuarioContext> options)
            : base(options)
        {
        }

        // Defino el DbSet que representa la tabla "Usuario" en la base de datos
        // Cada fila de la tabla será un objeto de tipo Usuario
        public virtual DbSet<Usuario> Usuarios { get; set; }

        // Método para configurar cómo se mapea la entidad Usuario con la tabla de la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuro la entidad Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                // Defino la clave primaria de la tabla
                entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F6EF0162A");

                // Especifico el nombre real de la tabla en la base de datos
                entity.ToTable("Usuario");

                // Mapeo de cada propiedad con su columna correspondiente
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50) // Longitud máxima de 50 caracteres
                    .IsUnicode(false) // No usa Unicode (usa texto estándar)
                    .HasColumnName("apellidos"); // Nombre de columna en SQL

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime") // Tipo datetime en SQL
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            // Llamo al método parcial para permitir configuraciones adicionales si se agregan más adelante
            OnModelCreatingPartial(modelBuilder);
        }

        // Método parcial que puedo implementar en otro archivo si necesito agregar más configuraciones
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

