using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    public ButtonType btnType = ButtonType.normalAtk;

    private Image mask;
    public float coldTime = 3;
    private float coldTimer = 0;

    private PlayerAttack playerAtk;
    private Button btn;

	// Use this for initialization
	void Start () {
        playerAtk = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnAtkButtonClick);
        mask = transform.Find("mask").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (mask != null)
        {
            if (coldTimer > 0)
            {
                coldTimer -= Time.deltaTime;
                mask.fillAmount = coldTimer / coldTime;
            }
            else
            {
                mask.fillAmount = 0;
                btn.enabled = true;
            }
        }
	}

    private void OnAtkButtonClick()
    {
        playerAtk.OnAttackClick(btnType);
        coldTimer = coldTime;
        if (coldTime > 0)
        {
            btn.enabled = false;
        }
    }

}

public enum ButtonType
{
    normalAtk,
    skill1=1,
    skill2=2
}
