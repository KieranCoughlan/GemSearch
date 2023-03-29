using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class HighScore : MonoBehaviour
{
  public HighScoreEntry[] Entries;
  const int MAXSCORES = 10;

  // Start is called before the first frame update
  void Start()
  {
    Entries = new HighScoreEntry[MAXSCORES];

    string storedHighScores = PlayerPrefs.GetString("HighScores");

    if (string.IsNullOrEmpty(storedHighScores))
    {
      Entries = new HighScoreEntry[] { new HighScoreEntry() { Name = "AAA", Score = 100 },
                                       new HighScoreEntry() { Name = "MMM", Score = 90 },
                                       new HighScoreEntry() { Name = "MFM", Score = 85 },
                                       new HighScoreEntry() { Name = "FOX", Score = 80 },
                                       new HighScoreEntry() { Name = "OLI", Score = 75 },
                                       new HighScoreEntry() { Name = "KIX", Score = 70 },
                                       new HighScoreEntry() { Name = "KMA", Score = 65 },
                                       new HighScoreEntry() { Name = "EIO", Score = 60 },
                                       new HighScoreEntry() { Name = "LUK", Score = 55 },
                                       new HighScoreEntry() { Name = "RUR", Score = 50 },
                                       new HighScoreEntry() { Name = "DVS", Score = 45 }};
      Save();
    }
    else
    {
      JsonUtility.FromJsonOverwrite(storedHighScores, this);
    }
  }

  // Update is called once per frame
  void Update()
  {
        
  }

  public override string ToString()
  {
    StringBuilder sb = new StringBuilder();
    
    for (int i = 0; i < Entries.Length; i++)
    { 
      sb.Append(Entries[i].Name);
      sb.Append(": ");
      sb.Append(Entries[i].Score);

      if (i < Entries.Length - 1)
        sb.Append("\r\n");
    }

    return sb.ToString();
  }

  public void AddScore(string name, int score)
  {
    List<HighScoreEntry> asList = Entries.ToList();
    asList.Add(new HighScoreEntry() { Name = name, Score = score });
    asList.Sort((p, q) => p.Score.CompareTo(q.Score));
    
    while (asList.Count > MAXSCORES)
      asList.RemoveAt(MAXSCORES);

    Entries = asList.ToArray();
  }

  private void Save()
  {
    string json = JsonUtility.ToJson(this);
    PlayerPrefs.SetString("HighScores", json);
  }
}
