using UnityEngine;

public class GroundTile : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TrackGeneration.Instance.ChooseNextTrack(); // Spawn the next tile
            Destroy(gameObject, 2); // Destroy the ground tile 2 seconds after the player leaves it
        } 
    }
}
