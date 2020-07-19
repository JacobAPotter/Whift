using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageQueue : MonoBehaviour
{
    float timeStamp;
    const float displayTime = 3f;
    List<string> messages;
    Text displayText;

    void Start()
    {
        messages = new List<string>();
        displayText = GetComponent<Text>();
        displayText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad - timeStamp > displayTime)
        {
            if (messages.Count > 0)
            {
                displayText.text = messages[0];
                timeStamp = Time.timeSinceLevelLoad;
                messages.Remove(messages[0]);
            }
            else
                displayText.text = "";
        }
    }

    public void AddMessageToQueue(string s)
    {
        messages.Add(s);
    }
}
