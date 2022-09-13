using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bins_PcQuickStart
{
    class ApplicationManager
    {
        private static string BUSINESS_TYPE = "run_all_app";

        // 启动Windows上指定程序
        public static string runExe(string path)
        {
            string fileName = StringUtils.getLastText(path);
            string prePath = StringUtils.geBeforetLastText(path, fileName);

            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //设置外部程序名  
            Info.FileName = fileName;
            //设置外部程序工作目录为 
            Info.WorkingDirectory = @prePath;
            //最小化方式启动
            Info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            //声明一个程序类  
            System.Diagnostics.Process Proc;
            try
            {
                Proc = System.Diagnostics.Process.Start(Info);
                System.Threading.Thread.Sleep(500);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                Console.WriteLine("=================================Run Application Error=================================");
                return "Error:" + path;
            }
            return "Run Success,Application path:" + path;
        }

        /**
         * 该方法目前没有用到
         */
        public static bool setExeRunStatus(int isRunning)
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                business_control businessControl = bins_Pc_Db.business_control
                    .Where(ele => ele.business_type == BUSINESS_TYPE)
                    .FirstOrDefault();
                if (businessControl != null)
                {
                    businessControl.is_running = isRunning;
                    bins_Pc_Db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        /**
         * 获取 businessType 类型的软件的启动信息
         */
        public static business_control getBusinessControl(string businessType)
        {
            if (businessType == null)
            {
                businessType = BUSINESS_TYPE;
            }
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                business_control businessControl = bins_Pc_Db.business_control
                    .Where(ele => ele.business_type == BUSINESS_TYPE)
                    .FirstOrDefault();
                if (businessControl != null)
                {
                    return businessControl;
                }
                return null;
            }
        }

        public static string addBusinessControl(int isRunning, string logsInfo)
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                business_control businessControl = new business_control()
                {
                    //id = System.DateTime.Now.Millisecond,
                    business_type = BUSINESS_TYPE,
                    is_running = isRunning,
                    create_time = System.DateTime.Now,
                    update_time = System.DateTime.Now,
                    logs = logsInfo
                };
                try
                {
                    bins_Pc_Db.business_control.Add(businessControl);
                    bins_Pc_Db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("===Exception:" + ex);
                    return "操作失败:" + ex;
                }
                return "成功";
            }
        }

        public static string delBusinessControl()
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                business_control businessControl = bins_Pc_Db.business_control.Where(ele => ele.business_type == BUSINESS_TYPE).FirstOrDefault();
                if (businessControl != null)
                {
                    bins_Pc_Db.business_control.Remove(businessControl);
                    bins_Pc_Db.SaveChanges();
                    return "删除成功";
                }
                return "删除失败，查询为空";
            }
        }

    }
}
