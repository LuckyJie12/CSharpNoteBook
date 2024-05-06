# 1.泛型和约束

在C#中，泛型允许你编写在多种数据类型上操作的灵活、可重用的代码。泛型最常见的用途是在集合类中，如`List<T>`和`Dictionary<TKey, TValue>`，但你也可以在自己的类、接口和方法中使用泛型。

### 泛型的优点：

1. **类型安全**：泛型提供了类型安全性，无需在运行时进行类型转换。
2. **性能**：使用泛型可以避免在对象和基本类型数据之间进行装箱和拆箱操作，这可以提高性能。
3. **重用性**：可以用相同的代码逻辑处理不同的数据类型。

### 泛型约束：

泛型约束指定了一个泛型类型参数必须继承的基类，实现的接口，或者必须具有的特定特性（如构造函数）。这使得在泛型代码中可以安全地使用泛型类型参数的特定功能。

常见的泛型约束包括：

- `where T : struct` —— T必须是值类型。
- `where T : class` —— T必须是引用类型。
- `where T : new()` —— T必须有一个公共的无参数构造函数。
- `where T : BaseClass` —— T必须继承自BaseClass。
- `where T : Interface` —— T必须实现接口Interface。
- `where T : U` —— T必须是U的子类型。

### 示例：使用泛型和约束

下面是一个使用泛型和泛型约束的示例，定义了一个泛型类`Buffer<T>`，它只接受实现了`IComparable<T>`接口的类型，这样可以在类内部比较元素：

```csharp
using System;

// 定义一个泛型类 Buffer，它接受任何实现了 IComparable<T> 接口的类型 T。
public class Buffer<T> where T : IComparable<T>
{
    private T[] items;  // 用于存储元素的数组
    private int count;  // 当前存储的元素数量

    // 构造函数，初始化具有指定大小的缓冲区
    public Buffer(int size)
    {
        items = new T[size];
    }

    // 添加一个元素到缓冲区，如果缓冲区已满，则抛出异常
    public void Add(T item)
    {
        if (count < items.Length)
        {
            items[count++] = item;
        }
        else
        {
            throw new InvalidOperationException("Buffer is full");
        }
    }

    // 返回缓冲区中的最大元素
    public T GetMax()
    {
        T max = items[0];  // 假设第一个元素是最大的
        for (int i = 1; i < count; i++)
        {
            // 使用 CompareTo 方法比较元素
            if (items[i].CompareTo(max) > 0)
            {
                max = items[i];  // 更新最大元素
            }
        }
        return max;
    }
}

public class Program
{
    public static void Main()
    {
        // 创建一个整数类型的 Buffer 实例，并添加一些整数
        Buffer<int> intBuffer = new Buffer<int>(5);
        intBuffer.Add(10);
        intBuffer.Add(20);
        intBuffer.Add(5);
        Console.WriteLine("Max: " + intBuffer.GetMax());  // 输出最大值: 20

        // 创建一个字符串类型的 Buffer 实例，并添加一些字符串
        Buffer<string> stringBuffer = new Buffer<string>(3);
        stringBuffer.Add("Alice");
        stringBuffer.Add("Bob");
        stringBuffer.Add("Charlie");
        Console.WriteLine("Max: " + stringBuffer.GetMax());  // 输出字典序最大的字符串: Charlie
    }
}
```

### 代码解释：

- **泛型类 `Buffer<T>`**：定义了一个泛型类，它接受任何实现了`IComparable<T>`的类型。这允许在类内部使用`CompareTo`方法，这是比较元素所必需的。
- **方法 `Add` 和 `GetMax`**：`Add` 方法用于添加元素到缓冲区，`GetMax` 方法用于找到缓冲区中的最大元素。
- **测试**：创建了`Buffer<int>`和`Buffer<string>`的实例，并测试了添加和获取最大值的功能。

通过这个示例，你可以看到泛型如何提高代码的灵活性和重用性，同时泛型约束如何确保泛型代码的安全性和功能性。

# 2.反射

在C#中，反射是一个强大的功能，它允许程序在运行时检查其自身的结构，包括类、接口、字段、方法等。反射主要通过 `System.Reflection` 命名空间中的类来实现。使用反射，你可以动态地创建对象、调用方法、访问字段和属性，即使这些信息在编写代码时并不完全已知。

### 反射的主要用途：

1. **动态创建对象**：在不知道对象类型的情况下创建对象实例。
2. **获取类型信息**：检索关于类型的信息，如其方法、属性、字段和构造函数。
3. **动态调用方法**：在运行时调用方法，而无需在编译时知道方法的详细信息。
4. **访问属性和字段**：动态读取或修改对象的属性和字段。

### 反射的关键类和接口：

- `Type`：表示类型声明：类类型、接口类型、数组类型、值类型等。
- `MethodInfo`、`FieldInfo`、`PropertyInfo`：提供关于方法、字段和属性的信息，并提供对它们的访问。
- `Activator`：提供方法来创建类型的实例、访问本地或远程对象，或获取类型的信息。

### 示例：使用反射

#### 目录结构：

![image-20240427170619141](C:\Users\29969\AppData\Roaming\Typora\typora-user-images\image-20240427170619141.png)

#### `AllAnimals.cs`代码：

~~~c#
public class AndAnimals
{
    public class BirdS
    {
        public void MaQue()
        {
            Console.WriteLine("我是小麻雀");
        }
    }
    public void Dogs()
    {
        Console.WriteLine("小狗汪汪叫！！！");
    }
    public void Cats()
    {
        Console.WriteLine("小猫喵喵叫！！！");
    }
}
public class Show
{
    public void Show1()
    {
        Console.WriteLine("我是显示1");
    }
    public void Show2(int Nums)
    {
        Console.WriteLine($"我是有参显示2，参数是{Nums}");
    }
    public void Show3(string Name, int ID)
    {
        Console.WriteLine($"我叫{Name}，我的编号是{ID}");
    }
    private void Show4()
    {
        Console.WriteLine("我是私有方法Show4！");
    }
    public static void Show5(string Name)
    {
        Console.WriteLine($"我是静态有参，我是：{Name}");
    }
}
public class Generic<T>
{
    public void Gen<M, S>(T t, M m, S s)
    {
        Console.WriteLine($"M的类型：{m.GetType().Name}，值是：{m},T的类型：{t.GetType().Name},值是：{t},S的类型：{s.GetType().Name},值是：{s}");
    }
}
public class People
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description;
    public void User()
    {
        Console.WriteLine("lalla");
    }
}
~~~

#### Program.cs代码：

~~~c#
static void Main(string[] args)
{
    //需要类型转换调用方法
    {
        // 加载名为 "Animal" 的程序集
        Assembly assembly = Assembly.Load("Animal");
        // 遍历程序集中的所有模块，并打印每个模块的完全限定名
        foreach (var item in assembly.GetModules())
        {
            Console.WriteLine(item.FullyQualifiedName);
        }
        // 遍历程序集中的所有类型，并打印每个类型的完全限定名
        foreach (var type in assembly.GetTypes())
        {
            Console.WriteLine(type.FullName);
        }
        // 获取名为 "Animal.AndAnimals+BirdS" 的类型
        // 注意：+号表示这是一个嵌套类型，BirdS是AndAnimals类中定义的一个嵌套类
        Type GetType = assembly.GetType("Animal.AndAnimals+BirdS");
        // 使用Activator.CreateInstance动态创建上面获取的类型的实例
        object AllAM = Activator.CreateInstance(GetType);
        // 将创建的对象转换为具体的类型 BirdS，以便可以调用其方法
        BirdS andAnimals = (BirdS)AllAM;
        // 调用BirdS类型的实例的MaQue方法
        andAnimals.MaQue();
    }
    //不进行类型转换利用反射调用方法
    {
        Assembly assembly = Assembly.Load("Animal");
        Type type = assembly.GetType("Animal.Show");
        foreach (var item in type.GetMethods())
        {
            Console.WriteLine(item.Name);
        }
        object Obj = Activator.CreateInstance(type);
        {
            //调用无参方法
            MethodInfo methodInfo = type.GetMethod("Show1");
            methodInfo.Invoke(Obj, null);
        }
        {
            MethodInfo methodInfo = type.GetMethod("Show1", new Type[] { });
            methodInfo.Invoke(Obj, new object[] { });
        }
        {
            //调用有参方法
            MethodInfo methodInfo = type.GetMethod("Show2");
            methodInfo.Invoke(Obj, new object[] { 123 });
        }
        {
            //调用有参方法
            MethodInfo methodInfo = type.GetMethod("Show3");
            methodInfo.Invoke(Obj, new object[] { "Jack", 123 });
        }
        {
            //调用静态方法
            MethodInfo methodInfo = type.GetMethod("Show5");
            methodInfo.Invoke(Obj, new object[] { "Jack" });
            //静态方法也可以这样
            methodInfo.Invoke(null, new object[] { "Lucy" });
        }
        {
            //调用私有方法
            MethodInfo methodInfo = type.GetMethod("Show4", BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo.Invoke(Obj, null);
        }
        {
            //泛型类的调用
            Assembly Gen = Assembly.Load("Animal");
            Type type1 = assembly.GetType("Animal.Generic`1");
            Type NewType = type1.MakeGenericType(new Type[] { typeof(int) });
            Object Ject = Activator.CreateInstance(NewType);
            //调用泛型方法
            MethodInfo methodInfo = NewType.GetMethod("Gen");
            MethodInfo NewMethod = methodInfo.MakeGenericMethod(new Type[] { typeof(DateTime), typeof(string) });
            NewMethod.Invoke(Ject, new object[] { 123, DateTime.Now, "Jack" });
        }
    }
    {
        // 定义People类的实例并初始化
        People Peo = new People();
        Peo.ID = 1;  // 设置ID属性
        Peo.Name = "test";  // 设置Name属性
        Peo.Description = "test";  // 设置Description属性
        // 获取People类的Type对象
        Type type = typeof(People);
        // 使用反射创建People类的新实例
        object OPeople = Activator.CreateInstance(type);
        // 遍历People类的所有属性
        foreach (var Prop in type.GetProperties())
        {
            // 打印属性名
            Console.WriteLine(Prop.Name);
            // 打印属性的当前值（新创建的实例的初始值）
            Console.WriteLine(Prop.GetValue(OPeople));

            // 如果属性名为"ID"，则设置该属性的值为123
            if (Prop.Name == "ID")
            {
                Prop.SetValue(OPeople, 123);
            }
            // 如果属性名为"Name"，则设置该属性的值为"Jack"
            else if (Prop.Name.Equals("Name"))
            {
                Prop.SetValue(OPeople, "Jack");
            }
        }
        // 获取People类的名为"User"的方法的MethodInfo对象
        MethodInfo info = type.GetMethod("User");
        // 调用OPeople实例的"User"方法
        info.Invoke(OPeople, null);
    }
}
~~~

# 3.特性`Attribute`

在C#中，特性（`Attributes`）是一种将元数据或声明性信息附加到代码（如类、方法、属性等）上的方式。特性可以在运行时通过反射被访问，这使得它们在许多高级编程场景中非常有用，如配置类的行为、方法的特性，或是其他框架级的功能，例如序列化、测试和拦截。

## 特性的基本用途包括：

1. **标记数据**：可以用于标记类或成员，稍后可以通过反射检查这些标记。
2. **控制行为**：某些特性可以改变代码的行为，例如 `[Obsolete]` 特性用于标记过时的代码。
3. **与工具集成**：特性常用于与IDE或其他工具集成，例如 `[Conditional]` 特性。

## 定义和使用特性：

要使用特性，你可以应用预定义的特性或定义自己的特性。定义一个特性首先需要创建一个继承自 `System.Attribute` 的类。

## 自定义特性1：

```csharp
public enum UserState
{
    [StateAttribute("正常")]
    Noemal = 0,
    [State("冻结")]
    Freeze = 1,
    //[State("删除")]
    Delete = 2
}
public class StateAttribute : Attribute
{
    public StateAttribute(string State)
    {
        this._State = State;
    }
    private string _State;
    public string SetState()
    {
        return this._State;
    }
}
public static class Custom
{
    // 扩展方法GetRemarks，用于Enum类型
    public static string GetRemarks(this Enum Value)
    {
        // 获取枚举值的类型
        Type type = Value.GetType();
        // 获取枚举值对应的字段信息
        FieldInfo field = type.GetField(Value.ToString());
        // 检查字段是否定义了StateAttribute特性，第二个参数true表示查找继承链
        if (field.IsDefined(typeof(StateAttribute), true))
        {
            // 如果定义了StateAttribute特性，获取该特性的实例
            StateAttribute state = (StateAttribute)field.GetCustomAttribute(typeof(StateAttribute), true);
            // 调用StateAttribute的SetState方法，返回相关的状态描述
            return state.SetState();
        }
        else
        {
            // 如果没有定义StateAttribute特性，返回枚举值的名称
            return Value.ToString();
        }
    }
}
```

## 自定义特性2：

```csharp
public class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    [StuQQHeFa(123456,9999999999)]
    public long QQNumber { get; set; }
    
}
public static class Stu
{
    public static bool ValueIsok(this Student Obj)
    {
        Type type = Obj.GetType();
        foreach (var t in type.GetProperties())
        {
            if (t.IsDefined(typeof(StuQQHeFaAttribute), true))
            {
                object[] objects = t.GetCustomAttributes(typeof(StuQQHeFaAttribute), true);
                foreach (StuQQHeFaAttribute obj in objects)
                {
                    if (!obj.GetIsok(t.GetValue(Obj).ToString()))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}
public class StuQQHeFaAttribute : Attribute
{
    public StuQQHeFaAttribute(long MinNumber,long MaxNumber)
    {
        this._MinNumber = MinNumber;
        this._MaxNumber = MaxNumber;
    }
    private long _MinNumber;
    private long _MaxNumber;
    public bool GetIsok(string QQNumber)
    {
        if (long.TryParse(QQNumber,out long Value))
        {
            if (Value > _MinNumber && Value < _MaxNumber)
            {
                return true;
            }
        }
        return false;
    }
}
```

## 使用特性

~~~c#
Console.WriteLine(UserState.Noemal.GetRemarks());//正常
Student student = new Student();
student.ID = 1;
student.Name = "Test";
student.QQNumber = 1234567;
Console.WriteLine(student.ValueIsok());//True
student.QQNumber = 123;
Console.WriteLine(student.ValueIsok());//False
~~~



## 总结

特性提供了一种强大的方法来添加元数据到程序中的各个部分。通过使用特性，开发者可以为程序添加信息，控制程序行为，或与各种工具和框架进行交互。自定义特性特别有用，因为它们可以被设计来满足特定的需求。

# 4.事件与委托

在C#中，委托和事件是实现事件驱动编程的核心组件。委托提供了一种将方法作为参数传递给其他方法的方式，而事件则是一种基于委托的消息传递机制，允许一个对象通知其他对象发生了某些事情。

### 委托（Delegates）

委托是一种类型，它安全地封装了一个方法的引用，类似于函数指针但类型安全。委托可以指向一个或多个方法，并且可以在运行时动态地调用这些方法。

#### 定义委托

```csharp
public delegate void MyDelegate(string message);
```

这行代码定义了一个委托 `MyDelegate`，它可以引用任何接受单个 `string` 参数并返回 `void` 的方法。

### 事件（Events）

事件是一种使对象能够通知其他对象发生了某些事情的机制。事件是基于委托的，并且提供了一种订阅和取消订阅事件通知的方式。

#### 定义事件

在定义事件之前，通常需要先定义一个委托，该委托指定了事件处理方法的签名。

```csharp
public class Publisher
{
    // 声明事件，使用之前定义的委托类型
    public event MyDelegate MyEvent;

    // 触发事件的方法
    public void RaiseEvent()
    {
        // 检查是否有方法订阅了事件
        MyEvent?.Invoke("Event triggered!");
    }
}
```

### Action 和 Func

`Action` 和 `Func` 是两种预定义的委托类型，它们简化了委托的使用，使得在不需要显式定义委托类型的情况下，可以快速地创建委托实例。

- **Action**：代表一个没有返回值的方法，可以有0到16个输入参数。
- **Func**：代表一个有返回值的方法，可以有0到16个输入参数，最后一个参数是返回类型。

#### 示例：使用 Action 和 Func

```csharp
using System;

public class Program
{
    public static void Main()
    {
        Action<string> displayAction = Console.WriteLine;
        displayAction("Hello, Action!");

        Func<int, int, int> sumFunc = (x, y) => x + y;
        Console.WriteLine("Sum: " + sumFunc(10, 20));
    }
}
```

### 示例：使用委托和事件

下面的示例展示了如何定义委托和事件，以及如何订阅和触发事件。

```csharp
using System;

public class Program
{
    public static void Main()
    {
        // 创建发布者对象
        Publisher publisher = new Publisher();

        // 订阅事件
        publisher.MyEvent += HandleEvent;

        // 触发事件
        publisher.RaiseEvent();

        // 取消订阅事件
        publisher.MyEvent -= HandleEvent;
    }

    // 事件处理器
    public static void HandleEvent(string message)
    {
        Console.WriteLine(message);
    }
}
```

### 总结

委托和事件是C#中实现事件驱动编程的基础。委托提供了一种方式来引用和调用方法，而事件则提供了一种机制，允许对象通知其他对象发生了特定的动作。`Action` 和 `Func` 委托进一步简化了委托的使用，使得在许多场景中可以更加方便地处理方法引用和调用。这些机制在设计响应用户操作的GUI应用程序或需要通知机制的系统中尤其有用。

## 委托拓展

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

# 5.加密与解密

在C#中，MD5和RSA是两种常用的加密技术，分别用于生成数据的哈希值和进行数据的加密解密。下面将详细介绍这两种技术的使用方法和示例。

### MD5

MD5（Message-Digest Algorithm 5）是一种广泛使用的哈希算法，用于生成固定长度（通常是32个字符）的哈希值。它主要用于验证数据的完整性，而不是加密数据。MD5是不可逆的，意味着你不能从哈希值恢复原始数据。

#### 示例：MD5加密字符串和文件

```csharp
public class MD5Extend
{
    public void Show()
    {
        string MD5 = GetMD5("213");
        Console.WriteLine(MD5);
        Thread t = new Thread(s =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog.FileName;
                Console.WriteLine(MD5Extend.GetFileMD5(FilePath));
            }
        }
        );
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
        t.Join();
    }
    /// <summary>
    /// MD5字符串加密
    /// X的大小写位转换出来字母大小写，数字2则为转换长度位16X2为32为长度
    /// </summary>
    /// <param name="input">输入需要转换的文字</param>
    /// <returns></returns>
    public static string GetMD5(string input)
    {
        using (MD5 md5 = MD5.Create()) // 创建MD5实例
        {
            //将字符串转换为字节数组
            byte[] inputBytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(input));
            // 计算哈希值
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder(); // 创建StringBuilder实例
            for (int i = 0; i < hashBytes.Length; i++)
            {
                // 将字节转换为16进制字符串
                sb.Append(hashBytes[i].ToString("X2"));
            }
            // 返回加密后的结果
            return sb.ToString();
        }
    }
    /// <summary>
    /// 获取文件的MD5加密
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    public static string GetFileMD5(string FilePath)
    {
        // 创建一个MD5加密算法的实例
        using (var md5 = MD5.Create())
        {
            // 打开要计算MD5值的文件
            using (var stream = File.OpenRead(FilePath))
            {
                // 计算文件的MD5值
                var hash = md5.ComputeHash(stream);

                // 将MD5值转换为字符串形式
                var md5String = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return md5String;
            }
        }
    }
}
```

### RSA

RSA是一种非对称加密算法，使用一对公钥和私钥进行加密和解密。公钥用于加密数据，私钥用于解密数据。RSA常用于安全数据传输。

#### 示例：RSA加密和解密

```csharp
public class RSAExtend
{
    public  void Show()
    {
        string Str = "Hello World";
        Console.WriteLine("加密前数据："+Str);
        RsaEncrypt _RsaEncrypt = new RsaEncrypt();
        KeyValuePair<string, string> keyValuePairs = _RsaEncrypt.GetKeyValue();
        KeyValuePair<string, string> keyValuePairs1 = _RsaEncrypt.GetKeyValue();
        string GetRsa = _RsaEncrypt.GetRSA(Str, keyValuePairs.Key, Encoding.UTF8);
        Console.WriteLine($"加密后数据：{GetRsa}");
        string OpenRsa = _RsaEncrypt.OpenRSA(GetRsa, keyValuePairs.Value, Encoding.UTF8);
        Console.WriteLine($"解密后数据：{OpenRsa}");
    }
}
public class RsaEncrypt
{
    /// <summary>
    /// 获取公钥和私钥
    /// </summary>
    /// <returns></returns>
    public KeyValuePair<string, string> GetKeyValue()
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string publicKey = rsa.ToXmlString(false);//公钥
        string privateKey = rsa.ToXmlString(true);//私钥
        return new KeyValuePair<string, string>(publicKey, privateKey);
    }
    /// <summary>
    ///字符加密
    /// </summary>
    /// <param name="input">加密字符</param>
    /// <param name="key">加密key</param>
    /// <param name="encoding">编码格式</param>
    /// <returns></returns>
    public string GetRSA(string input, string key, Encoding encoding)
    {
        // 创建RSA实例
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(key);
            // 加密数据
            byte[] data = encoding.GetBytes(input);
            byte[] encryptedData = rsa.Encrypt(data, false);
            string GetStr = Convert.ToBase64String(encryptedData);
            return GetStr;
        }
    }
    /// <summary>
    /// 字符解密
    /// </summary>
    /// <param name="input">解密字符</param>
    /// <param name="value">解密value</param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public string OpenRSA(string input, string value, Encoding encoding)
    {
        try
        {
            // 创建RSA实例
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(value);
                // 解密数据
                byte[] encryptedData = Convert.FromBase64String(input);
                byte[] decryptedData = rsa.Decrypt(encryptedData, false);
                string decryptedString = encoding.GetString(decryptedData);
                return decryptedString;
            }
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message + "解密密钥错误");
        }
    }
}
```

### 总结

MD5和RSA在C#中的应用非常广泛。MD5主要用于生成数据的哈希值，以验证数据的完整性，而RSA用于加密和解密数据，确保数据的安全传输。使用这些技术可以有效地保护数据不被未授权访问和篡改。

- **MD5**：适用于快速生成数据的哈希值，用于比较数据是否被更改。然而，由于MD5容易受到碰撞攻击，它不再推荐用于安全敏感的应用。
- **RSA**：非对称加密提供了使用公钥加密和私钥解密的机制，适用于安全数据交换。由于其计算过程较慢，通常用于小数据量的加密或用于加密传输用的对称密钥。

在实际应用中，通常会结合使用对称加密和非对称加密技术。例如，可以使用RSA加密对称密钥，然后使用该对称密钥来加密大量数据。这种方法结合了RSA的安全性和对称加密的效率。

在使用这些加密技术时，重要的是要确保密钥管理的安全性，避免密钥泄露，这是保证加密安全的关键。同时，应定期更新和替换密钥，以防止潜在的安全风险。

# 6.异步与多线程

在C#中，异步编程和多线程是两种处理并发执行任务的主要方法。它们各有特点和适用场景，下面将详细解释这些技术的关键概念和区别。

### 异步编程（Async/Await）

异步编程在C#中通过`async`和`await`关键字实现，主要用于提高应用程序的响应性，特别是在涉及I/O操作（如文件访问、数据库操作、网络请求等）时。

- **`async`关键字**：用于声明一个方法是异步的。它修改方法的签名，使得方法可以使用`await`关键字调用其他异步方法，并最终返回一个`Task`或`Task<T>`。`async`方法不会自己创建新线程，而是在现有线程上执行，直到遇到`await`表达式。
  
- **`await`关键字**：用于暂停当前异步方法的执行，等待异步操作的完成。在等待期间，当前线程不会被阻塞，而是可以返回并执行其他工作。当异步操作完成后，执行上下文返回到原来的方法，并从暂停的位置继续执行。

#### 示例：使用Async/Await进行网络请求

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class AsyncDemo
{
    public static async Task Main()
    {
        string content = await FetchWebPage("http://example.com");
        Console.WriteLine(content);
    }

    public static async Task<string> FetchWebPage(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }
}
```

### 多线程

多线程处理可以通过不同的类和方法实现，主要包括`Thread`、`ThreadPool`和`Task`。

- **`Thread`类**：是.NET中最基本的线程操作类，允许直接管理线程的创建、启动、停止等。使用`Thread`类可以提供最大的控制力，但同时需要手动处理线程的生命周期和同步问题。

- **`ThreadPool`类**：用于重用一组线程的线程池。`ThreadPool`适用于执行多个较小的任务，可以减少线程创建和销毁的开销。它自动管理线程的数量和生命周期，适合执行大量短暂的后台任务。

- **`Task`类**：是基于`ThreadPool`的更高级的抽象，提供了一种基于任务的异步模式。`Task`可以返回结果，并支持连续的任务（Continuations）、并行运行、任务取消等高级功能。`Task`通常与`async`和`await`一起使用，简化异步编程。

#### 示例：使用Task并行处理

```csharp
using System;
using System.Threading.Tasks;

public class TaskDemo
{
    public static void Main()
    {
        Task<int> task1 = Task.Run(() => ComputeSum(10)); // 计算1到10的和
        Task<int> task2 = Task.Run(() => ComputeSum(100)); // 计算1到100的和

        Task.WaitAll(task1, task2); // 等待所有任务完成

        Console.WriteLine($"Sum 1 to 10: {task1.Result}");
        Console.WriteLine($"Sum 1 to 100: {task2.Result}");
    }

    private static int ComputeSum(int n)
    {
        int sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += i;
        }
        return sum;
    }
}
```

### 线程锁

在C#中，线程锁（thread locking）是一种同步机制，用于保证多个线程在访问共享资源时的安全性。当多个线程尝试同时访问同一资源（如数据或文件）时，如果没有适当的同步措施，就可能导致数据损坏或不一致的结果。线程锁可以防止这种情况的发生。

C#提供了几种不同的线程锁机制，包括：

1. **`lock`关键字**：最常用的同步机制，它基于`Monitor`类。`lock`块确保一次只有一个线程可以执行代码块。

2. **`Monitor`类**：提供了一组方法，用于控制对对象的锁定和解锁，比`lock`关键字更灵活。

3. **`Mutex`**：用于同一应用程序的不同线程或不同应用程序之间的同步。

4. **`Semaphore`和`SemaphoreSlim`**：限制可以同时访问某一资源或资源池的线程数。

5. **`ReaderWriterLockSlim`**：允许多个线程同时读取，但写入时需要独占访问。

#### 示例：使用`lock`

`lock`是实现线程安全的最简单方法。以下是一个使用`lock`的示例：

```csharp
using System;
using System.Threading;

public class Counter
{
    private int count = 0;
    private readonly object lockObject = new object();

    public void Increment()
    {
        lock (lockObject)
        {
            count++;
            Console.WriteLine($"Current count: {count}");
        }
    }

    public void Decrement()
    {
        lock (lockObject)
        {
            count--;
            Console.WriteLine($"Current count: {count}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Counter counter = new Counter();

        Thread t1 = new Thread(counter.Increment);
        Thread t2 = new Thread(counter.Decrement);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();
    }
}
```

在这个例子中，`lockObject`是一个用于同步的对象。`Increment`和`Decrement`方法通过`lock`块保证了在修改`count`值时的线程安全。

#### 注意事项

- **死锁**：如果不正确使用锁，可能会导致死锁，即两个或多个线程互相等待对方释放锁，从而无法继续执行。
- **性能**：过度使用锁可能会降低应用程序的性能，因为它限制了代码的并行执行。
- **选择合适的锁**：根据具体需求选择合适的锁类型，例如，如果读操作远多于写操作，使用`ReaderWriterLockSlim`可能更合适。

线程锁是确保多线程程序正确执行的关键工具，但需要谨慎使用，以避免死锁和性能问题。正确地使用线程锁可以帮助开发安全、高效的多线程应用程序。

### 总结

- **异步编程**（`async`和`await`）主要用于提高应用的响应性，特别是在执行I/O密集型操作时。
- **多线程**（`Thread`、`ThreadPool`、`Task`）用于提高应用的性能，通过并行处理来加速CPU密集型
- 任务。每种多线程技术都有其特定的用途和优势：

  - **`Thread`**：提供最大的控制力，适合于需要精细管理线程行为的场景。
  - **`ThreadPool`**：适用于执行多个较小的、短暂的任务，减少线程创建和销毁的开销，优化性能。
  - **`Task`**：提供基于任务的异步模式，支持异步操作和并行计算，是现代C#应用中推荐的方式。

  使用这些技术时，开发者需要注意线程安全问题，确保数据在多线程环境下的正确性和一致性。例如，当多个线程尝试同时修改同一数据时，应使用锁（如 `lock` 语句）或其他同步机制来避免竞态条件。

  总之，C#中的异步和多线程技术是强大的工具，可以帮助开发高效、响应快速的应用程序。正确地使用这些技术，可以显著提升程序的性能和用户体验。

# 7.数据结构和特殊类型

在C#中，有多种数据结构和特殊类型，每种都有其特定的用途和优势。这些数据结构和类型包括数组、列表、字典、集合、元组等，它们都在`System.Collections`和`System.Collections.Generic`命名空间中定义。下面将详细介绍这些数据结构和特殊类型，并提供示例。

### 数组（Array）

数组是最基本的数据结构，用于存储相同类型的元素集合。数组的大小在创建时确定，并且在整个生命周期中不可更改。

#### 示例：使用数组

```csharp
int[] numbers = new int[5] { 1, 2, 3, 4, 5 };
Console.WriteLine(numbers[0]); // 输出第一个元素
```

### 列表（List）

列表是一种动态大小的序列，提供了添加、删除和搜索元素的方法。`List<T>`是泛型版本，提供了类型安全。

#### 示例：使用列表

```csharp
List<string> fruits = new List<string>();
fruits.Add("Apple");
fruits.Add("Banana");
fruits.Add("Cherry");
Console.WriteLine(fruits[1]); // 输出"Banana"
```

### 字典（Dictionary）

字典是一种基于键值对的集合，可以快速检索数据。每个添加到字典中的元素都包含一个键和一个值。键必须是唯一的。

#### 示例：使用字典

```csharp
Dictionary<string, int> ages = new Dictionary<string, int>();
ages["Alice"] = 28;
ages["Bob"] = 25;
Console.WriteLine(ages["Alice"]); // 输出28
```

### 元组（Tuple）

元组是一种数据结构，用于存储不同类型的数据。C# 7.0引入了值元组，使得元组的使用更加方便和直观。

#### 示例：使用元组

```csharp
(var name, var age) = ("Alice", 30);
Console.WriteLine(name); // 输出"Alice"
Console.WriteLine(age); // 输出30
```

### 特殊类型：Nullable

`Nullable<T>`类型允许值类型（如int和double）接受null值，这在数据库操作和异常处理中非常有用。

#### 示例：使用Nullable

```csharp
int? a = null;
if (a.HasValue)
{
    Console.WriteLine(a.Value);
}
else
{
    Console.WriteLine("a is null");
}
```

### 哈希赛特：HashSet

`HashSet<T>` 是一种基于哈希表的数据结构，用于存储不重复的元素集合。它提供了高效的成员检查，并支持多种集合操作，如添加、删除、查找以及执行集合的并集、交集和差集等。`HashSet<T>` 是在 `System.Collections.Generic` 命名空间下。

#### 特点

- **唯一性**：自动确保所有元素都是唯一的，不允许重复。
- **高效性**：对元素的添加、删除和查找操作提供了高效的性能，这些操作的时间复杂度大致为 O(1)。
- **无序**：元素在 `HashSet` 中的存储是无序的，不保证任何特定的遍历顺序。

#### 常用方法

- **Add(T item)**：添加元素到集合中，如果元素已存在，则返回 `false`。
- **Remove(T item)**：从集合中移除指定元素，如果成功移除，则返回 `true`。
- **Contains(T item)**：检查集合中是否包含指定的元素。
- **Clear()**：清空集合中的所有元素。
- **UnionWith(IEnumerable<T> other)**：对当前集合与指定集合进行并集操作。
- **IntersectWith(IEnumerable<T> other)**：对当前集合与指定集合进行交集操作。
- **ExceptWith(IEnumerable<T> other)**：从当前集合中移除存在于指定集合中的元素（差集）。
- **Count**：获取集合中元素的数量。

#### 示例代码

下面是一个使用 `HashSet<T>` 的示例，展示了如何进行元素的添加、删除和集合操作。

```csharp
HashSet<int> numbers = new HashSet<int>();
// 添加元素
numbers.Add(1);
numbers.Add(2);
numbers.Add(3);
numbers.Add(2); // 重复添加，不会有任何效果
// 显示元素
Console.WriteLine("Elements in HashSet:");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
// 检查元素是否存在
if (numbers.Contains(2))
{
    Console.WriteLine("HashSet contains 2");
}
// 删除元素
numbers.Remove(2);
if (!numbers.Contains(2))
{
    Console.WriteLine("2 has been removed from the HashSet");
}
// 集合操作
HashSet<int> otherNumbers = new HashSet<int> { 3, 4, 5 };
numbers.UnionWith(otherNumbers); // 并集
Console.WriteLine("Union with {3, 4, 5}:");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
numbers.IntersectWith(new int[] { 3, 4 }); // 交集
Console.WriteLine("Intersection with {3, 4}:");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

#### 总结

`HashSet<T>` 是处理不需要重复元素的集合时的理想选择，特别是在需要快速查找和集合操作的场景中。它的主要优势是提供了高效的性能和自动的元素唯一性保证。

### SortedSet

`SortedSet<T>` 是一个基于红黑树的数据结构，用于存储不重复的元素，并且保持元素按照指定的比较器（或自然顺序）排序。这个类位于 `System.Collections.Generic` 命名空间中。`SortedSet<T>` 提供了一系列方法来操作有序集合，包括添加、删除、查找元素以及执行集合的并集、交集和差集等操作。

#### 特点

- **自动排序**：元素被自动排序，无需手动排序。
- **元素唯一性**：不允许重复的元素。
- **高效的查找操作**：由于基于红黑树，查找操作的时间复杂度为 O(log n)。
- **灵活的集合操作**：支持并集、交集和差集等操作。

#### 常用方法

- **Add(T item)**：向集合中添加一个元素，如果元素已存在，则不添加并返回 `false`。
- **Remove(T item)**：从集合中移除指定的元素，如果成功移除，则返回 `true`。
- **Contains(T item)**：检查集合中是否包含指定的元素。
- **Min** 和 **Max**：获取集合中的最小和最大元素。
- **UnionWith(IEnumerable<T> other)**：将另一个集合中的元素添加到当前集合中（并集）。
- **IntersectWith(IEnumerable<T> other)**：使当前集合仅包含同时存在于两个集合中的元素（交集）。
- **ExceptWith(IEnumerable<T> other)**：从当前集合中移除存在于另一个集合中的元素（差集）。
- **Clear()**：清空集合中的所有元素。
- **Count**：获取集合中元素的数量。

#### 示例代码

以下是一个使用 `SortedSet<T>` 的示例，展示了如何进行元素的添加、删除和集合操作。

```csharp
SortedSet<int> numbers = new SortedSet<int>();

// 添加元素
numbers.Add(3);
numbers.Add(1);
numbers.Add(4);
numbers.Add(1); // 重复添加，不会有任何效果

// 显示元素
Console.WriteLine("Elements in SortedSet:");
foreach (int number in numbers)
{
    Console.WriteLine(number); // 元素将按照升序显示
}

// 检查元素是否存在
if (numbers.Contains(3))
{
    Console.WriteLine("SortedSet contains 3");
}

// 删除元素
numbers.Remove(1);
if (!numbers.Contains(1))
{
    Console.WriteLine("1 has been removed from the SortedSet");
}

// 集合操作
SortedSet<int> otherNumbers = new SortedSet<int> { 2, 3, 5 };
numbers.UnionWith(otherNumbers); // 并集
Console.WriteLine("Union with {2, 3, 5}:");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

numbers.IntersectWith(new int[] { 2, 3 }); // 交集
Console.WriteLine("Intersection with {2, 3}:");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

#### 总结

`SortedSet<T>` 是处理需要排序且不重复元素的集合时的理想选择，特别是在需要保持元素有序的同时进行高效查找和集合操作的场景中。它结合了集合的灵活性和数组的排序功能，使其成为处理复杂数据集的强大工具。

### 哈希表格（Hashtable）

`Hashtable` 是 C# 中的一种非泛型集合类，用于存储键值对。它提供了一种快速查找的机制，通过将键映射到值来实现高效的数据访问。`Hashtable` 类位于 `System.Collections` 命名空间中。

#### 特点

- **键值对存储**：`Hashtable` 存储键值对，每个键都是唯一的。
- **快速查找**：通过哈希表实现，具有快速查找元素的能力。
- **动态大小**：`Hashtable` 的大小可以动态增长，根据需要自动调整。
- **无序**：元素在 `Hashtable` 中的存储是无序的，不保证任何特定的遍历顺序。

#### 常用方法

- **Add(object key, object value)**：向 `Hashtable` 中添加一个键值对。
- **Remove(object key)**：从 `Hashtable` 中移除指定键的键值对。
- **ContainsKey(object key)**：检查 `Hashtable` 中是否包含指定的键。
- **ContainsValue(object value)**：检查 `Hashtable` 中是否包含指定的值。
- **Clear()**：清空 `Hashtable` 中的所有键值对。
- **Count**：获取 `Hashtable` 中键值对的数量。

#### 示例代码

以下是一个使用 `Hashtable` 的示例，展示了如何添加、删除和查找键值对。

```csharp
Hashtable hashtable = new Hashtable();

// 添加键值对
hashtable.Add("A", 1);
hashtable.Add("B", 2);
hashtable.Add("C", 3);

// 显示所有键值对
foreach (DictionaryEntry entry in hashtable)
{
    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
}

// 检查键是否存在
if (hashtable.ContainsKey("B"))
{
    Console.WriteLine("Key 'B' exists in the Hashtable");
}

// 删除键值对
hashtable.Remove("B");

// 清空 Hashtable
hashtable.Clear();
Console.WriteLine($"Number of key-value pairs in Hashtable: {hashtable.Count}");
```

##### 总结

`Hashtable` 是一种经典的键值对集合，适用于需要快速查找和存储键值对的场景。它提供了一种高效的数据访问机制，通过哈希表实现快速的元素查找。在实际应用中，`Hashtable` 可以用于存储和管理各种类型的数据，提供了一种灵活且高效的数据结构。

### SortedList

`SortedList` 是 C# 中的一种有序集合类，用于存储键值对，并根据键的排序顺序进行排序。它实现了 `IDictionary` 和 `IList` 接口，可以按照键的顺序访问元素。`SortedList` 类位于 `System.Collections` 命名空间中。

#### 特点

- **有序存储**：`SortedList` 中的元素按照键的排序顺序进行存储。
- **键值对存储**：存储键值对，每个键都是唯一的。
- **快速查找**：通过二分查找实现快速查找元素。
- **动态大小**：`SortedList` 的大小可以动态增长，根据需要自动调整。

#### 常用方法

- **Add(key, value)**：向 `SortedList` 中添加一个键值对。
- **Remove(key)**：从 `SortedList` 中移除指定键的键值对。
- **ContainsKey(key)**：检查 `SortedList` 中是否包含指定的键。
- **ContainsValue(value)**：检查 `SortedList` 中是否包含指定的值。
- **Clear()**：清空 `SortedList` 中的所有键值对。
- **IndexOfKey(key)**：获取指定键的索引。
- **IndexOfValue(value)**：获取指定值的索引。
- **Count**：获取 `SortedList` 中键值对的数量。

#### 示例代码

以下是一个使用 `SortedList` 的示例，展示了如何添加、删除和查找键值对。

```csharp
SortedList sortedList = new SortedList();
// 添加键值对
sortedList.Add("B", 2);
sortedList.Add("A", 1);
sortedList.Add("C", 3);

// 显示所有键值对
foreach (DictionaryEntry entry in sortedList)
{
    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
}

// 检查键是否存在
if (sortedList.ContainsKey("B"))
{
    Console.WriteLine("Key 'B' exists in the SortedList");
}

// 删除键值对
sortedList.Remove("B");

// 清空 SortedList
sortedList.Clear();
Console.WriteLine($"Number of key-value pairs in SortedList: {sortedList.Count}");
```


#### 总结

`SortedList` 是一种有序的键值对集合，适用于需要按照键的顺序访问元素的场景。它提供了一种高效的有序存储和访问机制，通过二分查找实现快速的元素查找。在实际应用中，`SortedList` 可以用于按照键的顺序存储和管理数据，提供了一种有序且高效的数据结构。

### 链表（Queue）

`Queue` 是 C# 中的一种先进先出（FIFO）的集合类，用于存储对象并按照它们被添加的顺序进行访问。`Queue` 类位于 `System.Collections` 命名空间中。

#### 特点

- **先进先出**：`Queue` 中的元素按照它们被添加的顺序进行排列，最先添加的元素最先被访问。
- **动态大小**：`Queue` 的大小可以动态增长，根据需要自动调整。
- **高效操作**：`Queue` 提供了高效的入队（Enqueue）和出队（Dequeue）操作。

#### 常用方法

- **Enqueue(item)**：将元素添加到队列的末尾。
- **Dequeue()**：移除并返回队列开头的元素。
- **Peek()**：返回队列开头的元素，但不移除它。
- **Count**：获取队列中元素的数量。
- **Clear()**：清空队列中的所有元素。

#### 示例代码

以下是一个使用 `Queue` 的示例，展示了如何向队列中添加元素、移除元素以及获取队列中的元素数量。

```csharp
Queue queue = new Queue();

// 入队操作
queue.Enqueue("Apple");
queue.Enqueue("Banana");
queue.Enqueue("Cherry");

// 出队操作
string firstItem = (string)queue.Dequeue();
Console.WriteLine($"Dequeued item: {firstItem}");

// 查看队列开头的元素
string peekItem = (string)queue.Peek();
Console.WriteLine($"Peeked item: {peekItem}");

// 显示队列中的所有元素
Console.WriteLine("Elements in the queue:");
foreach (var item in queue)
{
    Console.WriteLine(item);
}

// 获取队列中元素的数量
Console.WriteLine($"Number of items in the queue: {queue.Count}");

// 清空队列
queue.Clear();
Console.WriteLine($"Number of items in the queue after clearing: {queue.Count}");
```


#### 总结

`Queue` 是一种常用的先进先出的集合类，适用于需要按照元素添加的顺序进行访问的场景。它提供了高效的入队和出队操作，可以用于实现队列数据结构。在实际应用中，`Queue` 可以用于处理需要按照先进先出顺序处理元素的情况，如任务调度、消息传递等。

### 链表（LinkedList）

`LinkedList` 是 C# 中的一种双向链表数据结构，用于存储和操作元素集合。每个节点都包含对前一个节点和后一个节点的引用，这使得在链表中插入、删除和移动元素更加高效。`LinkedList` 类位于 `System.Collections.Generic` 命名空间中。

#### 特点

- **双向链表**：每个节点都包含对前一个节点和后一个节点的引用，支持双向遍历。
- **动态大小**：`LinkedList` 的大小可以动态增长，根据需要自动调整。
- **高效操作**：插入、删除和移动元素的操作在链表中更加高效。

#### 常用方法

- **AddFirst(item)**：在链表的开头添加一个元素。
- **AddLast(item)**：在链表的末尾添加一个元素。
- **AddBefore(node, item)**：在指定节点之前插入一个元素。
- **AddAfter(node, item)**：在指定节点之后插入一个元素。
- **Remove(item)**：从链表中移除指定元素。
- **RemoveFirst()**：移除链表开头的元素。
- **RemoveLast()**：移除链表末尾的元素。
- **Clear()**：清空链表中的所有元素。
- **Count**：获取链表中元素的数量。

#### 示例代码

以下是一个使用 `LinkedList` 的示例，展示了如何向链表中添加元素、移除元素以及获取链表中的元素数量。

```csharp
LinkedList<string> linkedList = new LinkedList<string>();
// 在链表末尾添加元素
linkedList.AddLast("Apple");
linkedList.AddLast("Banana");

// 在链表开头添加元素
linkedList.AddFirst("Cherry");

// 在指定节点之后插入元素
LinkedListNode<string> node = linkedList.Find("Apple");
linkedList.AddAfter(node, "Orange");

// 移除指定元素
linkedList.Remove("Banana");

// 显示链表中的所有元素
Console.WriteLine("Elements in the linked list:");
foreach (var item in linkedList)
{
    Console.WriteLine(item);
}

// 获取链表中元素的数量
Console.WriteLine($"Number of items in the linked list: {linkedList.Count}");

// 清空链表
linkedList.Clear();
Console.WriteLine($"Number of items in the linked list after clearing: {linkedList.Count}");
```

#### 总结

`LinkedList` 是一种灵活且高效的数据结构，适用于需要频繁插入、删除和移动元素的场景。它提供了双向链表的特性，支持双向遍历和高效的元素操作。在实际应用中，`LinkedList` 可以用于实现各种数据结构和算法，如队列、栈、LRU缓存等。

### 链表（Stacks）

`Stack` 是 C# 中的一种后进先出（LIFO）的集合类，用于存储对象并按照后进先出的顺序进行访问。`Stack` 类位于 `System.Collections` 命名空间中。

#### 特点

- **后进先出**：`Stack` 中的元素按照后进先出的顺序进行排列，最后添加的元素最先被访问。
- **动态大小**：`Stack` 的大小可以动态增长，根据需要自动调整。
- **高效操作**：`Stack` 提供了高效的入栈（Push）和出栈（Pop）操作。

#### 常用方法

- **Push(item)**：将元素压入栈顶。
- **Pop()**：从栈顶弹出并返回元素。
- **Peek()**：返回栈顶的元素，但不移除它。
- **Clear()**：清空栈中的所有元素。
- **Count**：获取栈中元素的数量。

#### 示例代码

以下是一个使用 `Stack` 的示例，展示了如何向栈中压入元素、弹出元素以及获取栈中的元素数量。

```csharp
Stack stack = new Stack();

// 入栈操作
stack.Push("Apple");
stack.Push("Banana");
stack.Push("Cherry");

// 出栈操作
string topItem = (string)stack.Pop();
Console.WriteLine($"Popped item: {topItem}");

// 查看栈顶元素
string peekItem = (string)stack.Peek();
Console.WriteLine($"Peeked item: {peekItem}");

// 显示栈中的所有元素
Console.WriteLine("Elements in the stack:");
foreach (var item in stack)
{
    Console.WriteLine(item);
}

// 获取栈中元素的数量
Console.WriteLine($"Number of items in the stack: {stack.Count}");

// 清空栈
stack.Clear();
Console.WriteLine($"Number of items in the stack after clearing: {stack.Count}");
```

#### 总结

`Stack` 是一种常用的后进先出的集合类，适用于需要按照后进先出顺序处理元素的场景。它提供了高效的入栈和出栈操作，可以用于实现栈数据结构。在实际应用中，`Stack` 可以用于处理需要按照后进先出顺序处理元素的情况，如表达式求值、函数调用等。

# 8.依赖注入

依赖注入（Dependency Injection，简称 DI）是一种设计模式，用于减少组件之间的耦合度，并提高代码的可维护性、可测试性和可扩展性。它的基本思想是，将一个对象的依赖关系委托给外部容器来管理，而不是在对象内部直接创建或获取依赖对象。

**作用：**
1. **降低耦合度：** 通过将依赖关系从组件内部移动到外部容器中，使得组件不再直接依赖于具体的实现细节，而是依赖于抽象接口或基类，从而降低了组件之间的耦合度。
2. **提高可维护性：** 由于依赖关系是由外部容器管理的，因此可以更容易地修改、替换或升级依赖的实现，而不需要修改组件本身的代码。
3. **增强可测试性：** 通过依赖注入，我们可以轻松地替换真实的依赖对象为模拟对象，从而更方便地进行单元测试。
4. **促进代码重用和组件化：** 依赖注入使得组件更加独立和可复用，因为它们不再负责创建或获取依赖对象，而是通过外部容器来获取。

**好处：**
1. **灵活性：** 通过配置容器来管理对象之间的依赖关系，可以轻松地更改和配置对象的行为，而无需修改源代码。
2. **可测试性：** 依赖注入使得单元测试更加容易，因为我们可以轻松地用模拟对象替代真实的依赖对象。
3. **可维护性：** 通过降低组件之间的耦合度，依赖注入使得代码更加易于理解、修改和维护。
4. **可扩展性：** 依赖注入使得系统更加灵活和可扩展，可以轻松地添加新的功能或修改现有功能。

下面是一个简单的 C# 代码示例，演示了如何使用依赖注入：

```csharp
// 定义服务接口
public interface ILogger  
{  
    void Log(string message);  
}  
  
public class ConsoleLogger : ILogger  
{  
    public void Log(string message)  
    {  
        Console.WriteLine(message);  
    }  
}
// 需要使用 ILogger 服务的类
public class MyService  
{  
    private readonly ILogger _logger;  
  
    public MyService(ILogger logger)  
    {  
        _logger = logger;  
    }  
  
    public void DoSomething()  
    {  
        _logger.Log("Doing something...");  
        // ...其他业务逻辑...  
    }  
}
// 创建依赖注入容器
using Unity;  
class Program  
{  
    static void Main(string[] args)  
    {  
        // 创建Unity容器  
        var container = new UnityContainer();  
  
        // 注册接口和实现的映射关系  
        container.RegisterType<ILogger, ConsoleLogger>();  
  
        // 解析依赖关系，获取MyService的实例  
        var myService = container.Resolve<MyService>();  
  
        // 调用MyService的方法  
        myService.DoSomething();  
    }  
}
```

这段代码的作用和好处是：

- **解耦合**：`UserService` 不直接依赖于 `ConsoleLogger`，而是通过构造函数接收 `ILogger`，使得 `UserService` 和日志记录服务解耦合。
- **可测试性**：由于 `UserService` 依赖于抽象的 `ILogger` 接口，我们可以轻松地创建一个模拟日志记录服务用于测试 `UserService` 的行为。
- **可维护性**：通过依赖注入，代码的依赖关系变得清晰明确，易于理解和维护。
- **可扩展性**：如果我们需要更换日志记录服务的实现，只需修改依赖注入容器的配置即可，而不需要修改 `UserService` 类的代码。例如，我们可以轻松地替换 `ConsoleLogger` 为 `FileLogger` 或其他日志记录服务的实现。

# 9.配置文件读取写入

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

# 10.表达式树和IQueryable

### 1.1 表达式树

表达式树（Expression Trees）是表示代码中的Lambda表达式或LINQ查询的数据结构。它们允许你在运行时检查和操作代码的结构，这在某些高级场景中特别有用，比如动态查询构建、代码分析和转换等。

表达式树由`System.Linq.Expressions`命名空间中的类型表示。你可以使用`Expression`类及其子类来构建和操作表达式树。每个表达式树节点都是一个`Expression`类型的对象，它表示代码中的一个特定部分。

下面是一个简单的C#代码示例，展示了如何创建和使用表达式树：

```csharp
using System;  
using System.Linq.Expressions;  
  
class Program  
{  
    static void Main()  
    {  
        // 创建一个Lambda表达式：num => num * 2  
        Expression<Func<int, int>> lambda = num => num * 2;  
  
        // 输出Lambda表达式的文本表示  
        Console.WriteLine(lambda); // 输出：(num) => (num * 2)  
  
        // 获取Lambda表达式的参数和主体  
        ParameterExpression parameter = lambda.Parameters[0];  
        BinaryExpression body = (BinaryExpression)lambda.Body;  
  
        // 输出参数名称和类型  
        Console.WriteLine("Parameter Name: " + parameter.Name); // 输出：Parameter Name: num  
        Console.WriteLine("Parameter Type: " + parameter.Type); // 输出：Parameter Type: System.Int32  
  
        // 输出表达式的左操作数和右操作数  
        Console.WriteLine("Left Operand: " + body.Left); // 输出：Left Operand: num  
        Console.WriteLine("Right Operand: " + body.Right); // 输出：Right Operand: 2  
  
        // 执行Lambda表达式  
        Func<int, int> compiledLambda = lambda.Compile();  
        int result = compiledLambda(5); // 计算 5 * 2  
        Console.WriteLine("Result: " + result); // 输出：Result: 10  
    }  
}
```

在这个例子中，我们首先创建了一个Lambda表达式`num => num * 2`，并将其赋值给一个`Expression<Func<int, int>>`类型的变量`lambda`。然后，我们输出了这个Lambda表达式的文本表示。接着，我们访问了Lambda表达式的参数和主体，并输出了它们的信息。最后，我们使用`Compile`方法将Lambda表达式编译成一个可执行的委托（`Func<int, int>`），并调用这个委托来计算`5 * 2`的结果。

表达式树提供了一种灵活的方式来操作和转换代码，尤其是在构建动态查询或进行代码分析时非常有用。然而，它们通常比直接执行的代码慢，因为需要额外的解析和编译步骤。因此，在性能敏感的场景中应谨慎使用。

~~~c#
Expression<Func<string, bool>> expression = (s) => s.Contains("李") || s.Contains("张");
// 创建一个字符串列表
List<string> namesList = new List<string> { "张三", "Bob", "李四", "David", "Eve" };

// 将List转换为IQueryable
IQueryable<string> Names = namesList.AsQueryable();
var Result=Names.Where(expression);//张三-李四
~~~

### 1.2 IQueryable

`IQueryable<T>`接口是`LINQ`查询提供程序模型的一个核心组成部分，它允许`LINQ`查询被应用到数据源上。`IQueryable<T>`接口继承自`IEnumerable<T>`，并添加了对`LINQ`查询表达式的支持，这些表达式可以被转换成数据源可以理解的查询命令。

`IQueryable<T>`的主要特点是它允许延迟执行（deferred execution），这意味着当你编写LINQ查询时，实际上并没有立即执行查询。相反，查询的执行被推迟到你真正需要数据的时候，比如当你开始遍历结果或者调用如`.ToList()`、`.First()`等方法时。

下面是一个关于`IQueryable<T>`的简单代码示例：

```csharp
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Linq.Expressions;  
  
// 假设我们有一个简单的数据模型  
public class Product  
{  
    public int Id { get; set; }  
    public string Name { get; set; }  
    public decimal Price { get; set; }  
}  
  
// 假设这是我们的数据源，它实现了IQueryable<T>接口  
public class QueryableDataSource : IQueryable<Product>  
{  
    private List<Product> _products = new List<Product>  
    {  
        new Product { Id = 1, Name = "Product1", Price = 99.99M },  
        new Product { Id = 2, Name = "Product2", Price = 199.99M },  
        // ... 更多产品数据  
    };  
  
    public Expression Expression => _products.AsQueryable().Expression;  
  
    public Type ElementType => typeof(Product);  
  
    public IQueryProvider Provider => new QueryProvider(_products);  
  
    public IEnumerator<Product> GetEnumerator()  
    {  
        return _products.GetEnumerator();  
    }  
  
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()  
    {  
        return _products.GetEnumerator();  
    }  
}  
  
// 这是一个简单的查询提供程序，用于执行查询  
public class QueryProvider : IQueryProvider  
{  
    private readonly List<Product> _products;  
  
    public QueryProvider(List<Product> products)  
    {  
        _products = products;  
    }  
  
    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)  
    {  
        return new QueryableDataSource(_products.AsQueryable<Product>().Provider.CreateQuery<Product>(expression)) as IQueryable<TElement>;  
    }  
  
    public IQueryable CreateQuery(Expression expression)  
    {  
        return CreateQuery<Product>(expression);  
    }  
  
    public TResult Execute<TResult>(Expression expression)  
    {  
        return _products.AsQueryable().Provider.Execute<TResult>(expression);  
    }  
  
    public object Execute(Expression expression)  
    {  
        return Execute<object>(expression);  
    }  
}  
  
// 这是我们的主程序入口点，展示了如何使用IQueryable<T>接口进行查询操作。  
class Program  
{  
    static void Main(string[] args)  
    {  
        QueryableDataSource dataSource = new QueryableDataSource();  
          
        // 使用LINQ对IQueryable<T>数据源进行查询操作，此时查询并未真正执行。  
        var query = from product in dataSource  
                    where product.Price > 100M  
                    select product;  
                      
        // 当我们遍历结果或者调用ToList等方法时，查询才会真正被执行。  
        foreach (var product in query)  
        {  
            Console.WriteLine($"Product Name: {product.Name}, Price: {product.Price}");  
        }  
    }  
}
```

**注意**：这个示例是为了展示`IQueryable<T>`接口的基本使用而简化的，并不完全反映一个真实的查询提供程序的复杂性。在实际应用中，查询提供程序需要能够解析和处理各种LINQ表达式，并将其转换成对应数据源可以理解的查询命令。例如，在使用Entity Framework等ORM时，`IQueryable<T>`接口背后的查询提供程序会将LINQ查询转换成SQL语句来执行。

另外，请注意，在上面的代码中，我们假设了`QueryableDataSource`类内部有一个产品列表作为数据源，并且实现了一个简单的`QueryProvider`类来模拟查询的执行。在实际应用中，这些数据通常来自数据库或其他外部数据源，并且查询提供程序会更加复杂。在这个例子中，我们只是简单地调用了内部列表的`AsQueryable()`方法来利用LINQ to Objects的功能。























Hello Edit
