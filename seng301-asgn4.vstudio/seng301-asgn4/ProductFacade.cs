﻿using System;
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
            return hf.ProductList[index].Cost.Value;
        }

        public void Dispense(int index)
        {
            Vend.Hardware.ProductRacks.ElementAt(index).DispenseProduct();
        }

    }
}