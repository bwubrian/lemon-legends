using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
	move,
	attack
}

public class PlayerController : MonoBehaviour {
	// How fast the character moves (units/second)	
	[SerializeField] private float speed;
	
	[SerializeField] private Rigidbody2D rb; 

	[SerializeField] private Animator animator;

	private Vector3 currentDirection = Vector3.left;
	
	private PlayerState currentState = PlayerState.move;

	public const float ATK_ANIM_LENGTH = 0.217f;

    // Use this for initialization
    void Start () {
		animator.SetFloat("moveX", -1f);
	}
	
	// Update is called once per frame
	void Update () {
		// Check for attack input (higher priority than movement)
		if (Input.GetKeyDown("x") && currentState != PlayerState.attack)
		{
			animator.SetBool("isMoving", false);
			StartCoroutine(AttackCoroutine());
		}
		else if (currentState == PlayerState.move) {
			MoveCharacter();
		}
	}

	private void MoveCharacter() {
		// Create a direction vector from (-1,-1) to (1,1) using the keyboard inputs
		Vector3 direction = Vector3.zero;
		direction.x = Input.GetAxisRaw("Horizontal");
		direction.y = Input.GetAxisRaw("Vertical");
		// Make sure that the diagonal vector has a magnitude of 1
		direction = Vector3.Normalize(direction);

		if (direction != Vector3.zero) {
			// Adjust the value of speed to adjust how quickly the player moves
			Vector3 distance = transform.position + direction * speed * Time.deltaTime;
			rb.MovePosition(distance);
			
			// Set left or right walking animation
			if (direction.x < 0f) {
				animator.SetFloat("moveX", -1f);
			}
			else if (direction.x > 0f) {
				animator.SetFloat("moveX", 1f);
			}
			animator.SetBool("isMoving", true);
		}
		else {
			// Trigger the idle animation
			animator.SetBool("isMoving", false);
		}

		// Update the direction the player is facing
		if (direction != Vector3.zero) {
			if (direction.x < 0f) {
				currentDirection = Vector3.left;
			}
			else {
				currentDirection = Vector3.right;
			}
		}
	}
	
	private IEnumerator AttackCoroutine() {
		// Play the attack animation
		currentState = PlayerState.attack;
		animator.SetBool("isAttacking", true);
		// Wait 1 frame and stop the attack animation from looping
		yield return null;
		animator.SetBool("isAttacking", false);
		// Wait until the attack animation is finished
		yield return new WaitForSeconds(ATK_ANIM_LENGTH);
		// Attack animation finished, allow movement again
		currentState = PlayerState.move;
	}
}
