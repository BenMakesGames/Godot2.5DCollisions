using Godot;

namespace TwoPointFiveDee;

public sealed partial class Player : CharacterBody2D, ISwitchesZ
{
	[Export]
	public int Speed { get; set; } = 200;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		var inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;

		MoveAndSlide();
	}

	public int Z
	{
		get => ZIndex;
		set
		{
			ZIndex = value;
			CollisionLayer = (uint)(1 << (value - 1));
			CollisionMask = (uint)(1 << (value - 1));
		}
	}
}
