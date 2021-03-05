using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static Dictionary<int, ItemSpawner> spawners = new Dictionary<int, ItemSpawner>();
    private static int nextSpawnerID = 1;

    public int spawnerID;
    public bool hasItem = false;

    // Start is called before the first frame update
    void Start()
    {
        hasItem = false;
        spawnerID = nextSpawnerID;
        nextSpawnerID++;
        spawners.Add(spawnerID, this);

        StartCoroutine(SpawnItem());
    }

    private IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(10f);

        hasItem = true;
        ServerSend.ItemSpawned(spawnerID);
    }

    private void ItemPickedUp(int _player)
    {
        hasItem = false;
        ServerSend.ItemPickedUp(spawnerID, _player);

        StartCoroutine(SpawnItem());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(hasItem && collider.CompareTag("Player"))
        {
            Player _player = collider.GetComponent<Player>();
            if(_player.AttemptPickupItem())
            {
                ItemPickedUp(_player.id);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
