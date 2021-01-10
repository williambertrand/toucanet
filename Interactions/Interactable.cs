using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    DEFAULT,
    CARRY,
    DOOR,
    PUSH,
    INFO
}

public class Interactable : MonoBehaviour
{

    public InteractableType type;

    public virtual string GetInteractionString() {
        return PlayerConstants.DefaultInteractionString;
    }

    public virtual void Interact() { }

}
