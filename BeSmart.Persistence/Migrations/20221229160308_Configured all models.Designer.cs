﻿// <auto-generated />
using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeSmart.WebApi.Migrations
{
    [DbContext(typeof(BeSmartDbContext))]
    [Migration("20221229160308_Configured all models")]
    partial class Configuredallmodels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BeSmart.Domain.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Account_userEmail");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Account_userName");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Account_userPassword");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("AccoutType_Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("AccountType_Name");

                    b.HasKey("Id");

                    b.ToTable("AccountType");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Fidelity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Answer_text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Card_imageUrl");

                    b.Property<int>("LessonId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Card_text");

                    b.Property<string>("Transctipt")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Card_transcript");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Card_word");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("Category_name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("CountOfThemes")
                        .HasColumnType("integer")
                        .HasColumnName("Course_CountOfThemes");

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Course_Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedById");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Lesson_name");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Lesson_text");

                    b.Property<int>("ThemeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ThemeId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("TestId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Question_Text");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Test_Name");

                    b.Property<int>("QuestionsCount")
                        .HasColumnType("integer")
                        .HasColumnName("Test_QuestionsCount");

                    b.Property<int>("ThemeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ThemeId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountLesson")
                        .HasColumnType("integer")
                        .HasColumnName("Theme_CountLesson");

                    b.Property<int>("CountTest")
                        .HasColumnType("integer")
                        .HasColumnName("Theme_CountTest");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Theme_Name");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Account", b =>
                {
                    b.HasOne("BeSmart.Domain.Models.AccountType", "AccountType")
                        .WithMany("Accounts")
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountType");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Answer", b =>
                {
                    b.HasOne("BeSmart.Domain.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Card", b =>
                {
                    b.HasOne("BeSmart.Domain.Models.Lesson", "Lesson")
                        .WithMany("Cards")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Course", b =>
                {
                    b.HasOne("BeSmart.Domain.Models.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeSmart.Domain.Models.Account", "CreatedBy")
                        .WithMany("CreatedCourses")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Lesson", b =>
                {
                    b.HasOne("BeSmart.Domain.Models.Theme", "Theme")
                        .WithMany("Lessons")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Question", b =>
                {
                    b.HasOne("BeSmart.Domain.Models.Test", "Test")
                        .WithMany("Questsions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Test", b =>
                {
                    b.HasOne("BeSmart.Domain.Models.Theme", "Theme")
                        .WithMany("Tests")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Theme", b =>
                {
                    b.HasOne("BeSmart.Domain.Models.Course", "Course")
                        .WithMany("CourseThemes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Account", b =>
                {
                    b.Navigation("CreatedCourses");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.AccountType", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Category", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Course", b =>
                {
                    b.Navigation("CourseThemes");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Lesson", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Test", b =>
                {
                    b.Navigation("Questsions");
                });

            modelBuilder.Entity("BeSmart.Domain.Models.Theme", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Tests");
                });
#pragma warning restore 612, 618
        }
    }
}