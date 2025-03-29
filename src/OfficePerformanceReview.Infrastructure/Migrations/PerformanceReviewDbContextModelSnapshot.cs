﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OfficePerformanceReview.Infrastructure;

#nullable disable

namespace OfficePerformanceReview.Infrastructure.Migrations
{
    [DbContext(typeof(PerformanceReviewDbContext))]
    partial class PerformanceReviewDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("eventseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("reservationseq", "dbo")
                .IncrementsBy(10);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<long>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("OfficeReview.Domain.Events.Root.EventLogs", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "eventseq");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("EventLogs", (string)null);
                });

            modelBuilder.Entity("OfficeReview.Domain.Profile.Root.Staff", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StaffGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StaffGuid")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OfficeReview.Domain.Questions.Entities.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("EvaluationFormTemplateId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<Guid>("QuestionGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationFormTemplateId");

                    b.HasIndex("QuestionGuid")
                        .IsUnique();

                    b.ToTable("Questions", (string)null);
                });

            modelBuilder.Entity("OfficeReview.Domain.Questions.Root.EvaluationFormTemplate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EvaluationFormGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationFormGuid")
                        .IsUnique();

                    b.ToTable("EvaluationFormTemplates", (string)null);
                });

            modelBuilder.Entity("OfficeReview.Domain.Events.Root.EventLogs", b =>
                {
                    b.OwnsOne("OfficeReview.Domain.Events.Enum.EventType", "EventType", b1 =>
                        {
                            b1.Property<long>("EventLogsId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("EventLogsId");

                            b1.ToTable("EventLogs");

                            b1.WithOwner()
                                .HasForeignKey("EventLogsId");
                        });

                    b.Navigation("EventType")
                        .IsRequired();
                });

            modelBuilder.Entity("OfficeReview.Domain.Profile.Root.Staff", b =>
                {
                    b.OwnsMany("OfficePerformanceReview.Domain.Profile.ValueObjects.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseHiLo(b1.Property<long>("Id"), "reservationseq", "dbo");

                            b1.Property<DateTime>("DateCreatedUtc")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("DateExpiresUtc")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)");

                            b1.HasKey("Id");

                            b1.ToTable("User_RefreshTokens", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.OwnsOne("OfficeReview.Domain.Profile.Enums.Team", "Team", b1 =>
                        {
                            b1.Property<long>("StaffId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("StaffId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("StaffId");
                        });

                    b.Navigation("RefreshTokens");

                    b.Navigation("Team")
                        .IsRequired();
                });

            modelBuilder.Entity("OfficeReview.Domain.Questions.Entities.Question", b =>
                {
                    b.HasOne("OfficeReview.Domain.Questions.Root.EvaluationFormTemplate", null)
                        .WithMany("Questions")
                        .HasForeignKey("EvaluationFormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("OfficePerformanceReview.Domain.Questions.Enum.QuestionType", "QuestionType", b1 =>
                        {
                            b1.Property<long>("QuestionId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("int")
                                .HasColumnName("QuestionType_Id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("QuestionType_Name");

                            b1.HasKey("QuestionId");

                            b1.ToTable("Questions");

                            b1.WithOwner()
                                .HasForeignKey("QuestionId");
                        });

                    b.Navigation("QuestionType")
                        .IsRequired();
                });

            modelBuilder.Entity("OfficeReview.Domain.Questions.Root.EvaluationFormTemplate", b =>
                {
                    b.OwnsOne("OfficeReview.Domain.Questions.Enum.FormEvaluation", "EvaluationType", b1 =>
                        {
                            b1.Property<long>("EvaluationFormTemplateId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("EvaluationFormTemplateId");

                            b1.ToTable("EvaluationFormTemplates");

                            b1.WithOwner()
                                .HasForeignKey("EvaluationFormTemplateId");
                        });

                    b.Navigation("EvaluationType")
                        .IsRequired();
                });

            modelBuilder.Entity("OfficeReview.Domain.Questions.Root.EvaluationFormTemplate", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
