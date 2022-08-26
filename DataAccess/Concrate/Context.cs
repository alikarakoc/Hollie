using Application.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate
{
    public class Context : IdentityDbContext<AppUser, IdentityRole, string>
    {
        
        public Context(DbContextOptions options ) : base(options) { }

        public DbSet<Agency> Agencies { get; set; }   
        public DbSet<Contract> Contracts { get; set; }   
        public DbSet<Board> Boards { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CAgencyList> CAgencies { get; set; }
        public DbSet<CBoardList> CBoards { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<CMarketList> CMarkets { get; set; }
        public DbSet<CRoomTypeList> CRoomTypes { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<HotelFeature> HotelFeatures { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<MarketListA> AMarkets { get; set; }






    }
}
