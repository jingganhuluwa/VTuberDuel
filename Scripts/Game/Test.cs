// 文件：Test.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/19 17:28


using Godot;
using TinyFramework;

public partial class Test:Node
{

    private double _count;
    
    public override void _Process(double delta)
    {
        _count += delta;
        if (_count<0.5)
        {
            return;
        }
        if (Input.IsKeyPressed(Key.Q))
        {
            AudioManager.Instance.PlayBGM("healing01.mp3");
            _count = 0;
        }
        if (Input.IsKeyPressed(Key.W))
        {
            AudioManager.Instance.StopBGM();
            _count = 0;
        } 
        if (Input.IsKeyPressed(Key.E))
        {
            AudioManager.Instance.PauseBGM();
            _count = 0;
        }
        if (Input.IsKeyPressed(Key.R))
        {
            AudioManager.Instance.PlayAudio("Gunshot.wav",true);
            _count = 0;
        }
        if (Input.IsKeyPressed(Key.T))
        {
            AudioManager.Instance.SetBGMVolume(1);
            AudioManager.Instance.SetAudioVolume(1);
            _count = 0;
        }

        if (Input.IsKeyPressed(Key.Y))
        {
            SettingManager.SettingData.BGMVolume = 50;
            SettingManager.SettingData.AudioVolume = 60;
            SettingManager.Save();
            _count = 0;
        }if (Input.IsKeyPressed(Key.U))
        {
            UIManager.Instance.AddTips("你好");
            UIManager.Instance.AddTips("你好");
            UIManager.Instance.AddTips("你好");
            UIManager.Instance.AddTips("你好");
            _count = 0;
        }
        
        
        
    }
}