using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KoperChatController : MonoBehaviour
{
    public TMP_Text text;

    public void ActivateText()
    {
        text.gameObject.SetActive(true);
    }
    public void IncreasePage()
    {
        text.pageToDisplay++;
    }
}
