// 文件：SettingManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 19:10

using Godot;
using TinyFramework;

public class SettingManager : Singleton<SettingManager>
{
    public static SettingData SettingData { get; private set; } = new SettingData();

    public override void Init()
    {
        base.Init();
        SettingData = SaveAndLoadManager.Instance.Load<SettingData>();

        AudioManager.Instance.SetBGMVolume(SettingData.BGMVolume);
        AudioManager.Instance.SetAudioVolume(SettingData.AudioVolume);
    }

    public static void Save()
    {
        GD.Print("设定保存");
        SaveAndLoadManager.Instance.Save(SettingData);
    }
}