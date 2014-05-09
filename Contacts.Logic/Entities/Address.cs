using System;
using System.Text;
using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class Address
    {
        public Address()
        {
            
        }

        public Address(AddressDTO addressDTO)
        {
            if (addressDTO == null) throw new ArgumentNullException("addressDTO");
            this.Country = addressDTO.Town.Country.Name;
            this.Town = addressDTO.Town.Name;
            this.AddressVal = addressDTO.Address;
            this.PostCode = addressDTO.PostCode;
            this.IsPrimary = addressDTO.IsPrimary;
        }

        public string Country { get; set; }

        public string Town { get; set; }

        public string AddressVal { get; set; }

        public string PostCode { get; set; }

        public bool IsPrimary { get; set; }

        public string BuildAddress()
        {
            var builder = new StringBuilder(AddressVal);
            builder.Append(", " + PostCode);
            builder.AppendLine();
            builder.Append(Town + ", " + Country);
            return builder.ToString();
        }

    }
}
