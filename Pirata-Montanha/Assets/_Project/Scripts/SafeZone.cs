using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject refPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entrou na SZ");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Health = 3;
            collision.GetComponent<Player>().Spawn = refPos.gameObject.transform.position;
            //Debug.Log(collision.name + " Entrou na SZ");
            collision.GetComponent<Player>().IsSafe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Saiu na SZ");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().IsSafe = false;
            //Debug.Log(collision.name + " Saiu na SZ");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().IsSafe = true;
            //Debug.Log(collision.name + " Esta na SZ");
        }
    }
}
