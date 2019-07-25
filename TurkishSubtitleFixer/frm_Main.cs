using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace TurkishSubtitleFixer
{
    public partial class frm_Main : Form
    {
        public frm_Main(string[] args)
        {
            InitializeComponent();
            fix_subtitles(args);
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            //This application fixes subtitle unicode for Turkish subtitle 
            //files (.srt) that it will be shown correctly on every DVD player
        }

        private void frm_Main_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
            //e.Effect = DragDropEffects.Copy;

            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void frm_Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
            fix_subtitles(array);
        }

        private void fix_subtitles(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (Path.GetExtension(files[i]) == ".srt")
                {
                    try
                    {
                        string text = files[i];
                        StreamReader streamReader = new StreamReader(text, Encoding.GetEncoding("ISO-8859-9"));
                        string value = streamReader.ReadToEnd();
                        streamReader.Close();
                        System.IO.File.Move(text, text + ".bkup");
                        StreamWriter streamWriter = new StreamWriter(text, false, Encoding.UTF8);
                        streamWriter.Write(value);
                        streamWriter.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Bad or corrupted file!");
                    }
                }
                else
                {
                    MessageBox.Show("Unknown file type!");
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://twitter.com/hosseinpourziyaie");
        }
    }
}
