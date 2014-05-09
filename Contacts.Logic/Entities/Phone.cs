using System;
using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class Phone
    {
        public Phone()
        {
            
        }

        public Phone(PhoneDTO phoneDTO)
        {
            if (phoneDTO == null) throw new ArgumentNullException("phoneDTO");
            this.Number = phoneDTO.Number;
            this.Type = phoneDTO.Type;
            this.IsPrimary = phoneDTO.IsPrimary;
        }

        public string Number { get; set; }

        public PhoneType Type { get; set; }

        public bool IsPrimary { get; set; }
    }
}
