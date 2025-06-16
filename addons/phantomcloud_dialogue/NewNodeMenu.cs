using System.Text;
using Godot;

namespace DialoguePlugin.addons.phantomcloud_dialogue;

[Tool]
public partial class NewNodeMenu : MenuButton
{
    private GraphEdit NodeGraph;
    private PopupMenu PopupMenu;

    public override void _Ready()
    {
        GD.Print(GetTree().Root.GetChildren());
        NodeGraph = GetNode<GraphEdit>(GetParent().GetChild(0).GetPath());
        PopupMenu = GetPopup();

        PopupMenu.IdPressed += _on_menu_pressed;
    }
    
    private void _on_menu_pressed(long id)
    {
        // Check for the ID of the button that was pressed
        // 0 - Start
        // 1 - Dialogue
        // 2 - Choice
        // 3 - End

        switch (id)
        {
            case 0:
                GD.Print("Making new start node");
                _create_start_node();
                break;
            case 1:
                GD.Print("Making new dialogue node");
                _create_dialogue_node();
                break;
            case 2:
                _create_choice_node();
                GD.Print("Making new choice node");
                break;
            case 3:
                _create_end_node();
                GD.Print("Making new end node");
                break;
        }
    }

    private void _create_start_node()
    {
        var startFound = false;
        
        foreach (Node node in NodeGraph.GetChildren())
        {
            if (node is GraphNode && node.IsInGroup("StartNode"))
            {
                startFound = true;
            }
        }


        if (!startFound)
        {
            // Create the node and assign values
            var startNode = new GraphNode();
            startNode.Name = "startNode";
            startNode.Title = "Start";
            startNode.Size = new Vector2(75, 75);
            startNode.Position = new Vector2(0, 0);
            // Assigns the slots to their id
            // Assigns the position of the slot and the colour
            startNode.SetSlot(0,
                false, 1, Colors.Blue,
                true, 1, Colors.Blue);
        
            // Create the port
            var port = new Control();
        
            startNode.AddToGroup("StartNode");
        
            startNode.AddChild(port);
            NodeGraph.AddChild(startNode);
        }
    }
    private void _create_end_node()
    {
        var endFound = false;
        
        foreach (Node node in NodeGraph.GetChildren())
        {
            if (node is GraphNode && node.IsInGroup("EndNode"))
            {
                endFound = true;
            }
        }

        if (!endFound)
        {
            var endNode = new GraphNode();
            endNode.Name = "endNode";
            endNode.Title = "End";
            endNode.Size = new Vector2(75, 75);
            endNode.Position = new Vector2(0, 0);
        
            endNode.SetSlot(0,
                true, 1, Colors.Blue,
                false, 1, Colors.Blue);

            var port = new Control();
        
            endNode.AddToGroup("EndNode");
        
            endNode.AddChild(port);
            NodeGraph.AddChild(endNode);
        }
    }
    
    /*
     * The dialogue node contains a text area that allows the user to show text
     * Later it will also allow the dialogue to have 'Characters' to show their name
     * next to the text
     */
    
    private void _create_dialogue_node()
    {
        var dialogueNode = new GraphNode();
        dialogueNode.Name = "dialogueNode";
        dialogueNode.Title = "Dialogue";
        dialogueNode.Size = new Vector2(75, 75);
        dialogueNode.Position = new Vector2(0, 0);
        dialogueNode.Resizable = true;
        
        dialogueNode.SetSlot(0,
            true, 1, Colors.Blue,
            true, 1, Colors.Blue);

        var port = new Control();
        var textEdit = new LineEdit();
        
        dialogueNode.AddToGroup("DialogueNode");
        
        dialogueNode.AddChild(port);
        dialogueNode.AddChild(textEdit);
        
        NodeGraph.AddChild(dialogueNode);
    }

    /*
     * The choice node is a different from the rest
     * Requiring that the user can add their own amount of choices to the dialogue
     * So it has to be updated each time the user adds or removes a dialogue option
     */
    
    private void _create_choice_node()
    {
        var choiceNode = new GraphNode();
        choiceNode.Name = "choiceNode";
        choiceNode.Title = "Choice";
        choiceNode.Size = new Vector2(150, 75);
        choiceNode.Position = new Vector2(0, 0);
        choiceNode.Resizable = true;
        
        choiceNode.SetSlot(0,
            true, 1, Colors.Blue,
            false, 1, Colors.Blue);
        choiceNode.SetSlot(1,
            false, 1, Colors.Blue,
            true, 1, Colors.Blue);
        choiceNode.SetSlot(2,
            false, 1, Colors.Blue,
            true, 1, Colors.Blue);
        choiceNode.SetSlot(3,
            false, 1, Colors.Blue,
            true, 1, Colors.Blue);

        var port = new Control();
        var DialogueLabel = new RichTextLabel();
        var Dialogue = new LineEdit();
        var OptionsLabel = new RichTextLabel();
        var OptionOne = new LineEdit();
        var OptionTwo = new LineEdit();
        var OptionThree = new LineEdit();

        DialogueLabel.Text = "Dialogue";
        OptionsLabel.Text = "Options";
        DialogueLabel.SetCustomMinimumSize(new Vector2(50, 35));
        OptionsLabel.SetCustomMinimumSize(new Vector2(50, 35));
        
        choiceNode.AddToGroup("ChoiceNode");
        
        choiceNode.AddChild(DialogueLabel);
        choiceNode.AddChild(Dialogue);
        choiceNode.AddChild(OptionsLabel);
        choiceNode.AddChild(OptionOne);
        choiceNode.AddChild(OptionTwo);
        choiceNode.AddChild(OptionThree);
        choiceNode.AddChild(port);
        NodeGraph.AddChild(choiceNode);
    }
}