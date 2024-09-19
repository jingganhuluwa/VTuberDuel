// 文件：AudioManager.cs
// 作者：急冻雪柜
// 描述：音效管理器
// 日期：2024/09/19 16:21

using System.Collections.Generic;
using Godot;

namespace TinyFramework;

public partial class AudioManager : SingletonNode<AudioManager>
{
    private readonly AudioStreamPlayer _bgmPlayer=new AudioStreamPlayer();
    private readonly List<AudioStreamPlayer> _audioPlayerList=new List<AudioStreamPlayer>();

    private int _maxAudioPlayerNum = 10;
    private int _audioIndex;

    private float _minPitch = 0.8f;
    private float _maxPitch = 1.8f;


    public override void Init()
    {
        base.Init();
        this.AddChild(_bgmPlayer);
        _bgmPlayer.Name = "BGM";
        for (int i = 0; i < _maxAudioPlayerNum; i++)
        {
            var audioPlayer = new AudioStreamPlayer();
            audioPlayer.Name = "Audio" + i;
            AddChild(audioPlayer);
            _audioPlayerList.Add(audioPlayer);
        }
    }

    public void SetBGMVolume(float volume)
    {
        _bgmPlayer.SetVolumeDb(Mathf.LinearToDb(volume/100));
    }
    public void SetAudioVolume(float volume)
    {
        foreach (AudioStreamPlayer player in _audioPlayerList)
        {
            player.SetVolumeDb(Mathf.LinearToDb(volume/100));
        }
    }
    
    
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioStream"></param>
    public void PlayBGM(AudioStream audioStream)
    {
        if (audioStream != null)
        {
            _bgmPlayer.Stream = audioStream;
            _bgmPlayer.Play();
        }
    }

    public void PlayBGM(string name)
    {
        AudioStream audioStream = ResourceLoader.Load<AudioStream>(PathDefine.AudioPath + name);

        PlayBGM(audioStream);
    }

    public void StopBGM()
    {
        _bgmPlayer.Stop();
    }

    public void PauseBGM()
    {
        _bgmPlayer.StreamPaused = !_bgmPlayer.StreamPaused;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioStream"></param>
    /// <param name="isRandPitch">随机声音强度</param>
    public void PlayAudio(AudioStream audioStream,bool isRandPitch=false)
    {
        if (audioStream != null)
        {
            if (_audioIndex>_maxAudioPlayerNum)
            {
                _audioIndex = 0;
            }
            AudioStreamPlayer player = _audioPlayerList[_audioIndex];
            if (isRandPitch)
            {
                player.PitchScale = (float) GD.RandRange(_minPitch, _maxPitch);
            }
            else
            {
                player.PitchScale = 1;
            }
            player.Stream = audioStream;
            player.Play();
            _audioIndex++;
        }
    }
    
    public void PlayAudio(string name,bool isRandPitch=false)
    {
        AudioStream audioStream = ResourceLoader.Load<AudioStream>(PathDefine.AudioPath + name);

        PlayAudio(audioStream,isRandPitch);
    }
}