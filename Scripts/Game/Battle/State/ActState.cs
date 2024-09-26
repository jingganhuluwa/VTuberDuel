// 文件：ActState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/08 19:09

using System.Collections.Generic;
using System.Linq;

public class ActState:BaseState
{
    
    public override void OnEnter()
    {
        base.OnEnter();
        List<VTuberLogic> allVTuber = World.AllVTuber();
        VTuberLogic vTuberLogic = allVTuber.MaxBy(x=>x.RunCount-x.RunCountMax);
        //行动条清空,直接等0也行,看设计需求
        vTuberLogic.RunCount -= vTuberLogic.RunCountMax;
        
        vTuberLogic.Act(World,()=>ChangeState(World.RunState));
    }
    
}