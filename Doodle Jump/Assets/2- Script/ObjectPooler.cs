using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int num;
    }

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

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> platformPool = new Queue<GameObject>();
            for (int i = 0; i < pool.num; i++)
            {
                GameObject platform = Instantiate(pool.prefab);
                platform.SetActive(false);
                platformPool.Enqueue(platform);
            }
            poolDictionary.Add(pool.tag, platformPool);
        }
    }

    public GameObject SpawnFromPool(string platformTag, Vector3 platformPos, Quaternion platformRot)
    {
        if (!poolDictionary.ContainsKey(platformTag))
        {
            Debug.LogWarning("Pool with tag" + platformTag + "does not exist");
            return null;
        }

        GameObject platformToSpawn = poolDictionary[platformTag].Dequeue();
        platformToSpawn.SetActive(true);
        platformToSpawn.transform.position = platformPos;
        platformToSpawn.transform.rotation = platformRot;
        
        //poolDictionary[platformTag].Enqueue(platformToSpawn);

        return platformToSpawn;
    }

}
