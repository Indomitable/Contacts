using System.Collections.Generic;

namespace Contacts.Data.DTO {
    
    public class GroupDTO
    {
        private readonly IList<PersonDTO> members = new List<PersonDTO>();

        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<PersonDTO> Members { get { return members; } }
    }
}
