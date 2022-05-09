using UnityEngine;

// Placed on the "PlayerLanePositions" gameobject
public class PlayerLanePositions : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x, PlayerController.Instance.transform.position.y, PlayerController.Instance.transform.position.z); // Update z position to be the same as the player
    }
}
