using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Linq;

namespace c_sharp_ais_manager_gui
{

    public partial class Form1 : Form
    {
        Process process = new Process();

        int row = 0;

        bool process_started = false;

        bool send_commands = false;
       
        public Form1()
        {
            InitializeComponent();

        }

        private void bStart_Click(object sender, EventArgs e)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,null,dataGridView1,new object[] { true });
            // Invokes faster printing of large data, such as printing out Logs for example.
;
            if (process_started == false) // check if we don't have previously running process, or check in task manager/or run -> resmon
            {
                send_commands = false;    // flag for sending 'keep alive' commands, i.e '0's that keep shells main loop going;

                var thread = new Thread(new ThreadStart(() =>
               {
                   process.StartInfo.FileName = "CMD";
                   process.StartInfo.WorkingDirectory = "../../shell/bin/Release"; // directory of program we'll be executing
                   process.StartInfo.UseShellExecute = false; // necessary for redirection to be possible
                   process.StartInfo.CreateNoWindow = true;     // so console window does not appear
                   process.StartInfo.RedirectStandardInput = true; // input stream redirection set to true
                   process.StartInfo.RedirectStandardError = true; // error stream redirection set to true
                   process.StartInfo.RedirectStandardOutput = true; // output stream redirection set to true
                   process.StartInfo.Arguments = "/C C-sharp-ais-manager.exe -gui"; // shells arguments to CMD console that we'll be executing
                   process.EnableRaisingEvents = false;

                   process = Process.Start(process.StartInfo);

                   process.StandardInput.AutoFlush = true;

                   process_started = true; // flag indicating that we started our process

                   StreamReader sr = process.StandardOutput; // binding output stream of consoles output stream

                   row = 0; // used as grid row counter for appending RTEs

                   while (!process.HasExited)
                   {
                       string procOutput = sr.ReadLine();

                       // trying to do the control update directly will result in an invalid cross-thread operation exception
                       // instead, we invoke the control update on the window thread using Invoke(...) 

                       if (procOutput == "0" || procOutput == "alive")
                       {
                           Invoke(new Action<string>(s => { ShellStatus.Text = "Shell is running."; }), "");

                       } else if (procOutput == "RTE SENT") // line that indicates that device found RTE in shell app
                       {
                           string[] RTE = new string[10];

                           int j = 0;

                           for (; ;)
                           {
                               procOutput = sr.ReadLine();

                               RTE[j] = procOutput.Substring(procOutput.IndexOf(":") + 1);

                               j++;

                               if (procOutput == "RTE READ")
                                   break;
                           }
                       
                           
                            Invoke(new Action<string>(s => { dataGridView1.Rows.Add(row, RTE[0], RTE[1], RTE[2], RTE[3], RTE[4], RTE[5], RTE[6], RTE[7], RTE[8], RTE[9]); }), ""); // appending our RTE to grid row

                            Invoke(new Action<string>(s => { dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1; }), ""); // setting scroll of grid to it's last appended row
                           
                            Invoke(new Action<string>(s => { cmdProgressBar.Value = 0; }), "");

                           Application.DoEvents(); // process all messages in message queue

                           j = 0;

                           row++;

                       } else if (procOutput != null && procOutput.Contains("Downloading")) // parsing spam "Downloading ... # percent line from console
                       {
                            Invoke(new Action<string>( s => { cmdProgressBar.Increment(1); }), "");

                           procOutput = sr.ReadLine();

                       } else if (procOutput == "Press ESC to stop")
                       {
                           if (send_commands == true)
                           {
                               process.StandardInput.WriteLine("0"); // commmand for shell to keep it's loop going

                               Invoke(new Action<string>(s => { tKeepAlive.Start(); }), procOutput);

                               Thread.Sleep(500); // just wait for 500ms and send to loop again, avoiding command spam

                           } 
                       }
                       else
                       {
                            Invoke(new Action<string>(s => { tInfo.Text += s; }), procOutput); // received output to text box

                            Invoke(new Action<string>(s => { tInfo.Text += "\n"; }), "");      // appending new line to text box

                            Invoke(new Action<string>(s => { tInfo.SelectionStart = tInfo.Text.Length; }), ""); // set cursor to end of text box

                            Invoke(new Action<string>(s => { tInfo.ScrollToCaret(); }), ""); // scroll to end automatically automatically 

                            Invoke(new Action<string>(s => { cmdProgressBar.Value = 0; }), "");

                       }
                   }
               }));
                
                thread.IsBackground = true; // so the shell process closes when form/application gets closed;

                thread.Start();

                send_commands = true;               
            }
            else
            {
                MessageBox.Show("Shell process is already running in background!");
            }           
        }

        private void tKeepAlive_Tick(object sender, EventArgs e) // timer for sending '0's to shells Main loop after starting thread
        {
            if (send_commands == true && process.StandardInput != null)
            {
                process.StandardInput.WriteLine("0");
            } else
            {
                send_commands = false; 

                Invoke(new Action<string>(s => { ShellStatus.Text = "Shell is not running."; }), "");
            }
                
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("C-sharp-ais-manager"))
            {
                process.Kill();

                ShellStatus.Text = "Shell is not running";

                process_started = false;

                send_commands = false;

                Invoke(new Action<string>(s => { tInfo.AppendText("\n Process exited at: " + DateTime.Now.ToString());}), ""); // Timestamp when our process has exited

            }
                    process_started = false;

                    send_commands = false;
            
        }

        private void bReadWhitelistFromDB_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("1");
        }

        private void bReadBlacklistFromDB_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("2");
        }

        private void bGetLogs_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("3");
        }

        private void bReadWhitelistFromReader_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("4");
        }

        private void bReadBlacklistFromReaders_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("5");
        }

        private void bSendAllLogs_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("6");
        }
        
        private void bSetTimeToDevices_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("7");
        }

        private void bGetTimeFromDevices_Click(object sender, EventArgs eg)
        {
            process.StandardInput.WriteLine("8");
        }

        private void bGetLogsByIndex_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("I");

            string start_index = tStartIndex.Text;

            string end_index = tEndIndex.Text;

            process.StandardInput.WriteLine(start_index);

            process.StandardInput.WriteLine(end_index);

        }

        private void bGetLogsByTime_Click(object sender, EventArgs e)
        {
            process.StandardInput.WriteLine("T");

            string start_time = tStartTime.Text;

            string end_time = tEndTime.Text;

            process.StandardInput.WriteLine(start_time);

            process.StandardInput.WriteLine(end_time);

        }

        private void bClear_Click(object sender, EventArgs e)
        {

            row = 0;

            dataGridView1.Rows.Clear();

            dataGridView1.Refresh();
            
            tInfo.Clear();

            tStartIndex.Clear();

            tEndIndex.Clear();

            tStartTime.Text = "hh:mm:ss dd.mm.yyyy";

            tEndTime.Text = "hh:mm:ss dd.mm.yyyy";

            cmdProgressBar.Value = 0;

            dataGridView1.Rows.Clear();

        }        
    }
}

