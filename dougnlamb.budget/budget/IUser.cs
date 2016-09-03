using dougnlamb.budget.models;
using dougnlamb.core;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;

namespace dougnlamb.budget {
    public interface IUser : IBaseObject {
        int oid { get; }
        string UserId { get; }
        string DisplayName { get; }
        string Email { get; }
        ICurrency DefaultCurrency { get; }

        IObservableList<IAccount> Accounts { get; }
        IObservableList<IBudget> Budgets { get; }

        IAccountEditorModel CreateAccount(ISecurityContext securityContext);
        IAccount AddAccount(ISecurityContext securityContext, IAccountEditorModel model);

        IBudgetEditorModel CreateBudget(ISecurityContext securityContext);
        IBudget AddBudget(ISecurityContext securityContext, IBudgetEditorModel model);

        IUserViewModel View(ISecurityContext securityContext);
        IUserEditorModel Edit(ISecurityContext securityContext);
        void Save(ISecurityContext securityContext, IUserEditorModel model);
        void Save(ISecurityContext securityContext, IUserRegistrationModel model);
    }
}