using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntoldGarden
{
    public class CloseWarning : MonoBehaviour
    {
        public void CloseMe()
        {
            ErrorHandler.Instance.CloseWarning();
        }
    }
}
