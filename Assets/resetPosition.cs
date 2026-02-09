using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class resetPosition : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;


    [SerializeField] private InputActionReference xriLeftPrimaryButtonAction;

    private void OnEnable()
    {
        xriLeftPrimaryButtonAction.action.performed += OnPrimaryButtonPressed;
        xriLeftPrimaryButtonAction.action.Enable();
    }

    private void OnDisable()
    {
        xriLeftPrimaryButtonAction.action.performed -= OnPrimaryButtonPressed;
        xriLeftPrimaryButtonAction.action.Disable();
    }

    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        ResetPosition();
    }

    [ContextMenu("Reset Position")]
    public void ResetPosition()
    {
        var rotationAngley = resetTransform.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngley, 0);
        var distanceDiff = resetTransform.position - playerHead.transform.position;
        player.transform.position += distanceDiff;
    }
}
