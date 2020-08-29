using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBattle : MonoBehaviour
{

    private bool isFighting = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFighting)
            return;
    }

    public void OnClick()
    {
        if (isFighting)
            return;

        isFighting = true;
        GetComponent<Button>().interactable = false;

    }
}
