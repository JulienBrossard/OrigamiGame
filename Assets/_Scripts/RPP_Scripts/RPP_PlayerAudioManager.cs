using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_PlayerAudioManager : MonoBehaviour
{
    public RPP_LocalAudioManager coins, flight, boat, playerState, music;

    // Quand le joueur collecte des pièces
    public void CollectCoinSound()
    {
        int randomInt;
        randomInt = Random.Range(0, 2);
        coins.PlayAudio(randomInt);
    }

    // Quand l'avion tourne
    public void PlayerTurnSound()
    {
        flight.PlayAudio(0);
    }

    // Quand l'avion décole
    public void PlayerTakeoffSound()
    {
        flight.PlayAudio(1);
    }

    //Quand le bateau tombe
    public void PlayerFallSound()
    {
        flight.PlayAudio(2);
    }

    //Quand le joueur est en train de surfer dans un nuage
    public void CloudSurfingSound()
    {
        boat.PlayAudio(0);
    }

    //Quand le joueur se transforme
    public void TransformSound()
    {
        playerState.PlayAudio(0);
    }

    //Quand le joueur perds le jeu
    public void GameOverSound()
    {
        playerState.PlayAudio(1);
    }
}
