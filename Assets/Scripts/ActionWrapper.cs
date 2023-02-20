using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine;

public static class ActionWrapper
{
    public static string Wrap(BoardAction action)
    {
        Dictionary<string, string> wrappedAction = new Dictionary<string, string>();
        wrappedAction["ActionType"] = BoardActionsEnumMethods.ToInt(action).ToString();
        wrappedAction["Info"] = WrapActionInfo(action);
        return JsonConvert.SerializeObject(wrappedAction);
    }

    private static string WrapActionInfo(BoardAction action)
    {
        Dictionary<string, string> wrappedInfo = new Dictionary<string, string>();

        switch (action)
        {
            case BuildBuilding newAction:
            {
                wrappedInfo["BuildingCoord"] =
                    String.Format("({0},{1})", newAction.BuildingCoord.x, newAction.BuildingCoord.y);
                wrappedInfo["BuildingType"] = ((int) newAction.Building).ToString();
                break;
            }
            case OwnTile newAction:
            {
                Debug.Log("TileCoord: (" + newAction.TileCoord.x + ", " + newAction.TileCoord.y + ")");
                wrappedInfo["TileCoord"] =
                    String.Format("({0},{1})", newAction.TileCoord.x, newAction.TileCoord.y);
                break;
            }
            case Shoot newAction:
            {
                wrappedInfo["Source"] =
                    String.Format("({0},{1})", newAction.Source.x, newAction.Source.y);
                wrappedInfo["Destination"] =
                    String.Format("({0},{1})", newAction.Destination.x, newAction.Destination.y);
                break;
            }
            case DestroyBuilding newAction:
            {
                wrappedInfo["TileCoord"] =
                    String.Format("({0},{1})", newAction.BuildingCoord.x, newAction.BuildingCoord.y);
                break;
            }
            case ChangeTurn:
            {
                break;
            }
            case SendMessage newAction:
            {
                wrappedInfo["Message"] = newAction.Message;
                break;
            }
            case RepairBuilding newAction:
            {
                wrappedInfo["BuildingCoord"] =
                    String.Format("({0},{1})", newAction.BuildingCoord.x, newAction.BuildingCoord.y);
                wrappedInfo["RepairAmount"] = newAction.RepairAmount.ToString();
                break;
            }
            case SpecialAction newAction:
            {
                wrappedInfo["ActionCoord"] =
                    String.Format("({0},{1})", newAction.ActionCoord.x, newAction.ActionCoord.y);
                break;
            }
        }

        return JsonConvert.SerializeObject(wrappedInfo);
    }

    public static BoardAction Unwrap(string actionInfo)
    {
        Dictionary<string, string> mainInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(actionInfo);

        if (mainInfo != null && mainInfo.ContainsKey("ActionType") && mainInfo.ContainsKey("Info"))
        {
            BoardActionEnum boardActionEnum =
                BoardActionsEnumMethods.ToEnum(System.Convert.ToInt32(mainInfo["ActionType"]));
            return UnwrapInfo(boardActionEnum, mainInfo["Info"]);
        }

        return null;
    }

    private static BoardAction UnwrapInfo(BoardActionEnum boardActionEnum, string info)
    {
        Dictionary<string, string> unwrappedInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(info);

        if (unwrappedInfo == null) return null;

        switch (boardActionEnum)
        {
            case BoardActionEnum.BuildBuilding:
            {
                return new BuildBuilding(ParsePair(unwrappedInfo["BuildingCoord"]),
                    (BuildingType) System.Convert.ToInt32(unwrappedInfo["BuildingType"]));
            }
            case BoardActionEnum.OwnTile:
            {
                return new OwnTile(ParsePair(unwrappedInfo["TileCoord"]));
            }
            case BoardActionEnum.Shoot:
                return new Shoot(ParsePair(unwrappedInfo["Source"]), ParsePair(unwrappedInfo["Destination"]));
            case BoardActionEnum.DestroyBuilding: return new DestroyBuilding(ParsePair(unwrappedInfo["TileCoord"]));
            case BoardActionEnum.ChangeTurn: return new ChangeTurn();
            case BoardActionEnum.SendMessage: return new SendMessage(unwrappedInfo["Message"]);
            case BoardActionEnum.RepairBuilding:
                return new RepairBuilding(ParsePair(unwrappedInfo["BuildingCoord"]),
                    Int32.Parse(unwrappedInfo["RepairAmount"]));
            case BoardActionEnum.SpecialAction: return new SpecialAction(ParsePair(unwrappedInfo["ActionCoord"]));
        }

        return null;
    }

    private static Vector2Int ParsePair(string pair)
    {
        pair = pair.Replace("(", "");
        pair = pair.Replace(")", "");
        string[] numbers = pair.Split(",");
        return new Vector2Int(System.Convert.ToInt32(numbers[0]), System.Convert.ToInt32(numbers[1]));
    }
}