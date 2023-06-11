using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.FluentConfigs.Question
{
    public class AnswerFluentConfigs : IEntityTypeConfiguration<TopLearn.DataLayer.Entities.Question.Answer>
    {
        public void Configure(EntityTypeBuilder<Entities.Question.Answer> builder)
        {
            builder.HasKey(a => a.AnswerId);
            builder.Property(a => a.QuestionId).IsRequired();
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.Body).IsRequired();
            builder.Property(a => a.CreateDate).IsRequired();

            builder.HasOne(a => a.User).WithMany(a => a.Answers).HasForeignKey(a => a.UserId);
            builder.HasOne(a => a.Question).WithMany(a => a.Answers).HasForeignKey(a => a.QuestionId);
        }
    }
}