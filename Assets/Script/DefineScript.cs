using System.Collections;
using System.Collections.Generic;


public class DefineScript
{
    public enum CollisionIndex { Front = 0, Back, Right, Left, Top, Bottom };

    public static string PASS_RESOURCE_STAGE = "Prefabs/Stage/";
    public static string PASS_RESOURCE_BLOCKS = "Prefabs/Blocks/";
    public static string PASS_RESOURCE_BACKGROUNDS = "StageBackGround/";

    public static string PASS_GAMEMAIN_MAT = "GameMain/Materials";
    
    public static string PASS_SOUND_SE = "SE/";
    public static string PASS_SOUND_BGM = "BGM/";


    public static int NUM_STAGE = 1;
    public static int NUM_BACKGROUND = 1;
    public static int NUM_BLOCKS = 30;

    public static float JUDGE_BURNNINGTIME = 5.0f;
    public static float JUDGE_DISTANCE = 60.0f;
    public static float JUDGE_BURNNIGSPEED = 0.05f;
}
