using Godot;
using System;

public partial class RightClickContext : VBoxContainer
{
    public override void _Ready()
    {
        Visible = false;
    }

    private void _show_context_menu()
    {
        Position = GetGlobalMousePosition();
        Visible = true;
    }
}
