using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Fostor.CodeVsix
{
    public partial class GenCodeForm : Form
    {
        public GenCodeForm()
        {
            InitializeComponent();
        }

        private void btnSelRoot_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFolderRoot.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSelFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtEntityPath.Text = openFileDialog1.FileName;
            }
        }
      
        private void btnGenCode_Click(object sender, EventArgs e)
        {            
            try
            {
                string baseFolder = txtFolderRoot.Text.Trim();
                string entityPath = txtEntityPath.Text.Trim();
                if (baseFolder.IndexOf(@"\") < 1)
                {
                    MessageBox.Show("请选择模板文件根目录");
                    return;
                }
                if (entityPath.IndexOf(@"\") < 1)
                {
                    MessageBox.Show("请选择实体类文件路径");
                    return;
                }
                if (ddlGenType.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择生成代码类别");
                    return;
                }              
                
                EntityInfo entity = GetEntity(entityPath);
                if (ddlGenType.SelectedIndex == 0)
                {
                    //MessageBox.Show("AppServiceModule");
                    rtbInfo.AppendText(DateTime.Now.ToLongTimeString() + "  开始生成应用服务层模块代码..." + Environment.NewLine);
                    EntityEngine.GenApplicationCode(baseFolder, entity);
                    rtbInfo.AppendText(DateTime.Now.ToLongTimeString() + "  完成生成应用服务层模块代码。" + Environment.NewLine);
                }
                if (ddlGenType.SelectedIndex == 1)
                {
                    //MessageBox.Show("Case1Area");
                    rtbInfo.AppendText(DateTime.Now.ToLongTimeString() + "  开始生成弹窗风格的增删改查界面代码..." + Environment.NewLine);
                    EntityEngine.GenAreasCode(baseFolder, entity);
                    rtbInfo.AppendText(DateTime.Now.ToLongTimeString() + "  完成生成弹窗风格的增删改查界面代码。" + Environment.NewLine);
                }
                if (ddlGenType.SelectedIndex == 2)
                {
                    //MessageBox.Show("Case2Area");
                    rtbInfo.AppendText(DateTime.Now.ToLongTimeString() + "  开始生成新开页面的增删改查界面代码..." + Environment.NewLine);
                    EntityEngine.GenAreasCode(baseFolder, entity);
                    rtbInfo.AppendText(DateTime.Now.ToLongTimeString() + "  完成生成新开页面的增删改查界面代码。" + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成出错。具体信息:" + ex.Message);
            }
        }

        private EntityInfo GetEntity(string entityFilePath)
        {
            EntityInfo entity = new EntityInfo();
            if (File.Exists(entityFilePath))
            {                
                string modelContent = File.ReadAllText(entityFilePath, Encoding.UTF8);
                modelContent = modelContent.Substring(modelContent.IndexOf("namespace "));
                //namespace
                Regex regNS = new Regex(@"^((\s)*)namespace(\b)(.+)$", RegexOptions.Multiline);
                string ns = regNS.Match(modelContent).Value.Replace("namespace", "").Trim();
                //class
                Regex regClass = new Regex(@"^((\s)*)(.+)class(\b)(.+)$", RegexOptions.Multiline);
                string cls = regClass.Match(modelContent).Value.Replace(" public ", " ").Replace(" class ", " ").Trim();
                if (cls.IndexOf(':') > 0)
                {
                    cls = cls.Substring(0, cls.IndexOf(':')); //去掉继承的基类
                }
                //fields
                string fds = modelContent.Substring(modelContent.IndexOf("class "));
                fds = fds.Substring(fds.IndexOf("{") + 1);
                fds = fds.Substring(0, fds.LastIndexOf("}"));
                fds = fds.Substring(0, fds.LastIndexOf("}")).Trim();
                fds = fds.Replace("}", "}|");
                fds = fds.Substring(0, fds.LastIndexOf('|'));
                var fs = fds.Split('|');
                var fields = new FieldInfo[fs.Length];
                for (int i = 0; i < fs.Length; i++)
                {
                    var field = new FieldInfo();
                    string content = fs[i];
                    Regex regReq = new Regex(@"^((\s)*)(\[)Required(.*)$", RegexOptions.Multiline);
                    if (regReq.IsMatch(content)) { field.Required = true; }
                    Regex regMax = new Regex(@"^((\s)*)(\[)MaxLength(.+)$", RegexOptions.Multiline);
                    if (regMax.IsMatch(content))
                    {
                        string maxL = regMax.Match(content).Value.Replace("[MaxLength(", "").Replace(")", "").Replace("]", "").Replace("\r\n", "").Trim();
                        int maxLength = 0;
                        if (int.TryParse(maxL, out maxLength))
                        {
                            field.MaxLength = maxLength;
                        }
                    }
                    Regex regFd = new Regex(@"^((\s)*)public(\b)(.+)$", RegexOptions.Multiline);
                    string fdLine = regFd.Match(content).Value;
                    fdLine = fdLine.Replace(" virtual ", " ").Replace("public ", " ")
                        .Replace("get;", "").Replace("set;", "").Replace("{", "").Replace("}", "").Trim();
                    string typeName = fdLine.Substring(0, fdLine.IndexOf(" "));
                    string fname = fdLine.Replace(typeName, "").Trim();
                    field.Name = fname;
                    field.TypeName = typeName;
                    fields[i] = field;
                }
                //bind entity property
                entity.FullName = ns + "." + cls;
                entity.Name = cls;
                entity.ClassNameSpace = ns;
                entity.Fields = fields;
            }

            return entity;
        }
    }
}
