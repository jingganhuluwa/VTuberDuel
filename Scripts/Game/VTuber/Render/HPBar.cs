// 文件：HPBar.cs
// 作者：急冻雪柜
// 描述：
// 日期：2024/09/25 19:09


using Godot;

public partial class HPBar:Control
{
    [Export] private ColorRect _healing;
    [Export] private ColorRect _onHit;
    [Export] private ColorRect _hp;

    private float _currentRate;
    
    public void UpdateHP(float rate)
    {
        Vector2 hpScale = new Vector2(rate,1);
        Tween tween = CreateTween();
        if (rate>_currentRate)
        {
            //治疗
            _healing.Scale = hpScale;
            _onHit.Scale = hpScale;
            _healing.ZIndex = 2;
            _onHit.ZIndex = 1;
            tween.TweenProperty(_hp, "scale",hpScale,0.6);
            
        }
        else if(rate<_currentRate)
        {
            //受击
            _hp.Scale =hpScale ;
            _healing.Scale = hpScale;
            _healing.ZIndex = 1;
            _onHit.ZIndex = 2;
            tween.TweenProperty(_onHit, "scale",hpScale,0.6);
        }
        tween.Play();

        _currentRate = rate;
    }
}