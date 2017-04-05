using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend4.Hardware;
using Frontend4;

namespace seng301_asgn4
{
    public class Logic
    {
        PaymentFacade Payment;
        CommunicationFacade Com;
        ProductFacade Product;

        int paid;
        int index;
       

        int[] CoinIndex;

        public Logic(PaymentFacade Payment, ProductFacade Product, CommunicationFacade Com)
        {
            this.Payment = Payment;
            this.Com = Com;
            this.Product = Product;

            Com.SelectionMade += new EventHandler<SelectionEventArgs>(ButtonPressed);
            Payment.PaymentMade += new EventHandler<PaymentEventArgs>(PaymentAccepted);

        }

        public void Change(int change)
        {
            Cents[]  Denom = Payment.GetCoinKinds();
            int j;
            int k;
            while(change > Denom[0].Value)
            {
                k = 0;
                while(k < Denom.Length)
                {
                    j = Denom[k].Value;
                    if(j <= change)
                    {
                        change = change - j;
                        Payment.DispenseCoin(k);
                    }
                    k++;
                }
            }
        }
        public void ButtonPressed(object sender, SelectionEventArgs e)
        {
            this.index = e.Index;
        }

        public void PaymentAccepted(object sender, PaymentEventArgs e)
        {
            this.paid = +e.PaymentValue;
        }
    }
}
