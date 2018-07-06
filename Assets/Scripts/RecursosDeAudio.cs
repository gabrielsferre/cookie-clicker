using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RecursosDeAudio : MonoBehaviour {

    //dicionario com os efeitos de audio
    //"somCliqueLogo" eh o som do click no logo do fastfood
    //"somCliqueGoldenCoin" eh o som do click na golden coin
    Dictionary<string, AudioClip> dicionarioEfeitos = new Dictionary<string, AudioClip>();

    //musica
    AudioClip musica;

    //caminhos para os efeitos
    private string caminhoSomCliqueLogo = "Audio/clique";
    private string caminhoSomCliqueGoldenCoin = "Audio/goldenCoinClique";
    private string caminhoMusica = "Audio/musica";

    //dicionario que diz se um dado efeito esta ligado ou desligado
    Dictionary<string, bool> dicionarioEfeitosLigados = new Dictionary<string, bool>();

    //diz se a musica esta ligada ou nao
    bool musicaLigada = false;

    private float volumeInicial = 1F;

    //Array com os audio sources do objeto, [0] sera o dos efeitos e [1] o da musica
    private AudioSource[] audioSources = new AudioSource[2];



    // Use this for initialization
    void Start() {

        //Inicializa 'audioSources'
        audioSources = GetComponents<AudioSource>();
        audioSources[0].volume = volumeInicial;
        audioSources[1].volume = volumeInicial;

        //carrega efeitos sonoros
        carregaSom();

        //carrega dicionarioEfeitosLigados
        carregaDicionarioEfeitosLigados();

        //Define audioSources
        audioSources[1].clip = musica;

        //Toca musica
        tocaMusica();

    }

    // Update is called once per frame
    void Update() {

    }


    //Muda volume do som
    public void mudaVolume(float novoVolume)
    {
        audioSources[0].volume = novoVolume;
        audioSources[1].volume = novoVolume;
    }

    //Carrega efeitos sonoros
    private void carregaSom()
    {
        //Carrega dicionario com efeitos sonoros
        dicionarioEfeitos["somCliqueLogo"] = (AudioClip)Resources.Load(caminhoSomCliqueLogo);
        dicionarioEfeitos["somCliqueGoldenCoin"] = (AudioClip)Resources.Load(caminhoSomCliqueGoldenCoin);

        //Carrega musica
        musica = (AudioClip)Resources.Load(caminhoMusica);
    }

    //toca musica em loop
    void tocaMusica() {

        if (musicaLigada)
        {
            audioSources[1].loop = true;
            audioSources[1].Play();
        }
    }

    public void desligaMusica()
    {
        musicaLigada = false;
        audioSources[1].mute = true;
    }

    //toca o efeito dado sua chave no dicionario
    public void tocaEfeito(string chaveEfeito )
    {
        if (dicionarioEfeitosLigados[chaveEfeito])
        {
            audioSources[0].clip = dicionarioEfeitos[chaveEfeito];
            audioSources[0].Play();
        }
    }

    //carrega 'dicionarioEfeitosLigados' com true
    private void carregaDicionarioEfeitosLigados()
    {
        foreach( string chave in dicionarioEfeitos.Keys )
        {
            dicionarioEfeitosLigados[chave] = false;
        }
    }
}
