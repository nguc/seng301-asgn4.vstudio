using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend4;
using Frontend4.Hardware;

namespace seng301_asgn4
{
    public class PaymentFacade
    {
        public event EventHandler<PaymentEventArgs> PaymentMade;

        HardwareFacade HF;
        Cents[] CoinKinds;
        int coinValue;
        
        int Value;


        public PaymentFacade(HardwareFacade hf)
        {
            this.HF = hf;
            //this.CoinKinds = CoinKinds;
            hf.CoinSlot.CoinAccepted += new EventHandler<CoinEventArgs>(CoinAccept);
            
        }
        public void CoinAccept(object sender, CoinEventArgs e)
        {
            coinValue = e.Coin.Value.Value;
            Payment(coinValue);
        }


        public void Payment(int coinValue)
        {
            if (this.PaymentMade != null)
            {
                this.PaymentMade(this, new PaymentEventArgs() { PaymentValue = coinValue });
            }
        }


        public int getValue()
        {
            return Value;
        }

        public void DispenseCoin(int index)
        {
            HF.CoinRacks[index].ReleaseCoin();
        }

        public Cents[] GetCoinKinds()
        {
            return CoinKinds;
        }

    }



    // new Payment event args subclass
    public class PaymentEventArgs: EventArgs
    {
        public int PaymentValue { get; set; }
    }

}
