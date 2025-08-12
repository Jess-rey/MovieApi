using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    internal class MovieMapping : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Film)
                    .HasColumnName("film")
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(x => x.Genre)
                    .HasColumnName("genre")
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(x => x.Studio)
                    .HasColumnName("studio")
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(x => x.Score)
                    .HasColumnName("score")
                    .HasColumnType("INTEGER");

            builder.Property(x => x.Year)
                    .HasColumnName("year")
                    .HasColumnType("INTEGER");


        }
    }
}
