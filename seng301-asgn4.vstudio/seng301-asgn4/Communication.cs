using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend4;
using Frontend4.Hardware;

namespace seng301_asgn4
{
    public class CommunicationFacade
    {
        public event EventHandler<SelectionEventArgs> SelectionMade;

        HardwareFacade hf;
        int buttonNum;
        public CommunicationFacade(HardwareFacade hf)
        {
            this.hf = hf;
            SetListeners();
        }


        public void SetListeners()
        {
            foreach (SelectionButton B in this.hf.SelectionButtons)
            {
                B.Pressed += new EventHandler(ButtonPressed);
            }
        }

        // What happens when hears a button was pressed
        // gets index value of the button
        public void ButtonPressed(object sender, EventArgs e)
        {
            int index = 0;
            foreach (SelectionButton b in hf.SelectionButtons)
            {
                if ((SelectionButton)sender == b)
                {
                    Selection(index);
                    break;
                }
                index++;
            }
        }


        // Logic should subscribe to this
        // Sends button index to listeners (logic)
        public void Selection(int index)
        {
            if (this.SelectionMade != null)
            {
                this.SelectionMade(this, new SelectionEventArgs() {Index = index });
            }
        }

    }

    // new subclass of event args
    public class SelectionEventArgs: EventArgs
    {
        public int Index { get; set; }
    }
}
