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

        // TODO: Add DbSets

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Titles> Titles { get; set; }
        public DbSet<Stores> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Jobs>().ToTable("jobs");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");
                entity.HasOne<Jobs>(a => a.Jobs)
               .WithMany(a => a.Employee)
               .HasForeignKey(a => a.job_id);
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
                .HasKey(sales => new
                {
                    sales.stor_id,
                    sales.title_id
                });
            });
        }
    }


    public class Titles
    {
        // TODO
        [Key]
        public string title_id { get; set; }
        public string title { get; set; }
        public IList<Sales> Sales { get; set; }
    }


    public class Stores
    {
        // TODO
        [Key]
        public char stor_id { get; set; }
        public string stor_name { get; set; }
        public IList<Sales> Sales { get; set; }
    }


    public class Sales
    {
        // TODO
        [Key]
        public string ord_num { get; set; }
        public char stor_id { get; set; }
        public Stores Stores { get; set; }
        public string title_id { get; set; }
        public Titles Titles { get; set; }
    }

    public class Employee
    {
        // TODO
         [Key]
        public string emp_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public Int16 job_id { get; set; }
        public Jobs Jobs { get; set; }
    }

    public class Jobs
    {
        // TODO
        [Key]
        public Int16 job_id { get; set; }
        public string job_desc { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
