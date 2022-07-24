using Godot;
using System;
using System.Collections.Generic;

public class Room: Node
{
	[Export]
	public int Rows = 10;
	
	[Export]
	public int Columns = 10;

	[Export]
	public int CellSize = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GridMap gridMap = GetNode<GridMap>("GridMap");
		
		for(int row = 0; row < Rows; row ++)
		{
			for(int col = 0; col < Columns; col ++)
			{
				int index = 0;
				int rotation = 0;
				int height = 0;
				// Wall
				if (row == 0 || row == (Rows - 1))
				{
					index = 1;
				}
				else if (col == 0 || col == (Columns - 1))
				{
					index = 1;
					//rotation = 22;
				}
				else
				{
				}
				gridMap.SetCellItem(row, height, col , index, rotation);
			}
		}
	}
}
