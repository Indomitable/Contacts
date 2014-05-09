using System.Collections.Generic;

namespace Contacts.Logic.Entities
{
    public class ContactsResult
    {
        public IEnumerable<ContactInfo> Contacts { get; set; }

        public int PageCount { get; set; }
    }
}
