using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace tasks
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public string DbPath { get; }

        public ApplicationContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "tasks.db");

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().HasData(
                new tasks.Task
                {
                    Id = 1,
                    TaskName = "Работа 1",
                    Resource = "Задачи",
                    StartDate = new DateTime(2022, 9, 10),
                    EndDate = new DateTime(2022, 10, 10),
                    PercentComplete = 30
                },
                new tasks.Task
                {
                    Id = 2,
                    TaskName = "Работа 2",
                    Resource = "Задачи",
                    StartDate = new DateTime(2022, 9, 20),
                    EndDate = new DateTime(2022, 10, 30),
                    PercentComplete = 70
                },
                new tasks.Task
                {
                    Id = 3,
                    TaskName = "Работа 3",
                    Resource = "Задачи",
                    StartDate = new DateTime(2022, 10, 25),
                    EndDate = new DateTime(2022, 11, 15),
                    PercentComplete = 90
                }
                );
        }
    }
}
