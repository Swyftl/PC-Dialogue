using Godot;
using System;

[Tool]
public partial class RightClickContext : Tree
{
    public override void _Ready()
    {
        Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        // Check if the right-mouse button was clicked
        if (@event is InputEventMouseButton button)
        {
            if (button.ButtonIndex == MouseButton.Right)
            {
                _show_context_menu();
            }
        }
    }

    private void _show_context_menu()
    {
        var mousePosition = GetGlobalMousePosition();
        var Scale = GetScale();
        Position = new Vector2(mousePosition.X, mousePosition.Y);
        Visible = true;
    }

    private void _hide_context_menu()
    {
        Visible = false;
    }
}
