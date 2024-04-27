using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAppConfig.AppSetting
{
    /// <summary>
    /// 应用程序的总体配置类，包含数据库和日志配置。
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 数据库相关配置。
        /// </summary>
        public DatabaseConfig Database { get; set; }
        /// <summary>
        /// 日志相关配置。
        /// </summary>
        public LoggingConfig Logging { get; set; }
    }
    /// <summary>
    /// 数据库配置类，用于存储数据库连接字符串等信息。
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// 数据库连接字符串，包含连接数据库所需的所有信息。
        /// </summary>
        public string ConnectionString { get; set; }
    }

    /// <summary>
    /// 日志配置类，包含日志级别的配置。
    /// </summary>
    public class LoggingConfig
    {
        /// <summary>
        /// 日志级别配置，定义不同源的日志级别。
        /// </summary>
        public LogLevelConfig LogLevel { get; set; }
    }

    /// <summary>
    /// 日志级别详细配置类，指定各个组件的日志级别。
    /// </summary>
    public class LogLevelConfig
    {
        /// <summary>
        /// 默认日志级别。
        /// </summary>
        public string Default { get; set; }
        /// <summary>
        /// Microsoft组件的日志级别。
        /// </summary>
        public string Microsoft { get; set; }
        /// <summary>
        /// Microsoft Hosting生命周期事件的日志级别。
        /// </summary>
        public string MicrosoftHostingLifetime { get; set; }
        /// <summary>
        /// 最后使用时间
        /// </summary>
        public string LastDateTime { get; set; }
    }
}
