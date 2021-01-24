using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private string owner;
    private float _timer;
    private float nextShoot;
    private bool firstShoot = false;
    [SerializeField]
    private float _waitTimer = 12.0f;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float shootTimer = 3.0f;
    private Vector3 _target;
    private Vector3 max;
    [SerializeField]
    private AudioClip _cannon1;

    #region GET & SET
    public string Owner
    {
        get
        {
            return owner;
        }
        set
        {
            owner = value;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        max = new Vector3(1000, 1000);
        _target = max;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _waitTimer)
        {
            Destroy(this.gameObject);
            //Debug.Log("Ta na hora de dar tchau");
            _timer -= _waitTimer;
        }

        if (firstShoot)
        {
            if(Time.time >= nextShoot)
            {
                Fire();
            }
        }
    }
    private void Fire()
    {
        GameObject tmp = GameObject.Instantiate(bullet);
        tmp.transform.position = this.transform.position;
        tmp.GetComponent<Bullet>().Target = _target;
        Manager SoundManager = GameObject.Find("Manager").GetComponent<Manager>();
        SoundManager.GetComponent<SoundManager>().Play(_cannon1);
        //Debug.Log("Tiro");
        nextShoot = Time.time + shootTimer;
        firstShoot = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") && (collision.name != Owner)) 
            && (!collision.gameObject.GetComponent<Player>().IsSafe))
        {
            _target = max;
            if (Vector3.Distance(_target, this.gameObject.transform.position) >
                Vector3.Distance(this.gameObject.transform.position, collision.gameObject.transform.position))
            {
                _target = collision.gameObject.transform.position;
                //Debug.Log("Novo alvo esta em: " + _target + " E é o: " + collision.name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") && (collision.name != Owner))
            && (!collision.gameObject.GetComponent<Player>().IsSafe))
        {
            if(!firstShoot) Invoke("Fire", shootTimer);
            //Debug.Log("Hoho... mukatta kuruno ka?");
        }
    }
}
