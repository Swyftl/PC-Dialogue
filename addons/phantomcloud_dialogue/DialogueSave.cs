using System.Collections.Generic;
using System.Reflection;
using Godot;
using Godot.Collections;

namespace DialoguePlugin.addons.phantomcloud_dialogue
{
    [GlobalClass]
    public partial class DialogueSave : Resource
    {
        [Export] public Array<int> DialogueNodes;
        [Export] public Array<Dictionary> Connections;

        public DialogueSave(Array<int> dialogueNodes, Array<Dictionary> connections)
        {
            DialogueNodes = dialogueNodes;
            Connections = connections;
        }

        public DialogueSave() : this(null, null)
        {
        }
    }
}