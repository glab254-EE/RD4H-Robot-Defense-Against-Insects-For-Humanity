using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TowerSelectOptionBehaivor : MonoBehaviour
{
    [field: SerializeField]
    private TMP_Text towerNameScreen;
    protected internal TowerDataSO selectedTower;
    protected internal GameObject towerBaseModule;
    protected internal GameObject parentUI;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(new(TriggerBuyTower));
        towerNameScreen.text = selectedTower.TowerName;
        TMP_Text textLabelInsideButton = button.GetComponentInChildren<TMP_Text>();
        if (textLabelInsideButton != null)
        {
            textLabelInsideButton.text = "Buy for " + selectedTower.BaseCost.ToString() + " Rs";
        }
    }
    void TriggerBuyTower()
    {
        if (towerBaseModule == null) Destroy(gameObject);
        if (!TowersManager.instance.RequestToBuyTower(towerBaseModule, selectedTower))
        {
            Debug.Log("Failed to buy.");
        }
        else
        {
            Destroy(parentUI);
        }
    }
}
