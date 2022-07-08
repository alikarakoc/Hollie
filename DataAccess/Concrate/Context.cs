using Application.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder 
            optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=Efe;database=efe;integrated security=true");
        }
        public DbSet<Agency> Agencys { get; set; }   
        public DbSet<Board> Boards { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelCategory> HotelCategorys { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }

    }
}
