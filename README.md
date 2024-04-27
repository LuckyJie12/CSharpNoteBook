# 1.项目配置文件管理

## 1.1 App.config文件管理

### 1.1.1 文件配置

~~~c#
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>
	<appSettings>
		<add key="Singer" value="林俊杰"/>
	</appSettings>
	<connectionStrings>
		<add name="misicName" connectionString="交换余生"/>
	</connectionStrings>
</configuration>
~~~

### 1.1.2 文件操作

~~~c#
//读取配置项
var Singer = ConfigurationManager.AppSettings["Singer"];
var misicName = ConfigurationManager.ConnectionStrings["misicName"].ConnectionString;
//修改配置项
//将当前应用程序的配置文件作为 Configuration 对象打开
var cfm =ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//(AppSettings)
cfm.AppSettings.Settings["Singer"].Value = "赵二";
//(ConnectionStrings)
cfm.ConnectionStrings.ConnectionStrings["misicName"].ConnectionString = "孤岛";
//保存
cfm.Save();
//具体修改后App.config可以查看运行文件../bin/Debug/NameConfig.exe.Config
~~~

## 1.2 自定义Section文件管理

### 1.2.1 自定义Config配置

~~~c#
//新建类:UserSettings继承类:ConfigurationSection
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
~~~

### 1.2.2 文件操作

~~~c#
var configSetting = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
if (configSetting.GetSection("UserSettings") == null)
{
    configSetting.Sections.Add("UserSettings", new UserSettings());
    configSetting.Save();
}
var Config = (UserSettings)configSetting.GetSection("UserSettings");
//读取
var musicInfo = Config.Singer+Config.musicName;
//修改
if (Config != null)
{
    Config.Singer = "郭顶";
    Config.musicName = "水星记";
    configSetting.Save();//保存
}
~~~

## 1.3 appsettings.json文件管理

### 1.3.1 文件配置

~~~json
{
  "Database": {
    "ConnectionString": "Server=(localdb)\\mssqllocaldb;Database=MyDb;Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
~~~

### 1.3.2 文件操作

~~~c#
var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();
// 获取配置项
var connectionString = configuration.GetConnectionString("Database:ConnectionString");
var logLevel = configuration["Logging:LogLevel:Default"];
//可以通过 IConfigurationRoot 对象的索引器来修改配置值
configuration["Logging:LogLevel:Default"] = "Warning";
~~~

# 2.事件与委托

## 2.1定义环境

~~~c#
internal class Animals
{
    //委托
    public delegate void AnimalsAction();
    //初始化委托
    public Action<int> AnimalsDelegate=null;
    //事件
    public event Action<int> AnimalsEvent;
    /// <summary>
    /// 内部执行事件
    /// </summary>
    public void ActionEvent(int Num)
    {
        AnimalsEvent?.Invoke(Num);
        //清空AnimalsEvent
        AnimalsEvent = null;
    }
}
class Cat
{
    public void Show(int num)
    {
        Console.WriteLine($"小猫喵喵叫了{num}声");
    }
}
class Dog
{
    public void Show(int num)
    {
        Console.WriteLine($"小狗汪汪叫了{num}声");
    }
}
class Mouse
{
    public void Show(int num)
    {
        Console.WriteLine($"小鼠吱吱叫了{num}声");
    }
}
~~~



## 2.2 委托的使用

### 1.普通委托使用

~~~c#
Animals animals = new Animals();
var Cat= new Cat();
animals.AnimalsDelegate += Cat.Show;
animals.AnimalsDelegate += new Dog().Show;
animals.AnimalsDelegate += Cat.Show;
animals.AnimalsDelegate += new Mouse().Show;
{
    //不能直接减，不是同一个实例
    animals.AnimalsDelegate -= new Cat().Show;
    //必须为同一实例，减方法顺序为从后往前
    animals.AnimalsDelegate -= Cat.Show;
}
//？号为如果不为空则Invoke()
animals.AnimalsDelegate?.Invoke(5);
//可直接赋值为null
animals.AnimalsDelegate = null;
~~~

### 2.C#强类型委托Action和Func

~~~c#
//不带返回值
Action<int, int> action = (i, j) =>
{
    Console.WriteLine("参数一：" + i.ToString() + "参数二：" + j.ToString());
};
action(1, 2);
//带返回值
Func<int, int, bool> func = (i, j) =>
{
    if (i > j)
    {
        return true;
    }
    else
    {
        return false;
    }
};
Console.WriteLine("第一个数比第二个数大:" + func(2, 5));
Method(func);
void Method(Func<int,int,bool> func1)
{
    if (func1(2,5))
    {
        Console.WriteLine("返回："+true);
    }
    else
    {
        Console.WriteLine("返回："+false);
    }
}
~~~

## 2.2 事件的使用 

~~~c#
Animals animals = new Animals();
var Cat=new Cat();
animals.AnimalsEvent += Cat.Show;
animals.AnimalsEvent += new Dog().Show;
animals.AnimalsEvent += Cat.Show;
animals.AnimalsEvent += new Mouse().Show;
{
    //不能直接减，不是同一个实例
    animals.AnimalsEvent -= new Cat().Show;
    //必须为同一实例，减方法顺序为从后往前
    animals.AnimalsEvent -= Cat.Show;
}
//事件在外部不能Invoke和赋值只有声明者可以用，子类也不行
animals.ActionEvent(6);
~~~

## 2.3 委托和事件的弊端

- 调用委托时，如果其中的一个委托报错，则后面的不会被调用
- 只有最后一个的返回值才会作为委托的返回值
- 因为是数组所以删除的效率是O(n)---从后往前找删除
- 线程不安全

