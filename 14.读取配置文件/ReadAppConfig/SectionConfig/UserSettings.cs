using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAppConfig.SectionConfig
{
    internal class UserSettings : ConfigurationSection
    {
        //特性[ConfigurationProperty(名称,默认值)]
        [ConfigurationProperty("Singer", DefaultValue = "周杰伦")]
        public string Singer
        {
            get => (string)this["Singer"];
            set => this["Singer"] = value;
        }
        [ConfigurationProperty("musicName", DefaultValue = "夜曲")]
        public string musicName
        {
            get => (string)this["musicName"];
            set => this["musicName"] = value;
        }
    }
}
