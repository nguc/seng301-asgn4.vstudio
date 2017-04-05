using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seng301_asgn4
{
    class ProductFacade
    {
        VendingMachine Vend;
        public ProductFacade( VendingMachine VM)
        {
            Vend = VM;
        }
    }
}
