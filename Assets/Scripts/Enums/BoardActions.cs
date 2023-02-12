using UnityEngine;

public abstract class BoardAction
{
}

public class ChosenBuilding : BoardAction
{
    private readonly Vector2Int _buildingCoords;
    public Vector2Int BuildingCoord { get; }

    public ChosenBuilding(Vector2Int coords)
    {
        _buildingCoords = coords;
    }
}

public class BuildBuilding : BoardAction
{
    private readonly Vector2Int _buildingCoords;
    public Vector2Int BuildingCoord { get; }

    private readonly BuildingType _buildingType;
    public BuildingType Building { get; }

    public BuildBuilding(Vector2Int coords, BuildingType buildingType)
    {
        _buildingCoords = coords;
        _buildingType = buildingType;
    }
}

public class OwnTile : BoardAction
{
    private readonly Vector2Int _tileCoords;
    public Vector2Int TileCoord { get; }

    public OwnTile(Vector2Int coords)
    {
        _tileCoords = coords;
    }
}

public class Shoot : BoardAction
{
    private readonly Vector2Int _source;
    public Vector2Int Source { get; }

    private readonly Vector2Int _destination;
    public Vector2Int Destination { get; }

    private readonly int _amountOfDamage;
    public int AmountOfDamage { get; }

    public Shoot(Vector2Int sourceCoords, Vector2Int destinationCoords, int amountOfDamage)
    {
        _source = sourceCoords;
        _destination = destinationCoords;
        _amountOfDamage = amountOfDamage;
    }
}

public class DestroyBuilding : BoardAction
{
    private readonly Vector2Int _buildingCoords;

    public Vector2Int BuildingCoord { get; } // Нужно ли хранить какой игрок это запросил?
    // или просто будет сравниваться с текущим игроком

    public DestroyBuilding(Vector2Int coords)
    {
        _buildingCoords = coords;
    }
}

public class ChangeTurn : BoardAction
{
}

public class SendMessage : BoardAction
{
    private readonly string _message;
    public string Message { get; }

    public SendMessage(string message)
    {
        _message = message;
    }
}

public class RepairBuilding : BoardAction
{
    private readonly Vector2Int _buildingCoord;
    public Vector2Int BuildingCoord { get; }

    private readonly int _repairAmount;
    public int RepairAmount { get; }

    public RepairBuilding(Vector2Int buildingCoord, int repairAmount)
    {
        _buildingCoord = buildingCoord;
        _repairAmount = repairAmount;
    }
}