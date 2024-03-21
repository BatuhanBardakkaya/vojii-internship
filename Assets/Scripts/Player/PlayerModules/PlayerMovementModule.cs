using Assets.Scripts.Agent.AgentModule;
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    // PlayerMovementModule handles the player's movement, including walking, sprinting, jumping, and gliding.
    public class PlayerMovementModule : AgentModuleBase, IResettable
    {
        public event Action OnJump;
        public AudioSource Wind;

        // Animations
        public Animator skeletonMageAnimator;
        private float m_AnimHorizontal, m_AnimVertical;

        [SerializeField] private float m_AnimSmoothingSpeed = 1.15f;

        public float MovementSpeed = 5f;
        public float SprintSpeed = 7.5f;
        public float JumpHeight = 2f;
        public float Gravity = -9.81f;
        public float GlideGravity = -2f;
        public float TurnSpeed = 10f;
        public int MaxJumpCount = 2;
        private bool canMove = true;

        // Dash variables
        [SerializeField] private float dashSpeed = 20f;
        [SerializeField] private float dashDuration = 0.2f;
        private float dashTimeLeft;
        private bool isDashing;
        [SerializeField] private float decelerationFactor = 0.1f; 
        private bool shouldDecelerate = false;
        [SerializeField] private float dashCooldown = 3f; 
        private float dashCooldownTimeLeft;
        private ParticleSystem dashTrailParticleSystem;
        private ParticleSystem GlideTrailParticleSystem;
        
        //Glide
        [SerializeField] private float glideCooldown = 2f; 
        private float glideCooldownTimeLeft;
        

        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool isGrounded;
        private bool isGliding;
        private int currentJumpCount;
        private Vector3 initialPosition;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            initialPosition = transform.position;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            dashCooldownTimeLeft = 0;
            glideCooldownTimeLeft = 0;
            
            dashTrailParticleSystem = GameObject.FindGameObjectWithTag("DashTrail").GetComponent<ParticleSystem>();
            GlideTrailParticleSystem = GameObject.FindGameObjectWithTag("GlideTrail").GetComponent<ParticleSystem>();
            DOTween.Init();
        }

        public override void Tick()
        {
           UpdateDash();
           ApplyGravity();

           if (!isDashing && !shouldDecelerate)
           {
               HandleInput();
           }
           
           controller.Move(playerVelocity * Time.deltaTime);

           if (!isDashing)
           {
               Vector3 move = GetMoveInput();
               Animate(move);
               RotateTowardsCameraDirection();
           }

           // Soğuma zamanını güncelle
           if (dashCooldownTimeLeft > 0)
           {
               dashCooldownTimeLeft -= Time.deltaTime;
           }
           if (glideCooldownTimeLeft > 0)
           {
               glideCooldownTimeLeft -= Time.deltaTime;
           }
           
           
            base.Tick();
        }
        private void HandleInput()
        {
            //isGrounded = controller.isGrounded;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                currentJumpCount = 0;
                skeletonMageAnimator.SetBool("isGliding",false);
                
                if (isGliding) StopGliding();
                canMove = true;
            }

            Vector3 move = GetMoveInput();

            if (Input.GetKeyDown(KeyCode.Alpha3)) // Dash input
            {
                
                Dash();
            }

            if (!isDashing)
            {
                if (Input.GetButtonDown("Jump")) Jump();
                if (Input.GetKey(KeyCode.Space) && glideCooldownTimeLeft <= 0) HandleGliding();
                else if (isGliding) StopGliding();
                MoveCharacter(move);
            }
        }

        private Vector3 GetMoveInput()
        {
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
                isGrounded = false;
                playerVelocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                skeletonMageAnimator.SetBool("isGliding",true);
                currentJumpCount++;
                OnJump?.Invoke();
            }
        }

        private void ApplyGravity()
        {
            //bool wasGrounded = isGrounded;
            //isGrounded = controller.isGrounded;

            if (isGrounded)
            {
                // Karakter yeni yere değdi
                skeletonMageAnimator.SetBool("isLanding", false);  
            }

            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
    
            if (!isDashing || !isGrounded)
            {
                playerVelocity.y += Gravity * Time.deltaTime;
            }
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
            skeletonMageAnimator.SetBool("isGliding",true);
            GlideParticleActive();
            Wind.Play();
            Gravity = GlideGravity;
        }

        private void StopGliding()
        {
            isGliding = false;
            skeletonMageAnimator.SetBool("isGliding",false);
            skeletonMageAnimator.SetBool("isLanding",true);
            GlideParticleActive();
            Wind.Stop();
            Gravity = -9.81f;
        }

        private void Animate(Vector3 input)
        {
            Vector3 cameraRelativeInput = Camera.main.transform.InverseTransformDirection(input);
            m_AnimHorizontal = Mathf.MoveTowards(m_AnimHorizontal, cameraRelativeInput.x, Time.deltaTime * m_AnimSmoothingSpeed);
            m_AnimVertical = Mathf.MoveTowards(m_AnimVertical, cameraRelativeInput.z, Time.deltaTime * m_AnimSmoothingSpeed);

            skeletonMageAnimator.SetFloat("horizontal", m_AnimHorizontal);
            skeletonMageAnimator.SetFloat("vertical", m_AnimVertical);
        }

        private void Dash()
        {
            if (!isDashing && dashCooldownTimeLeft <= 0)
            {
                // Hareket yönünü al
                Vector3 moveInput = GetMoveInput();
                if (moveInput.sqrMagnitude > 0) // Eğer bir hareket girdisi varsa dash yap
                {
                   
                    CoreGameSignals.OnDashUsed?.Invoke();
                    isDashing = true;
                    skeletonMageAnimator.SetTrigger("Dash");
                    DashParticleActive();
                    dashTimeLeft = dashDuration;
                    
                    // Karakterin baktığı yöne değil, WASD ile belirlenen hareket yönüne doğru dash yap
                    Vector3 dashDirection = moveInput.normalized;
                    playerVelocity = dashDirection * dashSpeed;
                    dashCooldownTimeLeft = dashCooldown;
                }
            }
        }

        private void UpdateDash()
        {
            if (isDashing)
            {
                dashTimeLeft -= Time.deltaTime;
                if (dashTimeLeft <= 0)
                {
                    isDashing = false;
                    shouldDecelerate = true;
                    DashParticleActive();
                    playerVelocity.x = 0;
                    playerVelocity.z = 0;
                }
            }

            if (shouldDecelerate)
            {
                Decelerate();
                
                if (playerVelocity.magnitude < 0.1f)
                {
                    shouldDecelerate = false;
                }
            }
        }

        private void Decelerate()
        {
            
            if (shouldDecelerate)
            {
                playerVelocity = Vector3.Lerp(playerVelocity, new Vector3(0, playerVelocity.y, 0), decelerationFactor * Time.deltaTime);
        
               
                if (new Vector3(playerVelocity.x, 0, playerVelocity.z).magnitude < 0.1f)
                {
                    playerVelocity.x = 0;
                    playerVelocity.z = 0;
                    shouldDecelerate = false;
                }
            }
        }

        private void DashParticleActive()
        {
            if (dashTrailParticleSystem != null)
            {
                if (isDashing == true)
                    dashTrailParticleSystem.Play();
                else
                    dashTrailParticleSystem.Stop();
            }
        }

        private void GlideParticleActive()
        {
            if (GlideTrailParticleSystem != null)
            {
                if (isGliding == true)
                {
        
                    GlideTrailParticleSystem.Play();
                    
                    GlideTrailParticleSystem.transform.DOScale(2f, 1.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
                    {
                        GlideTrailParticleSystem.transform.DOScale(0f, 2.75f).SetEase(Ease.InOutQuad);
                    });
                    
                }
                if (isGrounded == true)
                {
                    GlideTrailParticleSystem.Stop();
                    CoreGameSignals.OnGlideUsed?.Invoke();
                    glideCooldownTimeLeft = glideCooldown;
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
            
        }

        private void OnCollisionExit(Collision other)
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }


        public void ResetToInitialState()
        {
            transform.position = initialPosition;
        }
        
    }
}
