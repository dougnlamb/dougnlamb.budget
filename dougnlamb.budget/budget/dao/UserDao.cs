using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public class UserDao : IUserDao {
        public IPagedList<IUser> Find(IUser user, string name) {
            throw new NotImplementedException();
        }

        public IPagedList<IUser> Find(ISecurityContext securityContext, string name) {
            throw new NotImplementedException();
        }

        public IUser Retrieve(ISecurityContext securityContext, int oid) {
            return new User {
                oid = oid,
                UserId = "bubba",
                DisplayName = "Bubba Gump",
                Email = "bubba@example.com"
            };
        }

        public void Save(ISecurityContext securityContext, IUser user) {
            throw new NotImplementedException();
        }
    }
}
