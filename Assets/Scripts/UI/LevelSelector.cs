using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public Button[] levelButtons;

    public void Start()
    {
        /* niveau max atteint, si on a jamais joué, c'est 1 */
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        /* au lancement du "Select Level" tous les boutons sont grisés par défaut */
        for (int i = 0; i < levelButtons.Length; i++) 
        {
            /* les niveaux supérieux au niveau max atteint sont grisés */
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void LoadLevelPassed(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }
}