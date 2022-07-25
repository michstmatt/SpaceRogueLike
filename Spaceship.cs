
using Godot;
using System;

public abstract class SpaceShip : KinematicBody
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

    private int _health = 100;
    [Export]
    public int Health {
        get => _health;
        set
        {
            _health = value;
            IsDead();
        }
    }

    protected Vector3 _velocity = Vector3.Zero;

    protected Vector3 target = Vector3.Zero;

    protected GameManager manager;

    protected float HoverDelta = 0.001f;
    private float MaxHeight;
    private float MinHeight;

    protected Direction CurrentDirection;

    public virtual bool IsDead()
    {
        return Health <= 0;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        target = this.Translation;
        MaxHeight = this.Translation.y + .1f;
        MinHeight = this.Translation.y - .1f;
        manager = GetNode<GameManager>("/root/Main");
    }


    public SpaceShip GetEnemySpaceShip()
    {
        Vector3 pos = this.target;

        if (CurrentDirection == Direction.Right)
            pos += new Vector3(TileSize, 0, 0);
        else if (CurrentDirection == Direction.Left)
            pos += new Vector3(-TileSize, 0, 0);
        else if (CurrentDirection == Direction.Up)
            pos += new Vector3(0, 0, -TileSize);
        else
            pos += new Vector3(0, 0, TileSize);
        

        var enemy = manager.GetAI(pos);
        return enemy;
    }

    public void Attack()
    {
        Console.WriteLine("{0}", this.target);
        var spaceShip = GetEnemySpaceShip();
        spaceShip.Health -= 50;
    }

    public bool Move(Vector3 direction)
    {
        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            GetNode<Spatial>("Pivot").LookAt(Translation + direction, Vector3.Up);
        }

        if (direction.x > 0f)
        {
            CurrentDirection = Direction.Right;
        }
        else if (direction.x < 0f)
        {
            CurrentDirection = Direction.Left;
        }
        else if (direction.z > 0f)
        {
            CurrentDirection = Direction.Down;
        }
        else if (direction.z < 0f)
        {
            CurrentDirection = Direction.Up;
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

        this.Translation += new Vector3(0, HoverDelta, 0);

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
