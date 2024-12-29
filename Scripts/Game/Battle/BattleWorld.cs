// 文件：BattleWorld.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/22 18:12


using System.Collections.Generic;
using Godot;
using TinyFramework;

public class BattleWorld
{
    public string Name { get; private set; }

    public BattleScene Scene { get; private set; }

    

    private BaseState _currentState;
    public BaseState StartState { get; private set; }
    public BaseState RunState { get; private set; }
    public BaseState ActState { get; private set; }
    public BaseState PauseState { get; private set; }
    public BaseState EndState { get; private set; }

    public SRandom SRandom { get; private set; }
    
    



    public BattleWorld Create(string battleWorldName, List<VTuberData> playerVTuberList,
        List<VTuberData> enemyVTuberList,uint seed)
    {
        Name = battleWorldName;

        RunState = new RunState() {World = this};
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
            VTuberLogic vTuber = VTuberFactory.CreateBattleVTuber(playerVTuberList[i], i + 1, Scene.PlayerSeatArr[i],TeamEnum.Player,this);

        }

        for (int i = 0; i < enemyVTuberList.Count; i++)
        {
            VTuberLogic vTuber = VTuberFactory.CreateBattleVTuber(enemyVTuberList[i], i + 1, Scene.EnemySeatArr[i],TeamEnum.Enemy,this);

        }

        //改变当前状态为开局转态
        ChangeState(StartState);

        SRandom=new SRandom(seed);
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





    
}