using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerAttack pa;
    void Start()
    {

    }

    // Update is called once per frame
    private void Attack()
    {
        pa.DetectHit();
    }
}
