using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostradorTela : MonoBehaviour {

    //Textos
    public Text textoDinheiro;
    public Text textoTaxaDinheiros;

    //Propriedades dinheiro
    public PropriedadesDinheiro propriedadesDinheiro;

	// Use this for initialization
	void Start () {

        //inicializa propriedadesDinheiro
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();
	}
	
	// Update is called once per frame
	void Update () {

        //texto com a quantidade de dinheiro e taxa de dinheiros
        textoDinheiro.text = PropriedadesDinheiro.abreviaNumero(propriedadesDinheiro.dinheiro) + " " + PropriedadesDinheiro.contagem(propriedadesDinheiro.dinheiro) + " reais";
        textoTaxaDinheiros.text = PropriedadesDinheiro.abreviaNumero(propriedadesDinheiro.getTaxaGanhoTotal()) + " " + 
            PropriedadesDinheiro.contagem(propriedadesDinheiro.getTaxaGanhoTotal()) + " reais por segundo";
    }
}
