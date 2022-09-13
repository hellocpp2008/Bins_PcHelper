using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bins_PcQuickStart
{
    class CmdUtils
    {
        /// ████████████████ 好使。不用打开CMD窗口也可运行CMD命令。 ████████████████
        /// <summary>
        /// 执行CMD命令
        /// </summary>
        /// <param name="cmd">要执行的命令</param>
        /// <returns></returns>
        public static string RunCMDCommand(string cmd)
        {
            string cmdPath = "C:\\Windows\\System32\\cmd.exe";   //cmd.exe执行文件目录
            cmd = cmd.Trim().TrimEnd('&') + "&exit";  //不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态

            string result = string.Empty;
            Process process = new Process();
            try
            {
                //设置要启动的执行程序
                process.StartInfo.FileName = cmdPath;

                //是否使用操作系统shell启动进程
                process.StartInfo.UseShellExecute = false;
                //应用程序的输入是否从Process.StandardInput流中读取/是否接受来自调用程序的输入信息
                process.StartInfo.RedirectStandardInput = true;

                //是否将应用程序的输出写入Process.StandardOutput流中/是否调用程序获取输出信息
                //置为false时StandardOutput.ReadToEnd获取异常
                process.StartInfo.RedirectStandardOutput = true;

                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                //向cmd窗口写入命令
                process.StandardInput.WriteLine(cmd);
                process.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();//等待程序执行完退出进程
                process.Close();
            }
            catch (Exception ex)
            {
                //记录错误日志信息
                //log4net
                result = string.Empty;
                Console.WriteLine("======Exception======" + ex);
            }
            finally
            {
                //释放
                process.Dispose();
            }
            return result;
        }

        /// ████████████████ 能打开CMD窗口，但是没有执行命令.... ████████████████
        /// <summary>
        /// 运行cmd命令
        /// 会显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        public static bool RunCmd(string cmdStr)
        {
            bool result = false;
            string cmdExe = "C:\\Windows\\System32\\cmd.exe";   //cmd.exe执行文件目录
            try
            {
                using (Process myPro = new Process())
                {
                    //指定启动进程是调用的应用程序和命令行参数
                    ProcessStartInfo psi = new ProcessStartInfo(cmdExe, cmdStr);
                    myPro.StartInfo = psi;
                    myPro.Start();
                    myPro.WaitForExit();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("======Exception======" + ex);
            }
            return result;
        }

        /// ████████████████ 没有测试 ████████████████
        /// <summary>
        /// 运行cmd命令
        /// 不显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        static bool RunCmd2(string cmdExe, string cmdStr)
        {
            bool result = false;
            try
            {
                using (Process myPro = new Process())
                {
                    myPro.StartInfo.FileName = "cmd.exe";
                    myPro.StartInfo.UseShellExecute = false;
                    myPro.StartInfo.RedirectStandardInput = true;
                    myPro.StartInfo.RedirectStandardOutput = true;
                    myPro.StartInfo.RedirectStandardError = true;
                    myPro.StartInfo.CreateNoWindow = true;
                    myPro.Start();
                    //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                    string str = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");

                    myPro.StandardInput.WriteLine(str);
                    myPro.StandardInput.AutoFlush = true;
                    myPro.WaitForExit();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("======Exception======" + ex);
            }
            return result;
        }

    }
}
