using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    #region VARIABLES
    private float gravity = 2.0f;
    [SerializeField]
    private float mass = 6.0f;
    [SerializeField]
    private float _waitTimer = 10.0f;
    private float _timer;
    [SerializeField]
    private AudioClip _rock1;
    #endregion

    #region GET & SET
    public float Mass
    {
        get
        {
            return mass;
        }
        set
        {
            mass = value;
        }
    }
    public float Timer
    {
        get
        {
            return _waitTimer;
        }
        set
        {
            _waitTimer = value;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        Vector3 move = new Vector3(0, mass * gravity * (-1), 0);

        move *= Time.deltaTime;

        transform.Translate(move);

        if (_timer > _waitTimer)
        {
            DestroyWithSound();
            //Debug.Log("Ta na hora de dar tchau");
            _timer -= _waitTimer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(1);
            DestroyWithSound();
        }
        else if (collision.gameObject.CompareTag("SafeZone")){
            //Debug.Log("Entrou na safe zone");
            DestroyWithSound();
        }
    }

    private void DestroyWithSound()
    {
        Manager SoundManager = GameObject.Find("Manager").GetComponent<Manager>();
        SoundManager.GetComponent<SoundManager>().Play(_rock1);
        Destroy(this.gameObject);
    }
}
