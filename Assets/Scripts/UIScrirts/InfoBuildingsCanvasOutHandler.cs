using UnityEngine;
using UnityEngine.UI;

public class InfoBuildingsCanvasOutHandler : MonoBehaviour
{
    [SerializeField] private GameObject typeTxt;
    [SerializeField] private GameObject idTxt;
    [SerializeField] private GameObject healthTxt;
    [SerializeField] private GameObject goldTxt;
    [SerializeField] private GameObject actionsTxt;

    public void Display(GameObject obj)
    {
        AbstractBuilding buildingAbstract = obj.GetComponent<AbstractBuilding>();
        typeTxt.GetComponent<Text>().text = buildingAbstract.PlayerInstance.ToString();
        idTxt.GetComponent<Text>().text = buildingAbstract.Id.ToString();
        healthTxt.GetComponent<Text>().text = buildingAbstract.Health.ToString();
        ProduceValue produceValue = buildingAbstract.Produce();
        goldTxt.GetComponent<Text>().text = produceValue.gold.ToString();
        actionsTxt.GetComponent<Text>().text = produceValue.actions.ToString();
    }
}