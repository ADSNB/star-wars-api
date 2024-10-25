using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using star_wars_api.Models;

namespace star_wars_api.Routes
{
    public static class StarWarsRoute
    {
        public static void StarWarsRoutes(this WebApplication app)
        {
            var routes = app.MapGroup("starwars");

            routes.MapGet("", async () => 
                {
                    try
                    {
                        var nextPage = "https://swapi.dev/api/starships/";
                        var starships = new List<Starship>();
                        var manufacturers = new List<string>();
                        using (var client = new HttpClient())
                        {
                            while (!string.IsNullOrEmpty(nextPage))
                            {
                                var response = await client.GetAsync(nextPage);

                                response.EnsureSuccessStatusCode();

                                string responseBody = await response.Content.ReadAsStringAsync();

                                var starshipsResults = JsonConvert.DeserializeObject<StarWarsRequest>(responseBody);

                                foreach (var starship in starshipsResults?.results)
                                {
                                    starships.Add(starship);
                                    manufacturers.Add(starship.manufacturer);
                                }

                                nextPage = starshipsResults.next;
                            }
                        }
                        var result = new StarWarsResponse(starships.OrderBy(a=> a.name).ToList(), manufacturers.Distinct().OrderBy(a=>a).ToList());
                        return Results.Ok(result);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("Message :{0} ", e.Message);
                        return Results.BadRequest(e.Message);
                    }
                }).RequireAuthorization();
        }
    }
}
