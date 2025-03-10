using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float weaponHitRadius;
    [SerializeField] private Transform weaponHitPoint;
    [SerializeField] private int damage;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("isAttacking");
        }
    }

    

    public void DetectHit()
    {
        Collider[] hit = Physics.OverlapSphere(weaponHitPoint.position, weaponHitRadius, targetLayer);

        if (hit.Length > 0)
        {
            hit[0].GetComponent<Health>().TakeDamage(damage);
            Instantiate(hitEffect.transform, hit[0].transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponHitPoint.position, weaponHitRadius);
    }
}
