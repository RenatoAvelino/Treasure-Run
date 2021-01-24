using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 5.0f;
    [SerializeField]
    private float _waitTimer = 5.0f;
    private float _timer;
    private Vector3 target;
    private Vector3 direction;

    public Vector3 Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        direction = target - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        Vector3 move = direction * Time.deltaTime * bulletSpeed;

        transform.Translate(move);
        /*float step = bulletSpeed * Time.deltaTime;
        if (Vector3.Distance(Target, this.transform.position) == 0)
        {
            
        }
        else { 
            this.transform.position = Vector3.MoveTowards(this.transform.position,
            Target, step); 
        }*/
        if (_timer > _waitTimer)
        {
            Destroy(this.gameObject);
            //Debug.Log("Ta na hora de dar tchau");
            _timer -= _waitTimer;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(2);
            Destroy(this.gameObject);
        }
    }
}
