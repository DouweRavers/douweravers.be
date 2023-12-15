using Godot;
using System;

public class Main : Control
{
    public override void _Ready()
    {
        var label = (Label) GetChild(0);
        var device = (bool)JavaScript.Eval("/iPhone|iPad|iPod|Android/i.test(navigator.userAgent);")?"Mobile":"Desktop";
        label.Text = "Detect OS: "+device;  
    }
}
