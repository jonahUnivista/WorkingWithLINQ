using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {

            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            //var cars = ProcessFile("fuel.csv");

            // This is how you define an ananomous object
            //var anon = new
            //{
            //    Name = "Scott"
            //};

            //var query =
            //    from car in cars
            //    where car.Manufacture == "BMW" && car.Year == 2016
            //    orderby car.Combined descending, car.Name ascending
            //    select new
            //    {
            //        car.Manufacture,
            //        car.Name,
            //        car.Combined
            //    };

            //var query1 = cars.OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name);

            //var top = cars.Where(c => c.Manufacture == "BMW" && c.Year == 2016)
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name)
            //    .Select(c => c)
            //    .First();
            ////Console.WriteLine(top.Name);

            //var result = cars.Select(c => c.Name);

            //foreach (var name in result)
            //{
            //    foreach (var character in name)
            //    {
            //        Console.WriteLine(character);
            //    }
            //    //Console.WriteLine(name);
            //}

            //var result2 = cars.SelectMany(c => c.Name);

            //foreach (var character in result2)
            //{
            //    Console.WriteLine(character);
            //}

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Manufacture} {car.Name} : {car.Combined}");
            //}


            // Joining, Gouping, and Aggregation
            // JOINING


            //var queryUsinQuerySyntax =
            //    from car in cars
            //    join manufacturer in manufacturers
            //        on car.Manufacturer equals manufacturer.Name
            //    orderby car.Combined descending, car.Name ascending
            //    select new
            //    {
            //        manufacturer.Headquarters,
            //        car.Name,
            //        car.Combined
            //    };
            //var queryUsinQuerySyntax2 =
            //    from car in cars
            //    join manufacturer in manufacturers
            //        on new { car.Manufacturer, car.Year } 
            //            equals 
            //                new { Manufacturer = manufacturer.Name, manufacturer.Year }
            //    orderby car.Combined descending, car.Name ascending
            //    select new
            //    {
            //        manufacturer.Headquarters,
            //        car.Name,
            //        car.Combined
            //    };


            //var queryUsingMethodSyntax =
            //    cars.Join(manufacturers,
            //    c => c.Manufacturer,
            //    m => m.Name, (c, m) => new
            //    {
            //        m.Headquarters,
            //        c.Name,
            //        c.Combined
            //    })
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name);

            //var queryUsingMethodSyntax2 =
            //    cars.Join(manufacturers,
            //    c => new { c.Manufacturer, c.Year },
            //    m => new { Manufacturer = m.Name, m.Year },
            //    (c, m) => new
            //    {
            //        m.Headquarters,
            //        c.Name,
            //        c.Combined
            //    })
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name);
            //foreach (var car in queryUsingMethodSyntax2.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //}

            //GROUPING  

            var queryWithGroupingQuerySyntax =
                from car in cars
                group car by car.Manufacturer.ToUpper() into manufacturer
                orderby manufacturer.Key
                select manufacturer;

            var queryWithGroupingMethodSyntax =
                cars.GroupBy(c => c.Manufacturer.ToUpper())
                .OrderBy(g => g.Key);

            foreach (var group in queryWithGroupingMethodSyntax)
            {
                Console.WriteLine($"{group.Key}");
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            Console.ReadLine();
        }

        private static List<Car> ProcessCars(string path)
        {
            var query =

                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToCar();

            return query.ToList();
        }
        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                .Where(l => l.Length > 1)
                .Select(l =>
                {
                    var columns = l.Split(',');
                    return new Manufacturer
                    {
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2])
                    };
                });
            return query.ToList();
        }
    }
    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }


        }
    }
}
