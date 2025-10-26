using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    private Quaternion targetRotation;
    private bool isTurning = false;
    private bool isGameOver = false;

    public GameObject gameOverUI;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (isGameOver) return;

        HandleTurnInput();
        SmoothRotate();
        MoveForward();
    }

    void HandleTurnInput()
    {
        if (isTurning) return;

        Vector3 dir = transform.forward;

        // Moving Right
        if (dir == Vector3.right)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                TurnLeft();
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                TurnRight();
        }
        // Moving Left
        else if (dir == Vector3.left)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                TurnLeft();
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                TurnRight();
        }
        // Moving Up
        else if (dir == Vector3.forward)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                TurnLeft();
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                TurnRight();
        }
        // Moving Down
        else if (dir == Vector3.back)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                TurnLeft();
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                TurnRight();
        }
    }

    void TurnLeft()
    {
        targetRotation = Quaternion.Euler(0, transform.eulerAngles.y - 90f, 0);
        isTurning = true;
    }

    void TurnRight()
    {
        targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90f, 0);
        isTurning = true;
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
            GameOver();
        }
    }

    void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        moveSpeed = 0;
        rotationSpeed = 0;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Debug.Log("🐍💥 Game Over UI Active!");
    }

    // These can be called from your UI buttons
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart Game");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
