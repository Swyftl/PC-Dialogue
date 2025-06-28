using Godot;
using Godot.Collections;

namespace DialoguePlugin.addons.phantomcloud_dialogue;

[Tool]
public partial class Save : Button
{
    private GraphEdit _graphEdit;
    private Resource _saveData;

    public override void _Ready()
    {
        _graphEdit = GetNode<GraphEdit>(GetParent().GetChild(0).GetPath());
    }

    private void _on_pressed()
    {
        /*
         * Create the variable of the resource that we intend to save
         * Then store all the nodes, and the connections to the nodes based on their ID in the array
         * Then save that to the file system
         */
        
        GD.Print("Save Pressed");

        _saveData = new DialogueSave();

        var SavedNodes = new Array<int>();

        foreach (Node node in _graphEdit.GetChildren())
        {
            if (node is GraphNode graphNode)
            {
                switch (graphNode.Name)
                {
                    case "startNode":
                        SavedNodes.Add(0);
                        break;
                    case "dialogueNode":
                        SavedNodes.Add(1);
                        break;
                    case "choiceNode":
                        SavedNodes.Add(2);
                        break;
                    case "endNode":
                        SavedNodes.Add(3);
                        break;
                }
            }
        }
        var NodeConnections = _graphEdit.GetConnectionList();
        GD.Print(NodeConnections);
        
        var SavedConnections = NodeConnections;

        if (_saveData is DialogueSave saveDataDialogue)
        {
            saveDataDialogue.DialogueNodes = SavedNodes;
            saveDataDialogue.Connections = SavedConnections;
            _saveData = saveDataDialogue;
        }

        _get_save_path();
    }

    private void _get_save_path()
    {
        var fileDialog = new FileDialog();
        fileDialog.FileMode = FileDialog.FileModeEnum.SaveFile;
        fileDialog.Access = FileDialog.AccessEnum.Resources;
        fileDialog.SetFileNameFilter("*.tres");

        fileDialog.FileSelected += _on_path_selected;
        
        var viewport = GetViewport();
        viewport.AddChild(fileDialog);
        fileDialog.Popup(new Rect2I(new Vector2I(100, 100), new Vector2I(100, 100)));
    }

    private void _on_path_selected(string path)
    {
        var SaveResult = ResourceSaver.Save(_saveData, path);
        GD.Print(path);
    }
}