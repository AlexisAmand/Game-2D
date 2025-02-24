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
        
        /* On g�n�re un chiffre al�atoire */
        int meteo = Random.Range(0, 3);
        Debug.Log("meteo");
        Debug.Log(meteo);

        switch (meteo)
        {
            case 1:
                if (rainActived)
                {
                    Debug.Log("Il pleut !");
                    /* On affiche le g�n�rateur de pluie, on masque le g�n�rateur de neige. Il pleut. */
                    rainGenerator.SetActive(true);
                    snowGenerator.SetActive(false);
                }
                break;

            case 2:
                if (snowActived)
                {
                    Debug.Log("Il neige !");
                    /* On masque le g�n�rateur de pluie, on affiche le g�n�rateur de neige. il neige. */
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
