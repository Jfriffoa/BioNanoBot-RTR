using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleObjectPool : MonoBehaviour {

    private static SingleObjectPool instance;

    [SerializeField]
    GameObject m_pooledObject;

    [SerializeField]
    int m_pooledAmount = 5;

    [SerializeField]
    bool m_willGrow = true;

    List<GameObject> m_pooledObjects;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        m_pooledObjects = new List<GameObject>();
        for (int i = 0; i < m_pooledAmount; i++) {
            GameObject go = Instantiate<GameObject>(m_pooledObject, transform.position, transform.rotation, transform);
            go.SetActive(false);
            m_pooledObjects.Add(go);
        }
    }
	
    GameObject Get() {
        for (int i = 0; i < m_pooledObjects.Count; i++) {
            if (!m_pooledObjects[i].activeInHierarchy)
                return m_pooledObjects[i];
        }

        if (m_willGrow) {
            GameObject go = Instantiate<GameObject>(m_pooledObject, transform.position, transform.rotation, gameObject.transform);
            m_pooledObjects.Add(go);
            return go;
        }

        return null;
    }

	public static GameObject GetObject() {
        return instance.Get();
    }
}
