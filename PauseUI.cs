using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject menuButton;
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.instance.Stop);
        resumeButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.Resume);
        menuButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.goMenu);
    }


}
