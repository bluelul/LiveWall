using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace LiveWall
{
    public partial class Form1 : Form
    {
        //=============================================
        // Define
        const string softwareName = "LiveWall";

        //=============================================
        // Init Param
        System.DayOfWeek dowNow = DateTime.Now.DayOfWeek;
        int hourNow = DateTime.Now.Hour;

        //=============================================
        // Global Variables
        string wallDirStr = Properties.Settings.Default["WallDirStr"].ToString();

        List<int> hourList = new List<int>();
        List<int> dowList = new List<int>();
        string[] wallList = new string[0];

        int lastHour = -1;
        int lastDow = -1;
        int lastIndexMatch = -1;

        int maxHourIndex = 0;
        int minHourIndex = 0;

        //=============================================
        // Init Process
        public Form1()
        {
            InitializeComponent();

            //-------------------------------
            // Check duplicate running application
            Process[] processlist = Process.GetProcesses();
            int count = 0;
            foreach (Process theprocess in processlist)
            {
                string processDesc;
                try
                {
                    processDesc = FileVersionInfo.GetVersionInfo(theprocess.MainModule.FileName).FileDescription;
                }
                catch
                {
                    processDesc = String.Empty;
                }
                if (processDesc == "LiveWall")
                    count++;
            }
            
            if (count > 1)
            {
                MessageBox.Show("Another LiveWall process is running","Error");

                notifyIcon1.Visible = false;
                System.Environment.Exit(1); 
            }
              
            //-------------------------------
            // load "Run at start up" state
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key.GetValue(softwareName) != null)
            {
                if (key.GetValue(softwareName).ToString() == Application.ExecutablePath)
                    cbxStartup.Checked = true;
                else
                    cbxStartup.Checked = false;
            }
            else
                cbxStartup.Checked = false;

            //-------------------------------
            // notify icon menu
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            
            //-------------------------------
            // load Wall Dir & Wall List
            string wallListStr = Properties.Settings.Default["WallListStr"].ToString();
            if ((wallDirStr != String.Empty) && (wallListStr != String.Empty))
            {
                // load wall list
                string[] wallListTemp = wallListStr.Split('\n');

                // check valid path files
                List<int> failIndex = new List<int>();
                for (int i = 0; i < wallListTemp.Length; i++)
                    if (File.Exists(Path.Combine(wallDirStr, wallListTemp[i])) == false)
                        failIndex.Add(i);
                
                // valid path
                if (failIndex.Count == 0)
                {
                    // process and save valid data
                    processValidData(wallListTemp);

                    // update wallpaper
                    UpdateWall();
                }
                
                // not valid path
                else
                {
                    string errorString = String.Empty;
                    for (int i = 0; i < failIndex.Count; i++)
                    {
                        errorString += Path.Combine(wallDirStr, wallListTemp[failIndex[i]]);
                        errorString += "\n";
                    }
                    errorString += "does not exist.";

                    wallDirStr = String.Empty;
                    wallListStr = String.Empty;
                    Properties.Settings.Default["WallDirStr"] = String.Empty;
                    Properties.Settings.Default["WallListStr"] = String.Empty;
                    Properties.Settings.Default.Save();
                    
                    MessageBox.Show(errorString, "Path Error");
                }
            }
            
            notifyIcon1.Visible = true;

            //-------------------------------
            // First Run
            if (Properties.Settings.Default["FirstRun"].ToString() == "True")
            {
                this.WindowState = FormWindowState.Normal;

                Properties.Settings.Default["FirstRun"] = false;
                Properties.Settings.Default.Save();
            }
        }

        //=============================================
        // Import Wallpaper
        private void btnChooseWall_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string wallDirStrTemp = Path.GetDirectoryName(openFileDialog1.FileNames[0]);

            string[] wallListTemp = new string[openFileDialog1.FileNames.Length];

            // check valid names
            List<int> failIndex = new List<int>();
            
            for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
            {
                wallListTemp[i] = Path.GetFileName(openFileDialog1.FileNames[i]);

                bool checkValid = false;
                if (wallListTemp[i].Contains(".") == true)
                {
                    string nameWall = wallListTemp[i].Split('.')[0];
                    if (nameWall.Length == 2)
                    {
                        Int16 nameInt;
                        if (Int16.TryParse(nameWall, out nameInt))
                        {
                            if ((nameInt >= 0) && (nameInt <= 23))
                                checkValid = true;
                        }
                                
                    }
                    else if (nameWall.Length == 3)
                    {
                        Int16 nameInt;
                        if (Int16.TryParse(nameWall, out nameInt))
                        {
                            if ((nameInt / 10 >= 0) && (nameInt / 10 <= 23) &&
                                (nameInt % 10 >= 2) && (nameInt % 10 <= 8))
                                checkValid = true;
                        }
                    }
                }

                if (checkValid == false)
                    failIndex.Add(i);
            }

            // valid path
            if (failIndex.Count == 0)
            {
                // Save config
                wallDirStr = wallDirStrTemp;

                string wallListStr = String.Empty;
                for (int i = 0; i < wallListTemp.Length; i++)
                {
                    if (i == 0) wallListStr += wallListTemp[i];
                    else wallListStr += "\n" + wallListTemp[i];
                }

                Properties.Settings.Default["WallDirStr"] = wallDirStr;
                Properties.Settings.Default["WallListStr"] = wallListStr;
                Properties.Settings.Default.Save();


                // process and save valid data
                processValidData(wallListTemp);

                // update wallpaper
                UpdateWall();
            }

            // not valid path
            else
            {
                string errorString = String.Empty;
                for (int i = 0; i < failIndex.Count; i++)
                {
                    errorString += Path.Combine(wallDirStrTemp, wallListTemp[failIndex[i]]);
                    errorString += "\n";
                }
                errorString += "does not match syntax.";

                MessageBox.Show(errorString, "Syntax Error");
            }
        }
        void processValidData(string[] wallListInput)
        {
            wallList = wallListInput;
            
            // Export file name to hour and dow
            hourList.Clear();
            dowList.Clear();
            maxHourIndex = 0;
            minHourIndex = 0;
            
            for (int i = 0; i < wallList.Length; i++)
            {
                string nameWall = wallList[i].Split('.')[0];
                hourList.Add((nameWall[0] - '0') * 10 + (nameWall[1] - '0'));

                if (nameWall.Length == 3) 
                    if (nameWall[2] == '8')
                        dowList.Add(0);
                    else
                        dowList.Add(nameWall[2] - '1');
                else dowList.Add(7); // no need dow

                if (i > 0)
                {
                    if (hourList[maxHourIndex] < hourList[i])
                        maxHourIndex = i;
                    if (hourList[minHourIndex] > hourList[i])
                        minHourIndex = i;
                }
            }

            // show result
            string tooltipFileStr = wallDirStr;
            for (int i = 0; i < wallList.Length; i++)
                tooltipFileStr += "\n" + wallList[i];
            toolTip1.SetToolTip(btnChooseWall, tooltipFileStr);
            
        }

        //=============================================
        // Timer Update
        private void timer1_Tick(object sender, EventArgs e)
        {
            hourNow = DateTime.Now.Hour;
            dowNow = DateTime.Now.DayOfWeek;

            if ((hourNow != lastHour) || ((int)dowNow != lastDow))
            {
                UpdateWall();
            }

            freeMemory();
        }
        private void UpdateWall()
        {
            if ((wallDirStr != String.Empty) && (wallList.Length > 0))
            {
                hourNow = DateTime.Now.Hour;
                dowNow = DateTime.Now.DayOfWeek;

                // check import wallList match hour and dow 
                int indexMatch = -1;
                for (int i = 0; i < hourList.Count; i++)
                {
                    if ((hourNow >= hourList[minHourIndex]) &&
                        (hourNow < hourList[maxHourIndex]))
                    {
                        if (hourNow >= hourList[i])
                        {
                            bool checkMatch = false;
                            if (indexMatch == -1)
                                checkMatch = true;
                            else if (hourList[indexMatch] < hourList[i])
                                checkMatch = true;
                            if (checkMatch)
                            {
                                if (dowList[i] == (int)dowNow) indexMatch = i;
                                else if (dowList[i] == 7) indexMatch = i;
                            }
                        }
                    }
                    else
                    {
                        indexMatch = maxHourIndex;
                    }
                }

                // new hour (and dow)
                if ((indexMatch != -1) && (indexMatch != lastIndexMatch))
                {
                    lastHour = hourNow;
                    lastDow = (int)dowNow;
                    lastIndexMatch = indexMatch;

                    Uri path = new Uri(Path.Combine(wallDirStr, wallList[indexMatch]));
                    Wallpaper.Set(path, Wallpaper.Style.Fill);
                }
            }
        }

        //=============================================
        // Run at start up
        private void cbxStartup_CheckedChanged(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (cbxStartup.Checked == true)
                key.SetValue(softwareName, Application.ExecutablePath);
            else
                key.DeleteValue(softwareName, false);
        }

        //=============================================
        // Free Memory (reduce RAM usage)
        private void freeMemory()
        {
            System.Diagnostics.Process.GetCurrentProcess().MinWorkingSet = System.Diagnostics.Process.GetCurrentProcess().MinWorkingSet;
        }

        //=============================================
        // Notify icon
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            System.Environment.Exit(1);            
        }
    }
}

public sealed class Wallpaper
{
    Wallpaper() { }

    const int SPI_SETDESKWALLPAPER = 20;
    const int SPIF_UPDATEINIFILE = 0x01;
    const int SPIF_SENDWININICHANGE = 0x02;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    public enum Style : int
    {
        Tile,
        Center,
        Stretch,
        Fill,
        Fit,
        Span
    }

    public static void Set(Uri uri, Style style)
    {
        System.IO.Stream s = new System.Net.WebClient().OpenRead(uri.ToString());

        System.Drawing.Image img = System.Drawing.Image.FromStream(s);
        string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
        img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);
        
        img.Dispose();
        s.Dispose();

        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
        if (style == Style.Fill)
        {
            key.SetValue(@"WallpaperStyle", 10.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
        }
        if (style == Style.Fit)
        {
            key.SetValue(@"WallpaperStyle", 6.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
        }
        if (style == Style.Span) // Windows 8 or newer only!
        {
            key.SetValue(@"WallpaperStyle", 22.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
        }
        if (style == Style.Stretch)
        {
            key.SetValue(@"WallpaperStyle", 2.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
        }
        if (style == Style.Tile)
        {
            key.SetValue(@"WallpaperStyle", 0.ToString());
            key.SetValue(@"TileWallpaper", 1.ToString());
        }
        if (style == Style.Center)
        {
            key.SetValue(@"WallpaperStyle", 0.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
        }

        SystemParametersInfo(SPI_SETDESKWALLPAPER,
            0,
            tempPath,
            SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
    }

}
