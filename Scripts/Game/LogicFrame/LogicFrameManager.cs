// 文件：LogicFrameManager.cs
// 作者：急冻雪柜
// 描述：逻辑帧管理器
// 日期：2024/09/22 16:44

using TinyFramework;

public class LogicFrameManager:Singleton<LogicFrameManager>
{
    private double _accLogicRuntime; //累计逻辑运行时间

    private double _nextLogicFrameTime; //下一逻辑帧时间

    public static double DeltaTime { get; private set; } //逻辑时间增量
    
    public void OnUpdate(double delta)
    {
        _accLogicRuntime += delta;

        while (_accLogicRuntime>_nextLogicFrameTime)
        {
            LogicFrameUpdate();
            _nextLogicFrameTime += LogicFrameConfig.LogicFrameInterval;
            LogicFrameConfig.LogicFrameId++;
        }

        DeltaTime = (_accLogicRuntime + LogicFrameConfig.LogicFrameInterval - _nextLogicFrameTime) /
                    LogicFrameConfig.LogicFrameInterval;
        
    }

    private void LogicFrameUpdate()
    {
        BattleWorldManager.Instance.LogicFrameUpdate();
        LogicTimerManager.Instance.OnLogicFrameUpdate();
        ActionManager.Instance.OnLogicFrameUpdate();
    }
}