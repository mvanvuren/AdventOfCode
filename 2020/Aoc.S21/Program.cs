using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace Aoc.S21
{
    class Program
    {


        static void Main(string[] args)
        {
            var regex = new Regex(@"(([a-z]+)\s+)+\(contains\s+(([a-z]+)(,\s){0,1})+\)", RegexOptions.Compiled);

            var foods = new List<Food>();
            var ingredients = new Dictionary<string, List<int>>();
            var containedIngredients = new Dictionary<string, List<int>>();
            var matchedIngredients = new Dictionary<string, List<string>>();

            var id = 0;

            foreach (var line in File.ReadLines("input.txt"))
            {
                //var match = regex.Match(line);

                var food = new Food
                {
                    Id = id,
                    //Ingredients = match.Groups[1].Captures.Select(c => c.Value).ToList(),
                    //ContainedIngredients = match.Groups[4].Captures.Select(c => c.Value).ToList()
                    Ingredients = new List<string>(line.Split("(contains ")[0].Trim().Split(" ").Select(s => s.Trim())),
                    ContainedIngredients = new List<string>(line.Split("(contains ")[1].Trim().Replace(")", "").Split(",").Select(s => s.Trim()))
                };

                foreach (var ingredient in food.Ingredients)
                {
                    if (!ingredients.ContainsKey(ingredient))
                    {
                        ingredients.Add(ingredient, new List<int>());
                    }
                    ingredients[ingredient].Add(id);
                }

                foreach (var containedIngredient in food.ContainedIngredients)
                {
                    if (!containedIngredients.ContainsKey(containedIngredient))
                    {
                        containedIngredients.Add(containedIngredient, new List<int>());
                    }
                    containedIngredients[containedIngredient].Add(id);

                    if (!matchedIngredients.ContainsKey(containedIngredient))
                    {
                        matchedIngredients[containedIngredient] = new List<string>(food.Ingredients);
                    }
                    else
                    {
                        matchedIngredients[containedIngredient] = matchedIngredients[containedIngredient]
                            .Intersect(food.Ingredients).ToList();
                    }
                }

                foods.Add(food);

                id++;
            }


            var allergens = new List<string>();

            var orderedMatchedIngredients = matchedIngredients.OrderBy(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
            foreach (var (key, value) in orderedMatchedIngredients)
            {
                if (value.Count == 1)
                {
                    allergens.AddRange(value);
                }
                else
                {
                    var filteredList = value.Except(allergens).ToList();
                    allergens.AddRange(filteredList);
                    matchedIngredients[key] = new List<string>(filteredList);
                }
            }

            var total = 0;
            var i = allergens;
            foreach (var food in foods)
            {
                total += food.Ingredients.Except(i).Count();
            }
            Console.WriteLine(total);

            // phc,spnd,zmsdzh,,fqqcnmpdt,,rjc,lzvhlsgqf
            //var orderedByName = matchedIngredients.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            //string canonicalDangerousIngredientString = String.Empty;
            //foreach (var entry in orderedByName)
            //{
            //    if (canonicalDangerousIngredientString == String.Empty)
            //    {
            //        canonicalDangerousIngredientString = string.Join(",", entry.Value);
            //    }
            //    else
            //    {
            //        canonicalDangerousIngredientString = $"{canonicalDangerousIngredientString},{string.Join(",", entry.Value)}";
            //    }
            //}

            //Console.WriteLine(string.Join(",", canonicalDangerousIngredientString.Split(",", StringSplitOptions.RemoveEmptyEntries).OrderBy(o => o)));
        }
    }

    public class Food
    {
        public int Id { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();

        public List<string> ContainedIngredients { get; set; } = new List<string>();
    }
}
