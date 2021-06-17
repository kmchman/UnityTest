using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestItem : MonoBehaviour
{
    [SerializeField] private Text descText;

    public void SetData(string data)
    {
        descText.text = data;
    }
}
