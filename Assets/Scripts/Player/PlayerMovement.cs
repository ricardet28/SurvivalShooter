using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    private Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Move(x, z);
        Turning();
        Animating(x, z);
    }

    private void Move(float x, float z)
    {
        movement.Set(x, 0f, z);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); //creates a ray from the camera to the mousePosition
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))//if there is a hit with the object with floorMask on a maximum distance camRayLength by camRay
        {
            Vector3 playerToMouse = floorHit.point - transform.position;//creates a vector from the hit point to the player
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    private void Animating(float x, float z)
    {
        bool walking = x != 0f || z != 0f;
        anim.SetBool("IsWalking", walking);
    }

    
}
