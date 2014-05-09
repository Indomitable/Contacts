using System.Collections.Generic;

namespace Contacts.Data.DTO {
    
    public class PersonDTO 
    {
        private readonly IList<AddressDTO> addresses = new List<AddressDTO>();
        private readonly IList<MailDTO> mails = new List<MailDTO>();
        private readonly IList<PhoneDTO> phones = new List<PhoneDTO>();
        private readonly IList<GroupDTO> groups = new List<GroupDTO>(); 

        public virtual long Id { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Title Title { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual PersonAvatarDTO Avatar { get; set; }

        public virtual IList<AddressDTO> Addresses { get { return addresses; } }
        public virtual IList<MailDTO> Mails { get { return mails; } }
        public virtual IList<PhoneDTO> Phones { get { return phones; } }
        public virtual IList<GroupDTO> Groups { get { return groups; } }
    }

    public class PersonAvatarDTO
    {
        public virtual long Id { get; set; }

        public virtual byte[] Image { get; set; }
    }
}
