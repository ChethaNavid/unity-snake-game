using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    private Quaternion targetRotation;
    private bool isTurning = false;
    private bool isDead = false;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (isDead) return; // Stop everything when dead

        HandleTurnInput();
        SmoothRotate();
        MoveForward();
    }

    void HandleTurnInput()
    {
        if (isTurning) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y - 90f, 0);
            isTurning = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90f, 0);
            isTurning = true;
        }
    }

    void SmoothRotate()
    {
        if (!isTurning) return;

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
            isTurning = false;
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log("🐍💥 Game Over!");
            SnakeGameUI.Instance.ShowGameOver();
            isDead = true;
        }
    }
}
