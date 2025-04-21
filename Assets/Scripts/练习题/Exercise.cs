using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
public class Student
{
    public int age;
    public string name;
    public int number;
    public bool sex;   

    public void Save(string fileName)
    {
        if(!Directory.Exists(Application.persistentDataPath + "/Student"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Student");
        }
        Debug.Log("保存数据到" + Application.persistentDataPath + "/Student/" + fileName);
        using(FileStream fs = File.Open(Application.persistentDataPath + "/Student/" + fileName+".bao",FileMode.OpenOrCreate,FileAccess.ReadWrite))
        {
           byte[] bytes = BitConverter.GetBytes(age);
           fs.Write(bytes,0,bytes.Length); 
           bytes = Encoding.UTF8.GetBytes(name);
           //存储字符串数组的长度
           fs.Write(BitConverter.GetBytes(bytes.Length),0,sizeof(int));
           //写入字符串数组的内容
           fs.Write(bytes,0,bytes.Length);
           bytes = BitConverter.GetBytes(number);
           fs.Write(bytes,0,bytes.Length);
           bytes = BitConverter.GetBytes(sex);
           fs.Write(bytes,0,bytes.Length);

           fs.Flush();
           fs.Close();
       }
    }

    public static Student Load(string fileName)
    {
        if(File.Exists(Application.persistentDataPath + "/Student/" + fileName+".bao") == false)
        {
            Debug.LogWarning("文件不存在");
            return null;
        }

        Student s = new Student();
        using(FileStream fs = File.Open(Application.persistentDataPath + "/Student/" + fileName+".bao",FileMode.Open,FileAccess.Read))
        {
            int index = 0;
            //读取文件的长度
            //把我们文件中的字节 全部读取出来
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes,0,bytes.Length);
            fs.Close();

            //挨个读取内容
            s.age = BitConverter.ToInt32(bytes,index); //读取第一个整型
            index += sizeof(int);
            //读取第二个字符串的长度
            int Length = BitConverter.ToInt32(bytes,sizeof(int));   //在存入的时候存入的就是字节长度(而不是字符串长度) 所以直接使用就行
            index += sizeof(int);
            //根据长度创建字节数组
            s.name = Encoding.UTF8.GetString(bytes, index,Length);
            index += Length;

            s.number = BitConverter.ToInt32(bytes, index); //读取第三个整型
            index += sizeof(int);
            s.sex = BitConverter.ToBoolean(bytes, index);
            index += sizeof(bool);

        }
        return s;
    }
}

public class Exercise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Student s = new Student();
        s.age = 18;
        s.name = "小明";
        s.number = 123456;
        s.sex = true;
        s.Save("Student");
        Student s1 = Student.Load("Student");
        Debug.Log("年龄:" + s1.age);
        Debug.Log("姓名:" + s1.name);
        Debug.Log("学号:" + s1.number);
        Debug.Log("性别:" + s1.sex);
    }

   
}
