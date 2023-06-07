using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public static DataManager instance;
    public Dictionary<string, int> itemInfoDict;
    
    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        itemInfoDict = new Dictionary<string, int>();
        itemInfoDict.Add("Fish", 0);
        itemInfoDict.Add("Bomb", 0);
    }
}
