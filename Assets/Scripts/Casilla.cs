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

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            mapGenerator.colocarBandera = true;
            mapGenerator.ModoBanderaOn();
            
        }
    }


    private void OnMouseDown()
    {
        if(mapGenerator.colocarBandera) 
        {
            ClickBandera();
        }
        else
        {
            Click();
        }
    }

    private void Click()
    {
        if (!mapGenerator.islose)
        {
            GetComponent<SpriteRenderer>().color = Color.white;

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

    private void ClickBandera()
    {
        mapGenerator.colocarBandera = false;
        mapGenerator.ModoBanderaOff();
        if (!mapGenerator.islose)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    
}
