using UnityEngine;

public class PlayerData 
{
    public void SetPlayerLevel(int level)
    {
        if (PlayerPrefs.HasKey("PL"))
        {
            PlayerPrefs.SetInt("PL", level);
        }
    }
    public int GetPlayerLevel() 
    {
        if (!PlayerPrefs.HasKey("PL")) 
        {
            PlayerPrefs.SetInt("PL",1);
        }
        return PlayerPrefs.GetInt("PL");
    }
}
