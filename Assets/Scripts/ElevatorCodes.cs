using UnityEngine;

[System.Serializable]
public partial class ElevatorCodes : MonoBehaviour
{
    public static bool[] Codes;
    public static bool[] TVsDisabled;
    public static bool initialized;
    public static void ClearCode()
    {
        if (!ElevatorCodes.initialized)
            ElevatorCodes.InitCodes();
    }

    public static void InitCodes()
    {
        ElevatorCodes.Codes = new bool[100];
        ElevatorCodes.initialized = true;
        ElevatorCodes.TVsDisabled = new bool[100];
    }

    public static void ClearTV(int num)
    {
        if (!ElevatorCodes.initialized)
            ElevatorCodes.InitCodes();
        ElevatorCodes.TVsDisabled[num] = true;
    }

    public static bool TVCleared(int num)
    {
        if (!ElevatorCodes.initialized)
            ElevatorCodes.InitCodes();
        return ElevatorCodes.TVsDisabled[num];
    }

}