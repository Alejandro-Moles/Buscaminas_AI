using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Casilla : MonoBehaviour
{
    public int posx, posy;
    public bool isBomb;
    public bool ispointed = false;

    private MapGenerator mapGenerator;


    private void Start()
    {
        mapGenerator = GameObject.FindGameObjectWithTag("Map").GetComponent<MapGenerator>();
    }


    private void OnMouseDown()
    {

        if (!mapGenerator.islose)
        {
            if (isBomb)
            {
                mapGenerator.TerminarPartida();
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                transform.Find("casilla_txt").GetComponent<TextMeshPro>().text = MapGenerator.instance.GetBombsAround(posx, posy).ToString();
                if (!ispointed)
                {
                    mapGenerator.contadorGanar++;
                    ispointed = true;
                    mapGenerator.puntuacion++;
                }
            }
        }
        
    }
}
