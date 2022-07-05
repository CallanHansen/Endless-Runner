using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);       
    }
}
