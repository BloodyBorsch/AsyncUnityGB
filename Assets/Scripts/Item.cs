using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    GameObject prefabPopup;
    CancellationTokenSource cts = new CancellationTokenSource();

    private GameObject _popup;

    public void OnDestroy()
    {
        cts.Dispose();
    }

    public void TryBuyItem()
    {
        _popup = Instantiate(prefabPopup, transform.parent);
        SomePopup popupScript = _popup.GetComponent<SomePopup>();
        popupScript.OnClose+=CompletePurhase;

        
        CancellationToken ct = cts.Token;
        popupScript.ActivatePopup(ct);
    }

    void CompletePurhase(bool completed)
    {
        
        if (completed) Debug.Log("Покупка совершена!");
        else Debug.Log("Покупка отменена!");
        _popup.GetComponent<SomePopup>().OnClose -= CompletePurhase;
        Destroy(_popup);
    }
}
