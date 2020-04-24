using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handler of in-app error and warning messages
// Create two files called ErrorMessages and WarningMessages and add in a folder called UI in the Resources folder
// Will create two panels for each type of message, to use a custom panel make them and name them ErrorPanel and WarningPanel

namespace UntoldGarden
{
    public class ErrorHandler : Singleton<ErrorHandler>
    {
        private GameObject errorPanel;
        private Text errorText;
        private TextAsset errorMessages;

        private GameObject warningPanel;
        private Text warningText;
        private TextAsset warningMessages;

        private string[] errorMsg;
        private string[] warningMsg;

        private readonly string baseError = "Sorry, an unknown error occurred.";
        private readonly string baseWarning = "Warning! Strong winds ahead.";

        private bool hasErrorCsv = false;
        private bool hasWarningCsv = false;

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
                hasErrorCsv = true;
            }
            catch
            {
                print("No error messages file. Make a csv and put in the Resources folder.");
            }

            try
            {
                warningMessages = Resources.Load<TextAsset>("UI/WarningMessages");
                warningMsg = warningMessages.text.Split(new char[] { '\n' });   //Splits the error messages csv into array
                hasWarningCsv = true;
            }
            catch
            {
                print("No warning messages file. Make a csv and put in the Resources folder.");
            }
        }

        public void Error(string errorInfo = "", int? errorNr = null)                   //Error message is displayed by calling the corresponding error array item
        {
            if (errorNr.HasValue && hasErrorCsv)
            {
                if (errorNr < errorMsg.Length)
                {
                    errorText.text = errorMsg[errorNr.Value] + errorInfo;                 //Only change error text if errorNr is within errorMsg array bounds
                }
                else if (errorInfo != "")
                {
                    errorText.text = errorInfo;                                     //Otherwise set the text to the errorInfo string
                }
            }
            else if (errorInfo != "")
            {
                errorText.text = errorInfo;                                     //Otherwise set the text to the errorInfo string
            }                                
            errorPanel.SetActive(true);
        }

        public void Warning(string warningInfo = "", int? warnNr = null)                //Error message is displayed by calling the corresponding error array item
        {
            if (warnNr.HasValue && hasWarningCsv)
            {
                if (warnNr < warningMsg.Length)
                {
                    warningText.text = warningMsg[warnNr.Value] + warningInfo;            //Only change error text if errorNr is within errorMsg array bounds
                }
                else if (warningInfo != "")
                {
                    warningText.text = warningInfo;                                 //Otherwise set the text to the warningInfo string
                }
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
