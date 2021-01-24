using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private GameObject[] Players;
    private int numPlayers = 2;
    [SerializeField]
    private AudioClip _music;

    #region GET & SET
    public int NumPlayers
    {
        get
        {
            return numPlayers;
        }
        set
        {
            numPlayers = value;
        }
    }
    #endregion
    private void Awake()
    {
        CreatePlayers();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SoundManager>().PlayMusic(_music);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private GameObject[] CreatePlayers()
    {
        GameObject[] Players = new GameObject[NumPlayers];
        Rect[] Viewports = InitiateViewports();
        Vector3 refPoint = this.gameObject.transform.position;

        for (int i = 0; i < NumPlayers; i++)
        {
            GameObject temp = GameObject.Instantiate(Player);
            temp.layer = 8;
            temp.transform.position = new Vector3(refPoint.x + i, refPoint.y, refPoint.z);
            temp.name = "Player" + (i + 1);
            temp.GetComponent<Player>().Spawn = this.gameObject.transform.position;
            temp.GetComponent<Player>().AxisX = "Horizontal" + (i + 1);
            temp.GetComponent<Player>().AxisY = "Vertical" + (i + 1);
            temp.GetComponent<Player>().UseKey = InitUse(i);
            temp.GetComponent<Player>().ClimbKey = InitClimb(i);
            temp.GetComponent<Player>().Health = 3;
            GameObject newEmpty = new GameObject();
            Camera newCam = newEmpty.AddComponent<Camera>();
            newEmpty.transform.parent = temp.transform;
            newCam.transform.position = new Vector3(temp.transform.position.x
                , temp.transform.position.y, -9.0f);
            newCam.orthographic = true;
            if (Viewports != null) newCam.rect = Viewports[i];
            Players[i] = temp;
        }

        return Players;
    }

    private KeyCode InitUse(int num)
    {
        KeyCode tmp = KeyCode.Escape;
        switch (num)
        {
            case 0:
                tmp = KeyCode.E;
                break;
            case 1:
                tmp = KeyCode.I;
                break;
            case 2:
                tmp = KeyCode.Joystick1Button1;
                break;
            default:
                Debug.LogError("Posição inexistente");
                break;
        }
        return tmp;
    }

    private KeyCode InitClimb(int num)
    {
        KeyCode tmp = KeyCode.Escape;
        switch (num)
        {
            case 0:
                tmp = KeyCode.Space;
                break;
            case 1:
                tmp = KeyCode.U;
                break;
            case 2:
                tmp = KeyCode.Joystick1Button0;
                break;
            default:
                Debug.LogError("Posição inexistente");
                break;
        }
        return tmp;
    }

    private Rect[] InitiateViewports()
    {
        Rect[] vector = new Rect[NumPlayers];
        switch (NumPlayers)
        {
            case 1:
                vector = null;
                break;
            case 2:
                vector[0] = new Rect(0,0,0.5f, 1);
                vector[1] = new Rect(0.5f, 0, 0.5f, 1);
                break;
            case 3:
                vector[2] = new Rect(0, 0.5f, 1, 0.5f);
                vector[0] = new Rect(0, 0, 0.5f, 0.5f);
                vector[1] = new Rect(0.5f, 0, 1, 0.5f);
                break;
            default:
                Debug.LogError("Numero inexistente");
                break;
        }
        return vector;
    }
}
