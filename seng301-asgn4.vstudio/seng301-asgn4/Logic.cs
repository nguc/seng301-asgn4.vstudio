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

        int amountPaid;

        public Logic(PaymentFacade Payment, ProductFacade Product, CommunicationFacade Com)
        {
            this.Payment = Payment;
            this.Com = Com;
            this.Product = Product;

            Com.SelectionMade += new EventHandler<SelectionEventArgs>(ButtonPressed);
            Payment.PaymentMade += new EventHandler<PaymentEventArgs>(PaymentAccepted);

        }
   
        // get product that is located at the button index
        public void ButtonPressed(object sender, SelectionEventArgs e)
        {
            int index = e.Index;
            int price = Product.getCost(index);

            if (price <= amountPaid)
            {
                Product.Dispense(index);
                int change = amountPaid - price;
                amountPaid = Payment.Change(change);
                Payment.StorePayment();       
            }
        }

        // increment payment value 
        public void PaymentAccepted(object sender, PaymentEventArgs e)
        {
            this.amountPaid += e.PaymentValue;
            
        }

   
    }
}
