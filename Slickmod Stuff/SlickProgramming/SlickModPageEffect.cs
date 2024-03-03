using System;
using LOR_DiceSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using static SlickRuinaMod.DiceCardSelfAbility_SlickMod_PackHuntingTactics;
using static DiceCardSelfAbility_unitePower;
using Hat_Method;
using static SlickRuinaMod.DiceCardSelfAbility_SlickMod_BarghestNail;

namespace SlickRuinaMod
{
    #region - COURIERS 1 -

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

    #region - SNOW COYOTE OFFICE -

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

        public static string Desc = "Turner's Exclusive Page\nGive all other allies a single-use copy of 'Pack Hunting Tactics' that exhausts at the end of the Scene; user and all allied Snow Coyote Fixers restore 2 Light";
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

    #region - INFERNAL CORPS I -
    
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

    #region - UN GOLDEN SPARK -

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
    public class DiceCardSelfAbility_SparkBreakdownUN : DiceCardSelfAbilityBase
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
    public class DiceCardSelfAbility_SparkMirageUN : DiceCardSelfAbilityBase
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
    // [On Use] If used alongside "Karyuken", gain 3 Poise
    public class DiceCardSelfAbility_TatsumakiUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo: Sakanagi]\n[On Use] If used alongside \"Karyuken\", gain 3 Poise";

        public override string[] Keywords => new string[2] { "SlickMod_Combo", "Hat_Poise_Keyword" };

        public override void OnStartBattle()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_UsingTatsumaki());
        }

        public override void OnUseCard()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_SakanagiComboPieceA());

            if (base.owner.bufListDetail.HasBuf<BattleUnitBuf_UsingKaryuken>())
            {
                this.owner.bufListDetail.AddKeywordBufThisRoundByCard(Hat_KeywordBuf.KeywordBufs.Poise, 3, owner);
            }
        }
    }

    // Karyuken: Page Effect
    // [Combo: Sakanagi]
    // [On Use] If used alongside "Tatsumaki", restore 1 Light
    public class DiceCardSelfAbility_KaryukenUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo: Sakanagi]\n[On Use] If used alongside \"Tatsumaki\", restore 1 Light";

        public override string[] Keywords => new string[2] { "SlickMod_Combo", "Energy_Keyword" };

        public override void OnStartBattle()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_UsingKaryuken());
        }

        public override void OnUseCard()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_SakanagiComboPieceB());

            if (base.owner.bufListDetail.HasBuf<BattleUnitBuf_UsingTatsumaki>())
            {
                base.owner.cardSlotDetail.RecoverPlayPointByCard(1);
            }
        }
    }

    // Sakanagi: Page Effect
    // [Combo Finisher]
    // [On Use] Gain 5 Poise
    public class DiceCardSelfAbility_SakanagiUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo Finisher]\n[On Use] Gain 5 Poise";

        public override string[] Keywords => new string[2] { "SlickMod_ComboFinisher", "Hat_Poise_Keyword" };

        public override void OnUseCard()
        {
            this.owner.bufListDetail.AddKeywordBufThisRoundByCard(Hat_KeywordBuf.KeywordBufs.Poise, 5, owner);
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            unit.allyCardDetail.ExhaustACardAnywhere(self);
        }
    }

    // Spiral Strike: Page Effect
    // Combo: Tempest
    // [On Use] Gain 2 Haste next Scene
    public class DiceCardSelfAbility_WindStepUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo: Tempest]\n[On Use] Gain 2 Haste next Scene";

        public override string[] Keywords => new string[2] { "SlickMod_Combo", "Quickness_Keyword" };

        public override void OnUseCard()
        {
            card.owner.bufListDetail.AddBuf(new BattleUnitBuf_TempestComboPieceA());
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, base.owner);
        }
    }

    // Spiral Strike: Page Effect
    // Combo: Tempest
    // [On Use] Gain 1 Poise for every 3 Samsara on self (max. 5)
    public class DiceCardSelfAbility_SpiralStrikeUN : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combo: Tempest]\n[On Use] Gain 1 Poise for every 3 Samsara on self (max. 5)";

        public override string[] Keywords => new string[3] { "SlickMod_Combo", "Hat_Poise_Keyword", "SlickMod_Samsara" };

        public override void OnUseCard()
        {
            BattleUnitBuf_SlickMod_SparkSamsara battleUnitBuf_slickSparkSamsaraSpiralStrikeSwag = this.card.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara) as BattleUnitBuf_SlickMod_SparkSamsara;
            if (battleUnitBuf_slickSparkSamsaraSpiralStrikeSwag != null && battleUnitBuf_slickSparkSamsaraSpiralStrikeSwag.stack >= 3)
            {
                this.owner.bufListDetail.AddKeywordBufThisRoundByCard(Hat_KeywordBuf.KeywordBufs.Poise, Mathf.Min(battleUnitBuf_slickSparkSamsaraSpiralStrikeSwag.stack / 3, 5), this.owner);
            }
        }
    }

    // Tempest: Page Effect
    // [Combo Finisher]
    // [Combat Start] Gain 1 Strength this Scene
    public class DiceCardSelfAbility_TempestUN : DiceCardSelfAbilityBase
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

    #region - BACKSTREET SLUGGERS -

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
        public static string Desc = "This page is exhausted on use and returns to hand after 3 Scenes\r\n [On Use] Gain 'Geared Up' next scene";

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

    #region - MIDNIGHT OFFICE -

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
    // This page is exhausted on use and returns to hand after 5 Scenes
    // [On Use] Gain 'Dangerous' next scene
    public class DiceCardSelfAbility_SlickMod_MidnightAnger : DiceCardSelfAbilityBase
    {
        public static string Desc = "This page is exhausted on use and returns to hand after 5 Scenes\r\n [On Use] Become 'Dangerous' next Scene";

        // Card is exhausted and a new copy is added after 5 Scenes
        public void ExhaustAndReturn()
        {
            card.card.exhaust = true;
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddBackAfterX(card.card.GetID(), 5));
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

    #region - UN CHEYTAC -

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
            List<BattleDiceCardBuf> list = this.card.card.GetBufList().FindAll((BattleDiceCardBuf x) => x is DiceCardSelfAbility_SlickMod_BizarreArtifact.CostDownSelfBuf);
            bool flag = list.Count < 2;
            if (flag)
            {
                this.card.card.AddBuf(new DiceCardSelfAbility_SlickMod_BizarreArtifact.CostDownSelfBuf());
            }
        }

        public class CostDownSelfBuf : BattleDiceCardBuf
        {
            public override int GetCost(int oldCost)
            {
                return oldCost - 1;
            }
        }
    }

    #endregion

}
