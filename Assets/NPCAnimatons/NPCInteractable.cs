// Assets/Scripts/NPCInteractable.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class NPCInteractable : XRBaseInteractable
{
    [Tooltip("Inspector�dan atayaca��n�z DialogueData asset�i")]
    public DialogueData dialogueData;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(OnSelected);
    }

    protected override void OnDisable()
    {
        selectEntered.RemoveListener(OnSelected);
        base.OnDisable();
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        Debug.Log("NPC se�ildi! Diyalo�u ba�lat�l�yor...");
        DialogueManager.Instance.StartDialogue(dialogueData);
    }
}
