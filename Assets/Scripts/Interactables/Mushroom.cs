using UnityEngine;

public class Mushroom : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        //DialogueSystem.TriggerDialogue();
        Debug.Log("Interacted with! " + this.gameObject.name);
    }
}