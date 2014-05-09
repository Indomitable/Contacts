using Contacts.Data.DTO;

namespace Contacts.Logic.Entities
{
    public class Town
    {
        public Town(TownDTO townDTO)
        {
            this.Id = townDTO.Id;
            this.Name = townDTO.Name;
        }

        public long Id { get; set; }

        public string Name { get; set; }
    }
}
