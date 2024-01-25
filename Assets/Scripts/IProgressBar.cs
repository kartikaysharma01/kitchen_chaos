using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBar {
    public event EventHandler<OnProgressChangeArgs> OnProgressChange;
    public class OnProgressChangeArgs: EventArgs {
        public float progressNormalised;
    }

}
