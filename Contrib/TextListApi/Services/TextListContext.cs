﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecAll.Contrib.TextList.Api.Models;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Design;

namespace RecAll.Contrib.TextList.Api.Services;
public class TextListContext : DbContext {

    public const string DefaultSchema = "textlist";
    public DbSet<TextItem> TextItems { get; set; }
    
    public TextListContext(DbContextOptions<TextListContext> options) :
        base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new TextItemConfiguration());
    }
}

public class TextItemConfiguration
    : IEntityTypeConfiguration<TextItem>
{
    public void Configure(EntityTypeBuilder<TextItem> builder)
    {
        builder.ToTable("textitems");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .UseHiLo("textitemseq", TextListContext.DefaultSchema);

        builder.Property(p => p.ItemId).IsRequired(false);
        builder.HasIndex(p => p.ItemId).IsUnique();

        builder.Property(p => p.Content).IsRequired();

        builder.Property(p => p.UserIdentityGuid).IsRequired();
        builder.HasIndex(p => p.UserIdentityGuid).IsUnique(false);

        builder.Property(p => p.IsDeleted).IsRequired();
    }

}

public class
    TextListContextDesignFactory : IDesignTimeDbContextFactory<
        TextListContext> {
    public TextListContext CreateDbContext(string[] args)
    {
        return new TextListContext(
            new DbContextOptionsBuilder<TextListContext>()
                .UseSqlServer("Server=.;Initial Catalog=RecAll.TextListDb;Integrated Security=true")
                .Options);
    }
}