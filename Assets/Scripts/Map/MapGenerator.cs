using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;

    // Tile Pooling
    public float tileLength;
    public int displayedTiles;
    public GameObject tilePrefab;
    private Queue<GameObject> inactiveTileQueue;
    private Queue<GameObject> activeTileQueue;
    private GameObject mostRecentTile;

    // Item Pooling
    public int totalItemCount = 10;
    public GameObject itemPrefab;
    private Queue<GameObject> inactiveItemQueue;
    private Queue<GameObject> activeItemQueue;

    void OnEnable() {
        transform.position = new Vector3(0, -1, 0);

        // Tile Pooling Initialization
        inactiveTileQueue = new Queue<GameObject>();
        activeTileQueue = new Queue<GameObject>();
        for (int i = 0; i < 10; i++) {
            GameObject tile = Instantiate(tilePrefab, this.transform);
            inactiveTileQueue.Enqueue(tile);
            tile.SetActive(false);
        }
        mostRecentTile = inactiveTileQueue.Dequeue();
        mostRecentTile.SetActive(true);
        activeTileQueue.Enqueue(mostRecentTile);
        mostRecentTile.transform.localPosition = new Vector3(0, 0, -tileLength / 2);

        // Item Pooling Initialization
        inactiveItemQueue = new Queue<GameObject>();
        activeItemQueue = new Queue<GameObject>();
        for (int i = 0; i < totalItemCount; i++) {
            GameObject item = Instantiate(itemPrefab, this.transform);
            inactiveItemQueue.Enqueue(item);
            item.SetActive(false);
        }

        tilePrefab.SetActive(false);
        itemPrefab.SetActive(false);

        // Start tile
        GameObject startTile = GameObject.Find("StartTile");
        startTile.transform.localPosition = new Vector3(0, 0, 3f);

        // Finish line
        GameObject finishLine = GameObject.FindWithTag("Finish");
        finishLine.SetActive(true);
        finishLine.transform.localPosition = new Vector3(0, -0.48f, gameManager.zLimit);
    }

    void Update() {
        // Extend map
        Vector3 playerPosition = player.transform.position;
        Vector3 tilePosition = mostRecentTile.transform.position;

        if ((playerPosition.z > tilePosition.z - (displayedTiles - 2) * tileLength) && (tilePosition.z < gameManager.zLimit - tileLength / 2)) {
            LoadTile();
        }
    }

    void LoadTile() {
        // Activate tile from pool
        GameObject newTile = inactiveTileQueue.Dequeue();
        newTile.SetActive(true);
        newTile.transform.localPosition = new Vector3(0, 0, mostRecentTile.transform.localPosition.z + tileLength);
        activeTileQueue.Enqueue(newTile);
        mostRecentTile = newTile;

        // Generate items
        LoadItems(totalItemCount / displayedTiles);

        // Collect tile to pool
        GameObject leastRecentTile;
        if (activeTileQueue.Peek().transform.position.z < player.transform.position.z - tileLength - 1f) {
            leastRecentTile = activeTileQueue.Dequeue();
            leastRecentTile.SetActive(false);
            inactiveTileQueue.Enqueue(leastRecentTile);
        }
    }

    void LoadItems(int num) {
        // Debug.LogFormat("Active: {0}, Inactive: {1}", activeItemQueue.Count, inactiveItemQueue.Count);
        // Collect items to pool
        if (activeItemQueue.Count > 0) {
            GameObject leastRecentItem;
            while (activeItemQueue.Peek().transform.position.z < player.transform.position.z) {
                leastRecentItem = activeItemQueue.Dequeue();
                leastRecentItem.SetActive(false);
                inactiveItemQueue.Enqueue(leastRecentItem);
            }
        }
        
        // Activate item from pool
        int itemCount = Random.Range(1, num);
        for (int i = 0; i < itemCount; i++) {
            GameObject item = inactiveItemQueue.Dequeue();
            item.SetActive(true);
            float generateX = (Random.Range(0, 3) - 1) * 1.8f;
            float generateZ = mostRecentTile.transform.localPosition.z + Random.Range(0, (int) tileLength); // Range: float -> [,] / int -> [,)
            item.transform.localPosition = new Vector3(generateX, 0, generateZ);
            activeItemQueue.Enqueue(item);
        }        
    }

    void OnDisable() {
        GameObject item;
        for (int i = 0; i < activeItemQueue.Count; i++) {
            item = activeItemQueue.Dequeue();
            item.SetActive(false);
            inactiveItemQueue.Enqueue(item);
        }

        GameObject tile;
        for (int i = 0; i < activeTileQueue.Count; i++) {
            tile = activeTileQueue.Dequeue();
            tile.SetActive(false);
            activeTileQueue.Enqueue(tile);
        }
    }
}
