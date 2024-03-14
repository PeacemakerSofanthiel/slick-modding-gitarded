using System;
using LOR_DiceSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using static SlickRuinaMod.DiceCardSelfAbility_SlickMod_WhoCaresClaw;
using static DiceCardSelfAbility_unitePower;
using static SlickRuinaMod.DiceCardSelfAbility_SlickMod_BarghestNail;

namespace SlickRuinaMod
{
    #region --COURIERS 1--

    // Take a Breather: Page Effect
    // Restore 1 Light upon discarding this page
    // [On Use] Restore 1 Light
    public class DiceCardSelfAbility_SlickMod_Courier_DiscardUseLight1 : DiceCardSelfAbilityBase
    {
        public static string Desc = "Restore 1 Light upon discarding this page\n[On Use] Restore 1 Light";

        // Light restore keyword
        public override string[] Keywords => new string[1] { "Energy_Keyword" };

        // Restore 1 Light upon discarding this page
        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            unit.cardSlotDetail.RecoverPlayPointByCard(1);
        }

        // [On Use] Restore 1 Light
        public override void OnUseCard()
        {
            base.owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
    }

    // Lucky Slip: Page Effect
    // [On Use] Gain 1 Haste next Scene; Gain 1 Cycle next Scene; If Speed is higher than the target's, all dice on this page gain +2 minimum roll value
    public class DiceCardSelfAbility_SlickMod_Courier_LuckySlipPage : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Gain 1 Haste next Scene; Gain 1 Cycle next Scene; If Speed is higher than the target's, all dice on this page gain +2 minimum roll value";

        // Keywords
        public override string[] Keywords => new string[2] { "Quickness_Keyword", "SlickMod_Cycle_Keyword" };

        public override void OnUseCard()
        {
            // [On Use] Gain 1 Haste next Scene; Gain 1 Cycle next Scene; 
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
            this.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Cycle, 1, owner);

            // If Speed is higher than the target's, all dice on this page gain +2 minimum roll value
            int speedDiceResultValue = card.speedDiceResultValue;
            BattleUnitModel target = card.target;
            int targetSlotOrder = card.targetSlotOrder;
            if (targetSlotOrder >= 0 && targetSlotOrder < target.speedDiceResult.Count)
            {
                SpeedDice speedDice = target.speedDiceResult[targetSlotOrder];
                if (speedDiceResultValue > speedDice.value)
                {
                    card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                    {
                        min = 2
                    });
                }
            }
        }
    }

    // Take It To Go: Page Effect
    // [On Use] Gain 3 Cycle next Scene; Draw 1 page
    public class DiceCardSelfAbility_SlickMod_Courier_Cycle3Draw1 : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Gain 3 Cycle next Scene; Draw 1 page";

        // Keywords
        public override string[] Keywords => new string[2] { "SlickMod_Cycle_Keyword", "DrawCard_Keyword" };

        public override void OnUseCard()
        {
            // [On Use] Gain 3 Cycle
            this.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Cycle, 3, owner);
            // Draw 1 page
            base.owner.allyCardDetail.DrawCards(1);
        }
    }

    // Buzz Off!: Page Effect
    // Draw 1 page upon discarding this page
    // [On Use] If user has Cycle, all dice on this page gain +1 Power
    public class DiceCardSelfAbility_SlickMod_FuckOff : DiceCardSelfAbilityBase
    {
        public static string Desc = "Draw 1 page upon discarding this page\n[On Use] If user has Cycle, all dice on this page gain +1 Power";

        // Keywords
        public override string[] Keywords => new string[2] { "DrawCard_Keyword", "SlickMod_Cycle_Keyword" };

        // Draw 1 page upon discarding this page
        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            unit.allyCardDetail.DrawCards(1);
        }

        // [On Use] If user has Cycle, all dice on this page gain +1 Power
        public override void OnUseCard()
        {
            if (base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_Cycle) != null)
            {
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = 1
                });
            }
        }
    }
    #endregion

    #region --SNOW COYOTE OFFICE--

    // Trudge: Page Effect
    // [On Use] Restore 1 Light; If Speed is 2 or lower, boost the minimum value of all dice on this page by 1
    public class DiceCardSelfAbility_SlickMod_Trudge : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Restore 1 Light; If Speed is 2 or lower, boost the minimum value of all dice on this page by 1";

        // Keywords
        public override string[] Keywords => new string[2] { "Energy_Keyword", "Binding_Keyword" };

        public override void OnUseCard()
        {
            base.owner.cardSlotDetail.RecoverPlayPoint(1);
            if (2 >= this.card.speedDiceResultValue)
            {
                this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    min = 1
                });
            }
        }
    }

    // Thorough Scavenging: Page Effect
    // [On Use] Draw 1 page; Inflict 1 Bind to self; if Speed is 2 or lower, draw 1 additional page and inflict 1 additional Bind to self
    public class DiceCardSelfAbility_SlickMod_ThoroughScavenging : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Draw 1 page; Inflict 1 Bind to self; if Speed is 2 or lower, draw 1 additional page and inflict 1 additional Bind to self";

        // Keywords
        public override string[] Keywords => new string[2] { "DrawCard_Keyword", "Binding_Keyword" };

        public override void OnUseCard()
        {
            base.owner.allyCardDetail.DrawCards(1);
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, base.owner);

            if (2 >= this.card.speedDiceResultValue)
            {
                base.owner.allyCardDetail.DrawCards(1);
                base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, base.owner);
            }
        }
    }

    // Nail of Barghest: Page Effect
    // [Combat Start] Gain 1 Strength and 3 Fragile this Scene
    // [On Use] Exhaust this page; add 'Nail of Barghest' to hand in 4 Scenes
    public class DiceCardSelfAbility_SlickMod_BarghestNail : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combat Start] Gain 1 Strength and 3 Fragile this Scene\n[On Use] Exhaust this page; add 'Nail of Barghest' to hand in 4 Scenes";

        // Keywords
        public override string[] Keywords => new string[2] { "Strength_Keyword", "Vulnerable_Keyword" };

        // Card is exhausted and a new copy is added after 4 Scenes
        public void ExhaustAndReturn()
        {
            card.card.exhaust = true;
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddBackAfterX(card.card.GetID(), 4));
        }

        // [Combat Start] Gain 1 Strength and 3 Fragile this Scene
        public override void OnStartBattle()
        {
            card.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1, base.owner);
            card.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Vulnerable, 3, base.owner);
        }

        // [On Use] Exhaust this page; add 'Nail of Barghest' to hand in 4 Scenes
        public override void OnUseCard()
        {
            ExhaustAndReturn();
        }
    }

    // Squall of War: Page Effect
    // [Combat Start] At the start of the next Scene, purge all Bind from self, then gain 2 Bind
    public class DiceCardSelfAbility_SlickMod_SquallOfWar : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combat Start] At the start of the next Scene, purge all Bind from self, then gain 2 Bind";

        // Keywords
        public override string[] Keywords => new string[1] { "Binding" };

        // Status that purges Bind, gives 2 Bind, then bobm
        public class BattleUnitBuf_SlickMod_Squall : BattleUnitBuf
        {
            public override void OnRoundStart()
            {
                _owner.bufListDetail.RemoveBufAll(KeywordBuf.Binding);
                _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Binding, 2);
                this.Destroy();
            }
        }

        // [Combat Start] Gain buff
        public override void OnStartBattle()
        {
            base.owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_SlickMod_Squall());
        }
    }

    // Pack Hunting Tactics: Page Effect
    // Dice on this page deal additional damage equal to the number of other allies
    // targeting this page's target with 'Pack Hunting Tactics' this Scene
    public class DiceCardSelfAbility_SlickMod_PackHuntingTactics : DiceCardSelfAbilityBase
    {
        public static string Desc = "Dice on this page deal additional damage equal to the number of other allies targeting this page's target with 'Pack Hunting Tactics' this Scene";
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is DiceCardSelfAbility_SlickMod_PackHuntingTactics.BattleUnitBuf_SlickMod_PackHuntingTactics);
            if (battleUnitBuf == null)
            {
                this.card.target.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_PackHuntingTactics(1));
                return;
            }
            battleUnitBuf.stack++;
        }

        public override void OnUseCard()
        {
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is DiceCardSelfAbility_SlickMod_PackHuntingTactics.BattleUnitBuf_SlickMod_PackHuntingTactics);
            if (battleUnitBuf != null)
            {
                foreach (BattleDiceBehavior battleDiceBehavior in this.card.cardBehaviorQueue)
                {
                    battleDiceBehavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        dmg = battleUnitBuf.stack
                    });
                }
            }
            base.OnUseCard();
        }

        public class BattleUnitBuf_SlickMod_PackHuntingTactics : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                this.Destroy();
                base.OnRoundEnd();
            }

            public BattleUnitBuf_SlickMod_PackHuntingTactics(int Stack)
            {
                this.stack = Stack;
            }
        }
    }

    // Biting Cold: Page Effect
    // [On Use] Inflict 2 Bind to self next Scene; If Speed is 2 or lower, restore 3 Light and draw 1 page
    public class DiceCardSelfAbility_SlickMod_BitingCold : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Inflict 2 Bind to self next Scene; If Speed is 2 or lower, restore 3 Light and draw 1 page";

        // Keywords
        public override string[] Keywords => new string[3] { "Binding_Keyword", "Energy_Keyword", "DrawCard_Keyword" };

        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 2, base.owner);
            if (2 >= this.card.speedDiceResultValue)
            {
                base.owner.cardSlotDetail.RecoverPlayPoint(3);
                base.owner.allyCardDetail.DrawCards(1);
            }
        }
    }

    // Hunting Hour: Page Effect
    // Turner's Exclusive Page
    // [On Hit] Give all other allies a single-use copy of 'Pack Hunting Tactics' that exhausts at the end of the Scene;
    // user and all allied Snow Coyote Fixers restore 2 Light
    public class DiceCardSelfAbility_SlickMod_TurnerExclusive : DiceCardSelfAbilityBase
    {
        // fake as fuck lmao

        public static string Desc = "[On Hit]Give all other allies a single-use copy of 'Pack Hunting Tactics' that exhausts at the end of the Scene; user and all allied Snow Coyote Fixers restore 2 Light";
    }

    // Pack Hunting Tactics (Turner): Page Effect
    // Can only be used this Scene
    // Dice on this page deal additional damage equal to the number of other allies
    // targeting this page's target with 'Pack Hunting Tactics' this Scene
    public class DiceCardSelfAbility_SlickMod_PackHuntingTacticsTurner : DiceCardSelfAbilityBase
    {
        public static string Desc = "Can only be used this Scene\nDice on this page deal additional damage equal to the number of other allies targeting this page's target with 'Pack Hunting Tactics' this Scene";
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is DiceCardSelfAbility_SlickMod_PackHuntingTacticsTurner.BattleUnitBuf_SlickMod_PackHuntingTactics);
            if (battleUnitBuf == null)
            {
                this.card.target.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_PackHuntingTactics(1));
                return;
            }
            battleUnitBuf.stack++;
        }

        public override void OnUseCard()
        {
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is DiceCardSelfAbility_SlickMod_PackHuntingTacticsTurner.BattleUnitBuf_SlickMod_PackHuntingTactics);
            if (battleUnitBuf != null)
            {
                foreach (BattleDiceBehavior battleDiceBehavior in this.card.cardBehaviorQueue)
                {
                    battleDiceBehavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        dmg = battleUnitBuf.stack
                    });
                }
            }
            base.OnUseCard();
            card.card.exhaust = true;
        }

        public class BattleUnitBuf_SlickMod_PackHuntingTactics : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                this.Destroy();
                base.OnRoundEnd();
            }

            public BattleUnitBuf_SlickMod_PackHuntingTactics(int Stack)
            {
                this.stack = Stack;
            }
        }
    }

    #endregion

    #region --INFERNAL CORPS 1--
    
    // [On Use] Inflict 1 Overheat to self next Scene
    public class DiceCardSelfAbility_SlickMod_InfernalOverheat1 : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Inflict 1 Overheat to self next Scene";

        public override void OnUseCard()
        {
            base.OnUseCard();
            this.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_InfernalOverheat, 1, this.owner);
        }
    }

    // [On Use] Inflict 2 Overheat to self next Scene
    public class DiceCardSelfAbility_SlickMod_InfernalOverheat2 : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Inflict 2 Overheat to self next Scene";
        public override void OnUseCard()
        {
            base.OnUseCard();
            this.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_InfernalOverheat, 2, this.owner);
        }
    }

    // [On Use] Consume 2 Overheat to restore 3 Light
    public class DiceCardSelfAbility_SlickMod_InfernalOverEAT2Energy3 : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Consume 2 Overheat to restore 3 Light";
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf Overheat = this.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_InfernalOverheat);
            if (Overheat != null)
            {
                if (Overheat.stack >= 2)
                {
                    Overheat.stack -= 2;
                    this.owner.cardSlotDetail.RecoverPlayPointByCard(3);
                    if (Overheat.stack <= 0)
                    {
                        Overheat.Destroy();
                    }
                }
            }
        }
    }

    // Heavy Hand
    // [On Use] Inflict 3 Bind to self next Scene; Gain 1 Strength this Scene
    public class DiceCardSelfAbility_SlickMod_InfernalHeavyHand : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Inflict 3 Bind to self next Scene; Gain 1 Strength this Scene";
        public override void OnUseCard()
        {
            base.OnUseCard();
            this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 3, this.owner);
            this.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1, this.owner);
        }
    }


    // PRISM BLAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAST
    // [On Use] Prism Blast
    // Take damage equal to the amount of Overheat on self x5
    // Exhausts when used
    public class DiceCardSelfAbility_SlickMod_InfernalPRISIMBLAAAAAAAST : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Take damage equal to the amount of Overheat on self x5\nExhausts when used";
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf Overheat = this.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_InfernalOverheat);
            if (Overheat != null)
            {
                this.owner.TakeDamage(Overheat.stack * 5, DamageType.Buf, this.owner);
                this.card.card.exhaust = true;
            }
        }
    }

    // Burning Bladework
    // [On Use] Consume all Overheat and gain 1 Strength and Endurance this Scene for every 2 Overheat consumed; Become Immobilized next Scene
    public class DiceCardSelfAbility_SlickMod_InfernalBurningBladework : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Consume all Overheat and gain 1 Strength and Endurance this Scene for every 2 Overheat consumed; Become Immobilized next Scene";
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf Overheat = this.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_InfernalOverheat);
            if (Overheat != null)
            {
                int buffs = Overheat.stack / 2;
                this.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, buffs, this.owner);
                this.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Endurance, buffs, this.owner);
                this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Stun, 1, this.owner);
                Overheat.Destroy();
            }
        }
    }

    #endregion

    #region --UN GOLDEN SPARK--

    // Speed Break
    // Can only be used at 15+ Samsara
    // [On Use] Spend all Samsara; purge all Bind from self, gain an additional Speed die for the Scene,
    // and reroll this character's Speed Dice
    public class DiceCardSelfAbility_SlickMod_SparkSpeedBreak : DiceCardSelfAbilityBase
    {
        public static string Desc = "Can only be used at 15+ Samsara\n[On Use] Spend 15 Samsara; purge all Bind from self, gain an additional Speed die for the Scene, and reroll this character's Speed Dice";

        // Unusable unless Samsara >= 15
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            BattleUnitBuf activatedBuf = owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara);
            if (activatedBuf != null && activatedBuf.stack >= 15)
            {
                return true;
            }
            return false;
        }

        // On Use Instant
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            BattleUnitBuf_SlickMod_SparkSamsara battleUnitBuf_SparkSamsara = base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara) as BattleUnitBuf_SlickMod_SparkSamsara;
            if (battleUnitBuf_SparkSamsara != null && battleUnitBuf_SparkSamsara.stack >= 15)
            {
                BattleUnitBuf_SlickMod_SparkSamsara battleUnitBuf_SlickSparkSamsara = base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara) as BattleUnitBuf_SlickMod_SparkSamsara;
                bool flag = battleUnitBuf_SlickSparkSamsara != null && battleUnitBuf_SlickSparkSamsara.stack >= 15;
                if (flag)
                {
                    // Subtract 15 Samsara
                    battleUnitBuf_SlickSparkSamsara.stack -= 15;
                    unit.bufListDetail.RemoveBufAll(KeywordBuf.Binding);
                    this.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddedSpeedDie());
                    this.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_SparkSpeedBreak());
                    this.owner.view.speedDiceSetterUI.DeselectAll();
                    this.owner.OnRoundStartOnlyUI();
                    this.owner.RollSpeedDice();
                    SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
                }
            }
        }
    }

    // Breakdown: Page Effect
    // [On Use] Restore 1 Light; If user has 5+ Samsara, gain 1 Haste next Scene
    public class DiceCardSelfAbility_SlickMod_SparkBreakdownUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Restore 1 Light; If user has 5+ Samsara, gain 1 Haste next Scene";

        public override string[] Keywords => new string[3] { "Energy_Keyword", "Quickness_Keyword", "SlickMod_SparkSamsara" };

        public override void OnUseCard()
        {
            base.owner.cardSlotDetail.RecoverPlayPointByCard(1);

            BattleUnitBuf activatedBuf = base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara);
            if (activatedBuf != null && activatedBuf.stack >= 5)
            {
                base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
            }
        }
    }

    // Mirage: Page Effect
    // [On Use] Draw 1 page; Gain 1 Haste next Scene; If user has 7+ Samsara, all dice on this page gain +1 Power
    public class DiceCardSelfAbility_SlickMod_SparkMirageUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Draw 1 page; Gain 1 Haste next Scene; If user has 7+ Samsara, all dice on this page gain +1 Power";

        public override string[] Keywords => new string[3] { "DrawCard_Keyword", "Quickness_Keyword", "SlickMod_SparkSamsara" };

        public override void OnUseCard()
        {

            base.owner.allyCardDetail.DrawCards(1);
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);

            BattleUnitBuf activatedBuf = base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara);
            if (activatedBuf != null && activatedBuf.stack >= 7)
            {
                base.owner.currentDiceAction?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = 1
                });
            }
        }
    }

    // Tatsumaki: Page Effect
    // [Combo: Sakanagi]
    // [On Use] If used alongside "Karyuken", gain 3 Damage Up next Scene
    public class DiceCardSelfAbility_SlickMod_TatsumakiUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo: Sakanagi]\n[On Use] If used alongside \"Karyuken\", gain 2 Damage Up next Scene";

        public override string[] Keywords => new string[1] { "SlickMod_Combo" };

        public override void OnStartBattle()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_UsingTatsumaki());
        }

        public override void OnUseCard()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_SakanagiComboPieceA());

            if (base.owner.bufListDetail.HasBuf<BattleUnitBuf_SlickMod_UsingKaryuken>())
            {
                this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, 2, owner);
            }
        }
    }

    // Karyuken: Page Effect
    // [Combo: Sakanagi]
    // [On Use] If used alongside "Tatsumaki", restore 1 Light
    public class DiceCardSelfAbility_SlickMod_KaryukenUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo: Sakanagi]\n[On Use] If used alongside \"Tatsumaki\", restore 1 Light";

        public override string[] Keywords => new string[2] { "SlickMod_Combo", "Energy_Keyword" };

        public override void OnStartBattle()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_UsingKaryuken());
        }

        public override void OnUseCard()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_SakanagiComboPieceB());

            if (base.owner.bufListDetail.HasBuf<BattleUnitBuf_SlickMod_UsingTatsumaki>())
            {
                base.owner.cardSlotDetail.RecoverPlayPointByCard(1);
            }
        }
    }

    // Sakanagi: Page Effect
    // [Combo Finisher]
    // [On Use] Draw 1 page
    public class DiceCardSelfAbility_SlickMod_SakanagiUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo Finisher]\n[On Use] Draw 1 page";

        public override string[] Keywords => new string[1] { "SlickMod_ComboFinisher" };

        public override void OnUseCard()
        {
            base.owner.allyCardDetail.DrawCards(1);
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            unit.allyCardDetail.ExhaustACardAnywhere(self);
        }
    }

    // Wind Step: Page Effect
    // Combo: Tempest
    // [On Use] Gain 2 Haste next Scene
    public class DiceCardSelfAbility_SlickMod_WindStepUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo: Tempest]\n[On Use] Gain 2 Haste next Scene";

        public override string[] Keywords => new string[2] { "SlickMod_Combo", "Quickness_Keyword" };

        public override void OnUseCard()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_TempestComboPieceA());
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, base.owner);
        }
    }

    // Spiral Strike: Page Effect
    // Combo: Tempest
    // [On Use] Gain 1 Damage Up next Scene for every 5 Samsara on self (max. 3)
    public class DiceCardSelfAbility_SlickMod_SpiralStrikeUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo: Tempest]\n[On Use] Gain 1 Damage Up next Scene for every 5 Samsara on self (max. 3)";

        public override string[] Keywords => new string[1] { "SlickMod_Combo" };

        public override void OnUseCard()
        {
            BattleUnitBuf_SlickMod_SparkSamsara battleUnitBuf_slickSparkSamsaraSpiralStrikeSwag = this.card.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara) as BattleUnitBuf_SlickMod_SparkSamsara;
            if (battleUnitBuf_slickSparkSamsaraSpiralStrikeSwag != null && battleUnitBuf_slickSparkSamsaraSpiralStrikeSwag.stack >= 5)
            {
                this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, Mathf.Min(battleUnitBuf_slickSparkSamsaraSpiralStrikeSwag.stack / 5, 3), this.owner);
            }
        }
    }

    // Tempest: Page Effect
    // [Combo Finisher]
    // [Combat Start] Gain 1 Strength this Scene
    public class DiceCardSelfAbility_SlickMod_TempestUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo Finisher]\n[Combat Start] Gain 1 Strength this Scene";

        public override string[] Keywords => new string[3] { "SlickMod_ComboFinisher", "bstart_Keyword", "Strength_Keyword" };

        public override void OnStartBattle()
        {
            card.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1, base.owner);
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            unit.allyCardDetail.ExhaustACardAnywhere(self);
        }
    }

    #endregion

    #region --BACKSTREET SLUGGERS--

    // Rats with Bats
    // [On Use] Next Scene gain 1 Damage Up for each ally using 'Rats with Bats' this Scene (including self)
    public class DiceCardSelfAbility_SlickMod_BackstreetRatsBats : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Next Scene, gain 1 Damage Up for every ally using 'Rats with Bats' this Scene (including self)";

        public override void OnStartBattle()
        {
            base.OnStartBattle();
            owner.bufListDetail.AddBufWithoutDuplication(new SlickWereTheRatsBuf());
        }

        public override void OnUseCard()
        {
            base.OnUseCard();
            int count = 0;
            foreach (BattleUnitModel ally in BattleObjectManager.instance.GetAliveList(owner.faction))
            {
                BattleUnitBuf battleUnitBuf = ally.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is SlickWereTheRatsBuf && !x.IsDestroyed());
                if (battleUnitBuf != null)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, count, owner);
            }
        }

        public class SlickWereTheRatsBuf : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                Destroy();
            }
        }
    }

    // Head Crushing
    // [Combat Start] Deal +1 damage with blunt attacks this scene
    public class DiceCardSelfAbility_SlickMod_BackstreetHeadCrushing : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combat Start] Blunt dice deal +1 damage this Scene";

        public override string[] Keywords => new string[1] { "bstart_Keyword" };

        public override void OnStartBattle()
        {
            base.OnStartBattle();
            owner.bufListDetail.AddBuf(new SlickBluntDmg1thisRoundBuf());
        }

        public class SlickBluntDmg1thisRoundBuf : BattleUnitBuf
        {
            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                base.OnSuccessAttack(behavior);
                if (behavior.Detail == BehaviourDetail.Hit)
                {
                    behavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        dmg = 1
                    });
                }
            }

            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                Destroy();
            }
        }
    }

    // Rib Cracking
    // If the attack was one-sided, all Offensive dice on this page gain '[On Hit] Inflict 1 Fragile next Scene'
    public class DiceCardSelfAbility_SlickMod_BackstreetRibCracking : DiceCardSelfAbilityBase
    {
        public static string Desc = "If the attack is one-sided, all Offensive dice on this page gain '[On Hit] Inflict 1 Fragile next Scene'";

        public override string[] Keywords => new string[1] { "Vulnerable_Keyword" };

        public override void OnStartOneSideAction()
        {
            base.OnStartOneSideAction();
            card.ApplyDiceAbility(DiceMatch.AllAttackDice, new DiceCardAbility_vulnerable1atk());
        }
    }

    // Handling Real Power
    // This page is exhausted on use and returns to hand after 3 Scenes
    // [On Use] Gain 'Geared Up' next scene
    public class DiceCardSelfAbility_SlickMod_BackstreetHandlingPower : DiceCardSelfAbilityBase
    {
        public static string Desc = "This page is exhausted on use and returns to hand after 3 Scenes\r\n [On Use] Become 'Geared Up' next scene";

        // Card is exhausted and a new copy is added after 3 Scenes
        public void ExhaustAndReturn()
        {
            card.card.exhaust = true;
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddBackAfterX(card.card.GetID(), 3));
        }

        public override void OnUseCard()
        {
            owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_GearedUp, 1, owner);
        }
    }

    // BlazingBoneBreaker
    // Only usable when 'Geared Up'
    // [On Use] Lose 'Geared Up' status; all dice played this Scene gain '[On Hit] Inflict 5 Burn to each other'
    public class DiceCardSelfAbility_SlickMod_BackstreetBoneBreaker : DiceCardSelfAbilityBase
    {
        public static string Desc = "Only usable when 'Geared Up'\r\n [On Use] Lose 'Geared Up' status; this Scene, all of this character's Offensive dice gain '[On Hit] Inflict 5 Burn to each other'";

        public override string[] Keywords => new string[1] { "Burn_Keyword" };

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            if (owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_GearedUp) == null)
            {
                return false;
            }
            return true;
        }

        public override void OnUseCard()
        {
            owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_GearedUp).Destroy();
            owner.bufListDetail.AddBuf(new SlickBurnEachother5Buf());
            base.OnUseCard();
        }

        public class SlickBurnEachother5Buf : BattleUnitBuf
        {
            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                base.OnSuccessAttack(behavior);
                _owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 5, _owner);
                behavior.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 5, _owner);
            }

            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                Destroy();
            }
        }
    }

    #endregion

    #region --MIDNIGHT OFFICE--

    // Midnight Hunt
    // [On Use] Next Scene gain 1 Haste and Endurance for each ally using 'Midnight Hunt' this Scene (including self)
    public class DiceCardSelfAbility_SlickMod_MidnightHunt : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Next Scene gain 1 Haste and Endurance for each ally using 'Midnight Hunt' this Scene (including self)";

        public override string[] Keywords => new string[2] { "Quickness_Keyword", "Endurance_Keyword" };

        public override void OnStartBattle()
        {
            base.OnStartBattle();
            owner.bufListDetail.AddBufWithoutDuplication(new SlickWeStalkAtNight());
        }

        public override void OnUseCard()
        {
            base.OnUseCard();
            int count = 0;
            foreach (BattleUnitModel ally in BattleObjectManager.instance.GetAliveList(owner.faction))
            {
                BattleUnitBuf battleUnitBuf = ally.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is SlickWeStalkAtNight && !x.IsDestroyed());
                if (battleUnitBuf != null)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, count, owner);
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, count, owner);
            }
        }

        public class SlickWeStalkAtNight : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                Destroy();
            }
        }
    }

    // Javelin Throw
    // This page is exhausted on use and returns to hand after 2 Scenes
    public class DiceCardSelfAbility_SlickMod_MidnightJavelin : DiceCardSelfAbilityBase
    {
        public static string Desc = "This page is exhausted on use and returns to hand after 2 Scenes";

        // Card is exhausted and a new copy is added after 2 Scenes
        public void ExhaustAndReturn()
        {
            card.card.exhaust = true;
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddBackAfterX(card.card.GetID(), 2));
        }
    }

    // Penta Hit
    // Can only be used at Emotion Level 2 and above
    // [On Use] Recover 10 stagger resist; all Offensive dice on this page gain '[On Hit] Deal Stagger damage to self equal to the roll's value'
    public class DiceCardSelfAbility_SlickMod_MidnightPenta : DiceCardSelfAbilityBase
    {
        public static string Desc = "Can only be used at Emotion Level 2 and above\r\n [On Use] Recover 10 Stagger Resist; all Offensive dice on this page gain '[On Hit] Deal Stagger damage to self equal to the roll's value'";

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            if (owner.emotionDetail.EmotionLevel >= 2)
            {
                return true;
            }
            return false;
        }

        public override void OnUseCard()
        {
            base.OnUseCard();
            card.ApplyDiceAbility(DiceMatch.AllAttackDice, new DiceCardAbility_SlickMod_SelfStagger());
        }
    }

    // Ignite Bat
    // [On Use] Restore 3 light; draw 1 page; all dice played this Scene gain '[On hit] Inflict 1 Burn'
    public class DiceCardSelfAbility_SlickMod_MidnightIgnite : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Restore 3 light; draw 1 page; all dice played this Scene gain '[On hit] Inflict 1 Burn'";

        public override string[] Keywords => new string[3] { "Energy_Keyword", "DrawCard_Keyword", "Burn_Keyword" };

        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.cardSlotDetail.RecoverPlayPointByCard(3);
            owner.allyCardDetail.DrawCards(1);
            owner.bufListDetail.AddBuf(new SlickBurn1Buf());
        }
        public class SlickBurn1Buf : BattleUnitBuf
        {
            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                base.OnSuccessAttack(behavior);
                behavior.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 1, _owner);
            }

            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                Destroy();
            }
        }
    }

    // Unshakeable Anger
    // This page is exhausted on use and returns to hand after 3 Scenes
    // [On Use] Gain 'Dangerous' next scene
    public class DiceCardSelfAbility_SlickMod_MidnightAnger : DiceCardSelfAbilityBase
    {
        public static string Desc = "This page is exhausted on use and returns to hand after 3 Scenes\r\n [On Use] Become 'Dangerous' next Scene";

        // Card is exhausted and a new copy is added after 5 Scenes
        public void ExhaustAndReturn()
        {
            card.card.exhaust = true;
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddBackAfterX(card.card.GetID(), 3));
        }

        public override void OnUseCard()
        {
            this.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Dangerous, 1, owner);
        }
    }

    // Furious Flaming Rampage
    // Only usable when 'Dangerous'
    // [On Use] Lose 'Dangerous' status; all dice played this Scene gain '[Clash Win] Inflict 5 Burn to each other'
    public class DiceCardSelfAbility_SlickMod_MidnightRampage : DiceCardSelfAbilityBase
    {
        public static string Desc = "Only usable while 'Dangerous'\r\n [On Use] Lose 'Dangerous'; all dice played this Scene gain '[Clash Win] Inflict 5 Burn to each other'";

        public override string[] Keywords => new string[1] { "Burn_Keyword" };

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            if (owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_Dangerous) == null)
            {
                return false;
            }
            return true;
        }

        public override void OnUseCard()
        {
            owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_Dangerous).Destroy();
            owner.bufListDetail.AddBuf(new SlickBurnEachother5CWBuf());
            base.OnUseCard();
        }

        public class SlickBurnEachother5CWBuf : BattleUnitBuf
        {
            public override void OnWinParrying(BattleDiceBehavior behavior)
            {
                base.OnWinParrying(behavior);
                _owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 5, _owner);
                behavior.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 5, _owner);
            }

            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                Destroy();
            }
        }
    }

    #endregion

    #region --DANS SOLUTIONS--
    public class DiceCardSelfAbility_SlickMod_DanAmature : DiceCardSelfAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Mewhenihityou);
            bool thingy = battleUnitBuf == null;
            if (thingy)
            {
                this.card.target.bufListDetail.AddBuf(new BattleUnitBuf_Mewhenihityou(1));
            }
            else
            {
                battleUnitBuf.stack++;
            }
        }
        public override void OnEndBattle()
        {
            base.OnEndBattle();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Mewhenihityou);
            bool thingy = battleUnitBuf == null;
            if (thingy)
            {
                this.owner.TakeBreakDamage(7, DamageType.Card_Ability);
            }
        }
        public class BattleUnitBuf_Mewhenihityou : BattleUnitBuf
        {
            public BattleUnitBuf_Mewhenihityou(int stack)
            {
                this.stack = stack;
            }
        }
        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            base.OnRoundEnd(unit, self);
        }
    }

    public class DiceCardSelfAbility_SlickMod_DanBrainDamagedEconomy : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            this.owner.TakeBreakDamage(4, DamageType.Card_Ability);
            this.owner.allyCardDetail.DrawCards(1);
        }
    }

    public class DiceCardSelfAbility_SlickMod_DanBrainDamagedLight : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            this.owner.TakeBreakDamage(4, DamageType.Card_Ability);
            this.owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
    }

    public class DiceCardSelfAbility_SlickMod_DanChicago : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            foreach (BattleUnitModel guy in BattleObjectManager.instance.GetAliveList_random(base.owner.faction, 1))
            {
                guy.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.DmgUp, 1, base.owner);
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DanIHaveNoIdeaWhatIsHappening : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            this.owner.TakeBreakDamage(7, DamageType.Card_Ability);
        }
    }

    public class DiceCardSelfAbility_SlickMod_DanInkBlot : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            this.owner.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_BlackTieInk, 1, this.owner);
            this.owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
    public class DiceCardSelfAbility_SlickMod_DanShitAtArt : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf Inky = this.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_BlackTieInk);
            if (Inky != null)
            {
                if (Inky.stack >= 5)
                {
                    this.owner.allyCardDetail.DrawCards(2);
                    Inky.stack -= 2;
                }
                else
                {
                    this.owner.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_BlackTieInk, 1, this.owner);
                }
            }
            else
            {
                this.owner.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_BlackTieInk, 1, this.owner);
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DanAmateurInkwork : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf Inky = this.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_BlackTieInk);
            if (Inky != null)
            {
                if (Inky.stack >= 2)
                {
                    Inky.stack -= 2;
                    if (Inky.stack <= 0)
                    {
                        Inky.Destroy();
                    }
                    this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                    {
                        power = 2
                    });
                }
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DanArtistsInterpretation : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            this.owner.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_BlackTieInk, 2, this.owner);
            BattleUnitBuf Inky = this.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_BlackTieInk);
            if (Inky != null)
            {
                if (Inky.stack >= 7)
                {
                    this.owner.TakeBreakDamage(7, DamageType.Card_Ability);
                    foreach (BattleUnitModel guy in BattleObjectManager.instance.GetAliveList_random(base.owner.faction, 3))
                    {
                        guy.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, 1, base.owner);
                    }
                }
            }

        }
    }
    public class DiceCardSelfAbility_SlickMod_DanInkDroplets : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf Inky = this.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_BlackTieInk);
            if (Inky != null)
            {
                if (Inky.stack >= 7)
                {
                    Inky.stack -= 7;
                    if (Inky.stack <= 0)
                    {
                        Inky.Destroy();
                    }
                    this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                    {
                        power = 2
                    });
                    this.card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_DanInksAllOverYou());
                }
            }
        }
        public class DiceCardAbility_DanInksAllOverYou : DiceCardAbilityBase
        {
            public override void OnSucceedAttack()
            {
                base.OnSucceedAttack();
                this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_BlackTieInk, 2, this.owner);
            }
        }
    }

    #endregion

    #region --WORTHLESS AUTOMATONS--

    // Shock
    // 
    public class DiceCardSelfAbility_SlickMod_Shock : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Restore 2 Light; Channel 1 Lightning Orb; Inflict 1 Paralysis to self next Scene";

        public override string[] Keywords => new string[2] { "Energy_Keyword", "Paralysis_Keyword" };

        public override void OnUseCard()
        {
            base.owner.cardSlotDetail.RecoverPlayPointByCard(2);
            this.owner.bufListDetail.AddBuf(new SlickMod_Orb_Lightning());
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 1, base.owner);
        }
    }

    // Half-Hearted Claw
    // [On Use] Dice on this page gain +1 Power for every “Half-hearted Claw” used last Scene
    public class DiceCardSelfAbility_SlickMod_WhoCaresClaw : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Dice on this page gain +1 Power for every “Half-hearted Claw” used last Scene";

        public override void OnUseCard()
        {
            this.card.owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_SlickMod_WhoCaresClaw(1));

            BattleUnitBuf battleUnitBuf = this.card.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is DiceCardSelfAbility_SlickMod_WhoCaresClaw.BattleUnitBuf_SlickMod_WhoCaresClaw);
            if (battleUnitBuf != null)
            {
                foreach (BattleDiceBehavior battleDiceBehavior in this.card.cardBehaviorQueue)
                {
                    battleDiceBehavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        power = battleUnitBuf.stack
                    });
                }
            }
            base.OnUseCard();
        }

        public class BattleUnitBuf_SlickMod_WhoCaresClaw : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                this.Destroy();
                base.OnRoundEnd();
            }

            public BattleUnitBuf_SlickMod_WhoCaresClaw(int Stack)
            {
                this.stack = Stack;
            }
        }

    }

    // [On Use] Lower this page's Cost by 1
    public class DiceCardSelfAbility_SlickMod_OnUseCostDown1 : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Lower this page's Cost by 1";

        public override void OnUseCard()
        {
            card.card.AddBuf(new SlickMod_CostDownSelfBuf());
        }
    }

    // This page prompts no action because it has no non-Counter dice.
    // [On Use] Draw 3 pages
    public class DiceCardSelfAbility_SlickMod_Draw3OopsAllCounters : DiceCardSelfAbilityBase
    {
        public static string Desc = "This page prompts no action because it has no non-Counter dice.\r\n[On Use] Draw 3 pages";

        public override string[] Keywords => new string[1] { "DrawCard_Keyword" };

        public override void OnUseCard()
        {
            base.owner.allyCardDetail.DrawCards(3);
        }
    }

    #endregion

    #region --UN CHEYTAC--

    public class DiceCardSelfAbility_SlickMod_BizarreArtifact : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Restore 1 Light; Draw 1 page; Lower this page's Cost by 1 (Up to 2 times); Dice on this page lose Power equal to the difference between it's current and original Cost";

        public override void OnUseCard()
        {
            base.owner.allyCardDetail.DrawCards(1);
            base.owner.cardSlotDetail.RecoverPlayPointByCard(1);
            this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                power = -Math.Abs(this.card.card.GetCost() - this.card.card.GetOriginCost())
            });
            List<BattleDiceCardBuf> list = this.card.card.GetBufList().FindAll((BattleDiceCardBuf x) => x is SlickMod_CostDownSelfBuf);
            bool flag = list.Count < 2;
            if (flag)
            {
                this.card.card.AddBuf(new SlickMod_CostDownSelfBuf());
            }
        }
    }

    #endregion

    #region -DROWNED-
    public class DiceCardSelfAbility_SlickMod_DrownedSinkersSinking : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
            {
                BattleUnitBuf battleUnitBuf = battleUnitModel.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                if (battleUnitBuf != null)
                {
                    battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_Sinking, battleUnitBuf.stack / 2, this.owner);
                }
            }
        }
    }

    public class DiceCardSelfAbility_SlickMod_DrownedImtakingyouinfortreason : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, 1, this.owner);
            foreach (BattleUnitModel item in BattleObjectManager.instance.GetAliveList_random((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy, 1))
            {
                item.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, 1, this.owner);
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DrownedLoudNoise : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                int dice = battleUnitBuf.stack / 5;
                for (int i = 0; i < dice; i++)
                {
                    DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId("SlickMod", 137), false);
                    new DiceBehaviour();
                    int num = 0;
                    foreach (DiceBehaviour diceBehaviour in cardItem.DiceBehaviourList)
                    {
                        BattleDiceBehavior battleDiceBehavior = new BattleDiceBehavior();
                        battleDiceBehavior.behaviourInCard = diceBehaviour.Copy();
                        battleDiceBehavior.AddAbility(new DiceCardAbility_SlickMod_DrownedLoudNoiseDie());
                        battleDiceBehavior.SetIndex(num++);
                        this.card.AddDice(battleDiceBehavior);

                    }
                }
            }
        }
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            base.BeforeGiveDamage(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = -9999
            });
        }
    }

    public class DiceCardSelfAbility_SlickMod_DrownedDrop : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack >= 10)
                {
                    card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                    {
                        power = 5
                    });
                }
            }
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack >= 10)
                {
                    foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
                    {
                        battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedOmegaCringe, 5, this.owner);
                    }
                }
            }
        }
    }

    public class DiceCardSelfAbility_SlickMod_DrownedTwistingPathways : DiceCardSelfAbilityBase
    {
        protected virtual int Onslaught
        {
            get
            {
                BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                if (battleUnitBuf == null)
                {
                    return 0;
                }
                else
                {
                    return battleUnitBuf.stack / 5;
                }
            }
        }
        public override void OnApplyCard()
        {
            this.card.subTargets.Clear();
            List<BattlePlayingCardDataInUnitModel.SubTarget> list2 = new List<BattlePlayingCardDataInUnitModel.SubTarget>(this.card.subTargets);
            list2.Add(new BattlePlayingCardDataInUnitModel.SubTarget
            {
                target = this.card.target,
                targetSlotOrder = this.card.targetSlotOrder
            });
            int targetslotorder = this.card.targetSlotOrder;
            foreach (BattlePlayingCardDataInUnitModel.SubTarget subTarget in list2)
            {
                List<BattlePlayingCardDataInUnitModel.SubTarget> list3 = new List<BattlePlayingCardDataInUnitModel.SubTarget>();
                int num = UnityEngine.Mathf.Clamp(this.Onslaught, 0, (subTarget.target.speedDiceResult.Count - 1));
                for (int j = 0; j < num; j++)
                {
                    if (!list2.Exists((BattlePlayingCardDataInUnitModel.SubTarget x) => x.target == subTarget.target && x.targetSlotOrder <= j))
                    {
                        list3.Add(new BattlePlayingCardDataInUnitModel.SubTarget
                        {
                            target = subTarget.target,
                            targetSlotOrder = j + 1
                        });
                    }
                    else
                    {
                        list3.Add(new BattlePlayingCardDataInUnitModel.SubTarget
                        {
                            target = subTarget.target,
                            targetSlotOrder = j + 1
                        });
                    }
                }
            }
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack >= 5)
                {
                    foreach (BattleUnitModel item in BattleObjectManager.instance.GetAliveList_random((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy, 2))
                    {
                        item.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedOmegaCringe, 1, this.owner);
                    }
                }
            }
        }

    }

    public class DiceCardSelfAbility_SlickMod_DrownedLetitGo : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                int num = battleUnitBuf.stack;
                int power = Mathf.Clamp(num, 1, 10);
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = power
                });
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DrownedSmothering : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                int num = battleUnitBuf.stack / 4;
                this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_SinkingCount, num, this.owner);
                this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, num * 3, this.owner);
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DrownedMeaningless : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack >= 7)
                {
                    card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                    {
                        power = 5
                    });
                }
            }
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack >= 7)
                {
                    foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
                    {
                        battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedOmegaCringe, 2, this.owner);
                    }
                }
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DrownedNumbness : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            this.owner.bufListDetail.AddBuf(new BattleUnitBuf_DrownedImfuckinginvincible());
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
            {
                battleUnitModel.bufListDetail.AddBuf(new BattleUnitBuf_DrownedYoursodepressedrightnow());
            }
        }
        public class BattleUnitBuf_DrownedImfuckinginvincible : BattleUnitBuf
        {
            public override int GetDamageReduction(BattleDiceBehavior behavior)
            {
                return 9999999;
            }
            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                this.Destroy();
            }
        }
        public class BattleUnitBuf_DrownedYoursodepressedrightnow : BattleUnitBuf
        {
            public override int GetDamageReduction(BattleDiceBehavior behavior)
            {
                return 9999999;
            }
            public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
            {
                base.OnTakeDamageByAttack(atkDice, dmg);
                this._owner.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_Sinking, 1, this._owner);
                this._owner.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_SinkingCount, 1, this._owner);
            }
            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                this.Destroy();
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DrownedEmptyFeeling : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
            {
                battleUnitModel.bufListDetail.AddBuf(new BattleUnitBuf_DrownedCripplingDepression());
            }
        }
        public class BattleUnitBuf_DrownedCripplingDepression : BattleUnitBuf
        {
            public override void BeforeRollDice(BattleDiceBehavior behavior)
            {
                base.BeforeRollDice(behavior);
                BattleUnitBuf battleUnitBuf = this._owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_Sinking);
                if (battleUnitBuf != null)
                {
                    int depression = battleUnitBuf.stack / 5;
                    behavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        power = -depression
                    });
                }
            }
            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                this.Destroy();
            }
        }
    }

    public class DiceCardSelfAbility_SlickMod_DrownedGnawing : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
            {
                BattleUnitBuf battleUnitBuf = battleUnitModel.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                if (battleUnitBuf != null)
                {
                    battleUnitModel.TakeDamage(battleUnitBuf.stack, DamageType.Emotion);
                    battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_SinkingCount, battleUnitBuf.stack / 4, this.owner);
                }
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DrownedNoLight : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                int powerloss = battleUnitBuf.stack * 5;
                this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = -powerloss
                });
            }
        }
        public override void OnEndBattle()
        {
            base.OnEndBattle();
            BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack >= 10)
                {
                    battleUnitBuf.stack -= 10;
                    if (battleUnitBuf.stack <= 0)
                    {
                        battleUnitBuf.Destroy();
                    }
                }
            }
        }
    }
    public class DiceCardSelfAbility_SlickMod_DrownedNoDamage : DiceCardSelfAbilityBase
    {
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            base.BeforeGiveDamage(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = -9999,
                breakDmg = -9999
            });
        }
    }
    #endregion

}
