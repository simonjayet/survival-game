/*
	Made by Sunny Valle Studio
	(https://svstudio.itch.io)
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SVS
{
	public class PlayerAgentMovement : MonoBehaviour
	{
		CharacterController controller;
		Animator animator;
		public float rotationSpeed, movementSpeed, gravity = 20;
		Vector3 movementVector = Vector3.zero;
		private float desiredRotationAngle = 0;
		public Staminabar staminaBar;
		public int maxStamina = 100;
    	public int currentStamina;
		private bool canRun = true;
		private int runnningTime;

		private float inputVerticalDirection = 0;

		private void Start()
		{
			currentStamina = maxStamina;
        	staminaBar.SetMaxStamina(maxStamina);
			controller = GetComponent<CharacterController>();
			animator = GetComponent<Animator>();
		}

		public void HandleMovement(Vector2 input)
		{
			if (controller.isGrounded)
			{
				if(input.y != 0)
				{
					if(input.y > 0)
					{
						inputVerticalDirection = Mathf.CeilToInt(input.y);
					}
					else
					{
						inputVerticalDirection = Mathf.FloorToInt(input.y);
					}
					movementVector = input.y*transform.forward * movementSpeed;
				}
				else
				{
					movementVector = Vector3.zero;
					animator.SetFloat("move", 0);
				}
			}
		}

		public void HandleMovementDirection(Vector3 direction)
		{
			desiredRotationAngle = Vector3.Angle(transform.forward, direction);
			var crossProduct = Vector3.Cross(transform.forward, direction).y;
			if (crossProduct < 0)
			{
				desiredRotationAngle *= -1;
			}
		}

		private void RotateAgent()
		{
			if(desiredRotationAngle > 10 || desiredRotationAngle < -10)
			{
				transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
			}
		}

		private float SetCorrectAnimation(float inputVerticalDirection)
		{
			
			if(!canRun){
				animator.SetFloat("move",0.2f);
			}

			float currentAnimationSpeed = animator.GetFloat("move");
			
			if(desiredRotationAngle > 10 || desiredRotationAngle < -10)
			{
				if(Mathf.Abs(currentAnimationSpeed) < 0.2f)
				{
					currentAnimationSpeed += inputVerticalDirection* Time.deltaTime * 2;
					currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, -0.2f, 0.2f);
				}
				animator.SetFloat("move", currentAnimationSpeed);
			}
			else
			{
				if (currentAnimationSpeed < 1)
				{
					currentAnimationSpeed += inputVerticalDirection* Time.deltaTime * 2;
				}

				
				animator.SetFloat("move", Mathf.Clamp(currentAnimationSpeed,-1,1));

				/*	
				if (canRun)
				{
				} else{
					animator.SetFloat("move", currentAnimationSpeed);
				}
				*/
				
			}
			return Mathf.Abs(currentAnimationSpeed);
		}

		void DecreaseStamina(int points)
		{
			
			currentStamina -= points;
			staminaBar.SetStamina(currentStamina);
			
			if (currentStamina <= 0)
			{
				canRun = false;
				StartCoroutine(FillStamina());
			}

		}

		void IncreaseStamina(int points)
		{
			currentStamina += points;
			staminaBar.SetStamina(currentStamina);
		}

		IEnumerator FillStamina(){

			for (int i = 0; i <= 9; i++)
			{	
				if (currentStamina == maxStamina)
				{
					break;
				}
				//Debug.Log("currentStamina : " + currentStamina);
				currentStamina += 10;
				staminaBar.SetStamina(currentStamina);
				yield return new WaitForSeconds(0.5f);
			}

			canRun = true;
			
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				DecreaseStamina(20);
			}

			if (controller.isGrounded)
			{
				if (movementVector.magnitude > 0)
				{
					var animationSpeedMultiplier = SetCorrectAnimation(inputVerticalDirection);
					RotateAgent();
					movementVector *= animationSpeedMultiplier;

				}
			}
			
			if(movementVector != Vector3.zero && canRun){

				runnningTime += 1;
			}

			if(runnningTime == 100){
				runnningTime = 0;
				DecreaseStamina(20);
			}

			movementVector.y -= gravity;
			controller.Move(movementVector * Time.deltaTime);
		}
		
	}
}

