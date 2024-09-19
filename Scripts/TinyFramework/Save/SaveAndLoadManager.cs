// 文件：SaveAndLoadManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 17:58


using System.IO;
using Godot;
using Newtonsoft.Json;

namespace TinyFramework;

public class SaveAndLoadManager:Singleton<SaveAndLoadManager>
{
    public void Save<T>(T data) where T : IData
    {
        string json = JsonConvert.SerializeObject(data);
        string fullName = PathDefine.SavePath + typeof(T).Name;
        GD.Print($"{fullName}");
        using (StreamWriter sw = new StreamWriter(fullName))
        {
            sw.Write(json);
        }
    }

    public T Load<T>() where T : IData, new()
    {
        string fullName = PathDefine.SavePath + typeof(T).Name;
        if (File.Exists(fullName))
        {
            using (StreamReader sr=new StreamReader(fullName))
            {
                T data = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                if (data!=null)
                {
                    return data;
                }
            }
        }
        //原来的存档没数据,返回新存档
        return new T();
    }
}