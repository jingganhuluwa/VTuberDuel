// 文件：VTuberFactory.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 20:50


using Godot;
using TinyFramework;

public class VTuberFactory
{
    /// <summary>
    /// 战斗场景创建VTuber
    /// </summary>
    public static VTuberLogic CreateBattleVTuber(VTuberData vTuberData, int seat,Node3D parent)
    {

        PackedScene vTuberView = ResourceLoader.Load<PackedScene>(PathDefine.PrefabsPath + "VTuberRender.tscn");
        VTuberRender vTuberRender = vTuberView.Instantiate<VTuberRender>();
        parent.AddChild(vTuberRender);
        
        

        var vTuberLogic = new VTuberLogic
        {
            Speed = vTuberData.Speed,
            Hp = vTuberData.HP,
            MaxHp = vTuberData.HP,
            Atk = vTuberData.Atk,
            Team = TeamEnum.Player,
            Seat = seat,
            Config = ConfigLoader.GetOne<VTuberConfig>(vTuberData.ConfigId),
            Render = vTuberRender
        };
        vTuberRender.OwnerLogic = vTuberLogic;
        vTuberRender.UpdateHP(1);
        vTuberRender.VTuberName.Text = vTuberLogic.Config.Name;
        return vTuberLogic;
    }
}