using System;
using UnityEngine;

public class PluginException : NotImplementedException {

    public PluginException(object obj) : base(string.Format("[{0}] Not implemented on this platform: {1}", obj.GetType().Name, Application.platform)) {
    }
}
