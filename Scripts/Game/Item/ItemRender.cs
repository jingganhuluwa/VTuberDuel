using Godot;

public partial class ItemRender : Sprite2D
{
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
        ItemController.Instance.HoveredItem(this);
    }
    public void OnMouseExit()
    {
        // GD.Print("OnMouseExit");
        ItemController.Instance.HoveredOffItem(this);
    }
}