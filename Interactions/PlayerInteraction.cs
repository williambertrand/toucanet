using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInteraction: MonoBehaviour
{
    public Transform caryLoc; //location where an object will float while picked up
    public Camera playerCam;
    public GameObject currentCarryObject;

    public bool hasPickup;
    public bool isCarrying;

    Rigidbody pickupRB;

    //UI Fields for pickup and drop - SET IN EDITOR
    public Text interactField;
    public Text disengageField;

    private void Awake()
    {
        if(interactField)
        {
            interactField.enabled = false;
        }
        else
        {
            Debug.LogError("Player carry interact field not set!");
        }
        if (disengageField)
        {
            disengageField.enabled = false;
        }
        else
        {
            Debug.LogError("Player carry disengage field not set!");
        }
    }

    void Update()
    {
        RaycastHit hit;

        //TODO: Look at raycast forward from camera  
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, PlayerConstants.InteractionDist))
        {

            if (hit.collider.CompareTag("Interactable"))
            {

                //TODO: Check for generic interactable -> get type -> handle action

                Interactable focus = hit.collider.gameObject.GetComponent<Interactable>();
                if (focus)
                {

                    // Handle Carry item
                    if (focus.type == InteractableType.CARRY)
                    {
                        CarryItem focusCarry = (CarryItem)focus;

                        if (!isCarrying)
                        {
                            hasPickup = true;
                            ShowInteraction(focusCarry.interactStr);
                        }


                        if (!isCarrying && Input.GetKeyDown(KeyCode.E))
                        {
                            //TODO: Clean up a good way to mark an item as being carried: Ignore gravity and set cary transform as parent
                            hit.collider.gameObject.transform.position = caryLoc.position;
                            hit.collider.gameObject.transform.SetParent(caryLoc);

                            pickupRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                            pickupRB.useGravity = false;

                            pickupRB.freezeRotation = true;

                            pickupRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;

                            currentCarryObject = hit.collider.gameObject;

                            isCarrying = true;
                            pickupRB.velocity = Vector3.zero;

                            interactField.enabled = false;
                            disengageField.text = focusCarry.disEngageStr;
                            disengageField.enabled = true;

                            return;
                        }
                    }

                    // Handle Carry item
                    else if (focus.type == InteractableType.DOOR)
                    {
                        Door focusDoor = (Door)focus;
                        ShowInteraction(focusDoor.GetInteractionString());

                        //TODO: Split out to hasFocus and then oninput press handle the action
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            focusDoor.Interact();
                        }
                    }
                    else
                    {
                        //Handle generic interaction
                        ShowInteraction(focus.GetInteractionString());

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            focus.Interact();
                        }
                    }
                }
                else
                {
                    hasPickup = false;
                    HideInteraction();
                }
            }
            else
            {
                hasPickup = false;
                
            }

        }
        else
        {
            HideInteraction();
        }


        if (isCarrying && Input.GetKeyDown(KeyCode.E))
        {
            DropCurrentObject();
        }

    }

    private void LateUpdate()
    {
        if(isCarrying && pickupRB)
        {
            if (pickupRB.angularVelocity.sqrMagnitude > 0.0f)
            {
                pickupRB.angularVelocity = Vector3.zero;
            }
            if (pickupRB.velocity.sqrMagnitude > 0.0f)
            {
                pickupRB.velocity = Vector3.zero;
            }
        }
    }


    void DropCurrentObject()
    {
        disengageField.enabled = false;
        currentCarryObject.transform.parent = null;
        pickupRB.useGravity = true;
        pickupRB.constraints = RigidbodyConstraints.None;
        currentCarryObject = null;
        pickupRB = null;
        isCarrying = false;

    }


    void ShowInteraction(string message)
    {
        interactField.text = message;
        interactField.enabled = true;
    }

    void HideInteraction()
    {
        interactField.enabled = false;
    }

}
