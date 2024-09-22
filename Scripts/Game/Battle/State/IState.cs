// 文件：IState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/03 01:09

public interface IState
{
    public void OnEnter(BattleWorld battleWorld);
    public void OnFrameUpdate(BattleWorld battleWorld);
    public void OnExit(BattleWorld battleWorld);
}