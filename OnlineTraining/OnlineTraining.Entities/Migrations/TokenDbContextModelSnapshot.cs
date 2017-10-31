﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineTraining.Entities.Db;
using System;

namespace OnlineTraining.Entities.Migrations
{
    [DbContext(typeof(TokenDbContext))]
    partial class TokenDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("OnlineTraining.Entities.Auth.RToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ClientName")
                        .HasColumnName("client_name");

                    b.Property<int>("IsStop")
                        .HasColumnName("isstop");

                    b.Property<string>("RefreshToken")
                        .HasColumnName("refresh_token");

                    b.HasKey("Id");

                    b.ToTable("RTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
