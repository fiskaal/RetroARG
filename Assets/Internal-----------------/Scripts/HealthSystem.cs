using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthSystem : MonoBehaviour
{

    public int dealDamage = 1;
    public int maxHealth = 5;
    public int currentHealth = 3;
    [SerializeField] TMP_Text lifeText;
    public bool isDead = false;
    public GameObject gameOverScreen;
    public GameObject selectedGameOverButton;
    public GameObject player;
    [SerializeField] Animator animatorHurt;
    [SerializeField] Animator animatorImage;
    [SerializeField] public Vector3 checkpoint;
    public Transform spawnpoint;
    public int healAmount = 1;
    [SerializeField] public Animator animatorHeal;
    [SerializeField] public Animator animatorIcon;
    [SerializeField] private AudioSource healSound;
    public float respawnTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        checkpoint = spawnpoint.position;
        PlayerDied();
        lifeText.text = currentHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kill")
        {

            //other.gameObject.GetComponent<CharacterController>().Move(checkpoint - transform.position);
            DamagePlayer();
            StartCoroutine(PauseCoroutine());
        }

        if (other.gameObject.tag == "Heal" && currentHealth < maxHealth)
        {

            currentHealth += healAmount;
            Debug.Log("HEAL!");
            animatorHeal.PlayInFixedTime("NotificationAnimation", -1, 0f);
            animatorIcon.PlayInFixedTime("iconSizeUpDown", -1, 0f);
            //other.gameObject.SetActive(false);
            other.transform.parent.gameObject.SetActive(false);
            healSound.Play();

        }
    }

    public void DamagePlayer()
    {
        player.gameObject.GetComponent<CharacterController>().Move(checkpoint - transform.position);
        
        currentHealth -= dealDamage;
        Debug.Log("HURT!");
        animatorHurt.PlayInFixedTime("NotificationAnimation", -1, 0f);
        animatorImage.PlayInFixedTime("ImageFadeInOut", -1, 0f);
        //Time.timeScale = 0;
        
        
        //player.gameObject.SetActive(true);
        player.gameObject.GetComponent<CharacterController>().Move(checkpoint - transform.position);
    }





    public void PlayerDied()
    {
        if (currentHealth <= 0)
        {
            EventSystem.current.SetSelectedGameObject(selectedGameOverButton);
            EventSystem.current.firstSelectedGameObject = selectedGameOverButton;
            isDead = true;
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);


        }
    }

    private IEnumerator PauseCoroutine()
    {
        Time.timeScale = 0f; // Pause the game
        yield return new WaitForSecondsRealtime(respawnTime); // Wait for 2 seconds in real-time
        Time.timeScale = 1f; // Resume the game
    }


}
