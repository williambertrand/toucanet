using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableInfoType
{
    NORMAL,
    TUTORIAL
}

public class CarryItem : Interactable
{

    public InteractableInfoType infoType;

    //Shown before player interacts with object
    public string interactStr;

    //Show while player is interacting with object
    // Not all objects need this - TODO: This should be moved to a parent Interactable class
    public string disEngageStr;

    void Awake()
    {
        type = InteractableType.CARRY;
        if(infoType == InteractableInfoType.TUTORIAL)
        {
            interactStr = TextValues.PickupTut;
            disEngageStr = TextValues.DropTut;
        }
        else
        {
            interactStr = TextValues.Pickup;
            disEngageStr = TextValues.Drop;
        }
        
    }

}
