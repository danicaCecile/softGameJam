using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public void DeactivateScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

    public void ActivateScreen(GameObject screen)
    {
        screen.SetActive(true);
    }
}
