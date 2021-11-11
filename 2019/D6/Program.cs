using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace D6
{
    class Planet
    {
        public string Name { get; set; }
        public List<Planet> PlanetsOrbital { get; set; } = new List<Planet>();
        public Planet Parent { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var blackHole = new Planet { Name = "BlackHole" };

            Planet FindPlanet(Planet o, string name)
            {
                return o.Name == name ? o : o.PlanetsOrbital.Select(oo => FindPlanet(oo, name)).FirstOrDefault(ooo => ooo != null);
            }

            int CountOrbits(Planet o)
            {
                var total = 0;
                foreach (var oo in o.PlanetsOrbital)
                {
                    oo.Weight = oo.Parent.Weight + 1;
                    total += oo.Weight + CountOrbits(oo);
                }

                return total;
            }

            foreach (var line in File.ReadLines("data.txt"))
            {
                var planetNames = line.Split(')');

                var planet = FindPlanet(blackHole, planetNames[0]);
                if (planet == null)
                {
                    planet = new Planet
                    {
                        Name = planetNames[0],
                        Parent = blackHole
                    };
                    blackHole.PlanetsOrbital.Add(planet);
                }

                var planetOrbit = FindPlanet(blackHole, planetNames[1]);
                if (planetOrbit == null)
                {
                    planet.PlanetsOrbital.Add(new Planet
                    {
                        Name = planetNames[1],
                        Parent = planet
                    });
                }
                else
                {
                    planet.PlanetsOrbital.Add(planetOrbit);
                    planetOrbit.Parent.PlanetsOrbital.Remove(planetOrbit);
                    planetOrbit.Parent = planet;
                }
            }
            // D6P1: 315757
            Console.WriteLine(CountOrbits(blackHole.PlanetsOrbital[0]));

            // D6P2

            void GraphDirect(Planet o)
            {
                var oo = o;
                do
                {
                    Console.Write($"{oo.Name}(");
                    oo = oo.Parent;
                } while (oo != null);
                Console.WriteLine();
            }

            var you = FindPlanet(blackHole, "YOU");
            //GraphDirect(you);

            var san = FindPlanet(blackHole, "SAN");
            //GraphDirect(san);

            var cry = FindPlanet(blackHole, "CRY");

            Console.WriteLine((you.Weight - cry.Weight) + (san.Weight - cry.Weight) - 2);
        }
    }
}
