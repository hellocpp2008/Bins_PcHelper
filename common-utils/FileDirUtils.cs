using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bins_PcQuickStart.common_utils
{
    class FileDirUtils
    {
        public static string OpenFolderDialog()
        {
            string path = "c:\\";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            //打开的文件夹浏览对话框上的描述  
            dialog.Description = "请选择一个文件夹";
            //是否显示对话框左下角 新建文件夹 按钮，默认为 true  
            dialog.ShowNewFolderButton = false;
            //首次defaultPath为空，按FolderBrowserDialog默认设置（即桌面）选择  
            if (path != "")
            {
                //设置此次默认目录 
                dialog.SelectedPath = path;
            }
            //按下确定选择的按钮  
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //记录选中的目录  
                path = dialog.SelectedPath;
            }
            return path;
        }

        public static long ByteToM(long size)
        {
            return size / 1024 / 1024;
        }

    }

}
