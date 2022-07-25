using Godot;
using System;

public class Player : SpaceShip
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.

	private CombatMenu CombatMenu;

	public override void _Ready()
	{
		base._Ready();
		CombatMenu = GetNode<CombatMenu>("CombatMenu/PopupMenu");
		CombatMenu.AddItem("Blaster");
		CombatMenu.AddItem("Missile 10/10");
	}

	public void MenuCallback(int index)
	{
		Attack();
	}

	public override void _PhysicsProcess(float delta)
	{
		var direction = Vector3.Zero;

		// We check for each move input and update the direction accordingly
		if (Input.IsActionJustReleased("move_right"))
		{
			direction.x += 1f;
		}
		else if (Input.IsActionJustReleased("move_left"))
		{
			direction.x -= 1f;
		}
		else if (Input.IsActionJustReleased("move_back"))
		{
			direction.z += 1f;
		}
		else if (Input.IsActionJustReleased("move_forward"))
		{
			direction.z -= 1f;
		}
		else if (Input.IsActionJustReleased("combat_menu"))
		{
			CombatMenu.Show();
		}

		var moved = Move(direction);

		if (direction != Vector3.Zero)
		{
			manager.Act();
		}
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
