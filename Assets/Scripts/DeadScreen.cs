using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreen : MonoBehaviour
{
    public void Setup() {
        gameObject.SetActive(true);
    }

    public void RestartButton() {
        gameObject.SetActive(false);
        Application.LoadLevel(Application.loadedLevel);
    }
}
