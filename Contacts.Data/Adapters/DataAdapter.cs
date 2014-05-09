using System.Collections.Generic;
using System.Linq;
using Contacts.Data.DTO;
using NHibernate.Linq;

namespace Contacts.Data.Adapters
{
    public class DataAdapter : BaseAdapter
    {
        public IList<CountryDTO> GetCountries()
        {
            return Session.Query<CountryDTO>().ToList();
        }

        public List<TownDTO> GetTowns(long countryId)
        {
            return Session.Query<TownDTO>().Where(t => t.Country.Id == countryId).ToList();
        }
    }
}
