// 文件：SingletonNode.cs
// 作者：急冻雪柜
// 描述：Node节点单例基类
// 日期：2024/09/19 16:16

using Godot;

namespace TinyFramework;

public partial class SingletonNode<T> :Node where T:Node 
{
    public static T Instance { get; private set; }

    public override void _EnterTree()
    {
        base._EnterTree();
        Instance = this as T;
    }
    
    public virtual void Init()
    {
        GD.Print($"{typeof(T).Name}初始化");
    }
}