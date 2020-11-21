using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class PlayerAPI : MonoBehaviour
{
   private int slotocoins;
   public string filepath;

   void OnEnable() {
       StreamReader inpStm = new StreamReader(filepath);
       string inpLn = inpStm.ReadLine();
       slotocoins = Int32.Parse(inpLn);
   }

   void OnDisable() {
       string[] lines = {slotocoins.ToString()};
       System.IO.File.WriteAllLines(filepath,lines);
   }

    public void AddSlotocoins(int amount) {
        slotocoins += amount;
    }

    public void SpendSlotocoins(int amount) {
        slotocoins -= amount;
    }

    public int GetSlotocoins() {
        return slotocoins;
    }
}
