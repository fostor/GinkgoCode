using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fostor.CodeVsix
{
    [Serializable]
    public class EntityInfo
    {
        public string Name { get; set; }
        public string BaseClassName { get; set; }
        public string FullName { get; set; }
        public string RootNameSpace { get; set; }
        public string ProjectName { get; set; }

        public string ClassNameSpace {
            //get
            //{
            //    var index = this.FullName.LastIndexOf('.');
            //    return this.FullName.Substring(0,index);
            //}
            get; set;
        }
        public string ModuleName
        {
            get
            {
                var array = this.FullName.Split('.');
                return array[array.Length - 2];
            }
        }
        public FieldInfo[] Fields { get; set; }
    }
    [Serializable]
    public class FieldInfo
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string ShortTypeName
        {
            get
            {
                var array = this.TypeName.Split('.');
                return array[array.Length - 1];
            }
        }
        public string DisplayName { get; set; }
        public bool Nullable { get; set; }
        public bool Required { get; set; }
        public int? MaxLength { get; set; }      
    }
}
