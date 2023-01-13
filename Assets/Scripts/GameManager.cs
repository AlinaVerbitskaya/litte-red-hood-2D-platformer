using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private Checkpoint defaultCheckpoint;
    private Checkpoint currentCheckpoint;

    void Start()
    {
        currentCheckpoint = defaultCheckpoint;
        StartCoroutine(currentCheckpoint.Activate());
    }

    private void OnEnable()
    {
        EventManager.OnPlayerInDeathTrigger += GetPlayerToCheckpoint;
        EventManager.OnCheckpointActivated += ChangeCheckpoint;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerInDeathTrigger -= GetPlayerToCheckpoint;
        EventManager.OnCheckpointActivated -= ChangeCheckpoint;
    }


    private void ChangeCheckpoint(Checkpoint newCheckpoint)
    {
        StartCoroutine(currentCheckpoint.Deactivate());
        currentCheckpoint = newCheckpoint;
    }

    private void GetPlayerToCheckpoint()
    {
        player.Teleport(currentCheckpoint.teleportPoint.position);
    }
}
