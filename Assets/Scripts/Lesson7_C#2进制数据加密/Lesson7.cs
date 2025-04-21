using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Lesson7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 何时加密? 何时解密
        // 当我们将类对象转换为2进制数据时进行加密
        // 当我们将2进制数据转换为类对象时进行解密
        
        // 这样如果第三方获取到我们的2进制数据
        // 当他们不知道加密规则和解密秘钥时就无法获取正确的数据
        // 起到保证数据安全的作
        #endregion

        #region 知识点二 加密是否是100%安全的?
        //一定记住加密只是提高破解门槛，没有100%保密的数据
        //通过各种尝试始终是可以破解加密规则的，只是时间问题
        //加密只能起到提升一定的安全性
        #endregion

        #region 知识点三 常用的加密算法
        //MD5
        //SHA1
        //HMAC
        //DES
        //AES 等等

        //有很多的别人写好的第三发加密算法库
        //可以直接获取用于在程序中对数据进行加密
        //这里我们不深究 感兴趣的同学可以自己去了解
        #endregion

        #region 知识点四 用简单的异或加密来感受一下加密的作用
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

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
