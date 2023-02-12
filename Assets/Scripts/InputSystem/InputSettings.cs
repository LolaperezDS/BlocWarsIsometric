using UnityEngine;

public static class InputSettings
{
    private static KeyCode repare = KeyCode.R;
    private static KeyCode attachTile = KeyCode.X;
    private static KeyCode buildTown = KeyCode.Alpha1;
    private static KeyCode buildMine = KeyCode.Alpha2;
    private static KeyCode buildCannon = KeyCode.Alpha3;
    private static KeyCode buildMortar = KeyCode.Alpha4;
    private static KeyCode buildDefender = KeyCode.Alpha5;
    private static KeyCode destroyBuilding = KeyCode.K;
    private static KeyCode chat = KeyCode.M;
    private static KeyCode specialAction = KeyCode.W;
    private static KeyCode fire = KeyCode.F;
    private static KeyCode swapTurn = KeyCode.T;

    public static bool Repare => Input.GetKeyDown(repare);
    public static bool AttachTile => Input.GetKeyDown(attachTile);
    public static bool BuildTown => Input.GetKeyDown(buildTown);
    public static bool BuildMine => Input.GetKeyDown(buildMine);
    public static bool BuildCannon => Input.GetKeyDown(buildCannon);
    public static bool BuildMortar => Input.GetKeyDown(buildMortar);
    public static bool BuildDefender => Input.GetKeyDown(buildDefender);
    public static bool DestroyBuilding => Input.GetKeyDown(destroyBuilding);
    public static bool Chat => Input.GetKeyDown(chat);
    public static bool SpecialAction => Input.GetKeyDown(specialAction);
    public static bool Fire => Input.GetKeyDown(fire);
    public static bool SwapTurn => Input.GetKeyDown(swapTurn);


    public static void Setup()
    {
        // “”“ —ƒ≈À¿“‹ »Õ»÷»¿À»«¿÷»ﬁ »« ‘¿…À¿
    }
}
