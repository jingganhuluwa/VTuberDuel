// 文件：VTuberLogic.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 20:47

using System;
using System.Collections.Generic;
using Godot;

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
    public VInt Amor;

    public bool IsAlive = true;

    public TeamEnum Team;


    public VTuberConfig Config;

    public readonly List<Skill> SkillList=new List<Skill>();

    /// <summary>
    /// 所属战斗世界
    /// </summary>
    public BattleWorld OwnerBattleWorld;
    
    public VTuberRender Render;
    public VInt2 LogicPosition;
    
    public readonly Queue<Action> ActQueue = new Queue<Action>();
    public bool IsSkillFinish = true;

    
    
    

    public void Run()
    {
        RunCount += Speed;

        Render.UpdateRunBar((RunCount / RunCountMax).RawFloat * 100);
    }


    
    
    
    //vtuber行动
    public void Act(BattleWorld battleWorld, Action callback)
    {
        foreach (Skill skill in SkillList)
        {
            skill.TriggerSkill();
        }

        
        //执行普通攻击
        // List<VTuberLogic> vTuberList = battleWorld.AllEnemyTeamList(Team);
        // foreach (VTuberLogic target in vTuberList)
        // {
        //     if (target.isAlive)
        //     {
        //         Render.ZIndex = 5;
        //         //造成伤害
        //         MoveTo(target.Render, 1000,  ()=>{
        //             ATK(target, 1000,callback);
        //         });
        //         break;
        //     }
        // }

        //LogicTimerManager.Instance.DelayCall(800, callback);
    }


    // public void ATK(VTuberLogic target, VInt time, Action callback = null)
    // {
    //     target.OnHit(Atk);
    //     if (target.Hp <= 0)
    //     {
    //         target.isAlive = false;
    //         target.RunCount = 0;
    //     }
    //     Render.AnimSlash.Show();
    //     Render.AnimSlash.Modulate = new Color(GD.Randf(),GD.Randf(),GD.Randf())*2.5f;
    //     Render.AnimSlash.Play("Slash");
    //     LogicTimerManager.Instance.DelayCall(800, () =>
    //     {
    //         Render.AnimSlash.Stop();
    //         Render.AnimSlash.Hide();
    //     });
    //     AudioManager.Instance.PlayAudio("battle01.mp3",true);
    //     MoveTo(Render.GetParent<Node2D>(), time,callback);
    //     //LogicTimerManager.Instance.DelayCall(time, callback);
    // }

    public void MoveTo(Node2D target, VInt time, Action callback = null)
    {
        var moveToAction = new MoveToAction(this, target, time, callback);
        ActionManager.Instance.RunAction(moveToAction);
    }


    public void OnHit(VInt damage)
    {

        Render.UpdateHP( damage.RawInt);
        Render.Onhit();
    }

    public void OnFrameUpdate()
    {
        if (ActQueue.Count==0)
        {
            return;
        }

        if (IsSkillFinish)
        {
            ActQueue.Dequeue()?.Invoke();
        }
        
        
        
    }
    
}