using System.Collections;
using System.Collections.Generic;


public class DefineScript
{
    public static string PASS_STAGE = "Prefabs/Stage/";
    public static string PASS_BLOCKS = "Prefabs/Blocks/";
    public static string PASS_BACKGROUNDS = "StageBackGround/";


    public static int NUM_STAGE = 5;
    public static int NUM_BACKGROUND = 3;
    public static float JUDGE_DISTANCE = 3.0f;
    public enum CollisionIndex {Front=0,Back,Right,Left,Top,Bottom};
    public static float JUDGE_BURNNINGTIME = 5.0f;

}
