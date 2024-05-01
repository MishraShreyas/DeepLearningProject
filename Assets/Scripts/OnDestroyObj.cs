using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyObj : MonoBehaviour
{
    [SerializeField] bool isHuman;
    public RandomSpawner mySpawner;

    private void OnDestroy()
    {
        if (isHuman)
            mySpawner.humans.Remove(gameObject);
        else
            mySpawner.enemies.Remove(gameObject);
        mySpawner.SpawnObject(isHuman);
    }
}
