// 文件：SettingPanel.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 21:12


using Godot;
using TinyFramework;

public partial class SettingPanel:Panel
{
    [Export]
    private HSlider BGMSlider;
    [Export]
    private HSlider AudioSlider;
    [Export]
    private CheckButton BGMCheckButton;
    [Export]
    private CheckButton AudioCheckButton;
    [Export]
    private Button SaveButton;

    public override void _Ready()
    {
        base._Ready();
        BGMSlider.SetValue(SettingManager.SettingData.BGMVolume);
        AudioSlider.SetValue(SettingManager.SettingData.AudioVolume);
        BGMSlider.ValueChanged += ChangeBGMVolume;
        AudioSlider.ValueChanged += ChangeAudioVolume;
        SaveButton.Pressed += SaveButtonClick;
    }

    private void SaveButtonClick()
    {
        SettingManager.Save();
        Hide();
    }

    private void ChangeAudioVolume(double value)
    {
        AudioManager.Instance.SetAudioVolume((float) value);
        SettingManager.SettingData.AudioVolume = (int) value;
    }

    private void ChangeBGMVolume(double value)
    {
        AudioManager.Instance.SetBGMVolume((float) value);
        SettingManager.SettingData.BGMVolume = (int) value;
    }
}