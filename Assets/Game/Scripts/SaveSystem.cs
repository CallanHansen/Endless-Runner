using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveHighScore() // Save the high score into binary file
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoreData data = new ScoreData();

        formatter.Serialize(stream, data); // Write data to file
        stream.Close();
    }

    public static ScoreData LoadScore() // Load the high score, converting from binary into readable format
    {
        string path = Application.persistentDataPath + "/score.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoreData data = formatter.Deserialize(stream) as ScoreData; // Change from binary back into readable format
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
