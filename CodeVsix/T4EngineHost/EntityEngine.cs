using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using System.IO;
using System.CodeDom.Compiler;

namespace Fostor.CodeVsix
{
    public static class EntityEngine
    {
        public static void GenCode(EntityInfo entity, string templateFilePath, string outputFilePath)
        {
            EntityHost host = new EntityHost();
            host.Entity = entity;
            host.TemplateFile = templateFilePath;
            host.SetValue("NameSpace", entity.ClassNameSpace);
            host.SetValue("ClassName", entity.Name);
            host.SetValue("ModuleName", entity.ModuleName);
            host.SetValue("BaseNameSpace", entity.ClassNameSpace.Substring(0, entity.ClassNameSpace.LastIndexOf('.')));

            Engine engine = new Engine();
            string templateContent = string.Empty;
            if (File.Exists(templateFilePath))
            {
                templateContent = File.ReadAllText(templateFilePath);
            }
            else
            {
                throw new Exception("CannotFindTheTemplateFilePath");
            }
            var outputContent = engine.ProcessTemplate(templateContent, host);
            //error output
            StringBuilder sb = new StringBuilder();
            if (host.ErrorCollection != null && host.ErrorCollection.HasErrors)
            {
                foreach (CompilerError err in host.ErrorCollection)
                {
                    sb.AppendLine(err.ToString());
                }
                outputContent = outputContent + Environment.NewLine + sb.ToString();
                outputFilePath = outputFilePath + ".error";
            }
            File.WriteAllText(outputFilePath, outputContent, Encoding.UTF8);
        }

        public static void GenApplicationCode(string baseFolder,EntityInfo entity)
        {
            var templates = new[] {
                @"Application\{Module}\Dto\{Entity}Dto.tt.cs"
                , @"Application\{Module}\Dto\{Entity}QueryDto.tt.cs"
                , @"Application\{Module}\I{Entity}AppService.tt.cs"
                , @"Application\{Module}\{Entity}AppService.tt.cs"
            };
            foreach (var template in templates)
            {
                var templateFilePath = Path.Combine(baseFolder+@"\", template.Replace(".cs", ""));
                var outputFilePath = Path.Combine(baseFolder.Substring(0,baseFolder.LastIndexOf(@"\"))+@"\GenCode\", template.Replace("{Module}", entity.ModuleName).Replace("{Entity}", entity.Name).Replace(".tt", ""));
                var folder = outputFilePath.Substring(0, outputFilePath.LastIndexOf('\\'));
                Directory.CreateDirectory(folder);              
                GenCode(entity, templateFilePath, outputFilePath);
            }
        }

        public static void GenAreasCode(string baseFolder, EntityInfo entity, int caseType = 0)
        {
            var templates = new[] {
                @"Areas\{Module}\Controllers\{Entity}Controller.tt.cs"
                ,@"Areas\{Module}\Models\{Entity}ViewModel.tt.cs"
                , @"Areas\{Module}\Views\{Entity}\Index.tt.cshtml"
                , @"Areas\{Module}\Views\{Entity}\_EditModal.tt.cshtml"
                , @"Areas\{Module}\Views\{Entity}\_AddModal.tt.cshtml"
                , @"wwwroot\view-resources\{Module}\{Entity}\Index.tt.js"
                , @"wwwroot\view-resources\{Module}\{Entity}\_EditModal.tt.js"
                , @"wwwroot\view-resources\{Module}\{Entity}\_AddModal.tt.js"
            };
            if (caseType == 1)
            {
                templates = new[] {
                    @"Areas\{Module}\Controllers\{Entity}Controller2.tt.cs"
                    ,@"Areas\{Module}\Models\{Entity}ViewModel.tt.cs"
                    , @"Areas\{Module}\Views\{Entity}\Index2.tt.cshtml"
                    , @"Areas\{Module}\Views\{Entity}\Edit.tt.cshtml"
                    , @"Areas\{Module}\Views\{Entity}\Add.tt.cshtml"
                    , @"wwwroot\view-resources\{Module}\{Entity}\Index2.tt.js"
                    , @"wwwroot\view-resources\{Module}\{Entity}\Edit.tt.js"
                    , @"wwwroot\view-resources\{Module}\{Entity}\Add.tt.js"
                };
            }
            foreach (var template in templates)
            {
                var templateFilePath = Path.Combine(baseFolder + @"\", template.Replace(".cshtml", "").Replace(".js", "").Replace(".cs", ""));
                var outputFilePath = Path.Combine(baseFolder.Substring(0, baseFolder.LastIndexOf(@"\")) + @"\GenCode\", template.Replace("{Module}", entity.ModuleName).Replace("{Entity}", entity.Name).Replace(".tt", "").Replace("2.","."));
                var folder = outputFilePath.Substring(0, outputFilePath.LastIndexOf('\\'));
                Directory.CreateDirectory(folder);
                GenCode(entity, templateFilePath, outputFilePath);
            }
        }
    }
}
