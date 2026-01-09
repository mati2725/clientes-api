using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd_Intuit.Domain.Entities;

namespace BackEnd_Intuit.Infrastructure.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Nombre)
                   .HasColumnName("nombre")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.Apellido)
                   .HasColumnName("apellido")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.RazonSocial)
                   .HasColumnName("razon_social")
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(c => c.Cuit)
                   .HasColumnName("cuit")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasIndex(c => c.Cuit)
                   .IsUnique();

            builder.Property(c => c.FechaNacimiento)
                   .HasColumnName("fecha_nacimiento")
                   .IsRequired();

            builder.Property(c => c.TelefonoCelular)
                   .HasColumnName("telefono_celular")
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(c => c.Email)
                   .HasColumnName("email")
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasIndex(c => c.Email)
                   .IsUnique();

            builder.Property(c => c.FechaCreacion)
                   .HasColumnName("fecha_creacion")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(c => c.FechaModificacion)
                   .HasColumnName("fecha_modificacion")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
