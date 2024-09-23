// 文件：IState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/03 01:09

using Godot;

public class BaseState
{
    public BattleWorld World;
    
    
    
    public virtual void OnEnter()
    {
        GD.Print($"{GetType().Name}");
    }
    public virtual void OnFrameUpdate(){}
    public virtual void OnExit(){}

    protected void ChangeState(BaseState state)
    {
        World.ChangeState(state);
    }
}