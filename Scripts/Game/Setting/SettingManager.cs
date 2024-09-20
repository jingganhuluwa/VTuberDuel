// 文件：SettingManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 19:10

using Godot;
using TinyFramework;

public class SettingManager : Singleton<SettingManager>
{
    public  SettingData SettingData { get; private set; } = new SettingData();

    public override void Init()
    {
        base.Init();
        SettingData = SaveAndLoadManager.Instance.Load<SettingData>();

        AudioManager.Instance.SetBGMVolume(SettingData.BGMVolume);
        AudioManager.Instance.SetAudioVolume(SettingData.AudioVolume);
        
        if (SettingData.IsBGMMute)
        {
            AudioManager.Instance.SetBGMVolume(0);
        }

        if (SettingData.IsAudioMute)
        {
            AudioManager.Instance.SetAudioVolume(0);
        }
    }

    public  void Save()
    {
        GD.Print("设定保存");
        SaveAndLoadManager.Instance.Save(SettingData);
    }
}