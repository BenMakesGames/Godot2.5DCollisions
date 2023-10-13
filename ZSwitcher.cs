using System;
using Godot;

namespace TwoPointFiveDee;

public sealed partial class ZSwitcher : Area2D
{
	[Export]
	public int LowerZ { get; set; }

	[Export]
	public int UpperZ { get; set; }

	[Export]
	public Direction UpDirection { get; set; }

	public override void _EnterTree()
	{
		CollisionLayer = (uint)(1 << (LowerZ - 1)) + (uint)(1 << (UpperZ - 1));
		CollisionMask = (uint)(1 << (LowerZ - 1)) + (uint)(1 << (UpperZ - 1));

		BodyExited += OnBodyExited;
	}

	public override void _ExitTree()
	{
		BodyExited -= OnBodyExited;
	}

	public void OnBodyExited(Node2D body)
	{
		if (body is not IZTraveller switches)
			return;

		if (switches.Z == LowerZ)
		{
			if (UpDirection == Direction.North && switches.Velocity.Y < 0)
				switches.Z = UpperZ;

			if (UpDirection == Direction.East && switches.Velocity.X > 0)
				switches.Z = UpperZ;

			if (UpDirection == Direction.South && switches.Velocity.Y > 0)
				switches.Z = UpperZ;

			if (UpDirection == Direction.West && switches.Velocity.X < 0)
				switches.Z = UpperZ;
		}
		else if (switches.Z == UpperZ)
		{
			if (UpDirection == Direction.North && switches.Velocity.Y > 0)
				switches.Z = LowerZ;

			if (UpDirection == Direction.East && switches.Velocity.X < 0)
				switches.Z = LowerZ;

			if (UpDirection == Direction.South && switches.Velocity.Y < 0)
				switches.Z = LowerZ;

			if (UpDirection == Direction.West && switches.Velocity.X > 0)
				switches.Z = LowerZ;
		}
	}
}

public enum Direction
{
	North,
	East,
	South,
	West
}
