```cs
using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using UnityEngine;
using Pixelplacement;
using HarmonyLib;
using InscryptionAPI;
using InscryptionAPI.Ascension;
using InscryptionAPI.Boons;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Guid;
using InscryptionAPI.Helpers;
using InscryptionAPI.Regions;
using InscryptionAPI.Saves;
using InscryptionCommunityPatch;
using DiskCardGame;

namespace FiveNightsatInscryption.abilities
{
	public class AftonAbility : DrawCreatedCard
    {
		public override Ability Ability
		{
			get
			{
				return AftonAbility.ability;
			}
		}
		protected override CardInfo CardToDraw
		{
			get
			{
				bool flag = base.Card.Info.iceCubeParams.creatureWithin != null;
				if (flag)
				{
					CardInfo cardByName = base.Card.Info.iceCubeParams.creatureWithin;
					cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(new Ability[]
					{
					this.Ability
					}));
					return cardByName;
				}
                else
				{
					CardInfo cardByName = CardLoader.GetCardByName("Squirrel");
					cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(new Ability[]
					{
					this.Ability
					}));
					return cardByName;
				}
			}
		}
		public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
		{
			return true;
		}

		public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
		{
			bool flag = base.Card.Info.iceCubeParams.creatureWithin != null;
			if (flag)
			{
				CustomCoroutine.Instance.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("Seems like Afton got away again!", 2.5f, 0f, Emotion.Laughter,
					TextDisplayer.LetterAnimation.Jitter, DialogueEvent.Speaker.Single, null));
			}
            else
            {
				CustomCoroutine.Instance.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("I Wonder how Incarnation works?", 2.5f, 0f, Emotion.Anger,
					TextDisplayer.LetterAnimation.WavyJitter, DialogueEvent.Speaker.Single, null));
			}
				Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
			yield return base.PreSuccessfulTriggerSequence();
			yield return base.CreateDrawnCard();
			yield return base.LearnAbility(0f);
			yield break;
		}
		public static Ability ability;
	}
}
```
