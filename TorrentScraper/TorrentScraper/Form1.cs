using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorrentScraper.Class_Library;
using TorrentScraper.Enum;
using Timer = System.Windows.Forms.Timer;

namespace TorrentScraper
{
    public partial class Form1 : Form
    {
        private int tickerValue;
        Timer t = new Timer();
        private int lastClickedSite;
        private string tempURL;
        private string lastCheckedAnime;
        private Dictionary<string, Dictionary<string, string>> EpisodeDictionary;

        //form object declaration
        LoadingForm form = new LoadingForm();
        HorribleSubs horribleSubs = new HorribleSubs();
        DameDesuYo dameDesuYo = new DameDesuYo();

        public Form1()
        {
            InitializeComponent();
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            lblStatus.Text = "";
            lblTorStatus.Text = "";
            chkListAnime.Items.Clear();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Close();
        }

        private void DisableUsage()
        {
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
        }

        private void EnableUsage()
        {
            groupBox1.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
        }

        private void PopulateDataGrid(Dictionary<string, string> AnimeListing)
        {
            //reset datagridview and source
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            Application.EnableVisualStyles();
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.DataSource = (from show in AnimeListing
                                        orderby show.Key
                                        select new { show.Key, show.Value }).ToList();

            //hide url column
            dataGridView1.Columns[1].Visible = false;

            //resize column to fit
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                textBox1.Text = row.Cells[0].Value.ToString();
                switch (lastClickedSite)
                {
                    case (int)siteEnum.HorribleSubs:
                        tempURL = "http://horriblesubs.info/" + row.Cells[1].Value.ToString();
                        break;
                    case (int)siteEnum.damedesuyo:
                        tempURL = row.Cells[1].Value.ToString();
                        break;
                }
            }
        }

        private void btnHorribleSubs_Click(object sender, EventArgs e)
        {
            DisableUsage();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(this.Location.X + (this.Width - form.Width) / 2, this.Location.Y + (this.Height - form.Height) / 2);
            form.Show();

            //run processes
            HorribleSubsWorker.RunWorkerAsync();

            lastClickedSite = (int)siteEnum.HorribleSubs;
        }

        private void btndamedesuyo_Click(object sender, EventArgs e)
        {
            DisableUsage();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(this.Location.X + (this.Width - form.Width) / 2, this.Location.Y + (this.Height - form.Height) / 2);
            form.Show();

            //run processes
            damedesuyoWorker.RunWorkerAsync();

            lastClickedSite = (int)siteEnum.damedesuyo;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            DisableUsage();
            chkListAnime.Items.Clear();

            //reset text and variables for lastcheckedanime
            lastCheckedAnime = textBox1.Text;
            lblLowRes.Text = "Number of [480p] episodes (Magnet Link) =\t 0";
            lblMidRes.Text = "Number of [720p] episodes (Magnet Link) =\t 0";
            lblHiRes.Text = "Number of [1080p] episodes (Magnet Link) =\t 0";

            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(this.Location.X + (this.Width - form.Width) / 2, this.Location.Y + (this.Height - form.Height) / 2);
            form.Show();

            CheckingWorker.RunWorkerAsync();
        }

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Path to save magnet links";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void writeToText(int resolutionValue)
        {
            if (txtFilePath.TextLength > 0)
            {
                using (StreamWriter sw = new StreamWriter(txtFilePath.Text + "\\" + lastCheckedAnime + " episode magnet links.txt"))
                {
                    switch (resolutionValue)
                    {
                        case (int)resolutionEnum.LowRes:
                            if (checkBox1.Checked)
                            {
                                foreach (string animeTitle in chkListAnime.CheckedItems)
                                {
                                    sw.WriteLine(animeTitle);
                                    sw.WriteLine(EpisodeDictionary["LowRes"][animeTitle]);
                                }
                            }
                            else
                            {
                                foreach (var magnetLink in EpisodeDictionary["LowRes"])
                                {
                                    sw.WriteLine(magnetLink.Key);
                                    sw.WriteLine(magnetLink.Value);
                                }
                            }
                            break;
                        case (int)resolutionEnum.MidRes:
                            if (checkBox1.Checked)
                            {
                                foreach (string animeTitle in chkListAnime.CheckedItems)
                                {
                                    sw.WriteLine(animeTitle);
                                    sw.WriteLine(EpisodeDictionary["MidRes"][animeTitle]);
                                }
                            }
                            else
                            {
                                foreach (var magnetLink in EpisodeDictionary["MidRes"])
                                {
                                    sw.WriteLine(magnetLink.Key);
                                    sw.WriteLine(magnetLink.Value);
                                }
                            }
                            break;
                        case (int)resolutionEnum.HiRes:
                            if (checkBox1.Checked)
                            {
                                foreach (string animeTitle in chkListAnime.CheckedItems)
                                {
                                    sw.WriteLine(animeTitle);
                                    sw.WriteLine(EpisodeDictionary["HiRes"][animeTitle]);
                                }
                            }
                            else
                            {
                                foreach (var magnetLink in EpisodeDictionary["HiRes"])
                                {
                                    sw.WriteLine(magnetLink.Key);
                                    sw.WriteLine(magnetLink.Value);
                                }
                            }
                            break;
                    }
                }
            }
        }

        private void pushToTorrent(int resolutionValue)
        {
            if (txtTorrentPath.TextLength > 0)
            {
                List<string> tempList = new List<string>();
                if (checkBox1.Checked)
                {
                    tempList.AddRange(chkListAnime.CheckedItems.Cast<string>());
                }
                switch (resolutionValue)
                {
                    case (int)resolutionEnum.LowRes:
                        if (checkBox1.Checked)
                        {
                            Parallel.ForEach(tempList, animeTitle =>
                            {
                                if (EpisodeDictionary["LowRes"][animeTitle].ToString().ToLower().Contains("magnet:"))
                                {
                                    Process.Start(txtTorrentPath.Text, EpisodeDictionary["LowRes"][animeTitle].ToString());
                                }
                                else
                                {
                                    WebClient webClient = new WebClient();

                                    //apparently needed to get file name from url header
                                    var data = webClient.DownloadData("http://" + EpisodeDictionary["LowRes"][animeTitle].ToString());
                                    string name = webClient.ResponseHeaders["Content-Disposition"].Substring(webClient.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "").Trim();

                                    webClient.DownloadFile("http://" + EpisodeDictionary["LowRes"][animeTitle].ToString(), name);
                                    Process.Start(name);
                                }
                            });
                        }
                        else
                        {
                            //List<string> removeList = new List<string>();
                            Parallel.ForEach(EpisodeDictionary["LowRes"], magnetLink =>
                            {
                                if (magnetLink.ToString().ToLower().Contains("magnet:"))
                                {
                                    Process.Start(txtTorrentPath.Text, magnetLink.Value);
                                }
                                else
                                {
                                    WebClient webClient = new WebClient();

                                    //apparently needed to get file name from url header
                                    var data = webClient.DownloadData(magnetLink.Value);
                                    string name = webClient.ResponseHeaders["Content-Disposition"].Substring(webClient.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "").Trim();

                                    webClient.DownloadFile(magnetLink.Value, name);
                                    Process.Start(name);
                                    //removeList.Add(name);

                                    //sleeps thread when adding torrent. If removed this might cause torrent to be deleted before added to torrent client.
                                    //Thread.Sleep(2000);
                                }
                            });
                            //if (removeList.Count > 0)
                            //{
                            //    foreach (string name in removeList)
                            //    {
                            //        File.Delete(name);
                            //    }
                            //}
                        }
                        break;
                    case (int)resolutionEnum.MidRes:
                        if (checkBox1.Checked)
                        {
                            Parallel.ForEach(tempList, animeTitle =>
                            {
                                if (EpisodeDictionary["MidRes"][animeTitle].ToString().ToLower().Contains("magnet:"))
                                {
                                    Process.Start(txtTorrentPath.Text, EpisodeDictionary["MidRes"][animeTitle].ToString());
                                }
                                else
                                {
                                    WebClient webClient = new WebClient();

                                    //apparently needed to get file name from url header
                                    var data = webClient.DownloadData("http://" + EpisodeDictionary["MidRes"][animeTitle].ToString());
                                    string name = webClient.ResponseHeaders["Content-Disposition"].Substring(webClient.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "").Trim();

                                    webClient.DownloadFile("http://" + EpisodeDictionary["MidRes"][animeTitle].ToString(), name);
                                    Process.Start(name);
                                }
                            });
                        }
                        else
                        {
                            //List<string> removeList = new List<string>();
                            Parallel.ForEach(EpisodeDictionary["MidRes"], magnetLink =>
                            {
                                if (magnetLink.ToString().ToLower().Contains("magnet:"))
                                {
                                    Process.Start(txtTorrentPath.Text, magnetLink.Value);
                                }
                                else
                                {
                                    WebClient webClient = new WebClient();

                                    //apparently needed to get file name from url header
                                    var data = webClient.DownloadData(magnetLink.Value);
                                    string name = webClient.ResponseHeaders["Content-Disposition"].Substring(webClient.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "").Trim();

                                    webClient.DownloadFile(magnetLink.Value, name);
                                    Process.Start(name);
                                    //removeList.Add(name);

                                    //sleeps thread when adding torrent. If removed this might cause torrent to be deleted before added to torrent client.
                                    //Thread.Sleep(2000);
                                }
                            });
                            //if (removeList.Count > 0)
                            //{
                            //    foreach (string name in removeList)
                            //    {
                            //        File.Delete(name);
                            //    }
                            //}
                        }
                        break;
                    case (int)resolutionEnum.HiRes:
                        if (checkBox1.Checked)
                        {
                            Parallel.ForEach(tempList, animeTitle =>
                            {
                                if (EpisodeDictionary["HiRes"][animeTitle].ToString().ToLower().Contains("magnet:"))
                                {
                                    Process.Start(txtTorrentPath.Text, EpisodeDictionary["HiRes"][animeTitle].ToString());
                                }
                                else
                                {
                                    WebClient webClient = new WebClient();

                                    //apparently needed to get file name from url header
                                    var data = webClient.DownloadData("http://" + EpisodeDictionary["HiRes"][animeTitle].ToString());
                                    string name = webClient.ResponseHeaders["Content-Disposition"].Substring(webClient.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "").Trim();

                                    webClient.DownloadFile("http://" + EpisodeDictionary["HiRes"][animeTitle].ToString(), name);
                                    Process.Start(name);
                                }
                            });
                        }
                        else
                        {
                            //List<string> removeList = new List<string>();
                            Parallel.ForEach(EpisodeDictionary["HiRes"], magnetLink =>
                            {
                                if (magnetLink.ToString().ToLower().Contains("magnet:"))
                                {
                                    Process.Start(txtTorrentPath.Text, magnetLink.Value);
                                }
                                else
                                {
                                    WebClient webClient = new WebClient();

                                    //apparently needed to get file name from url header
                                    var data = webClient.DownloadData("http://" + magnetLink.Value);
                                    string name = webClient.ResponseHeaders["Content-Disposition"].Substring(webClient.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "").Trim();

                                    webClient.DownloadFile("http://" + magnetLink.Value, name);
                                    Process.Start(name);

                                    //sleeps thread when adding torrent. If removed this might cause torrent to be deleted before added to torrent client.
                                    //Thread.Sleep(2000);

                                    //removeList.Add(name);
                                }
                            });
                            //if (removeList.Count > 0)
                            //{
                            //    foreach (string name in removeList)
                            //    {
                            //        File.Delete(name);
                            //    }
                            //}
                        }
                        break;
                }
            }
        }

        private void btnLowRes_Click(object sender, EventArgs e)
        {

            lblStatus.Text = "";
            writeToText((int)resolutionEnum.LowRes);
            StatusChange();
        }

        private void btnMidRes_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            writeToText((int)resolutionEnum.MidRes);
            StatusChange();
        }

        private void btnHiRes_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            writeToText((int)resolutionEnum.HiRes);
            StatusChange();
        }

        private void btnTorLowRes_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            pushToTorrent((int)resolutionEnum.LowRes);
            TorStatusChange();
        }

        private void btnTorMidRes_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            pushToTorrent((int)resolutionEnum.MidRes);
            TorStatusChange();
        }

        private void btnTorHiRes_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            pushToTorrent((int)resolutionEnum.HiRes);
            TorStatusChange();
        }

        private void StatusChange()
        {
            lblStatus.Text = "Done...";
        }

        private void TorStatusChange()
        {
            lblTorStatus.Text = "Pushing...";
        }

        private void btnTorrentPath_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select uTorrent.exe";
            openFileDialog1.FileName = "uTorrent";
            openFileDialog1.Filter = "Executable | *.exe";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTorrentPath.Text = openFileDialog1.FileName;
            }
        }

        //backgroundwokers
        private void HorribleSubsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //this fetches the result and passes it to RunWorkerCompleted
            e.Result = horribleSubs.fetchAnimeList();
        }

        private void HorribleSubsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //populate dataGrid with dictionary from DoWork bgworker
            PopulateDataGrid((Dictionary<string, string>)e.Result);
            EnableUsage();
            form.Hide();
        }

        private void damedesuyoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = dameDesuYo.fetchAnimeList();
        }

        private void damedesuyoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PopulateDataGrid((Dictionary<string, string>)e.Result);
            EnableUsage();
            form.Hide();
        }

        private void CheckingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!string.IsNullOrEmpty(tempURL))
            {
                switch (lastClickedSite)
                {
                    case (int)siteEnum.HorribleSubs:
                        EpisodeDictionary = horribleSubs.GetEpisodes(tempURL);
                        break;
                    case (int)siteEnum.damedesuyo:
                        EpisodeDictionary = dameDesuYo.GetEpisodes(tempURL);
                        break;
                }
            }
        }

        private void CheckingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (lastClickedSite)
            {
                case (int)siteEnum.HorribleSubs:
                    if (EpisodeDictionary.Count > 0)
                    {
                        groupBox4.Enabled = true;
                        groupBox5.Enabled = true;
                        btnLowRes.Enabled = false;
                        btnMidRes.Enabled = false;
                        btnHiRes.Enabled = false;
                        btnTorLowRes.Enabled = false;
                        btnTorMidRes.Enabled = false;
                        btnTorHiRes.Enabled = false;

                        if (EpisodeDictionary.ContainsKey("LowRes"))
                        {
                            btnLowRes.Enabled = true;
                            btnTorLowRes.Enabled = true;
                            lblLowRes.Text = "Number of [480p] episodes (Magnet Link) =\t" + EpisodeDictionary["LowRes"].Count;
                            foreach (string lowResEp in EpisodeDictionary["LowRes"].Keys)
                            {
                                chkListAnime.Items.Add(lowResEp);
                            }
                        }
                        if (EpisodeDictionary.ContainsKey("MidRes"))
                        {
                            btnMidRes.Enabled = true;
                            btnTorMidRes.Enabled = true;
                            lblMidRes.Text = "Number of [720p] episodes (Magnet Link) =\t" + EpisodeDictionary["MidRes"].Count;
                            foreach (string midResEp in EpisodeDictionary["MidRes"].Keys)
                            {
                                chkListAnime.Items.Add(midResEp);
                            }
                        }
                        if (EpisodeDictionary.ContainsKey("HiRes"))
                        {
                            btnHiRes.Enabled = true;
                            btnTorHiRes.Enabled = true;
                            lblHiRes.Text = "Number of [1080p] episodes (Magnet Link) =\t" + EpisodeDictionary["HiRes"].Count;
                            foreach (string HiResEp in EpisodeDictionary["HiRes"].Keys)
                            {
                                chkListAnime.Items.Add(HiResEp);
                            }
                        }
                    }
                    else if (EpisodeDictionary.Count == 0)
                    {
                        lblLowRes.Text = "Number of [480p] episodes (Magnet Link) =\t 0";
                        lblMidRes.Text = "Number of [720p] episodes (Magnet Link) =\t 0";
                        lblHiRes.Text = "Number of [1080p] episodes (Magnet Link) =\t 0";
                        chkListAnime.Items.Clear();
                    }
                    break;
                case (int)siteEnum.damedesuyo:
                    if (EpisodeDictionary.Count > 0)
                    {
                        groupBox4.Enabled = true;
                        groupBox5.Enabled = true;
                        btnLowRes.Enabled = false;
                        btnMidRes.Enabled = false;
                        btnHiRes.Enabled = false;
                        btnTorLowRes.Enabled = false;
                        btnTorMidRes.Enabled = false;
                        btnTorHiRes.Enabled = false;

                        if (EpisodeDictionary.ContainsKey("LowRes"))
                        {
                            btnLowRes.Enabled = true;
                            btnTorLowRes.Enabled = true;
                            var first = EpisodeDictionary["LowRes"].First();
                            if (EpisodeDictionary["LowRes"].Keys.Count == 1 || first.Key.ToLower().Contains("batch"))
                            {
                                lblMidRes.Text = "Number of [480p] episodes (Magnet Link) =\t" + " Batch file";
                                chkListAnime.Items.Add(first.Key);
                            }
                            else
                            {
                                lblLowRes.Text = "Number of [480p] episodes (Magnet Link) =\t" + EpisodeDictionary["LowRes"].Count;
                                foreach (string lowResEp in EpisodeDictionary["LowRes"].Keys)
                                {
                                    chkListAnime.Items.Add(lowResEp);
                                }
                            }
                        }
                        if (EpisodeDictionary.ContainsKey("MidRes"))
                        {
                            btnMidRes.Enabled = true;
                            btnTorMidRes.Enabled = true;
                            var first = EpisodeDictionary["MidRes"].First();
                            if (EpisodeDictionary["MidRes"].Keys.Count == 1 || first.Key.ToLower().Contains("batch"))
                            {
                                lblMidRes.Text = "Number of [720p] episodes (Magnet Link) =\t" + " Batch file";
                                chkListAnime.Items.Add(first.Key);
                            }
                            else
                            {
                                lblMidRes.Text = "Number of [720p] episodes (Magnet Link) =\t" + EpisodeDictionary["MidRes"].Count;
                                foreach (string midResEp in EpisodeDictionary["MidRes"].Keys)
                                {
                                    chkListAnime.Items.Add(midResEp);
                                }
                            }
                        }
                        if (EpisodeDictionary.ContainsKey("HiRes"))
                        {
                            btnHiRes.Enabled = true;
                            btnTorHiRes.Enabled = true;
                            var first = EpisodeDictionary["HiRes"].First();
                            if (EpisodeDictionary["HiRes"].Keys.Count == 1 || first.Key.ToLower().Contains("batch"))
                            {
                                lblMidRes.Text = "Number of [1080p] episodes (Magnet Link) =\t" + " Batch file";
                                chkListAnime.Items.Add(first.Key);
                            }
                            else
                            {
                                lblHiRes.Text = "Number of [1080p] episodes (Magnet Link) =\t" + EpisodeDictionary["HiRes"].Count;
                                foreach (string HiResEp in EpisodeDictionary["HiRes"].Keys)
                                {
                                    chkListAnime.Items.Add(HiResEp);
                                }
                            }
                        }
                    }
                    else if (EpisodeDictionary.Count == 0)
                    {
                        lblLowRes.Text = "Number of [480p] episodes (Magnet Link) =\t 0";
                        lblMidRes.Text = "Number of [720p] episodes (Magnet Link) =\t 0";
                        lblHiRes.Text = "Number of [1080p] episodes (Magnet Link) =\t 0";
                        chkListAnime.Items.Clear();
                    }
                    break;
            }

            EnableUsage();
            form.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                chkListAnime.Enabled = true;
            }
            else
            {
                chkListAnime.Enabled = false;
            }
        }

    }
}
