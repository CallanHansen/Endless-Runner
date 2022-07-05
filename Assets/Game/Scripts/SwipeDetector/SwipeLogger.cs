using UnityEngine;

// Takes care of controls on mobile
public class SwipeLogger : MonoBehaviour
{
    [SerializeField] private SwipeDetector swipeDetector;

    void Awake()
    {
        swipeDetector.OnSwipe += SwipeDetector_OnSwipe; // Adding the action
    }

    // Called when finger is swiped on screen
    void SwipeDetector_OnSwipe(SwipeData data)
    {
        Debug.Log("Swipe in Direction: " + data.Direction);

        if (PlayerController.Instance.CanMove)
        {
            // Swiping Inputs
            switch (data.Direction)
            {
                case SwipeDirection.Right:
                    if (PlayerController.Instance.currentLaneIndex < PlayerController.Instance.playerLaneLocations.Length - 1) // Ensuring the player doesnt go outside array bounds
                    {
                        PlayerController.Instance.currentLaneIndex++;
                    }
                    break;

                case SwipeDirection.Up:
                    PlayerController.Instance.Jump();
                    break;

                case SwipeDirection.Down:
                    PlayerController.Instance.Slide();
                    break;

                case SwipeDirection.Left:
                    if (PlayerController.Instance.currentLaneIndex > 0) // Ensure the player doesn't go outside array bounds
                    {
                        PlayerController.Instance.currentLaneIndex--;
                    }
                    break;
            }
        }      
    }
}