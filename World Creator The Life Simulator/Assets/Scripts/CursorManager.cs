using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to alter the looks of the cursor
public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(9, 9), CursorMode.Auto);
    }

}
