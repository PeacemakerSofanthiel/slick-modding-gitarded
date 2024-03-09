using HarmonyLib;
using LOR_DiceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace The_Aftermath_Collection
{

    #region - GENERIC ABILITIES -
    //On Clash Win - Add 3 power to the next die
    public class DiceCardAbility_AftermathAdd3PowerNextDie : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            BattlePlayingCardDataInUnitModel bruh = owner.currentDiceAction;
            if (bruh == null)
            {
                return;
            }

            bruh.AddDiceAdder(DiceMatch.NextDice, 3);
        }

        public static string Desc = "[On Clash Win] Add +3 Power to next die";
    }

    //On Clash Lose - Add +3 Power to the last die 
    public class DiceCardAbility_AftermathAdd3PowerLastDie : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            BattlePlayingCardDataInUnitModel bruh = owner.currentDiceAction;
            if (bruh == null)
            {
                return;
            }

            bruh.AddDiceAdder(DiceMatch.LastDice, 3);
        }
        public static string Desc = "[On Clash Lose] Add +3 Power to last die";
    }

    // courtesy of coding wizard uGuardian
    // THIS MUST BE GIVEN TO DICE VIA A CARD ABILITY ON USE!!! OTHERWISE IT FUCKS WITH MASSES
    public class DiceCardAbility_AftermathReduceDmgNatRoll : DiceCardAbilityBase
    {
        public override void BeforeRollDice()
        {
            base.BeforeRollDice();
            owner.bufListDetail.AddBuf(new BattleUnitBuf_DiceDamageReduction(behavior));
        }

        public class BattleUnitBuf_DiceDamageReduction : BattleUnitBuf
        {
            readonly BattleDiceBehavior myBehavior;
            public BattleUnitBuf_DiceDamageReduction(BattleDiceBehavior behavior)
            {
                myBehavior = behavior;
            }

            public override int GetDamageReduction(BattleDiceBehavior behavior)
            {
                return myBehavior?.DiceResultValue ?? base.GetDamageReduction(behavior);
            }
            public override void AfterDiceAction(BattleDiceBehavior behavior)
            {
                Destroy();
            }
        }

        public static string Desc = "[On Clash Lose] Reduce incoming damage by the natural roll";
    }

    // [On Hit] Inflict 1 Spore
    public class DiceCardAbility_AftermathSpore1 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack();

            BattleUnitBuf battleUnitBuf = target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_Spore);
            if (battleUnitBuf != null)
            {
                battleUnitBuf.stack++;
            }

            else
            {
                target.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_Spore(1));
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Aftermath_Spore_Keyword"
                };
            }
        }

        public static string Desc = "[On Hit] Inflict 1 Spore next Scene";
    }

    // [On Hit] Inflict 2 Spore
    public class DiceCardAbility_AftermathSpore2 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack();

            BattleUnitBuf battleUnitBuf = target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_Spore);
            if (battleUnitBuf != null)
            {
                battleUnitBuf.stack += 2;
            }

            else
            {
                target.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_Spore(2));
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                        "Aftermath_Spore_Keyword"
                };
            }
        }

        public static string Desc = "[On Hit] Inflict 2 Spore next Scene";
    }

    // [On Clash Lose] Gain 1 Negative Emotion Coin
    public class DiceCardAbility_AftermathClashLoseGainNegEmotCoin : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative);
            SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, EmotionCoinType.Negative, 1);
        }

        public static string Desc = "[On Clash Lose] Gain 1 Negative Emotion Coin";
    }

    // [On Clash Lose] Draw 1 page
    public class DiceCardAbility_AftermathClashLoseDraw1Page : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            owner.allyCardDetail.DrawCards(1);
        }

        public static string Desc = "[On Clash Lose] Draw 1 page";
    }

    // [On Hit] Trigger target's Burn
    public class DiceCardAbility_AftermathTriggerTargetBurn : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                var burn = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                if (burn != null)
                {
                    burn.OnRoundEndTheLast();
                }
            }
        }

        public static string Desc = "[On Hit] Trigger target's Burn";
    }

    #endregion


    #region - ZWEI WESTERN SECTION 3 -
    //On Clash Lose - Add a copy of 'Justice: Duty' to hand (made for Flexible Stance)
    public class DiceCardAbility_AftermathGiveFunnyPage : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 50105), false);
        }

        public static string Desc = "[On Clash Lose] Add 'Justice: Duty' to user's hand";
        public override string[] Keywords => new string[] { "Duty_description_Keyword", "DrawCard_Keyword" };
    }

    //This die and the dice targetting it cannot be destroyed; [On Hit] Inflict 3 Bleed (made for Justice: Vengeance)
    public class DiceCardAbility_AftermathFunnyPage : DiceCardAbilityBase
    {

        public override bool IsImmuneDestory => true;

        private class DiceCardAbility_noBreak : DiceCardAbilityBase
        {
            public override bool IsImmuneDestory => true;
        }

        public override void BeforeRollDice_Target(BattleDiceBehavior targetDice)
        {
            base.BeforeRollDice_Target(targetDice);
            targetDice.card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_noBreak());
        }

        public override void OnSucceedAttack()
        {
            BattleUnitModel battleUnitModel = behavior.card?.target;
            if (battleUnitModel != null)
            {
                battleUnitModel.bufListDetail.AddKeywordBufByCard(KeywordBuf.Bleeding, 3, base.owner);
            }
        }

        public static string Desc = "This die and the dice targetting it cannot be destroyed; [On Hit] Inflict 3 Bleed";
        public override string[] Keywords => new string[] { "Bleed_Keyword" };
    }

    #endregion

    #region - TIES AND FAMILY -

    // [On Hit] Add 1 random Venom to hand
    public class DiceCardAbility_AftermathAddVenomOnHit : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            VenomCardModel.AddVenomToHand(owner);
        }
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                        "Family_Only_Keyword"
                };
            }
        }

        public static string Desc = "[On Hit] Add 1 random Venom to hand";
    }


    // [On Clash Lose] Activate 'Blade Thesis'
    public class DiceCardAbility_AftermathBladeThesisFirst : DiceCardAbilityBase
    {
        private AudioClip hitBTfirst; 

        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            if (!owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>())
            {
                PassiveAbility_Aftermath_BladeThesis.ActivateBladeThesis(owner);
            }
        }

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            if (target != null)
            {
                if (owner.battleCardResultLog == null)
                {
                    return;
                }
                if (owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>())
                {
                    hitBTfirst = AftermathCollectionInitializer.aftermathMapHandler.GetAudioClip("hitBTfirst.mp3");
                    owner.battleCardResultLog.SetPrintEffectEvent(new BattleCardBehaviourResult.BehaviourEvent(Effect));
                }
            }
        }

        private void Effect()
        {
            CameraFilterUtil.EarthQuake(0.06f, 0.04f, 40f, 0.40f);
            AftermathCollectionInitializer.PlaySound(hitBTfirst, owner.view.transform, 1.75f);
        }

        public static string Desc = "[On Clash Lose] Activate 'Blade Thesis'";
    }


    // [On Hit] Decrease target's Speed to 1 next Scene
    public class DiceCardAbility_AftermathBladeThesisLast : DiceCardAbilityBase
    {
        private AudioClip hitBTlast;

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            if (target != null)
            {
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.DecreaseSpeedTo1, 1, owner);

                if (owner.battleCardResultLog == null)
                {
                    return;
                }
                if (owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>())
                {
                    hitBTlast = AftermathCollectionInitializer.aftermathMapHandler.GetAudioClip("hitBTlast.mp3");
                    behavior.behaviourInCard.EffectRes = "FX_PC_RolRang_Greatsword";
                    owner.battleCardResultLog.SetPrintEffectEvent(new BattleCardBehaviourResult.BehaviourEvent(Effect));
                }
            }
        }

        private void Effect()
        {
            CameraFilterUtil.EarthQuake(0.08f, 0.05f, 50f, 0.50f);
            AftermathCollectionInitializer.PlaySound(hitBTlast, owner.view.transform, 2.15f);
        }

        public static string Desc = "[On Hit] Target's Speed is fixed at 1 next Scene";
    }


    // This die's type is randomized [On Hit] Inflict 2 ??? next Scene
    public class DiceCardAbility_AftermathRandomize2 : DiceCardAbilityBase
    {
        public override void BeforeRollDice()
        {
            base.BeforeRollDice();
            behavior.behaviourInCard.Detail = RandomUtil.SelectOne(BehaviourDetail.Slash, BehaviourDetail.Penetrate, BehaviourDetail.Hit);
        }

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);

            if (target != null)
            {
                List<KeywordBuf> bruh = new List<KeywordBuf>
                    {
                        KeywordBuf.Burn,
                        KeywordBuf.Paralysis,
                        KeywordBuf.Vulnerable,
                        KeywordBuf.Bleeding
                    };

                KeywordBuf funne = RandomUtil.SelectOne(bruh);

                switch (funne)
                {
                    case KeywordBuf.Paralysis:
                        behavior.behaviourInCard.EffectRes = "Bada_Z";
                        break;

                    case KeywordBuf.Bleeding:
                        behavior.behaviourInCard.EffectRes = "Sword_Z";
                        break;

                    case KeywordBuf.Vulnerable:
                        behavior.behaviourInCard.EffectRes = "SevenAssociation_Z";
                        break;

                    case KeywordBuf.Burn:
                        behavior.behaviourInCard.EffectRes = "Liu1_Z";
                        break;

                    default:
                        behavior.behaviourInCard.EffectRes = "Usett_Z";
                        break;
                }

                target.bufListDetail.AddKeywordBufByCard(funne, 2, owner);
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Random_Keyword"
                };
            }
        }

        public static string Desc = "This die's type is randomized [On Hit] Inflict 2 ??? next Scene";
    }

    // [On Hit] Inflict 2 ??? next Scene
    public class DiceCardAbility_AftermathQuestion2Slash : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);

            if (target != null)
            {
                List<KeywordBuf> bruh = new List<KeywordBuf>
                    {
                        KeywordBuf.Burn,
                        KeywordBuf.Paralysis,
                        KeywordBuf.Vulnerable,
                        KeywordBuf.Bleeding
                    };

                KeywordBuf funne = RandomUtil.SelectOne(bruh);

                switch (funne)
                {
                    case KeywordBuf.Paralysis:
                        behavior.behaviourInCard.EffectRes = "Bada_J";
                        break;

                    case KeywordBuf.Bleeding:
                        behavior.behaviourInCard.EffectRes = "Sword_J";
                        break;

                    case KeywordBuf.Vulnerable:
                        behavior.behaviourInCard.EffectRes = "SevenAssociation_J";
                        break;

                    case KeywordBuf.Burn:
                        behavior.behaviourInCard.EffectRes = "Liu1_J";
                        break;

                    default:
                        behavior.behaviourInCard.EffectRes = "Usett_J";
                        break;
                }

                target.bufListDetail.AddKeywordBufByCard(funne, 2, owner);
            }
        }

        public static string Desc = "[On Hit] Inflict 2 ??? next Scene";
    }

    // [On Hit] Inflict 5 Burn to target to target and 1 Burn to self next Scene
    public class DiceCardAbility_AftermathBoom : DiceCardAbilityBase
    {
        private AudioClip boomBurn;

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            boomBurn = AftermathCollectionInitializer.aftermathMapHandler.GetAudioClip("boomBurn.mp3");
            owner.battleCardResultLog.SetPrintEffectEvent(new BattleCardBehaviourResult.BehaviourEvent(Effect));
            target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 5, owner);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 1, owner);
        }

        private void Effect()
        {
            CameraFilterUtil.EarthQuake(0.08f, 0.04f, 55f, 0.50f);
            AftermathCollectionInitializer.PlaySound(boomBurn, owner.view.transform, 1.67f);
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                     "Burn_Keyword"
                };
            }
        }

        public static string Desc = "[On Hit] Inflict 5 Burn to target and 1 Burn to self this Scene";
    }
    #endregion

    #region - LIU REMNANTS -    

    // [On Clash Lose] Inflict 2 Burn to each other
    public class DiceCardAbility_AftermathClashLoseBurn2Both : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            var target = behavior.card.target;
            if (target != null)
            {
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 2, owner);
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 2, owner);
            }

        }

        public static string Desc = "[On Clash Lose] Inflict 2 Burn to each other";
    }

    // [On Hit] Inflict 1 + (Personal Emotion Level) amount of Burn to each other
    public class DiceCardAbility_AftermathMutuallyAssuredIgnitionHunanDie : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                int burn = 1 + owner.emotionDetail.EmotionLevel;
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, burn, owner);
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, burn, owner);
            }
        }

        public static string Desc = "[On Hit] Inflict 1 + (Personal Emotion Level) amount of Burn to each other";
    }

    // [On Clash Lose] Add a copy of ‘Let It All Out’ to hand
    public class DiceCardAbility_AftermathBottleUpDie : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60203));
        }

        public static string Desc = "[On Clash Lose] Add a copy of ‘Let It All Out’ to hand";
    }

    // [On Clash Win] Inflict Burn to target equal to stacks of Inner Flame [On Clash Lose] Inflict Burn to self equal to stacks of Inner Flame
    public class DiceCardAbility_AftermathLetItOutDie : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            BattleUnitBuf battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_InnerFlame);
            if (battleUnitBuf != null)
            {
                owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, battleUnitBuf.stack, owner);
                owner.bufListDetail.RemoveBuf(battleUnitBuf);
                battleUnitBuf.Destroy();
            }
        }

        public override void OnWinParrying()
        {
            BattleUnitBuf battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_InnerFlame);
            BattleUnitModel target = card.target;
            if (battleUnitBuf != null && target != null)
            {
                target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, battleUnitBuf.stack, owner);
                owner.bufListDetail.RemoveBuf(battleUnitBuf);
                battleUnitBuf.Destroy();
            }
        }

        public static string Desc = "[On Clash] Activate Inner Flame and purge all of its stacks";
    }

    // [On Clash Win] Recycle this die (Max. 1); Inflict 2 Burn to target and self
    public class DiceCardAbility_AftermathFlamingBurdenFirst : DiceCardAbilityBase
    {
        bool rolled = false;

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 2, owner);
                target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 2, owner);
            }
        }

        public override void AfterAction()
        {
            if (!rolled)
            {
                rolled = true;
                ActivateBonusAttackDice();
            }
        }

        public static string Desc = "[On Clash Win] Recycle this die (Max. 1); Inflict 2 Burn to target and self";
    }

    // [On Hit] Trigger Burn on target and self
    public class DiceCardAbility_AftermathTriggerTargetAndSelfBurn : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                var burnOwn = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                var burn = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                if (burn != null)
                    burn.OnRoundEndTheLast();
                if (burnOwn != null)
                    burnOwn.OnRoundEndTheLast();
            }
        }

        public static string Desc = "[On Hit] Trigger Burn on target and self";
    }

    // [On Clash Win] Inflict 2 Burn (Personal Emotion Level) times
    public class DiceCardAbility_AftermathInflict2BurnEmotion : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
                for (int i = 0; i < owner.emotionDetail.EmotionLevel; i++)
                    target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 1, owner);
        }


        public static string Desc = "[On Clash Win] Inflict 2 Burn (Personal Emotion Level) times";
    }



    #endregion

    #region - COLOR CHUN - 

    // [On Hit] Inflict 1 Burn X times; lose X stacks of Burn
    public class DiceCardAbility_ColorChunZeroBurn6 : DiceCardAbilityBase
    {

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 1, owner);
                }
                BattleUnitBuf buf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                if (buf.stack >= 6) buf.stack -= 6;
                else buf.Destroy();

            }
        }

        public static string Desc = "[On Hit] Inflict 1 Burn 6 times; lose 6 stacks of Burn";
    }
    public class DiceCardAbility_ColorChunZeroBurn7 : DiceCardAbilityBase
    {

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                for (int i = 0; i < 7; i++)
                {
                    target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 1, owner);
                }
                BattleUnitBuf buf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                if (buf.stack >= 7) buf.stack -= 7;
                else buf.Destroy();

            }
        }

        public static string Desc = "[On Hit] Inflict 1 Burn 7 times; lose 7 stacks of Burn";
    }
    public class DiceCardAbility_ColorChunZeroBurn9 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                for (int i = 0; i < 9; i++)
                {
                    target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 1, owner);
                }
                BattleUnitBuf buf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                if (buf.stack >= 9) buf.stack -= 9;
                else buf.Destroy();

            }
        }

        public static string Desc = "[On Hit] Inflict 1 Burn 9 times; lose 9 stacks of Burn";
    }

    // ! description only !
    // [On Clash] Recover Light equal to stacks of Burn purged / 7 (rounded down)
    // [Unattacked] Draw pages equal to stacks of Burn purged / 7 (rounded down)
    public class DiceCardAbility_ColorChunEconomyOne : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash] Recover Light equal to stacks of Burn purged / 6 (rounded down)";
    }
    public class DiceCardAbility_ColorChunEconomyTwo : DiceCardAbilityBase
    {
        public static string Desc = "[Unattacked] Draw pages equal to stacks of Burn purged / 6 (rounded down)";
    }

    #endregion

    #region - RETURN OF THE INDEX -

    [UnusedAbility]
    public class DiceCardAbility_Aftermath_TwoHasteBind : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] If user has 2+ Haste, inflict 2 Bind next Scene";

        public override void OnSucceedAttack()
        {
            BattleUnitBuf_quickness battleUnitBuf_quickness = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness) as BattleUnitBuf_quickness;
            if (battleUnitBuf_quickness != null && battleUnitBuf_quickness.stack >= 2)
            {
                BattleUnitModel target = base.card.target;
                if (target == null)
                {
                    return;
                }
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 2, owner);
            }
        }
    }

    [UnusedAbility]
    public class DiceCardAbility_Aftermath_FourHasteFragile : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] If user has 4+ Haste, inflict 2 Fragile next Scene";

        public override void OnSucceedAttack()
        {
            BattleUnitBuf_quickness battleUnitBuf_quickness = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness) as BattleUnitBuf_quickness;
            if (battleUnitBuf_quickness != null && battleUnitBuf_quickness.stack >= 4)
            {
                BattleUnitModel target = base.card.target;
                if (target == null)
                {
                    return;
                }
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Vulnerable, 2, base.owner);
            }
        }
    }

    [UnusedAbility]
    public class DiceCardAbility_Aftermath_SixHasteErode : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] If user has 6+ Haste, inflict 2 Erosion this Scene";

        public override void OnSucceedAttack()
        {
            BattleUnitBuf_quickness battleUnitBuf_quickness = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness) as BattleUnitBuf_quickness;
            if (battleUnitBuf_quickness != null && battleUnitBuf_quickness.stack >= 6)
            {
                BattleUnitModel target = base.card.target;
                if (target == null)
                {
                    return;
                }
                target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Decay, 2, base.owner);
                BattleUnitBuf_Decay battleUnitBuf_Decay = target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) as BattleUnitBuf_Decay;
                if (battleUnitBuf_Decay != null)
                {
                    battleUnitBuf_Decay.ChangeToYanDecay();
                }
            }
        }
    }

    public class DiceCardAbility_Aftermath_hasterepeat3 : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Spend 1 Haste to reroll this die (Max. 3)";

        private int count;

        public override void OnSucceedAttack()
        {
            if (this.count >= 3)
            {
                return;
            }

            BattleUnitBuf_quickness battleUnitBuf_quickness = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness) as BattleUnitBuf_quickness;
            if (battleUnitBuf_quickness != null && battleUnitBuf_quickness.stack >= 1)
            {
                base.ActivateBonusAttackDice();
                this.count++;
                battleUnitBuf_quickness.stack -= 1;
                base.owner.UnitData.historyInUnit.trashDisposalReuse++;
            }
        }


    }

    public class DiceCardAbility_Aftermath_EvanHit : DiceCardAbilityBase
    {
        public override void BeforeGiveDamage()
        {
            this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                dmgRate = Mathf.Min(5, Mathf.RoundToInt(((float)base.owner.MaxHp - base.owner.hp) / 10f)),
            });
        }
    }

    public class DiceCardAbility_Aftermath_EvanSpecialDie : DiceCardAbilityBase
    {
        public static string Desc = "Reduce target's current Offensive die to the minimum value";
        public override void BeforeRollDice()
        {
            if (this.behavior.IsParrying() && base.IsAttackDice(this.behavior.TargetDice.Detail))
            {
                this.behavior.TargetDice.AddAbility(new DiceCardAbility_Aftermath_RollZero());
            }
        }
    }

    public class DiceCardAbility_Aftermath_RollZero : DiceCardAbilityBase
    {
        public override bool Invalidity
        {
            get
            {
                return true;
            }
        }
    }

    public class DiceCardAbility_Aftermath_Spend2HasteFor1Erosion : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            var buf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
            if (buf != null)
            {
                if (buf.stack >= 2 && owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null)
                {
                    buf.stack -= 2;
                    target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
                }
            }
        }

        public static string Desc = "[On Hit] If in the 'Blade Unlocked' state, spend 2 Haste to inflict 1 Erosion next Scene";
    }

    public class DiceCardAbility_Aftermath_SubmissionWillLastDice : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            var buf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
            var buf2 = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
            if (buf != null && buf2 != null)
            {
                if (buf2.stack >= 2)
                {
                    buf2.stack -= 2;
                    for (int i = 0; i < buf.stack; i++)
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, owner);
                }
            }
        }

        public static string Desc = "[On Hit] Spend 2 Haste to gain 1 Haste next scene (stacks of Erosion on target) times";
    }

    public class DiceCardAbility_Aftermath_OpheliaPUNCHHIMUNTILHEEXPLODES : DiceCardAbilityBase
    {
        int count;

        public static string Desc = "[On Hit] Spend 1 Haste to recycle this die; if this die has been recycled at least once, inflict 1 Erosion next Scene";

        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            var buf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
            if (buf != null)
            {
                if (buf.stack > 0)
                {
                    buf.stack--;
                    count++;
                    ActivateBonusAttackDice();
                    if (count > 1)
                    {
                        var target = behavior.card.target;
                        if (target != null)
                        {
                            target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
                        }
                    }
                } else
                {
                    buf.Destroy();
                }
            }
        }
    }

    public class DiceCardAbility_Aftermath_LevigationSecondDie : DiceCardAbilityBase
    {
        bool freebie;

        public override void AfterAction()
        {
            base.AfterAction();
            var buf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
            if (buf.stack > 0 || freebie)
            {
                ActivateBonusAttackDice();
                if (freebie)
                {
                    freebie = false;
                }
                else
                {
                    buf.stack--;
                    freebie = true;
                }
            }
        }

        public static string Desc = "[On Hit] Spend 1 Haste to roll this die twice";
    }
    
    #endregion

    #region - NORINCO WORKSHOP -

    public class DiceCardAbility_Aftermath_NorincoShank : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            var target = behavior.card.target;
            if (target != null) target.TakeDamage(2, DamageType.Card_Ability, owner);
        }

        public override void OnLoseParrying()
        {
            owner.breakDetail.TakeBreakDamage(2, DamageType.Card_Ability, owner);
            behavior.card.card.exhaust = true;
        }

        public static string Desc = "[On Clash Win] Deal 2 damage to target [On Clash Lose] Deal 2 stagger damage to self; exhaust this page";
    }

    public class DiceCardAbility_Aftermath_ExhaustRandomOnClashLose : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            var card = owner.allyCardDetail.GetHand().SelectOneRandom();
            if (card != null) card.exhaust = true;
        }

        public static string Desc = "[On Clash Lose] Exhaust a random page in hand";
    }

    public class DiceCardAbility_Aftermath_RemoveDieOnClashLose : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            List<DiceBehaviour> list = new List<DiceBehaviour>();
            foreach (DiceBehaviour diceBehaviour in this.card.card.XmlData.DiceBehaviourList.FindAll(x => x != behavior.behaviourInCard))
            {
                DiceBehaviour diceBehaviour2 = diceBehaviour.Copy();
                list.Add(diceBehaviour2);
            }
            this.card.card.XmlData.DiceBehaviourList = list;
        }

        public static string Desc = "[On Clash Lose] Remove this die for the rest of the Act";
    }

    public class DiceCardAbility_Aftermath_Take1DmgClashWin : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            owner.TakeDamage(1, DamageType.Card_Ability, owner);
        }

        public static string Desc = "[On Clash Win] Deal 1 damage to self";
    }

    public class DiceCardAbility_Aftermath_ExhaustSelfOnClashLose : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            var card = behavior.card;
            if (card != null) card.card.exhaust = true;
        }

        public static string Desc = "[On Clash Lose] Exhaust this page";
    }

    public class DiceCardAbility_Aftermath_DmgAtkHalfStagger : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            if (target != null) target.TakeDamage(owner.breakDetail.breakGauge / 2, DamageType.Card_Ability, owner);
        }

        public static string Desc = "[On Hit] Deal damage equal to half of user’s current Stagger Resist";
    }

    #endregion

    #region - C.B.L. I -

    public class DiceCardAbility_Aftermath_Thunderstruck1atk : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            var buf = target.bufListDetail.GetReadyBufList().Find(x => x is BattleUnitBuf_Aftermath_Thunderstruck);
            if (buf == null) target.bufListDetail.AddReadyBuf(new BattleUnitBuf_Aftermath_Thunderstruck() { stack = 1 });      
            else buf.stack += 1;
        }

        public override string[] Keywords => new string[] { "Aftermath_Thunderstruck_Keyword" }; 

        public static string Desc = "[On Hit] Inflict 1 Thunderstruck next Scene";
    }

    public class DiceCardAbility_Aftermath_Thunderstruck2atk : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            var buf = target.bufListDetail.GetReadyBufList().Find(x => x is BattleUnitBuf_Aftermath_Thunderstruck);
            if (buf == null) target.bufListDetail.AddReadyBuf(new BattleUnitBuf_Aftermath_Thunderstruck() { stack = 2 });
            else buf.stack += 2;
        }

        public override string[] Keywords => new string[] { "Aftermath_Thunderstruck_Keyword" };

        public static string Desc = "[On Hit] Inflict 2 Thunderstruck next Scene";
    }

    public class DiceCardAbility_Aftermath_StoredChems1atk : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_StoredChems);
            if (buf == null) owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_StoredChems() { stack = 1});
            else buf.stack += 1;
        }

        public override string[] Keywords => new string[] { "Aftermath_StoredChems" };

        public static string Desc = "[On Hit] Gain 1 Stored Chems";
    }

    public class DiceCardAbility_Aftermath_StoredChems1pwBoost3NextDie : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();       
            var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_StoredChems);
            if (buf == null) owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_StoredChems() { stack = 1 });
            else buf.stack += 1;
            behavior.card.ApplyDiceStatBonus(DiceMatch.NextDice, new DiceStatBonus { max = 3 });
        }

        public override string[] Keywords => new string[] { "Aftermath_StoredChems" };

        public static string Desc = "[On Clash Win] Gain 1 Stored Chems; boost next die's max value by +3";
    }

    public class DiceCardAbility_Aftermath_Purge1Hasteplose : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            var buf = owner.bufListDetail.GetReadyBuf(KeywordBuf.Quickness);
            if (buf != null)
            {
                if (buf.stack <= 1) buf.Destroy();
                else buf.stack -= 1;
            }
        }

        public static string Desc = "[On Clash Lose] Purge 1 Haste next Scene";
    }

    public class DiceCardAbility_Aftermath_Purge2Hasteplose : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            var buf = owner.bufListDetail.GetReadyBuf(KeywordBuf.Quickness);
            if (buf != null)
            {
                if (buf.stack <= 2) buf.Destroy();
                else buf.stack -= 2;
            }
        }

        public static string Desc = "[On Clash Lose] Purge 2 Haste next Scene";
    }

    #endregion

    #region - MOBIUS OFFICE I -

    public class DiceCardAbility_Aftermath_Overcharge2Para : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
            {
                BattleUnitModel target = base.card.target;
                if (target == null)
                {
                    return;
                }
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 2, base.owner);
            }
        }

        public override string[] Keywords => new string[] { "Paralysis_Keyword" };

        public static string Desc = "[On Clash Win] If user has Overcharge, inflict 2 Paralysis";
    }

    public class DiceCardAbility_Aftermath_winenergy1 : DiceCardAbilityBase
    {
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Energy_Keyword"
                };
            }
        }

        public override void OnWinParrying()
        {
            base.owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
        public static string Desc = "[On Clash Win] Restore 1 Light";
    }

    public class DiceCardAbility_Aftermath_para1Overcharge : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
            {
                BattleUnitModel target = base.card.target;
                if (target == null)
                {
                    return;
                }
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 1, base.owner);
            }
        }
        public static string Desc = "[On Hit] If the user has Overcharge, inflict 1 Paralysis";
    }

    public class DiceCardAbility_Aftermath_wincharge3 : DiceCardAbilityBase
    {
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "WarpCharge"
                };
            }
        }

        public override void OnWinParrying()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 3, null);
        }
        public static string Desc = "[On Clash Win] Gain 3 Charge";
    }

    public class DiceCardAbility_Aftermath_SpendChargeRecycle : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            if (this.count >= 3)
            {
                return;
            }
            BattleUnitBuf_warpCharge battleUnitBuf_warpCharge = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;
            if (battleUnitBuf_warpCharge != null && battleUnitBuf_warpCharge.stack >= 3)
            {
                battleUnitBuf_warpCharge.UseStack(3, true);
                base.ActivateBonusAttackDice();
                this.count++;
            }
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
            {
                BattleUnitModel target = base.card.target;
                if (target == null)
                {
                    return;
                }
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 2, base.owner);
            }
        }

        private int count;
        public static string Desc = "[On Hit] Spend 3 Charge to recycle this die (Up to 3 times). If user has Overcharge, inflict 2 Paralysis";
    }

    public class DiceCardAbility_Aftermath_RemoveIfNoCharge : DiceCardAbilityBase
    {
        public static string Desc = "If user does not have Overcharge, destroy this dice";
    }

    public class DiceCardAbility_Aftermath_aceCardPara : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            BattleUnitModel target = base.card.target;
            if (target == null)
            {
                return;
            }
            target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 5, base.owner);
        }
        public static string Desc = "[On Hit] Inflict 5 Paralysis next Scene";
    }

    public class DiceCardAbility_Aftermath_lanceCardRecycle : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            BattleUnitBuf_warpCharge battleUnitBuf_warpCharge = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;
            if (battleUnitBuf_warpCharge != null && battleUnitBuf_warpCharge.stack >= 4)
            {
                battleUnitBuf_warpCharge.UseStack(4, true);
                base.ActivateBonusAttackDice();
            }
        }
        public override void OnLoseParrying()
        {
            BattleUnitModel target = base.card.target;
            if (target == null)
            {
                return;
            }
            target.TakeDamage(8, DamageType.Card_Ability, base.owner, KeywordBuf.None);

            base.owner.TakeDamage(8, DamageType.Card_Ability, base.owner, KeywordBuf.None);
        }
        public static string Desc = "[On Hit] Spend 4 Charge to recycle this die [On Clash Lose] Deal 8 damaget to self and target";
    }

    public class DiceCardAbility_Aftermath_powerDown2 : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            BattleUnitModel target = base.card.target;
            if (target == null)
            {
                return;
            }
            BattlePlayingCardDataInUnitModel currentDiceAction = target.currentDiceAction;
            if (currentDiceAction == null)
            {
                return;
            }
            currentDiceAction.AddDiceAdder(DiceMatch.NextDice, -2);
        }
        public static string Desc = "[On Clash Win] Reduce Power of opponent’s next die by 2";
    }

    #endregion

    #region - THE LIME LIME -

    public class DiceCardAbility_Aftermath_Erosion1AtkBoth : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            target?.bufListDetail?.AddKeywordBufThisRoundByCard(KeywordBuf.Decay, 1, owner);
            owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Decay, 1, owner);
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "[On Hit] Inflict 1 Erosion to self and target this Scene";
    }

    public class DiceCardAbility_Aftermath_Erosion2AtkBoth : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            target?.bufListDetail?.AddKeywordBufThisRoundByCard(KeywordBuf.Decay, 2, owner);
            owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Decay, 2, owner);
        }

        public override string[] Keywords => new string[] {"Aftermath_Decay_Keyword"};

        public static string Desc = "[On Hit] Inflict 2 Erosion to self and target this Scene";
    }

    public class DiceCardAbility_Aftermath_Inflict2BasedClashLose : DiceCardAbilityBase
    {
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            var target = behavior.card.target;
            if (target != null)
            {
                var buf = target.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Based);
                if (buf == null)
                    target.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_Based() { stack = 2 });
                else
                    buf.stack += 2;
            }
        }

        public override string[] Keywords => new string[] { "Aftermath_Basic" };

        public static string Desc = "[On Clash Lose] Give 2 Based to target";
    }

    public class DiceCardAbility_Aftermath_StealBasedFromTarget : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            if (target != null)
            {
                var buf = target.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Based);
                if (buf == null)
                    return;
                else
                {
                    var buf2 = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Based);
                    if (buf2 == null)
                        owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_Based() { stack = buf.stack });
                    else
                        buf2.stack += buf.stack;
                    buf.Destroy();
                }
            }
        }

        public override string[] Keywords => new string[] { "Aftermath_Basic" };

        public static string Desc = "[On Hit] Purge Based from target; gain Based equal to stacks purged";
    }

    public class DiceCardAbility_Aftermath_Gain1StaggerProtectionOnHit : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.BreakProtection, 1, owner);
        }

        public override string[] Keywords => new string[] { "BreakProtection_Keyword" };

        public static string Desc = "[On Hit] Gain 1 Stagger Protection next Scene";
    }

    public class DiceCardAbility_Aftermath_Gain1StaggerProtectionOnClash : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.BreakProtection, 1, owner);
        }

        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.BreakProtection, 1, owner);
        }

        public override void OnDrawParrying()
        {
            base.OnDrawParrying();
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.BreakProtection, 1, owner);
        }

        public override string[] Keywords => new string[] { "BreakProtection_Keyword" };

        public static string Desc = "[On Clash] Gain 1 Stagger Protection next Scene";
    }

    public class DiceCardAbility_Aftermath_MinMaxAgainstErosion : DiceCardAbilityBase
    {
        public override void BeforeRollDice()
        {
            base.BeforeRollDice();
            if (behavior.card.target != null && behavior.card.target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) != null)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus { min = 1, max = 1 });
            }
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "If target has Erosion, boost this die's min and max values by +1";
    }

    public class DiceCardAbility_Aftermath_Inflict1Decay3Atk : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "[On Hit] Inflict 1 Erosion 3 times";
    }

    public class DiceCardAbility_Aftermath_Inflict1Decay2Clash : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            var target = behavior.card.target;
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
        }

        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            var target = behavior.card.target;
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
        }

        public override void OnDrawParrying()
        {
            base.OnDrawParrying();
            var target = behavior.card.target;
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "[On Clash] Inflict 1 Erosion 2 times";
    }

    public class DiceCardAbility_Aftermath_AcidRainFirst : DiceCardAbilityBase
    {
        public override void BeforeRollDice()
        {
            base.BeforeRollDice();
            var die = behavior.TargetDice;
            if (die != null && die.Type == BehaviourType.Def)
                die.ApplyDiceStatBonus(new DiceStatBonus { power = -2 });
        }

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner);
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "Reduce Power of target's current Defensive die by 2 [On Hit] Inflict 1 Erosion";
    }

    public class DiceCardAbility_Aftermath_AcidRainSecond : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            if (target != null && target.bufListDetail.GetKewordBufAllStack(KeywordBuf.Decay) >= 4)
            {
                BattleObjectManager.instance.GetAliveList(target.faction).ForEach(x => { x.bufListDetail.AddKeywordBufByCard(KeywordBuf.Decay, 1, owner); });
            }
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "[On Hit] If target has 4 or more Erosion, inflict 1 Erosion to all enemies";
    }

    #endregion

    #region - B.B.'S FIGHT CLUB -

    public class BrawlDiceAbilityBase : DiceCardAbilityBase
    {
        protected virtual string BasePackageId => AftermathCollectionInitializer.packageId;

        protected virtual int BaseId => 0;

        public bool IsCopied()
        {
            if (behavior.card.card.GetID() == new LorId(this.BasePackageId, this.BaseId))
                return true;
            return false;
        }
    }

    public class DiceCardAbility_Aftermath_BrawlThunder2Atk3 : BrawlDiceAbilityBase
    {
        protected override int BaseId => 40103;

        public override void OnSucceedAttack()
        {
            if (this.IsCopied() && behavior.card.target != null)
            {
                behavior.card.target.bufListDetail.AddBuf<BattleUnitBuf_Aftermath_Thunderstruck>(2);
            }
        }

        public static string Desc = "[On Hit] If this die is not rolled within its original page, inflict 2 Thunderstruck next Scene";
    }

    public class DiceCardAbility_Aftermath_BrawlTake3Dmg : BrawlDiceAbilityBase
    {
        protected override int BaseId => 40103;

        public override void OnSucceedAttack()
        {
            if (this.IsCopied())
                owner.TakeDamage(3, DamageType.Card_Ability, owner);
        }

        public static string Desc = "[On Hit] If this die is not rolled within its original page, take 3 damage";
    }

    #endregion

}