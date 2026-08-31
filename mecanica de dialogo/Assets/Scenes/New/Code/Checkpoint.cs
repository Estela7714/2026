using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform centerPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.ActivateCheckpoint();
            GetComponent<Collider>().enabled = false;
        }
    }
}