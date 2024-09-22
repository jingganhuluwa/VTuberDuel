// 文件：VTuberLogic.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 20:47

using Godot;

public enum TeamEnum
{
    Player,
    Enemy
}

public class VTuberLogic
{
    public int Seat;
    public VInt Speed;
    public VInt ActCount;
    public VInt ActCountMax = 1000;

    public TeamEnum Team;
    

    public VTuberConfig Config;

    public VTuberRender Render;

    //vtuber行动
    public void Act()
    {
        if (Team == TeamEnum.Player)
        {
            GD.Print($"玩家VTuber: {Config.Name} 行动,速度为 {Speed}");
        }
        else
        {
            GD.Print($"敌方VTuber: {Config.Name} 行动,速度为 {Speed}");
        }

        
    }
}