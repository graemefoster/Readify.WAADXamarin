
namespace Timesheet.Client.Shared.ResourceAccess
{
    public abstract class Resource
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() == this.GetType())
                return Id.Equals(((Resource)obj).Id);

            return false;
        }

        public override int GetHashCode()
        {
            if (Id == 0)
// ReSharper disable BaseObjectGetHashCodeCallInGetHashCode
                return base.GetHashCode();
// ReSharper restore BaseObjectGetHashCodeCallInGetHashCode

            return Id.GetHashCode();
        }
    }
}