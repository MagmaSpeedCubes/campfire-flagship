public static class GameState
{
    static string[] players;

    public static string[] Players => players;
    public static int PlayerCount => players.Length;

    public static void Initialize(string[] playerArray)
    {
        players = playerArray;
    }
}
