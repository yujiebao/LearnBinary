using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public class Test
{
    public int age = 18;
    public string name = "唐老师";
    public int[] ids = new int[]{1,2,3,4,5};
    public List<int> list = new List<int>(){1,2,3,4,5};

    public Test(int age, string name)
    {
        this.age = age;
        this.name = name;
    }

    public override string ToString()
    {
        // 初始化一个StringBuilder来构建字符串
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
        sb.AppendLine($"Age: {age}");
        sb.AppendLine($"Name: {name}");
        sb.AppendLine($"Ids: {string.Join(", ", ids)}");
        sb.AppendLine($"List: {string.Join(", ", list)}");
        return sb.ToString();
    }
}
public class Exercise2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Test a = new Test(18, "唐老师");
        Test b = new Test(19, "小明");
        BinaryDataMgr.Instance.Save(a, "Test");
        BinaryDataMgr.Instance.Save(b, "Test2");
        Test c = BinaryDataMgr.Instance.Load<Test>("Test");
        Test d = BinaryDataMgr.Instance.Load<Test>("Test2");
        print(c);
        print(d);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
