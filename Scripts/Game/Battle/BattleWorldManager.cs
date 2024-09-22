// 文件：BattleWorldManager.cs
// 作者：急冻雪柜
// 描述：战斗世界管理器
//     管理战斗世界,以后可能会创建多个战斗世界,比如一个后台挂着推图,一个打竞技场(占坑,可能不填)
// 日期：2024/09/22 18:08

using System.Collections.Generic;
using TinyFramework;

public class BattleWorldManager:Singleton<BattleWorldManager>
{
    private Dictionary<string, BattleWorld> _battleWorldDict = new Dictionary<string, BattleWorld>();

    /// <summary>
    /// 创建战斗世界
    /// </summary>
    public BattleWorld CreateBattleWorld(string battleWorldName, List<VTuberData> playerVTuberList,
        List<VTuberData> enemyVTuberList)
    {
        BattleWorld battleWorld = new BattleWorld();

        BattleWorld world = battleWorld.Create(battleWorldName,playerVTuberList, enemyVTuberList);
        
        _battleWorldDict.Add(battleWorldName,world);

        return battleWorld;
    }
    
    public BattleWorld GetBattleWorld(string battleWorldName)
    {
        _battleWorldDict.TryGetValue(battleWorldName, out BattleWorld world);
        return world;
    }

    public void LogicFrameUpdate()
    {
        foreach (BattleWorld battleWorld in _battleWorldDict.Values)
        {
            battleWorld?.LogicFrameUpdate();
        }
    }
    
    /// <summary>
    /// 销毁世界
    /// </summary>
    public void DestroyWorld(string battleWorldName)
    {
        _battleWorldDict.TryGetValue(battleWorldName, out BattleWorld world);
        world?.OnDestroyWorld();
        _battleWorldDict.Remove(battleWorldName);
    }
}