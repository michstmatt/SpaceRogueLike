using Godot;
using System;

public class AI : SpaceShip
{
	public bool DoMove;
	public override void _Ready()
	{
		base._Ready();
		this.manager.AddSubscriber(this);
	}
	public override void _PhysicsProcess(float delta)
	{
		var direction = new Vector3(1,0,0);
		if (DoMove)
		{
			//Move(direction);
			DoMove = false;
		}
	}
}
