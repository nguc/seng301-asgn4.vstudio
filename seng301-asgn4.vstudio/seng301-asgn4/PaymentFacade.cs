﻿using System;
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

        HardwareFacade hf;
        Cents[] CoinKinds;
        Dictionary<int, int> coinKindToCoinRackIndex;
        int coinValue;
        
        public PaymentFacade(HardwareFacade hf)
        {
            this.hf = hf;
            this.coinKindToCoinRackIndex = new Dictionary<int, int>();
            for (int i = 0; i < this.hf.CoinRacks.Length; i++)
            {
                this.coinKindToCoinRackIndex[this.hf.GetCoinKindForCoinRack(i).Value] = i;
            }
            //this.CoinKinds = CoinKinds;
            hf.CoinSlot.CoinAccepted += new EventHandler<CoinEventArgs>(CoinAccept);         
        }


        // listens for payment to be made then sends value of the payment to listeners (logic)
        public void CoinAccept(object sender, CoinEventArgs e)
        {
            coinValue = e.Coin.Value.Value;
            Payment(coinValue);
        }

        // Fire event to logic giving it the value of the payment
        public void Payment(int coinValue)
        {
            if (this.PaymentMade != null)
            {
                this.PaymentMade(this, new PaymentEventArgs() { PaymentValue = coinValue });
            }
        }


        // Code taken from Tony Tang A2 solution - calculates change to be dispensed
        public int Change(int change)
        {
            while (change > 0)
            {
                var coinRacksWithMoney = this.coinKindToCoinRackIndex.Where(ck => ck.Key <= change && this.hf.CoinRacks[ck.Value].Count > 0).OrderByDescending(ck => ck.Key);

                if (coinRacksWithMoney.Count() == 0)
                {
                    return change; // this is what's left as available funds
                }

                var biggestCoinRackCoinKind = coinRacksWithMoney.First().Key;
                var biggestCoinRackIndex = coinRacksWithMoney.First().Value;
                var biggestCoinRack = this.hf.CoinRacks[biggestCoinRackIndex];

                change = change - biggestCoinRackCoinKind;
                biggestCoinRack.ReleaseCoin();
            }

            return 0;
        }

        public void StorePayment()
        {
            hf.CoinReceptacle.StoreCoins();
        }

    }



    // new Payment event args subclass
    public class PaymentEventArgs: EventArgs
    {
        public int PaymentValue { get; set; }
    }

}
