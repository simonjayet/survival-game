/*
	Made by Sunny Valle Studio
	(https://svstudio.itch.io)
*/
using System;
using UnityEngine;

namespace SVS
{
	public class SimpleAiFriendInput : MonoBehaviour, IInput
	{
		public Action<Vector3> OnMovementDirectionInput { get; set ; }
		public Action<Vector2> OnMovementInput { get; set; }

		bool playerDetectionResult = false;
		public Transform eyesTransform;
		public Transform playerTransform;
		public LayerMask playerLayer;
		public float visionDistance, stoppingDistance = 1.2f;
		public GameObject DialogText;
		private bool dialogOnce = true;

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
					if (dialogOnce)
					{
						DialogText.SetActive(true);
						dialogOnce = false;
					}
					return;
				}
			}else{
				dialogOnce = true;
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
	}
}

