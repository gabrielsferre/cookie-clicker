using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleTextoUp : MonoBehaviour {

    //velocidade de subida
    private Vector3 velocidade = new Vector3(0,1,0);

    //tempo que demora para desaparecer 99% em segundos
    private float tempoDecaimento = 3F;

    //tempo usado para contar o quanto vai demorar para o objeto desaparecer
    private float tempoDecorrido = 0F;

    //script que controla o dinheiro
    private PropriedadesDinheiro propriedadesDinheiro;

    //rect transform do objeto
    private RectTransform rectTransform;

    //texto do objeto
    private Text texto;

    // Use this for initialization
    void Start () {

        //inicializa propriedadesDinheiro
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();

        //inicaliza rectTranfrom
        rectTransform = GetComponent<RectTransform>();

        //inicializa texto
        texto = GetComponent<Text>();
        texto.text = "+" + PropriedadesDinheiro.abreviaNumero(propriedadesDinheiro.incrementoClick) + " " + PropriedadesDinheiro.contagem(propriedadesDinheiro.incrementoClick);
    }
	
	// Update is called once per frame
	void Update () {

        //Move texto
        rectTransform.position += velocidade * Time.deltaTime;

        //Apaga texto gradualmente
        texto.color = new Color(0f, 0f, 0f, desaparecimento(tempoDecorrido));

        //Incrementa tempoDecorrido
        tempoDecorrido += Time.deltaTime;
	}

    //Calcula o alpha (transparencia) do texto para um certo valor de tempo, se o alpha for pequeno, exclui objeto
    private float desaparecimento(float tempo)
    {
        float resultado = Mathf.Exp(-2 * Mathf.Log(10) * tempo / tempoDecaimento);
        if (resultado > 0.01)
        {
            return resultado;
        }

        Destroy(gameObject);
        return 0f;
    }
}
