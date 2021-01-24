using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Creator : MonoBehaviour
{
    [SerializeField]
    private int numPlayer;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tmp = GameObject.Find("Player" + numPlayer);
        if(tmp != null)
        {
            tmp.GetComponent<Player>().HUDItem = this.gameObject;
        }
        else
        {
            this.GetComponent<Image>().enabled = false;
        }
        //Debug.Log("Achei o: " + tmp.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
