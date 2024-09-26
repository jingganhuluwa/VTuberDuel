// 文件：LogicTimer.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/23 20:57


using System;

public class LogicTimer
{
    public bool Finished;
    public VInt DelayTime;
    public int LoopCount;
    public int Loop;
    public Action OnTimerComplete;

    private VInt _accTime; //当前累加时间
    

    /// <param name="delayTime">延迟时间</param>
    /// <param name="callback">回调</param>
    /// <param name="loop">循环次数</param>
    /// <param name="initAccTime">初始累计时间</param>
    public LogicTimer(VInt delayTime, Action callback, int loop = 1,int initAccTime=0)
    {
        DelayTime = delayTime;
        OnTimerComplete = callback;
        Loop = loop;
        _accTime = initAccTime;
    }
    
    public void OnLogicFrameUpdate()
    {
        _accTime += (VInt) LogicFrameConfig.LogicFrameIntervalMS;
        
        if (_accTime>=DelayTime&&LoopCount<Loop)
        {
            OnTimerComplete?.Invoke();
            _accTime = 0;
            LoopCount++;
            if (LoopCount>=Loop)
            {
                Finished = true;
            }
        }
    }
}