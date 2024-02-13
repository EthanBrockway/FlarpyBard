using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ToggleScript : MonoBehaviour
{
    public TMP_InputField passwordInput;
    public UnityEngine.UI.Toggle hideToggle;
    public GameObject background;
    public UnityEngine.UI.Image backgroundImage;

    private void Start()
    {
    backgroundImage = background.GetComponent<Image>();
    }

    public void TogglePasswordHide ()
    {
    
        if (hideToggle.isOn)
        {
          passwordInput.contentType = TMP_InputField.ContentType.Standard;
          passwordInput.ForceLabelUpdate();
          backgroundImage.color = Color.green;
           
        }
        else
        {
          backgroundImage.color = Color.white;
          passwordInput.contentType = TMP_InputField.ContentType.Password; 
          passwordInput.ForceLabelUpdate();
        }
    }



}
