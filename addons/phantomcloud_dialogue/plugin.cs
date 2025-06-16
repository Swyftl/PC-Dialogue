#if TOOLS
using Godot;
using System;

[Tool]
public partial class plugin : EditorPlugin
{
    private PackedScene MainPanel =
        ResourceLoader.Load<PackedScene>("res://addons/phantomcloud_dialogue/dialogue_dock.tscn");

    private Control MainPanelInstance;

    public override void _EnterTree()
    {
        MainPanelInstance = (Control)MainPanel.Instantiate();
        // Add the main panel to the editor's main viewport.
        EditorInterface.Singleton.GetEditorMainScreen().AddChild(MainPanelInstance);
        // Hide the main panel. Very much required.
        _MakeVisible(false);

        var settings = EditorInterface.Singleton.GetEditorSettings();

        string settingName = "Phantom_Cloud_Settings/Dialogue/General/TestSetting";

        if (!settings.HasSetting(settingName))
        {
            settings.SetSetting(settingName, true);

            var propertyInfo = new Godot.Collections.Dictionary
            {
                { "name", settingName },
                { "type", (int)Variant.Type.Bool },
                { "hint", (int)PropertyHint.None },
                { "usage", (int)PropertyUsageFlags.Default }
            };

            settings.AddPropertyInfo(propertyInfo);
        }
    }

    public override void _ExitTree()
    {
        if (MainPanelInstance != null)
        {
            MainPanelInstance.QueueFree();
        }
    }

    public override bool _HasMainScreen()
    {
        return true;
    }

    public override void _MakeVisible(bool visible)
    {
        if (MainPanelInstance != null)
        {
            MainPanelInstance.Visible = visible;
        }
    }

    public override string _GetPluginName()
    {
        return "PC_Dialogue";
    }

    public override Texture2D _GetPluginIcon()
    {
        return EditorInterface.Singleton.GetEditorTheme().GetIcon("Node", "EditorIcons");
    }
}
#endif