using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.budget;
using dougnlamb.core.security;

namespace dougnlamb.core {
    public abstract class BaseObject : IBaseObject {
        protected bool mIsLoaded;
        protected ISecurityContext mSecurityContext;

        protected BaseObject(ISecurityContext securityContext) {
            mSecurityContext = securityContext;
        }

        private IUser mCreatedBy;
        public IUser CreatedBy {
            get {
                Load();
                return mCreatedBy;
            }
            internal set {
                mCreatedBy = value;
            }
        }

        private DateTime mCreatedDate;
        public DateTime CreatedDate {
            get {
                Load();
                return mCreatedDate;
            }
            internal set {
                mCreatedDate = value;
            }
        }

        private IUser mUpdatedBy;
        public IUser UpdatedBy {
            get {
                Load();
                return mUpdatedBy;
            }
            internal set {
                mUpdatedBy = value;
            }
        }

        private DateTime mUpdatedDate;
        public DateTime UpdatedDate {
            get {
                Load();
                return mUpdatedDate;
            }
            internal set {
                mUpdatedDate = value;
            }
        }

        public virtual bool CanRead(IUser user) {
            return true;
        }

        public virtual bool CanUpdate(IUser user) {
            return true;
        }

        protected void Load() {
            if (!mIsLoaded) {
                Refresh();
                mIsLoaded = true;
            }
        }

        public abstract void Refresh();

        protected void RefreshFrom(IBaseObject obj) {
            this.CreatedBy = obj.CreatedBy;
            this.CreatedDate = obj.CreatedDate;
            this.UpdatedBy = obj.UpdatedBy;
            this.UpdatedDate = obj.UpdatedDate;
        }
    }
}
