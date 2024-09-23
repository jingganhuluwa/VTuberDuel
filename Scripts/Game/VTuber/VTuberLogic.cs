// 文件：VTuberLogic.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 20:47

using System;
using System.Collections.Generic;

public enum TeamEnum
{
    Player,
    Enemy
}

public class VTuberLogic
{
    public int Seat;
    public VInt Speed;
    public VInt RunCount;
    public VInt RunCountMax = 1000;

    public VInt Hp;

    public VInt MaxHp;

    public VInt Atk;

    public bool isAlive = true;

    public TeamEnum Team;
    

    public VTuberConfig Config;

    public VTuberRender Render;

    //vtuber行动
    public void Act(BattleWorld battleWorld,Action callback)
    {
        
        //执行普通攻击
        List<VTuberLogic> vTuberList = battleWorld.GetEnemyList(Team);
        foreach (VTuberLogic target in vTuberList)
        {
            if (target.isAlive)
            {
                //造成伤害
                target.Hp -= Atk;
                if (target.Hp<=0)
                {
                    target.isAlive = false;
                    target.RunCount = 0;
                }
                break;
            }
        }

       
        LogicTimerManager.Instance.DelayCall(1000,callback);
    }
}