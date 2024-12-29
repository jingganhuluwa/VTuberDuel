using Godot;

public partial class ItemRender : Sprite2D
{
    public Vector2 TargetPosition = new Vector2(0, 0);

    private ItemController _itemController => GetParent().GetParent<ItemController>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnMouseEnter()
    {
        // GD.Print("OnMouseEnter");
        _itemController.HoveredItem(this);
    }

    public void OnMouseExit()
    {
        // GD.Print("OnMouseExit");
        _itemController.HoveredOffItem(this);
    }
}