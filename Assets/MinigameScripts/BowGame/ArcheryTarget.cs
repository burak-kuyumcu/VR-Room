using System.Collections;
using System.Collections.Generic;
// ArcheryTarget.cs
using UnityEngine;

public class ArcheryTarget : MonoBehaviour
{
    [Header("Ba�lant�lar")]
    public ArcheryManager archeryManager;

    private bool _hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        TryHit(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryHit(other);
    }

    private void TryHit(Collider col)
    {
        Debug.Log($"[ArcheryTarget:{name}] TryHit tetiklendi, _hit={_hit}, hedef:{col.name}");
        if (_hit)
        {
            Debug.Log($"[ArcheryTarget:{name}] zaten vurulmu�, return.");
            return;
        }

        if (col.GetComponent<Arrow>() != null)
        {
            _hit = true;
            Debug.Log($"[ArcheryTarget:{name}] OK tespit edildi, RegisterHit �a�r�l�yor.");
            if (archeryManager != null)
                archeryManager.RegisterHit();
            else
                Debug.LogError($"[ArcheryTarget:{name}] archeryManager null!");
        }
        else
        {
            Debug.Log($"[ArcheryTarget:{name}] OK de�il, return.");
        }
    }
}






