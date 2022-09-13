using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace Bins_PcQuickStart
{
    public partial class MainForm : Form
    {
        private string appConfig = null;

        private int NULL = -9999;

        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;// 显示在屏幕的正中心
        }

        /**
         *
         */
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 此处注掉，从数据库中读取（当如果不用数据库，下面从本机读取json配置也是可行的）
            // load0(false);
            // load1();

            // 设置表头
            setUpListView1_title();
            // 从数据库中读取
            load_listView1_FromDb(NULL);

            // 加载数据到 DataGridView 中
            loadData2DataGridView();
            // 设置 DataGridView 的样式
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["ID"].Width = 20;
            dataGridView1.Columns["del_Column"].Width = 50;
            dataGridView1.Columns["update_Column"].Width = 50;
            dataGridView1.Columns["PATH"].Width = 100;
            dataGridView1.Columns["TITLE"].Width = 80;
            dataGridView1.Columns["PC_NAME"].Width = 80;

            //dataGridView1.Columns["path"].ReadOnly = false;

            business_control businessControl = ApplicationManager.getBusinessControl(null);
            if (businessControl != null && businessControl.is_running == 1)
            {
                if (textBox2.Text == null || textBox2.Text == "")
                {
                    textBox2.Text = businessControl.logs;
                    label1.Text = "Running...";
                }
            }
            else
            {
                label1.Text = "Stopped...";
            }

        }

        // 设置表头
        private void setUpListView1_title()
        {
            this.listView1.Columns.Add("软件位置", 200, HorizontalAlignment.Left);
            this.listView1.Columns.Add("软件名称", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("创建时间", 120, HorizontalAlignment.Left);
            this.listView1.Columns.Add("更新时间", 120, HorizontalAlignment.Left);
        }

        // *****检查并创建配置文件*****【=====走数据库就不需要走这个方法了=====】
        private void load0(bool isDefault)
        {
            // 创建目录保存配置文件
            string myDoc = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Console.WriteLine("我的文档目录：" + myDoc);
            string appPath = myDoc + "\\bins";
            string filePath = appPath + "\\application.json";
            // 目录不存在
            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(@appPath);
            }
            if (!File.Exists(filePath) || isDefault)
            {
                FileStream fs = new FileStream(@filePath, FileMode.Create);
                StreamWriter wr = null;
                wr = new StreamWriter(fs);
                string json = "{'qucikPath':['myDoc'],'k':{'a':1}}";
                wr.WriteLine(json);
                wr.Close(); //关闭
                fs.Close();
            }
            appConfig = filePath;
        }

        // *****加载 ListView 数据*****（读取的“我的文档”下面“bins"目录下的json数据）【=====走数据库就不需要走这个方法了=====】
        private void load1()
        {
            this.listView1.View = View.Details;// 设置 ListView 的视图布局
            this.listView1.GridLines = true;// 设置行和列之间是否显示网格线。（默认为false）提示：只有在Details视图该属性才有意义。
            this.listView1.Scrollable = true;

            List<string> list = loadJson(null);
            // 设置表头
            this.listView1.Columns.Add("软件位置", 200, HorizontalAlignment.Left);
            this.listView1.Columns.Add("备注", 100, HorizontalAlignment.Left);
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    // 第0个是测试数据，忽略
                    continue;
                }
                ListViewItem lvi = new ListViewItem();
                lvi.Text = list[i];
                lvi.SubItems.Add("11");
                lvi.SubItems.Add("22");
                this.listView1.Items.Add(lvi);
            }
        }

        // *****【=====走数据库就不需要走这个方法了=====】*****
        private List<string> loadJson(JObject jo)
        {
            string josnString = File.ReadAllText(appConfig, Encoding.Default);
            if (jo == null)
            {
                jo = JObject.Parse(josnString);
            }
            string all = jo.ToString();
            Console.WriteLine("==========all" + all);
            string qucikVal = jo["qucikPath"].ToString();
            Console.WriteLine("==========qucikVal" + qucikVal);
            string[] strArray = qucikVal.Split(',');
            JArray jArray = JArray.Parse(qucikVal);
            Console.WriteLine("==========jArray:" + jArray);
            List<string> list = new List<string>();
            foreach (string item in jArray)
            {
                string sItem = (string)item;
                list.Add(sItem);
            }
            return list;
        }

        private void loadData2DataGridView()
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                List<quick_app_info> quickAppInfoList = bins_Pc_Db.quick_app_info
                    .Where(ele => ele.is_test == 0)
                    .Where(ele => ele.is_del == 0)
                    .ToList();
                List<QuickAppInfoView> quickAppInfoViews = new List<QuickAppInfoView>();
                foreach (quick_app_info info in quickAppInfoList)
                {
                    QuickAppInfoView view = new QuickAppInfoView();
                    view.ID = info.id;
                    view.PATH = info.path;
                    view.TITLE = info.title;
                    view.PC_NAME = info.pc_name;
                    view.CREATE_TIME = info.create_time;
                    view.UPDATE_TIME = info.update_time;
                    quickAppInfoViews.Add(view);
                }
                dataGridView1.DataSource = quickAppInfoViews;
            }
        }

        // 加载 ListView 数据（读取数据库中的数据）
        private void load_listView1_FromDb(int is_test)
        {
            List<int> list = new List<int>();
            if (is_test == NULL)
            {
                list.Add(1);
                list.Add(0);
            }
            else
            {
                list.Add(is_test);
            }
            this.listView1.View = View.Details;// 设置 ListView 的视图布局
            this.listView1.GridLines = true;// 设置行和列之间是否显示网格线。（默认为false）提示：只有在Details视图该属性才有意义。
            this.listView1.Scrollable = true;
            // 设置表头【表头不能在这里设置，不然每次调用该方法都会增加表头】
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                List<quick_app_info> quickAppInfoList = bins_Pc_Db.quick_app_info
                    .Where(ele => list.Contains(ele.is_test))
                    .ToList();
                if (quickAppInfoList != null && quickAppInfoList.Count > 0)
                {
                    Console.WriteLine("查询结果:" + Newtonsoft.Json.JsonConvert.SerializeObject(quickAppInfoList));
                    foreach (quick_app_info info in quickAppInfoList)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = info.path;
                        lvi.SubItems.Add(info.title);
                        lvi.SubItems.Add(info.create_time.ToString());
                        lvi.SubItems.Add(info.update_time.ToString());
                        this.listView1.Items.Add(lvi);
                    }
                }
            }
        }

        // 重新从数据库中读取数据，放到 listView1
        private void realod_listView1(int is_test)
        {
            this.listView1.Clear();
            // 上面 Clear() 之后表头也被清除了，所以需要重新添加表头
            setUpListView1_title();
            load_listView1_FromDb(is_test);
        }

        // 重新从数据库中读取数据，放到 dataGridView1
        private void realod_dataGridView1()
        {
            loadData2DataGridView();
        }

        // 按钮“一键启动所有软件”
        private void button1_Click(object sender, EventArgs e)
        {
            business_control businessControl = ApplicationManager.getBusinessControl(null);
            if (businessControl != null && businessControl.is_running == 1)
            {
                MessageBox.Show("启动失败，程序已经启动！");
                if (textBox2.Text == null || textBox2.Text == "")
                {
                    textBox2.Text = businessControl.logs;
                }
                label1.Text = "Running...";
                return;
            }
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                List<quick_app_info> quickAppInfoList = bins_Pc_Db.quick_app_info
                    .Where(ele => ele.is_test == 0)
                    .Where(ele => ele.is_del == 0)
                    .ToList();
                foreach (quick_app_info info in quickAppInfoList)
                {
                    string result = ApplicationManager.runExe(info.path);
                    this.textBox2.AppendText(result + "\r\n");
                }
                ApplicationManager.addBusinessControl(1, textBox2.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void test1()
        {
            string fName = "c:\\";
            StreamReader file = File.OpenText(appConfig);
            Console.WriteLine("=====================方法一====================");
            JsonTextReader reader = new JsonTextReader(file);
            JObject jsonObj = (JObject)JToken.ReadFrom(reader);
            string value = jsonObj.ToString();
            Console.WriteLine("button5_Click.value:" + value);
            string paths = jsonObj["qucikPath"].ToString();
            string[] strArray = paths.Split(',');
            JArray jArray = JArray.Parse(paths);
            Console.WriteLine("button5_Click.jArray:" + jArray);
            List<string> list = new List<string>();
            foreach (string item in jArray)
            {
                string s = (string)item;
                list.Add(s);
            }
            list.Add(fName);
            // 不适用上面的 list 了，直接更新吧
            paths += paths + "," + fName;
            jsonObj["qucikPath"] = paths;
            // 怎么把这个值更新到json中呢？？？？？

            file.Close();
        }

        private void test2()
        {
            List<string> list = loadJson(null);
            int count = list.Count;
            string s = list[count - 1];
            Console.WriteLine("==========s" + s);
            string sFile = File.ReadAllText(s, Encoding.Default);
            Console.WriteLine("==========sFile" + sFile);
        }

        // *****添加快捷启动路径（走JSON）*****【=====走数据库就不需要走这个方法了=====】
        private void test_button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\"; // 对话框的初始目录
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog1.FileName;
                Console.WriteLine("button5_Click.fName:" + fName);
                string josnString = File.ReadAllText(appConfig, Encoding.Default);
                JObject jo = JObject.Parse(josnString);
                List<string> list = loadJson(jo);
                list.Add(fName);
                string str = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                jo["qucikPath"] = str;
                string newJson = jo.ToString();
                File.WriteAllText(appConfig, newJson);
            }
            listView1.Clear();// 清除之后，再调用load1
            load1();
        }

        // 添加快捷启动路径（走数据库）
        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\"; // 对话框的初始目录
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog1.FileName;
                Console.WriteLine("button5_Click.fName:" + fName);
                using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
                {
                    quick_app_info quickAppInfo = new quick_app_info()
                    {
                        path = fName,
                        title = StringUtils.getLastText(fName),
                        pc_name = "binsPc",
                        is_test = 0,
                        create_time = System.DateTime.Now,
                        update_time = System.DateTime.Now
                    };
                    bins_Pc_Db.quick_app_info.Add(quickAppInfo);
                    bins_Pc_Db.SaveChanges();
                }
                realod_listView1(0);
                realod_dataGridView1();
            }
        }

        // 恢复初始配置
        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            setUpListView1_title();
            // load0(true);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // *****增【测试】*****
        private void button2_Click_1(object sender, EventArgs e)
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                quick_app_info quickAppInfo = new quick_app_info()
                {
                    path = "abc/efg" + Guid.NewGuid(),
                    title = "测试标题",
                    pc_name = "binsPc",
                    is_test = 1,
                    create_time = System.DateTime.Now,
                    update_time = System.DateTime.Now
                };
                bins_Pc_Db.quick_app_info.Add(quickAppInfo);
                bins_Pc_Db.SaveChanges();
            }
            realod_listView1(NULL);
        }

        // *****删【测试】*****
        private void button3_Click_1(object sender, EventArgs e)
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                quick_app_info quickAppInfo = bins_Pc_Db.quick_app_info.Where(ele => ele.id == 2).FirstOrDefault();//用FirstOrDefault(),用First()如果查询结果null会报错
                if (quickAppInfo != null)
                {
                    bins_Pc_Db.quick_app_info.Remove(quickAppInfo);
                    bins_Pc_Db.SaveChanges();
                    MessageBox.Show("删除id=2的数据成功");
                }
                else
                {
                    MessageBox.Show("删除失败，id=2的数据查询为空");
                }
            }
        }

        // *****改【测试】*****
        private void button6_Click(object sender, EventArgs e)
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                quick_app_info quickAppInfo = bins_Pc_Db.quick_app_info.Where(ele => ele.id == 1).FirstOrDefault();
                if (quickAppInfo != null)
                {
                    Console.WriteLine("修改前:" + Newtonsoft.Json.JsonConvert.SerializeObject(quickAppInfo));
                    quickAppInfo.path = new Random().Next(1, 1000).ToString();
                    quickAppInfo.update_time = System.DateTime.Now;
                    bins_Pc_Db.SaveChanges();
                    MessageBox.Show("修改id=1的数据成功");
                }
                else
                {
                    MessageBox.Show("修改失败，id=1的数据查询为空");
                }
            }
        }

        // *****查【测试】*****
        private void button7_Click(object sender, EventArgs e)
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                List<quick_app_info> quickAppInfoList = bins_Pc_Db.quick_app_info
                    .Where(ele => ele.id >= 1 && ele.id < 10)
                    .Where(ele => ele.pc_name == "binsPc")
                    .ToList();
                if (quickAppInfoList != null && quickAppInfoList.Count > 0)
                {
                    Console.WriteLine("查询结果:" + Newtonsoft.Json.JsonConvert.SerializeObject(quickAppInfoList));
                    MessageBox.Show("查询成功");
                }
                else
                {
                    MessageBox.Show("查询失败");
                }
            }
        }

        // 刷新可用的数据
        private void button8_Click(object sender, EventArgs e)
        {
            realod_listView1(0);
        }

        // 加载全部的数据
        private void button9_Click(object sender, EventArgs e)
        {
            realod_listView1(NULL);
        }

        // **********测试分隔文件路径 **********
        private void button10_Click(object sender, EventArgs e)
        {
            string s1 = StringUtils.getLastText("C:\\_skpclog");
            MessageBox.Show(s1);
            string s2 = StringUtils.geBeforetLastText("C:\\_skpclog", s1);
            MessageBox.Show(s2);
        }

        //********** 测试启动一个程序 **********
        private void button11_Click(object sender, EventArgs e)
        {
            string path = "C:\\3_dev_files\\tomcat-9\\bin\\startup";
            string result = ApplicationManager.runExe(path);
            this.textBox2.AppendText(result + "\r\n");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // 处理 dataGridView1 的点击事件
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            Console.WriteLine("ColumnIndex是：" + columnIndex + ", rowIndex是：" + rowIndex);
            if (this.dataGridView1.Columns[columnIndex] is DataGridViewButtonColumn && rowIndex > -1)
            {
                if (this.dataGridView1.CurrentCell.FormattedValue.ToString() == "删除")
                {
                    Console.WriteLine("点击了删除按钮");
                    int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["ID"].Value);
                    string title = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["TITLE"].Value);
                    Console.WriteLine("要删除的行的id是：" + id);
                    string sMsg = String.Format("确定要删除吗？");
                    if (MessageBox.Show(sMsg, "确认删除该行数据:" + title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        delById(id);
                        realod_dataGridView1();
                    }
                }
            }
        }

        // 按 id 删除（逻辑删除）
        private void delById(int id)
        {
            using (Bins_Pc_HelperEntities bins_Pc_Db = new Bins_Pc_HelperEntities())
            {
                quick_app_info quickAppInfo = bins_Pc_Db.quick_app_info.Where(ele => ele.id == id).FirstOrDefault();
                if (quickAppInfo != null)
                {
                    quickAppInfo.is_del = 1;
                    bins_Pc_Db.SaveChanges();
                    MessageBox.Show("删除id=" + id + "的数据成功");
                }
                else
                {
                    MessageBox.Show("删除失败，id=" + id + "的数据查询为空");
                }
            }
        }

        // 手动删除标记
        private void button12_Click(object sender, EventArgs e)
        {
            string s = ApplicationManager.delBusinessControl();
            MessageBox.Show(s);
        }

        // 手动增加标记
        private void button13_Click(object sender, EventArgs e)
        {
            string s = ApplicationManager.addBusinessControl(1, "已增加标记\n");
            MessageBox.Show(s);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            ApplicationManager.runExe("C:\\Program Files(x86)\\Microsoft Visual Studio\\2019\\Community\\Common7\\IDE\\devenv.exe");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ApplicationManager.runExe("C:\\Program Files\\JetBrains\\PyCharm Community Edition 2021.3\\bin\\pycharm64.exe");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ApplicationManager.runExe("C:\\11_Android\\android_setup\\bin\\studio64.exe");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ApplicationManager.runExe("C:\\Program Files\\JetBrains\\PhpStorm 2021.3.2\\bin\\phpstorm64.exe");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ApplicationManager.runExe("C:\\Program Files\\JetBrains\\IntelliJ IDEA 2022.1.3\\bin\\idea64.exe");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ApplicationManager.runExe("C:\\0_快速启动\\双微信.bat");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ApplicationManager.runExe("C:\\Qt\\Qt5.9.9\\Tools\\QtCreator\\bin\\qtcreator.exe");
        }

        // ***** 测试在 C# 中执行 cmd 命令 *****
        private void button14_Click(object sender, EventArgs e)
        {
            // bool result = CmdUtils.RunCmd("ipconfig" + "&exit");
            string result = CmdUtils.RunCMDCommand("ipconfig");
            MessageBox.Show(result);
        }

        // 打开另一个子窗口（执行 python 脚本）
        private void pYTHONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void 文件夹复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void btn24_GCViewer_Click(object sender, EventArgs e)
        {
            string result = CmdUtils.RunCMDCommand("java -jar C:\\3_dev_files\\gcviewer-1.29\\gcviewer-1.37-SNAPSHOT[ok].jar");
            Console.WriteLine("===========result:" + result);
        }
    }
}
