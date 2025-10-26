using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;       // Forward movement speed
    public float rotationSpeed = 200f; // Speed of rotation

    private int turnDirection = 0;     // -1 = left, 1 = right
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation; // initial rotation
    }

    void Update()
    {
        // Check input for turning
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            turnDirection = -1;
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y - 90f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            turnDirection = 1;
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90f, 0);
        }

        // Smoothly rotate
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move forward constantly
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
