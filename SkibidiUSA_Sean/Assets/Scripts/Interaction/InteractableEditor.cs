using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(InteractableEditor),true)]
public class InteractableEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Interactable interacbtable = (Interactable)target;
        base.OnInspectorGUI();
        if (interacbtable.useEvents)
        {
            interacbtable.gameObject.AddComponent<
        }
    }

}
