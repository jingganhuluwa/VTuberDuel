// 文件：RunState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/08 19:09

using System.Collections.Generic;
using System.Linq;
using Godot;

public class RunState:IState
{
   
    public void OnEnter(BattleWorld battleWorld)
    {
        GD.Print("RunState");
    }

    public void OnFrameUpdate(BattleWorld battleWorld)
    {
        
        //遍历所有VTuber,累加行动条
        List<VTuberLogic> allVTuber = battleWorld.GetAllVTuber();
        foreach (VTuberLogic vTuberLogic in allVTuber)
        {
            vTuberLogic.ActCount += vTuberLogic.Speed;
        }

        //当前行动条-行动条最大值>0,转为行动状态
        if (allVTuber.Max(x => x.ActCount - x.ActCountMax)>0)
        {
            battleWorld.ChangeState(battleWorld.ActState);
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

    public void OnExit(BattleWorld battleWorld)
    {
        
    }
}