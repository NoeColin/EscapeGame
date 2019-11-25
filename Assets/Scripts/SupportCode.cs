﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SupportCode : MonoBehaviour {

    private bool isCodeRight = false;

    private bool changeLoaded = false;
    	
    public void UpdateCodeBool()
    {
        isCodeRight = true;
        
        foreach (var child in gameObject.GetComponentsInChildren<TabletteSupport>())
        {
            if (!child.isValueCorrect)
                isCodeRight = false;                
        }

        CheckCode();
    }
    
    private void CheckCode()
    {

        if (isCodeRight && !changeLoaded)
        {
            this.UnLockNextLevelTrophee();
            StartCoroutine("ChangeScene", "Menu");
        }
           
    }

    private void UnLockNextLevelTrophee()
    {
        Scene scene = SceneManager.GetActiveScene();
        Trophee trophee = TropheesManager.Trophees.Find(t => t.Level == scene.name);
        Trophee nextLevelTrophee = TropheesManager.UnlockedTrophees.Find(t => t.LevelNumber == trophee.LevelNumber + 1);
        nextLevelTrophee.IsLocked = false;
    }

    IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSeconds(1);
        changeLoaded = true;

        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        yield return null;
    }
}
