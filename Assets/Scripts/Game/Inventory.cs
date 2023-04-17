using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    /* Ajout d'un singleton pour avoir une seule classe Inventory dans le jeu */

    public int coinsCount;
    public Text coinsCountText;

    public int keysCount;
    public Text keysCountText;

    public int arrowsCount;
    public Text arrowsCountText;

    public static Inventory Instance;

    /* La fonction Awake est lue avant tout le monde, avant m�me la fonction start */
    /* on va pouvoir acc�der � l'inventory depuis n'importe o� */

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la sc�ne");
            return;
        }

        Instance = this;
    }

    /* ajout d'une piece dans l'inventaire */

    public void AddCoins(int count)
    {
        coinsCount += count;
        UpdateTextUI();
    }

    /* suppression d'une piece de l'inventaire */

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        UpdateTextUI();
    }

    /* ajout d'une cl� dans l'inventaire */

    public void AddKeys(int count)
    {        
        keysCount += count;
        keysCountText.text = keysCount.ToString();
    }

    /* suppression d'une cl� de l'inventaire */

    public void RemoveKeys(int count)
    {
        keysCount -= count;
        keysCountText.text = keysCount.ToString();
    }

    /* ajout d'une fl�che dans l'inventaire */

    public void AddArrows(int count)
    {
        arrowsCount += count;
        arrowsCountText.text = arrowsCount.ToString();
    }

    /* suppression d'une fl�che de l'inventaire */

    public void RemoveArrows(int count)
    {
        arrowsCount -= count;
        arrowsCountText.text = arrowsCount.ToString();
    }

    public void UpdateTextUI()
    {
        coinsCountText.text = coinsCount.ToString();
    }

}
