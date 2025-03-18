using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("MapManager").AddComponent<MapManager>();
            }
            return _instance;
        }
    }

    private Resource _resource;
    public Resource Resource
    {
        get { return _resource; }
        set { _resource = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
