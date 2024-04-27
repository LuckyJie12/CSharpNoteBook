using Newtonsoft.Json;
using ReadAppConfig.AppSetting;
using ReadAppConfig.SectionConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ReadAppConfig
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigAction();
            UserSettingAction();
            GetAppSetting();
        }
        /// <summary>
        /// 读取以及修改App.config配置内容
        /// </summary>
        public static void ConfigAction()
        {
            //读取配置项
            var Singer = ConfigurationManager.AppSettings["Singer"];
            var misicName = ConfigurationManager.ConnectionStrings["misicName"].ConnectionString;
            //修改配置项
            //将当前应用程序的配置文件作为 Configuration 对象打开
            var cfm = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //(AppSettings)
            cfm.AppSettings.Settings["Singer"].Value = "赵二";
            //(ConnectionStrings)
            cfm.ConnectionStrings.ConnectionStrings["misicName"].ConnectionString = "孤岛";
            //保存
            cfm.Save();
            //具体修改后App.config可以查看../bin/Debug/NameConfig.exe.Config
        }
        /// <summary>
        /// 自定义SectionConfig的操作（读取以及修改）
        /// </summary>
        public static void UserSettingAction()
        {
            var configSetting = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (configSetting.GetSection("UserSettings") == null)
            {
                configSetting.Sections.Add("UserSettings", new UserSettings());
                configSetting.Save();
            }
            var Config = (UserSettings)configSetting.GetSection("UserSettings");
            //读取
            var musicInfo = Config.Singer + Config.musicName;
            //修改
            if (Config != null)
            {
                Config.Singer = "郭顶";
                Config.musicName = "水星记";
                configSetting.Save();
            }
        }
        /// <summary>
        /// 读取appsettings.json配置内容
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void GetAppSetting()
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", ""); // 获取项目根目录
            string configJsonFolderPath = Path.Combine(projectRoot, "AppSetting\\appsettings.json");
            AppSettings appSettings = new AppSettings();
            if (File.Exists(configJsonFolderPath))
            {
                string Json = File.ReadAllText(configJsonFolderPath);
                appSettings = JsonConvert.DeserializeObject<AppSettings>(Json);
                //修改配置文件
                appSettings.Logging.LogLevel.LastDateTime = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss");
                // 保存修改后的JSON回文件
                // 序列化修改后的对象回JSON
                string updatedJson = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
                File.WriteAllText(configJsonFolderPath, updatedJson);

                Console.WriteLine("配置文件已更新.");
            }
            else
            {
                throw new Exception("Json文件：AppSetting\\appsetting.json不纯在");
            }
        }
    }
}
