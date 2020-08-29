using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartSkirmish : MonoBehaviour
{
    public GameMaster GameMaster;

    void Start()
    {
        GameMaster.InteractionStateChange += InteractionStateChange;
    }

    private void InteractionStateChange()
    {
        GetComponent<Button>().interactable = GameMaster.isGameInteractable;
    }

    void Update()
    {
    }

    public void OnClick()
    {
        if (!GameMaster.isGameInteractable)
            return;

        _ = GameMaster.StartSkirmish();
    }
}
