using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class Country
    {
        public Country(CountryDTO countryDTO)
        {
            this.Id = countryDTO.Id;
            this.Name = countryDTO.Name;
        }

        public long Id { get; set; }

        public string Name { get; set; }
    }
}
