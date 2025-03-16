using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float weaponHitRadius;
    [SerializeField] private Transform weaponHitPoint;
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private LayerMask targetLayer;
    
    [SerializeField] private AudioSource chopSound;
    [SerializeField] private HealthSystem hs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetectPlayerHit()
    {
        Collider[] hit = Physics.OverlapSphere(weaponHitPoint.position, weaponHitRadius, targetLayer);

        if (hit.Length > 0)
        {
            HealthSystem targetHealth = hit[0].GetComponent<HealthSystem>();
            //targetHealth.DamagePlayer();
            //chopSound.Play();
            hs.DamagePlayer();
            //hit[0].GetComponent<BarrelHealth>().TakeDamage(damage);
            //hit[0].GetComponent<HealthSystem>().DamagePlayer();
            Instantiate(hitEffect.transform, hit[0].transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            //Instantiate(ps, transform.position, transform.rotation);
            //gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponHitPoint.position, weaponHitRadius);
    }
}
