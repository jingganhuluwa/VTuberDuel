// 文件：Skill.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/10/10 22:22


using System;
using System.Collections.Generic;
using TinyFramework;

public class Skill
{
    public int SkillAcc { get; private set; } = 0;

    public readonly List<VTuberLogic> Targets = new List<VTuberLogic>();

    public VTuberLogic Owner;
    public SkillConfig Config { get; private set; }

    private BattleWorld _battleWorld => Owner.OwnerBattleWorld;

    private readonly List<VInt> _damagesList = new List<VInt>();

    

    public Skill(int skillId)
    {
        Config = ConfigLoader.GetOne<SkillConfig>(skillId);
    }


    public void TriggerSkill()
    {
        if (Config.SkillReleaseType == SkillReleaseType.ProbabilityType)
        {
            //概率触发
            if (_battleWorld.SRandom.Range(0, 100) <= Config.SkillChance)
            {
                UseSkill();
            }
        }
        else
        {
            //计数触发
            SkillAcc++;
            if (SkillAcc == Config.SkillAcc)
            {
                SkillAcc = 0;
                UseSkill();
            }
        }
    }

    private void UseSkill()
    {
        //设定技能目标
        SetTarget();
        //todo 先计算扣除血量
        CalculateDamage();
        
        _battleWorld.ActQueue.Enqueue(() =>
        {
            Owner.Render.UpdateSkillName(Config.Name);
            SkillForward(SkillBack);
        } );
    }

    /// <summary>
    /// 技能前摇
    /// </summary>
    /// <param name="callback"></param>
    private void SkillForward(Action callback)
    {
        _battleWorld.IsSkillFinish = false;
        switch (Config.SkillType)
        {
            case SkillType.MoveToTarget:
                MoveToAction moveToAction = new MoveToAction(Owner, Targets[0].Render, 1000, callback);
                ActionManager.Instance.RunAction(moveToAction);
                break;
            case SkillType.MoveToEnemyCenter:
                moveToAction = new MoveToAction(Owner, Targets[0].Render, 1000, callback);
                ActionManager.Instance.RunAction(moveToAction);
                break;
            case SkillType.MoveToCenter:
                moveToAction = new MoveToAction(Owner, Targets[0].Render, 1000, callback);
                ActionManager.Instance.RunAction(moveToAction);
                break;
            case SkillType.Bullet:
                break;
        }
        
    }

    
    /// <summary>
    /// 技能后摇
    /// </summary>
    private void SkillBack()
    {
        for (int i = 0; i < Targets.Count; i++)
        {
            VTuberLogic target = Targets[i];
            target.OnHit(_damagesList[i]);
        }

        MoveToAction moveToAction = new MoveToAction(Owner, Owner.Render.Parent, 500, ()=>{_battleWorld.IsSkillFinish = true;});
        ActionManager.Instance.RunAction(moveToAction);
        Owner.Render.ZIndex = 0;
    }

    /// <summary>
    /// 计算伤害
    /// </summary>
    private void CalculateDamage()
    {
        _damagesList.Clear();
        VInt calculateNum;
        VInt damage;
        //todo  伤害波动,关联角色幸运
        int damageRange = _battleWorld.SRandom.Range(90, 110);
        //todo 闪避
        switch (Config.SkillCalculateType)
        {
            case SkillCalculateType.SelfAtk:
            default:
                calculateNum = Owner.Atk;
                break;
            case SkillCalculateType.SelfMaxHP:
                calculateNum = Owner.MaxHp;
                break;
            case SkillCalculateType.SelfAmor:
                calculateNum = Owner.Amor;
                break;
        }

        foreach (VTuberLogic target in Targets)
        {
            switch (Config.SkillCalculateType)
            {
                case SkillCalculateType.TargetCurrentHP:
                    calculateNum = target.Hp;
                    break;
                case SkillCalculateType.TargetMaxHP:
                    calculateNum = target.MaxHp;
                    break;
                case SkillCalculateType.TargetAmor:
                    calculateNum = target.Amor;
                    break;
            }

            calculateNum = calculateNum * damageRange * Config.SkillCalculatValue / 10000;
            switch (Config.SkillDamageType)
            {
                case SkillDamageType.Normal:
                default:
                    damage=calculateNum -target.Amor;
                    break;
                case SkillDamageType.Magic:
                    damage = calculateNum * Owner.Atk / (Owner.Atk + target.Amor);
                    break;
                case SkillDamageType.Real:
                    damage = calculateNum;
                    break;
            }
            _damagesList.Add(damage);
        }
    }

    /// <summary>
    /// 设定技能目标
    /// </summary>
    private void SetTarget()
    {
        Targets.Clear();
        if (Config.SkillAtkType == SkillAtkType.Self)
        {
            Targets.Add(Owner);
            return;
        }

        List<VTuberLogic> teamList;

        if (Config.TeamType == TeamType.FriendTeam)
        {
            teamList = Owner.OwnerBattleWorld.AllFriendTeamList(Owner.Team);
        }
        else
        {
            teamList = Owner.OwnerBattleWorld.AllEnemyTeamList(Owner.Team);
        }

        switch (Config.SkillAtkType)
        {
            case SkillAtkType.One:
            default:
                //todo 设定技能目标
                foreach (VTuberLogic vTuber in teamList)
                {
                    if (vTuber.IsAlive)
                    {
                        Targets.Add(vTuber);
                        break;
                    }
                }
                break;
            case SkillAtkType.All:
                foreach (VTuberLogic vTuber in teamList)
                {
                    if (vTuber.IsAlive)
                    {
                        Targets.Add(vTuber);
                    }
                }

                break;
            case SkillAtkType.Global:
                foreach (VTuberLogic vTuber in Owner.OwnerBattleWorld.AllVTuber)
                {
                    if (vTuber.IsAlive)
                    {
                        Targets.Add(vTuber);
                    }
                }
                break;
            case SkillAtkType.FrontRow:
                break;
            case SkillAtkType.BackRow:
                break;
        }
    }
}