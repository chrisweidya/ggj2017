using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void StartJumpR();
    public static event StartJumpR onStartJumpR;
    public static void fireOnStartJumpR ()
    { 
        if (onStartJumpR != null)
            onStartJumpR();
    }

    public delegate void StartJumpL();
    public static event StartJumpL onStartJumpL;
    public static void fireOnStartJumpL()
    {
        if (onStartJumpL != null)
            onStartJumpL();
    }

    public delegate void StartHiveJump();
    public static event StartHiveJump onStartHiveJump;
    public static void fireOnStartHiveJump()
    {
        if (onStartHiveJump != null)
            onStartHiveJump();
    }

    public delegate void StartOctopusLegAction();
    public static event StartOctopusLegAction onStartOctopusLeg;
    public static void fireOnStartOctopusLeg ()
    {
        if (onStartOctopusLeg != null)
            onStartOctopusLeg();
    }

    public delegate void StartSubmarineBreakingAction();
    public static event StartSubmarineBreakingAction onSubmarineBreaking;
    public static void fireOnSubmarineBreaking ()
    {
        if(onSubmarineBreaking != null)
            onSubmarineBreaking();
    }
}
