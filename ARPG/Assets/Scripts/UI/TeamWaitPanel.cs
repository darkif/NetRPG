using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamWaitPanel : MonoBehaviour {
    private Button cancelBtn;

    public static TeamWaitPanel _instance;

    private void Awake()
    {
        _instance = this;

        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        cancelBtn.onClick.AddListener(OnCancelButtonClick);

        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        transform.DOScale(1, 0.4f);
    }

    void OnCancelButtonClick()
    {
        Tweener tweener = transform.DOScale(0, 0.4f);
        tweener.OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        OnlinePanel._instance.ShowPanel();
    }

}
