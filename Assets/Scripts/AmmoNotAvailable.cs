using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoNotAvailable : System.Exception {

    public AmmoNotAvailable()
    {
    }

    public  AmmoNotAvailable(string message)
        : base(message)
    {
    }

    public AmmoNotAvailable(string message, System.Exception inner)
        : base(message, inner)
    {
    }
}
