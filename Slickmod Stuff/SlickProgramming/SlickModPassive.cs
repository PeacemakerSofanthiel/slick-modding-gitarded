using System;
using LOR_DiceSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using HyperCard;

namespace SlickRuinaMod
{
    #region --GENERAL PURPOSE--

    // Heightened Emotions I
    // Start the reception at Emotion Level 1.
    public class PassiveAbility_SlickMod_StartEmoLevel1 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            for (int i = 0; i <= 1 - this.owner.emotionDetail.EmotionLevel; i++)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
                this.owner.emotionDetail.CheckLevelUp();
            }
        }
    }

    // Heightened Emotions II
    // Start the reception at Emotion Level 2.
    public class PassiveAbility_SlickMod_StartEmoLevel2 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            for (int i = 0; i <= 2 - this.owner.emotionDetail.EmotionLevel; i++)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
                this.owner.emotionDetail.CheckLevelUp();
            }
        }
    }

    // Heightened Emotions III
    // Start the reception at Emotion Level 3.
    public class PassiveAbility_SlickMod_StartEmoLevel3 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            for (int i = 0; i <= 3 - this.owner.emotionDetail.EmotionLevel; i++)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
                this.owner.emotionDetail.CheckLevelUp();
            }
        }
    }

    // Heightened Emotions IV
    // Start the reception at Emotion Level 4.
    public class PassiveAbility_SlickMod_StartEmoLevel4 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            for (int i = 0; i <= 4 - this.owner.emotionDetail.EmotionLevel; i++)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
                this.owner.emotionDetail.CheckLevelUp();
            }
        }
    }

    // Heightened Emotions V
    // Start the reception at Emotion Level 5.
    public class PassiveAbility_SlickMod_StartEmoLevel5 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            for (int i = 0; i <= 5 - this.owner.emotionDetail.EmotionLevel; i++)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
                this.owner.emotionDetail.CheckLevelUp();
            }
        }
    }

    // Posthaste
    // Gain an additional Speed Die at Emotion Level 2.
    public class PassiveAbility_SlickMod_HalfSpeed : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            BattleUnitModel owner = this.owner;
            int num1;
            if (owner == null)
            {
                num1 = 0;
            }
            else
            {
                int? emotionLevel = owner.emotionDetail?.EmotionLevel;
                int num2 = 2;
                num1 = emotionLevel.GetValueOrDefault() >= num2 & emotionLevel.HasValue ? 1 : 0;
            }
            return num1 != 0 ? 1 : 0;
        }
    }

    // Speed IV
    // Speed Dice Slot +2. Gain an additional Speed Die at Emotion Level 3.
    public class PassiveAbility_SlickMod_Speed4 : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            BattleUnitModel owner = this.owner;
            int num1;
            if (owner == null)
            {
                num1 = 0;
            }
            else
            {
                int? emotionLevel = owner.emotionDetail?.EmotionLevel;
                int num2 = 3;
                num1 = emotionLevel.GetValueOrDefault() >= num2 & emotionLevel.HasValue ? 1 : 0;
            }
            return num1 != 0 ? 3 : 2;
        }
    }

    // Speed X
    // Speed Dice Slot +1. Gain an additional Speed Die at each Emotion Level (Except 4).
    public class PassiveAbility_SlickMod_Speed10 : PassiveAbilityBase
    {
        // Token: 0x06002F55 RID: 12117 RVA: 0x000E9ABC File Offset: 0x000E7CBC
        public override int SpeedDiceNumAdder()
        {
            if (this.owner.emotionDetail.EmotionLevel >= 4)
            {
                return this.owner.emotionDetail.EmotionLevel;
            }


            else
            {
                return 1 + this.owner.emotionDetail.EmotionLevel;
            }
        }
    }

    // Delayed Speed I
    // Speed Dice Slot +1. Gain an additional Speed die on the third Scene. (Cannot Overlap)
    public class PassiveAbility_SlickMod_DelayedSpeed1 : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            int num = 1;
            if (Singleton<StageController>.Instance.RoundTurn >= 3)
            {
                num++;
            }
            return num;
        }
    }

    // Delayed Speed II
    // Speed Dice Slot +2. Gain an additional Speed die on the third Scene. (Cannot Overlap)
    public class PassiveAbility_SlickMod_DelayedSpeed2 : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            int num = 2;
            if (Singleton<StageController>.Instance.RoundTurn >= 3)
            {
                num++;
            }
            return num;
        }
    }

    // Sparkling
    // Restore 1 additional Light and draw 1 additional page at the start of each Scene.
    public class PassiveAbility_SlickMod_Sparkling : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            this.owner.cardSlotDetail.RecoverPlayPointByCard(1);
            this.owner.allyCardDetail.DrawCards(1);
        }

    }

    // Flow State (Player Ver. 1)
    // Upon winning 3 or more clashes in a Scene, enter the Flow State next Scene.
    // When hit, reduce incoming damage and Stagger damage by 2-3, then leave the Flow State.
    public class PassiveAbility_SlickMod_Flow1Player : PassiveAbilityBase
    {
        // Counts number of clashes won, then gives Flow State next scene if 3 or more clashes are won
        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            this.count++;
            bool flag = this.count >= 3;
            if (flag)
            {
                this.count -= 3;
                bool flag2 = this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_FlowState) == null;
                if (flag2)
                {
                    this.owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_SlickMod_FlowState());
                }
            }
        }

        // Sets clash win count to 0 at end of turn
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            this.count = 0;
        }

        // Reduces damage by 2-3 while in Flow State
        public override int GetDamageReduction(BattleDiceBehavior behavior)
        {
            BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_FlowState);
            bool flag = battleUnitBuf != null;
            int result;
            if (flag)
            {
                result = RandomUtil.Range(2, 3);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        // Reduces Stagger damage by 2-3 while in Flow State
        public override int GetBreakDamageReduction(BattleDiceBehavior behavior)
        {
            BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_FlowState);
            bool flag = battleUnitBuf != null;
            int result;
            if (flag)
            {
                result = RandomUtil.Range(2, 3);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        // Lose Flow State upon being hit
        public override void AfterTakeDamage(BattleUnitModel attacker, int dmg)
        {
            base.AfterTakeDamage(attacker, dmg);
            bool flag = attacker == null;
            if (flag)
            {
                this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_FlowState).Destroy();
            }
        }

        private int count = 0;
    }

    // Flow State (Enemy Ver. 1)
    // Upon landing 3 or more hits in a Scene, enter the Flow State next Scene.
    // When hit, reduce incoming damage and Stagger damage by 5, then leave the Flow State.
    public class PassiveAbility_SlickMod_Flow1Enemy : PassiveAbilityBase
    {
        // Counts number of successful hits, then gives Flow State next scene if 3 or more hits hit eks dee
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            this.count++;
            bool flag = this.count >= 3;
            if (flag)
            {
                this.count -= 3;
                bool flag2 = this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_FlowState) == null;
                if (flag2)
                {
                    this.owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_SlickMod_FlowState());
                }
            }
        }

        // Sets count to 0 at the end of the scene
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            this.count = 0;
        }

        // Reduces incoming damage by 5 while in the Flow State
        public override int GetDamageReduction(BattleDiceBehavior behavior)
        {
            BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_FlowState);
            bool flag = battleUnitBuf != null;
            int result;
            if (flag)
            {
                result = 5;
            }
            else
            {
                result = 0;
            }
            return result;
        }

        // Reduces incoming Stagger damage by 5 while in the Flow State
        public override int GetBreakDamageReduction(BattleDiceBehavior behavior)
        {
            BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_FlowState);
            bool flag = battleUnitBuf != null;
            int result;
            if (flag)
            {
                result = 5;
            }
            else
            {
                result = 0;
            }
            return result;
        }

        // lose Flow State upon being hit
        public override void AfterTakeDamage(BattleUnitModel attacker, int dmg)
        {
            base.AfterTakeDamage(attacker, dmg);
            bool flag = attacker == null;
            if (flag)
            {
                this.owner.bufListDetail.GetReadyBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_FlowState).Destroy();
            }
        }

        private int count = 0;
    }

    // Slash Up 1
    // Slash Dice Power +1
    public class PassiveAbility_SlickMod_SlashUp1 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Slash)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 1
            });
        }
    }

    // Slash Up 2
    // Slash Dice Power +2
    public class PassiveAbility_SlickMod_SlashUp2 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Slash)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 2
            });
        }
    }

    // Ame-no-Habakiri
    // Slash Dice Power +3
    public class PassiveAbility_SlickMod_SlashUp3 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Slash)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 3
            });
        }
    }

    // Wedge
    // Pierce Dice Power +1
    public class PassiveAbility_SlickMod_PierceUp1 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Penetrate)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 1
            });
        }
    }

    // Allas Prowess
    // Pierce Dice Power +2
    public class PassiveAbility_SlickMod_PierceUp2 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Penetrate)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 2
            });
        }
    }

    // Amenonuhoko
    // Pierce Dice Power +3
    public class PassiveAbility_SlickMod_PierceUp3 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Penetrate)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 3
            });
        }
    }

    // Hammer Down
    // Blunt Dice Power +1
    public class PassiveAbility_SlickMod_BluntUp1 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Hit)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 1
            });
        }
    }

    // Masterpiece
    // Blunt Dice Power +2
    public class PassiveAbility_SlickMod_BluntUp2 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Hit)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 2
            });
        }
    }

    // Uchide-no-Kozuchi
    // Blunt Dice Power +3
    public class PassiveAbility_SlickMod_BluntUp3 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Hit)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 3
            });
        }
    }

    // Steel Star
    // Block Dice Power +2
    public class PassiveAbility_SlickMod_BlockUp2 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Guard)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 2
            });
        }
    }

    // Evasive
    // Evade Dice Power +1
    public class PassiveAbility_SlickMod_EvadeUp1 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Evasion)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 1
            });
        }
    }

    // Evasive II
    // Evade Dice Power +2
    public class PassiveAbility_SlickMod_EvadeUp2 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Evasion)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 2
            });
        }
    }

    // Higher Caliber
    // Ranged Dice Power +1
    public class PassiveAbility_SlickMod_RangedUp1 : PassiveAbilityBase
    {
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            if (behavior.card.card.GetSpec().Ranged != CardRange.Far)
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 1
            });
        }
    }

    // Defense Up 2
    // Defensive Dice Power +2
    public class PassiveAbility_SlickMod_DefenseUp2 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (IsDefenseDice(behavior.Detail))
            {
                this.owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 2
                });
            }
        }
    }

    // Counter Up 1
    // Counter Dice Power +1
    public class PassiveAbility_SlickMod_CounterUp1 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Type == (BehaviourType)2)
            {
                this.owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
        }
    }

    // Counter Up 2
    // Counter Dice Power +2
    public class PassiveAbility_SlickMod_CounterUp2 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Type == (BehaviourType)2)
            {
                this.owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 2
                });
            }
        }
    }

    // Combat Mastery
    // Dice Power +1
    public class PassiveAbility_SlickMod_AllDiceUp1 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 1
            });
        }
    }

    // Firestarter
    // Masterful Smoker but for Burn
    public class PassiveAbility_SlickMod_BurnMastery : PassiveAbilityBase
    {
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (!this.CheckCondition(curCard))
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
            {
                power = 1
            });
        }

        private bool CheckCondition(BattlePlayingCardDataInUnitModel card)
        {
            if (card == null)
                return false;
            DiceCardXmlInfo xmlData = card.card.XmlData;
            if (xmlData == null)
                return false;
            if (xmlData.Keywords.Contains("Burn_Keyword"))
                return true;
            List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
            for (int index = 0; index < abilityKeywords.Count; ++index)
            {
                if (abilityKeywords[index] == "Burn_Keyword")
                    return true;
            }
            foreach (DiceBehaviour behaviour in card.card.GetBehaviourList())
            {
                List<string> keywordsByScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(behaviour.Script);
                for (int index = 0; index < keywordsByScript.Count; ++index)
                {
                    if (keywordsByScript[index] == "Burn_Keyword")
                        return true;
                }
            }
            return false;
        }
    }

    // Bloodshed
    // Masterful Smoker but for Bleed
    public class PassiveAbility_SlickMod_BleedMastery : PassiveAbilityBase
    {
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (!this.CheckCondition(curCard))
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
            {
                power = 1
            });
        }

        private bool CheckCondition(BattlePlayingCardDataInUnitModel card)
        {
            if (card == null)
                return false;
            DiceCardXmlInfo xmlData = card.card.XmlData;
            if (xmlData == null)
                return false;
            if (xmlData.Keywords.Contains("Bleed"))
                return true;
            List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
            for (int index = 0; index < abilityKeywords.Count; ++index)
            {
                if (abilityKeywords[index] == "Bleed")
                    return true;
            }
            foreach (DiceBehaviour behaviour in card.card.GetBehaviourList())
            {
                List<string> keywordsByScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(behaviour.Script);
                for (int index = 0; index < keywordsByScript.Count; ++index)
                {
                    if (keywordsByScript[index] == "Bleed")
                        return true;
                }
            }
            return false;
        }
    }

    // Gigavoltage
    // Masterful Smoker but for Charge
    public class PassiveAbility_SlickMod_ChargeMastery : PassiveAbilityBase
    {
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (!this.CheckCondition(curCard))
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
            {
                power = 1
            });
        }

        private bool CheckCondition(BattlePlayingCardDataInUnitModel card)
        {
            if (card == null)
                return false;
            DiceCardXmlInfo xmlData = card.card.XmlData;
            if (xmlData == null)
                return false;
            if (xmlData.Keywords.Contains("WarpCharge") || xmlData.Name.Contains("충전"))
                return true;
            List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
            for (int index = 0; index < abilityKeywords.Count; ++index)
            {
                if (abilityKeywords[index] == "WarpCharge")
                    return true;
            }
            foreach (DiceBehaviour behaviour in card.card.GetBehaviourList())
            {
                List<string> keywordsByScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(behaviour.Script);
                for (int index = 0; index < keywordsByScript.Count; ++index)
                {
                    if (keywordsByScript[index] == "WarpCharge")
                        return true;
                }
            }
            return false;
        }
    }

    // Lonely At The Top
    // At the end of each Scene, gain 2 Strength, Endurance and Haste if no other allies are present.
    public class PassiveAbility_SlickMod_LonelyAtTheTop : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (BattleObjectManager.instance.GetAliveList(this.owner.faction).Exists((Predicate<BattleUnitModel>)(x => x != this.owner)))
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 2, this.owner);
            this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 2, this.owner);
            this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 2, this.owner);
        }
    }

    // Final Sentinel
    // At the start of each Scene, gain 3 Protection and Stagger Protection if no other allies are present.
    public class PassiveAbility_SlickMod_FinalSentinel : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            if (BattleObjectManager.instance.GetAliveList(this.owner.faction).Exists((Predicate<BattleUnitModel>)(x => x != this.owner)))
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, 3, this.owner);
            this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.BreakProtection, 3, this.owner);
        }
    }

    #endregion

    #region --COURIERS 1--

    // Pony Express
    // At the end of the Scene, if all allies are alive, gain 1 Haste and 1 Cycle next Scene; if all other allies are dead, inflict 2 Bind on self instead.
    // This passive ability is only active if the Act started with one or more other allies.
    public class PassiveAbility_SlickMod_PonyExpress : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {

            if (owner.IsDead())
            {
                return;
            }
            int count = BattleObjectManager.instance.GetList(owner.faction).Count;
            int count2 = BattleObjectManager.instance.GetAliveList(owner.faction).Count;
            if (count > 1)
            {
                owner.battleCardResultLog?.SetPassiveAbility(this);
                if (count == count2)
                {
                    owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 1, base.Owner);
                    this.owner.bufListDetail.AddKeywordBufByEtc(MyKeywordBufs.SlickMod_Cycle, 1, owner);
                }
                if (count2 == 1)
                {
                    owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Binding, 2, base.Owner);
                }
            }
        }
    }

    // Special Delivery!
    // While the character has Cycle, Blunt and Defensive dice gain +1 Power.
    public class PassiveAbility_SlickMod_SpecialDelivery : PassiveAbilityBase
    {
        // Defensive and Blunt dice gain +1 Power
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            if (CheckCondition() && IsDefenseDice(behavior.Detail))
            {
                this.owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior?.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
            if (CheckCondition() && behavior.Detail == BehaviourDetail.Hit)
            {
                this.owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
        }

        // if user has Cycle
        private bool CheckCondition()
        {
            BattleUnitModel battleUnitModel = owner;
            if (battleUnitModel != null && battleUnitModel.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_Cycle)?.stack > 0)
            {
                return true;
            }
            return false;
        }
    }

    // Same-Day Delivery
    // If 3 or fewer pages are in-hand at the end of the Scene, gain 1 Haste and Cycle next Scene.
    public class PassiveAbility_SlickMod_SameDayDelivery : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (owner.allyCardDetail.GetHand().Count <= 3)
            {
                owner.ShowPassiveTypo(this);
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 1, owner);
                this.owner.bufListDetail.AddKeywordBufByEtc(MyKeywordBufs.SlickMod_Cycle, 1, owner);

            }
        }
    }

    #endregion

    #region --SNOW COYOTE OFFICE--

    // Arctic Drift
    // Offensive dice on the first Combat Page the character uses each Scene gain an additional effect:
    // ‘[On Hit] Inflict 1 Bind to each other next Scene’
    public class PassiveAbility_SlickMod_ArcticDrift : PassiveAbilityBase
    {
        private bool isFirstUseCard;


        public override void OnRoundStart()
        {
            base.OnRoundStart();
            isFirstUseCard = false;
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (curCard != null && !isFirstUseCard)
            {
                this.owner.battleCardResultLog?.SetPassiveAbility(this);
                curCard.ApplyDiceAbility(DiceMatch.AllAttackDice, new DiceCardAbility_SlickMod_Mutual1Bind());
                isFirstUseCard = true;
            }
        }
    }

    // Steady
    // Take 2 less damage and Stagger damage from enemies with higher Speed.
    public class PassiveAbility_SlickMod_Steady : PassiveAbilityBase
    {
        public override int GetDamageReduction(BattleDiceBehavior behavior)
        {
            if (behavior.card.speedDiceResultValue > behavior.card.target.speedDiceResult[behavior.card.targetSlotOrder].value)
            {
                return 2;
            }
            return 0;
        }
        public override int GetBreakDamageReduction(BattleDiceBehavior behavior)
        {
            if (behavior.card.speedDiceResultValue > behavior.card.target.speedDiceResult[behavior.card.targetSlotOrder].value)
            {
                return 2;
            }
            return 0;
        }
    }

    // Vengeance Call
    // If an ally dies, restore 1 Light and gain a Speed die next Scene.
    public class PassiveAbility_SlickMod_VengeanceCall : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            if (this.hasdied)
            {
                this.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddedSpeedDie());
                this.owner.cardSlotDetail.RecoverPlayPoint(1);
            }
            base.OnRoundStart();
            this.hasdied = false;
        }

        public override void OnDieOtherUnit(BattleUnitModel unit)
        {
            if (unit.faction == this.owner.faction)
            {
                this.hasdied = true;
            }
        }

        private bool hasdied;

    }

    // Aurora Workshop Firearm
    // Ranged Attack Stagger Damage +2
    public class PassiveAbility_SlickMod_AuroraBorealis : PassiveAbilityBase
    {
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            if (behavior.card.card.GetSpec().Ranged == CardRange.Far)
            {
                this.owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    breakDmg = 2
                });
            }
        }
    }

    // Do I Wanna Know?
    // Choose the Speed dice with the lowest value; the Speed values of the dice become 2.
    public class PassiveAbility_SlickMod_TurnerTrolling : PassiveAbilityBase
    {
        public override void OnRollSpeedDice()
        {
            int minValue = 2;
            foreach (SpeedDice item in owner.speedDiceResult)
            {
                if (item.value < minValue)
                {
                    minValue = item.value;
                }
            }
            foreach (SpeedDice item2 in owner.speedDiceResult.FindAll((SpeedDice x) => x.value == minValue))
            {
                item2.value = 2;
            }
            owner.speedDiceResult.Sort(delegate (SpeedDice d1, SpeedDice d2)
            {
                if (d1.breaked && d2.breaked)
                {
                    if (d1.value > d2.value)
                    {
                        return -1;
                    }
                    if (d1.value < d2.value)
                    {
                        return 1;
                    }
                    return 0;
                }
                if (d1.breaked && !d2.breaked)
                {
                    return -1;
                }
                if (!d1.breaked && d2.breaked)
                {
                    return 1;
                }
                if (d1.value > d2.value)
                {
                    return -1;
                }
                return (d1.value < d2.value) ? 1 : 0;
            });
        }
    }

    #endregion

    #region --INFERNAL CORPS 1--

    // Inferno Templar
    // Pierce dice on Melee pages gain +1 Power and deal +2 damage.
    public class PassiveAbility_SlickMod_InfernalInfernoTemplar : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            if (behavior.card.card.GetSpec().Ranged == CardRange.Near && behavior.Detail == BehaviourDetail.Penetrate)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1,
                    dmg = 2
                });
            }
        }
    }

    // Inferno Corps
    // Pierce Power +1; Pierce dice on Ranged pages deal +1 damage.
    public class PassiveAbility_SlickMod_InfernalInfernoCorps : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            if (behavior.Detail == BehaviourDetail.Penetrate)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
            if (behavior.card.card.GetSpec().Ranged == CardRange.Far && behavior.Detail == BehaviourDetail.Penetrate)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    dmg = 1
                });
            }
        }
    }

    // Experimental Plate B1
    // When hit, while this character has Overheat, inflict 1 Burn to the attacker
    public class PassiveAbility_SlickMod_InfernalExperimentalPlateB1 : PassiveAbilityBase
    {
        
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_InfernalOverheat) != null)
            {
                atkDice.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 1, owner);
            }
        }
    }

    // Temperamental Weapon
    // Upon a successful attack, if this character has Overheat, inflict 1 Burn
    public class PassiveAbility_SlickMod_InfernalTemperamental : PassiveAbilityBase
    {

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_InfernalOverheat) != null)
            {
                behavior.card.target?.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 1, owner);
            }
        }
    }

    // Experimental Plate D4
    // While this character has Overheat, gain two Counter dice (Block, 3-6) at combat start.
    public class PassiveAbility_SlickMod_InfernalCounterBlock : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            if (base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_InfernalOverheat) != null)
            {
                DiceBehaviour diceBehaviour = new DiceBehaviour
                {
                    Min = 3,
                    Dice = 6,
                    Type = BehaviourType.Standby,
                    Detail = BehaviourDetail.Guard,
                    EffectRes = "Liu1_G"
                };
                DiceCardXmlInfo cardInfo = new DiceCardXmlInfo(new LorId(-1))
                {
                    Artwork = "Dawn5",
                    Rarity = Rarity.Special,
                    DiceBehaviourList = new List<DiceBehaviour>
                    {
                        diceBehaviour,
                    },
                    Chapter = 5,
                    Priority = 0,
                    isError = true
                };
                BattleDiceBehavior battleDiceBehavior = new BattleDiceBehavior();
                battleDiceBehavior.behaviourInCard = diceBehaviour.Copy();
                battleDiceBehavior.SetIndex(0);
                this.owner.cardSlotDetail.keepCard.AddBehaviours(cardInfo, new List<BattleDiceBehavior>
                {
                    battleDiceBehavior,
                    battleDiceBehavior
                });
            }
        }
    }

    #endregion

    #region --UN GOLDEN SPARK--

    // Beginnings of Samsara
    // Untransferable. "Speed Break" becomes accessible, and can be used by spending Samsara.
    // At the start of each Scene, gain Samsara equal to the amount of Haste on self. Upon using a Combo Finisher page, gain Samsara equal to its original Cost. Upon a successful evade, gain 1 Samsara.
    public class PassiveAbility_SlickMod_SparkSamsaraGaming : PassiveAbilityBase
    {
        // Adds Speed Break to Ego Hand
        public override void OnWaveStart()
        {
            owner.personalEgoDetail.AddCard(new LorId("SlickMod", 0504011));
        }

        // Gains Samsara equal to Haste at the start of the Scene
        public override void OnRoundStartAfter()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_SparkSamsara, this.owner.bufListDetail.GetKewordBufStack(KeywordBuf.Quickness), base.owner);
        }

        // Gains Samsara equal to the original Cost of Combo Finishers
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (CheckCondition(curCard.card))
            {
                this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_SparkSamsara, curCard.card.GetOriginCost(), this.owner);
            }
        }

        // Gains 1 Samsara upon clash win using Evade
        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Evasion)
            {
                base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_SparkSamsara, 1, base.owner);
            }
        }

        #region - Combo Finisher keyword check -
        private bool CheckCondition(BattleDiceCardModel card)
        {
            if (card == null)
                return false;
            DiceCardXmlInfo xmlData = card.XmlData;
            if (xmlData == null)
                return false;
            if (xmlData.Keywords.Contains("SlickMod_ComboFinisher"))
                return true;
            List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
            for (int index = 0; index < abilityKeywords.Count; ++index)
            {
                if (abilityKeywords[index] == "SlickMod_ComboFinisher")
                    return true;
            }
            foreach (DiceBehaviour behaviour in card.GetBehaviourList())
            {
                List<string> keywordsByScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(behaviour.Script);
                for (int index = 0; index < keywordsByScript.Count; ++index)
                {
                    if (keywordsByScript[index] == "SlickMod_ComboFinisher")
                        return true;
                }
            }
            return false;
        }
        #endregion
    }

    // lol
    // lmao
    public class PassiveAbility_SlickMod_ComboFinisherPower : PassiveAbilityBase
    {
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (!this.CheckCondition(curCard))
                return;
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
            {
                power = 1
            });
        }

        #region - Combo Finisher keyword check -
        private bool CheckCondition(BattlePlayingCardDataInUnitModel card)
        {
            if (card == null)
                return false;
            DiceCardXmlInfo xmlData = card.card.XmlData;
            if (xmlData == null)
                return false;
            if (xmlData.Keywords.Contains("SlickMod_ComboFinisher"))
                return true;
            List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
            for (int index = 0; index < abilityKeywords.Count; ++index)
            {
                if (abilityKeywords[index] == "SlickMod_ComboFinisher")
                    return true;
            }
            foreach (DiceBehaviour behaviour in card.card.GetBehaviourList())
            {
                List<string> keywordsByScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(behaviour.Script);
                for (int index = 0; index < keywordsByScript.Count; ++index)
                {
                    if (keywordsByScript[index] == "SlickMod_ComboFinisher")
                        return true;
                }
            }
            return false;
        }
        #endregion
    }

    // Combo Master
    // Reduce the Cost of Combo Finisher pages by 1.
    public class PassiveAbility_SlickMod_ComboFinisherMastery : PassiveAbilityBase
    {
        public class BattleDiceCardBuf_CFMasteryCostDown : BattleDiceCardBuf
        {
            public override int GetCost(int oldCost)
            {
                return oldCost - 1;
            }

            public override void OnUseCard(BattleUnitModel owner)
            {
                Destroy();
            }
        }

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            foreach (BattleDiceCardModel thingy in this.owner.personalEgoDetail.GetHand())
            {
                if (thingy != null && CheckCondition(thingy))
                {
                    if (!thingy.HasBuf<BattleDiceCardBuf_CFMasteryCostDown>())
                    {
                        thingy.AddBuf(new BattleDiceCardBuf_CFMasteryCostDown());
                    }
                }
            }

            base.OnRoundStart();
            foreach (BattleDiceCardModel thingy in this.owner.allyCardDetail.GetHand())
            {
                if (thingy != null && CheckCondition(thingy))
                {
                    if (!thingy.HasBuf<BattleDiceCardBuf_CFMasteryCostDown>())
                    {
                        thingy.AddBuf(new BattleDiceCardBuf_CFMasteryCostDown());
                    }
                }
            }

        }

        // I;m stuff

        #region - Combo Finisher keyword check -
        private bool CheckCondition(BattleDiceCardModel card)
        {
            if (card == null)
                return false;
            DiceCardXmlInfo xmlData = card.XmlData;
            if (xmlData == null)
                return false;
            if (xmlData.Keywords.Contains("SlickMod_ComboFinisher"))
                return true;
            List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
            for (int index = 0; index < abilityKeywords.Count; ++index)
            {
                if (abilityKeywords[index] == "SlickMod_ComboFinisher")
                    return true;
            }
            foreach (DiceBehaviour behaviour in card.GetBehaviourList())
            {
                List<string> keywordsByScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(behaviour.Script);
                for (int index = 0; index < keywordsByScript.Count; ++index)
                {
                    if (keywordsByScript[index] == "SlickMod_ComboFinisher")
                        return true;
                }
            }
            return false;
        }
        #endregion
    }

    // Former Shi Fixer
    // All dice gain +1 Power if an ally died this Act.
    public class PassiveAbility_SlickMod_FormerShiFixer : PassiveAbilityBase
    {
        private int _stack;

        public override void OnWaveStart()
        {
            _stack = 0;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (_stack > 0)
            {
                this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus()
                {
                    power = 1
                });
            }
        }

        public override void OnDieOtherUnit(BattleUnitModel unit)
        {
            if (unit.faction == owner.faction && _stack < 1)
            {
                _stack++;
            }
        }
    }

    // Enemy Spark Passive
    // Goes gamer mode upon taking lethal damage
    public class PassiveAbility_SlickMod_SparkUnwavering : PassiveAbilityBase
    {
        public bool IsActivated
        {
            get
            {
                return this._activated;
            }
        }

        public override bool isHide
        {
            get
            {
                return this._activated;
            }
        }

        public override void OnWaveStart()
        {
            this._activated = false;
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            base.OnRoundEndTheLast_ignoreDead();
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            bool activated = this._activated;
            bool result;
            if (activated)
            {
                result = false;
            }
            else
            {
                bool flag = this.owner.hp <= (float)dmg;
                if (flag)
                {
                    this._activated = true;
                    this.owner.bufListDetail.AddBuf(new PassiveAbility_SlickMod_SparkUnwavering.UnwaverBuf());
                    this.owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_OppSpeedBreak());

                }
                result = false;
            }
            return result;
        }

        private bool _activated;

        // Gains Samsara equal to Haste at the start of the Scene
        public override void OnRoundStartAfter()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_SparkSamsara, this.owner.bufListDetail.GetKewordBufStack(KeywordBuf.Quickness), base.owner);
        }

        // Gains Samsara equal to the original Cost of Combo Finishers
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (CheckCondition(curCard.card))
            {
                this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_SparkSamsara, curCard.card.GetOriginCost(), this.owner);
            }
        }

        // Gains 1 Samsara upon clash win using Evade
        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Evasion)
            {
                base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_SparkSamsara, 1, base.owner);
            }
        }

        public class UnwaverBuf : BattleUnitBuf
        {
            public UnwaverBuf()
            {
                this.stack = 69;
            }

            public override int GetDamageReductionAll()
            {
                return this.stack;
            }

            public override void OnRoundEndTheLast()
            {
                bool flag = this._owner.breakDetail.IsBreakLifeZero();
                if (flag)
                {
                    this._owner.RecoverBreakLife(this._owner.MaxBreakLife, false);
                    this._owner.breakDetail.nextTurnBreak = false;
                }
                this._owner.breakDetail.RecoverBreak(this._owner.breakDetail.GetDefaultBreakGauge());
                this._owner.breakDetail.breakLife = this._owner.breakDetail.breakGauge;
                this.Destroy();
            }
        }

        #region - Combo Finisher keyword check -
        private bool CheckCondition(BattleDiceCardModel card)
        {
            if (card == null)
                return false;
            DiceCardXmlInfo xmlData = card.XmlData;
            if (xmlData == null)
                return false;
            if (xmlData.Keywords.Contains("SlickMod_ComboFinisher"))
                return true;
            List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
            for (int index = 0; index < abilityKeywords.Count; ++index)
            {
                if (abilityKeywords[index] == "SlickMod_ComboFinisher")
                    return true;
            }
            foreach (DiceBehaviour behaviour in card.GetBehaviourList())
            {
                List<string> keywordsByScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(behaviour.Script);
                for (int index = 0; index < keywordsByScript.Count; ++index)
                {
                    if (keywordsByScript[index] == "SlickMod_ComboFinisher")
                        return true;
                }
            }
            return false;
        }
        #endregion

        #region - Funny enemy version of Speed Break -
        public class BattleUnitBuf_OppSpeedBreak : BattleUnitBuf
        {
            // Get keyword
            public override string keywordId => "SlickMod_SparkSpeedBreak";

            // Neutral status; doesn't need anything to override BufPositiveType

            // Thing
            public override KeywordBuf bufType
            {
                get
                {
                    return MyKeywordBufs.SlickMod_SparkSpeedBreak;
                }
            }
            public override void OnRoundStart()
            {
                this._owner.cardSlotDetail.RecoverPlayPoint(this._owner.cardSlotDetail.GetMaxPlayPoint());
                this._owner.bufListDetail.RemoveBufAll(KeywordBuf.Binding);
                this._owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddedSpeedDie());
                this._owner.RollSpeedDice();
                SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            }
        }
        #endregion
    }

    #endregion

    #region --BACKSTREET SLUGGERS--

    // Rat Breaker
    // Blunt Stagger Damage +1. Blunt Damage +1 against staggered enemies.
    public class PassiveAbility_SlickMod_BackstreetBlunt : PassiveAbilityBase
    {
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            base.BeforeGiveDamage(behavior);
            if (behavior.Detail == BehaviourDetail.Hit)
            {
                owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    breakDmg = 1
                });
                BattleUnitModel target = behavior.card?.target;
                if (target != null && target.IsBreakLifeZero())
                {
                    behavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        dmg = 1
                    });
                }
            }
        }
    }

    // Cautious
    // At the start of the Act, gain 2 Protection
    public class PassiveAbility_SlickMod_BackstreetProt : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            base.OnWaveStart();
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Protection, 2, owner);
        }
    }

    // Violent
    // At the start of the Act, gain 1 Damage Up
    public class PassiveAbility_SlickMod_BackstreetDmgUp : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            base.OnWaveStart();
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.DmgUp, 1, owner);
        }
    }

    // Striking Upward
    // Deal +2 Damage and Stagger Damage to enemies with a higher keypage rarity
    public class PassiveAbility_SlickMod_BackstreetPunchUp : PassiveAbilityBase
    {
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            base.BeforeGiveDamage(behavior);
            BattleUnitModel target = behavior.card?.target;
            if (target != null && (int)target.Book.Rarity > (int)owner.Book.Rarity)
            {
                owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    dmg = 2,
                    breakDmg = 2
                });
            }
        }
    }

    // Blazing Bat
    // Upon a successful Blunt attack, inflict 1 burn. At the start of each Scene, gain 2 burn.
    public class PassiveAbility_SlickMod_BackstreetBlazing : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 2, owner);
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            base.OnSucceedAttack(behavior);
            if (behavior.Detail == BehaviourDetail.Hit)
            {
                owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.card.target?.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 1, owner);
            }
        }
    }

    // Rampaging Flames
    // At the start of each Scene, inflict 1 burn to all characters, including self. Every 3 scenes, increase the burn inflicted by 1 (max 15).
    public class PassiveAbility_SlickMod_BackstreetFlames : PassiveAbilityBase
    {
        private int burnStack;
        private int counter;
        public override void OnWaveStart()
        {
            base.OnWaveStart();
            burnStack = 1;
            counter = 0;
        }
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            if (burnStack < 15)
            {
                counter++;
                if (counter >= 3)
                {
                    counter = 0;
                    burnStack++;
                }
            }
            foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList())
            {
                alive.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, burnStack, owner);
            }
        }
    }
    #endregion

    #region --MIDNIGHT OFFICE--

    // Midnight Arrival
    // Starting with the fifth Scene, gain 1 Protection, Stagger Protection, Haste, and Damage Up each Scene
    public class PassiveAbility_SlickMod_MidnightArrival : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            if (Singleton<StageController>.Instance.RoundTurn >= 5)
            {
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, 1, owner);
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.BreakProtection, 1, owner);
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, 1, owner);
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.DmgUp, 1, owner);
            }
        }
    }

    // Calm
    // At the start of the Act, gain 1 Endurance
    public class PassiveAbility_SlickMod_MidnightCalm : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            base.OnWaveStart();
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 1, owner);
        }
    }

    // Courageous
    // At the start of the Act, gain 1 Strength
    public class PassiveAbility_SlickMod_MidnightCourageous : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            base.OnWaveStart();
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, owner);
        }
    }

    // Rekindled Anger
    // Every third successful attack inflicts 2 burn
    public class PassiveAbility_SlickMod_MidnightRekindled : PassiveAbilityBase
    {
        private int counter = 0;
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            base.OnSucceedAttack(behavior);
            counter++;
            if (counter == 3 && behavior.card.target != null)
            {
                behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 2, owner);
                counter = 0;
            }
        }
    }

    // Ignited Bat
    // Inflict 1 Burn on hit. At the start of each Scene, gain 5 burn.
    public class PassiveAbility_SlickMod_MidnightIgnited : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 5, owner);
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            base.OnSucceedAttack(behavior);
            owner.battleCardResultLog?.SetPassiveAbility(this);
            behavior.card.target?.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 1, owner);
        }
    }

    // Smoldering Insanity
    // Dice gain +1 Power for every 20 stacks of Burn on self (max 3). If Burn on self reaches 70...
    public class PassiveAbility_SlickMod_MidnightSmoldering : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            int num = Math.Min(owner.bufListDetail.GetKewordBufStack(KeywordBuf.Burn) / 20, 3);
            if (num > 0)
            {
                owner.battleCardResultLog?.SetPassiveAbility(this);
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = num
                });
            }
        }
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            if (owner.bufListDetail.GetKewordBufStack(KeywordBuf.Burn) > 70)
            {
                owner.TakeDamage(100, DamageType.Buf, owner, KeywordBuf.Burn);
                owner.bufListDetail.GetActivatedBuf(KeywordBuf.Burn).Destroy();
            }
        }
    }

    // Cauterize
    // Once per reception, when Staggered, recover from Stagger, recover 40 Stagger resist and 20 HP, and gain 20 Burn.
    public class PassiveAbility_SlickMod_MidnightCauterize : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            base.OnWaveStart();
            _triggered = false;
            // Check if there is a stored value for this passive
            if (Singleton<StageController>.Instance.GetStageModel().GetStageStorageData("MidnightCauterize", out List<UnitCount> list))
            {
                // Check if it corresponds to this specific unit, and if so, set _triggered to the stored value
                if (list.Exists((UnitCount x) => x.Unit == owner.UnitData))
                {
                    _triggered = list.Find((UnitCount x) => x.Unit == owner.UnitData).Triggered;
                }
            }
        }

        public override bool OnBreakGageZero()
        {
            if (!_triggered)
            {
                _triggered = true;
                owner.breakDetail.RecoverBreak(40);
                owner.RecoverHP(20);
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 20, owner);
                return true;
            }
            return false;
        }

        public override void OnBattleEnd()
        {
            base.OnBattleEnd();
            // Check if there is a stored value for this passive
            Singleton<StageController>.Instance.GetStageModel().GetStageStorageData("MidnightCauterize", out List<UnitCount> list);
            if (list == null)
            {
                // If there isn't, set one up
                list = new List<UnitCount>();
                Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData("MidnightCauterize", list);
            }
            // Check if there is a stored value for this specific unit; if so, update the Triggered variable with the current one
            if (list.Exists((UnitCount x) => x.Unit == owner.UnitData))
            {
                list.Find((UnitCount x) => x.Unit == owner.UnitData).Triggered = _triggered;
            }
            else
            {
                // If there isn't, add one, and set the UnitData and Triggered variables to the ones for this unit and passive
                list.Add(new UnitCount
                {
                    Unit = owner.UnitData,
                    Triggered = _triggered,
                });
            }
        }

        private bool _triggered;
        public class UnitCount
        {
            // Keeps track of which unit the saved data corresponds to
            public UnitBattleDataModel Unit { get; set; }
            public bool Triggered { get; set; }
        }
    }
    #endregion

    #region --DANS SOLUTIONS--
    public class PassiveAbility_SlickMod_DanDansGuarentee : PassiveAbilityBase
    {
        public override void OnDie()
        {
            base.OnDie();
            BattleUnitModel battleUnitModel = BattleObjectManager.instance.GetAliveList(this.owner.faction).Find((BattleUnitModel x) => x.index == 0);
            bool flag = battleUnitModel != null;
            if (flag)
            {
                battleUnitModel.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_Barrier, 5, battleUnitModel);
                battleUnitModel.cardSlotDetail.RecoverPlayPoint(1);
                battleUnitModel.allyCardDetail.DrawCards(1);
                battleUnitModel.RecoverBreakLife(5);
            }
        }
    }

    public class PassiveAbility_SlickMod_DanWtfThisWasntSupposedToHappen : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            if (!BattleObjectManager.instance.GetAliveList(owner.faction).Exists((BattleUnitModel x) => x != owner))
            {
                owner.bufListDetail.AddBuf(new BattleUnitBuf_DanIsIncrediblyFast());
            }
        }

        public class BattleUnitBuf_DanIsIncrediblyFast : BattleUnitBuf
        {
            public override int SpeedDiceNumAdder()
            {
                return 1;
            }
            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                this.Destroy();
            }
        }
    }

    public class PassiveAbility_SlickMod_DanSurplusInkwork : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            BattleUnitBuf Inky = this.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_BlackTieInk);
            if (Inky != null)
            {
                if (Inky.stack >= 10 && base.IsAttackDice(behavior.Detail))
                {
                    behavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        power = 1
                    });
                }
            }
        }
    }

    public class PassiveAbility_SlickMod_DanJustBusiness : PassiveAbilityBase
    {
        public override void OnDieOtherUnit(BattleUnitModel unit)
        {
            base.OnDieOtherUnit(unit);
            if (unit.faction == owner.faction)
            {
                this.owner.allyCardDetail.DrawCards(1);
                this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, 1, this.owner);
            }
        }
    }

    #endregion

    #region --WORTHLESS AUTOMATONS--

    // Faulty Capacitor
    // Can store up to 3 Orbs. Upon Evoking an Orb, take 5 damage.
    public class PassiveAbilityBase_SlickMod_FaultyCapacitor3 : passivedefectorbbase
    {
        public override int orblimit => 3;

        public override void onevoke()
        {
            this.owner.TakeDamage(5, DamageType.Passive);
        }
    }

    // Parameter Push
    // At the start of each Scene, all allied Defective Machines take 5 Stagger damage and Channel 1 Lightning Orb.
    public class PassiveAbilityBase_SlickMod_ParameterPush : DiceCardSelfAbilityBase
    {

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(unit.faction);
            aliveList.Remove(unit);
            foreach (BattleUnitModel item in aliveList)
            {
                item.bufListDetail.AddBuf(new SlickMod_Orb_Lightning());
                item.TakeBreakDamage(5, DamageType.Passive);
            }
        }
    }

    // Better Things to do
    // Fuck off lol
    public class PassiveAbility_SlickMod_AquamarineManFuckThisImOut : PassiveAbilityBase
    {

        public override void OnWaveStart()
        {
            owner.emotionDetail.SetMaxEmotionLevel(0);
        }

        public override int GetEmotionCoinAdder(int defaultCount)
        {
            return --defaultCount;
        }

        public override void OnRoundEnd()
        {
            if (!BattleObjectManager.instance.GetAliveList(owner.faction).Exists((BattleUnitModel x) => x != owner))
            {
                owner.SetHp(950);
            }
        }
    }

    #endregion

    #region --DROWNED--
    public class PassiveAbility_SlickMod_DrownedDenizenofNothing : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            List<BattleUnitModel> aliveList_opponent = BattleObjectManager.instance.GetAliveList_opponent(this.owner.faction);
            if (this.owner.emotionDetail.EmotionLevel < 5)
            {
                int stack = aliveList_opponent.Count;
                return stack;
            }
            else
            {
                int stack2 = aliveList_opponent.Count * 2;
                return stack2;
            }

        }
        public override void OnUnitCreated()
        {
            base.OnUnitCreated();
            this.minhp = 1;
        }

        public override void OnWaveStart()
        {
            this.owner.view.EnableStatNumber(false);
        }
        public override int GetMinHp()
        {
            return this.minhp;
        }
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            base.OnTakeDamageByAttack(atkDice, dmg);
            this.owner.TakeBreakDamage(dmg, DamageType.Attack);
        }

        public override void OnBreakState()
        {
            base.OnBreakState();
            this.owner.Die();
        }
        public override float BreakDmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
        {
            return (type == DamageType.Card_Ability) ? .25f : 1f;
        }
        private int minhp;
    }

    public class PassiveAbility_SlickMod_DrownedAbyssalDepths : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
            {
                enemycount++;
                if (battleUnitModel.breakDetail.breakLife == 0)
                {
                    this.owner.RecoverBreakLife(50);
                    battleUnitModel.breakDetail.ResetGauge();
                    staggeredcount++;
                }
                if (battleUnitModel != null)
                {
                    int schizo = battleUnitModel.breakDetail.GetDefaultBreakGauge();
                    if (schizo <= 0)
                    {
                        battleUnitModel.Die();
                        foreach (BattleUnitModel battleUnitModel2 in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
                        {
                            battleUnitModel2.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_DrownedDrowning, 3, this.owner);
                        }
                    }
                }
                enemycount = 0;
                staggeredcount = 0;
            }
            if (staggeredcount <= 0)
            {
                this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_DrownedDrowning, 2, this.owner);
                this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, this.owner);
                this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, this.owner);
            }
            if (staggeredcount == enemycount)
            {
                this.owner.allyCardDetail.AddNewCard(new LorId(SlickModInitializer.packageId, 170113))?.SetCostToZero();
            }
        }
        public int staggeredcount;
        public int enemycount;
    }
    public class PassiveAbility_SlickMod_DrownedWanderingAimlessly : PassiveAbilityBase
    {
        private bool Staggeredererereded;
        public override void OnUnitCreated()
        {
            base.OnUnitCreated();
            this.speed = 0;
        }

        public override void OnRoundStart()
        {
            base.OnRoundStart();
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
            {
                if (battleUnitModel.breakDetail.IsBreakLifeZero())
                {
                    this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Binding, 1, this.owner);
                }
            }
        }
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            Staggeredererereded = false;
            if (curCard.target != null && curCard.target.IsBreakLifeZero())
            {
                Staggeredererereded = true;
            }
            int speedDiceResultValue = curCard.speedDiceResultValue;
            BattleUnitModel target = curCard.target;
            int targetSlotOrder = curCard.targetSlotOrder;
            if (targetSlotOrder < 0 || targetSlotOrder >= target.speedDiceResult.Count)
            {
                return;
            }
            SpeedDice speedDice = target.speedDiceResult[targetSlotOrder];
            if (speedDiceResultValue > speedDice.value)
            {
                int num = speedDiceResultValue - speedDice.value;
                int num2 = Mathf.Clamp(num, -20, 20);
                if (num2 > 0)
                {
                    curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                    {
                        breakDmg = num2
                    });
                }
            }
        }
        public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnEndBattle(curCard);
            if (!Staggeredererereded && curCard.target.IsBreakLifeZero())
            {
                curCard.target.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_DrownedDrowning, 1, this.owner);
                this.speed++;
            }
        }

        public override int GetSpeedDiceAdder(int speedDiceResult)
        {
            return this.speed;
        }
        public int speed;

    }

    public class PassiveAbility_SlickMod_DrownedMisery : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            int stagger = this.owner.breakDetail.breakLife;
            int stagger2 = this.owner.breakDetail.GetDefaultBreakGauge();
            int cringeinfliction = stagger2 - stagger;
            for (int i = 0; i < cringeinfliction / 10; i++)
            {
                foreach (BattleUnitModel item in BattleObjectManager.instance.GetAliveList_random((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy, 1))
                {
                    item.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_DrownedOmegaCringe, 1, base.owner);
                }
            }
        }
        public override void AfterGiveDamage(int damage)
        {
            base.AfterGiveDamage(damage);
            if (damage <= 10)
            {
                foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
                {
                    battleUnitModel.TakeBreakDamage(damage / 2, DamageType.Passive);
                }
            }
        }
    }

    public class PassiveAbility_SlickMod_DrownedShimmering : PassiveAbilityBase
    {
        private int Patterncounter;

        public override void OnRoundStartAfter()
        {
            base.OnRoundStartAfter();
            SetCards_phase();
            this.Patterncounter++;
            this.Patterncounter %= 5;
        }
        private void SetCards_phase()
        {
            owner.allyCardDetail.ExhaustAllCards();
            if (Patterncounter == 0 && this.owner.emotionDetail.EmotionLevel < 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170102);
                AddNewCard(170104);
                AddNewCard(170106);
                AddNewCard(170103);
                AddNewCard(170109);
            }
            else if (Patterncounter == 1 && this.owner.emotionDetail.EmotionLevel < 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170102);
                AddNewCard(170107);
                AddNewCard(170103);
                AddNewCard(170103);
                AddNewCard(170108);
            }
            else if (Patterncounter == 2 && this.owner.emotionDetail.EmotionLevel < 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170102);
                AddNewCard(170107);
                AddNewCard(170106);
                AddNewCard(170105);
                AddNewCard(170110);
            }
            else if (Patterncounter == 3 && this.owner.emotionDetail.EmotionLevel < 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170102);
                AddNewCard(170107);
                AddNewCard(170106);
                AddNewCard(170106);
                AddNewCard(170111);
            }
            else if (Patterncounter == 4 && this.owner.emotionDetail.EmotionLevel < 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170112);
            }
            else if (Patterncounter == 0 && this.owner.emotionDetail.EmotionLevel == 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170102);
                AddNewCard(170102);
                AddNewCard(170104);
                AddNewCard(170104);
                AddNewCard(170105);
                AddNewCard(170106);
                AddNewCard(170107);
                AddNewCard(170103);
                AddNewCard(170109);
            }
            else if (Patterncounter == 1 && this.owner.emotionDetail.EmotionLevel == 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170102);
                AddNewCard(170102);
                AddNewCard(170102);
                AddNewCard(170104);
                AddNewCard(170107);
                AddNewCard(170106);
                AddNewCard(170103);
                AddNewCard(170103);
                AddNewCard(170108);
            }
            else if (Patterncounter == 2 && this.owner.emotionDetail.EmotionLevel == 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170102);
                AddNewCard(170104);
                AddNewCard(170105);
                AddNewCard(170104);
                AddNewCard(170106);
                AddNewCard(170106);
                AddNewCard(170107);
                AddNewCard(170107);
                AddNewCard(170110);
            }
            else if (Patterncounter == 3 && this.owner.emotionDetail.EmotionLevel == 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170102);
                AddNewCard(170103);
                AddNewCard(170102);
                AddNewCard(170102);
                AddNewCard(170106);
                AddNewCard(170106);
                AddNewCard(170103);
                AddNewCard(170106);
                AddNewCard(170111);
            }
            else if (Patterncounter == 4 && this.owner.emotionDetail.EmotionLevel == 5)
            {
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170101);
                AddNewCard(170103);
                AddNewCard(170103);
                AddNewCard(170112);
            }
        }

        private void AddNewCard(int id)
        {
            this.owner.allyCardDetail.AddTempCard(new LorId(SlickModInitializer.packageId, id))?.SetCostToZero();
        }
    }
    #endregion

    #region --AESIR OFFICE--

    public class PassiveAbility_SlickMod_GamerCycle : PassiveAbilityBase
    {
        // :geguh:
    }

    #endregion

    #region --GRADE 1 FIXER MAO--

    // Newjack Workshop
    // Dice Power +1. At the start of each Scene, randomize the types of all dice on all pages in the deck. (including Counter dice)
    public class PassiveAbility_SlickMod_NewjackImprov : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            foreach (BattleDiceCardModel amongus in this.owner.allyCardDetail.GetAllDeck())
            {
                if (amongus != null)
                {
                    amongus.CopySelf();
                    foreach (DiceBehaviour sussy in amongus.GetBehaviourList())
                    {
                        if (sussy != null)
                        {
                            switch (sussy.Type)
                            {
                                case BehaviourType.Atk:
                                    sussy.Detail = RandomUtil.SelectOne(new List<BehaviourDetail> { BehaviourDetail.Slash, BehaviourDetail.Penetrate, BehaviourDetail.Hit });
                                    break;
                                case BehaviourType.Def:
                                    sussy.Detail = RandomUtil.SelectOne(new List<BehaviourDetail> { BehaviourDetail.Guard, BehaviourDetail.Evasion });
                                    break;
                                case BehaviourType.Standby:
                                    sussy.Detail = RandomUtil.SelectOne(new List<BehaviourDetail> { BehaviourDetail.Slash, BehaviourDetail.Penetrate, BehaviourDetail.Hit, BehaviourDetail.Guard, BehaviourDetail.Evasion });
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase)this);
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 1
            });
        }

    }

    #endregion

}
