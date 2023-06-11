using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.DataLayer.FluentConfigs.Order
{
    public class DiscountFluentConfigs : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(c=>c.DiscountId);
            builder.Property(d => d.DiscountCode).IsRequired().HasMaxLength(100);
        }
    }
}
