using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float weaponHitRadius;
    [SerializeField] private Transform weaponHitPoint;
   
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private LayerMask[] targetLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource chopSound;
    [SerializeField] private AudioSource killSound;
    public int enemiesKilled;
    public int damage;

    

    //[SerializeField] private GameObject ps;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Circle"))
        {
            animator.SetTrigger("isAttacking");
        }
    }

    

    public void DetectEnemyHit()
    {
        Collider[] hit = Physics.OverlapSphere(weaponHitPoint.position, weaponHitRadius, targetLayer[0]);

        if (hit.Length > 0)   
        {
            enemiesKilled++;
            killSound.Play();
            //hit[0].GetComponent<BarrelHealth>().TakeDamage(damage);
            hit[0].GetComponent<EnemyHealth>().TakeDamage(damage);
            Instantiate(hitEffect.transform, hit[0].transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            //Instantiate(ps, transform.position, transform.rotation);
        }
    }

    public void DetectBarrelHit()
    {
        Collider[] hit = Physics.OverlapSphere(weaponHitPoint.position, weaponHitRadius, targetLayer[1]);

        if (hit.Length > 0)
        {
            chopSound.Play();
            hit[0].GetComponent<BarrelHealth>().TakeDamage(damage);
            //hit[0].GetComponent<EnemyHealth>().TakeDamage(damage);
            Instantiate(hitEffect.transform, hit[0].transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            //Instantiate(ps, transform.position, transform.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponHitPoint.position, weaponHitRadius);
    }
}
