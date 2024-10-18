using TinyFramework;

public class SkillConfig : BaseConfig
{
	/// <summary> 名字 </summary>
	public string Name;
	/// <summary> 技能描述 </summary>
	public string Desc;
	/// <summary> 技能类型 </summary>
	public SkillType SkillType;
	/// <summary> 目标团队 </summary>
	public TeamType TeamType;
	/// <summary> 技能逻辑 </summary>
	public SkillLogicType SkillLogicType;
	/// <summary> 攻击类型 </summary>
	public SkillAtkType SkillAtkType;
	/// <summary> 目标类型(单体) </summary>
	public SkillTargetType SkillTargetType;
	/// <summary> 伤害类型 </summary>
	public SkillDamageType SkillDamageType;
	/// <summary> 计算类型 </summary>
	public SkillCalculateType SkillCalculateType;
	/// <summary> 计算数值 </summary>
	public int SkillCalculatValue;
	/// <summary> 技能释放类型 </summary>
	public SkillReleaseType SkillReleaseType;
	/// <summary> 技能释放概率(技能释放类型1) </summary>
	public int SkillChance;
	/// <summary> 技能释放计数(技能释放类型2) </summary>
	public int SkillAcc;
	/// <summary> 技能限制释放次数(0不限制次数) </summary>
	public int SkillLimit;
	/// <summary> 技能重复概率 </summary>
	public int SkillRepeatChance;
	/// <summary> 技能重复限制 </summary>
	public int SkillRepeatLimit;
	/// <summary> 技能触发时机 </summary>
	public SkillTriggerTime SkillTriggerTime;
	/// <summary> 技能添加buffs </summary>
	public int[] SkillAddBuff;
	/// <summary> 技能位移 </summary>
	public SkillMove SkillMove;
}