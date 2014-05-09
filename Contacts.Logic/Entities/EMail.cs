using System;
using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class EMail
    {
        public EMail()
        {
            
        }

        public EMail(MailDTO mailDTO)
        {
            if (mailDTO == null) throw new ArgumentNullException("mailDTO");
            this.Email = mailDTO.Email;
            this.IsPrimary = mailDTO.IsPrimary;
        }

        public string Email { get; set; }

        public bool IsPrimary { get; set; }
    }
}
