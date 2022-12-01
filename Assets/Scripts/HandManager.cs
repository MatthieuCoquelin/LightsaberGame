using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class HandManager
{
    private string m_handTag;
    public void GetHandTag(BaseInteractionEventArgs eventArgs)
    {
        m_handTag = "";

        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
            GetHandTag(controllerInteractor.xrController);
    }

    public void GetHandTag(XRBaseController controller)
    {
        m_handTag = controller.tag;
    }

    public string HandTag
    {
        get
        {
            return m_handTag;
        }
    }
}
