using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Lesson2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 代码中的文件操作是做什么 
        //在电脑上我们可以在操作系统中创建删除修改文件
        //可以增删查改各种各样的文件类型
        
        //代码中的文件操作就是通过代码来做这些事情了
        #endregion

        #region 知识点二 文件相关操作公共类
        // C#提供了一个名为File(文件)的公共类
        // 让我们可以快捷的通过代码操作文件相关
        
        // 类名:File
        // 命名空间: system.IO
        #endregion

        #region 知识点三 文件操作File类的常用方法
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
        #endregion

        #region 总结
        //一般情况下想要整体读写内容 可以使用File提供的write和Read相关功能
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
