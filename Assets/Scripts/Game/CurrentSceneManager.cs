using UnityEngine;
using UnityEngine.UI;

public class CurrentSceneManager : MonoBehaviour
{
    // public bool isPlayerPresentByDefault = false;
    public int coinsPickedUpInThisSceneCount;
    public int keysPickedUpInThisSceneCount;
    public int arrowsPickedUpInThisSceneCount;

    /* pour stocker la position du point de réaparition */
    public Vector3 respawnPoint;

    /* niveau à débloquer */
    public int levelToUnlock;

    /* nom du niveau en cours */
    public Text levelTitle;

    public static CurrentSceneManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la scène");
            return;
        }

        Instance = this;

        /* le point de respawn par défaut est l'endroit où le joueur commence */

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;

        /* affichage du numéro de niveau en cours */

        int currentlevel = levelToUnlock - 1;
        levelTitle.text = "Level   #" + currentlevel.ToString();

    }

}
