namespace MusicAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MusicAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicAPI.Models.MusicAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicAPI.Models.MusicAPIContext context)
        {
            context.Artists.AddOrUpdate(x => x.Id,
                new Artist() { Id = 1, Name = "Garth Brooks", Gender = "Male", DateOfBirth = new DateTime(1962,02,01) },
                new Artist() { Id = 2, Name = "Britney Spears", Gender = "Female", DateOfBirth = new DateTime(1982,01,03) },
                new Artist() { Id = 3, Name = "Alanis Morissette", Gender = "Female", DateOfBirth = new DateTime(1974,11,02) }
                );

            context.Albums.AddOrUpdate(x => x.Id,
                new Album { Id = 1, Title ="Man Againts Machine", Genre="Country", Price= 14.99M, Year=2014, ArtistId=1},
                new Album { Id = 2, Title = "No Frences", Genre = "Country", Price = 12.99M, Year = 1990, ArtistId = 1},
                new Album { Id = 3, Title = "Blackout", Genre = "Pop", Price = 15.99M, Year = 2007, ArtistId = 2 },
                new Album { Id = 4, Title = "Glory", Genre = "Pop", Price = 12.99M, Year = 2016, ArtistId = 2 },
                new Album { Id = 5, Title = "Jagged Little Pill", Genre = "Alternative", Price = 14.99M, Year = 1995, ArtistId = 3 },
                new Album { Id = 6, Title = "Now is the Time", Genre = "Alternative", Price = 13.99M, Year = 1992, ArtistId = 3 }
                );

        }
    }
}
