using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] EnemyAttack ea;
    private void AttackPlayer()
    {
        ea.DetectPlayerHit();
    }
}
