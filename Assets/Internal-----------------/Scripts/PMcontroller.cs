using UnityEngine;

public class PMcontroller : MonoBehaviour
{
    public GameObject[] objects;
    public int index = 0;
    public PauseMenuSimple pms;
    public AudioSource buttonSound;

    void Start()
    {
        ShowOnlyCurrent(index);
    }

    void Update()
    {
        if (objects == null || objects.Length == 0) return;
        if (pms.isPaused)
        {
            if (Input.GetButtonDown("R1"))
            {
                buttonSound.Play();
                index = (index + 1) % objects.Length;
                ShowOnlyCurrent(index);
            }

            if (Input.GetButtonDown("L1"))
            {
                buttonSound.Play();
                index = (index - 1 + objects.Length) % objects.Length;
                ShowOnlyCurrent(index);
            }
        }
    }

    void ShowOnlyCurrent(int i)
    {
        for (int j = 0; j < objects.Length; j++)
        {
            if (objects[j] != null)
                objects[j].SetActive(j == i);
        }

        Debug.Log("Visible object at index " + i + ": " + objects[i].name);
    }
}
