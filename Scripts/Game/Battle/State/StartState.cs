// 文件：StartState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/03 01:09

public class StartState : BaseState
{

    public override void OnEnter()
    {
        //todo 遍历所有VTuber,触发开局技能
        base.OnEnter();
        World.ChangeState(World.RunState);
    }
    
}