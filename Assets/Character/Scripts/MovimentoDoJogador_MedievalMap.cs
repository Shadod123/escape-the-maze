using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentoDoJogador_MedievalMap : MonoBehaviour
{
    CharacterController controller;

    public GameObject pauseMenu; 

    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    float forwardSpeed = 17f;
    float strafeSpeed = 10f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 1.7f;
    float timeToMaxHeight = 0.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;
    }

    void Update()
    {
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        // force = input * speed * direction
        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if (controller.isGrounded)
        {
            vertical = Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            vertical = jumpSpeed * Vector3.up;
        }

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            vertical = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Vector3 finalVelocity = forward + strafe + vertical;

        controller.Move(finalVelocity * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collided){
        if (collided.gameObject.CompareTag("PuzzleTriggerMedieval")){
            Destroy(collided.gameObject);
            SceneManager.LoadScene("PuzzleMedieval");   
        }
    }
}
