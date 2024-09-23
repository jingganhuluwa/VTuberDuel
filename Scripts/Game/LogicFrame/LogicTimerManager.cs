// 文件：LogicTimerManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/23 21:01

using System;
using System.Collections.Generic;
using TinyFramework;

public class LogicTimerManager:Singleton<LogicTimerManager>
{
    private readonly List<LogicTimer> _logicTimerList = new List<LogicTimer>();
    
    /// <summary>
    /// 延迟调度任务
    /// </summary>
    /// <param name="delayTime">延迟时间</param>
    /// <param name="callback">回调</param>
    /// <param name="loop">循环次数</param>
    /// <param name="initAccTime">初始累计时间</param>
    public void DelayCall(VInt delayTime, Action callback, int loop = 1,int initAccTime=0)
    {
        LogicTimer timer = new LogicTimer(delayTime,callback,loop,initAccTime);
        _logicTimerList.Add(timer);
    }


    public void OnLogicFrameUpdate()
    {
        //遍历执行逻辑帧更新
        foreach (LogicTimer logicTimer in _logicTimerList)
        {
            logicTimer?.OnLogicFrameUpdate();
        }

        //移除完成的计时任务
        for (int i = _logicTimerList.Count - 1; i >= 0; i--)
        {
            if (_logicTimerList[i].Finished)
            {
                _logicTimerList.RemoveAt(i);
            }
        }
    }

    public void OnDestroy()
    {
        _logicTimerList.Clear();
    }
}