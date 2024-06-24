using UnityEngine;
using System.Collections;

/* Cette fonction g�n�re �  des position alal�atoires les 3 �sp�ces de poissons qui nagent
 */
public class ActivateAllDisplays : MonoBehaviour
{
    void Start()
    {
        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

    void Update()
    {

    }
}
