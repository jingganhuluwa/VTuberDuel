// 文件：FlyDamage.cs
// 作者：急冻雪柜
// 描述：伤害飘字
// 日期：2024/09/25 20:08


using Godot;

public partial class FlyDamage:Node3D
{
    [Export] private Label3D _label3D;

    public void Show(int damage)
    {
        _label3D.Text = damage.ToString();
        _label3D.Modulate=Colors.White;
        if (damage<0)
        {
            //治疗
            _label3D.Modulate=Colors.Green;
        }

        var position = new Vector3(0,0.8f,0);
        Tween tween = CreateTween();
        tween.TweenProperty(_label3D, "position",position,0.8);
        tween.TweenCallback(Callable.From(() =>
        {
            //todo 暂时销毁,后面可能用对象池循环利用
            Free();
        }));
    }
}