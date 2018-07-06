using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleGoldenCoin : MonoBehaviour {

    //bonus que sera dado pela moeda
    //pode ser "dinheiro" ou "taxa"
    public string tipoDeBonus;

    public bool foiClicado = false;

    //caminho para prefab da purpurina
    private string caminhoPurpurina = "Prefabs/Purpurina";

    //script que lida com o dinheiro
    private PropriedadesDinheiro propriedadesDinheiro;

    //script que lida com a golden coin
    private PropriedadesGoldenCoin propriedadesGoldenCoin;

    //script que contem o audio
    public RecursosDeAudio recursosDeAudio;

    // Use this for initialization
    void Start () {

        //inicializa 'propriedadesDinheiro'
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();

        //inicializa 'propriedadesGoldenCoin'
        propriedadesGoldenCoin = GameObject.FindGameObjectWithTag("propriedadesGoldenCoin").GetComponent<PropriedadesGoldenCoin>();

        //incializa 'recursosDeAudio'
        recursosDeAudio = GameObject.FindGameObjectWithTag("recursosDeAudio").GetComponent<RecursosDeAudio>();

    }

	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cliqueNaGoldenCoin();
        }
    }

    //executa o bonus de dinheiro ou taxa e some com a golden coin
    private void cliqueNaGoldenCoin()
    {
        foiClicado = true;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        if (tipoDeBonus == "dinheiro")
        {
            bonusDinheiro();
        }

        if(tipoDeBonus == "taxa")
        {
            bonusTaxa();
        }

        //toca som do click
        recursosDeAudio.tocaEfeito("somCliqueGoldenCoin");

        //emite purpurina
        //criaPurpurina();

    }

    //incrementa dinheiro de acordo com o bonus
    private void bonusDinheiro()
    {
        propriedadesGoldenCoin.finalizaBonus();

        propriedadesDinheiro.acrescentaBonusDeDinherio(propriedadesGoldenCoin.fatorBonusDinheiro);

        Destroy(gameObject);
    }

    //incrementa taxa de dinheiro por segundo de acordo com o bonus e retira o bonus depois de um certo intervalo de tempo
    private void bonusTaxa()
    {
        propriedadesGoldenCoin.iniciaBonusTaxa();

        propriedadesDinheiro.adicionaTaxaBonus(propriedadesGoldenCoin.fatorBonusTaxa);

        Invoke("retiraBonusTaxa", propriedadesGoldenCoin.tempoBonusTaxa);
    }

    //retira bonus de taxa de dinheiro por segundo
    private void retiraBonusTaxa()
    {
        propriedadesGoldenCoin.finalizaBonus();

        propriedadesDinheiro.retiraTaxaBonus();

        Destroy(gameObject);
    }
}
