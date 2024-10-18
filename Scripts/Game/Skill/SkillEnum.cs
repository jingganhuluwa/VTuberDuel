// 文件：SkillEnum.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/10/10 22:22

/// <summary>
/// 技能类型
/// </summary>
public enum SkillType
{
    /// <summary> 1.移动到目标 </summary>
    MoveToTarget = 1,

    /// <summary> 2.移动到敌方中心</summary>
    MoveToEnemyCenter = 2,

    /// <summary> 3.移动到画面中心</summary>
    MoveToCenter = 3,

    /// <summary> 4.弹道型 </summary>
    Bullet = 4
}

/// <summary> 目标团队 </summary>
public enum TeamType
{
    /// <summary>敌方</summary>
    EnemyTeam = 1,

    /// <summary>友方</summary>
    FriendTeam=2
}

/// <summary> 技能逻辑 </summary>
public enum SkillLogicType
{
    /// <summary>普通</summary>
    Normal=1,
    /// <summary>特殊</summary>
    Special=2,
    /// <summary>混合</summary>
    Blender=3
}

/// <summary> 攻击类型 </summary>
public enum SkillAtkType
{
    /// <summary>单体</summary>
    One=1,
    /// <summary>全体</summary>
    All=2,
    /// <summary>全球(含敌我双方)</summary>
    Global=3,
    /// <summary>前排</summary>
    FrontRow=4,
    /// <summary>后排</summary>
    BackRow=5,
    /// <summary>自身</summary>
    Self=6
}



/// <summary> 目标类型(单体) </summary>
public enum SkillTargetType
{
    /// <summary>普通(按站位)</summary>
    Normal=1,
    /// <summary>最低血量</summary>
    MinHP=2,
    /// <summary>最高血量</summary>
    MaxHP=3,
    /// <summary>最低护甲</summary>
    MinAmor=4,
    /// <summary>最高护甲</summary>
    MaxAmor=5
    
}

/// <summary> 伤害类型 </summary>
public enum SkillDamageType
{
    /// <summary>普通</summary>
    Normal=1,
    /// <summary>魔法</summary>
    Magic=2,
    /// <summary>真实</summary>
    Real=3
}

/// <summary> 计算类型 </summary>
public enum SkillCalculateType
{
    /// <summary> 自身攻击力 </summary>
    SelfAtk=1,
    /// <summary> 自身最大生命百分比 </summary>
    SelfMaxHP=2,
    /// <summary> 自身护甲 </summary>
    SelfAmor=3,
    /// <summary> 目标当前生命百分比 </summary>
    TargetCurrentHP=4,
    /// <summary> 目标最大生命百分比 </summary>
    TargetMaxHP=5,
    /// <summary> 目标护甲 </summary>
    TargetAmor=6,

    
}

/// <summary> 技能释放类型 </summary>
public enum SkillReleaseType
{
    /// <summary> 概率型 </summary>
    ProbabilityType=1,
    /// <summary> 计数型 </summary>
    CountType=2
}

/// <summary> 技能触发时机 </summary>
public enum SkillTriggerTime
{
    /// <summary> 开局触发 </summary>
    OnStart=1,
    /// <summary> 行动触发 </summary>
    OnAct=2,
    /// <summary> 受击触发 </summary>
    OnHit=3,
    /// <summary> 死亡触发 </summary>
    OnDeath=4
}

/// <summary> 技能位移 </summary>
public enum SkillMove
{
    /// <summary> 前进 </summary>
    Forward=1,
    /// <summary> 后退 </summary>
    Back=-1
}