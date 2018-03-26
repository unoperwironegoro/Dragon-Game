using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController {
    ControlDir Flap();
    ControlDir Release();
}

public enum ControlDir {
    NONE,
    LEFT,
    RIGHT
}