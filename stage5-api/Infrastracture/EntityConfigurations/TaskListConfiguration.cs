using Domain.AggregatesModel.TaskListAggregate.cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.EntityConfigurations
{
    public class TaskListConfiguration : IEntityTypeConfiguration<TaskListAggregateModel>
    {
        public void Configure(EntityTypeBuilder<TaskListAggregateModel> builder)
        {
            builder.ToTable("tasklist");

            builder.Property(a => a.TaskName).HasColumnName("task_name");

            builder.Property(a => a.TaskDetails).HasColumnName("task_details");

            builder.Property(a => a.Email).HasColumnName("email");

            builder.Property(a => a.CreatedAt).HasColumnName("created_at");

            builder.Property(a => a.LastModified).HasColumnName("last_modified");
        }
    }
}
