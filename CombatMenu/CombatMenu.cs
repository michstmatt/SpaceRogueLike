using Godot;
using System;

public class CombatMenu: PopupMenu
{
    private Player parent;
    public override void _Ready()
    {
        base._Ready();
        parent = this.GetParent<Control>().GetParent<Player>();
    }
    public void OnMenuItemSelected(int index) => this.parent?.MenuCallback(index); 
} 