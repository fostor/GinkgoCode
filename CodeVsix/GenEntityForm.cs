using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fostor.CodeVsix
{
    public partial class GenEntityForm : Form
    {
        private string _baseFolder;
        public GenEntityForm()
        {
            InitializeComponent();
        }

        public GenEntityForm(string baseFolder)
        {
            _baseFolder = baseFolder;
            InitializeComponent();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNameSpace.Text.Trim().Length < 1)
                {
                    MessageBox.Show("命名空间需要输入");
                }
                if (txtClassName.Text.Trim().Length < 1)
                {
                    MessageBox.Show("实体类名需要输入");
                }
                if (rtbContent.Text.Trim().Length < 1)
                {
                    MessageBox.Show("类属性文本需要输入");
                }
                EntityInfo entity = GetEntity();
                txtResult.AppendText(DateTime.Now.ToLongTimeString() + "  开始生成代码..." + Environment.NewLine);
                EntityEngine.GenEntityCode(_baseFolder, entity);
                txtResult.AppendText(DateTime.Now.ToLongTimeString() + "  完成实体代码生成。" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成出错:" + ex.ToString());
            }
        }

        private EntityInfo GetEntity()
        {
            string modelContent = rtbContent.Text;
            EntityInfo entity = new EntityInfo();
            string ns = txtNameSpace.Text.Trim();
            string cls = txtClassName.Text.Trim();
            entity.FullName = ns + "." + cls;
            entity.Name = cls;
            entity.ClassNameSpace = ns;

            //fields  
            //modelContent = modelContent.Replace(Environment.NewLine, "|");
            var fs = modelContent.Split('\n');
            //忽略第一行的标题内容
            var fields = new FieldInfo[fs.Length - 1];
            for (int i = 1; i < fs.Length; i++)
            {
                var field = new FieldInfo();
                string content = fs[i];
                string[] ss = content.Split('\t');
                string typeName = ss[2];
                string fname = ss[0];
                if (typeName.StartsWith("string", true, null))
                {
                    string maxL = typeName.Substring(typeName.IndexOf("(") + 1);
                    maxL = maxL.Replace(")", "").Trim();
                    int maxLength = 0;
                    if (int.TryParse(maxL, out maxLength))
                    {
                        field.MaxLength = maxLength;
                    }
                    if (typeName.Contains("("))
                    {
                        typeName = typeName.Replace("(" + maxL + ")", "").Trim();
                    }
                }
                field.Name = fname;
                field.TypeName = typeName;
                fields[i - 1] = field;
            }
            entity.Fields = fields;
            return entity;
        }
    }
}
