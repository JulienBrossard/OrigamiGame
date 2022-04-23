using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler instance;
    
    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    [Tooltip("La clé du pooler de l'objet attitré, attention pour les objets du level design le nom de l'objet et de sa clé doivent être exactement le même")]
    [SerializeField] private List<PoolKey> poolKeys = new List<PoolKey>();

    private int i;

    [System.Serializable]
    public class Pool
    {
        [Tooltip("Le préfab de l'objet, à récupérer dans le dossier Préfabs")]
        public GameObject prefab;
        public Queue<GameObject> queue = new Queue<GameObject>();
        [Tooltip("Le nombre d'objet à pool")]
        public int baseCount;
        [Tooltip("Le refresh de base pour pool, généralement entre 1 et 5")]
        public float baseRefreshSpeed = 5;
        public float refreshSpeed = 5;
    }

    [System.Serializable]
    public class PoolKey
    {
        public string key;
        public Pool pool;
    }

    private void Awake()
    {
        instance = this;
        
        InitPools();
        PopulatePools();
    }


    void InitPools()
    {
        for (i = 0; i < poolKeys.Count; i++)
        {
            pools.Add(poolKeys[i].key, poolKeys[i].pool);
        }
    }

    void PopulatePools()
    {
        foreach (var pool in pools)
        {
            PopulatePool(pool.Value);
        }
    }

    void PopulatePool(Pool pool)
    {
        for (i = 0; i < pool.baseCount; i++)
        {
            AddInstance(pool);
        }
    }

    private GameObject objectInstance;
    void AddInstance(Pool pool)
    {
        objectInstance = Instantiate(pool.prefab,transform);
        objectInstance.SetActive(false);
        
        pool.queue.Enqueue(objectInstance);
    }

    private void Start()
    {
        InitRefreshCount();
    }

    void InitRefreshCount()
    {
        foreach (KeyValuePair<string, Pool> pool in pools)
        {
            StartCoroutine(RefreshPool(pool.Value, pool.Value.refreshSpeed));
        }
    }

    IEnumerator RefreshPool(Pool pool, float t)
    {
        yield return new WaitForSeconds(t);
        if (pool.queue.Count < pool.baseCount)
        {
            AddInstance(pool);
            pool.refreshSpeed = pool.baseRefreshSpeed * pool.queue.Count / pool.baseCount;
        }
        
        StartCoroutine(RefreshPool(pool, pool.refreshSpeed));
    }

    public GameObject Pop(string key)
    {
        if (pools[key].queue.Count == 0)
        {
            Debug.LogWarning("Pool of "+key+" is empty");
            AddInstance(pools[key]);
        }
        
        objectInstance = pools[key].queue.Dequeue();
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public void DePop(string key, GameObject go)
    {
        pools[key].queue.Enqueue(go);

        go.transform.parent = transform;
        go.SetActive(false);
    }

    public void DelayedDePop(float t, string key, GameObject go)
    {
        StartCoroutine(DelayedDePopCoroutine(t, key, go));
    }

    IEnumerator DelayedDePopCoroutine(float t, string key, GameObject go)
    {
        yield return new WaitForSeconds(t);
        DePop(key, go);
    }
}
