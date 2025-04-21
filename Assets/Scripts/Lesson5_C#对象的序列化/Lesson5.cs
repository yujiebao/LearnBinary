using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[System.Serializable]
public class Preson
{
    public int age = 18;
    public string name = "唐老师";
    public int[] ids = new int[]{1,2,3,4,5};
    public List<int> list = new List<int>(){1,2,3,4,5};
    public Dictionary<int, string> dic = new Dictionary<int, string>()
    {
        {1,"唐老师1"},
        {2,"唐老师2"},
        {3,"唐老师3"},
        {4,"唐老师4"},
        {5,"唐老师5"}
    };
    StructTest structTest = new StructTest(18, "唐老师");
    classTest classTest = new classTest(198, "唐老师7");

    public override string ToString()
    {
        // 初始化一个StringBuilder来构建字符串
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        // 添加基本信息
        sb.AppendLine($"Age: {age}");
        sb.AppendLine($"Name: {name}");

        // 添加数组信息
        sb.Append("Ids: ");
        sb.AppendLine(string.Join(", ", ids));

        // 添加列表信息
        sb.Append("List: ");
        sb.AppendLine(string.Join(", ", list));

        // 添加字典信息
        sb.AppendLine("Dictionary:");
        foreach (var kvp in dic)
        {
            sb.AppendLine($"  Key: {kvp.Key}, Value: {kvp.Value}");
        }

        // 添加结构体信息
        sb.AppendLine($"StructTest - Age: {structTest.age}, Name: {structTest.name}");

        // 添加类信息
        sb.AppendLine($"classTest - Age: {classTest.age}, Name: {classTest.name}");

        // 返回构建好的字符串
        return sb.ToString();
    }
}

[Serializable]
public struct StructTest
{
    public int age;
    public string name;
    public StructTest(int age, string name)
    {
        this.age = age;
        this.name = name;
    }
}

[Serializable]
public class classTest
{
    public int age;
    public string name;
    public classTest(int age, string name)
    {
        this.age = age;
        this.name = name;
    }
}
public class Lesson5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 序列化类对象的第一步 声明类对象
        // 注意:如果要使用c#自带的序列化2进制方法
        // 申明类时需要添加[system.serializable]特性
        Preson p = new Preson();
        #endregion

        #region 知识点二 序列化类对象第二步-将对象进行2进制序列化
        // 方法一:使用内存流得到2进制字节数组
        // 主要用于得到字节数组 可以用于网络传输
        // 新知识点
        // 1.内存流对象
        // 类名:Memorystream
        // 命名空间:system.IO
        // 2.2进制格式化对象
        // 类名:BinaryFormatter
        // 命名空间:System.Runtime.Serialization.Formatters.Binary.
        // 主要方法:序列化方法 Serialize
        using(MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, p);   //序列化对象 生成2进制字节数组 写入到内存流中
            byte[] bytes = ms.GetBuffer();
            //bytes就是序列化后的字节数组 从内存流中获取
            
            //存储字节
            File.WriteAllBytes(Application.dataPath + "/Preson.bytes", bytes);
            print(Application.dataPath + "/Preson.bytes");

            ms.Close();
        }

        //方法二:使用文件流进行存储
        // 主要用于存储到文件中
        using(FileStream fs = new FileStream(Application.dataPath + "/Preson_2.bytes", FileMode.OpenOrCreate,FileAccess.Write))
        {
            //2进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            //程序化对象 生成2进制字节数组 写入到内存流中
            bf.Serialize(fs, p);   //序列化对象 生成2进制字节数组 写入到内存流中
            fs.Close();
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
