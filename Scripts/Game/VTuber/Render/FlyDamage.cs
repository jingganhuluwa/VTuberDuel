// 文件：FlyDamage.cs
// 作者：急冻雪柜
// 描述：伤害飘字
// 日期：2024/09/25 20:08


using Godot;

public partial class FlyDamage:Node2D
{
    [Export] private Label _label;

    public void Show(int damage)
    {
        _label.Text = damage.ToString();
        _label.Modulate=Colors.White;
        if (damage<0)
        {
            //治疗
            _label.Modulate=Colors.Green;
        }

        var position = new Vector2(0,-100);
        Tween tween = CreateTween();
        tween.TweenProperty(_label, "position",position,0.8);
        tween.TweenCallback(Callable.From(() =>
        {
            //todo 暂时销毁,后面可能用对象池循环利用
            Free();
        }));
    }
}