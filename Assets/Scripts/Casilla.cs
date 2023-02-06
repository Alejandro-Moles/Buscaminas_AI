using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Casilla : MonoBehaviour
{
    public int posx, posy;
    public bool isBomb;


    private void OnMouseDown()
    {
        if (isBomb)
        {
            Debug.Log("BOOOOOOOOOOOOOOOOOOOOOBA");
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            transform.Find("casilla_txt").GetComponent<TextMeshPro>().text = MapGenerator.instance.GetBombsAround(posx,posy).ToString();
        }
    }
}
