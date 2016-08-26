using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface ICurrency {
        string Code { get; set; }
        string Description { get; set; }

        IMoney Convert(IMoney money);
    }
}
