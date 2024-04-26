using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading
{
    public partial class BicolorSphere : Form
    {
        RandomHelper RHelper = new RandomHelper();
        public BicolorSphere()
        {
            InitializeComponent();
        }
        private string[] RedNums =
            {
                "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36"
        };
        private string[] BlueNums =
            {
                "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16"
        };
        private static readonly object UpdateOnly = new object();
        private void UpdateLab(Label lab, string Value)
        {
            if (lab.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    lab.Text = Value;
                }));
            }
            else
            {
                lab.Text = Value;
            }
        }
        private void MsgLabText()
        {
            MessageBox.Show($"开奖号码是：{this.RedLab1.Text}  {this.RedLab2.Text}  {this.RedLab3.Text}  {this.RedLab4.Text}  {this.RedLab5.Text}  {this.RedLab6.Text}  {this.BlueLab.Text}");
        }
        private bool IsStop;
        private List<Task> tasks = new List<Task>();
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            IsStop = true;
            tasks.Clear();
            this.RedLab1.Text = "00";
            this.RedLab2.Text = "00";
            this.RedLab3.Text = "00";
            this.RedLab4.Text = "00";
            this.RedLab5.Text = "00";
            this.RedLab6.Text = "00";
            this.BlueLab.Text = "00";
            foreach (var item in groupBox1.Controls)
            {
                if (item is Label)
                {
                    Label label = (Label)item;
                    if (label.Name.Contains("Blue"))
                    {
                        tasks.Add(Task.Run(() =>
                        {
                            while (IsStop)
                            {
                                int Index = RHelper.GetNumLong(0, this.BlueNums.Length);
                                string Num = BlueNums[Index];
                                lock (UpdateOnly)
                                {
                                    UpdateLab(label, Num);
                                }
                            }
                        }));
                    }
                    else//其他都是红色球
                    {
                        tasks.Add(Task.Run(() =>
                        {
                            while (IsStop)
                            {
                                int Index = RHelper.GetNumLong(0, this.RedNums.Length);
                                string Num = RedNums[Index];
                                lock (UpdateOnly)
                                {
                                    if (OnlyOneNum(Num))
                                    {
                                        continue;
                                    }
                                    UpdateLab(label, Num);
                                }
                            }
                        }));
                    }
                }
            }
            Task.Run(() =>
            {
                while (true)
                {
                    if (!OnlyOneNum("00") && this.BlueLab.Text != "00")
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.btnStop.Enabled = true;
                        }));
                        break;
                    }
                }
            });
            Task.WhenAll(tasks.ToArray()).ContinueWith(t =>
            {
                MsgLabText();
            });
        }
        private bool OnlyOneNum(string Value)
        {
            foreach (var item in groupBox1.Controls)
            {
                if (item is Label)
                {
                    Label Lab = (Label)item;
                    if (Lab.Name.Contains("Red"))
                    {
                        if (Lab.Text.Contains(Value))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            IsStop = false;
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }
    }
}
