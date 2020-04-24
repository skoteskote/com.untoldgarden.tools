using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntoldGarden
{
    public class CloseError : MonoBehaviour
    {
        public void CloseMe()
        {
            ErrorHandler.Instance.CloseError();
        }
    }
}
