﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TorrentScraper.Class_Library;
using TorrentScraper.Enum;

namespace TorrentScraper
{
    public partial class Form1 : Form
    {
        private int lastClickedSite;
        LoadingForm form = new LoadingForm();

        public Form1()
        {
            InitializeComponent();
        }

        private void disableUsage()
        {
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
            groupBox3.Enabled = false;
        }

        private void enableUsage()
        {
            groupBox1.Enabled = true;
            groupBox3.Enabled = true;
            groupBox3.Enabled = true;
        }

        private void populateDataGrid(Dictionary<string, string> AnimeListing)
        {
            //reset datagridview and source
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            Application.EnableVisualStyles();
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.DataSource = (from show in AnimeListing
                                        orderby show.Key
                                        select new {show.Key, show.Value}).ToList();

            //hide url column
            dataGridView1.Columns[1].Visible = false;

            //resize column to fit
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnHorribleSubs_Click(object sender, EventArgs e)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(this.Location.X + (this.Width - form.Width) / 2, this.Location.Y + (this.Height - form.Height) / 2);
            form.Show();

            //run processes
            HorribleSubsWorker.RunWorkerAsync();

            lastClickedSite = (int)siteEnum.HorribleSubs;
        }

        private void HorribleSubsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            HorribleSubs horribleSubs = new HorribleSubs();
            
            //this fetches the result and passes it to RunWorkerCompleted
            e.Result = horribleSubs.fetchAnimeList();
        }

        private void HorribleSubsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //populate dataGrid with dictionary from DoWork bgworker
            populateDataGrid((Dictionary<string, string>)e.Result);
            form.Close();
        }
    }
}