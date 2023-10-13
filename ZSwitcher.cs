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
		GD.Print("ZSwitcher._EnterTree");
		BodyExited += OnBodyExited;
	}

	public override void _ExitTree()
	{
		GD.Print("ZSwitcher._ExitTree");
		BodyExited -= OnBodyExited;
	}

	public void OnBodyExited(Node2D body)
	{
		GD.Print($"ZSwitcher.OnBodyExited(#{body.GetInstanceId()})");

		if (body is not ISwitchesZ switches)
			return;

		GD.Print($"  old body Z: {switches.Z}");
		GD.Print($"  velocity: ({switches.Velocity.X}, {switches.Velocity.Y})");

		GD.Print($"  LowerZ: {LowerZ}");
		GD.Print($"  UpperZ: {LowerZ}");
		GD.Print($"  UpDirection: {UpDirection}");

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

		GD.Print($"  new body Z: {switches.Z}");
	}
}

public enum Direction
{
	North,
	East,
	South,
	West
}
