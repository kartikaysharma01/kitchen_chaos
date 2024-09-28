using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private static void Awake() {
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
    }
}
