// 文件：EndState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/08 19:09

using Godot;

public class EndState:BaseState
{
    public override void OnEnter()
    {
        base.OnEnter();

        if (World.WinTeam==TeamEnum.Player)
        {
            GD.Print("游戏结束,玩家胜利");
        }
        else
        {
            GD.Print("游戏结束,敌方胜利");
        }
        
    }
}