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

        public event Action OnJump;
        public AudioSource Wind;

        //animations
        public Animator skeletonMageAnimator;
        private float m_AnimHorizontal, m_AnimVertical;

        [SerializeField] private float m_AnimSmoothingSpeed = 2;

        //Will delete
        public int fpsTarget = 60;



        public float MovementSpeed = 5f;
        public float SprintSpeed = 7.5f;
        public float JumpHeight = 2f;
        public float Gravity = -9.81f;
        public float GlideGravity = -2f;
        public float TurnSpeed = 10f;
        public int MaxJumpCount = 2;

        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool isGrounded;
        private bool isGliding;
        private int currentJumpCount;
        private IResettable _resettableImplementation;
        private Vector3 initalPosition;


        private Vector3 lastPlatformPosition;
        private Transform movingPlatform;
        
        
        public override IEnumerator IE_Initialize()
        {
            controller = GetComponent<CharacterController>();
            initalPosition = transform.position;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            yield return null;
        }


         public override void Tick()
        {
            HandleInput();
            ApplyGravity();
            controller.Move(playerVelocity * Time.deltaTime);
            Vector3 move = GetMoveInput();
            Animate(move);
            RotateTowardsCameraDirection(); 
            
            base.Tick();
        }

        public override void FixedTick()
        {
            //PlatformCheck();
            base.FixedTick();
        }


        private void HandleInput()
        {
            isGrounded = controller.isGrounded;
            if (isGrounded)
            {
                currentJumpCount = 0;
                if (isGliding) StopGliding();
            }

            Vector3 move = GetMoveInput();

            if (Input.GetButtonDown("Jump")) Jump();

            if (Input.GetKey(KeyCode.Space))
            {
                HandleGliding();
            }
            else if (isGliding)
            {
                StopGliding();
            }

            MoveCharacter(move);
        }

        private Vector3 GetMoveInput()
        {
            // Kameranın mevcut yönüne göre hareket vektörünü ayarla
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            return forward * vertical + right * horizontal;
        }

        private void MoveCharacter(Vector3 move)
        {
            float speed = Input.GetKey(KeyCode.LeftShift) ? SprintSpeed : MovementSpeed;
            controller.Move(move * speed * Time.deltaTime);
        }

        private void RotateTowardsCameraDirection()
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
        }

        private void Jump()
        {
            if (isGrounded || currentJumpCount < MaxJumpCount)
            {
                playerVelocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                currentJumpCount++;
                OnJump?.Invoke();
            }
        }

        private void ApplyGravity()
        {
            if (isGrounded && playerVelocity.y < 0) playerVelocity.y = 0f;
            playerVelocity.y += Gravity * Time.deltaTime;
        }

        private void HandleGliding()
        {
            if (!isGliding && !isGrounded && currentJumpCount >= MaxJumpCount)
            {
                StartGliding();
            }
        }

        private void StartGliding()
        {
            isGliding = true;
            Wind.Play();
            Gravity = GlideGravity;
        }

        private void StopGliding()
        {
            isGliding = false;
            Wind.Stop();
            Gravity = -9.81f;
        }

        private void Animate(Vector3 input)
        {
            Vector3 cameraRelativeInput = Camera.main.transform.InverseTransformDirection(input);
            float targetHorizontal = cameraRelativeInput.x;
            float targetVertical = cameraRelativeInput.z;

            float multiplier = Input.GetKey(KeyCode.LeftShift) ? 1 : 0.3f;
            m_AnimHorizontal = Mathf.MoveTowards(m_AnimHorizontal, targetHorizontal * multiplier, Time.deltaTime * m_AnimSmoothingSpeed);
            m_AnimVertical = Mathf.MoveTowards(m_AnimVertical, targetVertical * multiplier, Time.deltaTime * m_AnimSmoothingSpeed);

            skeletonMageAnimator.SetFloat("horizontal", m_AnimHorizontal);
            skeletonMageAnimator.SetFloat("vertical", m_AnimVertical);
        }

        private void PlatformCheck()
        {
            if (movingPlatform != null)
            {
                // Platformun mevcut pozisyonu ve son kaydedilen pozisyonu arasındaki farkı hesapla
                Vector3 deltaPosition = movingPlatform.position - lastPlatformPosition;
        
                // Karakterin pozisyonunu bu delta kadar artır, böylece platform ile birlikte hareket etmiş olur
                transform.position += deltaPosition;
        
                // Son platform pozisyonunu güncelle
                lastPlatformPosition = movingPlatform.position;
            }
        }
        /* private void OnCollisionEnter(Collision collision)
         {
             if (collision.gameObject.CompareTag("Platform"))
             {
                 Debug.Log("I am in");
                 transform.SetParent(collision.transform); // Karakteri platformun çocuğu yap
             }
         }


         private void OnCollisionExit(Collision collision)
         {
             if (collision.gameObject.CompareTag("Platform"))
             {Debug.Log("I am OUT");
                transform.parent = null; // Bağlantıyı kaldır
             }
         }

         private void OnCollisionStay(Collision other)
         {
             if (other.gameObject.CompareTag("Platform"))
             {
                 Debug.Log("I am STAY");
                 controller.Move(other.transform.position);
             }
         }
         */
        
        public static void PlayerSpawned(Transform playerTransform)
        {
            
        }

        public void ResetToInitialState()
        {
            transform.position = initalPosition;
        }
        
        
    }
}