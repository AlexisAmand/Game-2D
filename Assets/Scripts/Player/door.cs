using UnityEngine.UI;
using UnityEngine;

public class door : MonoBehaviour
{
    public GameObject doorGameObject;
    private Text doorUI;
    private bool playerInRange;

    void Awake()
    {
        doorUI = GameObject.FindGameObjectWithTag("DoorUI").GetComponent<Text>();
    }

    void Start()
    {
        doorUI.enabled = false;
       // playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (Inventory.Instance.keysCount > 0)
            {
                // Player has enough keys, hide UI and destroy door object
                doorUI.enabled = false;
                doorGameObject.SetActive(false);
                Inventory.Instance.RemoveKeys(1);
            }
            else
            {
                // Player does not have enough keys, show UI
                doorUI.enabled = true;
               // playerInRange = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player is no longer in range, hide UI
            doorUI.enabled = false;
           // playerInRange = false;
        }
    }

}
