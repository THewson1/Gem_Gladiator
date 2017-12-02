using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChange : MonoBehaviour {

   // public Button button;
    public GameObject canvasToOpen;
    public GameObject canvasToClose;

    public void ChangeUI ()
    {
        canvasToClose.SetActive(false);
        canvasToOpen.SetActive(true);
    }
  
}
