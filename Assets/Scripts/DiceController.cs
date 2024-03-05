using System.Collections;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    private Vector3[] originalPositions;
    private bool isDiceThrown = false;

    void Start()
    {
        // Save the original positions when the game starts
        SaveOriginalPositions();
    }

    void Update()
    {
        // Check for mouse clicks
        if (Input.GetMouseButtonDown(0))
        {
            if (!isDiceThrown)
            {
                // First click: Throw the dice
                StartCoroutine(ThrowDice());
            }
            else
            {
                // Second click: Reset the dice to their original positions
                ResetDice();
            }
        }
    }

    void SaveOriginalPositions()
    {
        // Save the original positions of the dice objects
        originalPositions = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            originalPositions[i] = transform.GetChild(i).position;
        }
    }

    IEnumerator ThrowDice()
    {
        // Simulate a dice throw by applying random forces to the dice objects
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(Random.onUnitSphere * 5f, ForceMode.Impulse);
            }
        }

        yield return new WaitForSeconds(0); // Adjust the duration of the throw as needed

        // Set the flag to indicate that the dice are thrown
        isDiceThrown = true;
    }

    void ResetDice()
    {
        // Reset the dice to their original positions
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Rigidbody rb = child.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            child.position = originalPositions[i];
        }

        // Reset the flag
        isDiceThrown = false;
    }
}