using System;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace CP380_PubsLab.Models
{
    public class PubsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\pubs.mdf"));
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Integrated Security=true;AttachDbFilename={dbpath}");
        }

        

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Jobs>().ToTable("jobs");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasOne<Jobs>(s => s.Jobs)
               .WithMany(s => s.Employee)
               .HasForeignKey(s => s.job_id);
            });

            modelBuilder.Entity<Titles>(entity =>
            {
                entity.ToTable("titles");
            });
            modelBuilder.Entity<Stores>(entity =>
            {
                entity.ToTable("stores");
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.ToTable("sales")
                .HasKey(sal => new
                {
                    sal.stor_id,
                    sal.title_id
                });
            });

            // TODO: Add DbSets

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Titles> Titles { get; set; }
        public DbSet<Stores> Stores { get; set; }

    public class Titles
    {        
        [Key]
        public string title_id { get; set; }
        public string title { get; set; }
        public IList<Sales> Sales { get; set; }
    }


    public class Stores
    {
        [Key]
        public char stor_id { get; set; }
        public string stor_name { get; set; }
        public IList<Sales> Sales { get; set; }
    }


    public class Sales
    {
        [Key]
        public string ord_num { get; set; }
        public char stor_id { get; set; }
        public Stores Stores { get; set; }
        public string title_id { get; set; }
        public Titles Titles { get; set; }
    }

    public class Employee
    {
        [Key]
        public string emp_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public Int16 job_id { get; set; }
        public Jobs Jobs { get; set; }
    }

    public class Jobs
    {
        [Key]
        public Int16 job_id { get; set; }
        public string job_desc { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }

    }
}