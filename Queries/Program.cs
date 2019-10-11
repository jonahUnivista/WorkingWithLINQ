﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight", Rating = 8.9f, Year = 2008},
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010},
                new Movie { Title = "Cassablanca", Rating = 8.5f, Year = 1942},
                new Movie { Title = "Star Wars V", Rating = 8.7f, Year = 1980}
            };

            var queryByCustomeSyntax = movies.Filter(m => m.Year > 2000);

            var enumerator = queryByCustomeSyntax.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }

            // Previous version 
            //foreach (var movie in queryByCustomeSyntax)
            //{
            //    Console.WriteLine(movie.Title);
            //}

            Console.WriteLine("\n*************************\n");

            var queryByMethodSyntax = movies.Where(m => m.Year > 2000);


            foreach (var movie in queryByMethodSyntax)
            {
                Console.WriteLine(movie.Title);
            }

            Console.ReadLine();
        }
    }
}
