using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    //public static RandomSpawner instance;

    [Header("Range")]
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX, minZ, maxZ;

    [Header("Spawning")]
    [SerializeField]
    private GameObject humanPrefab;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int objectCount;

    public List<GameObject> humans = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartSpawning()
    {
        for (int i=0; i < objectCount; i++)
        {
            SpawnObject(true);
            SpawnObject(false);
        }
    }

    public void SpawnObject(bool isHuman)
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        float rot = Random.Range(0, 360);
        Vector3 pos = new Vector3(x, 0, z);
        GameObject go = isHuman? Instantiate(humanPrefab, transform) : Instantiate(enemyPrefab, transform);
        go.transform.localPosition = pos;
        go.transform.rotation = Quaternion.Euler(0, rot, 0);
        go.GetComponent<OnDestroyObj>().mySpawner = this;

        if (isHuman)
            humans.Add(go);
        else
            enemies.Add(go);
    }
}
