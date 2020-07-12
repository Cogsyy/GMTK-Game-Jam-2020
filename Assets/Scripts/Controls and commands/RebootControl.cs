using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public class RebootControl : Controls
{
    [SerializeField] private AudioClip _completeClip;

    protected override void Execute(params string[] parameters)
    {
        base.Execute();

        string finalParam = EnsureSlash(parameters[0]);

        Controls commandToReboot = CommandsList.Instance.FindControl(finalParam);
        StartCoroutine(RebootCo(commandToReboot));
    }

    protected override bool CanActivate(out string errorMessage)
    {
        //parameters are valid at this point
        string finalParam = EnsureSlash(currentParameters[0]);
        Controls commandToReboot = CommandsList.Instance.FindControl(finalParam);
        if (commandToReboot.HaveControl)
        {
            errorMessage = "Error, overlords already control this action\n";
            return false;
        }

        return base.CanActivate(out errorMessage);
    }

    private string EnsureSlash(string command)
    {
        if (!command.Contains("/"))
        {
            command = "/" + command;
        }

        return command;
    }

    protected override bool ValidParameters(params string[] parameters)
    {
        if (parameters.Length > 1)
        {
            return false;
        }

        string paramWithAddedSlash = "/" + parameters[0];
        return CommandsList.Instance.CommandExists(paramWithAddedSlash, out Controls unused);
    }

    private IEnumerator RebootCo(Controls commandToReboot)
    {
        CommandTyper.Instance.SetCommandBlocked(true);

        string rebootingText = "Started human reboot routines...\n";
        CommandTyper.Instance.SetCommandText(rebootingText);

        float iterations = 4;
        for (int i = 0; i < iterations; i++)
        {
            CommandTyper.Instance.PlayCommandSound(audioClip);

            string text = ((i / iterations) * 100).ToString() + "% complete" + "\n";
            CommandTyper.Instance.SetCommandText(text);
            yield return new WaitForSeconds(1);
        }

        CommandTyper.Instance.PlayCommandSound(_completeClip);
        CommandTyper.Instance.SetCommandText("Reboot of small human brain successful\n");

        commandToReboot.Deactivate();
        Deactivate();
        commandToReboot.SetHaveControl(true);
        CommandsList.Instance.RegainControl(commandToReboot);

        CommandTyper.Instance.SetCommandBlocked(false);
    }
}
