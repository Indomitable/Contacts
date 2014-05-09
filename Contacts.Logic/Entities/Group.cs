using System;
using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class Group
    {
        public Group()
        {
            
        }

        public Group(GroupDTO groupDTO)
        {
            if (groupDTO == null) throw new ArgumentNullException("groupDTO");
            this.Id = groupDTO.Id;
            this.Name = groupDTO.Name;
        }

        public long Id { get; set; }

        public string Name { get; set; }
    }
}
