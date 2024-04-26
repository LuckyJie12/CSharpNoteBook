using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Homework2
{
    public class People
    {
        public string Name { get; set; }
        public ConsoleColor Color { get; set; }
        public List<string> Experience { get; set; }
    }
    internal class JSONHelper
    {
        public  T JsonToList<T>(string filePath)
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", ""); // 获取项目根目录
            string configJsonFolderPath = Path.Combine(projectRoot, filePath);
            if (File.Exists(configJsonFolderPath))
            {
                string Json=File.ReadAllText(configJsonFolderPath);
                return JsonConvert.DeserializeObject<T>(Json);
            }
            else
            {
                throw new Exception($"Json文件：{filePath}不纯在");
            }
        }
    }
}
