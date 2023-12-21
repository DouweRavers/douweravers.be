using Godot;
using System;

public class VRWorld : Spatial
{
    public Button VRbutton {get => GetNode<Button>("../Button"); }

    WebXRInterface webXRInterface;
    bool isVRSupported;

    public override void _Ready()
    {
        webXRInterface = ARVRServer.FindInterface("WebXR") as WebXRInterface;
        if (webXRInterface == null) return;

        webXRInterface.XrStandardMapping = true;
        webXRInterface.Connect("session_supported", this, "WebXRSessionSupported");
        webXRInterface.Connect("session_started", this, "WebXRSessionStarted");
        webXRInterface.Connect("session_ended", this, "WebXRSessionEnded");
        webXRInterface.Connect("session_failed", this, "WebXRSessionFailed");
        webXRInterface.IsSessionSupported("immersive-vr");
    }

    public void WebXRSessionSupported(string sessionMode, bool supported){
        if (sessionMode.Equals("immersive-vr")) isVRSupported = supported;
    }

    public void WebXRSessionStarted(){
        VRbutton.Visible = false;
        GetViewport().Arvr = true;
        
    }
    public void WebXRSessionEnded(){
        VRbutton.Visible = true;
        GetViewport().Arvr = false;
    }

    public void WebXRSessionFailed(string message){
        OS.Alert("Failed to initialize VR: " + message);
    }

    public void OnVRButtonPressed(){
        if (!isVRSupported){OS.Alert("Your browser doesn't support VR"); return; }
        webXRInterface.SessionMode = "immersive-vr";
        webXRInterface.RequestedReferenceSpaceTypes = "bounded-floor, local-floor, local";
        webXRInterface.RequiredFeatures = "local-floor";
        webXRInterface.OptionalFeatures = "bounded-floor";
        if(!webXRInterface.Initialize()) { OS.Alert("Failed to initialize"); return; }
    }

}
