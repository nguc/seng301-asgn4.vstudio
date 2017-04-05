using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend4;
using Frontend4.Hardware;

namespace seng301_asgn4
{
    public class ProductFacade
    {
        HardwareFacade hf;

        public ProductFacade(HardwareFacade hf)
        {
            this.hf = hf;
        }

        public int getCost(int index)
        {
            return this.hf.ProductKinds[index].Cost.Value;
        }

        public string Dispense(int index)
        {
            hf.ProductRacks.ElementAt(index).DispenseProduct();
            string productName = hf.ProductRacks.ElementAt(index).ToString();
            return productName;
        }

        public void LoadProducts(int[] prodCount)
        {
            hf.LoadProducts(prodCount);
        }
    }
}
