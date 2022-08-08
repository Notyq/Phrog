using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float runSpeed = 80f;
        [SerializeField] private float chargeSpeed = 2;
        private Animator m_Animator;
        private CharacterController2D m_Controller;
        private float m_HorizontalInput;
        private bool m_Jump = false;
        private float m_JumpCharge = 0f;

        // Update is called once per frame
        private void Start()
        {
            m_Controller = GetComponent<CharacterController2D>();
            m_Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            m_HorizontalInput = Input.GetAxis("Horizontal") * runSpeed;
            
            // m_Animator.SetFloat("Speed", Mathf.Abs(m_HorizontalInput));

            if (Input.GetButton("Jump"))
            {
                m_JumpCharge += Time.deltaTime * chargeSpeed;
            }

            if (Input.GetButtonUp("Jump"))
            {
                // m_Animator.SetBool("IsJumping", true);
                m_Jump = true;
            }
        }

        private void FixedUpdate()
        {
            if (m_Jump)
            {
                m_JumpCharge = m_JumpCharge < 1 ? 1 : m_JumpCharge > 2 ? 2 : m_JumpCharge;
                m_Controller.Move(m_HorizontalInput * Time.deltaTime, false, m_Jump, m_JumpCharge);
                m_JumpCharge = 0f;        
                m_Jump = false;

                return;
            }
            m_Controller.Move(m_HorizontalInput * Time.deltaTime, false, m_Jump, m_JumpCharge);
        }

        public void OnLanding()
        {
            // m_Animator.SetBool("IsJumping", false);
        }
    }
}