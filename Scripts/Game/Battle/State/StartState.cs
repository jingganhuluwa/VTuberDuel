// 文件：StartState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/03 01:09

using Godot;

public class StartState : IState
{

    public void OnEnter(BattleWorld battleWorld)
    {
        //todo 遍历所有VTuber,触发开局技能
        
        GD.Print("StartState");
        battleWorld.ChangeState(battleWorld.RunState);
    }

    public void OnFrameUpdate(BattleWorld battleWorld)
    {
    }

    public void OnExit(BattleWorld battleWorld)
    {
    }
}