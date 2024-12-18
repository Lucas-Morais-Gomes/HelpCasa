﻿// <auto-generated />
using System;
using HelpCasa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HelpCasa_Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241129225109_AddRatingFieldsToUser")]
    partial class AddRatingFieldsToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HelpCasa.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int?>("EmployerId")
                        .HasColumnType("integer");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("text");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("ServicePrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EmployerId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("HelpCasa.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("AverageRating")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfRatings")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ResetPasswordExpire")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("text");

                    b.Property<bool>("Subscription")
                        .HasColumnType("boolean");

                    b.Property<int>("TotalRating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("HelpCasa.Models.Employee", b =>
                {
                    b.HasBaseType("HelpCasa.Models.User");

                    b.Property<string>("AreaOfExpertise")
                        .HasColumnType("text");

                    b.Property<string>("AvailableTimeRange")
                        .HasColumnType("text");

                    b.Property<string>("Experience")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("HelpCasa.Models.Employer", b =>
                {
                    b.HasBaseType("HelpCasa.Models.User");

                    b.HasDiscriminator().HasValue("Employer");
                });

            modelBuilder.Entity("HelpCasa.Models.Service", b =>
                {
                    b.HasOne("HelpCasa.Models.Employee", "Employee")
                        .WithMany("OfferedServices")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HelpCasa.Models.Employer", "Employer")
                        .WithMany("ContractedServices")
                        .HasForeignKey("EmployerId");

                    b.Navigation("Employee");

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("HelpCasa.Models.Employee", b =>
                {
                    b.Navigation("OfferedServices");
                });

            modelBuilder.Entity("HelpCasa.Models.Employer", b =>
                {
                    b.Navigation("ContractedServices");
                });
#pragma warning restore 612, 618
        }
    }
}
