using Godot;
using Godot.Collections;

namespace DialoguePlugin.addons.phantomcloud_dialogue;

[Tool]
public partial class Save : Button
{
    private GraphEdit graphEdit;
    private Resource SaveData;

    public override void _Ready()
    {
        graphEdit = GetNode<GraphEdit>(GetParent().GetChild(0).GetPath());
    }

    private void _on_pressed()
    {
        /*
         * Create the variable of the resource that we intend to save
         * Then store all the nodes, and the connections to the nodes based on their ID in the array
         * Then save that to the file system
         */
        
        GD.Print("Save Pressed");

        SaveData = new DialogueSave();

        var SavedNodes = new Array<int>();
        var SavedConnections = new Array<Dictionary>();
        
        foreach (Node node in graphEdit.GetChildren())
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
        var NodeConnections = graphEdit.GetConnectionList();
        GD.Print(NodeConnections);
        
        SavedConnections = NodeConnections;

        if (SaveData is DialogueSave saveDataDialogue)
        {
            saveDataDialogue.DialogueNodes = SavedNodes;
            saveDataDialogue.Connections = SavedConnections;
            SaveData = saveDataDialogue;
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
        var SaveResult = ResourceSaver.Save(SaveData, path);
        GD.Print(path);
    }
}