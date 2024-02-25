
using HyperCard;
using LOR_BattleUnit_UI;
using LOR_DiceSystem;
using Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace The_Aftermath_Collection
{

    #region - GENERAL STUFF -


    #endregion


    #region - ZWEI WESTERN SECTION 3 -

    //Decisive Swordsmanship (Liam)
    public class PassiveAbility_Aftermath_DecisiveSwordmanship : PassiveAbilityBase
    {
        private bool activated;

        public override void OnStartBattle()
        {
            base.OnStartBattle();
            activated = false;
        }
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.GetOriginalDiceBehaviorList().Count == 1 && curCard.GetOriginalDiceBehaviorList().FindAll((DiceBehaviour x) => x.Detail == BehaviourDetail.Slash).Count == 1)
            {
                BattleCardTotalResult battleCardResultLog = this.owner.battleCardResultLog;
                if (battleCardResultLog != null)
                {
                    battleCardResultLog.SetPassiveAbility(this);
                }
                curCard.emotionMultiplier = 2;
                curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = 1
                });

                if (!activated)
                {
                    curCard.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_oswald_destroy1dice());
                    activated = true;
                }
            }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Slash)
            {
                this.owner.ShowPassiveTypo(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
        }
    }

    //Iron Fist (Daniel)
    public class PassiveAbility_Aftermath_IronFist : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Hit)
            {
                this.owner.ShowPassiveTypo(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
        }

        public override void OnRoundStart()
        {
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList_random(base.owner.faction, 2))
            {
                battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Protection, 1, base.owner);
                battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.BreakProtection, 1, base.owner);
            }
        }

        public override int GetDamageReductionAll()
        {
            return 1;
        }

        public override int GetBreakDamageReductionAll(int dmg, DamageType dmgType, BattleUnitModel attacker)
        {
            return 1;
        }
    }

    //Butcher Tendencies (Tamora)
    public class PassiveAbility_Aftermath_23Butcher : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            owner.breakDetail.RecoverBreak(1);
            owner.RecoverHP(1);
        }

        public override void OnRoundStart()
        {
            this.owner.RecoverHP(2);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Penetrate)
            {
                this.owner.ShowPassiveTypo(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
        }
    }

    #endregion

    #region - TIES & FAMILY -

    // Non-Combatant (Benito)
    /* Untransferable. Speed Dice Slot +2. This character is invincible and untargetable,
     * and all pages this character uses gain "[On Use] Gain 1 Emotion Point correspondent to the target's team".                                          
     * Cannot use Combat Pages with any dice, this character's Speed is fixed at 1 and if there are no allies left alive, this character dies at the end of the Scene.
    */
    public class PassiveAbility_Aftermath_NoCombat : PassiveAbilityBase
    {
        public override bool isTargetable => false;

        public override bool isImmortal => true;

        // Benito's AI
        public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
        {
            List<LorId> useOnAlly = new List<LorId>
            {
                new LorId(AftermathCollectionInitializer.packageId, 60105),
                new LorId(AftermathCollectionInitializer.packageId, 60120),
                new LorId(AftermathCollectionInitializer.packageId, 60121),
                new LorId(AftermathCollectionInitializer.packageId, 60122),
                new LorId(AftermathCollectionInitializer.packageId, 60130),
                new LorId(AftermathCollectionInitializer.packageId, 60131)
            };

            List<LorId> useOnEnemy = new List<LorId>
            {
                new LorId(AftermathCollectionInitializer.packageId, 60118),
                new LorId(AftermathCollectionInitializer.packageId, 60119),
                new LorId(AftermathCollectionInitializer.packageId, 60123)
            };

            if (useOnAlly.Contains(card.GetID()))
            {
                List<BattleUnitModel> bruh = BattleObjectManager.instance.GetAliveList(owner.faction);
                bruh.Remove(owner);

                if (card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 60130))
                {
                    List<int> what = new List<int>();
                    foreach (BattleUnitModel model in bruh)
                    {
                        int what2 = 0;
                        foreach (BattleUnitBuf why in model.bufListDetail.GetActivatedBufList().FindAll(x => x.positiveType == BufPositiveType.Negative))
                        {
                            what2 += why.stack;
                        }

                        what.Add(what2);
                    }
                    return bruh[what.IndexOf(what.Max())];
                }

                return RandomUtil.SelectOne(bruh);
            }

            else if (useOnEnemy.Contains(card.GetID()))
            {
                List<BattleUnitModel> bruh = BattleObjectManager.instance.GetAliveList_opponent(owner.faction);
                return RandomUtil.SelectOne(bruh);
            }
            return base.ChangeAttackTarget(card, idx);
        }

        public override bool OnBreakGageZero()
        {
            return true;
        }

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override bool TeamKill()
        {
            return true;
        }

        public override int GetSpeedDiceAdder(int speedDiceResult)
        {
            return -99;
        }

        public override void OnAfterRollSpeedDice()
        {
            base.OnAfterRollSpeedDice();
            foreach (SpeedDice speedDice in owner.speedDiceResult)
            {
                speedDice.value = 1;
            }
        } //this shit is here literally just to counter stuff like The Strongest

        public override void OnRoundEndTheLast()
        {
            base.OnRoundEndTheLast();
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_NoCombat>())
            {
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_NoCombat());
            }

            if (BattleObjectManager.instance.GetAliveList(owner.faction).FindAll(x => !x.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_NoCombat>()).Count == 0)
            { //if the number of allies without this passive reaches 0, die
                owner.Die();
            }
        }

        public override void OnRoundEnd()
        {
            foreach (BattlePlayingCardDataInUnitModel card in owner.cardSlotDetail.cardQueue)
            {
                if (card.target.faction != owner.faction)
                {
                    owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative);
                    SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, EmotionCoinType.Negative, 1);
                }

                else if (card.target.faction == owner.faction)
                {
                    owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Positive);
                    SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, EmotionCoinType.Positive, 1);
                }

                else
                {
                    EmotionCoinType bruh = RandomUtil.SelectOne(EmotionCoinType.Negative, EmotionCoinType.Positive);
                    owner.emotionDetail.CreateEmotionCoin(bruh);
                    SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, bruh, 1);
                }
            }
            owner.allyCardDetail.DiscardInHand(owner.allyCardDetail.GetHand().Count);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (curCard.card.GetSpec().Ranged == CardRange.Instance)
            {
                BattleUnitModel target = curCard.target;
                if (target != null)
                {
                    if (target.faction != owner.faction)
                    {
                        owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative);
                        SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, EmotionCoinType.Negative, 1);
                    }

                    else if (target.faction == owner.faction)
                    {
                        owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Positive);
                        SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, EmotionCoinType.Positive, 1);
                    }

                    else
                    {
                        EmotionCoinType bruh = RandomUtil.SelectOne(EmotionCoinType.Negative, EmotionCoinType.Positive);
                        owner.emotionDetail.CreateEmotionCoin(bruh);
                        SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, bruh, 1);
                    }
                }
            }
        }

        public override void OnRoundStart() 
        {
            owner.allyCardDetail.DrawCards(4 + owner.emotionDetail.EmotionLevel);
            owner.cardSlotDetail.RecoverPlayPoint(3);
            if (owner.faction == Faction.Enemy) // makes Benito use his On Plays
            {
                foreach (var card in owner.allyCardDetail.GetHand().FindAll(x => x.GetSpec().Ranged == CardRange.Instance))
                {
                    BattleUnitModel target = null;
                    if (card.GetID().id == 60105 || card.GetID().id == 60121) {
                        var list = BattleObjectManager.instance.GetAliveList(owner.faction);
                        list.Sort((BattleUnitModel x, BattleUnitModel y) => x.PlayPoint - y.PlayPoint);
                        if (list.Any())
                            target = list[0];
                    } else if (card.GetID().id == 60117) {
                        var list = BattleObjectManager.instance.GetAliveList(owner.faction);
                        list.Sort((BattleUnitModel x, BattleUnitModel y) => x.allyCardDetail.GetHand().Count - y.allyCardDetail.GetHand().Count);
                        if (list.Any())
                            target = list[0];
                    }

                    if (target != null) owner.cardSlotDetail.AddCard(card, target, 0);
                }
            }
        }

        public override void OnWaveStart()
        {
            base.OnWaveStart();
            owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_NoCombat());
        }

        public override void OnStartBattle()
        {
            base.OnStartBattle();
            foreach (var card in owner.cardSlotDetail.cardAry)
            {
                if (card != null)
                {
                    if (card.GetOriginalDiceBehaviorList().Count > 0)
                    {
                        card.DestroyPlayingCard();
                    }
                }
            }
            List<BattleUnitModel> actionableEnemyList = Singleton<StageController>.Instance.GetActionableEnemyList();
            if (owner.faction == Faction.Player)
            {
                for (int i = 0; i < actionableEnemyList.Count; i++)
                {
                    BattleUnitModel battleUnitModel = actionableEnemyList[i];
                    if (battleUnitModel.turnState != BattleUnitTurnState.BREAK)
                    {
                        battleUnitModel.turnState = BattleUnitTurnState.WAIT_CARD;
                    }
                    try
                    {
                        for (int j = 0; j < battleUnitModel.speedDiceResult.Count; j++)
                        {
                            if (!battleUnitModel.speedDiceResult[j].breaked && j < battleUnitModel.cardSlotDetail.cardAry.Count)
                            {
                                BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel = battleUnitModel.cardSlotDetail.cardAry[j];
                                if (battlePlayingCardDataInUnitModel != null && battlePlayingCardDataInUnitModel.card != null)
                                {
                                    if (battlePlayingCardDataInUnitModel.card.GetSpec().Ranged == CardRange.FarArea || battlePlayingCardDataInUnitModel.card.GetSpec().Ranged == CardRange.FarAreaEach)
                                    {
                                        List<BattlePlayingCardDataInUnitModel.SubTarget> subTargets = battlePlayingCardDataInUnitModel.subTargets;
                                        if (subTargets.Exists((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == owner))
                                        {
                                            subTargets.RemoveAll((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == owner);
                                        }
                                        else if (battlePlayingCardDataInUnitModel.target == owner)
                                        {
                                            if (battlePlayingCardDataInUnitModel.subTargets.Count > 0)
                                            {
                                                BattlePlayingCardDataInUnitModel.SubTarget subTarget = RandomUtil.SelectOne<BattlePlayingCardDataInUnitModel.SubTarget>(battlePlayingCardDataInUnitModel.subTargets);
                                                battlePlayingCardDataInUnitModel.target = subTarget.target;
                                                battlePlayingCardDataInUnitModel.targetSlotOrder = subTarget.targetSlotOrder;
                                                battlePlayingCardDataInUnitModel.earlyTarget = subTarget.target;
                                                battlePlayingCardDataInUnitModel.earlyTargetOrder = subTarget.targetSlotOrder;
                                            }
                                            else
                                            {
                                                battleUnitModel.allyCardDetail.ReturnCardToHand(battleUnitModel.cardSlotDetail.cardAry[j].card);
                                                battleUnitModel.cardSlotDetail.cardAry[j] = null;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        List<BattlePlayingCardDataInUnitModel.SubTarget> subTargets3 = battlePlayingCardDataInUnitModel.subTargets;
                                        if (subTargets3.Exists((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == owner))
                                        {
                                            subTargets3.RemoveAll((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == owner);
                                        }
                                        if (battlePlayingCardDataInUnitModel.target == owner)
                                        {
                                            BattleUnitModel targetByCard = BattleObjectManager.instance.GetTargetByCard(battleUnitModel, battlePlayingCardDataInUnitModel.card, j, battleUnitModel.TeamKill());
                                            if (targetByCard != null)
                                            {
                                                int num = UnityEngine.Random.Range(0, targetByCard.speedDiceResult.Count);
                                                num = battleUnitModel.ChangeTargetSlot(battlePlayingCardDataInUnitModel.card, targetByCard, j, num, battleUnitModel.TeamKill());
                                                battlePlayingCardDataInUnitModel.target = targetByCard;
                                                battlePlayingCardDataInUnitModel.targetSlotOrder = num;
                                                battlePlayingCardDataInUnitModel.earlyTarget = targetByCard;
                                                battlePlayingCardDataInUnitModel.earlyTargetOrder = num;
                                            }
                                            else
                                            {
                                                battleUnitModel.allyCardDetail.ReturnCardToHand(battleUnitModel.cardSlotDetail.cardAry[j].card);
                                                battleUnitModel.cardSlotDetail.cardAry[j] = null;
                                            }
                                        }
                                        else if (battlePlayingCardDataInUnitModel.earlyTarget == owner)
                                        {
                                            BattleUnitModel targetByCard2 = BattleObjectManager.instance.GetTargetByCard(battleUnitModel, battlePlayingCardDataInUnitModel.card, j, battleUnitModel.TeamKill());
                                            if (targetByCard2 != null)
                                            {
                                                int num2 = UnityEngine.Random.Range(0, targetByCard2.speedDiceResult.Count);
                                                num2 = battleUnitModel.ChangeTargetSlot(battlePlayingCardDataInUnitModel.card, targetByCard2, j, num2, battleUnitModel.TeamKill());
                                                battlePlayingCardDataInUnitModel.earlyTarget = targetByCard2;
                                                battlePlayingCardDataInUnitModel.earlyTargetOrder = num2;
                                            }
                                            else
                                            {
                                                battlePlayingCardDataInUnitModel.earlyTarget = battlePlayingCardDataInUnitModel.target;
                                                battlePlayingCardDataInUnitModel.earlyTargetOrder = battlePlayingCardDataInUnitModel.targetSlotOrder;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Debug.LogError("AftermathCollection: failed to redirect cards (Benito - Non-combatant)");
                    }
                }
                SingletonBehavior<BattleManagerUI>.Instance.ui_TargetArrow.UpdateTargetList();
            }
        }

        public override bool IsImmune(KeywordBuf buf)
        {
            if (buf is KeywordBuf.Stun || buf is KeywordBuf.Seal)
            {
                return true;
            }
            return base.IsImmune(buf);
        }

        public override bool IsTargetable_theLast()
        {
            return false;
        }
        public override bool IsTargetable(BattleUnitModel attacker)
        {
            return false;
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (origin.CreateDiceCardBehaviorList().Count > 0) return null;
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }
    }

    // Blade Thesis (Silvio)
    /* This passive is activated for one Scene if the character clashes with a Block die against an Offensive die.
     * While deactivated, all Slash dice turn into Blunt dice, inflict one Paralysis when hitting enemies with a Blunt die (twice per Scene).
     * While activated, all Blunt dice become Slash dice. Slash Dice Power +2. */
    public class PassiveAbility_Aftermath_BladeThesis : PassiveAbilityBase
    {
        private int _blunt;

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            base.OnRollDice(behavior);
            bool hasBuf = owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>();
            if (behavior.Detail == BehaviourDetail.Slash && !hasBuf)
            {
                behavior.behaviourInCard.Detail = BehaviourDetail.Hit;
                behavior.behaviourInCard.MotionDetail = MotionDetail.H;
                behavior.behaviourInCard.EffectRes = "Sword_H";
            }

            if (behavior.Detail == BehaviourDetail.Guard && !hasBuf)
            {
                var die = behavior.TargetDice;
                if (die != null)
                    if (die.Type == BehaviourType.Atk)
                        ActivateBladeThesis(owner);
            }
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            _blunt = 0;
        }


        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            base.OnSucceedAttack(behavior);
            bool hasBuf = owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>();

            if (owner.currentDiceAction.target != null)
            {
                if (behavior.Detail == BehaviourDetail.Hit && _blunt < 2 && !hasBuf)
                {
                    _blunt++;
                    this.owner.currentDiceAction.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Paralysis, 1, this.owner);
                    BattleCardTotalResult battleCardResultLog = this.owner.battleCardResultLog;
                    if (battleCardResultLog != null)
                    {
                        battleCardResultLog.SetPassiveAbility(this);
                    }
                }
            }
        }

        public static void ActivateBladeThesis(BattleUnitModel feller)
        {
            feller.bufListDetail.AddBuf(new BattleUnitBuf_BladeThesisActive());
            AftermathCollectionInitializer.PlaySound(AftermathCollectionInitializer.aftermathMapHandler.GetAudioClip("activateBT.mp3"), feller.view.transform, 2f);
            BattleCardTotalResult battleCardResultLog = feller.battleCardResultLog;

            if (battleCardResultLog != null)
            {
                battleCardResultLog.SetPassiveAbility(new PassiveAbility_Aftermath_BladeThesis());
            }

            if (feller.customBook.ClassInfo.Name == "Silvio" || feller.customBook.ClassInfo.Name == "Silvio's Page")
            {
                feller.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
                feller.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
                feller.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
            }
        }


        public class BattleUnitBuf_BladeThesisActive : BattleUnitBuf
        {
            public override void BeforeRollDice(BattleDiceBehavior behavior)
            {
                base.BeforeRollDice(behavior);

                if (behavior.Detail == BehaviourDetail.Hit)
                {
                    behavior.behaviourInCard.Detail = BehaviourDetail.Slash;
                    behavior.behaviourInCard.MotionDetail = MotionDetail.J;
                    behavior.behaviourInCard.EffectRes = "Sword_J";
                }

                if (behavior.Detail == BehaviourDetail.Slash)
                {
                    behavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        power = 2
                    });
                    BattleCardTotalResult battleCardResultLog = this._owner.battleCardResultLog;
                    if (battleCardResultLog != null)
                    {
                        battleCardResultLog.SetPassiveAbility(new PassiveAbility_Aftermath_BladeThesis());
                    }
                }
            }

            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                if (_owner.customBook.ClassInfo.Name == "Silvio" || _owner.customBook.ClassInfo.Name == "Silvio's Page" || _owner.customBook.ClassInfo.Name == "Silvio II")
                {
                    _owner.view.charAppearance.RemoveAltMotion(ActionDetail.Hit);
                    _owner.view.charAppearance.RemoveAltMotion(ActionDetail.Slash);
                    _owner.view.charAppearance.RemoveAltMotion(ActionDetail.Penetrate);
                }
                this.Destroy();
            }
        }
    }

    // Shroomite's Legacy
    // Untransferable. If this character has 5+ Venoms in hand at the end of the Scene, add a single-use 'Overdose' to hand and reduce its Cost to 0 (once per 3 Scenes).
    public class PassiveAbility_Aftermath_ShroomLegacy : PassiveAbilityBase
    {
        private int delay = 0;

        public override void OnRoundEnd()
        {
            delay--;
            if (owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword")).Count > 5 && delay <= 0)
            {
                owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60129));
                delay = 3;
            }
        }
    }

    // Inherited Addiction
    // Every 2 Scenes, draw 1 Venom. If at the end of the Scene this character has 8+ Venoms in hand, exhaust all Venoms and draw 4 pages.
    public class PassiveAbility_Aftermath_ShroomDraw : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            if (Singleton<StageController>.Instance.RoundTurn % 2 == 0)
            {
                VenomCardModel.AddVenomToHand(owner);
            }

            if (owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword")).Count >= 8)
            {
                foreach (BattleDiceCardModel bruh in owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword")))
                {
                    owner.allyCardDetail.DiscardACardByAbility(bruh);
                }
                owner.allyCardDetail.DrawCards(4);
            }
        }
    }

    // Diplomatic Competence
    // Damage and Stagger Damage -3. When inflicting Burn, Paralysis, Fragile or Bleed using Combat Pages, apply +1 additional stack. 
    public class PassiveAbility_Aftermath_DiplomatComp : PassiveAbilityBase
    {
        public override int OnGiveKeywordBufByCard(BattleUnitBuf buf, int stack, BattleUnitModel target)
        {
            List<KeywordBuf> bruh = new List<KeywordBuf>
                {
                    KeywordBuf.Burn,
                    KeywordBuf.Paralysis,
                    KeywordBuf.Vulnerable,
                    KeywordBuf.Bleeding
                };

            if (bruh.Contains(buf.bufType))
            {
                return 1;
            }

            return 0;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus { dmg = -3, breakDmg = -3 });
        }
    }

    // Diplomatic Expertise
    // At the start of the Scene, give 1 Strength to a random ally and 1 Feeble to a random enemy.
    public class PassiveAbility_Aftermath_DiplomatExpert : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            List<BattleUnitModel> bruh = BattleObjectManager.instance.GetAliveList(owner.faction);
            bruh.Remove(owner);
            RandomUtil.SelectOne(bruh).bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, owner);
            RandomUtil.SelectOne(BattleObjectManager.instance.GetAliveList_opponent(owner.faction)).bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Weak, 1, owner);
        }
    }

    // Cauterize Wounds
    // At the start of the Act and every 3 Scenes, this character uses "Cauterize", which heals 50 HP in exchange for 25 Stagger.
    public class PassiveAbility_Aftermath_TorchGain : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            if (Singleton<StageController>.Instance.RoundTurn % 3 == 0)
            {
                owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60127));
            }
        }

        public override void OnWaveStart()
        {
            base.OnWaveStart();
            owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60127)).SetCostToZero();
            AftermathCollectionInitializer.aftermathMapHandler.InitCustomMap<BreadBoysMapManager>("BreadBoysLastStand");
        }

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            AftermathCollectionInitializer.aftermathMapHandler.EnforceMap();
        }
    }


    // Family Comes First / Passive Injection (Silvio)
    // At the start of the Scene, discard all pages in hand, draw 5 pages and set their Cost to 0.
    public class PassiveAbility_Aftermath_FamilyFirstS : PassiveAbilityBase
    {
        private bool triggered = false;

        public override void OnRoundStartAfter()
        {
            base.OnRoundStartAfter();
            if (owner.emotionDetail.EmotionLevel >= 2 && !triggered)
            {
                owner.allyCardDetail.AddNewCardToDeck(new LorId(AftermathCollectionInitializer.packageId, 60114));
                owner.allyCardDetail.AddNewCardToDeck(new LorId(AftermathCollectionInitializer.packageId, 60114));
                triggered = true;
            }
        }

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            owner.allyCardDetail.DrawCards(5);
            foreach (BattleDiceCardModel bruh in owner.allyCardDetail.GetHand())
            {
                bruh.SetCostToZero();
            }
        }


        public override void OnWaveStart()
        {
            base.OnWaveStart();
            owner.formation.ChangePos(new Vector2Int(8, 0));
        }
    }

    // Family Comes First (Benito)
    // At the start of the Scene, draw 5 pages and set the cost of all pages in hand to 0.
    public class PassiveAbility_Aftermath_FamilyFirstB : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            owner.allyCardDetail.DrawCards(5);
            foreach (BattleDiceCardModel bruh in owner.allyCardDetail.GetHand())
            {
                bruh.SetCostToZero();
            }
        }

        public override void OnWaveStart()
        {
            base.OnWaveStart();
            owner.formation.ChangePos(new Vector2Int(21, 0));
        }
    }

    #endregion

    #region - LIU REMNANTS -

    // Starvation/Adaptive Warfare
    // This character does not restore Light passively or by leveling up Emotion.
    // When this character's Emotion Level changes, gain 1 Endurance for every 2 Positive Emotion Points,
    // and 1 Strength for every 2 Negative Emotion Points that the character had before their Emotion Level changed.
    public class PassiveAbility_Aftermath_Aftermathstarvation : PassiveAbilityBase
    {
        int _lightLastScene;
        bool h = false;


        public override void OnStartOneSidePlay(BattlePlayingCardDataInUnitModel card) => owner.battleCardResultLog?.SetEndCardActionEvent(PrintEffect);
        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card) => owner.battleCardResultLog?.SetEndCardActionEvent(PrintEffect);

        private void PrintEffect()
        {
            _lightLastScene = owner.PlayPoint;
            int posCoin = owner.emotionDetail.PositiveCoins.Count;
            int negCoin = owner.emotionDetail.NegativeCoins.Count;

            if (owner.emotionDetail.CheckLevelUp())
            {
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, negCoin / 2, owner);
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, posCoin / 2, owner);
                owner.cardSlotDetail.SetPlayPoint(_lightLastScene + owner.cardSlotDetail.GetMaxPlayPoint() / 2);
            }
        }

        public override void OnRoundEnd()
        {
            _lightLastScene = owner.PlayPoint;
            int balls = owner.emotionDetail.AllEmotionCoins.Count;

            if (balls >= owner.emotionDetail.MaximumCoinNumber)
            {
                h = true;
                int posCoin = owner.emotionDetail.PositiveCoins.Count;
                int negCoin = owner.emotionDetail.NegativeCoins.Count;

                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, negCoin / 2, owner);
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, posCoin / 2, owner);
            }
        }

        public override void OnRoundEndTheLast()
        {
            if (h)
            {
                h = false;
                owner.cardSlotDetail.SetPlayPoint(_lightLastScene + owner.cardSlotDetail.GetMaxPlayPoint() / 2);
            }
        }
    }

    // Starvation/Fervent Passion
    // This character does not restore Light passively or by leveling up Emotion.
    // Gain +1 Power for every 2 Emotion Levels. Upon leveling up Emotion, draw 1 page and reduce its cost by 1.
    public class PassiveAbility_Aftermath_Aftermathstarvation2 : PassiveAbilityBase
    {
        int _lightLastScene;
        bool h = false;

        public override void OnLevelUpEmotion()
        {
            base.OnLevelUpEmotion();
            var page = RandomUtil.SelectOne(owner.allyCardDetail.GetDeck());
            if (page != null)
            {
                if (page.GetCost() > 0)
                {
                    page.AddCost(-1);
                }
                owner.allyCardDetail.AddCardToHand(page);
            }
        }

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = owner.emotionDetail.EmotionLevel / 2 });
        }

        public override void OnStartOneSidePlay(BattlePlayingCardDataInUnitModel card) => owner.battleCardResultLog?.SetEndCardActionEvent(PrintEffect);
        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card) => owner.battleCardResultLog?.SetEndCardActionEvent(PrintEffect);

        private void PrintEffect()
        {
            _lightLastScene = owner.PlayPoint;

            if (owner.emotionDetail.CheckLevelUp())
            {
                owner.cardSlotDetail.SetPlayPoint(_lightLastScene + owner.cardSlotDetail.GetMaxPlayPoint() / 2);
            }
        }

        public override void OnRoundEnd()
        {
            _lightLastScene = owner.PlayPoint;

            if (owner.emotionDetail.AllEmotionCoins.Count >= owner.emotionDetail.MaximumCoinNumber)
            {
                h = true;
            }

        }

        public override void OnRoundEndTheLast()
        {
            if (h)
            {
                h = false;
                owner.cardSlotDetail.SetPlayPoint(_lightLastScene + owner.cardSlotDetail.GetMaxPlayPoint() / 2);
            }
        }
    }

    // Claws of the Phoenix
    // When inflicting Burn using Combat Pages, inflict +1 additional stack.
    // +1 Offensive Dice Power against targets with Burn. At Emotion Level 4, add 'Rise From The Ashes' to hand.
    public class PassiveAbility_Aftermath_Aftermathdragonfist : PassiveAbilityBase
    {
        bool emot = false;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Type == BehaviourType.Atk)
            {
                BattleUnitModel target = behavior.card.target;
                if (target != null && target.bufListDetail.GetKewordBufAllStack(KeywordBuf.Burn) > 0)
                {
                    behavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        power = 1
                    });
                }
            }
        }

        public override int OnGiveKeywordBufByCard(BattleUnitBuf buf, int stack, BattleUnitModel target)
        {
            if (buf.bufType == KeywordBuf.Burn)
            {
                return 1;
            }
            return 0;
        }

        public override void OnRoundStart()
        {
            if (!emot && owner.emotionDetail.EmotionLevel >= 4)
            {
                emot = true;
                owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60217));
            }
        }
    }

    // Fire Up
    // Whenever this character's Emotion Level changes,
    // all attacks inflict an additional stack of Burn the following Scene.
    public class PassiveAbility_Aftermath_AftermathFireUp : PassiveAbilityBase
    {
        int _emotionLevel = 0;

        public override void OnRoundStart()
        {
            if (_emotionLevel != owner.emotionDetail.EmotionLevel)
            {
                owner.bufListDetail.AddBuf(new BattleUnitBuf_FireUpGaming());
                _emotionLevel = owner.emotionDetail.EmotionLevel;
            }
        }

        public class BattleUnitBuf_FireUpGaming : BattleUnitBuf
        {
            public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
            {
                if (cardBuf.bufType == KeywordBuf.Burn)
                {
                    return 1;
                }
                return 0;
            }

            public override void OnRoundEnd()
            {
                _owner.bufListDetail.RemoveBuf(this);
                this.Destroy();
            }
        }
    }

    // Together We Can
    // At the end of the Scene, if 4 or more allies are alive,
    // gain a Positive Emotion Point.
    public class PassiveAbility_Aftermath_AftermathTogetherWeCan : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (owner.IsDead())
            {
                return;
            }

            if (BattleObjectManager.instance.GetAliveList(owner.faction).Count >= 4)
            {
                owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Positive, 1);
                SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, EmotionCoinType.Positive, 1);
            }
        }
    }

    // Survival Instincts
    // At the end of the Scene, if this character took 15 or more damage from attacks in the previous Scene,
    // gain a Negative Emotion Point.
    public class PassiveAbility_Aftermath_AftermathSurvivalInstinct : PassiveAbilityBase
    {
        int _damage;

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            _damage += dmg;
        }

        public override void OnRoundEnd()
        {
            if (owner.IsDead())
            {
                return;
            }

            if (_damage >= 15)
            {
                owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative, 1);
                SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, EmotionCoinType.Positive, 1);
            }
            _damage = 0;
        }
    }

    // Scorched Body
    // gain 10% burn prot on clash win
    public class PassiveAbility_Aftermath_AftermathScorchBody : PassiveAbilityBase
    {
        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_AftermathBurnProtection);
            if (buf != null)
            {
                buf.stack++;
                buf.Init(owner);
            }
            else
                owner.bufListDetail.AddBuf(new BattleUnitBuf_AftermathBurnProtection() { stack = 1 });
        }

        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_AftermathBurnProtection() { stack = 5 });
        }
    }

    // Inflammed Mind
    // At the start of each Scene, gain 1 Strength. For every 2 clashes won, inflict 3 Burn to self.
    public class PassiveAbility_Aftermath_AftermathFireMind : PassiveAbilityBase
    {
        int _win = 0;

        public override void OnRoundStart()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, owner);
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            _win++;
            if (_win % 2 == 0)
            {
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 3, owner);
            }
        }

        // Bottle It Up AI
        public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
        {
            if (card.GetID().id == 60202 && card.GetID().packageId == AftermathCollectionInitializer.packageId)
            {
                List<BattleUnitModel> bruh = BattleObjectManager.instance.GetAliveList(true);
                bruh.Remove(owner);

                List<int> what = new List<int>();
                foreach (BattleUnitModel model in bruh)
                {
                    what.Add(model.bufListDetail.GetKewordBufAllStack(KeywordBuf.Burn));
                }
                if (bruh[what.IndexOf(what.Max())] != null)
                    return bruh[what.IndexOf(what.Max())];
            }
            return base.ChangeAttackTarget(card, idx);
        }
    }

    // Burning Spirit
    // Restore 1 Light at the end of the Scene for every 5 stacks of Burn inflicted onto enemies that Scene (Up to 2).
    public class PassiveAbility_Aftermath_AftermathBurnSpirit : PassiveAbilityBase
    {
        int _THISGIRLISONFIIIIRE = 0;

        public override int OnGiveKeywordBufByCard(BattleUnitBuf buf, int stack, BattleUnitModel target)
        {
            if (buf.bufType == KeywordBuf.Burn)
            {
                _THISGIRLISONFIIIIRE += stack;
            }
            return base.OnGiveKeywordBufByCard(buf, stack, target);
        }

        public override void OnRoundStart()
        {
            _THISGIRLISONFIIIIRE /= 5;
            if (_THISGIRLISONFIIIIRE >= 2)
            {
                owner.cardSlotDetail.RecoverPlayPoint(2);
            }
            else
            {
                owner.cardSlotDetail.RecoverPlayPoint(_THISGIRLISONFIIIIRE);
            }
            _THISGIRLISONFIIIIRE = 0;
        }
    }

    #endregion

    #region - COLOR CHUN -

    // The Vermillion Dragon
    // On hit, inflict 1 Burn for each Emotion Level this character has (Up to Level 3).
    public class PassiveAbility_Aftermath_AftermathVerDragon : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            var target = behavior.card.target;
            if (target != null)
            {
                int funne = owner.emotionDetail.EmotionLevel;
                if (funne >= 3)
                {
                    target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 3, owner);
                }
                else if (funne > 0)
                {
                    target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, funne, owner);
                }
            }
        }

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            if (owner.faction == Faction.Enemy)
            {
                owner.cardSlotDetail.RecoverPlayPoint(1);
                owner.allyCardDetail.DrawCards(2);
            }
        }
    }

    // The Fighter That Went Beyond
    // When the Emotion Level rises, gain 1 Strength and 1 Endurance.
    // At Emotion Level 3 and above, gain 1 Strength and Endurance each Scene.
    // At Emotion Level 5 and above, gain 2 Strength and Endurance each Scene.
    public class PassiveAbility_Aftermath_AftermathFighterBeyond : PassiveAbilityBase
    {
        int str = 0;
        int end = 0;

        public override void OnLevelUpEmotion()
        {
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, owner);
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 1, owner);
            str++;
            end++;
        }

        public override void OnRoundStart()
        {
            if (owner.emotionDetail.EmotionLevel >= 5)
            {
                if (str < 2)
                    owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 2 - str, owner);
                if (end < 2)
                    owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 2 - end, owner);
            }
            else if (owner.emotionDetail.EmotionLevel >= 3)
            {
                if (str < 1)
                    owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, owner);
                if (end < 1)
                    owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, owner);
            }
        }

        public override void OnRoundEnd()
        {
            end = 0;
            str = 0;
        }
    }

    #endregion

    #region - RETURN OF THE INDEX -

    // Locked Potential (stays between Acts)
    public class PassiveAbility_Aftermath_SuperLockedPotential : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (this.owner.UnitData.floorBattleData.param1 == 0)
            {
                this._cardIdList = new List<LorId>();
            }
            if (this.owner.UnitData.floorBattleData.param1 == 1)
            {
                this.IndexWaveStartUnlock();
            }
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (this.owner.UnitData.floorBattleData.param1 == 0)
            {
                LorId id = curCard.card.GetID();
                if (!this._cardIdList.Contains(id))
                {
                    this._cardIdList.Add(id);
                }
                if (!this.trigger)
                {
                    if (this._cardIdList.Count >= 6)
                    {
                        this.owner.bufListDetail.RemoveBufAll(typeof(PassiveAbility_250115.BattleUnitBuf_indexReleaseCount));
                        if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) == null)
                        {
                            this.IndexCardUnlock();
                        }
                    }
                    else
                    {
                        this.owner.bufListDetail.RemoveBufAll(typeof(PassiveAbility_250115.BattleUnitBuf_indexReleaseCount));
                        this.owner.bufListDetail.AddBufWithoutDuplication(new PassiveAbility_250115.BattleUnitBuf_indexReleaseCount
                        {
                            stack = this._cardIdList.Count
                        });
                    }
                }
            }
        }

        public override void OnRoundStart()
        {
            if (this.owner.UnitData.floorBattleData.param1 == 0)
            {
                foreach (BattleDiceCardModel battleDiceCardModel in this.owner.allyCardDetail.GetAllDeck())
                {
                    battleDiceCardModel.RemoveBuf<PassiveAbility_250115.BattleDiceCardBuf_indexReleaseCount>();
                }
                if (this._cardIdList.Count >= 6)
                {
                    return;
                }
                foreach (BattleDiceCardModel battleDiceCardModel2 in this.owner.allyCardDetail.GetAllDeck())
                {
                    if (!this._cardIdList.Contains(battleDiceCardModel2.GetID()) && battleDiceCardModel2.GetSpec().Ranged != CardRange.Instance)
                    {
                        battleDiceCardModel2.AddBuf(new PassiveAbility_250115.BattleDiceCardBuf_indexReleaseCount());
                    }
                }
            }
        }

        public override void OnDestroyed()
        {
            if (this._aura != null)
            {
                UnityEngine.Object.Destroy(this._aura);
            }
        }

        private void SetParticle()
        {
            if (this._aura == null)
            {
                UnityEngine.Object @object = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_Aura");
                if (@object != null)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate(@object) as GameObject;
                    gameObject.transform.parent = this.owner.view.charAppearance.transform;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    gameObject.transform.localScale = Vector3.one;
                    IndexReleaseAura component = gameObject.GetComponent<IndexReleaseAura>();
                    if (component != null)
                    {
                        component.Init(this.owner.view);
                    }
                    this._aura = gameObject;
                }
                UnityEngine.Object object2 = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_ActivateParticle");
                if (object2 != null)
                {
                    GameObject gameObject2 = UnityEngine.Object.Instantiate(object2) as GameObject;
                    gameObject2.transform.parent = this.owner.view.charAppearance.transform;
                    gameObject2.transform.localPosition = Vector3.zero;
                    gameObject2.transform.localRotation = Quaternion.identity;
                    gameObject2.transform.localScale = Vector3.one;
                }
                SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Buf/Effect_Index_Unlock", false, 1f, null);
            }
        }

        public override void OnBattleEnd_alive()
        {
            this.owner.UnitData.floorBattleData.param1 = 0;
            if (this.trigger)
            {
                this.owner.UnitData.floorBattleData.param1 = 1;
            }
        }

        private void IndexCardUnlock()
        {
            this.trigger = true;
            this.owner.bufListDetail.RemoveBufAll(typeof(PassiveAbility_250115.BattleUnitBuf_indexReleaseCount));
            if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) == null)
            {
                this.owner.bufListDetail.AddBuf(new PassiveAbility_250115.BattleUnitBuf_indexRelease());
                BattleUnitModel owner = this.owner;
                if (owner != null)
                {
                    BattleCardTotalResult battleCardResultLog = owner.battleCardResultLog;
                    if (battleCardResultLog != null)
                    {
                        battleCardResultLog.SetUseCardEvent(new BattleCardBehaviourResult.BehaviourEvent(this.SetParticle));
                    }
                }
            }
            if (this.owner.customBook.ContainsCategory(BookCategory.TheIndex))
            {
                this.owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
                this.owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
                this.owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
            }
            if (owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42060302) || owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42060301))
            {
                owner.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "BLADE_UNLOCK_" + Singleton<System.Random>.Instance.Next(2).ToString());
            }
        }

        private void IndexWaveStartUnlock()
        {
            this.trigger = true;
            this.owner.bufListDetail.RemoveBufAll(typeof(PassiveAbility_250115.BattleUnitBuf_indexReleaseCount));
            if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) == null)
            {
                this.owner.bufListDetail.AddBuf(new PassiveAbility_250115.BattleUnitBuf_indexRelease());
                BattleUnitModel owner = this.owner;
                if (owner != null)
                {
                    this.SetParticle();
                }
            }
            if (this.owner.customBook.ContainsCategory(BookCategory.TheIndex))
            {
                this.owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
                this.owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
                this.owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
            }
        }

        public void ClearList()
        {
            this._cardIdList.Clear();
        }

        private List<LorId> _cardIdList = new List<LorId>();

        private bool trigger;

        private GameObject _aura;
    }

    // Thrill of Battle
    public class PassiveAbility_Aftermath_Evan1 : PassiveAbilityBase
    {
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            LorId id = curCard.card.GetID();
            if (!this._cardIdList.Contains(id))
            {
                this._cardIdList.Add(id);
            }
            if (this._cardIdList.Count >= 4)
            {
                this._cardIdList.Clear();
                int e = (int)((float)this.owner.MaxHp * 0.10f);
                if (e > 8) e = 8;
                this.owner.RecoverHP(e);
            }
        }

        public override void OnRoundStart()
        {
            if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null)
            {
                BattleUnitBuf_Aftermath_EvanShield.GainBuf(base.owner, 1);
            }
        }

        private List<LorId> _cardIdList = new List<LorId>();
    }

    // Juggernaut
    public class PassiveAbility_Aftermath_Evan2 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            this.owner.personalEgoDetail.AddCard(new LorId(AftermathCollectionInitializer.packageId, 60319));
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            int num = 0;
            BattleUnitModel unitModel = this.owner;
            Predicate<BattlePlayingCardDataInUnitModel.SubTarget> match = (BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == unitModel;
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(false))
            {
                foreach (BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel in battleUnitModel.cardSlotDetail.cardAry)
                {
                    bool flag = false;
                    if (battlePlayingCardDataInUnitModel != null && battlePlayingCardDataInUnitModel.card != null)
                    {
                        if (battlePlayingCardDataInUnitModel.subTargets.Count > 0 && battlePlayingCardDataInUnitModel.subTargets.Exists(match))
                        {
                            flag = true;
                        }
                        if (battlePlayingCardDataInUnitModel.target == unitModel || flag)
                        {
                            num++;
                        }
                    }
                }
            }
            if (num > 5)
            {
                num = 5;
            }
            owner.bufListDetail.AddBuf(new BattleUnitBuf_EvanProtectionJuggernaut(num));
        }

        public override void OnStartBattle()
        {
            int num = 0;
            BattleUnitModel unitModel = this.owner;
            Predicate<BattlePlayingCardDataInUnitModel.SubTarget> match = (BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == unitModel;
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(false))
            {
                foreach (BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel in battleUnitModel.cardSlotDetail.cardAry)
                {
                    bool flag = false;
                    if (battlePlayingCardDataInUnitModel != null && battlePlayingCardDataInUnitModel.card != null)
                    {
                        if (battlePlayingCardDataInUnitModel.subTargets.Count > 0 && battlePlayingCardDataInUnitModel.subTargets.Exists(match))
                        {
                            flag = true;
                        }
                        if (battlePlayingCardDataInUnitModel.target == unitModel || flag)
                        {
                            num++;
                        }
                    }
                }
            }
            if (num >= 5)
            {
                DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId(AftermathCollectionInitializer.packageId, 60318));
                new DiceBehaviour();
                List<BattleDiceBehavior> list = new List<BattleDiceBehavior>();
                int num2 = 0;
                foreach (DiceBehaviour diceBehaviour in cardItem.DiceBehaviourList)
                {
                    BattleDiceBehavior battleDiceBehavior = new BattleDiceBehavior();
                    battleDiceBehavior.behaviourInCard = diceBehaviour.Copy();
                    battleDiceBehavior.SetIndex(num2++);
                    list.Add(battleDiceBehavior);
                }
                this.owner.cardSlotDetail.keepCard.AddBehaviours(cardItem, list);
            }
        }
        public override void OnRoundStart()
        {
            List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(this.owner.faction);
            aliveList.Remove(this.owner);
            BattleUnitModel battleUnitModel = null;
            bool flag = aliveList.Count > 0 && this.owner.emotionDetail.EmotionLevel >= 3;
            if (flag)
            {
                foreach (BattleUnitModel battleUnitModel2 in aliveList)
                {
                    bool flag2 = battleUnitModel == null;
                    if (flag2)
                    {
                        battleUnitModel = battleUnitModel2;
                    }
                    else
                    {
                        bool flag3 = battleUnitModel2.hp == battleUnitModel.hp;
                        if (flag3)
                        {
                            bool flag4 = RandomUtil.SelectOne<int>(new List<int>
                            {
                                0,
                                1
                            }) == 1;
                            bool flag5 = flag4;
                            if (flag5)
                            {
                                battleUnitModel = battleUnitModel2;
                            }
                        }
                        else
                        {
                            bool flag6 = battleUnitModel2.hp < battleUnitModel.hp;
                            if (flag6)
                            {
                                battleUnitModel = battleUnitModel2;
                            }
                        }
                    }
                }
                bool flag7 = battleUnitModel != null;
                if (flag7)
                {
                    battleUnitModel.bufListDetail.AddBuf(new PassiveAbility_Aftermath_Evan2.BattleUnitBuf_EvanBodyGuard());
                }
            }
        }

        public class BattleUnitBuf_EvanBodyGuard : BattleUnitBuf
        {
            public override bool Hide
            {
                get
                {
                    return true;
                }
            }

            public override bool IsTargetable(BattleUnitModel attacker)
            {
                foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(this._owner.faction))
                {
                    bool flag = battleUnitModel != this._owner && battleUnitModel.IsTargetable(attacker);
                    if (flag)
                    {
                        return false;
                    }
                }
                return base.IsTargetable(attacker);
            }

            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                this.Destroy();
            }
        }
        
        public class BattleUnitBuf_EvanProtectionJuggernaut : BattleUnitBuf
        {
            public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
            {
                return 1f - (stack / 100f * 5f);
            }

            public BattleUnitBuf_EvanProtectionJuggernaut(int stacks)
            {
                this.stack = stacks;
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }
    }

    // The Messenger
    public class PassiveAbility_Aftermath_Evan5 : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            foreach (int cardId2 in PassiveAbility_Aftermath_Evan5.targetIds)
            {
                this.owner.personalEgoDetail.RemoveCard(new LorId(AftermathCollectionInitializer.packageId, cardId2));
            }
            (this.owner.passiveDetail.PassiveList.Find((PassiveAbilityBase x) => x is PassiveAbility_Aftermath_Evan5) as PassiveAbility_Aftermath_Evan5).GivePrescriptPages(this.owner);
        }

        public void AddOne(List<int> list)
        {
            bool flag = list.Count <= 0;
            bool flag2 = !flag;
            if (flag2)
            {
                int num = RandomUtil.SelectOne<int>(list);
                this.owner.personalEgoDetail.AddCard(new LorId(AftermathCollectionInitializer.packageId, (num)));
                list.Remove(num);
            }
        }

        public void GivePrescriptPages(BattleUnitModel unit)
        {
            List<int> list = new List<int>(PassiveAbility_Aftermath_Evan5.targetIds);
            if (this.owner.emotionDetail.EmotionLevel >= 3)
            {
                this.AddOne(list);
                this.AddOne(list);
                return;
            }
            this.AddOne(list);
        }

        public static readonly List<int> targetIds = new List<int>
        {
            60320,
            60321,
            60322,
            60323
        };

    }

    // Unladen Blade
    public class PassiveAbility_Aftermath_Ophe_1 : PassiveAbilityBase
    {

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (curCard.card.CheckForKeyword("Quickness_Keyword")) curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus() { power = 1 });
        }


        /*
        public bool rerolled
        {
            get
            {
                return this._bRerolled;
            }
        }

        public override void ChangeDiceResult(BattleDiceBehavior behavior, ref int diceResult)
        {
            BattleUnitBuf_quickness battleUnitBuf_quickness = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness) as BattleUnitBuf_quickness;

            int num = 0; // reroll count
            int diceMin = behavior.GetDiceMin();
            int diceMax = behavior.GetDiceMax();

            // Check if the dice result is less than or equal to the minimum value, has haste and the counter is less than or equal to 2
            if (diceResult <= diceMin && battleUnitBuf_quickness != null && battleUnitBuf_quickness.stack >= 1 && num <= 2)
            {
                // reroll
                diceResult = DiceStatCalculator.MakeDiceResult(diceMin, diceMax, 0);

                BattleCardTotalResult battleCardResultLog = behavior.owner.battleCardResultLog;
                if (battleCardResultLog != null)
                {
                    battleCardResultLog.SetVanillaDiceValue(diceMin);
                    battleCardResultLog.SetPassiveAbility(this);
                }

                num++; // Increment the counter
                battleUnitBuf_quickness.stack = battleUnitBuf_quickness.stack - 1; // no more haste :c
            }

            this._bRerolled = (diceResult <= diceMin); // Update the rerolled flag
        }

        private bool _bRerolled; // Flag to indicate if the dice result was rerolled

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);

            var card = behavior.card;
            if (card != null)
            {
                if (card.slotOrder == 0 && card.target != null)
                {
                    if (card.target.currentDiceAction != null)
                        if (card.target.currentDiceAction.earlyTarget != owner)
                        {
                            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
                        }
                }
            }
        }
        */
    }

    // Rapid Response
    public class PassiveAbility_Aftermath_Ophe_2 : PassiveAbilityBase
    {
        private bool first = true;

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (first)
            {
                if (owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null)
                {
                    base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 4, base.owner);
                }

                else
                {
                    base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, base.owner);
                }
                first = false;
            }            
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            first = true;
            int buf = owner.bufListDetail.GetKewordBufStack(KeywordBuf.Quickness);
            if (buf <= 0)
            {
                owner.cardSlotDetail.RecoverPlayPoint(1);
                owner.allyCardDetail.DrawCards(1);
            }
        }
    }

    // Speed P
    public class PassiveAbility_Aftermath_SpeedPrescript : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            return 1 + BattleObjectManager.instance.GetAliveList(owner.faction).FindAll(x => x != owner && x.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null).Count;
        }
    }

    // Guided Assault
    public class PassiveAbility_Aftermath_GuidedAssault : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            var card = owner.cardSlotDetail.cardQueue.Last();
            if (card != null)
            {
                var list = card.GetDiceBehaviorList().FindAll(x => IsAttackDice(x.Detail));
                if (list.Count > 1)
                {
                    var die = list.First(x => IsAttackDice(x.Detail));
                    foreach (var dice in list)
                    {
                        dice.behaviourInCard.Detail = die.Detail;
                    }
                }
            }
        }
    }

    #endregion

    #region - NORINCO WORKSHOP / C.B.L. I -

    public class PassiveAbility_Aftermath_NorincoDesperation : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (owner.cardSlotDetail.cardQueue.ToList().Find(x => x.card.exhaust == true) != null) owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, owner);

        }
    }

    public class PassiveAbility_Aftermath_NorincoSuperIdol : PassiveAbilityBase
    {
        private bool canYOULIVE;

        public override void OnRoundStart()
        {
            canYOULIVE = true;
        }

        public override bool OnBreakGageZero()
        {
            if (canYOULIVE && RandomUtil.SystemRange(2) == 0)
            {
                owner.breakDetail.RecoverBreak(owner.breakDetail.GetDefaultBreakGauge() / 2);
                canYOULIVE = false;
                return true;
            }
            return false;
        }
    }

    public class PassiveAbility_Aftermath_StoredGoods : PassiveAbilityBase
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            self.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_StoredChems() { stack = 7 });
        }
    }
    
    public class PassiveAbility_Aftermath_RunningInThe90s : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            base.OnWaveStart();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 2, owner);
        }

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            base.OnRollDice(behavior);
            if (behavior.behaviourInCard.ActionScript == "" && (owner.customBook.ClassInfo.GetCharacterSkin() == "Dave" || owner.customBook.ClassInfo.GetCharacterSkin() == "Dave_player"))
            {
                behavior.behaviourInCard.ActionScript = "AftermathDaveDawnAction";
            }
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            base.OnSucceedAttack(behavior);
            var buf = owner.bufListDetail.GetReadyBuf(KeywordBuf.Quickness);
            if (buf != null)
            {
                buf.stack--;
                if (buf.stack < 1) buf.Destroy();
            }
        }
    }

    public class PassiveAbility_Aftermath_OverdriveShift : PassiveAbilityBase
    {
        bool threshold = false;

        public override void OnDie()
        {
            if (owner.customBook.ClassInfo.GetCharacterSkin() == "Dave_player")
            {
                owner.view.deadEvent = AftermathUtilityExtensions.ExplodeOnDeath;
                base.OnDie();
            }
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            base.OnTakeDamageByAttack(atkDice, dmg);
            if ((owner.MaxHp / 4) > owner.hp && !threshold)
            {
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_WELCUMTOBOTTMGEARMATES());
                threshold = true;
                AftermathCollectionInitializer.aftermathMapHandler.SetEnemyTheme("dememenic_keter3eurobeat.mp3", false);
            }
        }

        public class BattleUnitBuf_Aftermath_WELCUMTOBOTTMGEARMATES : BattleUnitBuf
        {
            int RENDFROMEXISTENCE = 4;

            public override void OnRoundEnd()
            {
                AftermathCollectionInitializer.aftermathMapHandler.EnforceTheme();
                base.OnRoundEnd();
                _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 3, _owner);
                RENDFROMEXISTENCE--;
                if (RENDFROMEXISTENCE <= 0)
                {
                    _owner.Die(null, true);
                    AftermathCollectionInitializer.aftermathMapHandler.UnEnforceTheme();
                }
            }

            public override void BeforeRollDice(BattleDiceBehavior behavior)
            {
                base.BeforeRollDice(behavior);
                behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 2 });
            }

            public override string keywordId => "Aftermath_TopGear";

            public override string keywordIconId => "Aftermath_TopGear";
        }
    }

    public class PassiveAbility_Aftermath_OverdriveShiftBoss : PassiveAbilityBase
    {
        bool threshold = false;
        bool goTopGearGo = false;

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if ((owner.MaxHp / 4) > owner.hp && !threshold)
            {
                owner.SetHp(owner.MaxHp / 4);
                threshold = true;
                goTopGearGo = true;
            }
            return goTopGearGo;
        }

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            if (!owner.breakDetail.IsBreakLifeZero() && goTopGearGo)
            {
                goTopGearGo = false;
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_TOPGEAREALSHIT());
                AftermathCollectionInitializer.aftermathMapHandler.InitCustomMap<CBLOnePointTwoManager>("CBLOnePointTwo");
                if (owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42020102))
                {
                    owner.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "TOP_GEAR_" + Singleton<System.Random>.Instance.Next(2).ToString());
                }
            }
            if (owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_TOPGEAREALSHIT) != null) AftermathCollectionInitializer.aftermathMapHandler.EnforceMap(1);
            else AftermathCollectionInitializer.aftermathMapHandler.EnforceMap();
        }

        public override bool IsImmuneBreakDmg(DamageType type)
        {
            return goTopGearGo;
        }

        public class BattleUnitBuf_Aftermath_TOPGEAREALSHIT : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 3, _owner);
                _owner.cardSlotDetail.RecoverPlayPoint(2);
                _owner.allyCardDetail.DrawCards(2);
            }

            public override void OnDie()
            {
                _owner.view.deadEvent = AftermathUtilityExtensions.ExplodeOnDeath;
                base.OnDie();
            }

            public override void BeforeRollDice(BattleDiceBehavior behavior)
            {
                base.BeforeRollDice(behavior);
                behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 2 });
            }

            public override bool IsImmuneBreakDmg(DamageType type)
            {
                return true;
            }

            public override int MaxPlayPointAdder()
            {
                return 1;
            }

            public override int SpeedDiceNumAdder()
            {
                return 2;
            }

            public override string keywordId => "Aftermath_TopGearBoss";

            public override string keywordIconId => "Aftermath_TopGear";
        }
    }

    public class PassiveAbility_Aftermath_MeAndTheBoys : PassiveAbilityBase
    {
        private int curThreshhold;
        private bool stahpBroPls = false;

        public override void OnWaveStart()
        {
            base.OnWaveStart();
            AftermathCollectionInitializer.aftermathMapHandler.InitCustomMap<CBLOneMapManager>("CBLOneMapManager");
            owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S10);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Damaged, ActionDetail.S10);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S10);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Guard, ActionDetail.S10);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Aim, ActionDetail.S10);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.S11);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.S11);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.S11);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Move, ActionDetail.S11);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Fire, ActionDetail.S11);
            owner.view.charAppearance.SetAltMotion(ActionDetail.Evade, ActionDetail.S12);
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (owner.MaxHp * 0.75 > owner.hp - dmg && curThreshhold < 1)
            {
                owner.SetHp((int)Math.Truncate(owner.MaxHp * 0.75));
                curThreshhold++;
                stahpBroPls = true;
            }
            else if (owner.MaxHp * 0.5 > owner.hp - dmg && curThreshhold < 2)
            {
                owner.SetHp((int)Math.Truncate(owner.MaxHp * 0.5));
                curThreshhold++;
                stahpBroPls = true;
            }
            return stahpBroPls;
        }

        public override void OnRoundEnd()
        {
            owner.cardSlotDetail.RecoverPlayPoint(1);
            owner.allyCardDetail.DrawCards(1);
            base.OnRoundEnd();
            if (stahpBroPls)
            {
                stahpBroPls = false;
                switch (curThreshhold) {
                    case 1:
                        var model1 = Singleton<StageController>.Instance.AddModdedUnit(Faction.Enemy, new LorId(AftermathCollectionInitializer.packageId, 10102), -1, Singleton<System.Random>.Instance.Next(165, 176), new XmlVector2() { x = 18, y = 16 });
                        var model2 = Singleton<StageController>.Instance.AddModdedUnit(Faction.Enemy, new LorId(AftermathCollectionInitializer.packageId, 10102), -1, Singleton<System.Random>.Instance.Next(165, 176), new XmlVector2() { x = 17, y = -16 });
                        owner.view.charAppearance.RemoveAllAltMotion();
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S13);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Damaged, ActionDetail.S13);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S13);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Guard, ActionDetail.S13);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Aim, ActionDetail.S13);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.S14);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.S14);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.S14);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Move, ActionDetail.S14);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Fire, ActionDetail.S14);
                        owner.view.charAppearance.SetAltMotion(ActionDetail.Evade, ActionDetail.S15);
                        UnitUtil.RefreshCombatUI();
                        model1.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "GREENHORNS_OUT_" + Singleton<System.Random>.Instance.Next(3).ToString());
                        model2.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "GREENHORNS_OUT_" + Singleton<System.Random>.Instance.Next(3).ToString());
                        if (owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42020102))
                        {
                            owner.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "GREENHORNS_OUT_" + Singleton<System.Random>.Instance.Next(2).ToString());
                        }
                        break;

                    case 2:
                        var model3 = Singleton<StageController>.Instance.AddModdedUnit(Faction.Enemy, new LorId(AftermathCollectionInitializer.packageId, 20101), -1, Singleton<System.Random>.Instance.Next(165, 176), new XmlVector2() { x = 8, y = 8 });
                        var model4 = Singleton<StageController>.Instance.AddModdedUnit(Faction.Enemy, new LorId(AftermathCollectionInitializer.packageId, 20101), -1, Singleton<System.Random>.Instance.Next(165, 176), new XmlVector2() { x = 8, y = -8 });
                        owner.view.charAppearance.RemoveAllAltMotion();
                        UnitUtil.RefreshCombatUI();
                        model3.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "JUNKIES_OUT_" + Singleton<System.Random>.Instance.Next(3).ToString());
                        model4.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "JUNKIES_OUT_" + Singleton<System.Random>.Instance.Next(3).ToString());
                        if (owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42020102))
                        {
                            owner.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "JUNKIES_OUT_" + Singleton<System.Random>.Instance.Next(2).ToString());
                        }
                        break;
                } 
            }
        }

        public override bool IsImmuneDmg()
        {
            return stahpBroPls;
        }
    }

    public class PassiveAbility_Aftermath_FairPlayEL2 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
            {
                if (unit.emotionDetail.MaximumEmotionLevel > 2)
                unit.emotionDetail.SetMaxEmotionLevel(2);
            }
        }
    }

    #endregion

    #region - MOBIUS OFFICE I -

    public class PassiveAbility_Aftermath_DMO_MobiusBattleSuit : PassiveAbilityBase
    {

    }

    public class PassiveAbility_Aftermath_DMO_CaduceusAugmentation : PassiveAbilityBase
    {
        private readonly List<BehaviourDetail> enduredResists = new List<BehaviourDetail>();
        private readonly BehaviourDetail[] possibleDetails = new[] { BehaviourDetail.Slash, BehaviourDetail.Penetrate, BehaviourDetail.Hit };
        public override void OnRoundStart()
        {
            enduredResists.Clear();
            var test = possibleDetails.OrderBy(x => RandomUtil.valueForProb);
            if (owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) is BattleUnitBuf_warpCharge charge && charge.UseStack(2, false))
            {
                enduredResists.AddRange(test.Take(2));
            }
            else
            {
                enduredResists.Add(test.First());
                owner.RecoverHP(2);
                owner.breakDetail.RecoverBreak(2);
            }
        }
        public override AtkResist GetResistHP(AtkResist origin, BehaviourDetail detail)
            => enduredResists.Contains(detail) ? AtkResist.Endure : base.GetResistHP(origin, detail);
    }

    public class PassiveAbility_Aftermath_DMO_CaduceusProcedure : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            int caduceus = this.owner.bufListDetail.GetKewordBufStack(KeywordBuf.WarpCharge) / 5;
            this.owner.RecoverHP(3 * caduceus);

        }
        public static string Desc = "At the start of each Scene, recover 3 HP for every 5 stacks of Charge.";
    }

    public class PassiveAbility_Aftermath_DMO_ShockTherapy : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(this.owner.faction);
            int num = 2;
            while (aliveList.Count > 0 && num > 0)
            {
                BattleUnitModel battleUnitModel = RandomUtil.SelectOne<BattleUnitModel>(aliveList);
                aliveList.Remove(battleUnitModel);
                battleUnitModel.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 1);
                num--;
            }
        }
        public static string Desc = "At the start of each Scene, give 1 Charge to 2 random other allies.";
    }

    public class PassiveAbility_Aftermath_DMO_ImpulseField : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
            {
                this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, this.owner);
            }
        }
        public static string Desc = "At the start of each Scene, if the user has Overcharge, gain 1 strength.";
    }

    public class PassiveAbility_Aftermath_DMO_QuantumGenerator : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 3, this.owner);
        }
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
            {
                BattleCardTotalResult battleCardResultLog = this.owner.battleCardResultLog;
                if (battleCardResultLog != null)
                {
                    battleCardResultLog.SetPassiveAbility(this);
                }
                int stack = RandomUtil.Range(1, 3);
                atkDice.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Paralysis, stack, this.owner);
            }
        }
        public static string Desc = "At the start of each Scene, gain 3 Charge. When hit, if the character has Overcharge, inflict 1-3 Paralysis to the attacker.";
    }

    #endregion

    #region - THE LIME LIME -

    public class PassiveAbility_Aftermath_ShimmeringDeezNutsInYoMouth : PassiveAbilityBase
    {
        public List<LorId> deck = new List<LorId>();

        public override void OnWaveStart()
        {
            base.OnWaveStart();
            deck.AddRange(owner.allyCardDetail.GetAllDeck().FindAll(x => x != null).Select(x => x.GetID()));
        }

        public override void OnKill(BattleUnitModel target)
        {
            base.OnKill(target);
            if (target.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 100))
                owner.view.DisplayDlg(LOR_XML.DialogType.SPECIAL_EVENT, "CHUN_KILL");
        }

        public override void OnRoundStart()
        {
            owner.allyCardDetail.ExhaustAllCards();
            owner.cardSlotDetail.RecoverPlayPoint(owner.cardSlotDetail.GetMaxPlayPoint());
            deck = deck.Shuffle();
            int b = deck.Count;
            for (int i = 0; i < 6; i++)
            {
                try
                {
                    owner.allyCardDetail.AddNewCard(deck[b - 1]);
                    b--;
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player).FindAll(x => x.IsImmune(KeywordBuf.Decay)))
                BattleObjectManager.instance.UnregisterUnitByIndex(Faction.Player, unit.index);
        }
    }

    public class PassiveAbility_Aftermath_Enemy_AcidicSpill : PassiveAbilityBase
    {
        bool first;

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            first = true;
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (first)
            {
                curCard.ApplyDiceAbility(DiceMatch.AllAttackDice, new DiceCardAbility_Aftermath_Erosion2AtkBoth());
                curCard.ApplyDiceStatBonus(DiceMatch.AllAttackDice, new DiceStatBonus() { power = 2 });
                first = false;
            }
            base.OnUseCard(curCard);
            
        }
    }

    public class PassiveAbility_Aftermath_AcidicSpill : PassiveAbilityBase
    {
        bool first;

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            first = true;
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (first)
            {
                curCard.ApplyDiceAbility(DiceMatch.AllAttackDice, new DiceCardAbility_Aftermath_Erosion2AtkBoth());
                first = false;
            }
            base.OnUseCard(curCard);

        }
    }

    public class PassiveAbility_Aftermath_Enemy_RapidNeutralizationProtocol : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            base.OnSucceedAttack(behavior);
            var buf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
            if (buf != null)
            {
                owner.RecoverHP(buf.stack);
                owner.breakDetail.RecoverBreak(buf.stack);
            }
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            base.OnTakeDamageByAttack(atkDice, dmg);
            var buf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
            if (buf != null && atkDice.owner != null)
            {
                atkDice.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Decay, buf.stack / 5, owner);
            }
        }
    }

    public class PassiveAbility_Aftermath_RapidNeutralizationProtocol : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            base.OnSucceedAttack(behavior);
            var buf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
            if (buf != null)
            {
                owner.RecoverHP(buf.stack / 2);
                owner.breakDetail.RecoverBreak(buf.stack / 2);
            }
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            base.OnTakeDamageByAttack(atkDice, dmg);
            var buf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
            if (buf != null && atkDice.owner != null)
            {
                atkDice.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Decay, buf.stack / 5, owner);
            }
        }
    }

    public class PassiveAbility_Aftermath_Enemy_CausticPlating : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            foreach (var unit in BattleObjectManager.instance.GetAliveList())
            {
                var buf = unit.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
                if (buf != null && owner.emotionDetail.EmotionLevel > buf.stack)
                    buf.stack = owner.emotionDetail.EmotionLevel;
                else
                    unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Decay, owner.emotionDetail.EmotionLevel, owner);
            }
        }
    }

    public class PassiveAbility_Aftermath_CausticPlating : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            var buf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
            if (buf != null && owner.emotionDetail.EmotionLevel > buf.stack)
                buf.stack = owner.emotionDetail.EmotionLevel;
            else
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Decay, owner.emotionDetail.EmotionLevel, owner);
        }
    }

    public class PassiveAbility_Aftermath_TheLimeLime : PassiveAbilityBase
    {
        bool isitthreyet;

        public override void OnWaveStart()
        {
            base.OnWaveStart();
            if (LibraryModel.Instance.GetEquipedBookList().FindAll(x => x is BookModel book && book.BookId == new LorId(AftermathCollectionInitializer.packageId, 300)).Count > 1)
            {
                owner.view.deadEvent = AftermathUtilityExtensions.ExplodeOnDeath;
                owner.Die();
            }
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (curCard.card.GetRarity() > 0 || curCard.card.XmlData.optionList.Contains(CardOption.EgoPersonal) || curCard.card.XmlData.optionList.Contains(CardOption.EGO))
            {
                owner.view.deadEvent = AftermathUtilityExtensions.ExplodeOnDeath;
                owner.Die();
            }
        }

        public override void OnLevelUpEmotion()
        {
            base.OnLevelUpEmotion();
            if (owner.emotionDetail.EmotionLevel >= 4 && !isitthreyet)
            {
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_CitrusAuraEgo());
                owner.personalEgoDetail.AddCard(new LorId(AftermathCollectionInitializer.packageId, 301));
                owner.personalEgoDetail.AddCard(new LorId(AftermathCollectionInitializer.packageId, 302));
                isitthreyet = true;
            }
        }
    }

    public class PassiveAbility_Aftermath_YouveDoneFuckedUp : PassiveAbilityBase
    {
        bool neverAgain;

        public override bool BeforeTakeBreakDamage(BattleUnitModel attacker, int dmg)
        {
            if (owner.MaxHp * 0.5 > owner.hp - dmg && !neverAgain)
            {
                owner.SetHp((int)Math.Truncate(owner.MaxHp * 0.5));
                return true;
            }
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (owner.MaxHp * 0.5 > owner.hp - dmg && !neverAgain)
            {
                owner.SetHp((int)Math.Truncate(owner.MaxHp * 0.5));
                return true;
            }
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            if (owner.MaxHp * 0.5 >= owner.hp && !owner.IsBreakLifeZero())
            {
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_CitrusAuraEgo());
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_PrimeTimeOfYourLime());
                owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 301));
                owner.breakDetail.ResetBreakDefault();
                ((PassiveAbility_Aftermath_ShimmeringDeezNutsInYoMouth)owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Aftermath_ShimmeringDeezNutsInYoMouth)).deck.Add(new LorId(AftermathCollectionInitializer.packageId, 301));
                neverAgain = true;
            }
        }
    }

    #endregion
}