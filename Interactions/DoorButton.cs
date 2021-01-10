using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Interactable
{

    public Door target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        target.Interact();
    }

    public override string GetInteractionString()
    {
        if(target.state == DoorState.OPEN)
        {
            return "E: Close door";
        }
        else
        {
            return "E: Open door";
        }
    }



}
