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
        int deleteId;
        int screenWidth_old, screenWidth;
        int w1, w2, w3, w4, w5, w6, hh, hr;
        Color hbg = Color.Blue;
        Color fcl = Color.Yellow;
        Font hf = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        FlowLayoutPanel hrow;
        Label hid, htitle, hpath, hfilter, hcontent, hopen;
        List<TextBox> npathList;
        List<TextBox> nfilterList;
        List<ComboBox> ncontentList;
        List<FlowLayoutPanel> nrowList;

        public FileFinder()
        {
            InitializeComponent();
            
            
        }
        private void FileFinder_Load(object sender, EventArgs e)
        {
            deleteId = -1;
            screenWidth_old = screenWidth = outerBox.Width;
            outerBox.AutoScroll = true;
            outerBox.WrapContents = false;
            outerBox.BackColor = Color.Transparent;

            nrowList = new List<FlowLayoutPanel>();
            npathList = new List<TextBox>();
            nfilterList = new List<TextBox>();
            ncontentList = new List<ComboBox>();

            //w1 = 30; w2 = 70; w3 = 300; w4 = 100; w5 = 200; w6 = 50;
            //.03,       .1,       .4,     .127     .286      .08
            //          .1         0.4     0.2      .3
            w1 = 20; w2 = 80;  w6 = 50; 
            w3 = (int)((screenWidth - w1 - w2 - w6 - 5) * .5);
            w4 = (int)((screenWidth - w1 - w2 - w6 - 5) * .2);
            w5 = (int)((screenWidth - w1 - w2 - w6 - 5) * .2);
            hh = 25; hr = 25;
            seq = 0;

            addHeader();
            addRow();


        }
        private void FileFinder_ResizeEnd(object sender, EventArgs e)
        {
            resize();
        }

       
        private void resize() {

            screenWidth = outerBox.Width;

            //w1 = 30; w2 = 70; w6 = 50;
            w3 = (int)((screenWidth - w1 - w2 - w6 - 5) * .5);
            w4 = (int)((screenWidth - w1 - w2 - w6 - 5) * .2);
            w5 = (int)((screenWidth - w1 - w2 - w6 - 5) * .2);

            // header
            hrow.Width = screenWidth;
            hpath.Width = w3;
            hfilter.Width = w4;
            hcontent.Width = w5;

            foreach (FlowLayoutPanel l in nrowList)
            {
                l.Width = screenWidth;
            }
            // 3
            foreach (TextBox t in npathList)
            {
                t.Width = w3;
            }
            // 4
            foreach (TextBox t in nfilterList)
            {
                t.Width = w4;
            }
            //5
            foreach (ComboBox t in ncontentList)
            {
                t.Width = w5;
            }
            

        }

        private void addHeader() {
            //1
            hid = new Label();
            hid.Text = "ID";
            hid.Width = w1;
            hid.TextAlign = ContentAlignment.MiddleCenter;
            hid.BackColor = hbg;
            hid.ForeColor = fcl;
            hid.Font = hf;
            hid.Tag = -1;
            //2
            htitle = new Label();
            htitle.Text = "TITLE";
            htitle.Width = w2;
            htitle.TextAlign = ContentAlignment.MiddleCenter;
            htitle.BackColor = hbg;
            htitle.ForeColor = fcl;
            htitle.Font = hf;
            htitle.Tag = -1;
            //3
            hpath = new Label();
            hpath.Text = "PATH";
            hpath.Width = w3;
            hpath.TextAlign = ContentAlignment.MiddleCenter;
            hpath.BackColor = hbg;
            hpath.ForeColor = fcl;
            hpath.Font = hf;
            hpath.Tag = -1;

            //4
            hfilter = new Label();
            hfilter.Text = "FILTER";
            hfilter.Width = w4;
            hfilter.TextAlign = ContentAlignment.MiddleCenter;
            hfilter.BackColor = hbg;
            hfilter.ForeColor = fcl;
            hfilter.Font = hf;
            hfilter.Tag = -1;
            //5
            hcontent = new Label();
            hcontent.Text = "CONTENT";
            hcontent.Width = w5;
            hcontent.TextAlign = ContentAlignment.MiddleCenter;
            hcontent.BackColor = hbg;
            hcontent.ForeColor = fcl;
            hcontent.Font = hf;
            hcontent.Tag = -1;
            //6
            hopen = new Label();
            hopen.Text = "OPEN";
            hopen.Width = w6;
            hopen.TextAlign = ContentAlignment.MiddleCenter;
            hopen.BackColor = hbg;
            hopen.ForeColor = fcl;
            hopen.Font = hf;
            hopen.Tag = -1;
            
            hrow = new FlowLayoutPanel();
            hrow.FlowDirection = FlowDirection.LeftToRight;
            hrow.Width = outerBox.Width;
            hrow.Tag = -1;
            hrow.Controls.Add(hid);
            hrow.Controls.Add(htitle);
            hrow.Controls.Add(hpath);
            hrow.Controls.Add(hfilter);
            hrow.Controls.Add(hcontent);           
            hrow.Controls.Add(hopen);
            hrow.Height = hh;
            hrow.BackColor = Color.Transparent;

            outerBox.Controls.Add(hrow);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addRow();
        }
        private void addRow() {
            //1
            Label nid = new Label();
            nid.Text = seq.ToString();
            nid.Width = w1;
            nid.TextAlign = ContentAlignment.MiddleCenter;
            nid.Tag = seq;
            nid.DoubleClick += id_DoubleClick;
            if (seq % 3 == 0)
            {
                nid.BackColor = Color.OrangeRed;

            }
            else if (seq % 3 == 1)
            {
                nid.BackColor = Color.GreenYellow;
            }
            else if (seq % 3 == 2)
            {
                nid.BackColor = Color.CornflowerBlue;
            }
            //2
            TextBox ntitle = new TextBox();
            ntitle.Width = w2;
            ntitle.Tag = seq;

            //3
            TextBox npath = new TextBox();
            npath.Width = w3;
            npath.Name = "path";
            npath.Tag = seq;
            npath.TextChanged += getContent;
            npath.DoubleClick += txtPath_DoubleClick;
            npath.KeyDown += enter_copy;
            //4
            TextBox nfilter = new TextBox();
            nfilter.Width = w4;
            nfilter.Name = "filter";
            nfilter.Tag = seq;
            nfilter.TextChanged += filterContent;
            //5
            ComboBox ncontent = new ComboBox();
            ncontent.Width = w5;
            ncontent.Name = "content";
            ncontent.DropDownStyle = ComboBoxStyle.DropDownList;
            ncontent.Tag = seq;
            ncontent.KeyDown += enter_copy;
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
            nrow.Width = outerBox.Width;
            nrow.Tag = seq;

            nrow.Controls.Add(nid);
            nrow.Controls.Add(ntitle);
            nrow.Controls.Add(npath);
            nrow.Controls.Add(nfilter);
            nrow.Controls.Add(ncontent);
            nrow.Controls.Add(nopen);
            nrow.Height = hr;
            nrow.BackColor = Color.Transparent;
            

            npathList.Add(npath);
            nfilterList.Add(nfilter);
            ncontentList.Add(ncontent);
            nrowList.Add(nrow);
            
            outerBox.Controls.Add(nrow);

            seq++;
        
        }
        private void enter_copy(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                //Clipboard.SetText("x");
                String type = sender.GetType().Name;
                if (type == "TextBox")
                {
                    TextBox t = (TextBox)sender;
                    if (t.Text.Length > 0) { Clipboard.SetText(t.Text); }
                }else if(type == "ComboBox")
                {
                    ComboBox c = (ComboBox)sender;
                    if (c.SelectedIndex >= 0) { Clipboard.SetText(c.SelectedItem.ToString()); }
                    
                }
            }

 
        }

        private void id_DoubleClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            int index = (int)l.Tag;
            if (deleteId < 0)
            {
                deleteId = index;
                nrowList[deleteId].BorderStyle = BorderStyle.FixedSingle;
            }
            else if (deleteId == index)
            {
                nrowList[deleteId].BorderStyle = BorderStyle.None;
                deleteId = -1;
                //nrowList[deleteId].BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                nrowList[deleteId].BorderStyle = BorderStyle.None;
                deleteId = index;
                nrowList[deleteId].BorderStyle = BorderStyle.FixedSingle;
            }           

        }
        private void txtPath_DoubleClick(object sender, EventArgs e)
        {
            TextBox trigger = (TextBox)sender;
            int index = (int)trigger.Tag;
            string path = trigger.Text;
            if (Directory.Exists(path))
            {
                // open 
                Process.Start("explorer.exe", path);
            }
            else if (File.Exists(path))
            {
                // open notepad
                string pp = Directory.GetParent(path).ToString();
                //MessageBox.Show(p);
                //Process.Start("notepad++.exe", path);
                Process.Start("explorer.exe", pp);
            }
        }
        private void filterContent(object sender, EventArgs e) {
            TextBox trigger = (TextBox)sender;
            int index = (int)trigger.Tag;
            string filter = trigger.Text.ToLower();
            string path = npathList[index].Text;

            string[] files = Directory.GetFileSystemEntries(path);
            List<string> result = new List<string>();
            for (int i=0; i<files.Length; i++)
            {
                string _file = files[i].ToLower();

                if (_file.Contains(filter))
                {
                    
                    result.Add(Path.GetFileName(files[i]));
                    
                }
            }
            
            ncontentList[index].Items.Clear();
            if (result.Count > 0) {
                ncontentList[index].Items.AddRange(result.ToArray());
                ncontentList[index].SelectedIndex = 0;
            }
            
        }

        private void getContent(object sender, EventArgs e)
        {
            
            TextBox trigger = (TextBox)sender;
            int index = (int)trigger.Tag;
            String path = trigger.Text;
            bool isDir = Directory.Exists(path);
            bool isFile = File.Exists(path);
            if (isDir)
            {
                nfilterList[index].Enabled = true;
                ncontentList[index].Enabled = true;

                string[] files = Directory.GetFileSystemEntries(path);
                ncontentList[index].Items.Clear();
                foreach (string f in files)
                {
                    // to filter content ....
                    ncontentList[index].Items.Add(Path.GetFileName(f));             
                }
                if (ncontentList[index].Items.Count > 0)
                {
                    ncontentList[index].SelectedIndex = 0;
                }
                
            }
            else if (isFile) 
            {
                nfilterList[index].Enabled = false;
                ncontentList[index].Enabled = false;
                                   
            }
            else  // not exist
            {
                nfilterList[index].Enabled = false;
                ncontentList[index].Enabled = false;
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Button trigger = (Button)sender;
            int index = (int)trigger.Tag;
            string path;
            if (ncontentList[index].SelectedIndex < 0)
            {
                path = npathList[index].Text;
            }
            else 
            {
                path = npathList[index].Text + "\\" + ncontentList[index].SelectedItem.ToString(); 
            }
            
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (deleteId >= 0)
            {
                nrowList[deleteId].Visible = false;
            }
        }







    }
}
