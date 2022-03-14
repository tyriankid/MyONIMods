using System;
using KMod;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using HarmonyLib;


namespace TempretureControler
{
    public  class ModLoad :UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(true);
            new POptions().RegisterOptions(this, typeof(TestModSettings));
        }
    }
}
