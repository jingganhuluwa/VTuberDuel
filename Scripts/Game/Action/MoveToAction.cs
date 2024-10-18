// 文件：MoveToAction.cs
// 作者：急冻雪柜
// 描述：移动Action
// 日期：2024/10/05 4:34


using System;
using Godot;

public class MoveToAction:ActionBase
{
    private VTuberLogic _moveObject;
    private Node2D _target;
    private VInt2 _targetPos;
    private VInt _times;
    public Action OnMoveComplete;

    private VInt _lerpTime = 0;

    private VInt2 _originPos;
    
    
    /// <param name="moveObj">移动的对象</param>
    /// <param name="targetPos">移动的目标位置</param>
    /// <param name="times">移动用时(毫秒级)</param>
    /// <param name="moveComplete">移动完成后的回调</param>
    public MoveToAction(VTuberLogic moveObj,Node2D target, VInt times, Action onMoveComplete=null)
    {
        _moveObject = moveObj;
        _target = target;
        _targetPos =new VInt2(target.GlobalPosition)  ;
        _times = times;
        OnMoveComplete = onMoveComplete;
        _originPos = moveObj.LogicPosition;
    }
    
    public override void OnLogicFrameUpdate()
    {
        base.OnLogicFrameUpdate();
        _lerpTime += (VInt) LogicFrameConfig.LogicFrameIntervalMS;
        VInt lerpValue = _lerpTime / _times;

        _targetPos =new VInt2(_target.GlobalPosition) ;
        
        //计算新的位置
        _moveObject.LogicPosition= VInt2.Lerp(_originPos, _targetPos, lerpValue.RawFloat);


        if (lerpValue > VInt.one)
        {
            //行动完成
            OnMoveComplete?.Invoke();
            ActionComplete = true;
            return;
        }

    }

    
}