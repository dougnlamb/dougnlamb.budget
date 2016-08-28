using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public interface IUserDao {
        IPagedList<IUser> Find(ISecurityContext securityContext, string name);
        IUser Retrieve(ISecurityContext securityContext, int oid);
        void Save(ISecurityContext securityContext, IUser user);
    }
}