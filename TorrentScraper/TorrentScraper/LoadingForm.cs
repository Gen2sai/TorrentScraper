using System;
using System.Windows.Forms;

namespace TorrentScraper
{
    public partial class LoadingForm : Form
    {
        public int i = 0;
        public LoadingForm()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;
            ShowIcon = false;
            pictureBox1.Image = Properties.Resources.loading;
            Timer t = new Timer();
            t.Tick += new EventHandler(loadingText);
            t.Interval = 1000;
            t.Start();
        }

        private void loadingText(object sender, EventArgs e)
        {
            if (i == 0)
            {
                label1.Text = "Loading .";
                i++;
            }
            else if (i == 1)
            {
                label1.Text = "Loading ..";
                i++;
            }
            else
            {
                label1.Text = "Loading ...";
                i = 0;
            }
        }

    }
}
