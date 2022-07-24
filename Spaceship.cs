
using Godot;
using System;

public abstract class SpaceShip: KinematicBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public int Speed = 10;

    [Export]
    public int TileSize = 4;

    [Export]
    public int XOffset = 2;

    [Export]
    public int YOffset = 2;

    protected Vector3 _velocity = Vector3.Zero;

    protected Vector3 target = Vector3.Zero;

	protected GameManager manager;

    protected float HoverDelta = 0.001f;
    private float MaxHeight;
    private float MinHeight;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        target = this.Translation;
        MaxHeight = this.Translation.y + .1f;
        MinHeight = this.Translation.y - .1f;
		manager = GetNode<GameManager>("/root/Main");
    } 

    public bool Move(Vector3 direction)
    {
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			GetNode<Spatial>("Pivot").LookAt(Translation + direction, Vector3.Up);
		}

        var move = direction * TileSize;

        var collides = TestMove(this.Transform, move);

        if (collides)
        {
            return false;
        }

        target += direction * TileSize;
        _velocity.y = 0;

        var distance = target - Translation;
        if (distance.Length() < .01)
        {
            //_velocity = Vector3.Zero;
            //this.Translation = Translation.Snapped(Vector3.One);
        }
        else
        {
            _velocity.x = distance.x * Speed;
            _velocity.z = distance.z * Speed;
        }
        _velocity = MoveAndSlide(_velocity, Vector3.Up);

        return true;
    }

    public override void _Process(float delta)
    {
        if (this.Translation.y > MaxHeight || this.Translation.y < MinHeight)
        {
            HoverDelta = -HoverDelta;
        }

        this.Translation += new Vector3(0,HoverDelta,0);

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
