using Godot;

namespace TwoPointFiveDee;

public interface IZTraveller
{
    int Z { get; set; }
    Vector2 Velocity { get; }
}
