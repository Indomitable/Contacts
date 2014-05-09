using System;
using System.Collections.Generic;


namespace Contacts.Data.DTO
{

    public class AddressDTO : IEquatable<AddressDTO>, IComparable<AddressDTO>
    {
        private readonly IList<PersonDTO> persons = new List<PersonDTO>();

        public virtual long Id { get; set; }
        public virtual TownDTO Town { get; set; }
        public virtual string Address { get; set; }
        public virtual string PostCode { get; set; }
        public virtual bool IsPrimary { get; set; }

        public virtual IList<PersonDTO> Persons { get { return persons; } }

        #region Equal

        public virtual int CompareTo(AddressDTO other)
        {
            return Id.CompareTo(other.Id);
        }

        public virtual bool Equals(AddressDTO other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            AddressDTO cast = obj as AddressDTO;
            if (cast == null)
                return false;
            return Equals(cast);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(AddressDTO left, AddressDTO right)
        {
            return ReferenceEquals(null, left) ? ReferenceEquals(null, right) : left.Equals(right);
        }

        public static bool operator !=(AddressDTO left, AddressDTO right)
        {
            return !(left == right);
        }

        #endregion Equal
    }
}
