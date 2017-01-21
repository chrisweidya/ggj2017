using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void StartJumpR(float charge);
    public static event StartJumpR onStartJumpR;
    public static void fireOnStartJumpR (float charge)
    { 
        if (onStartJumpR != null)
            onStartJumpR(charge);
    }

    public delegate void StartJumpL(float charge);
    public static event StartJumpL onStartJumpL;
    public static void fireOnStartJumpL(float charge)
    {
        if (onStartJumpL != null)
            onStartJumpL(charge);
    }

    public delegate void StartHiveJump();
    public static event StartHiveJump onStartHiveJump;
    public static void fireOnStartHiveJump()
    {
        if (onStartHiveJump != null)
            onStartHiveJump();
    }

    public delegate void StartWaveAction(int dir);
    public static event StartWaveAction onStartWave;
    public static void fireOnStartWave (int dir)
    {
        if (onStartWave != null)
            onStartWave(dir);
    }

    public delegate void StartSubmarineBreakingAction();
    public static event StartSubmarineBreakingAction onSubmarineBreaking;
    public static void fireOnSubmarineBreaking ()
    {
        if(onSubmarineBreaking != null)
            onSubmarineBreaking();
    }
}
