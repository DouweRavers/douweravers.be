using Godot;
using System;

public class Main : Node
{
	public bool IsMobile { get => (bool)JavaScript.Eval("/iPhone|iPad|iPod|Android/i.test(navigator.userAgent);"); }
}
