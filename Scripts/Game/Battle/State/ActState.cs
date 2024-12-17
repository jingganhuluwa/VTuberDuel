// 文件：ActState.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/08 19:09

using System.Collections.Generic;
using System.Linq;

public class ActState : BaseState
{


    private int _frameUpdateCount = 1;
    public override void OnEnter()
    {
        base.OnEnter();
        List<VTuberLogic> allVTuber = World.AllVTuber;
        VTuberLogic vTuberLogic = allVTuber.MaxBy(x => x.RunCount - x.RunCountMax);
        //行动条清空,直接等0也行,看设计需求
        vTuberLogic.RunCount -= vTuberLogic.RunCountMax;

        vTuberLogic.Act(World, () =>
        {
            vTuberLogic.Render.ZIndex = 0;
            //ChangeState(World.RunState);
        });
    }

    public override void OnFrameUpdate()
    {
        base.OnFrameUpdate();
        

        foreach (VTuberLogic vTuberLogic in World.AllVTuber)
        {
            if (vTuberLogic.IsAlive)
            {
                vTuberLogic.OnFrameUpdate();
            }
        }

        _frameUpdateCount++;
        //20次帧更新,即2秒检测一次
        if (_frameUpdateCount==20)
        {
            _frameUpdateCount = 1;
            //检测当前行动人数,少于2时,切换为RunState
            CheckAndSwitchState();
        }
        
    }
    
    /// <summary>
    /// 检测当前行动人数,少于2时,切换为RunState
    /// </summary>
    private void CheckAndSwitchState()
    {
        int actNum = 0;
        foreach (VTuberLogic vTuberLogic in World.AllVTuber)
        {
            if (!vTuberLogic.IsAlive)
            {
                continue;
            }

            if (!vTuberLogic.IsSkillFinish)
            {
                actNum++;
            }

        }

        if (actNum<2)
        {

            World.ChangeState(World.RunState);
        }
    }
}