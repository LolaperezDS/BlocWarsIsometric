using UnityEngine;
using UnityEngine.UI;

public class InfoTileCanvasOutHandler : MonoBehaviour
{
    [SerializeField] private GameObject typeTxt;
    [SerializeField] private GameObject idTxt;
    [SerializeField] private GameObject attachTxt;
    [SerializeField] private GameObject image;

    public void Display(GameObject obj)
    {
        AbstractTile tileAbstract = obj.GetComponent<AbstractTile>();
        typeTxt.GetComponent<Text>().text = tileAbstract.Type.ToString();
        idTxt.GetComponent<Text>().text = "ID: " + tileAbstract.Id.ToString();
        attachTxt.GetComponent<Text>().text = "Command " + tileAbstract.PlayerInstance.ToString();
        image.GetComponent<Image>().sprite = obj.GetComponent<SpriteRenderer>().sprite;
    }
}
