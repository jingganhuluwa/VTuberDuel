// 文件：VTuberRender.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 20:54


using System;
using Godot;
using TinyFramework;

public partial class VTuberRender : Node3D
{
    //[Export] public ProgressBar HPBar;
    [Export] public HPBar HPBar;
    [Export] public ProgressBar RunBar;
    [Export] public Sprite3D VTuberSprite;
    [Export] public Label3D VTuberName;

    public VTuberLogic OwnerLogic;


    

    // public override void _Process(double delta)
    // {
    //     if (OwnerLogic is null)
    //     {
    //         return;
    //     }
    //     //UpdateRunBar();
    // }

    public void UpdateHP(float rate,int damage=0)
    {
        
        HPBar.UpdateHP(rate);
        if (damage==0)
        {
            return;
        }
        //伤害飘字
        var packedScene = ResourceLoader.Load<PackedScene>(PathDefine.PrefabsPath + "FlyDamage.tscn");
        var flyDamage = packedScene.Instantiate<FlyDamage>();
        AddChild(flyDamage);
        flyDamage.Show(damage);
    }



    public void UpdateRunBar(float value)
    {
        RunBar.Value = value;
        //RunBar.Value = (OwnerLogic.RunCount / OwnerLogic.RunCountMax).RawFloat*100;
    }

    public void MoveTo(Vector3 targetPos, float time, Action callback)
    {
        Tween tween = CreateTween();
        tween.TweenProperty(this, "global_position", targetPos, time);
        tween.TweenCallback(Callable.From(()=>
        {
            callback?.Invoke();
        }));
        tween.Play();
        
    }
}