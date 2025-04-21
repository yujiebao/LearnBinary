using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Lesson4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 文件夹操作是指什么
        //平时我们可以在操作系统的文件管理系统中
        //通过一些操作增删查改文件夹
        
        //我们目前要学习的就是通过代码的形式
        //来对文件夹进行增删查改的操作
        #endregion

        #region 知识点二 C#提供给我们的文件夹操作公共类
        // 类名:Directory
        // 命名空间:using system.IO
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
        #endregion

        #region 知识点三 DirectoryInfo和FileInfo
        // DirectoryInfo日录信息类
        // 我们可以通过它获取文件夹的更多信息
        // 它主要出现在两个地方
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

        // FileInfo文件信息类
        // 我们可以通过它获取文件的更多信息
        FileInfo[] info2 = info1.GetFiles();
        foreach (FileInfo fileInfo in info2)
        {
            print(fileInfo.FullName);
            print(fileInfo.Name);
            fileInfo.Open(FileMode.Open, FileAccess.Read); //打开文件流
        }
        #endregion

        #region 总结
        // Directory提供给我们了常用的文件夹相关操作的API
        // 只需要熟练使用它即可
        
        // DinectoryInfo和FileInfo 一般在多文件夹和多文件操作时会用到
        // 了解即可
        // 日前用的相对较少 他们的用法和Directory和File类的用法大同小异
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
