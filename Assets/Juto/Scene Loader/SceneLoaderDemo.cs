using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*
Author: Julius Bendt
Created: 11/16/2018 7:51:47 PM
Project name: Juto Standard Asset
Company: Juto Studio
Unity Version: 2018.1.0f2


This script is under the CC0 license.
https://creativecommons.org/publicdomain/zero/1.0/

Credit isn't needed, but I would greatly appreciate it.
Give me credit as following:

"Using assets by Julius bendt,
https://www.juto.dk"

*/

namespace Juto.demo
{
    
    public class SceneLoaderDemo : MonoBehaviour 
	{

        #region Public Fields

        public Color[] colors; //could also be sprites, ect

        public string[] hints;

        public float secPrHint = 5;

        public Image spriteImage;
        public TextMeshProUGUI hint;

		#endregion
		
		#region Private Fields


        private void Start()
        {
            StartCoroutine(Change());
        }

        private IEnumerator Change()
        {
            while(true)
            {
                hint.text = hints[Random.Range(0, hints.Length)];
                spriteImage.color = colors[Random.Range(0, colors.Length)];
                yield return new WaitForSeconds(secPrHint);   
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        #endregion


        #region Public Functions

		
		#endregion
		
		#region Private Functions
		#endregion
	}
}
