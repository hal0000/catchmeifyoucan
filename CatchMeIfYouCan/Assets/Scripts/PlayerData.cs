[System.Serializable]
public class PlayerData
{
    public int Level;
    public float Gold;
    public int GunLevel;

    public PlayerData(int level = 0, float gold = 0, int gunLevel = 0)
    {
        Level = level;
        Gold = gold;
        GunLevel = gunLevel;
    }
}