using System;
using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class SearchEntry
    {
        public SearchEntry(PersonDTO personDTO)
        {
            if (personDTO == null) throw new ArgumentNullException("personDTO");
            this.Id = personDTO.Id;
            this.FullName = personDTO.FirstName + " " + personDTO.LastName;
        }

        public long Id { get; set; }

        public string FullName { get; set; }
    }
}
