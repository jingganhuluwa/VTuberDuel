// 文件：StartPanel.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 20:48

using System.Collections.Generic;
using Godot;
using TinyFramework;

public partial class StartPanel : Panel
{
    [Export] private Button StartButton;
    [Export] private Button SettingButton;
    [Export] private Button ExitButton;

    public override void _Ready()
    {
        StartButton.Pressed += StartButtonClick;
        SettingButton.Pressed += SettingButtonClick;
        ExitButton.Pressed += ExitButtonClick;

        AudioManager.Instance.PlayBGM("healing01.mp3");
    }

    private void ExitButtonClick()
    {
    }

    private void SettingButtonClick()
    {
        UIManager.Instance.ShowUI<SettingPanel>();
    }

    private void StartButtonClick()
    {
        var playerVTuberDatas = new List<VTuberData>();
        int idCount = 0;
        for (int i = 0; i < 5; i++)
        {
            VTuberData data = new VTuberData
            {
                Id = idCount++,
                ConfigId = 1001 + i,
                Speed = GD.RandRange(40, 100),
                HP = GD.RandRange(400, 1000),
                Atk = GD.RandRange(50, 100),
                Amor = GD.RandRange(5, 30)
            };
            playerVTuberDatas.Add(data);
        }

        var enemyVTuberDatas = new List<VTuberData>();
        for (int i = 0; i < 5; i++)
        {
            VTuberData data = new VTuberData
            {
                Id = idCount++,
                ConfigId = 1001 + i,
                Speed = GD.RandRange(40, 100),
                HP = GD.RandRange(400, 1000),
                Atk = GD.RandRange(50, 100),
                Amor = GD.RandRange(5, 30)
            };
            enemyVTuberDatas.Add(data);
        }


        BattleWorldManager.Instance.CreateBattleWorld("5v5", playerVTuberDatas, enemyVTuberDatas);


        Hide();
    }

    public override void _ExitTree()
    {
        StartButton.Pressed -= StartButtonClick;
        SettingButton.Pressed -= SettingButtonClick;
        ExitButton.Pressed -= ExitButtonClick;
    }
}