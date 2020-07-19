using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PresetManager : MonoBehaviour
{
    const string PresetDirectory = "Presets";

    private void Start()
    {
        UpdateDropdownList();
    }

    public void LoadPresetToOrb(string presetName, Orb orb)
    {
        orb.presetName = presetName;

        string[] lines = File.ReadAllLines(PresetDirectory + "/" + presetName);

        for (int i = 0; i < lines.Length; i++)
        {
            //float parameter
            if (lines[i][0] == 'f')
            {
                lines[i] = RemoveNextComma(lines[i]);

                //key
                int nextValLength = lines[i].IndexOf(',');
                int key;
                if (!int.TryParse(lines[i].Substring(0, nextValLength), out key))
                    Debug.Log("Key");

                lines[i] = RemoveNextComma(lines[i]);

                //min
                nextValLength = lines[i].IndexOf(',');
                float min;
                if (!float.TryParse(lines[i].Substring(0, nextValLength), out min))
                    Debug.Log("Min");

                //max
                lines[i] = RemoveNextComma(lines[i]);
                nextValLength = lines[i].IndexOf(',');
                float max;
                if (!float.TryParse(lines[i].Substring(0, nextValLength), out max))
                    Debug.Log("Max");

                //rate
                lines[i] = RemoveNextComma(lines[i]);
                nextValLength = lines[i].IndexOf(',');
                float rate;
                if (!float.TryParse(lines[i].Substring(0, nextValLength), out rate))
                    Debug.Log("Rate");

                //offset
                lines[i] = RemoveNextComma(lines[i]);
                nextValLength = lines[i].IndexOf(',');
                float offset;
                if (!float.TryParse(lines[i].Substring(0, nextValLength), out offset))
                    Debug.Log("Offset");

                //random
                lines[i] = RemoveNextComma(lines[i]);
                nextValLength = lines[i].IndexOf(',');
                float rand;
                if (!float.TryParse(lines[i].Substring(0, nextValLength), out rand))
                    Debug.Log("Rand");

                //cyclical
                lines[i] = RemoveNextComma(lines[i]);
                nextValLength = lines[i].IndexOf(',');
                bool cyclical = lines[i].Substring(0, nextValLength).ToLower() == "true";

                //startAsc
                lines[i] = RemoveNextComma(lines[i]);
                nextValLength = lines[i].IndexOf(',');
                bool startAscending = lines[i].Substring(0, nextValLength).ToLower() == "true";

                //Ascend
                lines[i] = RemoveNextComma(lines[i]);
                nextValLength = lines[i].IndexOf(',');
                bool ascend = lines[i].Substring(0, nextValLength).ToLower() == "true";

                FloatParameter parameter = orb.CurrentBrush.ParameterMap.GetFloatParameterByKey(key);

                if (parameter == null)
                {
                    Debug.Log("Param not found");
                    return;
                }

                parameter.SetMin(min);
                parameter.SetMax(max);
                parameter.SetRate(rate);
                parameter.SetOffset(offset);
                parameter.SetRandom(rand);
                parameter.SetCyclical(cyclical);
                parameter.SetStartAscending(startAscending);
                parameter.SetAscend(ascend);
            }
            //bool param
            else if (lines[i][0] == 'b')
            {

            }
        }
    }

    string RemoveNextComma(string s)
    {
        //remove everything before comma
        while (s[0] != ',')
            s = s.Substring(1);
        //remove comma
        s = s.Substring(1);

        return s;
    }

    public void SavePreset(string presetName)
    {

        if(presetName == "" || presetName.ToLower() == "custom")
            GameManager.ActiveGameManager.MessageQueue.AddMessageToQueue("Invalid Name");

        string[] files = Directory.GetFiles(PresetDirectory);

        foreach (string s in files)
        {
            if (s.Substring(PresetDirectory.Length + 1) == presetName)
            {
                //promt for overwrite
                GameManager.ActiveGameManager.OverwritePresetWindow.gameObject.SetActive(true);
                GameManager.ActiveGameManager.OverwritePresetWindow.OpenWindow();


                return;
            }
        }

        //create new preset file
        FileStream f = File.Create(PresetDirectory + "/" + presetName);
        f.Close();
        StreamWriter w = new StreamWriter(PresetDirectory + "/" + presetName);

        foreach (FloatParameter p in GameManager.ActiveGameManager.OrbManager.CurrentOrb.CurrentBrush.ParameterMap.FloatParameters)
        {
            string line = "f";
            line += ",";
            line += p.ParamKey.ToString();
            line += ",";
            line += p.Min.ToString();
            line += ",";
            line += p.Max.ToString();
            line += ",";
            line += p.Rate.ToString();
            line += ",";
            line += p.Offset.ToString();
            line += ",";
            line += p.Randomness.ToString();
            line += ",";
            line += p.IsCyclical.ToString();
            line += ",";
            line += p.StartAscending.ToString();
            line += ",";
            line += p.Ascend.ToString();
            line += ",";
            w.WriteLine(line);
        }

        foreach (BooleanParameter b in GameManager.ActiveGameManager.OrbManager.CurrentOrb.CurrentBrush.ParameterMap.BoolParameters)
        {
            string line = "b";
            line += ",";
            line += b.ParamKey.ToString();
            line += ",";
            line += b.Value.ToString();
            line += ",";
            w.WriteLine(line);
        }

        w.Close();
        UpdateDropdownList();
        GameManager.ActiveGameManager.MessageQueue.AddMessageToQueue("Preset " + presetName + " Created");

    }

    public void OverwritePreset(string presetName)
    {
        File.Delete(PresetDirectory + "/" + presetName);

        SavePreset(presetName);
    }

    void UpdateDropdownList()
    {
        if (!Directory.Exists(PresetDirectory))
            Directory.CreateDirectory(PresetDirectory);

        string[] files = Directory.GetFiles(PresetDirectory);

        Dropdown dropdown = GameManager.ActiveGameManager.BrushEditor.presetDropdownList;
        dropdown.ClearOptions();

        dropdown.options.Add(new Dropdown.OptionData("Custom"));

        foreach (string f in files)
            dropdown.options.Add(new Dropdown.OptionData(f.Substring(PresetDirectory.Length + 1)));
    }
}
