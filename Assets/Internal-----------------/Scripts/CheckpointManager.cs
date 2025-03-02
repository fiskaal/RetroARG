using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] Transform defaultCheckpoint;
    [SerializeField] Transform checkpointSetter;
    [SerializeField] GameObject checkpointObject;
    [SerializeField] Animator animatorCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            defaultCheckpoint.position = checkpointSetter.position;
            animatorCheckpoint.PlayInFixedTime("NotificationAnimation", -1, 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointObject.SetActive(false);
        }
    }
}
