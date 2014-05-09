using System;
using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class ContactInfo
    {
        public ContactInfo(Contact contact)
        {
            this.Id = contact.Id;
            this.Title = contact.Title != Data.DTO.Title.Other ? Enum.GetName(typeof (Title), contact.Title) : string.Empty;
            this.FullName = contact.FullName;
            if (contact.Phone != null)
                this.Phone = contact.Phone.Number;
            if (contact.Address != null)
            this.Address = contact.Address.BuildAddress();
            if (contact.EMail != null)
                this.EMail = contact.EMail.Email;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string FullName { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public string EMail { get; private set; }
    }
}