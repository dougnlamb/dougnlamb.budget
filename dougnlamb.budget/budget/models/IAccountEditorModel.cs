using dougnlamb.core.security;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IAccountEditorModel {
        int oid { get; }

        string Name { get; set; }
        ICurrency DefaultCurrency { get; set; }
        IUser Owner { get; set; }

        IAccount Save(ISecurityContext securityContext);
    }
}