using Bins_PcQuickStart.common_utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * 主要是备份的时候使用，比如将电脑上的一个文件夹内容，复制到移动硬盘上，移动硬盘上已经有这个文件夹，然后本程序比较电脑上的同名文件和移动硬盘上的同名文件的最后修改日期，
 * 如果电脑上的更新，则就用电脑上的文件覆盖移动硬盘上的。
 * 
 * ██████████████████████████████ TODO：从电脑往移动硬盘上备份，记录备份的进度，如果中断了可以继续？？██████████████████████████████
 * 
 */
namespace Bins_PcQuickStart
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;// 显示在屏幕的正中心
        }

        private FileInfo[] sourceFileInfos = null;
        private FileInfo[] targetFileInfos = null;
        private string targetDir = "";

        // ***** 测试打开FolderBrowserDialog *****
        private void TestOpenDirBtn_Click(object sender, EventArgs e)
        {
            string defaultPath = FileDirUtils.OpenFolderDialog();
            MessageBox.Show(defaultPath);
        }

        // == 打开源目录 ==
        private void button1Source_Click(object sender, EventArgs e)
        {
            sourceTextBox1.Text = "";
            string sourcePath = FileDirUtils.OpenFolderDialog();
            if (sourcePath == null || sourcePath == "")
            {
                MessageBox.Show("源目录为空，程序结束");
                throw new Exception("源目录为空，程序结束");
            }
            DirectoryInfo sourceRoot = new DirectoryInfo(sourcePath);
            // 1. 获取 源目录下所有 文件（已包含了子目录中的文件）
            FileInfo[] files = sourceRoot.GetFiles();
            if (files == null || files.Count() == 0)
            {
                MessageBox.Show("源目录文件为空，程序结束");
                throw new Exception("源目录文件为空");
            }
            else
            {
                // 处理 源目录下所有 文件
                foreach (FileInfo fileInfo in files)
                {
                    string fullName = fileInfo.Name;
                    DateTime lastWriteTime = fileInfo.LastWriteTime;
                    string s = fullName + "\n\r" + "█最后修改时间" + lastWriteTime + "\n\r";
                    Console.WriteLine("====:fullName:" + fullName + ",lastWriteTime:" + lastWriteTime);
                    sourceTextBox1.AppendText(s + "\n\r");
                }
                sourceFileInfos = files;
            }
            // ██ 2. 获取源目录下所有 子目录（TODO 子目录的文件还需要处理）██
            DirectoryInfo[] dics = sourceRoot.GetDirectories();
            if (dics == null || dics.Count() == 0)
            {
                // MessageBox.Show("源目录的子目录为空");
                // throw new Exception("源目录的子目录为空");
            }
            else
            {
                // 处理 源目录下所有 子目录
            }
        }

        // == 打开目标目录 ==
        private void button2Target_Click(object sender, EventArgs e)
        {
            targetTextBox1.Text = "";
            string targetPath = FileDirUtils.OpenFolderDialog();
            if (targetPath == null || targetPath == "")
            {
                MessageBox.Show("目标目录为空，程序结束");
                throw new Exception("目标目录为空");
            }
            DirectoryInfo targetRoot = new DirectoryInfo(targetPath);
            FileInfo[] files = targetRoot.GetFiles();
            if (files == null || files.Count() == 0)
            {
                MessageBox.Show("目标目录文件为空，程序结束");
                throw new Exception("目标目录文件为空");
            }
            else
            {
                // 处理 源目录下所有 文件
                foreach (FileInfo fileInfo in files)
                {
                    string fullName = fileInfo.Name;
                    DateTime lastWriteTime = fileInfo.LastWriteTime;
                    string s = fullName + "\n\r" + "█最后修改时间" + lastWriteTime + "\n\r";
                    Console.WriteLine("====:fullName:" + fullName + ",lastWriteTime:" + lastWriteTime);
                    targetTextBox1.AppendText(s + "\n\r");
                }
                targetFileInfos = files;
                targetDir = targetPath;
            }
        }

        // 根据更新时间，进行文件替换
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (sourceFileInfos == null || sourceFileInfos.Count() == 0)
            {
                MessageBox.Show("源目录为空，程序结束");
                throw new Exception("源目录为空，程序结束");
            }
            if (targetFileInfos == null || targetFileInfos.Count() == 0)
            {
                MessageBox.Show("目标目录文件为空，程序结束");
                throw new Exception("目标目录文件为空");
            }
            // key是文件名
            Dictionary<string, FileInfo> mapTarget = new Dictionary<string, FileInfo>();
            foreach (FileInfo info in targetFileInfos)
            {
                mapTarget.Add(info.Name, info);
            }

            foreach (FileInfo sourceInfo in sourceFileInfos)
            {
                string fileName = sourceInfo.Name;
                DateTime updateTime = sourceInfo.LastWriteTime;
                long size = FileDirUtils.ByteToM(sourceInfo.Length);
                // FileInfo targetFileInfo = mapTarget[fileName];// 直接这样获取（像Java一样），如果mapTarget中不存在这个key就会报错
                if (mapTarget.ContainsKey(fileName))
                {
                    FileInfo targetFileInfo = mapTarget[fileName];
                    if (updateTime.CompareTo(targetFileInfo.LastWriteTime) > 0)
                    {
                        Console.WriteLine("=====================源文件修改时间大于目标文件修改时间:" + fileName);
                        // 1. 修改目标文件的文件名（加后缀“备份”）
                        string backupName = targetFileInfo.FullName + "备份";
                        File.Move(targetFileInfo.FullName, backupName);
                        // 2. 复制源文件到目标文件夹里面
                        sourceInfo.CopyTo(targetFileInfo.FullName);
                        // 3. 删除备份的文件
                        File.Delete(backupName);
                        string s = fileName
                            + "\n\r\n\r" + "源文件的修改时间:" + updateTime.ToString() + "█大小:" + size + "M" + "█字节:" + sourceInfo.Length + "\n\r\n\r" +
                            "目标文件修改时间:" + targetFileInfo.LastWriteTime.ToString() + "█大小:" + FileDirUtils.ByteToM(targetFileInfo.Length) + "M" + "█字节:" + targetFileInfo.Length + "\n\r\n\r";
                        resultTextBox1.AppendText(s + "\n\r\n\r");
                    }
                }
            }
            resultTextBox1.AppendText("=========== 结束 ===========\n\r\n\r");
        }
    }
}
