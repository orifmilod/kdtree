using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
public class AsteroidMoving : MonoBehaviour {

	public float speed;
}

class MovingSystem : ComponentSystem
{
	struct Components
	{
		public Transform transform;
		public AsteroidMoving asteroidMoving;
	}
	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Components>())
		{
			e.transform.Rotate(0, e.asteroidMoving.speed * Time.deltaTime, 0);
		} 
	}
}
