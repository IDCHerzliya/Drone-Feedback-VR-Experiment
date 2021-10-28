using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioA : MonoBehaviour
{
    public DroneController controller;

    private void Start()
    {
        controller.SetFog(true);
        controller.Wait(3F);
        controller.Move(new Vector3(1.22f, 1.6f, -27f), 5f, true);
        controller.Wait(3f);
        controller.SetTexture(0);
        controller.AwaitUserInput();

        controller.AwaitAdminInput();
        controller.SetTexture(1);
        controller.AwaitUserInput();
        controller.Wait(1f);

        controller.AwaitAdminInput();
        controller.SetTexture(2);
        controller.AwaitUserInput();
        controller.Wait(1f);

        controller.AwaitAdminInput();
        controller.SetTexture(3);
        controller.AwaitUserInput();
        controller.Wait(1f);
    }
}
