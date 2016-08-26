using dougnlamb.budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.core {
    public interface IBaseObject {
        bool CanRead(IUser user);
        bool CanUpdate(IUser user);

        DateTime CreatedDate { get; }
        DateTime CreatedBy { get; }

        DateTime UpdatedDate { get; }
        IUser UpdatedBy { get;  }
    }
}
