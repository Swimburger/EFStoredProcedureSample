﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFStoredProcedureSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Setup();
            QueryPetsByType(type: "Cat");
        }

        private static void Setup()
        {
            using var petContext = new PetContext();
            petContext.Database.EnsureCreated();
            petContext.Database.ExecuteSqlRaw("DELETE FROM Pets WHERE 1 = 1");
            petContext.Pets.Add(new Pet { Name = "Grumpy Cat", Type = "Cat" });
            petContext.Pets.Add(new Pet { Name = "Smelly Cat", Type = "Cat" });
            petContext.Pets.Add(new Pet { Name = "Lassie", Type = "Dog" });
            petContext.SaveChanges();
        }

        private static void QueryPetsByType(string type)
        {
            using var petContext = new PetContext();
            List<Pet> pets = petContext.Pets
                .FromSqlRaw("EXEC GetPetsByType @Type={0};", type)
                .ToList();

            foreach (var pet in pets)
            {
                Console.WriteLine($"Name: {pet.Name}");
            }
        }
    }

    public class PetContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=.;Initial Catalog=PetsDb;Integrated Security=True");
    }

    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
