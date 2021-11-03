using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputX : Element {

    public abstract bool isTouching();
    public abstract bool isTouchingOneTime();
    public abstract bool isLetTouchOneTime();

}
