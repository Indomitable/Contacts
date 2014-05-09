using System;

namespace Contacts.Data.DTO
{
    public class PhoneDTO : IEquatable<PhoneDTO>, IComparable<PhoneDTO>
    {
        public virtual long Id { get; set; }
        public virtual PersonDTO Person { get; set; }
        public virtual string Number { get; set; }
        public virtual PhoneType Type { get; set; }
        public virtual bool IsPrimary { get; set; }

        #region Equal

        public virtual int CompareTo(PhoneDTO other)
        {
            return Id.CompareTo(other.Id);
        }

        public virtual bool Equals(PhoneDTO other)
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
            PhoneDTO cast = obj as PhoneDTO;
            if (cast == null)
                return false;
            return Equals(cast);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(PhoneDTO left, PhoneDTO right)
        {
            return ReferenceEquals(null, left) ? ReferenceEquals(null, right) : left.Equals(right);
        }

        public static bool operator !=(PhoneDTO left, PhoneDTO right)
        {
            return !(left == right);
        }

        #endregion Equal
    }
}
