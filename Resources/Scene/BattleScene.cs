using Godot;

public partial class BattleScene : Node
{
	[Export] public Node3D[] PlayerSeatArr; 
	[Export] public Node3D[] EnemySeatArr; 
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
