using System;
using System.Collections.Generic;
using UnityEngine;


public enum PoolObjectType
{
    Green,
    Brown,
    Blue,
    White,
    Spring,
    Trampoline,
    Propeller,
    Jet,
    Hole,
    Enemy
}

[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int poolAmount = 0;
    public GameObject prefab;
    public GameObject container;
}

public class ObjectPooler : MonoBehaviour
{
    
    [SerializeField] private List<PoolInfo> allPoolsList;

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public Dictionary<PoolObjectType, Queue<GameObject>> poolDictionary;
    public Queue<GameObject> poolObjects = new Queue<GameObject>();

    private void Start()
    {
        poolDictionary = new Dictionary<PoolObjectType, Queue<GameObject>>();
        foreach (PoolInfo pool in allPoolsList)
        {
            poolObjects = new Queue<GameObject>();
            for (int i = 0; i < pool.poolAmount; i++)
            {
                GameObject firstObject = Instantiate(pool.prefab, pool.container.transform);
                firstObject.SetActive(false);
                poolObjects.Enqueue(firstObject);
            }
            poolDictionary.Add(pool.type, poolObjects);
        }
    }

    public GameObject SpawnFromPool(PoolObjectType type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogError("Pool With Type:" + " " + type + "Does Not Exist");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[type].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        //poolDictionary[type].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
