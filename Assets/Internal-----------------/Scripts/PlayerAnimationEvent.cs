using UnityEngine;
namespace Ketra
{
    public class PlayerAnimationEvent : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] PlayerAttack pa;
        [SerializeField] EnemyAttack ea;
        [SerializeField] AudioSource swoosh;
        void Start()
        {

        }

        // Update is called once per frame
        private void AttackEnemy()
        {
            pa.DetectEnemyHit();

        }

        private void AttackBarrel()
        {

            pa.DetectBarrelHit();
        }

        private void AttackPlayer()
        {


            ea.DetectPlayerHit();
        }

        private void SwordSound()
        {
            swoosh.Play();
        }
    }
}
