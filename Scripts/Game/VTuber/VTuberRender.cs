// 文件：VTuberRender.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 20:54


using Godot;

public partial class VTuberRender : Node3D
{
    [Export] public ProgressBar HPBar;
    [Export] public ProgressBar RunBar;
    [Export] public Sprite3D VTuberSprite;
    [Export] public Label VTuberName;

    public VTuberLogic OwnerLogic;


    

    public override void _Process(double delta)
    {
        if (OwnerLogic is null)
        {
            return;
        }
        UpdateHP();
        UpdateRunBar();
    }

    public void UpdateHP()
    {
        HPBar.Value = (OwnerLogic.Hp / OwnerLogic.MaxHp).RawFloat*100;
    }
    
    public void UpdateRunBar()
    {
        RunBar.Value = (OwnerLogic.RunCount / OwnerLogic.RunCountMax).RawFloat*100;
    }
}