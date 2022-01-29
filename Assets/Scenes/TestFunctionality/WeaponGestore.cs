using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGestore : MonoBehaviour
{
    public Gun primaArma;
    public Gun secondaArma;
    public Gun terzaArma;
    public Gun quartaArma;

    public Gun coltello;

    public Gun selectedArma;

    void Start()
    {
        primaArma.ActivateWeapon(true);
        secondaArma.ActivateWeapon(false);
        terzaArma.ActivateWeapon(false);
        quartaArma.ActivateWeapon(false);

        coltello.ActivateWeapon(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            primaArma.ActivateWeapon(true);
            secondaArma.ActivateWeapon(false);
            terzaArma.ActivateWeapon(false);
            quartaArma.ActivateWeapon(false);

            coltello.ActivateWeapon(false);
            selectedArma = primaArma;
        }

        //Select primary weapon when pressing 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            primaArma.ActivateWeapon(false);
            secondaArma.ActivateWeapon(true);
            terzaArma.ActivateWeapon(false);
            quartaArma.ActivateWeapon(false);

            coltello.ActivateWeapon(false);
            selectedArma = secondaArma;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            primaArma.ActivateWeapon(false);
            secondaArma.ActivateWeapon(false);
            terzaArma.ActivateWeapon(true);
            quartaArma.ActivateWeapon(false);

            coltello.ActivateWeapon(false);
            selectedArma = terzaArma;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            primaArma.ActivateWeapon(false);
            secondaArma.ActivateWeapon(false);
            terzaArma.ActivateWeapon(false);
            quartaArma.ActivateWeapon(true);

            coltello.ActivateWeapon(false);
            selectedArma = quartaArma;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            primaArma.ActivateWeapon(false);
            secondaArma.ActivateWeapon(false);
            terzaArma.ActivateWeapon(false);
            quartaArma.ActivateWeapon(false);

            coltello.ActivateWeapon(true);
            selectedArma = coltello;
        }
    }
}
