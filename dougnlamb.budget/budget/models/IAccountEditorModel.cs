﻿using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IAccountEditorModel {
        int oid { get; }

        string Name { get; set; }

        IUser Owner { get; set; }
        ICurrency DefaultCurrency { get; set; }

        IAccount Save(ISecurityContext securityContext);
    }
}