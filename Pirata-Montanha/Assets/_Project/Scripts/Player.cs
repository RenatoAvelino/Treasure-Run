using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region VARIABLES
    private string axisX, axisY;
    [SerializeField]
    private Vector2 speed = new Vector2(5, 5);
    private Vector2 refSpeed;
    private Vector3 originalPoint;
    private Vector3 respawnPoint;
    [SerializeField]
    private float stunTime = 1.0f;
    private int health = 3;
    private bool isSafe = true;
    private bool leftStep = false;
    private bool isChest = false;
    private bool isAlive = true;
    private bool isStun = false;
    private KeyCode useItem;
    private KeyCode climb;
    [SerializeField]
    private AudioClip _damageSound;
    [SerializeField]
    private GameObject GUIanchor;
    [SerializeField]
    private Sprite none;
    [SerializeField]
    private Sprite front, back, left, right, climbing, climbing2;
    #endregion

    #region GET & SET
    public string AxisX
    {
        get
        {
            return axisX;
        }
        set
        {
            axisX = value;
        }
    }
    public string AxisY
    {
        get
        {
            return axisY;
        }
        set
        {
            axisY = value;
        }
    }
    public KeyCode UseKey
    {
        get
        {
            return useItem;
        }
        set
        {
            useItem = value;
        }
    }
    public KeyCode ClimbKey
    {
        get
        {
            return climb;
        }
        set
        {
            climb = value;
        }
    }
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public Vector3 Spawn
    {
        get
        {
            return respawnPoint;
        }
        set
        {
            respawnPoint = value;
        }
    }
    public Vector2 Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    public bool IsSafe
    {
        get
        {
            return isSafe;
        }
        set
        {
            isSafe = value;
        }
    }
    public bool IsChest
    {
        get
        {
            return isChest;
        }
        set
        {
            isChest = value;
        }
    }
    public GameObject HUDItem
    {
        get
        {
            return GUIanchor;
        }
        set
        {
            GUIanchor = value;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        refSpeed = speed;
        originalPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStun)
        {
            return;
        }
        if (isAlive)
        {
            Move();
            if (Input.GetKeyDown(ClimbKey))
            {
                //Debug.Log("Climbando");
            }
            if (Input.GetKeyDown(UseKey))
            {
                //Debug.Log("Usando");
                if(!(this.gameObject.GetComponent<Item>().Index == -1))
                {
                    this.gameObject.GetComponent<Item>().Use();
                    Invoke("UnStun", 2.0f);
                }
                else
                {

                }
            }
        }
        else
        {
            Respawn();
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis(AxisX);
        float inputY = Input.GetAxis(AxisY);

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        if (this.gameObject.layer == 8)
        {
            if (Mathf.Abs(inputX) > Mathf.Abs(inputY))
            {
                if (inputX > 0)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = right;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = left;
                }
            }
            else
            {
                if (inputY > 0)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = back;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = front;
                }
            }
        }
        else
        {
            if (Mathf.Abs(inputX) > Mathf.Abs(inputY))
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = climbing;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = climbing2;
            }
        }

        movement *= Time.deltaTime;

        transform.Translate(movement);
    }

    public void Damage(int damage)
    {
        if (isAlive)
        {
            health -= damage;
            Manager SoundManager = GameObject.Find("Manager").GetComponent<Manager>();
            SoundManager.GetComponent<SoundManager>().Play2(_damageSound);
            speed = new Vector2(0, 0);
            if (health <= 0)
            {
                isAlive = false;
                //Debug.Log("Voce morreu");
                return;
            }
            isStun = true;
            speed = new Vector2(0,0);
            Invoke("UnStun", stunTime);

        }
    }

    private void Respawn()
    {
        Health = 3;
        isAlive = true;
        this.gameObject.layer = 8;
        this.gameObject.transform.position = respawnPoint;
        speed = refSpeed;
    }

    private void UnStun()
    {
        isStun = false;
        speed = refSpeed;
    }

    public void UpdateItem(Sprite image)
    {
        GUIanchor.GetComponent<Image>().sprite = image;
    }
}
