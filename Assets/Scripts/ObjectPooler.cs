using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoBehaviour {

	[System.Serializable]
	public class Pool{
		public string name;
		public int numberOfObjects;
		public GameObject prefab; 
	}
	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;
	public static ObjectPooler Instance;
	void Awake()
	{
		Instance = this;
	}
	void Start()
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
		foreach (Pool item in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();
			for (int i = 0; i < item.numberOfObjects; i++)
			{
				GameObject obj = Instantiate(item.prefab);
				obj.SetActive(false);
				obj.name = i.ToString();
				objectPool.Enqueue(obj);
			}	
			poolDictionary.Add(item.name, objectPool); 
		}
	}
	public Asteroid SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
	{
		if(!poolDictionary.ContainsKey(tag))
		{
			Debug.LogError("Pool with the tag " + tag + " doesn't exist!");
			return null;
		} 
		
		GameObject objectToSpawn = poolDictionary[tag].Dequeue();
		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		return objectToSpawn.GetComponent<Asteroid>();
	}
}
