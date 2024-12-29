// 文件：RunState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/08 19:09

public class RunState:BaseState
{
    public override void OnEnter()
    {
        base.OnEnter();

    }

    public override void OnFrameUpdate()
    {
        
        //遍历所有VTuber,累加行动条
     

        //当前行动条-行动条最大值>0,转为行动状态
    
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