using CommandLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GConfig
{
    namespace GConfig
    {
        class Program
        {
            public class Options
            {
                [Option('s', "setting", Required = false, HelpText = "配置文件")]
                public string Setting { get; set; }

                [Option('c', "config", Required = false, HelpText = "待更新的配置文件")]
                public string Config { get; set; }
            }

            static void Main(string[] args)
            {
                Parser.Default.ParseArguments<Options>(args)
                       .WithParsed<Options>(o =>
                       {
                           if (string.IsNullOrEmpty(o.Setting))
                               o.Setting = "setting.json";

                           if (string.IsNullOrEmpty(o.Config))
                               o.Config = "../assets/config.json";

                           var settingPath = GetPath(o.Setting);
                           var configPath = GetPath(o.Config);

                           if (!File.Exists(settingPath))
                           {
                               Error($"找不到setting文件 {settingPath}");
                               return;
                           }
                           Info($"加载setting文件 {settingPath}");

                           if (!File.Exists(configPath))
                           {
                               Error($"找不到config文件 {configPath}");
                               return;
                           }
                           Info($"加载config文件 {configPath}");

                           var settingContent = File.ReadAllText(settingPath);
                           var configContent = File.ReadAllText(configPath);

                           var setting = JsonConvert.DeserializeObject<Setting>(settingContent);
                           var config = JsonConvert.DeserializeObject<Config>(configContent);

                           if (setting.ApplicationPaths == null || setting.ApplicationPaths.Count <= 0)
                           {
                               Info("配置文件中未设置 applicationPaths");
                               return;
                           }

                           foreach (var apppath in setting.ApplicationPaths)
                           {
                               var appconfigPath = GetPath(apppath);
                               if (!File.Exists(appconfigPath))
                               {
                                   Error($"{appconfigPath} 应用文件不存在");
                                   return;
                               }

                               FileInfo appConfigInfo = new FileInfo(appconfigPath);
                               XmlDocument xmldoc = new XmlDocument();
                               xmldoc.Load(appconfigPath);
                               //获取节点列表 
                               var assemblynode = xmldoc["asmv1:assembly"]["assemblyIdentity"];
                               var version = assemblynode.Attributes["version"].Value;
                               var assemblyName = assemblynode.Attributes["name"].Value;

                               var appconfigs = config.Apps.Where(x => x.DeployassemblyId == assemblyName);
                               if (appconfigs == null || appconfigs.Count() <= 0)
                                   return;
                               foreach(var appconfig in appconfigs)
                               {
                                   appconfig.Version = version;
                                   appconfig.PublishDate = appConfigInfo.LastWriteTime.ToLongDateString()
                                       + " " + appConfigInfo.LastWriteTime.ToLongTimeString();
                               }
                               Info($"更新{assemblyName}版本号及发布时间信息");
                           }

                           configContent = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                           {
                               ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                           });
                           File.WriteAllText(configPath, configContent, Encoding.UTF8);
                           Info($"更新{o.Config}成功");
                       });


            }

            static string GetPath(string absolutePath)
            {
                absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, absolutePath);
                return Path.GetFullPath((new Uri(absolutePath)).LocalPath);
            }

            static void Info(string msg)
            {
                Console.WriteLine("info:" + msg);
            }

            static void Error(string msg)
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error:" + msg);
                Console.ForegroundColor = defaultColor;
            }
        }
    }
}
