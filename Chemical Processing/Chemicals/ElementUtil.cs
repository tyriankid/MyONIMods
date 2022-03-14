using System;
using STRINGS;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
	public static class ElementUtil
	{
		public static void RegisterElementStrings(string elementId, string name, string description)
		{
			string text = elementId.ToUpper();
			Strings.Add(new string[]
			{
				"STRINGS.ELEMENTS." + text + ".NAME",
				UI.FormatAsLink(name, text)
			});
			Strings.Add(new string[]
			{
				"STRINGS.ELEMENTS." + text + ".DESC",
				description
			});
		}

		public static KAnimFile FindAnim(string name)
		{
			KAnimFile kanimFile = Assets.Anims.Find((KAnimFile anim) => anim.name == name);
			if (kanimFile == null)
			{
				Debug.LogError("Failed to find KAnim: " + name);
			}
			return kanimFile;
		}

		public static void AddSubstance(Substance substance)
		{
			Assets.instance.substanceTable.GetList().Add(substance);
		}

		public static Substance CreateSubstance(string name, Element.State state, KAnimFile kanim, Material material, Color32 colour)
		{
			return ModUtil.CreateSubstance(name, state, kanim, material, colour, colour, colour);
		}

		public static Substance CreateRegisteredSubstance(string name, Element.State state, KAnimFile kanim, Material material, Color32 colour)
		{
			Substance substance = ElementUtil.CreateSubstance(name, state, kanim, material, colour);
			SimHashUtil.RegisterSimHash(name);
			ElementUtil.AddSubstance(substance);
			ElementLoader.FindElementByHash(substance.elementID).substance = substance;
			return substance;
		}
	}
}
