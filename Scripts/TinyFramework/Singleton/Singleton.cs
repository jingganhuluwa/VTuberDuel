// 文件：Singleton.cs
// 作者：急冻雪柜
// 描述：单例基类
// 日期：2024/09/19 16:13

using Godot;

namespace TinyFramework;

public class Singleton<T> where T:new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }

    public virtual void Init()
    {
        GD.Print($"{typeof(T).Name}初始化");
    }
}