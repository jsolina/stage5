using Domain.AggregatesModel.ItemListAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.EntityConfigurations
{
    public class ItemListConfiguration : IEntityTypeConfiguration<ItemListAggregateModel>
    {
        public void Configure(EntityTypeBuilder<ItemListAggregateModel> builder)
        {
            builder.ToTable("itemlist");

            builder.Property(a => a.IdTask).HasColumnName("id_task");

            builder.Property(a => a.ItemName).HasColumnName("item_name");

            builder.Property(a => a.ItemDetails).HasColumnName("item_details");

            builder.Property(a => a.ItemStatus).HasColumnName("item_status");

            builder.Property(a => a.CreatedAt).HasColumnName("created_at");

            builder.Property(a => a.LastModified).HasColumnName("last_modified");
        }
    }
}
