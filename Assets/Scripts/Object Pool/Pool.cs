using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private List<GameObject> inactiveObjects = new List<GameObject>();
    private GameObject prefab;

    private Transform poolManager;

    public Pool(GameObject prefab) {
        this.prefab = prefab;
        poolManager = FindObjectOfType<PoolManager>().transform;
    }

    public GameObject CreateObject(Vector3 position, Quaternion  rotation) {
        GameObject obj;
        if (inactiveObjects.Count == 0) {
            obj = Instantiate(prefab, position, rotation);
            obj.name = prefab.name;
            obj.transform.SetParent(poolManager);
        }
        else {
            obj = inactiveObjects[inactiveObjects.Count - 1];
            inactiveObjects.RemoveAt(inactiveObjects.Count - 1);
        }
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    public void ReturnObject(GameObject obj) {
        obj.SetActive(false);
        inactiveObjects.Add(obj);
    }
}