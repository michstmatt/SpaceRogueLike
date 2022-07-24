using Godot;
using System;

public class Grid : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public int Rows = 100;
	
	[Export]
	public int Columns = 100;

	[Export]
	public int MinRoomDistance = 12;

	[Export]
	public int MaxRoomDistance = 24;

	[Export]
	public int MinRooms = 3;

	[Export]
	public int MaxRooms = 9;

	[Export]
	public int MinRoomSize = 5;

	[Export]
	public int MaxRoomSize = 10;

	[Export]
	public int FloorId = 0;

	protected Random rng = new Random();

	protected void setEmptyCellItem(GridMap gridMap, int x, int y, int item)
	{
		var current = gridMap.GetCellItem(x, 0, y);
		if (current == FloorId)
		{
			return;
		}
		gridMap.SetCellItem(x, 0, y, item);	
	}

	protected Direction getNewDirection (Direction current)
	{
		Direction newDirection = current;

		while (newDirection == current || newDirection == current.Opposite())
		{
			var val = rng.Next(0, 4);
			newDirection = (Direction)val;
		}

		return newDirection;
	}

	protected void MakeRoom(GridMap gridMap, int x, int y)
	{
		var width = rng.Next(MinRoomSize, MaxRoomSize + 1) / 2 + 1;
		var height = rng.Next(MinRoomSize, MaxRoomSize + 1) / 2 + 1;

		int id;
		int cX, cY;

		for(int xD = -width; xD <= width; xD++)
			for(int yD = -height; yD <= height; yD++)
			{
				cX = x + xD;
				cY = y + yD;
				var current = gridMap.GetCellItem(cX, 0, cY);

				if (xD == -width || xD == width || yD == -height || yD == height)
					id = 1;	
				else 
					id = FloorId;
				
				setEmptyCellItem(gridMap, x+xD, y+yD, id);
			}
	
	}

	protected (int, int) MakeHallway(GridMap gridMap, Direction direction, int x, int y)
	{
		var length = rng.Next(MinRoomDistance, MaxRoomDistance + 1);

		int xD = 0, yD = 0;
		if (direction == Direction.Up)	
			yD = -1;
		else if (direction == Direction.Down)
			yD = 1;
		else if (direction == Direction.Left)
			xD = -1;
		else
			xD = 1;
		
		for(int d = 0; d < length; d++)
		{

			if (xD == 0)
			{
				setEmptyCellItem(gridMap, x-1, y, 1);
				setEmptyCellItem(gridMap, x+1, y, 1);
			}
			else
			{
				setEmptyCellItem(gridMap, x, y-1, 1);
				setEmptyCellItem(gridMap, x, y+1, 1);
			}
			setEmptyCellItem(gridMap, x, y, 0);

			x += xD;
			y += yD;
		}
		return (x,y);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GridMap gridMap = GetNode<GridMap>("GridMap");
		int x = 0, y = 0;

		var numRooms = rng.Next(MinRooms, MaxRooms + 1);
		MakeRoom(gridMap, x, y);

		var direction = Direction.None;

		for(int room = 1; room <= numRooms; room++)
		{
			direction = getNewDirection(direction);
			(x,y) = MakeHallway(gridMap, direction, x, y);
			MakeRoom(gridMap, x, y);
		}

		//gridMap.UpdateBitmaskRegion();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
