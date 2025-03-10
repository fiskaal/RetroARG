using TMPro;
using UnityEngine;

public class AppleManager : MonoBehaviour
{
    [SerializeField] private TMP_Text appleText;
    [SerializeField] private int appleCount = 0;
    [SerializeField] private int maxAppleCount = 3;
    [SerializeField] private AudioSource appleSound;
    //[SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject portal;
    [SerializeField] private Animator animatorIcon;
    [SerializeField] private HealthSystem hs;
    


    private void Awake()
    {
        portal.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (appleCount == maxAppleCount)
        {
            hs.currentHealth++;
            appleCount = 0;
        }

        appleText.text = appleCount.ToString();

        if (appleCount == maxAppleCount)
        {
            portal.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
            //other.gameObject.SetActive(false);
            other.transform.parent.gameObject.SetActive(false);
            appleCount++;
            animatorIcon.PlayInFixedTime("iconSizeUpDown", -1, 0f);
            //ps.Play();
            //Instantiate(ps, transform.position, transform.rotation);
            appleSound.Play();

        }
    }

}
