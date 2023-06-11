using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Question;

namespace TopLearn.DataLayer.FluentConfigs.Question
{
    public class QuestionFluentConfigs : IEntityTypeConfiguration<TopLearn.DataLayer.Entities.Question.Question>
    {
        public void Configure(EntityTypeBuilder<Entities.Question.Question> builder)
        {
            builder.HasKey(q=>q.QuestionId);
            builder.Property(q => q.CourseId).IsRequired();
            builder.Property(q => q.UserId).IsRequired();
            builder.Property(q => q.Title).IsRequired().HasMaxLength(400);
            builder.Property(q => q.Body).IsRequired();
            builder.Property(q => q.CreateDate).IsRequired();

            builder.HasOne(q=>q.User).WithMany(q=>q.Questions).HasForeignKey(q=>q.UserId);
            builder.HasOne(q=>q.Course).WithMany(q=>q.Questions).HasForeignKey(q=>q.CourseId);
        }
    }
}
