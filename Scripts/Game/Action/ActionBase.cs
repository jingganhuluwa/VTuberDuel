// 文件：ActionBase.cs
// 作者：急冻雪柜
// 描述：行动基类
// 日期：2024/10/05 4:22


public class ActionBase
{
    /// <summary>
    /// 行动完成
    /// </summary>
    public bool ActionComplete  = false;
    public virtual void OnCreate()
    {
        
    }

    public virtual void OnLogicFrameUpdate()
    {
        
    }

    public virtual void OnDestroy()
    {
        
    }
}