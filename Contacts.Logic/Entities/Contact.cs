using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class Contact
    {
        private readonly List<Group> groups = new List<Group>();
        private readonly List<Phone> phones = new List<Phone>();
        private readonly List<Address> addresses = new List<Address>();
        private readonly List<EMail> eMails = new List<EMail>();

        public Contact()
        {
            
        }

        public Contact(PersonDTO personDTO)
        {
            if (personDTO == null) throw new ArgumentNullException("personDTO");
            this.Id = personDTO.Id;
            this.Title = personDTO.Title;
            this.FirstName = personDTO.FirstName;
            this.LastName = personDTO.LastName;
            this.Gender = personDTO.Gender;

            this.Addresses.AddRange(personDTO.Addresses.Select(a => new Address(a)));
            this.Phones.AddRange(personDTO.Phones.Select(p => new Phone(p)));
            this.EMails.AddRange(personDTO.Mails.Select(m => new EMail(m)));

            this.Groups.AddRange(personDTO.Groups.Select(g => new Group(g)));
        }

        public long Id { get; set; }

        public Title Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                StringBuilder builder = new StringBuilder(FirstName);
                if (!string.IsNullOrWhiteSpace(LastName))
                    builder.Append(" " + LastName);
                return builder.ToString();
            }
        }

        public Gender Gender { get; set; }

        public List<Phone> Phones { get { return phones; } }

        public Phone Phone
        {
            get
            {
                var phone = Phones.SingleOrDefault(p => p.IsPrimary);
                if (phone == null && Phones.Any())
                    return phones[0];
                return phone;
            }
        }

        public List<Address> Addresses { get { return addresses; } }

        public Address Address
        {
            get
            {
                var address = Addresses.SingleOrDefault(a => a.IsPrimary);
                if (address == null && Addresses.Any())
                    return Addresses[0];
                return address;
            }
        }

        public List<EMail> EMails { get { return eMails; } }

        public EMail EMail
        {
            get
            {
                var mail = EMails.SingleOrDefault(e => e.IsPrimary);
                if (mail == null && EMails.Any())
                    return EMails[0];
                return mail;
            }
        }

        public List<Group> Groups { get { return groups; } }

    }
}
