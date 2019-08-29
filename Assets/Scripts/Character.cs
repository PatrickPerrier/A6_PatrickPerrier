using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float acceleration = 30;

    [SerializeField]
    float gravity = 25;

    [SerializeField]
    public GameObject childObject;

    Animator animator;

    private bool swim;
    private float jumpForce = 15.0f;
    private float gravityJump = 14.0f;
    public float jumpVelocity;

    CharacterController controller;

    private Vector3 change;

    public Vector3 verticalVelocity;
    Vector3 planarVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = childObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Movement
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (controller.isGrounded || swim == true)
        {
            planarVelocity = Vector3.MoveTowards(planarVelocity, moveInput * speed, acceleration * Time.deltaTime);
            verticalVelocity.y = 0;
        }

        verticalVelocity.y -= gravity * Time.deltaTime;
        
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //Jump
        if (controller.isGrounded || swim == true)
        {
            animator.SetLayerWeight(1, 0);
            jumpVelocity = -gravityJump * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = jumpForce;
            }
        }
        else
        {
            animator.SetLayerWeight(1, 1);
            jumpVelocity -= gravityJump * Time.deltaTime;
        }

        Vector3 jumpVector = new Vector3(0, jumpVelocity, 0);
        controller.Move(jumpVector * Time.deltaTime);

        MovePlayer();
    }
    void MovePlayer()
    {
        controller.Move((planarVelocity + verticalVelocity) * Time.deltaTime);
    }
    //Water 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            jumpForce = 20.0f;
            gravity = 0.3f;
            animator.SetLayerWeight(2, 1);
            swim = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            jumpVelocity = jumpForce;
            jumpForce = 15.0f;
            gravity = 25;
            animator.SetLayerWeight(2, 0);
            swim = false;
        }
    }

}
