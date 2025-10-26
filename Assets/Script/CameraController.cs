using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;      // Assign the Player transform
    public Vector3 offset = new Vector3(0, 6, -10); // Camera position relative to player
    public float smoothSpeed = 5f; // Smooth camera movement

    void LateUpdate()
    {
        if (player == null) return;

        // Desired position is behind the player
        Vector3 desiredPosition = player.position + player.transform.TransformDirection(offset);

        // Smoothly move camera to desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Always look at the player
        transform.LookAt(player.position + Vector3.up * 1.5f); // look slightly above player center
    }
}
