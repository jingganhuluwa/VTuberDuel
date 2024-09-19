// 文件：StartPanel.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 20:48

using Godot;
using TinyFramework;

public partial class StartPanel:Panel
{
    [Export]
    private Button StartButton;
    [Export]
    private Button SettingButton;
    [Export]
    private Button ExitButton;

    public override void _Ready()
    {
        StartButton.Pressed += StartButtonClick;
        SettingButton.Pressed += SettingButtonClick;
        ExitButton.Pressed += ExitButtonClick;
    }

    private void ExitButtonClick()
    {
        
    }

    private void SettingButtonClick()
    {
        UIManager.Instance.ShowPanel<SettingPanel>();
    }

    private void StartButtonClick()
    {
        
    }

    public override void _ExitTree()
    {
        StartButton.Pressed -= StartButtonClick;
        SettingButton.Pressed -= SettingButtonClick;
        ExitButton.Pressed -= ExitButtonClick;
    }
    
    
}