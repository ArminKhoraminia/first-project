﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.Permission;
using TopLearn.DataLayer.Entities.Question;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;
using TopLearn.DataLayer.FluentConfigs.Order;
using TopLearn.DataLayer.FluentConfigs.Question;

namespace TopLearn.DataLayer.Context
{
    public class TopLearnContext : DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext> options) : base(options)
        {

        }

        #region User
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDiscount> UserDiscount { get; set; }
        // edite comment
        // for push git
        // edit again 2

        #endregion

        #region Wallet
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<WalletType> WalletType { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion  

        #region Course

        public DbSet<Course> Course { get; set; }
        public DbSet<CourseEpisode> CourseEpisode { get; set; }
        public DbSet<CourseGroup> CourseGroup { get; set; }
        public DbSet<CourseLevel> CourseLevel { get; set; }
        public DbSet<CourseStatus> CourseStatus { get; set; }
        public DbSet<UserCourse> UserCourse { get; set; }
        public DbSet<CourseComment> CourseComment { get; set; }
        public DbSet<CourseVote> CourseVote { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Discount> Discount { get; set; }

        #endregion

        #region Question

        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }

        #endregion

        #region Query Filter

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new DiscountFluentConfigs());
            modelBuilder.ApplyConfiguration(new QuestionFluentConfigs());
            modelBuilder.ApplyConfiguration(new AnswerFluentConfigs());

            #region User

            modelBuilder.Entity<User>().HasQueryFilter(u => u.IsDelete == false);
            base.OnModelCreating(modelBuilder);

            #endregion

            #region Role

            modelBuilder.Entity<Role>().HasQueryFilter(r => r.IsDelete == false);
            base.OnModelCreating(modelBuilder);

            #endregion

            modelBuilder.Entity<CourseGroup>().HasQueryFilter(r => r.IsDelete == false);
            base.OnModelCreating(modelBuilder);

            #region Course

            modelBuilder.Entity<Course>().HasQueryFilter(r => r.IsDelete == false);
            base.OnModelCreating(modelBuilder);

            #endregion

        }

        #endregion


    }
}
