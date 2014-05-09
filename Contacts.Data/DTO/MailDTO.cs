using System;

namespace Contacts.Data.DTO
{
    public class MailDTO :  IEquatable<MailDTO>, IComparable<MailDTO>
    {
        public virtual long Id { get; set; }
        public virtual PersonDTO Person { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsPrimary { get; set; }

        #region Equal

        public virtual int CompareTo(MailDTO other)
        {
            return Id.CompareTo(other.Id);
        }

        public virtual bool Equals(MailDTO other)
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
            MailDTO cast = obj as MailDTO;
            if (cast == null)
                return false;
            return Equals(cast);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(MailDTO left, MailDTO right)
        {
            return ReferenceEquals(null, left) ? ReferenceEquals(null, right) : left.Equals(right);
        }

        public static bool operator !=(MailDTO left, MailDTO right)
        {
            return !(left == right);
        }

        #endregion Equal
    }
}
