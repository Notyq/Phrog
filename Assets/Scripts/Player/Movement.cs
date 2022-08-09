using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float runSpeed = 80f;
        [SerializeField] private float chargeSpeed = 2;
        [SerializeField] private AudioClip jumpSound;
        private Animator mAnimator;
        private CharacterController2D mController;
        private float mHorizontalInput;
        private bool mJump = false;
        private float mJumpCharge = 0f;
        private bool isPaused;

        private UIManager uiManager;

        // Update is called once per frame
        private void Awake()
        {
            mController = GetComponent<CharacterController2D>();
            mAnimator = GetComponent<Animator>();
            uiManager = FindObjectOfType<UIManager>();
        }

        private void Update()
        {
            mHorizontalInput = Input.GetAxis("Horizontal") * runSpeed;

            if (Input.GetButton("Jump"))
            {
                mJumpCharge += Time.deltaTime * chargeSpeed;
            }

            if (Input.GetButtonUp("Jump"))
            {
                mJump = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
            {
                uiManager.Pause();
                isPaused = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPaused != false)
            {
                uiManager.ResumeGame();
                isPaused = false;
            }
        }

        private void FixedUpdate()
        {
            if (mJump)
            {
                mJumpCharge = mJumpCharge < 1 ? 1 : mJumpCharge > 2 ? 2 : mJumpCharge;
                mController.Move(mHorizontalInput * Time.deltaTime, false, mJump, mJumpCharge);
                SoundManager.instance.PlaySound(jumpSound);
                mJumpCharge = 0f;
                mJump = false;

                return;
            }
            mController.Move(mHorizontalInput * Time.deltaTime, false, mJump, mJumpCharge);
        }

        public void OnLanding()
        {
            // m_Animator.SetBool("IsJumping", false);
        }
    }
}