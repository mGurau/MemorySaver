﻿// <auto-generated />
using MemorySaver.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MemorySaver.Data.Migrations
{
    [DbContext(typeof(MemorySaverDBContext))]
    [Migration("20180129115219_AddedFacebookIdForFiles")]
    partial class AddedFacebookIdForFiles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MemorySaver.Domain.Entities.Chest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsPublic");

                    b.Property<string>("Name");

                    b.Property<Guid>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Chests");
                });

            modelBuilder.Entity("MemorySaver.Domain.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChestId");

                    b.Property<string>("Description");

                    b.Property<string>("FacebookId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ChestId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("MemorySaver.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MemorySaver.Domain.Entities.Chest", b =>
                {
                    b.HasOne("MemorySaver.Domain.Entities.User", "Owner")
                        .WithMany("OwnedChests")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MemorySaver.Domain.Entities.File", b =>
                {
                    b.HasOne("MemorySaver.Domain.Entities.Chest", "Chest")
                        .WithMany("FilesInChest")
                        .HasForeignKey("ChestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
