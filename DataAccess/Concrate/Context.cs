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
        
        public Context(DbContextOptions options) : base(options) { }


        public DbSet<Agency> Agencies { get; set; }   
        public DbSet<Contract> Contracts { get; set; }   
        public DbSet<Board> Boards { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CAgencyList> CAgencies { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }


    }
}
