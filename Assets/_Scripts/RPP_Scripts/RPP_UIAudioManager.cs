using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_UIAudioManager : MonoBehaviour
{
     [SerializeField] private RPP_LocalAudioManager uiSoundManager;

     //Click Normal
     public void NormalClick()
     {
          uiSoundManager.PlayAudio(1);
     }

     //Click quand le joueur achete un truc
     public void BuyClick()
     {
          uiSoundManager.PlayAudio(0);
     }

     //Click quand le joueur retourne en arrière
     public void ReturnClick()
     {
          uiSoundManager.PlayAudio(2);
     }
}
