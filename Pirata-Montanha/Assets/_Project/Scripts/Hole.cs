using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField]
    private float _waitTimer = 1.0f;
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _waitTimer)
        {
            Destroy(this.gameObject);
            //Debug.Log("Ta na hora de dar tchau");
            _timer -= _waitTimer;
        }
    }
}
