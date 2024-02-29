using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.ModulerSystem;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    // PlayerMovementModule handles the player's movement, including walking, sprinting, jumping, and gliding.
    public class PlayerMovementModule : AgentModuleBase, IResettable
    {
        public float jumpForce = 5f; // The force applied when jumping.
        public Rigidbody Rigidbody; // Reference to the Rigidbody component for physics interactions.
        public float MovementSpeed = 5f; // Normal walking speed.
        private float _originalSpeed; // Used to store the original movement speed.
        public float sprintMultiplier = 1.5f; // Multiplier applied to speed when sprinting.
        public float backwardMultiplier = 0.5f;
        private bool isGrounded; // Flag to check if the player is touching the ground.
        private int jumpCount = 0; // Counter for the number of consecutive jumps.
        public int maxJumpCount = 2; // Maximum number of consecutive jumps allowed.
        public float glideGravity = 2f; // Gravity applied when gliding.
        private bool isGliding = false; // Flag to check if the player is currently gliding.
        public float turnSpeed = 10f; // Speed at which the player turns to face movement direction.
        private Vector3 initialPosition;
        
        public event Action OnJump;
        public AudioSource Wind;

        //animations
        public Animator skeletonMageAnimator;
        private float m_AnimHorizontal, m_AnimVertical;
        [SerializeField] private float m_AnimSmoothingSpeed = 2;
        //Will delete
        public int fpsTarget = 60;



        // Initialize sets up the module, storing original speed and getting the Rigidbody component.

        public override  IEnumerator IE_Initialize()
        {
            
            StartCoroutine(base.IE_Initialize());
            Debug.Log("Player MOvement Start is Worknig");
            _originalSpeed = MovementSpeed;
            Rigidbody = GetComponent<Rigidbody>();
            initialPosition = transform.position;
            Wind = GetComponent<AudioSource>();
            skeletonMageAnimator = transform.Find("Visuals/Skeleton_Mage").GetComponent<Animator>();
            yield return null;
        }
        public void ResetToInitialState()
        {
            transform.position = initialPosition; 
            
        }
        // Activate is called to activate this module.
      

        // Tick is called every frame and handles input for movement, jumping, and gliding.
        public override void Tick()
        {
            if (GeneralState != ModuleState.Activated)
            {
                return;

            }

            Application.targetFrameRate = fpsTarget;
            Move();
            
            HandleMovementInput();
            HandleJumpInput();
            HandleGlideInput();
            ApplyGlideGravity();
            Rotate();
            
        }

        // FixedTick is called every fixed framerate frame and applies physics-based movement, rotation, and gliding gravity.
        public override void FixedTick()
        {
            if (GeneralState != ModuleState.Activated)
            {
                return;
            }
            base.FixedTick();
        }

        // HandleMovementInput updates the movement speed based on whether the player is sprinting.
        private void HandleMovementInput()
        {
            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S))
            {
                MovementSpeed = _originalSpeed * sprintMultiplier;
            }
            else if (Input.GetKey(KeyCode.S)) 
            {
                
                MovementSpeed = _originalSpeed * backwardMultiplier;
            }
            else
            {
                MovementSpeed = _originalSpeed;
            }
        }

        // HandleJumpInput checks for jump input and whether jumping is allowed, then calls Jump.
        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < maxJumpCount))
            {
                Jump();
            }
        }

        // HandleGlideInput checks for glide input and starts or stops gliding.
        private void HandleGlideInput()
        {
            if (!isGrounded && jumpCount >= maxJumpCount && Input.GetKey(KeyCode.Space))
            {
                if (!isGliding) 
                {
                    StartGliding();
                }
            }
            else
            {
                if (isGliding) 
                {
                    StopGliding();
                }
            }
        }

        // Move applies movement based on player input and current speed.
        private void Move()
        {
            
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var newspeed = MovementSpeed * Time.deltaTime;
            Vector3 movement = Camera.main.transform.TransformDirection(input) * newspeed;
            movement.y = 0;
            Rigidbody.MovePosition(Rigidbody.position + movement);
            Animate(input);
        }

        // Rotate adjusts the player's rotation to face the direction of movement.
        private void Rotate()
        {
       
         Vector3 cameraForward = Camera.main.transform.forward;
         cameraForward.y = 0; 
         cameraForward.Normalize(); 

      
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

         
         Rigidbody.rotation = Quaternion.Slerp(Rigidbody.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // Jump applies a force upwards and increments the jump counter.
        private void Jump()
        {
            Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpCount++;
            
            OnJump?.Invoke(); // Event trig
            
        }

        // StartGliding initiates gliding, reducing fall speed.
        private void StartGliding()
        {
           
            isGliding = true;
            Wind.Play();

            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, Mathf.Max(Rigidbody.velocity.y, -glideGravity), Rigidbody.velocity.z);
            
        }

        // StopGliding stops the gliding action.
        private void StopGliding()
        { 
            isGliding = false;
            Wind.Stop();

        }

        // ApplyGlideGravity adjusts drag to simulate gliding when applicable.
        private void ApplyGlideGravity()
        {
            if (isGliding)
            {
                Rigidbody.drag = 1;
            }

            else
            {
               
                Rigidbody.drag = 0; }
        }

        // OnCollisionEnter checks for collisions with the ground to reset jump capabilities.
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                jumpCount = 0;
            }
        }
        private void Animate(Vector3 input)
        {
            float multiplier = Input.GetKey(KeyCode.LeftShift) ? 1 : 0.3f;
            float targetHorizontal = input.x * multiplier;
            float targetVertical = input.z * multiplier;
            if (m_AnimHorizontal < targetHorizontal)
            {
                m_AnimHorizontal += Time.deltaTime * m_AnimSmoothingSpeed;
            }
            else if (m_AnimHorizontal >targetHorizontal)
            {
                m_AnimHorizontal -= Time.deltaTime * m_AnimSmoothingSpeed;
            }if (m_AnimVertical < targetVertical)
            {
                m_AnimVertical += Time.deltaTime * m_AnimSmoothingSpeed;
            }
            else if (m_AnimVertical > targetVertical)
            {
                m_AnimVertical -= Time.deltaTime * m_AnimSmoothingSpeed;
            }
            skeletonMageAnimator.SetFloat("horizontal", m_AnimHorizontal );
            skeletonMageAnimator.SetFloat("vertical", m_AnimVertical );

        }
    }
}
