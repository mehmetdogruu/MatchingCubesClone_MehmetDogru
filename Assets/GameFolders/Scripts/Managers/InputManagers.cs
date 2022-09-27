using UnityEngine;

public class InputManagers
{
    public bool MouseDown => Input.GetMouseButtonDown(0);
    public bool MouseHold => Input.GetMouseButton(0);
    public bool MouseUp => Input.GetMouseButtonUp(0);
}
