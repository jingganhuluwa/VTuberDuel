// 文件：ActState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/08 19:09

using System.Collections.Generic;
using System.Linq;

public class ActState:IState
{
    
    public void OnEnter(BattleWorld battleWorld)
    {
        List<VTuberLogic> allVTuber = battleWorld.GetAllVTuber();
        VTuberLogic vTuberLogic = allVTuber.MaxBy(x=>x.ActCount-x.ActCountMax);
        //行动条清空,直接等0也行,看设计需求
        vTuberLogic.ActCount -= vTuberLogic.ActCountMax;
        vTuberLogic.Act();
        battleWorld.ChangeState(battleWorld.RunState);
    }

    public void OnFrameUpdate(BattleWorld battleWorld)
    {
        
    }

    public void OnExit(BattleWorld battleWorld)
    {
        
    }
}