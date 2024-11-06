﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpotifyApp.Storage.Sqlite;

#nullable disable

namespace SpotifyApp.Storage.Sqlite.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("SpotifyApp.Storage.Contracts.Models.OAuthToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccessToken")
                        .HasColumnType("TEXT");

                    b.Property<long?>("AccessTokenExpiration")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("AuthenticationTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OAuthTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
