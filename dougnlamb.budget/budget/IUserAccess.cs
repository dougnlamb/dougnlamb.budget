using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IUserAccess {
        IUser user { get; }
        UserAccessMode Mode { get; }
    }
    public enum UserAccessMode {
        ReadOnly,
        ReadWrite
    }
}
