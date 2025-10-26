using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    public LayerMask wallLayer;

    private Quaternion targetRotation;
    private bool isTurning = false;
    public GameObject gameOverUI;
    private bool isGameOver = false;

    void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        moveSpeed = 0;  // stop movement
        rotationSpeed = 0;

        gameOverUI.SetActive(true);
        Debug.Log("Game Over! UI Activated");
    }

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        HandleTurnInput();
        SmoothRotate();

        Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.red);

        if (!IsWallAhead())
        {
            MoveForward();
        }
        else
        {
            GameOver();
        }
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

    bool IsWallAhead()
    {
        return Physics.Raycast(
            transform.position,
            transform.forward,
            1.5f,
            wallLayer
        );
    }

    void GameOver()
    {
        Debug.Log("🐍💥 Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
