using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.PickerWheelUI;

[System.Serializable]
public class PopupItems
{
    public GameObject popupPanel;
    public Text ammountText;
    public Button ok_Button;
}
public class SpinManager : MonoBehaviour
{
    [SerializeField] Text[] aommuntText;
    [SerializeField] PopupItems popupItems;
    [SerializeField] Text spinCount_Text;
    string firstSpinKey = "firstSpinKey";

    [SerializeField] private Button uiSpinButton;
    [SerializeField] private Text uiSpinButtonText;

    [SerializeField] private PickerWheel pickerWheel;
    // Start is called before the first frame update
    void Start()
    {
        #region GIVE SPIN COUNT
        if (!PlayerPrefs.HasKey(firstSpinKey))
        {
            Game.TotalSpinCount = 30;
            PlayerPrefs.SetString(firstSpinKey, firstSpinKey);
        }
        #endregion
        if (popupItems.ok_Button) { popupItems.ok_Button.onClick.AddListener(ClosePopup); }
        UpdateUI();
        ClosePopup();
        AssignSpinButton();
    }
    void AssignSpinButton()
    {
        uiSpinButton.onClick.AddListener(() => {

            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Spinning";

            pickerWheel.OnSpinEnd(wheelPiece => {
                Game.TotalSpinCount--;
                Game.TotalCoins += wheelPiece.Amount;
                ShowPopup(wheelPiece.Amount);

                //Debug.Log(
                //   @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
                //   + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
                //);

                uiSpinButton.interactable = true;
                uiSpinButtonText.text = "SPIN NOW";
            });

            pickerWheel.Spin();

        });
    }
    void ClosePopup()
    {
        popupItems.popupPanel.SetActive(false);
    }
    public void ShowPopup(int ammount)
    {
        popupItems.popupPanel.SetActive(true);
        popupItems.ammountText.text = ammount.ToString();
        UpdateUI();
    }
    void UpdateUI()
    {
        if (aommuntText.Length > 0)
        {
            for (int i = 0; i < aommuntText.Length; i++)
            {
                aommuntText[i].text = Game.TotalCoins.ToString();
            }
        }
        spinCount_Text.text = Game.TotalSpinCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
