using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private SimpleSettingsSO settings;
    [SerializeField] private float minimumPosition, maximumPosition;

    private void Update()
    {
        if (moveAction.action.enabled)
        {
            float moveValue = moveAction.action.ReadValue<float>();
            Vector3 distance = Vector3.right * (moveValue * playerStats.movementSpeed * Time.deltaTime * settings.movementUnitConversionRate);
            Vector3 finalPosition = transform.position + distance;
            finalPosition.x = Mathf.Clamp(finalPosition.x, minimumPosition, maximumPosition);
            transform.position = finalPosition;
        }
    }
}
