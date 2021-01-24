using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    #region VARIABLES
    [SerializeField]
    private GameObject _rock;
    [SerializeField]
    private float ratio = 50.0f;
    [SerializeField]
    private float _maxWeight = 5.0f;
    [SerializeField]
    private float _waitTimer = 2.0f;
    [SerializeField]
    private float rockTimer = 5.0f;
    private float _timer;
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
        if(_timer > _waitTimer)
        {
            float action = Random.Range(1.0f, 100.0f);
            if(action <= ratio)
            {
                Spawn();
            }
            _timer -= _waitTimer;
        }
    }

   public void Spawn()
    {
        //float weight = Random.Range(1.0f, 10.0f);
        GameObject rock = GameObject.Instantiate(_rock);
        rock.transform.position = this.gameObject.transform.position;
        rock.GetComponent<Rock>().Timer = rockTimer;
    }
}
