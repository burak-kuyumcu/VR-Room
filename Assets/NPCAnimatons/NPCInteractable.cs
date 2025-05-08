// Assets/Scripts/NPCInteractable.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class NPCInteractable : XRGrabInteractable
{
    [Tooltip("Inspector�dan atayaca��n�z DialogueData asset�i")]
    public DialogueData dialogueData;

    private NavMeshAgent navAgent;
    private CharacterController characterController;
    private Animator animator;

    [Header("Animasyon Triggers")]
    [Tooltip("Diyalog ba�lad���nda tetiklenecek Trigger")] public string talkingTrigger = "Talking";
    [Tooltip("Diyalog �ncesi selamla�ma animasyonu (opsiyonel)")] public string wavingTrigger = "Waving";

    protected override void Awake()
    {
        base.Awake();
        navAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // 1) Hareketi durdur
        if (navAgent != null)
            navAgent.isStopped = true;
        if (characterController != null)
            characterController.enabled = false;

        // 2) Selamla�ma animasyonu
        if (animator != null && !string.IsNullOrEmpty(wavingTrigger))
            animator.SetTrigger(wavingTrigger);

        // 3) Diyalog animasyonu
        if (animator != null && !string.IsNullOrEmpty(talkingTrigger))
            animator.SetTrigger(talkingTrigger);

        Debug.Log("NPC se�ildi! Diyalo�u ba�lat�l�yor...");
        DialogueManager.Instance.StartDialogue(dialogueData);
    }
}

