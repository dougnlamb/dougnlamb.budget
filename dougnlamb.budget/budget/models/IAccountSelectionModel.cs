using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public interface IAccountSelectionModel {
        int SelectedAccountId { get; set; }
        IAccountViewModel SelectedAccount { get;  }
        IList<IAccountViewModel> Accounts { get; }
    }
}
