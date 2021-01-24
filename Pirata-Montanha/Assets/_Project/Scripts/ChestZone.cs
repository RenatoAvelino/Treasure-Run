using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestZone : MonoBehaviour
{
    private bool isClosed = false;
    [SerializeField]
    private float waitTimer = 5.0f;
    [SerializeField]
    private Sprite opened, closed;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = closed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().IsChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().IsChest = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Esta na CZ");
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(collision.GetComponent<Player>().UseKey))
        {
            if (!isClosed)
            {
                collision.GetComponent<Item>().Index = Random.Range(0,3);
                //Debug.Log(collision.name + " Pegou o Item: " + collision.GetComponent<Item>().GetName());
                isClosed = true;
                this.gameObject.GetComponent<SpriteRenderer>().sprite = opened;
                Invoke("Open", waitTimer);
            }
        }
    }

    private void Open()
    {
        isClosed = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = closed;
    }
}
