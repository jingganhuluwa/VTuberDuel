// 文件：UIManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 19:51

using System;
using System.Collections.Generic;
using Godot;

namespace TinyFramework;

public enum UILayer
{
    Bottom,
    Middle,
    Top
}

public partial class UIManager:SingletonNode<UIManager>
{
    [Export]
    private Control _bottom;
    [Export]
    private Control _middle;
    [Export]
    private Control _top;
    [Export]
    private Label _tips;


    private Dictionary<string, Control> _panelDict = new Dictionary<string, Control>();
    private Queue<string> _tipsQueue = new Queue<string>();
    private bool isTipShow = false;

    public override void Init()
    {
        base.Init();
        _tips.Hide();
        
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!isTipShow)
        {
            ShowTips();
        }
    }

    public void ShowUI<T>(UILayer uiLayer=UILayer.Bottom,Action callback=null) where T : Control
    {
        string panelName = typeof(T).Name;
        if (_panelDict.TryGetValue(panelName,out Control ui))
        {
            ui?.Show();
            callback?.Invoke();
            return;
        }
        
        PackedScene packedScene = ResourceLoader.Load<PackedScene>(PathDefine.UIPath + panelName+".tscn");
        if (packedScene is null)
        {
            return;
        }
        ui = packedScene.Instantiate<Control>();
        ui.Name = panelName;
        switch (uiLayer)
        {
            case UILayer.Bottom:
                _bottom.AddChild(ui);
                break;
            case UILayer.Middle:
                _middle.AddChild(ui);
                break;
            case UILayer.Top:
                _top.AddChild(ui);
                break;
        }
        ui.Show();
        callback?.Invoke();
        _panelDict.Add(panelName,ui);
    }
    
    public void AddTips(string content)
    {
        _tipsQueue.Enqueue(content);
    }

    private void ShowTips()
    {
        if (_tipsQueue.Count>0)
        {
            string content = _tipsQueue.Dequeue();
            isTipShow = true;
            _tips.Show();
            _tips.Text = content;
            Tween tween = CreateTween();
            Vector2 position = _tips.Position;
            position.Y -= 200;
            Color color = _tips.SelfModulate;
            color.A = 1;
            tween.TweenProperty(_tips, "position",position,2);
            tween.SetParallel();
            tween.TweenProperty(_tips, "self_modulate",color,1);
            tween.SetParallel(false);
            color.A = 0;
            tween.TweenProperty(_tips, "self_modulate",color,1);
            tween.TweenCallback(Callable.From(() =>
            {
                isTipShow = false;
                position.Y += 200;
                _tips.Position = position;
                _tips.SelfModulate=color;
                _tips.Hide();
            }));
            tween.Play();
        }
    }
}