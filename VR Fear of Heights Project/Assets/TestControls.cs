using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class TestControls : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    public Camera mainCamera;
    public Color paintColor = Color.white;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //if (characterController.isGrounded)
        //{
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            if (Input.GetMouseButtonDown(1))
            {
                moveDirection.y = -jumpSpeed;
            }
        //}

        if (Input.GetMouseButtonDown(0))
        {
            PaintManager manager = GameManager.GetInstance().GetPaintManager();
            for (int i = 0; i < 14; ++i)
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
                {
                    if (hit.collider is MeshCollider)
                    {
                        PaintCanvas script = hit.collider.gameObject.GetComponent<PaintCanvas>();
                        if (null != script)
                        {
                            script.PaintOnColored(hit.textureCoord, manager.GetRandomProjectileSplash(), paintColor);
                        }
                    }
                }
            }
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        //moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
    
}