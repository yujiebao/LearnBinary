# 1.各种类型数据转换为二进制

## 1.各类型数据和字节数据相互转换

### BitConverter 

```c#
//1.将各类型数据转换为字节
        byte[] bytes = BitConverter.GetBytes(123);
//2.将字节转换为各类型数据
        int num = BitConverter.ToInt32(bytes, 0);  //第二个代表bytes 的索引位置
```

## 2.标准编码格式

编码是用预先规定的方法将文字、数字或其它对象编成数码，或将信息、数据转换成规定的电脉冲信号。

为保证编码的正确性，编码要规范化、标准化，即需有标准的编码格式。

常见的编码格式有ASCII、ANSI、GBK、GB2312、UTF-8、GB18038和UNICODE等。

游戏开发中常用编码格式 UTF-8
中文相关编码格式 GBK
英文相关编码格式 ASCII

类名:Encoding
需要引用命名空间:using system.Text;
1.将字符串以指定编码格式转字节

```C#
// 1.将字符串以指定编码格式转字节
byte[] bytes1 = Encoding.UTF8.GetBytes("你好啊");

//2.将字节以指定编码格式转字符串
string str = Encoding.UTF8.GetString(bytes1,0,bytes1.Length);  //第二个参数代表字节数组的起始索引位置，第三个参数代表读取的长度
        
```

## 3.总结

我们可以通过Bitconverter和Encoding类

将所有c#提供给我们的数据类型和字节数组之间进行相互转换了

我们需要熟练掌握其中的API

# 2.文件操作相关

## 1.文件相关操作公共类

C#提供了一个名为File(文件)的公共类

让我们可以快捷的通过代码操作文件相        

类名:File

命名空间: system.IO

## 2.文件操作File类的常用方法 

```C#
//1.判断文件是否存在
// File.Exists("文件路径")  //返回bool值 true/false
        
//2.创建文件
// FileStream fs = File.Create(Application.persistentDataPath + "/test.bao"); //返回FileStream对象

//3.写入文件
//将指定字节数组 写入到指定路径的文件中
byte[] bytes = BitConverter.GetBytes(999); //将数字转换为字节数组
File.WriteAllBytes(Application.persistentDataPath + "/test.bao", bytes); //将字节数组写入到文件中
//将指定的string数组内容 一行行写入到指定路径中
string[] strs = new string[3] { "1616", "274", "73" };
File.WriteAllLines(Application.persistentDataPath + "/test.bao1", strs); //将字符串数组写入到文件中
//将指定的字符串写入到指定路径中
File.WriteAllText(Application.persistentDataPath + "/test.bao2", "你好啊\ndasdasdsa");   //支持换行

//4.读取文件
//读取字节数据
bytes =  File.ReadAllBytes(Application.persistentDataPath + "/test.bao");
print(BitConverter.ToInt32(bytes, 0)); //将字节数组转换为int类型
//读取所有行
strs =File.ReadAllLines(Application.persistentDataPath + "/test.bao1"); //将文件中的所有行读取到字符串数组中
for (int i = 0; i < strs.Length; i++)
{
	print(strs[i]); //打印每一行的内容
}
//读取文本内容
string str = File.ReadAllText(Application.persistentDataPath + "/test.bao2"); //将文件中的所有内容读取到字符串中
print(str);

//5.删除文件
//注意:如果删除打开的文件会报错
File.Delete(Application.persistentDataPath + "/test.bao");

//6.复制文件
//参数一:现有文件 需要是流关闭状态
//参数二:目标文件
//参数三:是否覆盖目标文件
File.Copy(Application.persistentDataPath + "/test.bao1", Application.persistentDataPath + "/test.bao1.copy",true);  //如果目标文件已经储存在,不设置是否覆盖就会报错

//7.文件替换
//参数一:用来替换的路径
//参数二:被替换的路径
//参数三:备份路径  把第二个参数的文件备份
File.Replace(Application.persistentDataPath + "/test.bao1.copy", Application.persistentDataPath + "/test.bao2", Application.persistentDataPath + "/test.bao1.copy.bak");

//8.以流的形式 打开文件并写入或读取
//参数一:文件路径
//参数二:打开模式   打开/打开或创建
//参数三::访问模式  读/写/读写
//参数四:共享模式
//FileStream fs = File.Open(Application.persistentDataPath + "/test.bao1", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        
```

# 3.文件流

## 1.什么是文件流

​        在C#中提供了一个文件流类 FileStream类

​        它主要作用是用于读写文件的细节

​        我们之前学过的File只能整体读写文件

​        而FileStream可以以读写字节的形式处理文件

 

​        说人话:

​        文件里面存储的数据就像是一条数据流(数组或者列表)

​        我们可以通过FileStream一部分一部分的读写数据流

​        比如我可以先存一个int(4个字节)再存一个bool(1个字节)再存一个string(n个字节)

​        利用FileStream可以以流式逐个读写

## 2.FileStream文件流类常用的方法

### 1.打开或创建指定的文件 

```C#
//方法一: new FileStream
        //参数一:文件路径   
        //参数二:打开文件的方式
        //FileMode.Create:创建一个新文件,如果文件存在则覆盖
        //FileMode.CreateNew:创建一个新文件,如果文件存在则报错
        //FileMode.Open:打开一个已存在的文件,如果文件不存在则报错
        //FileMode.OpenOrCreate:打开一个已存在的文件,如果文件不存在则创建   
        //FileMode.Append:打开一个已存在的文件,如果文件不存在则创建,并且在文件末尾添加数据
        //FileMode.Truncate:打开一个已存在的文件,如果文件不存在则报错,并且清空文件内容
        //参数三:访问模式
        //参数四:共享权限
        //Read:允许别的程序读当前文件
        //Write:允许别的程序写当前文件
        //ReadWrite:允许别的程序读写当前文件
        //None:不共享
        FileStream fs = new FileStream(Application.dataPath + "/Lesson3.bao", FileMode.Create,FileAccess.ReadWrite,FileShare.None);

        //方法二 File.Create
        //参数一:文件路径
        //参数二:缓存大小
        //参数三:描述如何创建或覆盖该文件
        //Asynchronous 可用于异步读写
        //DeleteOnClose 不在使用时，自动删除
        //Encrypted 加密
        //None 不应用其它选项
        //RandomAccess 随机访问文件
        //SequentialScan 从头到尾顺序访问文件
        //WriteThrough 通过中间缓存直接写入磁盘
		FileStream fs2 = File.Create(Application.dataPath + "/Lesson3.bao1",2048,FileOptions.None);

        //方法三: File.Open
        //参数一:文件路径
        //参数二:打开文件的方式
        //参数三:访问模式
        //参数四:共享权限
        FileStream fs3 = File.Open(Application.dataPath + "/Lesson3.bao2",FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.None);
        
```

### 2.重要属性和方法

```C#
        FileStream fs = File.Open(Application.dataPath + "/Lesson3.bao", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

        //文件的长度
        Debug.Log(fs.Length);

        //是否可写可读
        Debug.Log("CanWrite:" + fs .CanWrite + " CanRead:" + fs.CanRead);

        //将字节写入文件 当写入后一定要执行一次该方法  避免数据丢失
        fs.Flush();

        //关闭文件流 释放资源  写完后一定要执行一次
        fs.Close();

        //缓存资源销毁回收
        fs.Dispose();

```

### 3.写入字节 

```C#
FileStream fs = new FileStream(Application.persistentDataPath + "/Lesson.bao", FileMode.OpenOrCreate, FileAccess.Write);
byte[] bytes = BitConverter.GetBytes(999);
//方法：write
//参数一:写入的字节数组
//参数二:写入字节数组的起始位置
//参数三:写入字节数组的长度       参数二和参数三可以不写,默认从0开始写入所有字节 但是当一个大的Byte数组写入时,可能就要使用参数二和参数三了
fs.Write(bytes, 0, bytes.Length); 

//写入字符串时   先写入字符串的长度  不然无法确定字符串的长度
bytes = Encoding.UTF8.GetBytes("你好啊"); 
//先写入长度
fs.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int)); 
//再写入字符串的具体内容
fs.Write(bytes, 0, bytes.Length);

//避免数据丢失 一定要写入后执行的方法
fs.Flush();
//关闭文件流 释放资源 
fs.Close();
fs.Dispose();
```

### 4.读取字节

#### 方法一:挨个读取字节数组 

```C#
FileStream fs2 = File.Open(Application.persistentDataPath + "/Lesson.bao", FileMode.Open, FileAccess.Read);
//读取第一个整型
byte[] bytes2 = new byte[sizeof(int)]; 
//参数一:用于存储读取的字节数组的容器
//参数二:容器中开始的位置
//参数三:读取多少个字节装入容器
//返回值:当前流索引前进了几个位置  
int index = fs2.Read(bytes2, 0, sizeof(int));
print(BitConverter.ToInt32(bytes2, 0));
print("索引值移动了:" + index); 
//读取第二个字符串
index = fs2.Read(bytes2, 0, sizeof(int));
print(BitConverter.ToInt32(bytes2, 0)); 
byte[] bytes3 = new byte[BitConverter.ToInt32(bytes2, 0)]; //根据长度创建字节数组
index = fs2.Read(bytes3, 0, BitConverter.ToInt32(bytes2, 0));
print(Encoding.UTF8.GetString(bytes3)); 
print("索引值移动了:" + index);
//读取字符串字节数组长度

//关闭文件流 释放资源 
fs2.Close();
fs2.Dispose();
```

#### 方法二:一次性读取再挨个读取

  ```C#
print("===================================");
FileStream fs3 = File.Open(Application.persistentDataPath + "/Lesson.bao", FileMode.Open, FileAccess.Read);
byte[] bytes4 = new byte[fs3.Length]; 
fs3.Read(bytes4, 0, (int)fs3.Length);   //读取全部字节
fs3.Dispose(); 
//读取整数
print(BitConverter.ToInt32(bytes4, 0));   // ToInt32只会读取一个整数  也就是只读四个字节
print("===================================");
int Length = BitConverter.ToInt32(bytes4, sizeof(int)); //读取字符串的长度
//读取字符串
string str = Encoding.UTF8.GetString(bytes4, sizeof(int) * 2, Length); //从第8个字节开始读取字符串  也就是从第二个整数的后面开始读取
print(str);
  ```

## 3.更加安全的使用文件流对象

```C#
//using关键字重要用法
//using(申明一个引用对象)
//{
//   使用对象的操作
//}
//无论发生什么情况 当using语句块结束后
//会自动调用该对象的销毁方法 避免忘记销毁或关闭流
//using是一种更安全的使用方法

//强调:
//目前我们对文件流进行操作 为了文件操作安全 都用using来进行处理最好
        
//如更改知识点二中读文件中的方法二
// FileStream fs3 = File.Open(Application.persistentDataPath + "/Lesson.bao", FileMode.Open, FileAccess.Read);
using(FileStream fs3 = File.Open(Application.persistentDataPath + "/Lesson.bao", FileMode.Open, FileAccess.Read))
{
	byte[] bytes4 = new byte[fs3.Length]; 
	fs3.Read(bytes4, 0, (int)fs3.Length);   //读取全部字节
	//读取整数
	print(BitConverter.ToInt32(bytes4, 0));   // ToInt32只会读取一个整数  也就是只读四个字节
	print("===================================");
	int Length = BitConverter.ToInt32(bytes4, sizeof(int)); //读取字符串的长度
	//读取字符串
	string str = Encoding.UTF8.GetString(bytes4, sizeof(int) * 2, Length); //从第8个字节开始读取字符串  也就是从第二个整数的后面开始读取
	print(str);
}
	FileStream fs2 = File.Open(Application.persistentDataPath + "/Lesson.bao", FileMode.Open, FileAccess.Read);
//读取第一个整型
byte[] bytes2 = new byte[sizeof(int)]; 
//参数一:用于存储读取的字节数组的容器
//参数二:容器中开始的位置
//参数三:读取多少个字节装入容器
//返回值:当前流索引前进了几个位置  
int index = fs2.Read(bytes2, 0, sizeof(int));
print(BitConverter.ToInt32(bytes2, 0));
print("索引值移动了:" + index); 
//读取第二个字符串
index = fs2.Read(bytes2, 0, sizeof(int));
print(BitConverter.ToInt32(bytes2, 0)); 
byte[] bytes3 = new byte[BitConverter.ToInt32(bytes2, 0)]; //根据长度创建字节数组
index = fs2.Read(bytes3, 0, BitConverter.ToInt32(bytes2, 0));
print(Encoding.UTF8.GetString(bytes3)); 
print("索引值移动了:" + index);
```



# 4.文件夹

## 1.C#提供给我们的文件夹操作公共类

类名:Directory

命名空间:using system.IO

```C#
// 1.判断文件夹是否存在
        if(Directory.Exists(Application.persistentDataPath + "/数据持久化4"))
        {
            print("文件夹存在");
        }
        else
        {
            print("文件夹不存在");
        }
        //2.创建文件夹
        DirectoryInfo info =  Directory.CreateDirectory(Application.persistentDataPath + "/数据持久化4");

        //3.删除文件夹
        //参数一 :路径
        //参数二: 是否删除非空目录，如果为true,则删除非空目录 如果为false,仅当目录为空时才删除(不为空就报错)
        // Directory.Delete(Application.persistentDataPath + "/数据持久化4", true);

        //4.查找文件夹和文件
        //得到指定路径下所有的文件夹名
        string[] s = Directory.GetDirectories(Application.persistentDataPath);  
        foreach (string str in s)
        {
            print(str);
        }
        s = Directory.GetFiles(Application.persistentDataPath);
        foreach (string str in s)
        {
            print(str);
        }

        //5.移动文件夹
        // Directory.Move(Application.persistentDataPath + "/数据持久化4", Application.persistentDataPath + "/数据持久化5"); 
        //第二个文件的路径必须不存在 存在会报错      
        //复制的时候会把文件夹中的文件也复制到新的文件夹中
```

## 2.DirectoryInfo和FileInfo 



1.DirectoryInfo日录信息类  我们可以通过它获取文件夹的更多信息

  它主要出现在两个地方

```csharp
//1.创建文件夹方法的返回值
        DirectoryInfo info1 = Directory.CreateDirectory(Application.persistentDataPath + "/数据持久化1");
        print(info1.FullName);
        print(info1.Name);  

        //2.查找上级文件夹的信息
        info1 = Directory.GetParent(Application.persistentDataPath + "/数据持久化1");
        print(info1.FullName);
        print(info1.Name);

        //重要方法
        //得到所有子文件夹的目录信息
        DirectoryInfo[] directoryInfos = info1.GetDirectories();

```

2.FileInfo文件信息类        我们可以通过它获取文件的更多信息

```csharp
FileInfo[] info2 = info1.GetFiles();
        foreach (FileInfo fileInfo in info2)
        {
            print(fileInfo.FullName);
            print(fileInfo.Name);
            fileInfo.Open(FileMode.Open, FileAccess.Read); //打开文件流
        }
```

## 3.总结

Directory提供给我们了常用的文件夹相关操作的API    只需要熟练使用它即可

DinectoryInfo和FileInfo 一般在多文件夹和多文件操作时会用到          了解即可

日前用的相对较少 他们的用法和Directory和File类的用法大同小

# 5.对象的序列化

## 1.序列化类对象的第一步 声明类对象

注意:如果要使用c#自带的序列化2进制方法

申明类时需要添加[system.serializable]特性

## 2.序列化类对象第二步-将对象进行2进制序列化

### 方法一:使用内存流得到2进制字节数组 

主要用于得到字节数组 可以用于网络传输

新知识点

1.内存流对象

类名:Memorystream

命名空间:system.IO

2.2进制格式化对象

类名:BinaryFormatter

命名空间:System.Runtime.Serialization.Formatters.Binary.

主要方法:序列化方法 Serialize

```csharp
using(MemoryStream ms = new MemoryStream())
{
	BinaryFormatter bf = new BinaryFormatter();
	bf.Serialize(ms, p);   //序列化对象 生成2进制字节数组 写入到内存流中
	byte[] bytes = ms.GetBuffer();
	//bytes就是序列化后的字节数组 从内存流中获取
            
	//存储字节
	File.WriteAllBytes(Application.dataPath + "/Preson.bytes", bytes);

	ms.Close();
}
//先在内存流中写入序列化后的数据 再从内存流中获取序列化数据 存储到指定位置
```

### 方法二:使用文件流进行存储

主要用于存储到文件中

```C#
using(FileStream fs = new FileStream(Application.dataPath + "/Preson_2.bytes", FileMode.OpenOrCreate,    
FileAccess.Write))
{
	//2进制格式化程序
	BinaryFormatter bf = new BinaryFormatter();
	//程序化对象 生成2进制字节数组 写入到内存流中
	bf.Serialize(fs, p);   //序列化对象 生成2进制字节数组 写入到内存流中
	fs.Close();
}
//直接使用FileStream将对象序列化后的二进制数据写入到文件中
```

# 6.对象的反序列化

## 1.反序列化之 反序列化文件中的数据

主要类

FileStream文件流类

BinaryFormatter 2进制格式化类

主要方法

Deserizlize

```C#
using(FileStream fs = File.Open(Application.dataPath + "/Preson_2.bytes",
FileMode.Open,
FileAccess.Read))
{
BinaryFormatter bf = new BinaryFormatter();
Preson p = bf.Deserialize(fs) as Preson;
print(p);
}
```

## 2.反序列化之 反序列化网络传输过来的2进制数据

主要类

MemoryStream内存流类

BinaryFormatter 2进制格式化类

主要方法

Deserizlize

目前没有网络传输 我们还是直接从文件中获取

```C#
byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Preson.bytes");  //模拟网络传输过来的2进制数据
//声明内存流对象 一开始就把字节数组传输进去
using(MemoryStream ms = new MemoryStream(bytes))
{
	BinaryFormatter bf = new BinaryFormatter();
	Preson p = bf.Deserialize(ms) as Preson;
	print(p);
}
```

# 7.二进制数据加密

## 1.何时加密? 何时解密

当我们将类对象转换为2进制数据时进行加密

当我们将2进制数据转换为类对象时进行解密

## 2.简单的例子

```C#
Preson p = new Preson();
byte key = 199; //加密秘钥
using(MemoryStream mf = new MemoryStream())
{
	BinaryFormatter bf = new BinaryFormatter();
	bf.Serialize(mf, p);
	byte[] bytes = mf.GetBuffer();
	//异或加密
	for(int i = 0; i < bytes.Length; i++)
	{
		bytes[i] ^= key; //异或加密
	}
		File.WriteAllBytes(Application.dataPath + "/Lesson7.bao", bytes);
}

byte[] bytes1 = File.ReadAllBytes(Application.dataPath + "/Lesson7.bao");
for(int i = 0; i < bytes1.Length; i++)
{
	bytes1[i] ^= key; //异或解密
}
using(MemoryStream mf = new MemoryStream(bytes1))
	{
	BinaryFormatter bf = new BinaryFormatter();
	Preson p1 = bf.Deserialize(mf) as Preson;
	print(p1);
}
```

