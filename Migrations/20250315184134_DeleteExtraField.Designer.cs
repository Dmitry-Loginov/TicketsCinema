﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketsCinema.Models;

#nullable disable

namespace TicketsCinema.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20250315184134_DeleteExtraField")]
    partial class DeleteExtraField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TicketsCinema.Models.BookedSeat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<double>("PriceBooked")
                        .HasColumnType("float");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("SeatId");

                    b.HasIndex("UserId");

                    b.ToTable("BookedSeats");
                });

            modelBuilder.Entity("TicketsCinema.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PreviewUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ShortDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("TicketsCinema.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        },
                        new
                        {
                            Id = 4
                        },
                        new
                        {
                            Id = 5
                        },
                        new
                        {
                            Id = 6
                        },
                        new
                        {
                            Id = 7
                        },
                        new
                        {
                            Id = 8
                        },
                        new
                        {
                            Id = 9
                        },
                        new
                        {
                            Id = 10
                        },
                        new
                        {
                            Id = 11
                        },
                        new
                        {
                            Id = 12
                        },
                        new
                        {
                            Id = 13
                        },
                        new
                        {
                            Id = 14
                        },
                        new
                        {
                            Id = 15
                        },
                        new
                        {
                            Id = 16
                        },
                        new
                        {
                            Id = 17
                        },
                        new
                        {
                            Id = 18
                        },
                        new
                        {
                            Id = 19
                        },
                        new
                        {
                            Id = 20
                        },
                        new
                        {
                            Id = 21
                        },
                        new
                        {
                            Id = 22
                        },
                        new
                        {
                            Id = 23
                        },
                        new
                        {
                            Id = 24
                        },
                        new
                        {
                            Id = 25
                        },
                        new
                        {
                            Id = 26
                        },
                        new
                        {
                            Id = 27
                        },
                        new
                        {
                            Id = 28
                        },
                        new
                        {
                            Id = 29
                        },
                        new
                        {
                            Id = 30
                        },
                        new
                        {
                            Id = 31
                        },
                        new
                        {
                            Id = 32
                        },
                        new
                        {
                            Id = 33
                        },
                        new
                        {
                            Id = 34
                        },
                        new
                        {
                            Id = 35
                        },
                        new
                        {
                            Id = 36
                        },
                        new
                        {
                            Id = 37
                        },
                        new
                        {
                            Id = 38
                        },
                        new
                        {
                            Id = 39
                        },
                        new
                        {
                            Id = 40
                        },
                        new
                        {
                            Id = 41
                        },
                        new
                        {
                            Id = 42
                        },
                        new
                        {
                            Id = 43
                        },
                        new
                        {
                            Id = 44
                        },
                        new
                        {
                            Id = 45
                        },
                        new
                        {
                            Id = 46
                        },
                        new
                        {
                            Id = 47
                        },
                        new
                        {
                            Id = 48
                        },
                        new
                        {
                            Id = 49
                        },
                        new
                        {
                            Id = 50
                        },
                        new
                        {
                            Id = 51
                        },
                        new
                        {
                            Id = 52
                        },
                        new
                        {
                            Id = 53
                        },
                        new
                        {
                            Id = 54
                        },
                        new
                        {
                            Id = 55
                        },
                        new
                        {
                            Id = 56
                        },
                        new
                        {
                            Id = 57
                        },
                        new
                        {
                            Id = 58
                        },
                        new
                        {
                            Id = 59
                        },
                        new
                        {
                            Id = 60
                        },
                        new
                        {
                            Id = 61
                        },
                        new
                        {
                            Id = 62
                        },
                        new
                        {
                            Id = 63
                        },
                        new
                        {
                            Id = 64
                        },
                        new
                        {
                            Id = 65
                        },
                        new
                        {
                            Id = 66
                        },
                        new
                        {
                            Id = 67
                        },
                        new
                        {
                            Id = 68
                        },
                        new
                        {
                            Id = 69
                        },
                        new
                        {
                            Id = 70
                        },
                        new
                        {
                            Id = 71
                        },
                        new
                        {
                            Id = 72
                        },
                        new
                        {
                            Id = 73
                        },
                        new
                        {
                            Id = 74
                        },
                        new
                        {
                            Id = 75
                        },
                        new
                        {
                            Id = 76
                        },
                        new
                        {
                            Id = 77
                        },
                        new
                        {
                            Id = 78
                        },
                        new
                        {
                            Id = 79
                        },
                        new
                        {
                            Id = 80
                        },
                        new
                        {
                            Id = 81
                        },
                        new
                        {
                            Id = 82
                        },
                        new
                        {
                            Id = 83
                        },
                        new
                        {
                            Id = 84
                        },
                        new
                        {
                            Id = 85
                        },
                        new
                        {
                            Id = 86
                        },
                        new
                        {
                            Id = 87
                        },
                        new
                        {
                            Id = 88
                        },
                        new
                        {
                            Id = 89
                        },
                        new
                        {
                            Id = 90
                        },
                        new
                        {
                            Id = 91
                        },
                        new
                        {
                            Id = 92
                        },
                        new
                        {
                            Id = 93
                        },
                        new
                        {
                            Id = 94
                        },
                        new
                        {
                            Id = 95
                        },
                        new
                        {
                            Id = 96
                        },
                        new
                        {
                            Id = 97
                        },
                        new
                        {
                            Id = 98
                        },
                        new
                        {
                            Id = 99
                        },
                        new
                        {
                            Id = 100
                        },
                        new
                        {
                            Id = 101
                        },
                        new
                        {
                            Id = 102
                        },
                        new
                        {
                            Id = 103
                        },
                        new
                        {
                            Id = 104
                        },
                        new
                        {
                            Id = 105
                        },
                        new
                        {
                            Id = 106
                        },
                        new
                        {
                            Id = 107
                        },
                        new
                        {
                            Id = 108
                        },
                        new
                        {
                            Id = 109
                        },
                        new
                        {
                            Id = 110
                        },
                        new
                        {
                            Id = 111
                        },
                        new
                        {
                            Id = 112
                        },
                        new
                        {
                            Id = 113
                        },
                        new
                        {
                            Id = 114
                        },
                        new
                        {
                            Id = 115
                        },
                        new
                        {
                            Id = 116
                        },
                        new
                        {
                            Id = 117
                        },
                        new
                        {
                            Id = 118
                        },
                        new
                        {
                            Id = 119
                        },
                        new
                        {
                            Id = 120
                        },
                        new
                        {
                            Id = 121
                        },
                        new
                        {
                            Id = 122
                        },
                        new
                        {
                            Id = 123
                        },
                        new
                        {
                            Id = 124
                        },
                        new
                        {
                            Id = 125
                        },
                        new
                        {
                            Id = 126
                        },
                        new
                        {
                            Id = 127
                        },
                        new
                        {
                            Id = 128
                        },
                        new
                        {
                            Id = 129
                        },
                        new
                        {
                            Id = 130
                        },
                        new
                        {
                            Id = 131
                        },
                        new
                        {
                            Id = 132
                        },
                        new
                        {
                            Id = 133
                        },
                        new
                        {
                            Id = 134
                        },
                        new
                        {
                            Id = 135
                        },
                        new
                        {
                            Id = 136
                        },
                        new
                        {
                            Id = 137
                        },
                        new
                        {
                            Id = 138
                        },
                        new
                        {
                            Id = 139
                        },
                        new
                        {
                            Id = 140
                        },
                        new
                        {
                            Id = 141
                        },
                        new
                        {
                            Id = 142
                        },
                        new
                        {
                            Id = 143
                        },
                        new
                        {
                            Id = 144
                        },
                        new
                        {
                            Id = 145
                        },
                        new
                        {
                            Id = 146
                        },
                        new
                        {
                            Id = 147
                        },
                        new
                        {
                            Id = 148
                        },
                        new
                        {
                            Id = 149
                        },
                        new
                        {
                            Id = 150
                        },
                        new
                        {
                            Id = 151
                        },
                        new
                        {
                            Id = 152
                        },
                        new
                        {
                            Id = 153
                        },
                        new
                        {
                            Id = 154
                        },
                        new
                        {
                            Id = 155
                        },
                        new
                        {
                            Id = 156
                        },
                        new
                        {
                            Id = 157
                        },
                        new
                        {
                            Id = 158
                        },
                        new
                        {
                            Id = 159
                        },
                        new
                        {
                            Id = 160
                        },
                        new
                        {
                            Id = 161
                        },
                        new
                        {
                            Id = 162
                        },
                        new
                        {
                            Id = 163
                        },
                        new
                        {
                            Id = 164
                        },
                        new
                        {
                            Id = 165
                        },
                        new
                        {
                            Id = 166
                        },
                        new
                        {
                            Id = 167
                        },
                        new
                        {
                            Id = 168
                        },
                        new
                        {
                            Id = 169
                        },
                        new
                        {
                            Id = 170
                        },
                        new
                        {
                            Id = 171
                        },
                        new
                        {
                            Id = 172
                        },
                        new
                        {
                            Id = 173
                        },
                        new
                        {
                            Id = 174
                        },
                        new
                        {
                            Id = 175
                        },
                        new
                        {
                            Id = 176
                        },
                        new
                        {
                            Id = 177
                        },
                        new
                        {
                            Id = 178
                        },
                        new
                        {
                            Id = 179
                        },
                        new
                        {
                            Id = 180
                        },
                        new
                        {
                            Id = 181
                        },
                        new
                        {
                            Id = 182
                        },
                        new
                        {
                            Id = 183
                        },
                        new
                        {
                            Id = 184
                        },
                        new
                        {
                            Id = 185
                        },
                        new
                        {
                            Id = 186
                        },
                        new
                        {
                            Id = 187
                        },
                        new
                        {
                            Id = 188
                        },
                        new
                        {
                            Id = 189
                        },
                        new
                        {
                            Id = 190
                        },
                        new
                        {
                            Id = 191
                        },
                        new
                        {
                            Id = 192
                        },
                        new
                        {
                            Id = 193
                        },
                        new
                        {
                            Id = 194
                        },
                        new
                        {
                            Id = 195
                        },
                        new
                        {
                            Id = 196
                        },
                        new
                        {
                            Id = 197
                        },
                        new
                        {
                            Id = 198
                        },
                        new
                        {
                            Id = 199
                        },
                        new
                        {
                            Id = 200
                        });
                });

            modelBuilder.Entity("TicketsCinema.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<double>("Budget")
                        .HasColumnType("float");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TicketsCinema.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TicketsCinema.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketsCinema.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TicketsCinema.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TicketsCinema.Models.BookedSeat", b =>
                {
                    b.HasOne("TicketsCinema.Models.Movie", "Movie")
                        .WithMany("BookedSeats")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketsCinema.Models.Seat", "Seat")
                        .WithMany("BookedSeats")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketsCinema.Models.User", "User")
                        .WithMany("BookedSeats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Seat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TicketsCinema.Models.Movie", b =>
                {
                    b.Navigation("BookedSeats");
                });

            modelBuilder.Entity("TicketsCinema.Models.Seat", b =>
                {
                    b.Navigation("BookedSeats");
                });

            modelBuilder.Entity("TicketsCinema.Models.User", b =>
                {
                    b.Navigation("BookedSeats");
                });
#pragma warning restore 612, 618
        }
    }
}
