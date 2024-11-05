﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpotifyApp.Storage.Sqlite;

#nullable disable

namespace SpotifyApp.Storage.Sqlite.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241105205534_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTimeOffset?>("AccessTokenExpiration")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("AuthenticationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OAuthTokens");
                });
#pragma warning restore 612, 618
        }
    }
}