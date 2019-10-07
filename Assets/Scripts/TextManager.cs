using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;
    public Text moneyText;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;

        CustomStart();
    }

    void CustomStart()
    {
        moneyText.text = "$ " + GameManager.Instance.points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        moneyText.text = "$ " + GameManager.Instance.points.ToString();
    }
}
