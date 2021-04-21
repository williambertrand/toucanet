using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DoorState
{
    CLOSED,
    OPEN
}

public class Door : Interactable
{

    //NOTE: set to start state in editor
    public DoorState state;

    private Animator animator;
    private BoxCollider blocker;

    private void Awake()
    {
        type = InteractableType.DOOR;
        animator = GetComponent<Animator>();
        blocker = GetComponent<BoxCollider>();
    }

    public override string GetInteractionString()
    {
        switch (state)
        {
            case DoorState.OPEN:
                return TextValues.CloseDoor;
            case DoorState.CLOSED:
                return TextValues.OpenDoor;
            default:
                Debug.LogError("UNHANDLED DOOR STATE: " + state);
                return TextValues.OpenDoor;
        }

    }

    public override void Interact()
    {
        animator.SetBool("open", !animator.GetBool("open"));

        if(state == DoorState.OPEN)
        {
            //close the door
            blocker.enabled = true;
            state = DoorState.CLOSED;
        }
        else if (state == DoorState.CLOSED)
        {
            blocker.enabled = false;
            state = DoorState.OPEN;
        }

    }
}
