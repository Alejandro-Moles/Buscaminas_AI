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
        //si se le da al click derecho cambiamos al modo bandera
        if (Input.GetMouseButton(1))
        {
            mapGenerator.colocarBandera = true;
            //activamos la imagen para que el jugador sepa que esta en ese modo
            mapGenerator.ModoBanderaOn();
            
        }
    }


    private void OnMouseDown()
    {
        //si esta activado el modo bandera ejecutamos otra funcion distinta
        if(mapGenerator.colocarBandera) 
        {
            ClickBandera();
        }
        else
        {
            Click();
        }
    }

    //funcion que hace el click en la casilla y comprueba si hay bomba o no
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

    //funcion que pone la "bandera" en la casilla cliqueada
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
