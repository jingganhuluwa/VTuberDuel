// 文件：VTuberLogic.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 20:47

using System;
using System.Collections.Generic;
using Godot;
using TinyFramework;

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
    public VInt RunCountMax = 2000;

    public VInt Hp;

    public VInt MaxHp;

    public VInt Atk;

    public bool isAlive = true;

    public TeamEnum Team;


    public VTuberConfig Config;

    public VTuberRender Render;


    public void Run()
    {
        RunCount += Speed;

        Render.UpdateRunBar((RunCount / RunCountMax).RawFloat * 100);
    }


    //vtuber行动
    public void Act(BattleWorld battleWorld, Action callback)
    {
        //执行普通攻击
        List<VTuberLogic> vTuberList = battleWorld.AllEnemyTeamList(Team);
        foreach (VTuberLogic target in vTuberList)
        {
            if (target.isAlive)
            {
                //造成伤害
                MoveTo(target.Render.GlobalPosition, 0.8f,  ()=>{
                    ATK(target, 0.6f,callback);
                });
                break;
            }
        }

        //LogicTimerManager.Instance.DelayCall(800, callback);
    }


    public void ATK(VTuberLogic target, float time, Action callback = null)
    {
        target.OnHit(Atk);
        if (target.Hp <= 0)
        {
            target.isAlive = false;
            target.RunCount = 0;
        }
        AudioManager.Instance.PlayAudio("battle01.mp3",true);
        MoveTo(Render.GetParent<Node3D>().GlobalPosition, time);
        LogicTimerManager.Instance.DelayCall(new VInt(time * 1000), callback);
    }

    public void MoveTo(Vector3 targetPos, float time, Action callback = null)
    {
        Render.MoveTo(targetPos, time, callback);
    }


    public void OnHit(VInt damage)
    {
        Hp -= damage;
        Render.UpdateHP((Hp / MaxHp).RawFloat, damage.RawInt);
    }
}