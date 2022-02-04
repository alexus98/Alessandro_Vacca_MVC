using Esercitazione.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.RepositoryEF.Configurations
{
    internal class PiattoConfiguration : IEntityTypeConfiguration<Piatto>
    {
        public void Configure(EntityTypeBuilder<Piatto> builder)
        {
            builder.ToTable("Piatti");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired();
            builder.Property(p => p.Prezzo).IsRequired();
            builder.Property(p => p.Prezzo).HasPrecision(9, 2);
            builder.Property(p => p.TipoPiatto).IsRequired();
            builder.Property(p => p.Descrizione).IsRequired();

            builder.HasOne(p => p.Menu).WithMany(m => m.Piatti).HasForeignKey(p => p.IdMenu);

        }
    }
}
