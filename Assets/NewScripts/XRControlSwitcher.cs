using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

// Alias tan�m�yla hangi XRController�� kulland���m�z� netle�tiriyoruz:
using InputXRController = UnityEngine.InputSystem.XR.XRController;

public class XRControlSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;     // XR Rig�in PlayerInput component�i
    [SerializeField] private XRDeviceSimulator deviceSimulator; // Sahnendeki Device Simulator

    private void Start()
    {
        // Ba�lang��ta otomatik olarak VR kontrolc�ye ge�
        Invoke(nameof(SwitchToVR), 1f);
    }

    public void SwitchToVR()
    {
        // Simulator�� kapat
        deviceSimulator.enabled = false;

        // Sadece InputSystem i�indeki XRController tipindeki cihazlar� al
        var xrDevices = InputSystem.devices
            .Where(d => d is InputXRController)
            .ToArray();

        // �XR Controller� scheme�ini, bulunan XR controller cihazlar�yla aktif et
        playerInput.SwitchCurrentControlScheme("XR Controller", xrDevices);
        Debug.Log($"Switched to VR controllers: {string.Join(", ", xrDevices.Select(d => d.name))}");
    }

    public void SwitchToSimulator()
    {
        // Simulator�� a�
        deviceSimulator.enabled = true;

        // Klavye ve fareyi al�p �Keyboard&Mouse� scheme�ine geri d�n
        var kbMouse = new InputDevice[] { Keyboard.current, Mouse.current };
        playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", kbMouse);
        Debug.Log("Switched back to Simulator");
    }
}
