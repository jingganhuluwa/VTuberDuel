// 文件：ActionManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/10/05 4:23

using System.Collections.Generic;
using System.Linq;
using TinyFramework;

public class ActionManager:Singleton<ActionManager>
{
    /// <summary>
    /// 所有行动列表
    /// </summary>
    private readonly List<ActionBase> actionList = new List<ActionBase>();

    public override void Init()
    {
        base.Init();
    }

    public void RunAction(ActionBase action)
    {
        actionList.Add(action);
        //OnLogicFrameUpdate();
    }

    public void OnLogicFrameUpdate()
    {
        //todo bug修复:skill释放时,持续时间为0时有bug.  list.ToList()使用临时list,防止遍历时list被修改. 
        foreach (ActionBase actionBase in actionList.ToList())
        {
            actionBase.OnLogicFrameUpdate();
        }
        
        //移除完成的行动
        for (int i = actionList.Count - 1; i >= 0; i--)
        {
            if (actionList[i].ActionComplete)
            {
                actionList.RemoveAt(i);
            }
        }
    }
    
}