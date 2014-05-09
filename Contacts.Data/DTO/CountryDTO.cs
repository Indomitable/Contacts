using System.Collections.Generic;

namespace Contacts.Data.DTO
{

    public class CountryDTO
    {
        private readonly IList<TownDTO> towns = new List<TownDTO>();

        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<TownDTO> Towns { get { return towns; } }
    }
}
