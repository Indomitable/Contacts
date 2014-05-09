using System.Collections.Generic;
using System.Linq;
using Contacts.Data.Adapters;
using Contacts.Logic.Entities;

namespace Contacts.Logic.Managers
{
    public static class DataManager
    {
        public static List<Country> GetCountries()
        {
            using (var adapter = new DataAdapter())
            {
                return adapter.GetCountries().Select(x => new Country(x)).ToList();
            }
        }

        public static List<Town> GetTowns(long countryId)
        {
            using (var adapter = new DataAdapter())
            {
                return adapter.GetTowns(countryId).Select(x => new Town(x)).ToList();
            }
        }
    }
}
