using Godot;
using System;

[Tool]
public partial class GraphEdit : Godot.GraphEdit
{

    private void _on_connection_request(StringName fromNode, int fromPort, StringName toNode, int toPort)
    {
        ConnectNode(fromNode, fromPort, toNode, toPort);
    }

    private void _on_delete_node_pressed()
    {
        foreach (Node node in GetChildren())
        {
            if (node is GraphNode { Selected: true } graphNode)
            {
                graphNode.QueueFree();
            }
        }
    }

    private void _on_disconnection_request(StringName fromNode, int fromPort, StringName toNode, int toPort)
    {
        DisconnectNode(fromNode, fromPort, toNode, toPort);
    }
}
