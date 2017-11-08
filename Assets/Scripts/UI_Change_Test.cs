using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Change_Test : MonoBehaviour {

   // public Button button;
    public GameObject canvasToOpen;
    public GameObject canvasToClose;


   /* void start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(ChangeUI); 
    }*/

    public void ChangeUI ()
    {
        canvasToClose.SetActive(false);
        canvasToOpen.SetActive(true);
    }
  
}
