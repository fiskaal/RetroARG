using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject enemy;
    public string enemyQuestName;
    [SerializeField] private QuestManager qm;
    public Transform lootSpawnLocation;
    public EnemyController enemyController;
    [Header("Loot")]
    public List<LootItem> LootTable = new List<LootItem>();
    // Start is called before the first frame update
    private void Update()
    {
        if(health <= 0)
        {
            qm.enemyKilled = enemyQuestName;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            enemy.gameObject.SetActive(false);
            enemyController.EnemyKill();
            // spawn item

            //foreach (LootItem lootItem in LootTable)
            //{
            //    if (Random.Range(0f, 100f) <= lootItem.dropChance)
            //    {
            //        InstantiateLoot(lootItem.itemPrefab);
            //    }
            //}
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
