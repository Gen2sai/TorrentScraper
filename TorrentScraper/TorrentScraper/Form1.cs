using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TorrentScraper.Class_Library;

namespace TorrentScraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HorribleSubs horribleSubs = new HorribleSubs();
            horribleSubs.fetchAnimeList();
        }
    }
}
