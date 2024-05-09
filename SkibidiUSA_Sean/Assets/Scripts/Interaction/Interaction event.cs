using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Interactionevent : MonoBehaviour
{
    public bool useEvents;
    [SerializeField]
    public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
