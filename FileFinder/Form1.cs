using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileFinder
{
    public partial class FileFinder : Form
    {
        int seq;
        int w1, w2, w3, w4, w5, w6, hh, hr;
        Color hbg = Color.Blue;
        Color fcl = Color.Yellow;
        Font hf = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        List<TextBox> npathList;
        List<TextBox> nfilterList;
        List<ComboBox> ncontentList;
        List<FlowLayoutPanel> nrowList;
        public FileFinder()
        {
            InitializeComponent();
            windowBox.AutoScroll = true;
            windowBox.WrapContents = false;

            nrowList = new List<FlowLayoutPanel>();
            npathList = new List<TextBox>();
            nfilterList = new List<TextBox>();
            ncontentList = new List<ComboBox>();
            

            w1 = 20; w2 = 60; w3 = 240; w4 = 80; w5 = 180; w6 = 50;            
            hh = 25; hr = 25;
            seq = 0;

            addHeader();
        }

        private void addHeader() {
            //1
            Label hid = new Label();
            hid.Text = "ID";
            hid.Width = w1;
            hid.TextAlign = ContentAlignment.MiddleCenter;
            hid.BackColor = hbg;
            hid.ForeColor = fcl;
            hid.Font = hf;
            hid.Tag = -1;
            //2
            Label htitle = new Label();
            htitle.Text = "TITLE";
            htitle.Width = w2;
            htitle.TextAlign = ContentAlignment.MiddleCenter;
            htitle.BackColor = hbg;
            htitle.ForeColor = fcl;
            htitle.Font = hf;
            htitle.Tag = -1;
            //3
            Label hpath = new Label();
            hpath.Text = "PATH";
            hpath.Width = w3;
            hpath.TextAlign = ContentAlignment.MiddleCenter;
            hpath.BackColor = hbg;
            hpath.ForeColor = fcl;
            hpath.Font = hf;
            hpath.Tag = -1;

            //4
            Label hfilter = new Label();
            hfilter.Text = "FILTER";
            hfilter.Width = w4;
            hfilter.TextAlign = ContentAlignment.MiddleCenter;
            hfilter.BackColor = hbg;
            hfilter.ForeColor = fcl;
            hfilter.Font = hf;
            hfilter.Tag = -1;
            //5
            Label hcontent = new Label();
            hcontent.Text = "CONTENT";
            hcontent.Width = w5;
            hcontent.TextAlign = ContentAlignment.MiddleCenter;
            hcontent.BackColor = hbg;
            hcontent.ForeColor = fcl;
            hcontent.Font = hf;
            hcontent.Tag = -1;
            //6
            Label hopen = new Label();
            hopen.Text = "OPEN";
            hopen.Width = w6;
            hopen.TextAlign = ContentAlignment.MiddleCenter;
            hopen.BackColor = hbg;
            hopen.ForeColor = fcl;
            hopen.Font = hf;
            hopen.Tag = -1;
            
            FlowLayoutPanel hrow = new FlowLayoutPanel();
            hrow.FlowDirection = FlowDirection.LeftToRight;
            hrow.Width = windowBox.Width;
            hrow.Tag = -1;
            hrow.Controls.Add(hid);
            hrow.Controls.Add(htitle);
            hrow.Controls.Add(hpath);
            hrow.Controls.Add(hfilter);
            hrow.Controls.Add(hcontent);           
            hrow.Controls.Add(hopen);
            hrow.Height = hh;

            windowBox.Controls.Add(hrow);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {                        
            //1
            Label nid = new Label();
            nid.Text = seq.ToString();
            nid.Width = w1;
            nid.TextAlign = ContentAlignment.MiddleCenter;
            nid.Tag = seq;
            //2
            TextBox ntitle = new TextBox();
            ntitle.Width = w2;
            ntitle.Tag = seq;
            
            //3
            TextBox npath = new TextBox();
            npath.Width = w3;
            npath.Name = "path";
            npath.Tag = seq;
            npath.KeyDown += getContent;
            //4
            TextBox nfilter = new TextBox();
            nfilter.Width = w4;
            nfilter.Name = "filter";
            nfilter.Tag = seq;
            nfilter.KeyDown += filterContent;
            //5
            ComboBox ncontent = new ComboBox();
            ncontent.Width = w5;
            ncontent.Name = "content";
            ncontent.Tag = seq;
            //ncontent.SelectedIndexChanged += cbxContent_SelectedIndexChanged;
            //6
            Button nopen = new Button();
            nopen.Width = w6;
            nopen.Text = ">>";
            nopen.Name = "open";
            nopen.Tag = seq;
            nopen.Click += btnOpen_Click;

            FlowLayoutPanel nrow = new FlowLayoutPanel();
            nrow.FlowDirection = FlowDirection.LeftToRight;
            nrow.Width = windowBox.Width;       
            nrow.Tag = seq;

            nrow.Controls.Add(nid);
            nrow.Controls.Add(ntitle);
            nrow.Controls.Add(npath);
            nrow.Controls.Add(nfilter);
            nrow.Controls.Add(ncontent);
            nrow.Controls.Add(nopen);
            nrow.Height = hr;

            npathList.Add(npath);
            nfilterList.Add(nfilter);
            ncontentList.Add(ncontent);
            nrowList.Add(nrow);
            
            windowBox.Controls.Add(nrow);
            
            seq++;
        }
        private void filterContent(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox trigger = (TextBox)sender;
                int index = (int)trigger.Tag;
                String filter = trigger.Text;
                MessageBox.Show(ncontentList[index].Items.ToString());
                foreach (object it in ncontentList[index].Items)
                {
                    Console.WriteLine(it.ToString());
                }
            }
        }
        private void getContent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                TextBox trigger = (TextBox)sender;

                int index = (int)trigger.Tag;
                String path = trigger.Text;

                try
                {
                    string[] files = Directory.GetFileSystemEntries(path);
                    ncontentList[index].Items.Clear();
                    foreach (string f in files)
                    {
                        // to filter content ....
                        ncontentList[index].Items.Add(Path.GetFileName(f));
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                Button trigger = (Button)sender;
                int index = (int)trigger.Tag;
                string name = ncontentList[index].SelectedItem.ToString();
                string prefix = npathList[index].Text;
                string path = prefix + "\\" + name;

                if (Directory.Exists(path))
                {
                    // open 
                    Process.Start("explorer.exe", path);

                }
                else if (File.Exists(path))
                {
                    // open notepad
                    Process.Start("notepad++.exe", path);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
