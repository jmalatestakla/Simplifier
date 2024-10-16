﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simplifier.Entities;

#nullable disable

namespace Simplifier.Entities.Migrations
{
    [DbContext(typeof(SimplifierContext))]
    partial class SimplifierContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Simplifier.Entities.Application", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RawText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uuid");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Simplifier.Entities.FormFields", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExpectedResponse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormField")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uuid");

                    b.HasIndex("TemplateId");

                    b.ToTable("FormFields");
                });

            modelBuilder.Entity("Simplifier.Entities.FormResponses", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FormField")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uuid");

                    b.HasIndex("ApplicationId");

                    b.ToTable("FormResponses");
                });

            modelBuilder.Entity("Simplifier.Entities.Template", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uuid");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("Simplifier.Entities.User", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uuid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Simplifier.Entities.FormFields", b =>
                {
                    b.HasOne("Simplifier.Entities.Template", null)
                        .WithMany("FormFields")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Simplifier.Entities.FormResponses", b =>
                {
                    b.HasOne("Simplifier.Entities.Application", null)
                        .WithMany("FormResponses")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Simplifier.Entities.Application", b =>
                {
                    b.Navigation("FormResponses");
                });

            modelBuilder.Entity("Simplifier.Entities.Template", b =>
                {
                    b.Navigation("FormFields");
                });
#pragma warning restore 612, 618
        }
    }
}
