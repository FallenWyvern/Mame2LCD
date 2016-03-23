using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mame2LCD
{
    public partial class Form1 : Form
    {
        System.Threading.Thread scrapeThread;
        processScraper scraper;        

        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;

            scrapeThread = new System.Threading.Thread(StartScrape);
            scrapeThread.Start();
            this.ShowInTaskbar = false;

            this.FormClosing += (sender, e) =>
            {
                scrapeThread.Abort();
                mynotifyicon.Visible = false;
            };
        }

        void Form1_Resize(object sender, EventArgs e)
        {
            popUp();       
        }

        private void popUp()
        {            
                mynotifyicon.Visible = true;
            
                if (this.WindowState == FormWindowState.Minimized)
                {                                        
                    this.Hide();
                }                
        }

        private void StartScrape()
        {
            scraper = new processScraper();
            this.FormClosed += (sender, e) =>
            {
                scraper.running = false;                
            };            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;            
        }

        private void mynotifyicon_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Show();            
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void mynotifyicon_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
}

