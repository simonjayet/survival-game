/*
	Made by Sunny Valle Studio
	(https://svstudio.itch.io)
*/
using UnityEngine;

namespace SVS
{
	public class PlayerAgentController : MonoBehaviour
	{
		/*IInput input;
		PlayerAgentMovement movement;

		private void OnEnable()
		{
			input = GetComponent<IInput>();
			movement = GetComponent<PlayerAgentMovement>();
			input.OnMovementDirectionInput += movement.HandleMovementDirection;
			input.OnMovementInput += movement.HandleMovement;
		}

		private void OnDisable()
		{
			input.OnMovementDirectionInput -= movement.HandleMovementDirection;
			input.OnMovementInput -= movement.HandleMovement;
		}*/

		public CharacterController controller;
		public Transform cam;

		public float speed = 6f;
		public float turnSmoothTime = 0.1f;
		float turnSmoothVelocity;

		void Update()
		{
			float horizontal = Input.GetAxisRaw("Horizontal");
			float vertical = Input.GetAxisRaw("Vertical");
			Vector3 direction = new Vector3(horizontal,0f,vertical);

			if (direction.magnitude >= 0.1f){

				float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
				float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, turnSmoothTime);
				transform.rotation = Quaternion.Euler(0f,angle,0f);

				Vector3 moveDir =  Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
				controller.Move(moveDir * speed * Time.deltaTime);
			}

		}
	}

	
}

