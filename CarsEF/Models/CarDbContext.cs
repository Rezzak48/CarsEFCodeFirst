using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsEF.Models
{
    // Always inheret from DbConext
    public partial class CarDbContext : DbContext //partial means the class can be splitted
    {
        //Creating DbSets
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }

        public CarDbContext()
        {
            // Because we are using code first we must
            //ensure that the database is created
            this.Database.EnsureCreated();
        }

        // 2 Methods needs to be ovveriding
        // OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //This is how! if optionBuilder is not configured, we user our option.lazyproxies.sqlserver with the localpath
            //if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CarsDb.mdf;Integrated Security=True;MultipleActiveResultSets = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // connect one brand to many cars and one car to many brands
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasOne(car => car.Brand). //car has one brand
                WithMany(brand => brand.Cars). // brand has many cars
                HasForeignKey(car => car.BrandId).
                //HasPrincipalKey(car => car.Id).// car has a foreing key
                OnDelete(DeleteBehavior.ClientSetNull); // when delete a brand set it to null, not deleting the cars
            });

            //we can add data usie sql or code, we are using here code

            //Creating data
            Brand Bmw = new Brand() { Id = 1, Name = "BMW" };
            Brand Audi = new Brand() { Id = 2, Name = "AUDI" };    
            Brand Ferari = new Brand() { Id = 3, Name = "FERARI"};

            Car bmw1 = new Car() { Id = 1, Model = "bmw500", BrandId = Bmw.Id, BestPrice = 20000 };
            Car bmw2 = new Car() { Id = 2, Model = "bmw600", BrandId = Bmw.Id, BestPrice = 30000 };
            Car bmw3 = new Car() { Id = 3, Model = "bmw700", BrandId = Bmw.Id, BestPrice = 40000 };
            Car audi1 = new Car() { Id = 4, Model = "audi500", BrandId = Audi.Id, BestPrice = 20000 };
            Car audi2 = new Car() { Id = 5, Model = "audi500", BrandId = Audi.Id, BestPrice = 30000 };
            Car audi3 = new Car() { Id = 6, Model = "audi500", BrandId = Audi.Id, BestPrice = 40000 };
            Car ferari1 = new Car() { Id =7, Model = "ferari500", BrandId = Ferari.Id, BestPrice = 20000 };
            Car ferari2 = new Car() { Id = 8, Model = "ferari500", BrandId = Ferari.Id, BestPrice = 30000 };
            Car ferari3 = new Car() { Id = 9, Model = "ferari500", BrandId = Ferari.Id, BestPrice = 40000 };

            //inserting the data to the database
            modelBuilder.Entity<Brand>().HasData(Bmw, Audi, Ferari);
            modelBuilder.Entity<Car>().HasData(bmw1, bmw2, bmw3, audi1, audi2, audi3, ferari1, ferari2, ferari3);
            



        }
    }
}
