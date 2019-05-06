using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Fostor.GinkgoCode.EntityMeta;
using Fostor.GinkgoCode.Helpers;
using Fostor.GinkgoCode.T4EngineHost;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.RegularExpressions;


namespace Fostor.CodeGen
{
    public class GenCodeHelper
    {
        public static void TestGenCode()
        {
            //Assembly assembly = Assembly.LoadFrom("Fostor.Ginkgo.Core.dll");
            //object obj = assembly.CreateInstance("Fostor.Ginkgo.TaskSample.LeaveApplication");
            Type type = typeof(Fostor.Ginkgo.TaskSample.LeaveApplication);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            EntityInfo entity = new EntityInfo();
            entity.FullName = type.FullName;
            entity.Name = type.Name;
            entity.ClassNameSpace = type.Namespace;
            entity.BaseClassName = type.BaseType.ToString();
            var fields = new GinkgoCode.EntityMeta.FieldInfo[properties.Count];
            for(int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                var field = new GinkgoCode.EntityMeta.FieldInfo();
                field.Name = property.Name;
                field.TypeName = (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType).ToString();
                field.DisplayName = property.DisplayName;
                if(property.PropertyType.Name.Contains("Nullable"))
                {
                    field.Nullable = true;
                }
                foreach(var x in property.Attributes)
                {
                    if (x.GetType() == typeof(MaxLengthAttribute))
                    {
                        field.MaxLength = ((MaxLengthAttribute)x).Length;
                    }
                    if(x.GetType() == typeof(RequiredAttribute))
                    {
                        field.Required = true;
                    }
                }
                fields[i] = field;
            }
            entity.Fields = fields;
            //EntityEngine.GenApplicationCode(entity);
            EntityEngine.GenAreasCode(AppDomain.CurrentDomain.BaseDirectory + @"\GinkgoCode\Templates",entity,1);

        }

        public static void GenCode2()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\LeaveApplication.cs";
            string modelContent = File.ReadAllText(path, Encoding.UTF8);
            modelContent = modelContent.Substring(modelContent.IndexOf("namespace "));
            //namespace
            Regex regNS = new Regex(@"^((\s)*)namespace(\b)(.+)$", RegexOptions.Multiline);
            string ns = regNS.Match(modelContent).Value.Replace("namespace", "").Trim();
            //class
            Regex regClass = new Regex(@"^((\s)*)(.+)class(\b)(.+)$", RegexOptions.Multiline);
            string cls = regClass.Match(modelContent).Value.Replace(" public ", " ").Replace(" class ", " ").Trim();
            if(cls.IndexOf(':') > 0)
            {
                cls = cls.Substring(0, cls.IndexOf(':')); //去掉继承的基类
            }
            EntityInfo entity = new EntityInfo();
            entity.FullName = ns+"."+cls;
            entity.Name = cls;
            entity.ClassNameSpace = ns;
            //fields
            string fds = modelContent.Substring(modelContent.IndexOf("class "));
            fds = fds.Substring(fds.IndexOf("{") + 1);
            fds = fds.Substring(0, fds.LastIndexOf("}"));
            fds = fds.Substring(0, fds.LastIndexOf("}")).Trim();
            fds = fds.Replace("}", "}|");
            fds = fds.Substring(0, fds.LastIndexOf('|'));
            var fs = fds.Split('|');
            var fields = new GinkgoCode.EntityMeta.FieldInfo[fs.Length];
            for(int i = 0; i < fs.Length; i++)
            {
                var field = new GinkgoCode.EntityMeta.FieldInfo();
                string content = fs[i];
                Regex regReq = new Regex(@"^((\s)*)(\[)Required(.*)$", RegexOptions.Multiline);
                if (regReq.IsMatch(content)) { field.Required = true; }
                Regex regMax = new Regex(@"^((\s)*)(\[)MaxLength(.+)$", RegexOptions.Multiline);
                if (regMax.IsMatch(content))
                {
                    string maxL = regMax.Match(content).Value.Replace("[MaxLength(", "").Replace(")", "").Replace("]", "").Replace("\r\n","").Trim();
                    int maxLength = 0;                   
                    if(int.TryParse(maxL, out maxLength))
                    {
                        field.MaxLength = maxLength;
                    }
                }
                Regex regFd = new Regex(@"^((\s)*)public(\b)(.+)$", RegexOptions.Multiline);
                string fdLine = regFd.Match(content).Value;
                fdLine = fdLine.Replace(" virtual ", " ").Replace("public ", " ")
                    .Replace("get;","").Replace("set;", "").Replace("{","").Replace("}", "").Trim();
                string typeName = fdLine.Substring(0, fdLine.IndexOf(" "));
                string fname = fdLine.Replace(typeName, "").Trim();
                field.Name = fname;
                field.TypeName = typeName;
                fields[i] = field;
            }

            entity.Fields = fields;
            //EntityEngine.GenApplicationCode(AppDomain.CurrentDomain.BaseDirectory+ @"\GinkgoCode\Templates",entity);
            EntityEngine.GenAreasCode(AppDomain.CurrentDomain.BaseDirectory+ @"\GinkgoCode\Templates", entity, 1);
        }

        public static void GenCode3()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\LeaveApplication.cs";         
            EntityInfo entity = GetEntity(path);           
            EntityEngine.GenAreasCode(AppDomain.CurrentDomain.BaseDirectory + @"\GinkgoCode\Templates", entity, 1);
        }

        public static EntityInfo GetEntity(string entityFilePath)
        {
            EntityInfo entity = new EntityInfo();

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
            var fields = new GinkgoCode.EntityMeta.FieldInfo[fs.Length];
            for (int i = 0; i < fs.Length; i++)
            {
                var field = new GinkgoCode.EntityMeta.FieldInfo();
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

            return entity;
        }
    }
}
