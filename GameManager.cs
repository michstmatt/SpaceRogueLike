using Godot;
using System;
using System.Collections.Generic;

public class GameManager : Node
{
	protected List<AI> Subscribers = new List<AI>();
	public void AddSubscriber(AI subscriber) => Subscribers.Add(subscriber);
	public void RemoveSubscriber(AI subscriber) => Subscribers.Remove(subscriber);

	public void Act()
	{
		foreach(var ai in Subscribers)
		{
			ai.DoMove = true;
		}
	}

	public AI GetAI(Vector3 target)
	{
		AI ret = null;
		foreach(var ai in Subscribers)
		{
			var dist = target - ai.Translation;
			dist.y = 0;
			Console.WriteLine("ai: {0} {1}", ai.Translation, dist.Length());
			if (dist.Length() <= 1.0f)
			{
				ret = ai;
				break;
			}
		}

		return ret;
	}


}
