using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip ouchAudio;
        public AudioClip respawnAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;

        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        private bool stopJump;
        public AudioSource audioSource;
        public bool controlEnabled = true;
        public Collider2D collider2d;
        public Health health;
        public JumpState jumpState = JumpState.Grounded;

        internal Animator animator;
        private readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        private SpriteRenderer spriteRenderer;
        private Vector2 move;

        public Bounds Bounds => collider2d.bounds;

        private bool jump;

        [SerializeField, Tooltip("Amount of jumps allowed mid-air"), Range(0, int.MaxValue)]
        private int _airJumps = 1;

        private int _airJumpsLeft;

        private void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            _airJumpsLeft = _airJumps;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                HandleJump();
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        private void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;

                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;

                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;

                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    _airJumpsLeft = _airJumps;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && (IsGrounded || _airJumpsLeft >= 0 && !IsGrounded))
            {
                // If we're dubble jumping, we want to jump a bit less high.
                velocity.y = (jumpTakeOffSpeed * model.jumpModifier) * (_airJumpsLeft == _airJumps ? 1f : .75f);
                jump = false;
                --_airJumpsLeft;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed,
            DropAttack
        }

        private void HandleJump()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
            }

            if ((jumpState == JumpState.Grounded || _airJumpsLeft >= 0 && jumpState != JumpState.Grounded) && Input.GetButtonDown("Jump"))
            {
                jumpState = JumpState.PrepareToJump;
            }
            // Stop jump mid air
            else if (Input.GetButtonUp("Jump"))
            {
                stopJump = true;
                Schedule<PlayerStopJump>().player = this;
            }
        }

        // Function that disbles the controls for a short period of time.
        public void DisableControls(float duration)
        {
            controlEnabled = false;
            Invoke("EnableControls", duration);
        }

        private void EnableControls()
        {
            controlEnabled = true;
        }
    }
}
