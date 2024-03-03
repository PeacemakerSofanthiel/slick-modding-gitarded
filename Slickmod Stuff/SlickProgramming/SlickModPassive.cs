using System;
using LOR_DiceSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

namespace SlickRuinaMod
{
    #region - GENERAL PURPOSE -

    // Heightened Emotions II
    // Start the reception at Emotion Level 2.
    public class PassiveAbility_SlickMod_StartEmoLevel2 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if ( this.owner.emotionDetail.EmotionLevel == 0 )
            {
                this.owner.emotionDetail.LevelUp_Forcely(2);
            }

            if ( this.owner.emotionDetail.EmotionLevel == 1)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
            }
            this.owner.emotionDetail.CheckLevelUp();
        }
    }

    // Heightened Emotions III
    // Start the reception at Emotion Level 3.
    public class PassiveAbility_SlickMod_StartEmoLevel3 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.emotionDetail.EmotionLevel == 0)
            {
                this.owner.emotionDetail.LevelUp_Forcely(3);
            }

            if (owner.emotionDetail.EmotionLevel == 1)
            {
                this.owner.emotionDetail.LevelUp_Forcely(2);
            }

            if (owner.emotionDetail.EmotionLevel == 2)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
            }
            this.owner.emotionDetail.CheckLevelUp();
        }
    }

    // Heightened Emotions IV
    // Start the reception at Emotion Level 4.
    public class PassiveAbility_SlickMod_StartEmoLevel4 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.emotionDetail.EmotionLevel == 0)
            {
                this.owner.emotionDetail.LevelUp_Forcely(4);
            }

            if (owner.emotionDetail.EmotionLevel == 1)
            {
                this.owner.emotionDetail.LevelUp_Forcely(3);
            }

            if (owner.emotionDetail.EmotionLevel == 2)
            {
                this.owner.emotionDetail.LevelUp_Forcely(2);
            }

            if (owner.emotionDetail.EmotionLevel == 3)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
            }
            this.owner.emotionDetail.CheckLevelUp();
        }
    }

    // Heightened Emotions V
    // Start the reception at Emotion Level 5.
    public class PassiveAbility_SlickMod_StartEmoLevel5 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.emotionDetail.EmotionLevel == 0)
            {
                this.owner.emotionDetail.LevelUp_Forcely(5);
            }

            if (owner.emotionDetail.EmotionLevel == 1)
            {
                this.owner.emotionDetail.LevelUp_Forcely(4);
            }

            if (owner.emotionDetail.EmotionLevel == 2)
            {
                this.owner.emotionDetail.LevelUp_Forcely(3);
            }

            if (owner.emotionDetail.EmotionLevel == 3)
            {
                this.owner.emotionDetail.LevelUp_Forcely(2);
            }

            if (owner.emotionDetail.EmotionLevel == 4)
            {
                this.owner.emotionDetail.LevelUp_Forcely(1);
            }
            this.owner.emotionDetail.CheckLevelUp();
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

        // Token: 0x04000031 RID: 49
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
                owner.battleCardResultLog?.SetPassiveAbility(this);
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

    #region - COURIERS 1 -

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
                behavior?.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
            if (CheckCondition() && behavior.Detail == BehaviourDetail.Hit)
            {
                owner.battleCardResultLog?.SetPassiveAbility(this);
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

    #region - SNOW COYOTE OFFICE -

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
                owner.battleCardResultLog?.SetPassiveAbility(this);
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
    
    #region - INFERNAL TEMPLAR 1 -
    
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
                        diceBehaviour
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
                    battleDiceBehavior
                });
            }
        }
    }

    #endregion

    #region - GOLDEN SPARK -

    //
    // Untransferable. "Speed Break" becomes accessible, and can be used by spending Samsara.
    // At the start of each Scene, gain Samsara equal to the amount of Haste on self. Upon using a Combo Finisher page, gain Samsara equal to its original Cost.
    public class PassiveAbility_SlickMod_SparkSamsaraGaming : PassiveAbilityBase
    {
        // Adds Speed Break to Ego Hand
        public override void OnWaveStart()
        {
            owner.personalEgoDetail.AddCard(new LorId("SlickMod", 22));
        }

        // Gains Samsara equal to Haste
        public override void OnRoundStart()
        {
            BattleUnitBuf activatedBuf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
            if (activatedBuf != null)
            {
                int stack = activatedBuf.stack;
                if (stack > 0)
                {
                    base.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_SparkSamsara, stack, base.owner);
                }
            }
        }
    }

    #endregion

}
