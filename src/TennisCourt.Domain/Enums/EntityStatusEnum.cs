using System.ComponentModel;

namespace TennisCourt.Domain.Enums
{
    [DefaultValue(Active)]
    public enum EntityStatusEnum
    {
        Deleted=0,
        Active=1,
    }
}
