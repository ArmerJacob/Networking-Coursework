using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using locationserver;

namespace locationserver
{
    public partial class locationServerUI : Form
    {
        public locationServerUI()
        {
            InitializeComponent();
            ToolTip portToolTip = new ToolTip();
            portToolTip.SetToolTip(this.portInfoButton, "Enter the port you want clients to be able to connect to.");
            ToolTip nameToolTip = new ToolTip();
            ToolTip timeoutToolTip = new ToolTip();
            timeoutToolTip.SetToolTip(this.timeoutInfoButton, "Enter the amount of time you want before the server disconets a client.");
        }
        //This initialises the UI, and sets up the tool tips for the information buttons.
        public void SetValues(int port, int timeout)
        {
            portTextBox.Text = port.ToString();
            timeoutTextBox.Text = timeout.ToString();
        }
        //this sets the values inputted from the args to the UI.
        private void runServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                int port = int.Parse(portTextBox.Text);
                int timeout = int.Parse(timeoutTextBox.Text);
                this.Close();
                LocationServer.runServer(port, timeout);
            }
            catch
            {
                MessageBox.Show("Please enter a number");
            }

        }
        //This button runs the server and gives the server the timeout and port entered in the the UI.
        private void portInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter the port that the server will use for clients to connect through, if you leave this blank it will default to 43.");
        }

        private void timeoutInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter the amount of milliseconds you want before the server disconnect a client for not responding, if you leave this blank it will default to 1000");
        }
        //These methods are extra information on the info buttons, when you press them they open a message box.
        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //This closes the program.
    }
}
