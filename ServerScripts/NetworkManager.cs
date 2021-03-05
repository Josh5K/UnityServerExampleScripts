using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject playerPrefab;
    public bool needsHost;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, new Vector3(5f, 0.5f, 15f), Quaternion.identity).GetComponent<Player>();
    }

    public bool NeedsHost()
    {
        if(needsHost)
        {
            needsHost = false;
            return true;
        }
        return needsHost;
    }

    public void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        needsHost = true;

        Server.Start(50, 26950);
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }
}
