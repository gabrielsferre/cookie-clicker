using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleBotaoConstrucao : MonoBehaviour {

    //script que controla o dinheiro
    private PropriedadesDinheiro propriedadesDinheiro;

    //script que controla as construcoes
    private PropriedadesConstrucoes propriedadesConstrucoes;

    //index da construcao representada pelo botao
    public int index;

    // Use this for initialization
    void Start () {

        //inicializa propriedadesDinheiro
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();

        //inicializa propriedadesConstrucoes
        propriedadesConstrucoes = GameObject.FindGameObjectWithTag("propriedadesConstrucoes").GetComponent<PropriedadesConstrucoes>();

        //Adiciona a funcao que sera usada no click
        GetComponent<Button>().onClick.AddListener(executaNoClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void executaNoClick()
    {
        if( propriedadesConstrucoes.modo == "compra" )
        {
            compraConstrucao();
        }

        if( propriedadesConstrucoes.modo == "venda" )
        {
            vendeConstrucao();
        }
    }

    //compra construcao caso tenha dinheiro o suficiente
    private void compraConstrucao()
    {
        //reduz dinheiro caso seja o suficiente
        propriedadesDinheiro.realizaCompra(propriedadesConstrucoes.precosCompra[index]);

        //aumenta quantidade de construcoes e atualiza seu preco de venda e compra
        propriedadesConstrucoes.compraConstrucao(index);

        //atualiza taxa de ganho de dinheiro por segundo
        propriedadesDinheiro.calculaTaxaGanho();
    }

    //vende construcao
    private void vendeConstrucao()
    {
        if (propriedadesConstrucoes.quantidadesConstrucoes[index] > 0)
        {
            //incrementa dinheiro
            propriedadesDinheiro.aumentaDinheiro(propriedadesConstrucoes.precosVenda[index]);

            //diminui quantidade de construcoes e atualiza seu preco de venda e compra
            propriedadesConstrucoes.vendeConstrucao(index);

            //atualiza taxa de ganho de dinheiro por segundo
            propriedadesDinheiro.calculaTaxaGanho();
        }
    }
}
