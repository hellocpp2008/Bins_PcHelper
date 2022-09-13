using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bins_PcQuickStart
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;// 显示在屏幕的正中心
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result1 = CmdUtils.RunCMDCommand("python C:\\1_projects\\my_python_repo\\python_pycharm\\i\\test1.py");
            string result2 = CmdUtils.RunCMDCommand("python C:\\1_projects\\my_python_repo\\python_pycharm\\iii_File\\create_dir_if_not_there.py");
            Console.WriteLine("===========result1:" + result1 + ",===========result2" + result2);
            textBox1.Text = result2;
        }
    }
}
