using UnityEngine;
using System.Collections;

public class GroundTile : MonoBehaviour
{
    private const float TIME_UNTIL_DISABLE = 2.0f;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TrackGeneration.Instance.ChooseNextTrack(); // Spawn the next tile
            StartCoroutine(DisableGameobject());
        } 
    }

    IEnumerator DisableGameobject()
    {
        gameObject.SetActive(true);

        yield return new WaitForSeconds(TIME_UNTIL_DISABLE);

        gameObject.SetActive(false);

        yield break;
    }
}
