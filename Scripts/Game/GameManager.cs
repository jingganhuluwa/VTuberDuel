// 文件：GameManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 16:40

using Godot;
using TinyFramework;

public partial class GameManager:SingletonNode<GameManager>
{

    public Node Game;
    public override void _Ready()
    {
        AudioManager.Instance.Init();
        
        SaveAndLoadManager.Instance.Init();
        
        SettingManager.Instance.Init();
        
        LogicFrameManager.Instance.Init();
        
        ActionManager.Instance.Init();


        PackedScene UI = ResourceLoader.Load<PackedScene>(PathDefine.UIPath+"UIManager.tscn");
        AddChild(UI.Instantiate<UIManager>());
        UIManager.Instance.Init();
        
        UIManager.Instance.ShowUI<StartPanel>();
        //测试
        AddChild(new Test());

        Game = GetNode("/root/Game");
    }

    public override void _Process(double delta)
    {
        LogicFrameManager.Instance.OnUpdate(delta);
    }
}