using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SOIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TMP_Text uiTextValue;

    private void Start()
    {
        uiTextValue.text = soInt.value.ToString();
    }

    private void Update()
    {
        uiTextValue.text = soInt.value.ToString();
    }
}
