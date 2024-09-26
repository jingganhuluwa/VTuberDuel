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

    public BattleScene Scene { get; private set; }


    private readonly List<VTuberLogic> _allVTuberList = new List<VTuberLogic>();
    private readonly List<VTuberLogic> _playerVTuberList = new List<VTuberLogic>();
    private readonly List<VTuberLogic> _enemyVTuberList = new List<VTuberLogic>();

    private int _playerAliveCount;
    private int _enemyAliveCount;

    public TeamEnum WinTeam = TeamEnum.Player;

    private BaseState _currentState;
    public BaseState StartState { get; private set; }
    public BaseState RunState { get; private set; }
    public BaseState ActState { get; private set; }
    public BaseState PauseState { get; private set; }
    public BaseState EndState { get; private set; }


    public BattleWorld Create(string battleWorldName, List<VTuberData> playerVTuberList,
        List<VTuberData> enemyVTuberList)
    {
        Name = battleWorldName;

        RunState = new RunState() {World = this};
        ActState = new ActState() {World = this};
        PauseState = new PauseState() {World = this};
        EndState = new EndState() {World = this};
        StartState = new StartState() {World = this};

        //加载战斗场景
        PackedScene scene = ResourceLoader.Load<PackedScene>(PathDefine.ScenePath + "BattleScene.tscn");
        Scene = scene.Instantiate<BattleScene>();
        GameManager.Instance.Game.AddChild(Scene);
        
        AudioManager.Instance.PlayBGM("bgm_fantasy01.mp3");

        for (int i = 0; i < playerVTuberList.Count; i++)
        {
            VTuberLogic vTuber = VTuberFactory.CreateBattleVTuber(playerVTuberList[i], i + 1, Scene.PlayerSeatArr[i]);
            vTuber.Team = TeamEnum.Player;
            _playerVTuberList.Add(vTuber);
            _allVTuberList.Add(vTuber);
        }

        for (int i = 0; i < enemyVTuberList.Count; i++)
        {
            VTuberLogic vTuber = VTuberFactory.CreateBattleVTuber(enemyVTuberList[i], i + 1, Scene.EnemySeatArr[i]);
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
        _currentState?.OnFrameUpdate();
    }

    public void ChangeState(BaseState state)
    {
        _currentState?.OnExit();
        _currentState = state;
        _currentState?.OnEnter();
    }

    public void OnDestroyWorld()
    {
    }

    //toList,临时List,防手贱,在其他地方进行add,remove操作
    /// <returns>所有VTuber逻辑</returns>
    public List<VTuberLogic> AllVTuber() => _allVTuberList.ToList();

    /// <returns>所有敌方队伍VTuber逻辑</returns>
    public List<VTuberLogic> AllEnemyTeamList(TeamEnum team) =>
        team == TeamEnum.Player ? _enemyVTuberList.ToList() : _playerVTuberList.ToList();

    /// <returns>所有友方队伍VTuber逻辑</returns>
    public List<VTuberLogic> AllFriendTeamList(TeamEnum team) =>
        team != TeamEnum.Player ? _enemyVTuberList.ToList() : _playerVTuberList.ToList();

    public void AliveCount()
    {
        _playerAliveCount = 0;
        _enemyAliveCount = 0;
        foreach (VTuberLogic playerVTuber in _playerVTuberList)
        {
            if (playerVTuber.isAlive)
            {
                _playerAliveCount++;
            }
        }

        foreach (VTuberLogic enemyVTuber in _enemyVTuberList)
        {
            if (enemyVTuber.isAlive)
            {
                _enemyAliveCount++;
            }
        }


        if (_playerAliveCount == 0 || _enemyAliveCount == 0)
        {
            if (_playerAliveCount == 0)
            {
                WinTeam = TeamEnum.Enemy;
            }

            ChangeState(EndState);
        }
    }
}