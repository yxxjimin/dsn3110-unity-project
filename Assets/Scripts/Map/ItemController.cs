using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour{
    public enum ItemType { FISH, SUPERFISH, BOMB };
    public ItemType itemType;
    public GameObject[] itemPrefabs;
    public float fishRatio;
    public float superFishRatio;

    void OnEnable() {
        float probability = Random.Range(0f, 1f);
        itemType = (probability > fishRatio) ? ItemType.BOMB : ((probability > fishRatio * superFishRatio) ? ItemType.FISH : ItemType.SUPERFISH);
        
        switch (itemType) {
            case ItemType.FISH: 
                gameObject.tag = "Fish";
                SelectItem(0);
                break;
            case ItemType.SUPERFISH: 
                gameObject.tag = "Super Fish";
                SelectItem(1);
                break;
            case ItemType.BOMB:
                gameObject.tag = "Bomb";
                SelectItem(2);
                break;
            default: break;
        }
    }

    void SelectItem(int idx) {
        for (int i = 0; i < itemPrefabs.Length; i++) {
            if (i == idx) itemPrefabs[i].SetActive(true);
            else itemPrefabs[i].SetActive(false);
        }
    }
}
