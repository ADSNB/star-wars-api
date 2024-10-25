namespace star_wars_api.Models
{
    public class StarWarsResponse
    {
        public StarWarsResponse(List<Starship> starships, List<string> manuManufacturers)
        { 
            Starships = starships;
            Manufacturers = manuManufacturers;
        }
        public List<Starship> Starships { get; set; }
        public List<string> Manufacturers { get; set; }
    }
}
