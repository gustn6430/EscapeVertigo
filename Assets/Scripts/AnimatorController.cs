using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace SmallScaleInteractive._2DCharacter
{
   public class AnimatorController : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody2D rb;
        public float jumpForce = 5f; // Adjust the force as needed
        public LayerMask groundLayer; // Set this in the Inspector
        public Transform groundCheck; // Assign a child GameObject in the Inspector
        public float groundCheckRadius = 0.2f; // Adjust based on your needs
        public bool isCurrentlyJumping = false;
        public bool isGrounded;

        public float movementSpeed = 15f; // Adjustable speed of the character

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }


        void Update()
        {
           
            bool moveEast = Input.GetKey(KeyCode.D);
            bool moveWest = Input.GetKey(KeyCode.A);
            bool isSliding = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


            bool isCrouching = Input.GetKey(KeyCode.S); // Check if the 'S' key is being held down for crouching
            bool isAttacking = Input.GetMouseButtonDown(0); // Left mouse button
            bool isRanged = Input.GetMouseButtonDown(1); // Left mouse button
            bool isJumping = Input.GetKeyDown(KeyCode.Space);
            bool useSpecialAbility1 = Input.GetKeyDown(KeyCode.X);
            bool useSpecialAbility2 = Input.GetKeyDown(KeyCode.C);
            bool isDying = Input.GetKeyDown(KeyCode.V);
            bool isTakingDamage = Input.GetKeyDown(KeyCode.F);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // Handle crouching
            animator.SetBool("isCrouching", isCrouching);

            // Movement input
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            Vector2 movement = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);
            
            // Apply movement
            rb.velocity = movement;

            // Handle animations based on movement
            bool isMoving = moveHorizontal != 0;
            animator.SetBool("isWalking", isMoving);
            if (isMoving)
            {
                animator.SetInteger("Direction", moveHorizontal > 0 ? 0 : 1);
            }
                
            if (isCrouching)
            {
                // If crouching and moving east or west, enable crouch walking
                if (moveEast)
                {
                    animator.SetBool("isCrouchingWalking", true);
                    animator.SetInteger("Direction", 0); // 0 for East
                }
                else if (moveWest)
                {
                    animator.SetBool("isCrouchingWalking", true);
                    animator.SetInteger("Direction", 1); // 1 for West
                }
                else
                {
                    // If crouching but not moving east or west, not crouch walking
                    animator.SetBool("isCrouchingWalking", false);
                }
            }
            else
            {
                // If not crouching, handle walking or idle
                if (moveEast)
                {
                    animator.SetBool("isWalking", true);
                    animator.SetInteger("Direction", 0); // 0 for East
                }
                else if (moveWest)
                {
                    animator.SetBool("isWalking", true);
                    animator.SetInteger("Direction", 1); // 1 for West
                }
                else
                {
                    animator.SetBool("isWalking", false);
                }
                // Ensure crouch walking is disabled if not crouching
                animator.SetBool("isCrouchingWalking", false);
            }

            // Sliding logic
            if (isSliding && (moveEast || moveWest) && isGrounded)
            {
                animator.SetBool("isSliding", true);
                // Determine the direction of the slide and set the Direction parameter accordingly
                if (moveEast)
                {
                    animator.SetInteger("Direction", 0); // 0 for East
                }
                else if (moveWest)
                {
                    animator.SetInteger("Direction", 1); // 1 for West
                }
            }
            else
            {
                animator.SetBool("isSliding", false);
            }
            // Handle special ability 1
            if (useSpecialAbility1)
            {
                animator.SetBool("isUsingSpecialAbility1", true);
            }
            else
            {
                animator.SetBool("isUsingSpecialAbility1", false);
            }

            // Handle special ability 2
            if (useSpecialAbility2)
            {
                animator.SetBool("isUsingSpecialAbility2", true);
            }
            else
            {
                // Similar to special ability 1, resetting the parameter might depend on your specific implementation.
                animator.SetBool("isUsingSpecialAbility2", false);
            }

            // Update dying state based on 'V' key input
            if (isDying)
            {
                animator.SetBool("isDead", true);
            }
            else
            {
                animator.SetBool("isDead", false);
            }


            // Update taking damage state based on 'F' key input
            if (isTakingDamage)
            {
                animator.SetBool("isTakingDamage", true);
            }
            else
            {
                animator.SetBool("isTakingDamage", false);
            }

            // Handle jump action
            // While in air, adjust for falling and landing
            if (isCurrentlyJumping)
            {
                if (rb.velocity.y < 0 && !animator.GetBool("isFalling"))
                {
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isJumpMid", false);
                    animator.SetBool("isFalling", true);
                    isMoving = false;
                }
            }
            
            if (isGrounded && animator.GetBool("isFalling"))
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isJumpMid", false);
                animator.SetBool("isFalling", false);
                isCurrentlyJumping = false;
                if (isMoving) {
                    animator.SetBool("isLandingRunning", true);
                    animator.SetBool("isLandingRunning", false);
                } else {
                    animator.SetTrigger("isLanding");
                }
            }

            // Check for jump initiation
            if (isJumping && isGrounded && !isCurrentlyJumping)
            {
                isCurrentlyJumping = true;
                animator.SetBool("isJumping", true);
                StartCoroutine(DelayedJump(0.3f)); // Adjust force in coroutine if jump height is insufficient
            }
            else if (!isJumping && isGrounded)
            {
                animator.ResetTrigger("isLanding"); // Reset landing trigger if needed
            }

            // Update attacking state based on mouse button input
            animator.SetBool("isAttacking", isAttacking);
            animator.SetBool("isRanged", isRanged);
        }

        IEnumerator DelayedJump(float delay)
        {
            yield return new WaitForSeconds(delay);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            // Immediately after jumping, we're technically in the jumpMid phase:
            animator.SetBool("isJumpMid", true);
        }



    }
}
