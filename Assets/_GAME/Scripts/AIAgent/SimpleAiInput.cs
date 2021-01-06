/*
	Made by Sunny Valle Studio
	(https://svstudio.itch.io)
*/
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SVS
{
	public class SimpleAiInput : MonoBehaviour, IInput
	{
		public Action<Vector3> OnMovementDirectionInput { get; set ; }
		public Action<Vector2> OnMovementInput { get; set; }

		bool playerDetectionResult = false;
		public Transform eyesTransform;
		public Transform playerTransform;
		public LayerMask playerLayer;
		public float visionDistance, stoppingDistance = 1.2f;

		public int maxHealth = 100;
    	public int currentHealth;
		public HealthBar healthBar;
		private bool touched;

		void Start()
		{
			currentHealth = maxHealth;
			healthBar.SetMaxHealth(maxHealth);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			if (playerDetectionResult)
			{
				Gizmos.color = Color.red;
			}
			Gizmos.DrawWireSphere(eyesTransform.position, visionDistance);
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				TakeDamage(20);
			}

			playerDetectionResult = DetectPlayer();
			if (playerDetectionResult)
			{
				var directionToPlayer = playerTransform.position - transform.position;
				directionToPlayer = Vector3.Scale(directionToPlayer, Vector3.forward + Vector3.right);
				if (directionToPlayer.magnitude > stoppingDistance)
				{
					directionToPlayer.Normalize();
					OnMovementInput?.Invoke(Vector2.up);
					OnMovementDirectionInput?.Invoke(directionToPlayer);
					return;
				}else{
					
					if(!touched){
						touched = true;
						StartCoroutine(DrainHealth());     
					}
					 
				}
			}
		
			OnMovementInput?.Invoke(Vector2.zero);
			OnMovementDirectionInput?.Invoke(transform.forward);

		}

		private bool DetectPlayer()
		{
			Collider[] hitColliders = Physics.OverlapSphere(eyesTransform.position, visionDistance, playerLayer);
			foreach (var collider in hitColliders)
			{
				playerTransform = collider.transform;
				return true;
			}
			playerTransform = null;
			return false;
		}

		 public void TakeDamage(int damage)
		{
			currentHealth -= damage;
			healthBar.SetHealth(currentHealth);
		}

		IEnumerator DrainHealth(){

            //
			TakeDamage(10);

            yield return new WaitForSeconds(2.5f);

            touched = false;
		}
	}
}

