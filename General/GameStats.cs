
/**
 *  Static GameStats class used to preserve data between scenes
 */
public static class GameStats
{

    #region ExamplePoints
    /**
     * The points field is an example of how to get and set a value that can be preserved between scenes
     For example, setting GameStats.Points = currentScore; in your game over code will then let you read 
     it in a GameOver menu scene by accessing GameStats.Points
     */
    private static int points;
    public static int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }

    #endregion


    public static void Clear()
    {
        plants = 0;
    }

}
