using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleLogger : MonoBehaviour
{
    public TextMeshProUGUI consoleText;
    public int maxLines = 10;
    public int maxCharsPerLine = 100;

    private Queue<string> logLines = new Queue<string>();
    private Dictionary<string, int> logCounts = new Dictionary<string, int>();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (maxCharsPerLine > 0 && logString.Length > maxCharsPerLine)
        {
            logString = logString.Substring(0, maxCharsPerLine) + "...";
        }

        if (logCounts.ContainsKey(logString))
        {
            logCounts[logString]++;
        }
        else
        {
            logCounts[logString] = 1;
            logLines.Enqueue(logString);
        }

        if (logLines.Count > maxLines)
        {
            string oldestLog = logLines.Dequeue();
            logCounts.Remove(oldestLog);
        }

        UpdateConsoleText();
    }

    void UpdateConsoleText()
    {
        consoleText.text = string.Join("\n", GetFormattedLogs());
    }

    List<string> GetFormattedLogs()
    {
        List<string> formattedLogs = new List<string>();
        foreach (string log in logLines)
        {
            int count = logCounts[log];
            formattedLogs.Add(count > 1 ? $"{log} ({count})" : log);
        }
        return formattedLogs;
    }
}
