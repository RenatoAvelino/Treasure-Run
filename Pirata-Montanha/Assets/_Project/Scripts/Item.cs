using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int Type;
    private string[] itemName = {"Boot", "Shovel", "Cannon"};
    [SerializeField]
    private float bootMultiplier = 1.25f;
    [SerializeField]
    private GameObject Hole;
    [SerializeField]
    private GameObject Cannon;
    [SerializeField]
    private AudioClip _shovel1;
    [SerializeField]
    private Sprite none, shovel, boot, cannon;

    public int Index
    {
        get
        {
            return Type;
        }
        set
        {
            Type = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        //Debug.Log("Aqui estou");
        switch (Type)
        {
            case -1: // No Item
                gameObject.GetComponent<Player>().UpdateItem(none);
                break;
            case 0: //Boot
                gameObject.GetComponent<Player>().UpdateItem(boot);
                if (!gameObject.GetComponent<Player>().IsSafe && !gameObject.GetComponent<Player>().IsChest)
                {
                    gameObject.GetComponent<Player>().Speed *= bootMultiplier;
                    this.Index = -1;
                    gameObject.GetComponent<Player>().UpdateItem(none);
                }
                break;
            case 1: //Shovel
                //Creates a Sprite with a colisor
                gameObject.GetComponent<Player>().UpdateItem(shovel);
                if (!gameObject.GetComponent<Player>().IsSafe && !gameObject.GetComponent<Player>().IsChest)
                {
                    CreateHole();
                    Manager SoundManager = GameObject.Find("Manager").GetComponent<Manager>();
                    SoundManager.GetComponent<SoundManager>().Play(_shovel1);
                    this.Index = -1;
                    gameObject.GetComponent<Player>().UpdateItem(none);
                }
                //Debug.Log("Buracando");
                break;
            case 2: // Cannon
                gameObject.GetComponent<Player>().UpdateItem(cannon);
                if (gameObject.GetComponent<Player>().IsSafe && !gameObject.GetComponent<Player>().IsChest)
                {
                    CreateCannon();
                    this.Index = -1;
                    gameObject.GetComponent<Player>().UpdateItem(none);
                }
               break;
            default:
                Debug.LogError("Item inexistente");
               break;
        }
    }

    public void CreateHole()
    {
        GameObject tmp = GameObject.Instantiate(Hole);
        Vector3 position = this.gameObject.transform.position;
        tmp.transform.position = new Vector3(position.x, position.y - 0.5f, position.z);
    }

    public void CreateCannon()
    {
        GameObject tmp = GameObject.Instantiate(Cannon);
        tmp.GetComponent<Cannon>().Owner = this.gameObject.name;
        //Debug.Log(tmp.GetComponent<Cannon>().Owner);
        tmp.transform.position = this.gameObject.transform.position;
    }

    public string GetName()
    {
        return itemName[Index];
    }
}
