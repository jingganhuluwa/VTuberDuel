// 文件：RunState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/08 19:09

using System.Collections.Generic;
using System.Linq;

public class RunState:BaseState
{
    public override void OnEnter()
    {
        base.OnEnter();
        //判定双方英雄存活数是否为0
        World.AliveCount();
    }

    public override void OnFrameUpdate()
    {
        
        //遍历所有VTuber,累加行动条
        List<VTuberLogic> allVTuber = World.AllVTuber;
        foreach (VTuberLogic vTuberLogic in allVTuber)
        {
            if (vTuberLogic.IsAlive)
            {
                vTuberLogic.Run();
            }
            
        }

        //当前行动条-行动条最大值>0,转为行动状态
        if (allVTuber.Max(x => x.RunCount - x.RunCountMax)>0)
        {
            ChangeState(World.ActState);
        }
        //以上代码和一下代码作用一样
        // foreach (VTuberLogic vTuberLogic in allVTuber)
        // {
        //     if (vTuberLogic.ActCount-vTuberLogic.ActCountMax>0)
        //     {
        //         battleWorld.ChangeState(battleWorld.ActState);
        //     }
        // }
    }
    
}