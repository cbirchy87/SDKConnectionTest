using Paxton.Net2.OemClientLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDKConnectionTest
{
    public partial class Form1 : Form
    {
        public static ResourceManager rm;
        public Form1()
        {
            InitializeComponent();

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            if (currentCulture.Name == "nl")
            {
                rm = new ResourceManager("SDKConnectionTest.nl_local", Assembly.GetExecutingAssembly());
            }
            else
            {
                rm = new ResourceManager("SDKConnectionTest.en_local", Assembly.GetExecutingAssembly());
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = rm.GetString("Connecting");
            Task.Run(() =>
            {
                try
                {
                    OemClient c = new OemClient(tbHost.Text, 8025);
                    var operatorList = c.GetListOfOperators();
                    if (operatorList != null)
                    {
                        MessageBox.Show(rm.GetString("Connecting Sucessful"));
                        toolStripStatusLabel1.Text = rm.GetString("Ready");

                    }
                    else
                    {
                        MessageBox.Show(rm.GetString("Failed to Connect"));
                        toolStripStatusLabel1.Text = rm.GetString("Ready");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    toolStripStatusLabel1.Text = rm.GetString("Ready");
                }
            });
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = rm.GetString("AppTitle");
            btnConnect.Text = rm.GetString("Connect");
            label1.Text = rm.GetString("Host");
            label2.Text = rm.GetString("Username");
            label3.Text = rm.GetString("Password");
            toolStripStatusLabel1.Text = rm.GetString("Ready");
            var x = rm.GetString("Ready");
        }
    }
}
