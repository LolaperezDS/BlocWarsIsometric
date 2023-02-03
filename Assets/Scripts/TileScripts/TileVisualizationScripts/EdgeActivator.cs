using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeActivator : MonoBehaviour
{
    [SerializeField] private GameObject positiveXEdge;
    [SerializeField] private GameObject negativeXEdge;
    [SerializeField] private GameObject positiveYEdge;
    [SerializeField] private GameObject negativeYEdge;

    [SerializeField] private Material redColor;
    [SerializeField] private Material blueColor;
    [SerializeField] private Material greenColor;
    [SerializeField] private Material yellowColor;
    [SerializeField] private Material emptyColor;

    private AbstractTile positiveXTileAbstract;
    private AbstractTile negativeXTileAbstract;
    private AbstractTile positiveYTileAbstract;
    private AbstractTile negativeYTileAbstract;

    private AbstractTile abstractTile;
    private PlayerInstance lastPlayerAttachment;

    private void Start()
    {
        abstractTile = GetComponent<AbstractTile>();
        lastPlayerAttachment = PlayerInstance.Empty;

        positiveXTileAbstract = TileManager.TileExist(abstractTile.Id + Vector2Int.right) ? TileManager.GetTileFromId(abstractTile.Id + Vector2Int.right) : null;
        negativeXTileAbstract = TileManager.TileExist(abstractTile.Id + Vector2Int.left) ? TileManager.GetTileFromId(abstractTile.Id + Vector2Int.left) : null;
        positiveYTileAbstract = TileManager.TileExist(abstractTile.Id + Vector2Int.up) ? TileManager.GetTileFromId(abstractTile.Id + Vector2Int.up) : null;
        negativeYTileAbstract = TileManager.TileExist(abstractTile.Id + Vector2Int.down) ? TileManager.GetTileFromId(abstractTile.Id + Vector2Int.down) : null;
    }

    // Update is called once per frame
    private void Update()
    {
        if (lastPlayerAttachment != abstractTile.PlayerInstance)
        {
            UpdateEdges();
            if (positiveXTileAbstract != null) positiveXTileAbstract.gameObject.GetComponent<EdgeActivator>().UpdateEdges();
            if (negativeXTileAbstract != null) negativeXTileAbstract.gameObject.GetComponent<EdgeActivator>().UpdateEdges();
            if (positiveYTileAbstract != null) positiveYTileAbstract.gameObject.GetComponent<EdgeActivator>().UpdateEdges();
            if (negativeYTileAbstract != null) negativeYTileAbstract.gameObject.GetComponent<EdgeActivator>().UpdateEdges();
        }
    }

    private void UpdateEdges()
    {
        if (((positiveXTileAbstract != null && positiveXTileAbstract.PlayerInstance != abstractTile.PlayerInstance) || positiveXTileAbstract == null) && abstractTile.PlayerInstance != PlayerInstance.Empty)
        {
            positiveXEdge.SetActive(true);
        }
        else positiveXEdge.SetActive(false);
        if (((negativeXTileAbstract != null && negativeXTileAbstract.PlayerInstance != abstractTile.PlayerInstance) || negativeXTileAbstract == null) && abstractTile.PlayerInstance != PlayerInstance.Empty)
        {
            negativeXEdge.SetActive(true);
        }
        else negativeXEdge.SetActive(false);
        if (((positiveYTileAbstract != null && positiveYTileAbstract.PlayerInstance != abstractTile.PlayerInstance) || positiveYTileAbstract == null) && abstractTile.PlayerInstance != PlayerInstance.Empty)
        {
            positiveYEdge.SetActive(true);
        }
        else positiveYEdge.SetActive(false);
        if (((negativeYTileAbstract != null && negativeYTileAbstract.PlayerInstance != abstractTile.PlayerInstance) || negativeYTileAbstract == null) && abstractTile.PlayerInstance != PlayerInstance.Empty)
        {
            negativeYEdge.SetActive(true);
        }
        else negativeYEdge.SetActive(false);
        CheckColor();
    }

    private void CheckColor()
    {
        if (abstractTile.PlayerInstance != PlayerInstance.Empty)
        {
            switch (abstractTile.PlayerInstance)
            {
                case PlayerInstance.Blue:
                    ChangeColorAllEdges(blueColor);
                    break;
                case PlayerInstance.Red:
                    ChangeColorAllEdges(redColor);
                    break;
                case PlayerInstance.Green:
                    ChangeColorAllEdges(greenColor);
                    break;
                case PlayerInstance.Yellow:
                    ChangeColorAllEdges(yellowColor);
                    break;
                case PlayerInstance.Empty:
                    ChangeColorAllEdges(emptyColor);
                    break;
                default:
                    throw new System.Exception("Wrong Attachment");
            }
        }
    }

    private void ChangeColorAllEdges(Material mat)
    {
        positiveXEdge.GetComponent<Renderer>().material = mat;
        negativeXEdge.GetComponent<Renderer>().material = mat;
        positiveYEdge.GetComponent<Renderer>().material = mat;
        negativeYEdge.GetComponent<Renderer>().material = mat;
    }
}
