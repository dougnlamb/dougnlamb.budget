using dougnlamb.core.security;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IAccountEditorModel {
        int oid { get; }

        string Name { get; set; }

        int OwnerId { get; set; }
        IUserViewModel Owner { get;  }
        IList<IUserViewModel> PossibleOwners { get; }

        int DefaultCurrencyId { get; }

        IAccount Save(ISecurityContext securityContext);
    }
}