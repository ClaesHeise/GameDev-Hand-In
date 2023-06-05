using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{

    // Save player data
    public static void SavePlayer(GameObject player) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.gamedev";
        FileStream stream = new FileStream(path, FileMode.Create); // Create a file at the path

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Load player data
    public static PlayerData LoadPlayer()
    {
        Debug.Log("Loading game...");
        string path = Application.persistentDataPath + "/player.gamedev";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open); // Open the file at the path

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else 
        {
            Debug.LogError("No saved game found at " + path);
            return null;
        }
    }


    // Save ship data
    public static void SaveShip(GameObject ship) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ship.gamedev";
        FileStream stream = new FileStream(path, FileMode.Create); // Create a file at the path

        ShipData data = new ShipData(ship);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Load ship data
    public static ShipData LoadShip()
    {
        Debug.Log("Loading game...");
        string path = Application.persistentDataPath + "/ship.gamedev";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open); // Open the file at the path

            ShipData data = formatter.Deserialize(stream) as ShipData;
            stream.Close();

            return data;
        }
        else 
        {
            Debug.LogError("No saved game found at " + path);
            return null;
        }
    }
}
