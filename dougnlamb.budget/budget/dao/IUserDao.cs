using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public interface IUserDao {
        IPagedList<IUser> Find(ISecurityContext securityContext, string name);
        IUser Retrieve(ISecurityContext securityContext, int oid);
        IUser Retrieve(ISecurityContext securityContext, string userId);
        int Save(ISecurityContext securityContext, IUser user);
    }
}