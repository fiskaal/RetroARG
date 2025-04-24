using UnityEngine;

namespace Ketra
{
    public class EnemyAnimationEvent : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] EnemyAttack ea;
        private void AttackPlayer()
        {
            ea.DetectPlayerHit();
        }
    }
}
