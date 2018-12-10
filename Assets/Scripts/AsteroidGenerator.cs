using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AsteroidGenerator : MonoBehaviour {
	KdTree<Asteroid> asteroids = new KdTree<Asteroid>();
	KdTree<Asteroid> newAsteroidList = new KdTree<Asteroid>();
	ObjectPooler objectPooler;
	public int size;
	private bool spawnedAll = false;
	Asteroid nearestAsteroid;
	void Start()
	{
		objectPooler = ObjectPooler.Instance;
		SpawnAsteroid ();
	}

	private void SpawnAsteroid ()
	{	

		int column = (int) Mathf.Sqrt(size);
		int row = (int) Mathf.Ceil(size / (float) column);

		for (int xPosition = 0; xPosition < row; xPosition++)
		{
			for (int yPosition = 0; yPosition < column; yPosition++)
			{
				Asteroid newAst = objectPooler.SpawnFromPool("Asteroid", new Vector3(xPosition, yPosition, 0), Quaternion.identity);
				asteroids.Add(newAst);
			}
		}
		spawnedAll = true;	
	}
	KdTree<Asteroid> CheckedObject = new KdTree<Asteroid>();
	void Update()
	{
		if(spawnedAll)
		{
			asteroids.UpdatePositions();
			
			for (int i = 0; i < asteroids.Count; i++)
			{
				for (int j = i + 1 ; j < asteroids.Count; j++)
				{
					newAsteroidList.Clear();
					newAsteroidList.AddAll(asteroids.Where((v, k) => k >= j).ToList());

					nearestAsteroid = newAsteroidList.FindClosest(asteroids[i].transform.position);
					
					Debug.DrawLine(asteroids[i].transform.position,nearestAsteroid.gameObject.transform.position,Color.red);
				}
			}
		}
	}
}
