using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Handler of in-app error and warning messages
// Will 

namespace UntoldGarden
{
    public class ErrorHandler : Singleton<ErrorHandler>
    {
        private GameObject errorPanel;
        private Text errorText;
        private TextAsset errorMessages; // CSV with all error messages

        private GameObject warningPanel;
        private Text warningText;
        private TextAsset warningMessages; // CSV with all error messages

        private string[] errorMsg;
        private string[] warningMsg;

        private string baseError = "Sorry, an unknown error occurred.";
        private string baseWarning = "Warning! Strong winds ahead.";

        private bool reload = false;

        void Awake()
        {
            try
            {
                errorPanel = GameObject.Find("ErrorPanel");
                errorText = errorPanel.GetComponentInChildren<Text>();
                errorPanel.SetActive(false);
                errorText.text = baseError;
            }
            catch
            {
                print("ErrorHandler can't find an error panel. Making one.");
                try
                {
                    errorPanel = Instantiate(Resources.Load<GameObject>("UI/ErrorPanel"), GameObject.Find("Canvas").transform);
                    errorPanel.name = "ErrorPanel";
                    errorText = errorPanel.GetComponentInChildren<Text>();
                    errorText.text = baseError;
                    errorPanel.SetActive(false);
                }
                catch
                {
                    print("There is no canvas in scene.");
                }
            }

            try
            {
            warningPanel = GameObject.Find("WarningPanel");
            warningText = warningPanel.GetComponentInChildren<Text>();
            warningPanel.SetActive(false);
            warningText.text = baseWarning;
            }
            catch
            {
                print("ErrorHandler can't find a warning panel. Making one.");
                try
                {
                    warningPanel = Instantiate(Resources.Load<GameObject>("UI/WarningPanel"), GameObject.Find("Canvas").transform);
                    warningPanel.name = "WarningPanel";
                    warningText = warningPanel.GetComponentInChildren<Text>();
                    warningText.text = baseWarning;
                    warningPanel.SetActive(false);
                }
                catch
                {
                    print("There is no canvas in scene.");
                }
            }

            try
            {
                errorMessages = Resources.Load<TextAsset>("UI/ErrorMessages");
                errorMsg = errorMessages.text.Split(new char[] { '\n' });       //Splits the error messages csv into array
            }
            catch
            {
                string filePath = getPath("ErrorMessages.csv");
                StreamWriter outStream = File.CreateText(filePath);   // Creates an error messages csv in case there is none
                outStream.Close();
                reload = true;
            }

            try
            {
                warningMessages = Resources.Load<TextAsset>("UI/WarningMessages");
                warningMsg = warningMessages.text.Split(new char[] { '\n' });   //Splits the error messages csv into array
            }
            catch
            {
                string filePath = getPath("WarningMessages.csv");
                StreamWriter outStream = File.CreateText(filePath);   // Creates a warning messages csv in case there is none
                outStream.Close();
                reload = true;
            }
            if (reload) { ExitPlayMode(); }
            print("path:" + getPath(""));
        }

        private void ExitPlayMode()
        {
            Debug.LogError("Error Handler exited playmode to create error and warning message csv sheets. This will only happen once per project.");
            UnityEditor.EditorApplication.isPlaying = false;
        }

        public void Error(int errorNr, string errorInfo = "")                   //Error message is displayed by calling the corresponding error array item
        {
           
            if (errorNr < errorMsg.Length)
            {
                errorText.text = errorMsg[errorNr] + errorInfo;                 //Only change error text if errorNr is within errorMsg array bounds
            } 
            else if(errorInfo != "")
            {
                errorText.text = errorInfo;                                     //Otherwise set the text to the errorInfo string
            }                                
            errorPanel.SetActive(true);
        }

        public void Warning(int warnNr, string warningInfo = "")                //Error message is displayed by calling the corresponding error array item
        {
            if (warnNr < warningMsg.Length)
            {
                warningText.text = warningMsg[warnNr] + warningInfo;            //Only change error text if errorNr is within errorMsg array bounds
            } 
            else if (warningInfo != "")
            {
                warningText.text = warningInfo;                                 //Otherwise set the text to the warningInfo string
            }
            warningPanel.SetActive(true);
        }

        public void CloseError()
        {
            errorPanel.SetActive(false);
            errorText.text = baseError;                                         //Turn back error text to avoid wrong error being displayed 
        }

        public void CloseWarning()
        {
            warningPanel.SetActive(false);
            warningText.text = baseWarning;                                     //Turn back error text to avoid wrong error being displayed 
        }

        private string getPath(string fileName)
        {
            return Application.dataPath + "/Resources/UI/" + fileName;
        }
    }
}
