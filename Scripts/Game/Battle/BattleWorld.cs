// 文件：BattleWorld.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 18:12


using System.Collections.Generic;
using System.Linq;
using Godot;
using TinyFramework;

public class BattleWorld
{
    public string Name { get; private set; }
    
    public BattleScene Scene{ get; private set; }
    
    
    private readonly List<VTuberLogic> _allVTuberList = new List<VTuberLogic>();
    private readonly List<VTuberLogic> _playerVTuberList = new List<VTuberLogic>();
    private readonly List<VTuberLogic> _enemyVTuberList = new List<VTuberLogic>();
    
    private IState _currentState;
    public readonly IState StartState=new StartState();
    public readonly IState RunState=new RunState();
    public readonly IState ActState=new ActState();
    public readonly IState PauseState=new PauseState();
    public readonly IState EndState=new EndState();
    
    public BattleWorld Create(string battleWorldName, List<VTuberData> playerVTuberList, List<VTuberData> enemyVTuberList)
    {
        Name = battleWorldName;

        //加载战斗场景
        PackedScene scene = ResourceLoader.Load<PackedScene>(PathDefine.ScenePath + "BattleScene.tscn");
        Scene=scene.Instantiate<BattleScene>();
        GameManager.Instance.Game.AddChild(Scene);


        for (int i = 0; i < playerVTuberList.Count; i++)
        {
            VTuberLogic vTuber = VTuberFactory.CreateBattleVTuber(playerVTuberList[i], i + 1,Scene.PlayerSeatArr[i]);
            vTuber.Team = TeamEnum.Player;
            _playerVTuberList.Add(vTuber);
            _allVTuberList.Add(vTuber);
        }
        for (int i = 0; i < enemyVTuberList.Count; i++)
        {
            VTuberLogic vTuber = VTuberFactory.CreateBattleVTuber(enemyVTuberList[i], i + 1,Scene.EnemySeatArr[i]);
            vTuber.Team = TeamEnum.Enemy;
            _enemyVTuberList.Add(vTuber);
            _allVTuberList.Add(vTuber);
        }
        
        //改变当前状态为开局转态
        ChangeState(StartState);

        return this;
    }


    public void LogicFrameUpdate()
    {
        _currentState?.OnFrameUpdate(this);
    }
    
    public void ChangeState(IState state)
    {
        _currentState?.OnExit(this);
        _currentState = state;
        _currentState?.OnEnter(this);
    }

    public void OnDestroyWorld()
    {
        
    }

    public List<VTuberLogic> GetAllVTuber()
    {
        //toList,临时List,防手贱,在其他地方进行add,remove操作
        return _allVTuberList.ToList();
    }
}