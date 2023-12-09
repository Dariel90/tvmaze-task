﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TvMaze.Persistence;

#nullable disable

namespace TvMaze.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231208215818_FixRelations")]
    partial class FixRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TvMaze.Domain.Networks.Network", b =>
                {
                    b.Property<Guid>("SysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficialSite")
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Country", "TvMaze.Domain.Networks.Network.Country#Country", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Timezone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("SysId");

                    b.ToTable("Networks", (string)null);
                });

            modelBuilder.Entity("TvMaze.Domain.Shows.Show", b =>
                {
                    b.Property<Guid>("SysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AverageRuntime")
                        .HasColumnType("int");

                    b.Property<string>("DvdCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Ended")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NetworkSysId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OfficialSite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Premiered")
                        .HasColumnType("datetime2");

                    b.Property<int>("Runtime")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Updated")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebChannel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("Externals", "TvMaze.Domain.Shows.Show.Externals#Externals", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Imdb")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("TheTvDb")
                                .HasColumnType("int");

                            b1.Property<int>("TvRage")
                                .HasColumnType("int");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Image", "TvMaze.Domain.Shows.Show.Image#Image", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Medium")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Original")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Links", "TvMaze.Domain.Shows.Show.Links#Link", b1 =>
                        {
                            b1.IsRequired();

                            b1.ComplexProperty<Dictionary<string, object>>("PreviousEpisode", "TvMaze.Domain.Shows.Show.Links#Link.PreviousEpisode#Url", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("Href")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");
                                });

                            b1.ComplexProperty<Dictionary<string, object>>("Self", "TvMaze.Domain.Shows.Show.Links#Link.Self#Url", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("Href")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");
                                });
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Rating", "TvMaze.Domain.Shows.Show.Rating#Rating", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Average")
                                .HasColumnType("decimal(18,2)");
                        });

                    b.HasKey("SysId");

                    b.HasIndex("NetworkSysId");

                    b.ToTable("Shows", (string)null);
                });

            modelBuilder.Entity("TvMaze.Domain.Shows.Show", b =>
                {
                    b.HasOne("TvMaze.Domain.Networks.Network", "Network")
                        .WithMany("Shows")
                        .HasForeignKey("NetworkSysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("TvMaze.Domain.Shows.Schedule", "Schedules", b1 =>
                        {
                            b1.Property<Guid>("ShowSysId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("DayOfTheWeek")
                                .HasColumnType("int");

                            b1.Property<TimeOnly>("Time")
                                .HasColumnType("time");

                            b1.HasKey("ShowSysId", "Id");

                            b1.ToTable("Schedule");

                            b1.WithOwner()
                                .HasForeignKey("ShowSysId");
                        });

                    b.OwnsMany("TvMaze.SharedKernel.Entities.Genre", "Genres", b1 =>
                        {
                            b1.Property<Guid>("ShowSysId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Category")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ShowSysId", "Id");

                            b1.ToTable("Genre");

                            b1.WithOwner()
                                .HasForeignKey("ShowSysId");
                        });

                    b.Navigation("Genres");

                    b.Navigation("Network");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("TvMaze.Domain.Networks.Network", b =>
                {
                    b.Navigation("Shows");
                });
#pragma warning restore 612, 618
        }
    }
}