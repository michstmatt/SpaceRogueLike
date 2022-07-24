using Godot;
using System;
using System.Collections.Generic;

public class GameManager : Node
{
	protected List<AI> Subscribers = new List<AI>();
	public void AddSubscriber(AI subscriber) => Subscribers.Add(subscriber);

	public void Act()
	{
		foreach(var ai in Subscribers)
		{
			ai.DoMove = true;
		}
	}


}
