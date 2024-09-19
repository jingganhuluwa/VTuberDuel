// 文件：PathDefine.cs
// 作者：急冻雪柜
// 描述：路径定义
// 日期：2024/09/19 16:29

using Godot;

namespace TinyFramework;

public static class PathDefine
{
    public const string ResPath = "res://Resources/";
    public const string AudioPath = ResPath + "Audio/";
    public const string ScenePath = ResPath + "Scene/";
    public const string PrefabsPath = ResPath + "Prefabs/";
    public const string UIPath = ResPath + "UI/";
    public static string SavePath => ProjectSettings.GlobalizePath("user://");
}