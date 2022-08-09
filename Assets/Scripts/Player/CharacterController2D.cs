using UnityEngine;
using UnityEngine.Events;

namespace Player
{
	public class CharacterController2D : MonoBehaviour
	{
		private const float KGroundedRadius = .2f;									// Radius of the overlap circle to determine if grounded
		private const float KCeilingRadius = .2f;									// Radius of the overlap circle to determine if the player can stand up
		[SerializeField] private float jumpForce = 400f;							// Amount of force added when the player jumps.
		[Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
		[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;	// How much to smooth out the movement
		[SerializeField] private bool airControl;									// Whether or not a player can steer while jumping;
		[SerializeField] private LayerMask whatIsGround;							// A mask determining what is ground to the character
		[SerializeField] private Transform groundCheck;								// A position marking where to check if the player is grounded.
		[SerializeField] private Transform ceilingCheck;							// A position marking where to check for ceilings
		[SerializeField] private Collider2D crouchDisableCollider;					// A collider that will be disabled when crouching
		[SerializeField] private LayerMask groundLayer;
		[SerializeField] private LayerMask wallLayer;


		[Header("Events")]
		[Space]

		public UnityEvent onLandEvent;

		public BoolEvent onCrouchEvent;
		private BoxCollider2D boxCollider2D;
		private bool mFacingRight = true;											// For determining which way the player is currently facing.
		private bool mGrounded;														// Whether or not the player is grounded.
		private Rigidbody2D mRigidbody2D;
		private Vector3 mVelocity = Vector3.zero;
		private bool mWasCrouching;

		private void Awake()
		{
			mRigidbody2D = GetComponent<Rigidbody2D>();

			onLandEvent ??= new UnityEvent();

			onCrouchEvent ??= new BoolEvent();
			
			boxCollider2D = GetComponent<BoxCollider2D>();

		}

		private void FixedUpdate()
		{
			bool wasGrounded = mGrounded;
			mGrounded = false;

			// The player is grounded if a circle cast to the ground check position hits anything designated as ground
			// This can be done using layers instead but Sample Assets will not overwrite your project settings.
			Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, KGroundedRadius, whatIsGround);
			foreach (var t in colliders)
			{
				if (t.gameObject == gameObject) continue;
				mGrounded = true;
				if (!wasGrounded)
					onLandEvent.Invoke();
			}
		}


		public void Move(float move, bool crouch, bool jump, float jumpCharge)
		{
			// If crouching, check to see if the character can stand up
			if (!crouch)
			{
				// If the character has a ceiling preventing them from standing up, keep them crouching
				if (Physics2D.OverlapCircle(ceilingCheck.position, KCeilingRadius, whatIsGround))
				{
					crouch = true;
				}
			}

			//only control the player if grounded or airControl is turned on
			if (mGrounded || airControl)
			{

				// If crouching
				if (crouch)
				{
					if (!mWasCrouching)
					{
						mWasCrouching = true;
						onCrouchEvent.Invoke(true);
					}

					// Reduce the speed by the crouchSpeed multiplier
					move *= crouchSpeed;

					// Disable one of the colliders when crouching
					if (crouchDisableCollider != null)
						crouchDisableCollider.enabled = false;
				} else
				{
					// Enable the collider when not crouching
					if (crouchDisableCollider != null)
						crouchDisableCollider.enabled = true;

					if (mWasCrouching)
					{
						mWasCrouching = false;
						onCrouchEvent.Invoke(false);
					}
				}

				// Move the character by finding the target velocity
				var velocity = mRigidbody2D.velocity;
				Vector3 targetVelocity = new Vector2(move * 10f, velocity.y);
				// And then smoothing it out and applying it to the character
				mRigidbody2D.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref mVelocity, movementSmoothing);

				switch (move)
				{
					// If the input is moving the player right and the player is facing left...
					case > 0 when !mFacingRight:
					// Otherwise if the input is moving the player left and the player is facing right...
					// ... flip the player.
					case < 0 when mFacingRight:
						// ... flip the player.
						Flip();
						break;
				}
			}
			
			// If the player should jump...
			var bounds = boxCollider2D.bounds;
			RaycastHit2D raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0, new Vector2(transform.localScale.x, 0), 1f, wallLayer);
			if ((mGrounded || raycastHit.collider != null) && jump)
			{
				mGrounded = false;
				mRigidbody2D.AddForce(new Vector2(0f, jumpForce * jumpCharge));
			}
		}


		private void Flip()
		{
			// Switch the way the player is labelled as facing.
			mFacingRight = !mFacingRight;

			// Multiply the player's x local scale by -1.
			var transform1 = transform;
			Vector3 theScale = transform1.localScale;
			theScale.x *= -1;
			transform1.localScale = theScale;
		}

		[System.Serializable]
		public class BoolEvent : UnityEvent<bool> {}
	}
}