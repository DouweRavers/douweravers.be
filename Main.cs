using Godot;
using System;

public class Main : Control
{
    public override void _Ready()
    {
        var label = (Label) GetChild(0);
        label.Text = "Hello from C#!";
    }
}
