using UnityEngine;

public class PlayerData 
{
    public void SetPlayerLevel(int level)
    {
        if (PlayerPrefs.HasKey("PL"))
        {
            PlayerPrefs.SetInt("PL", level);
        }
        else
        {
            PlayerPrefs.SetInt("PL", 1);
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
