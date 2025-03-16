using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject barrel;
    public Transform lootSpawnLocation;
    [Header("Loot")]
    public List<LootItem> LootTable = new List<LootItem>();
    // Start is called before the first frame update
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            barrel.gameObject.SetActive(false);
            // spawn item

            foreach (LootItem lootItem in LootTable)
            {
                if(Random.Range(0f, 100f) <= lootItem.dropChance)
                {
                    InstantiateLoot(lootItem.itemPrefab);
                }
            }
        }
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, lootSpawnLocation.position, Quaternion.identity);
        }
    }
}
