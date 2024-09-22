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
        var sprite3D = new Sprite3D();
        sprite3D.Texture=  ResourceLoader.Load<Texture2D>("res://icon.svg");
        parent.AddChild(sprite3D);
        
        var vTuberRender = new VTuberRender()
        {
            Image = sprite3D
        };

        var vTuberLogic = new VTuberLogic
        {
            Speed = vTuberData.Speed,
            Seat = seat,
            Config = ConfigLoader.GetOne<VTuberConfig>(vTuberData.ConfigId),
            Render = vTuberRender
        };
        return vTuberLogic;
    }
}