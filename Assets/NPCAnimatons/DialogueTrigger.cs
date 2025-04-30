using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Sahneye atanacak diyalog sat�rlar�")] public string[] lines;
    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            // NPC Animator'� "isTalking" parametresini true yap
            Animator animator = GetComponentInParent<Animator>();
            if (animator != null) animator.SetBool("isTalking", true);

            // Diyalo�u ba�lat
            DialogueUI.Instance?.Show(lines, EndDialogue);
        }
    }

    // Diyalog bitti�inde �a�r�lacak
    private void EndDialogue()
    {
        Animator animator = GetComponentInParent<Animator>();
        if (animator != null) animator.SetBool("isTalking", false);
    }
}