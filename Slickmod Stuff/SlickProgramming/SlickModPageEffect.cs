using System;
using LOR_DiceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using static SlickRuinaMod.DiceCardSelfAbility_SlickMod_PackHuntingTactics;

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

        public class BattleUnitBuf_SlickMod_AddBackAfter4 : BattleUnitBuf
        {
            public int _count;

            public LorId _cardId = LorId.None;

            // Notes card ID and turn count
            public BattleUnitBuf_SlickMod_AddBackAfter4(LorId cardId, int turnCount)
            {
                _cardId = cardId;
                _count = turnCount;
            }

            public override void OnRoundStart()
            {
                _count--;
                if (_count <= 0)
                {
                    _owner.allyCardDetail.AddNewCard(_cardId);
                    Destroy();
                }
            }
        }
        // Card is exhausted and a new copy is added after 4 Scenes
        public void ExhaustAndReturn()
        {
            card.card.exhaust = true;
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_SlickMod_AddBackAfter4(card.card.GetID(), 4));
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

}
