using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是文件流
        // 在C#中提供了一个文件流类 FileStream类
        // 它主要作用是用于读写文件的细节
        // 我们之前学过的File只能整体读写文件
        // 而FileStream可以以读写字节的形式处理文件

        // 说人话:
        // 文件里面存储的数据就像是一条数据流(数组或者列表)
        // 我们可以通过FileStream一部分一部分的读写数据流
        // 比如我可以先存一个int(4个字节)再存一个bool(1个字节)再存一个string(n个字节)
        // 利用FileStream可以以流式逐个读写
        #endregion

        #region 知识点二 FileStream文件流类常用的方法
        //类名:FileStream
        //需要引用命名空间 :System.IO

        #region 1.打开或创建指定的文件
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
        // FileStream fs = new FileStream(Application.dataPath + "/Lesson3.bao", FileMode.Create,FileAccess.ReadWrite,FileShare.None);

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
        // FileStream fs2 = File.Create(Application.dataPath + "/Lesson3.bao1",2048,FileOptions.None);

        //方法三: File.Open
        //参数一:文件路径
        //参数二:打开文件的方式
        //参数三:访问模式
        //参数四:共享权限
        // FileStream fs3 = File.Open(Application.dataPath + "/Lesson3.bao2",FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.None);
        #endregion
        
        #region 2.重要属性和方法
        // FileStream fs = File.Open(Application.dataPath + "/Lesson3.bao", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

        //文件的长度
        // Debug.Log(fs.Length);
        //是否可写可读
        // Debug.Log("CanWrite:" + fs .CanWrite + " CanRead:" + fs.CanRead);
        //将字节写入文件 当写入后一定要执行一次该方法  避免数据丢失
        // fs.Flush();
        //关闭文件流 释放资源  写完后一定要执行一次
        // fs.Close();
        //缓存资源销毁回收
        // fs.Dispose();
        #endregion
        
        #region 3.写入字节
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
        #endregion
        
        #region 4.读取字节
        #region 方法一:挨个读取字节数组
        //  FileStream fs2 = File.Open(Application.persistentDataPath + "/Lesson.bao", FileMode.Open, FileAccess.Read);
        // //读取第一个整型
        // byte[] bytes2 = new byte[sizeof(int)]; 
        // //参数一:用于存储读取的字节数组的容器
        // //参数二:容器中开始的位置
        // //参数三:读取多少个字节装入容器
        // //返回值:当前流索引前进了几个位置  
        // int index = fs2.Read(bytes2, 0, sizeof(int));
        // print(BitConverter.ToInt32(bytes2, 0));
        // print("索引值移动了:" + index); 
        // //读取第二个字符串
        // index = fs2.Read(bytes2, 0, sizeof(int));
        // print(BitConverter.ToInt32(bytes2, 0)); 
        // byte[] bytes3 = new byte[BitConverter.ToInt32(bytes2, 0)]; //根据长度创建字节数组
        // index = fs2.Read(bytes3, 0, BitConverter.ToInt32(bytes2, 0));
        // print(Encoding.UTF8.GetString(bytes3)); 
        // print("索引值移动了:" + index);
        //读取字符串字节数组长度

        //关闭文件流 释放资源 
        // fs2.Close();
        // fs2.Dispose();
        #endregion
        
        #region 方法二:一次性读取再挨个读取
        // print("===================================");
        // FileStream fs3 = File.Open(Application.persistentDataPath + "/Lesson.bao", FileMode.Open, FileAccess.Read);
        // byte[] bytes4 = new byte[fs3.Length]; 
        // fs3.Read(bytes4, 0, (int)fs3.Length);   //读取全部字节
        // fs3.Dispose(); 
        // //读取整数
        // print(BitConverter.ToInt32(bytes4, 0));   // ToInt32只会读取一个整数  也就是只读四个字节
        // print("===================================");
        // int Length = BitConverter.ToInt32(bytes4, sizeof(int)); //读取字符串的长度
        // //读取字符串
        // string str = Encoding.UTF8.GetString(bytes4, sizeof(int) * 2, Length); //从第8个字节开始读取字符串  也就是从第二个整数的后面开始读取
        // print(str);
        #endregion
        #endregion
        #endregion
   
        #region 知识点三 更加安全的使用文件流对象
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
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
