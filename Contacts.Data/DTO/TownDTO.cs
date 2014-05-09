using System.Collections.Generic;

namespace Contacts.Data.DTO
{

    public class TownDTO
    {
        private readonly IList<AddressDTO> addresses = new List<AddressDTO>();

        public virtual long Id { get; set; }
        public virtual CountryDTO Country { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<AddressDTO> Addresses { get { return addresses; } }
    }
}
