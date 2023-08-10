using System.Net.Security;
using UnityEngine;

public class MeteoRandom : MonoBehaviour
{

    public GameObject rainGenerator;
    public GameObject snowGenerator;

    public bool rainActived = true;
    public bool snowActived = true;

    void Start()
    {
        
        /* On génére un chiffre aléatoire */
        int meteo = Random.Range(0, 3);
        Debug.Log("meteo");
        Debug.Log(meteo);

        switch (meteo)
        {
            case 1:
                if (rainActived)
                {
                    Debug.Log("Il pleut !");
                    /* On affiche le générateur de pluie, on masque le générateur de neige. Il pleut. */
                    rainGenerator.SetActive(true);
                    snowGenerator.SetActive(false);
                }
                break;

            case 2:
                if (snowActived)
                {
                    Debug.Log("Il neige !");
                    /* On masque le générateur de pluie, on affiche le générateur de neige. il neige. */
                    rainGenerator.SetActive(false);
                    snowGenerator.SetActive(true);
                }
                break;

            default:
                Debug.Log("il fait beau");
                rainGenerator.SetActive(false);
                snowGenerator.SetActive(false);
                break;
        }

    }

}
