using Godot;

public partial class BattleScene : Node
{
	[Export] public Node2D[] PlayerSeatArr; 
	[Export] public Node2D[] EnemySeatArr; 
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
