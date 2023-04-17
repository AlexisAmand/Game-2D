using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    // public bool isPlayerPresentByDefault = false;
    public int coinsPickedUpInThisSceneCount;
    public int keysPickedUpInThisSceneCount;
    public int arrowsPickedUpInThisSceneCount;

    /* pour stocker la position du point de r�aparition */
    public Vector3 respawnPoint;

    /* niveau � d�bloquer */
    public int levelToUnlock;

    public static CurrentSceneManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la sc�ne");
            return;
        }

        Instance = this;

        /* le point de respawn par d�faut est l'endroit o� le joueur commence */

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;

    }
}
