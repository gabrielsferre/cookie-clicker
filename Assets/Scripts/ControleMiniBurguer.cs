using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleMiniBurguer : MonoBehaviour {

    //caminho para as sprites
    private string caminho = "Sprites/miniBurguer1-Sheet";

    //index aleatorio da sprite que sera usada
    private int index;

    //possivel modulo da velocidade inicial(fracoes da velocidade maxima)
    private float[] velocidades = { 0.25F, 0.5F, 0.75F, 1F };

    //possiveis angulos da velocidade inicial em graus
    private float[] angulos = { 45F, 60F, 75F, 105F, 120F, 135F };

    //possiveis tamanhos (fracoes do tamanho maximo)
    private float[] tamanhos = { 0.25F, 0.5F, 0.75F, 1F };

    //modulo maximo da velocidade
    private float velocidadeMaxima = 1.5F;

    //aceleracao
    private Vector2 aceleracao = new Vector2(0,-2F);

    //tempo que demora para desaparecer 99% em segundos
    private float tempoDecaimento = 1.5F;

    //tempo usado para contar o quanto vai demorar para o objeto desaparecer
    private float tempoDecorrido = 0F;

    //rigidBody
    private Rigidbody2D rigidBody;

    //sprite
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {

        //inicializa rigidBody
        rigidBody = GetComponent<Rigidbody2D>();

        //inicializa spriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        atribuiSpriteAleatoria();
        atribuiVelocidadeAleatoria();
    }
	
	// Update is called once per frame
	void Update () {

        //aplica aceleracao
        rigidBody.velocity += aceleracao * Time.deltaTime;

        //aplica desaparecimento
        spriteRenderer.color = new Color(1F, 1F, 1F, desaparecimento(tempoDecorrido));

        //incrementa 'tempoDecorrido'
        tempoDecorrido += Time.deltaTime;
	}

    private void atribuiSpriteAleatoria()
    {
        //carrega sprites
        Sprite[] arraySprites = Resources.LoadAll<Sprite>(caminho);

        //Escolhe sprite de miniburguer aleatoria
        spriteRenderer.sprite = Resources.LoadAll<Sprite>(caminho)[Random.Range(0, arraySprites.Length)];
    }
    
    private void atribuiVelocidadeAleatoria()
    {
        float moduloVelocidade = velocidadeMaxima * velocidades[Random.Range(0, velocidades.Length)];

        //angulo em radianos
        float angulo = angulos[Random.Range(0, angulos.Length)] * Mathf.Deg2Rad;

        rigidBody.velocity = new Vector3(moduloVelocidade * Mathf.Cos(angulo), moduloVelocidade * Mathf.Sin(angulo), 0);
    }

    private void atribuiTamanhoAleatorio()
    {
        GetComponent<Transform>().localScale = GetComponent<Transform>().localScale * tamanhos[Random.Range(0, tamanhos.Length)];
    }

    //Calcula o alpha (transparencia) da sprite para um certo valor de tempo, se o alpha for pequeno, exclui objeto
    private float desaparecimento(float tempo)
    {   
        float resultado = Mathf.Exp(-2 * Mathf.Log(10) * tempo / tempoDecaimento);
        if (resultado > 0.01) {
            return resultado;
        }

        Destroy(gameObject);
        return 0;
    }
}
