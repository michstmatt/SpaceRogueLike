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

	public override bool IsDead()
	{
		var dead = base.IsDead();
		if (dead)
		{
			this.manager.RemoveSubscriber(this);

			this.Translation = new Vector3(0, -10000, 0);
		}

		return dead;
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
