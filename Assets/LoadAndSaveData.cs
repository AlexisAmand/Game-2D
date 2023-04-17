
using UnityEngine;

public class LoadAndSaveData : MonoBehaviour
{

    public static LoadAndSaveData Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la sc�ne");
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /* chargement des donn�es sauvegard�es */
        /* O est la valeur par d�faut, si le joueur n'a pas encore jou� */
        Inventory.Instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);

        /* mise � jour de l'interface avec les donn�es issues de la sauvegarde */
        Inventory.Instance.UpdateTextUI();

        /* r�cup de la vie */
        /* par d�faut, c'est 100 */
        int currentHealth = PlayerPrefs.GetInt("playerHealth", PlayerHealth.Instance.maxHealth);
        PlayerHealth.Instance.currentHealth = currentHealth;
        PlayerHealth.Instance.healthBar.SetHealth(currentHealth);

    }

    public void SaveData()
    {
        /* sauvegarde du nombre de pi�ces en passant la valeur */
        PlayerPrefs.SetInt("coinsCount", Inventory.Instance.coinsCount);

        /* sauvegarde du nombre de points de vie en passant la valeur */
        PlayerPrefs.SetInt("playerHealth", PlayerHealth.Instance.currentHealth);

        /* sauvegarde du niveau atteint si le niveau atteint est un niveau qui n'a pas encore �t� d�bloqu� */

        if(CurrentSceneManager.Instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.Instance.levelToUnlock);
        }

    }

}
