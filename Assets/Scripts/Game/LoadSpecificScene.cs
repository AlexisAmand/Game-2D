using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSpecificScene : MonoBehaviour  
{

    public string sceneName;
    private Animator fadeSystem;

    /* Gestion de l'ouverture de la porte */
    public Sprite sprite1; // porte ferm�e
    public Sprite sprite2; // porte ouverte
    public bool DoorClosed = true; // Elle est ferm�e ou ouverte ? Par d�faut elle est ferm�e.
    private SpriteRenderer spriteRenderer; /* On r�cup�re le spriterenderer pour pouvoir modifier le sprite tout � l'heure */

    public int CoinsGoal; /* Nombre de coins � trouver dans le niveau. Valeur � completer dans Unity */

    /* Son qui indique qu'on change de niveau */
    // public AudioClip levelEnd;
    /* Son qui indique que la porte s'ouvre */
    // public AudioClip doorOpen;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Acc�s au SpriteRenderer de l'objet

        /* Au chargement du niveau, on regarde si le nombre de piece est ok */
        if (Inventory.Instance.coinsCount >= CoinsGoal) 
        {
            openDoor();
        }
        
    }

    void Update()
    {
        /* Si toutes les pieces sont ramass�es en cour de partie */
        if (Inventory.Instance.coinsCount == CoinsGoal) 
        {
            openDoor();
        }
    }

    void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void openDoor()
    {
        spriteRenderer.sprite = sprite2; /* On remplace le sprite */
        DoorClosed = false; /* La porte est maintenant ouverte */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if ((collision.CompareTag("Player")) && (DoorClosed == false))
        {
            /* la porte est ouverte ! Le joueur peut passer */
            Debug.Log("C'est ok pour la porte");
            StartCoroutine(loadNextScene());
        }      
    }

    public IEnumerator loadNextScene()
    {
        /* On joue le son du changement de sc�ne */
        // AudioManager.Instance.PlayClipAt(levelEnd, transform.position);

        /* sauvegarde des donn�es au passage de la porte de changement du niveau */
        LoadAndSaveData.Instance.SaveData();

        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

}
