using UnityEngine;
using LOR_DiceSystem;
using System.Collections.Generic;
using System;
using System.Linq;
using KeywordUtil;
using EnumExtenderV2;

namespace The_Aftermath_Collection
{
    public enum AftermathBufs
    {
        Aftermath_Spore,
        Aftermath_BurnProtection,
        Aftermath_EvanShield,
        Aftermath_StoredChems,
        Aftermath_Dem_Overcharge
    }

    // it's Spore
    public class BattleUnitBuf_Aftermath_Spore : BattleUnitBuf
    {

        public BattleUnitBuf_Aftermath_Spore()
        {  }

        public BattleUnitBuf_Aftermath_Spore(int value)
        {
            this.stack = value;
        }

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            this._owner.TakeDamage(this.stack, DamageType.Buf, null, KeywordBuf.None);
            this.stack /= 2;
            if (this.stack <= 0)
            {
                this.Destroy();
            }
        }

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            base.OnRollDice(behavior);
            if (base.IsAttackDice(behavior.Detail))
            {
                this._owner.TakeDamage(this.stack, DamageType.Buf, null, KeywordBuf.None);
            }
        }

        public override buf

        public override BufPositiveType positiveType => BufPositiveType.Negative;

        public override string keywordId => "Aftermath_Spore_Keyword";
    }

    // Cannot use Combat Pages with 1+ Dice
    public class BattleUnitBuf_Aftermath_NoCombat : BattleUnitBuf
    {
        public override bool IsCardChoosable(BattleDiceCardModel card)
        {
            if (card.GetBehaviourList().Count > 0)
            {
                return false;
            }
            return true;
        }

        public BattleUnitBuf_Aftermath_NoCombat()
        {
            stack = 0;
        }

        public override void OnAfterRollSpeedDice()
        {
            base.OnAfterRollSpeedDice();
            List<BattleUnitModel> actionableEnemyList = Singleton<StageController>.Instance.GetActionableEnemyList();
            if (_owner.faction == Faction.Player)
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
                                        if (subTargets.Exists((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == _owner))
                                        {
                                            subTargets.RemoveAll((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == _owner);
                                        }
                                        else if (battlePlayingCardDataInUnitModel.target == _owner)
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
                                        if (subTargets3.Exists((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == _owner))
                                        {
                                            subTargets3.RemoveAll((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == _owner);
                                        }
                                        if (battlePlayingCardDataInUnitModel.target == _owner)
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
                                        else if (battlePlayingCardDataInUnitModel.earlyTarget == _owner)
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
                        Debug.LogError("AftermathCollection: failed to redirect cards (Non-combatant)");
                    }
                }
                SingletonBehavior<BattleManagerUI>.Instance.ui_TargetArrow.UpdateTargetList();
            }
        }

        public override string keywordId => "AftermathBuf_NoCombat";
    }

    // Burn Protection X
    // Take X*10% less damage from Burn.
    public class BattleUnitBuf_AftermathBurnProtection : BattleUnitBuf
    {
        public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
        {
            if (keyword == KeywordBuf.Burn)
            {
                if (stack >= 10)
                {
                    return 0f;
                }
                return 1f - stack / 10f;
            }
            return base.DmgFactor(dmg, type, keyword);
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            if (stack > 11) stack = 11;
        }

        public override void OnRoundEndTheLast()
        {
            stack--;
            if (stack <= 0)
            {
                this.Destroy();
            }
        }

        public override BufPositiveType positiveType => BufPositiveType.Positive;

        public override string keywordId => "Aftermath_BurnProtection";

        public override string bufActivatedText => Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(keywordId, stack, stack * 10);
    }

    // Inner Flame
    // Used in certain Combat Pages.
    public class BattleUnitBuf_Aftermath_InnerFlame : BattleUnitBuf
    {
        public override string keywordId => "Aftermath_InnerFlame_Keyword";

        public override void OnRoundEnd()
        {
            if (stack <= 0)
            {
                this.Destroy();
            }
        }
    }

    // Delayed Light
    // Restores 1 Light next Scene and the Scene after
    public class BattleUnitBuf_DelayedLight2 : BattleUnitBuf
    {
        int scene = 2;

        public override void OnRoundStart()
        {
            if (scene <= 0) this.Destroy();
            else
            {
                scene--;
                _owner.cardSlotDetail.RecoverPlayPoint(1);
            }
        }
    }

    // All dice gain '[On Clash Lose] Inflict 1 Burn to each other' for the Scene
    public class BattleUnitBuf_OnClashLoseInflictBurn : BattleUnitBuf
    {
        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            var target = behavior.card.target;
            if (target != null)
            {
                _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 1, _owner);
                target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 1, _owner);
            }
        }

        public override void OnRoundEnd()
        {
            this.Destroy();
        }
    }

    // All dice gain '[On Hit] Inflict 1 Burn' or '[On Clash Win] Inflict 1 Burn'
    public class BattleUnitBuf_MetalGearRisingFromTheAshes : BattleUnitBuf
    {
        public override string keywordId => "MetalGearRisingFromTheAshes";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            for (int i = 0; i < stack; i++) 
            {
                if (IsDefenseDice(behavior.Detail))
                {
                    behavior.AddAbility(new DiceCardAbility_burn1atk());
                }
                else
                {
                    behavior.AddAbility(new DiceCardAbility_Burn1OnClashWin());
                }
            }
        }

        public class DiceCardAbility_Burn1OnClashWin : DiceCardAbilityBase
        {
            public override void OnWinParrying()
            {
                var target = behavior.card?.target;
                if (target != null) target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 1, owner);
            }
        }
    }

    // Reduce the next {stacks} instances of damage taken by 75% and recover 10% of Max. HP (Max. 8). Lose 1 stack upon taking damage. Stacks up to 3.
    public class BattleUnitBuf_Aftermath_EvanShield : BattleUnitBuf
    {
        public override BufPositiveType positiveType
        {
            get
            {
                return BufPositiveType.Positive;
            }
        }
        public override string keywordId
        {
            get
            {
                return "Aftermath_EvanShield";
            }
        }

        public BattleUnitBuf_Aftermath_EvanShield(BattleUnitModel model)
        {
            this._owner = model;
            this.stack = 0;
        }

        public static void GainBuf(BattleUnitModel model, int add)
        {
            BattleUnitBuf_Aftermath_EvanShield BattleUnitBuf_Aftermath_EvanShield = model.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_EvanShield && !x.IsDestroyed()) as BattleUnitBuf_Aftermath_EvanShield;
            if (BattleUnitBuf_Aftermath_EvanShield == null)
            {
                BattleUnitBuf_Aftermath_EvanShield = new BattleUnitBuf_Aftermath_EvanShield(model);
                model.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_EvanShield(model));
                BattleUnitBuf_Aftermath_EvanShield = (model.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_EvanShield) as BattleUnitBuf_Aftermath_EvanShield);
                BattleUnitBuf_Aftermath_EvanShield.Add(add);
            }
            else
            {
                BattleUnitBuf_Aftermath_EvanShield.Add(add);
            }
        }

        public static void GainReadyBuf(BattleUnitModel model, int add)
        {
            BattleUnitBuf_Aftermath_EvanShield BattleUnitBuf_Aftermath_EvanShield = model.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_EvanShield && !x.IsDestroyed()) as BattleUnitBuf_Aftermath_EvanShield;
            if (BattleUnitBuf_Aftermath_EvanShield == null)
            {
                BattleUnitBuf_Aftermath_EvanShield = new BattleUnitBuf_Aftermath_EvanShield(model);
                model.bufListDetail.AddReadyBuf(new BattleUnitBuf_Aftermath_EvanShield(model));
                BattleUnitBuf_Aftermath_EvanShield = (model.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_EvanShield) as BattleUnitBuf_Aftermath_EvanShield);
                BattleUnitBuf_Aftermath_EvanShield.Add(add);
            }
            else
            {
                BattleUnitBuf_Aftermath_EvanShield.Add(add);
            }
        }
        public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
        {
            float num = (float)this.stack;
            if (num >= 1)
            {
                int e = (int)((float)this._owner.MaxHp * 0.10f);
                if (e > 8) e = 8;
                this._owner.RecoverHP(e);
                this.Add(-1);
                return 0.25f;
            }
            return base.DmgFactor(dmg, type, keyword);
        }
        public void Add(int add)
        {
            this.stack += add;
            if (this.stack < 1)
            {
                this.Destroy();
            }
            if (this.stack >= 3)
            {
                this.stack = 3;
            }
        }

        public override void AfterDiceAction(BattleDiceBehavior behavior)
        {
            if (base.IsDestroyed())
            {
                this._owner.bufListDetail.RemoveBuf(this);
            }
        }

        public override void OnRoundEnd()
        {
            if (base.IsDestroyed())
            {
                this._owner.bufListDetail.RemoveBuf(this);
            }
            else
            {
                BattleUnitBuf battleUnitBuf = this._owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_EvanShield);
                if (battleUnitBuf != null && battleUnitBuf.stack > 0)
                {
                    battleUnitBuf.Destroy();
                }
            }
        }
    }

    // Thunderstruck
    public class BattleUnitBuf_Aftermath_Thunderstruck : BattleUnitBuf
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (stack > 10) stack = 10;
            if (Singleton<System.Random>.Instance.Next(0, 10) < stack) behavior.ApplyDiceStatBonus(new DiceStatBonus { power = -2 });
        }

        public override void OnRoundEnd()
        {
            this.Destroy();
        }

        public override BufPositiveType positiveType => BufPositiveType.Negative;

        public override string keywordId => "Aftermath_Thunderstruck";

        public override string bufActivatedText => Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(keywordId, stack, stack * 10);
    }

    // Stored Chems
    public class BattleUnitBuf_Aftermath_StoredChems : BattleUnitBuf
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            if (this.stack < 0)
            {
                this.Destroy();
            }
        }

        public override string keywordId => "Aftermath_StoredChems";
    }

    // Rebound (Chem)
    public class BattleUnitBuf_Aftermath_ChemRebound : BattleUnitBuf
    {
        public override int MaxPlayPointAdder()
        {
            return this.stack;
        }

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            base.OnRollDice(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus { dmg = -Singleton<System.Random>.Instance.Next(1, stack + 3) });
        }

        public override string keywordId => "Aftermath_ChemRebound";

        public override string bufActivatedText => Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(keywordId, stack, stack + 2);
    }

    // Overcharge (Mobius Office)
    public class BattleUnitBuf_Aftermath_Overcharge : BattleUnitBuf
    {
        public BattleUnitBuf_Aftermath_Overcharge(int value)
        {
            this.stack = value;
        }
        public const int maxValue = 10;

        public override string keywordIconId
        {
            get
            {
                return "Aftermath_Overcharge";
            }
        }

        public override string keywordId => "Aftermath_Dem_Overcharge";

        public BattleUnitBuf_Aftermath_Overcharge()
        {
            stack = 0;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior != null && behavior.Type == BehaviourType.Atk)
            {
                if (UnityEngine.Random.Range(0, 100) >= 50)
                {
                    _owner.TakeDamage(stack, DamageType.Buf);
                }
                behavior.ApplyDiceStatBonus(new DiceStatBonus { dmg = stack });
            }
        }

        public override void OnAddBuf(int addedStack)
        {
            if (addedStack > 0)
                stack += addedStack;
            stack = Mathf.Clamp(stack, 0, maxValue);
        }

        public override void OnRoundEnd()
        {
            if (stack <= 0)
            {
                Destroy();
            }
        }

        public bool UseStack(int v)
        {
            if (stack < v)
            {
                return false;
            }
            stack -= v;
            return true;
        }
    }

    // Based
    public class BattleUnitBuf_Aftermath_Based : BattleUnitBuf
    {
        public override bool IsImmuneDmg(DamageType type, KeywordBuf keyword = KeywordBuf.None)
        {
            if (keyword == KeywordBuf.Decay)
                return true;
            return base.IsImmuneDmg(type, keyword);
        }

        public override float BreakDmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
        {
            if (keyword == KeywordBuf.Decay)
                return -999f;
            return base.DmgFactor(dmg, type, keyword);      
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            stack--;
            if (stack < 1) this.Destroy();
        }

        public override string keywordId => "Aftermath_Basic";
    }

    // Citrus EGO's Aura
    public class BattleUnitBuf_Aftermath_CitrusAuraEgo : BattleUnitBuf
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            foreach (var unit in BattleObjectManager.instance.GetAliveList().FindAll(x => !x.bufListDetail.GetActivatedBufList().Any(y => y is BattleUnitBuf_Aftermath_CitrusAuraEgoHidden)))
                unit.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_CitrusAuraEgoHidden());
        }

        public BattleUnitBuf_Aftermath_CitrusAuraEgo()
        {
            stack = 0;
        }

        public override string keywordId => "Aftermath_CitrusAuraEgo";

        public class BattleUnitBuf_Aftermath_CitrusAuraEgoHidden : BattleUnitBuf
        {
            public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
            {
                if (keyword == KeywordBuf.Decay)
                    return 2f;
                return base.DmgFactor(dmg, type, keyword);
            }


            public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
            {
                base.OnTakeDamageByAttack(atkDice, dmg);
                var buf = _owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
                if (buf != null)
                {
                    buf.stack /= 2;
                }
            }

            public override void OnRoundEndTheLast()
            {
                var buf = _owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
                if (buf != null) buf.stack /= 2;
                base.OnRoundEndTheLast();
            }
        }
    }

    // The Prime Time of Your Lime
    public class BattleUnitBuf_Aftermath_PrimeTimeOfYourLime : BattleUnitBuf
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
            var skill = ((DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage)behavior.abilityList.Find(x => x is DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage));
            if (behavior.Type == BehaviourType.Atk && !behavior.IsParrying() && skill == null)
            {
                behavior.AddAbility(new DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage());
                behavior.ApplyDiceStatBonus(new DiceStatBonus { dmgRate = -50, power = 1 });
            }
        }

        public BattleUnitBuf_Aftermath_PrimeTimeOfYourLime()
        {
            stack = 0;
        }

        public class DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage : DiceCardAbilityBase
        {
            public bool hasRerolled;

            public override void OnRollDice()
            {
                base.OnRollDice();
                if (!hasRerolled)
                {
                    ActivateBonusAttackDice();
                    hasRerolled = true;
                }
            }
        }

        public override string keywordIconId => "Aftermath_CitrusAuraEgo";

        public override string keywordId => "Aftermath_PrimeTimeOfYourLime";
    }

    // The Prime Time of Your Lime (one scene)
    public class BattleUnitBuf_Aftermath_PrimeTimeOfYourLimeOneScene : BattleUnitBuf
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
            var skill = ((BattleUnitBuf_Aftermath_PrimeTimeOfYourLime.DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage)behavior.abilityList.Find(x => x is BattleUnitBuf_Aftermath_PrimeTimeOfYourLime.DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage));
            if (behavior.Type == BehaviourType.Atk && !behavior.IsParrying() && skill == null)
            {
                behavior.AddAbility(new BattleUnitBuf_Aftermath_PrimeTimeOfYourLime.DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage());
                behavior.ApplyDiceStatBonus(new DiceStatBonus { dmgRate = -50, power = 1});
            }
        }

        public BattleUnitBuf_Aftermath_PrimeTimeOfYourLimeOneScene()
        {
            stack = 0;
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            this.Destroy();
        }

        public override string keywordIconId => "Aftermath_CitrusAuraEgo";

        public override string keywordId => "Aftermath_PrimeTimeOfYourLime";
    }
}