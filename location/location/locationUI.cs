using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Location;

namespace location
{
    public partial class locationUI : Form
    {
        public locationUI()
        {
            InitializeComponent();
            ToolTip ipToolTip = new ToolTip();
            ipToolTip.SetToolTip(this.ipInfoButton, "Enter the IP of the server you wish to connect to.");
            ToolTip portToolTip = new ToolTip();
            portToolTip.SetToolTip(this.portInfoButton, "Enter the port of the server you wish to connect to.");
            ToolTip nameToolTip = new ToolTip();
            nameToolTip.SetToolTip(this.nameInfoButton, "Enter the name you wish to update to the server.");
            ToolTip locationToolTip = new ToolTip();
            locationToolTip.SetToolTip(this.locationInfoButton, "Enter the location you wish to update to the server");
            ToolTip protocolTooTip = new ToolTip();
            protocolTooTip.SetToolTip(this.protocolInfoButton, "Select the protocol you wish to use to connect to the server");
            //This is initalises the UI and also sets up the tool tips for the information buttons.
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            LocationClass client = new LocationClass();
            string h = "-h";
            string ip = ipTextBox.Text;
            string p = "-p";
            string port = portTextBox.Text;
            string name = nameTextBox.Text;
            string location = locationTextBox.Text;
            string protocol = null;
            //This pulls all of the information from the UI
            if (protocolComboBox.SelectedItem.ToString() == "HTTP 0.9")
            {
                protocol = "-h9";
            }
            else if (protocolComboBox.SelectedItem.ToString() == "HTTP 1.0")
            {
                protocol = "-h0";
            }
            else if (protocolComboBox.SelectedItem.ToString() == "HTTP 1.1")
            {
                protocol = "-h1";
            }
            else
            {
              
            }
            //This sets the protocol depending on what one was chosen.
            List<string> userInputList = new List<string>();
            userInputList.Add(h);
            userInputList.Add(ip);
            userInputList.Add(p);
            userInputList.Add(port);
            userInputList.Add(name);
            userInputList.Add(location);
            userInputList.Add(protocol);
            string[] userInput = new string[userInputList.Count()];
            //This sorts the inputs into a array as long as the number of inputs put in.
            for (int i =0; i < userInputList.Count(); i++)
            {
                if(userInputList[i] != null)
                {
                    userInput[i] = userInputList[i];
                }
            }
            if (name == "" || name == null)
            {
                clientOutput.Text += "Please enter a name \r\n";
            }
            else
            {
                string clientOutputString = client.RunClient(userInput);
                clientOutput.Text += clientOutputString;
            }
            //This checks that the user entered a name, and if so runs the request.
        }

        private void ipInfoButton_Click(object sender, EventArgs e)
        {           
            MessageBox.Show("Enter here the IP you wish the client to connect to, however if you can leave this blank and it will default to whois.dcs.hull.ac.uk.");
        }

        private void portInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter the port you wish the client to connect to, however if you leave this blank it will default to 43.");
        }

        private void nameInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter the name you wish the client to update to the server, this is essential and this is the only feild that is required to be filled.");
        }

        private void locationInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter the location you wish the client to update to the server, this can be left blank however it will not update your location, it will search the database for the location of the name you entered.");
        }

        private void protocolInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chose the protocol you wish to use to connect to the server, if you leave this blank it will default to WhoIs.");
        }
        //These methods are just the information buttons, if pressed they give a more in depth explanation of the fields.
        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            clientOutput.Clear();
        }
        //This clears the log.
        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //This closes the program.
    }
}
