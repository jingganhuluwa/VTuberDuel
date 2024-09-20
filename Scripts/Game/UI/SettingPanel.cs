// 文件：SettingPanel.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 21:12


using Godot;
using TinyFramework;

public partial class SettingPanel : Panel
{
    [Export] private HSlider BGMSlider;
    [Export] private HSlider AudioSlider;
    [Export] private CheckButton BGMCheckButton;
    [Export] private CheckButton AudioCheckButton;
    [Export] private Button SaveButton;

    public override void _Ready()
    {
        base._Ready();
        BGMSlider.SetValue(SettingManager.Instance.SettingData.BGMVolume);
        AudioSlider.SetValue(SettingManager.Instance.SettingData.AudioVolume);
        if (SettingManager.Instance.SettingData.IsBGMMute)
        {
            BGMCheckButton.ButtonPressed = false;
        }

        if (SettingManager.Instance.SettingData.IsAudioMute)
        {
            AudioCheckButton.ButtonPressed = false;
        }


        BGMSlider.ValueChanged += ChangeBGMVolume;
        AudioSlider.ValueChanged += ChangeAudioVolume;
        BGMCheckButton.Pressed += MuteBGM;
        AudioCheckButton.Pressed += MuteAudio;
        SaveButton.Pressed += SaveButtonClick;
    }

    private void MuteAudio()
    {
        if (AudioCheckButton.ButtonPressed)
        {
            AudioManager.Instance.SetAudioVolume(SettingManager.Instance.SettingData.AudioVolume);
            SettingManager.Instance.SettingData.IsAudioMute = false;
            return;
        }

        AudioManager.Instance.SetAudioVolume(0);
        SettingManager.Instance.SettingData.IsAudioMute = true;
    }

    private void MuteBGM()
    {
        if (BGMCheckButton.ButtonPressed)
        {
            AudioManager.Instance.SetBGMVolume(SettingManager.Instance.SettingData.BGMVolume);
            SettingManager.Instance.SettingData.IsBGMMute = false;
            return;
        }

        AudioManager.Instance.SetBGMVolume(0);
        SettingManager.Instance.SettingData.IsBGMMute = true;
    }

    private void SaveButtonClick()
    {
        SettingManager.Instance.Save();
        Hide();
    }

    private void ChangeAudioVolume(double value)
    {
        AudioManager.Instance.SetAudioVolume((float) value);
        SettingManager.Instance.SettingData.AudioVolume = (int) value;
    }

    private void ChangeBGMVolume(double value)
    {
        AudioManager.Instance.SetBGMVolume((float) value);
        SettingManager.Instance.SettingData.BGMVolume = (int) value;
    }
}