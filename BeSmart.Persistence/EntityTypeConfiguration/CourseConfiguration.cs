﻿using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Course_Name");

            builder.Property(x => x.CountOfThemes)
                .IsRequired()
                .HasColumnName("Course_CountOfThemes");

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.CreatedCourses)
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CategoryId);

            builder.HasMany(x => x.CourseThemes)
                .WithOne(x => x.Course);
        }
    }
}
