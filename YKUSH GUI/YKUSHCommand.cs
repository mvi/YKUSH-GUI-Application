using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace YKUSHGUI;

public enum BoardType
{
    ykushxs, // Has one port
    ykush, // Has three ports
    ykush3, // Has three ports
}

public static class YKUSHCommand
{
    public static List<string> ListAttachedBoards(BoardType boardType)
    {
        List<string> boardSerials = new List<string>();

        ProcessStartInfo processStartInfo = new ProcessStartInfo("ykushcmd")
        {
            Arguments = $"{boardType} -l",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        Process process = Process.Start(processStartInfo);

        Regex regex = new Regex(@"\d+\. Board found with serial number: (\w+)");
        
        while (!process.StandardOutput.EndOfStream)
        {
            var line = process.StandardOutput.ReadLine().Trim();


            var match = regex.Match(line);
            if (match.Success)
            {
                boardSerials.Add(match.Groups[1].Value);
            }
        }

        return boardSerials;
    }

    public static bool GetPortStatus(BoardType boardType, int portIndex = -1)
    {
        bool isOn = false;
        string portSuffix = "";

        if (boardType != BoardType.ykush) // Has three ports
        {
            if (portIndex == -1)
            {
                portSuffix = " a"; // All
            }
            else
            {
                portSuffix = " " + portIndex;
            }
        }

        ProcessStartInfo processStartInfo = new ProcessStartInfo("ykushcmd")
        {
            Arguments = $"{boardType} -g {portSuffix}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        Process process = Process.Start(processStartInfo);

        while (!process.StandardOutput.EndOfStream)
        {
            var line = process.StandardOutput.ReadLine().Trim();

            if (line.EndsWith("ON", StringComparison.InvariantCultureIgnoreCase))
            {
                isOn = true;
            }
        }

        return isOn;
    }

    public static void ChangePortStatus(BoardType boardType, bool isOn, int portIndex = -1)
    {
        string portSuffix = "";

        if (boardType != BoardType.ykush) // Has three ports
        {
            if (portIndex == -1)
            {
                portSuffix = " a"; // All
            }
            else
            {
                portSuffix = " " + portIndex;
            }
        }

        ProcessStartInfo processStartInfo = new ProcessStartInfo("ykushcmd")
        {
            UseShellExecute = false,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        };

        if (isOn)
        {
            processStartInfo.Arguments = $"{boardType} -u {portSuffix}";
        }
        else
        {
            processStartInfo.Arguments = $"{boardType} -d {portSuffix}";
        }
        Process.Start(processStartInfo);
    }
}