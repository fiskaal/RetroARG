using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCycle : MonoBehaviour
{
    public Material[] materials; // Array to hold 4 materials
    public float cycleDuration = 1f; // Time between material swaps

    private Renderer objRenderer;
    private int currentIndex = 0;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        if (materials.Length == 0)
        {
            Debug.LogError("No materials assigned to the MaterialCycler script.");
            return;
        }

        StartCoroutine(CycleMaterials());
    }

    IEnumerator CycleMaterials()
    {
        while (true) // Infinite loop
        {
            objRenderer.material = materials[currentIndex]; // Set the material

            currentIndex = (currentIndex + 1) % materials.Length; // Cycle index

            yield return new WaitForSeconds(cycleDuration); // Wait before next cycle
        }
    }
}