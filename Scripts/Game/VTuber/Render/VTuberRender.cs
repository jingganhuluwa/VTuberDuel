// 文件：VTuberRender.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 20:54


using Godot;
using TinyFramework;

public partial class VTuberRender : Node2D
{
    //[Export] public ProgressBar HPBar;
    [Export] public HPBar HPBar;
    [Export] public ProgressBar RunBar;
    [Export] public Sprite2D VTuberSprite;
    [Export] public Label VTuberName;
    [Export] public AnimatedSprite2D AnimSlash;

    public VTuberLogic OwnerLogic;

    public Node2D Parent { get; private set; }


    

    public override void _Process(double delta)
    {
        if (OwnerLogic is null)
        {
            return;
        }
        //UpdateRunBar();
        UpdatePos(delta);
    }

    public void Init(VTuberLogic logic)
    {
        AnimSlash.Hide();
        OwnerLogic = logic;
        UpdateHP(1);
        VTuberName.Text = logic.Config.Name;
        logic.LogicPosition = new VInt2(GlobalPosition) ;
        Parent = GetParent<Node2D>();
    }
    
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

    // public void MoveTo(Vector2 targetPos, float time, Action callback)
    // {
    //     Tween tween = CreateTween();
    //     tween.TweenProperty(this, "global_position", targetPos, time);
    //     tween.TweenCallback(Callable.From(()=>
    //     {
    //         callback?.Invoke();
    //     }));
    //     tween.Play();
    //     
    // }

    private void UpdatePos(double delta)
    {
        GlobalPosition = VInt2.Lerp(new VInt2(GlobalPosition), OwnerLogic.LogicPosition, (float) delta).vec2;
    }
}