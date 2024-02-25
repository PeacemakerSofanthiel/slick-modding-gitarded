using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace The_Aftermath_Collection
{

    #region - GENERIC ABILITIES - 

    // [On Use] Restore 1 Light next Scene and the Scene after
    public class DiceCardSelfAbility_AftermathEnergy2Delayed : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_DelayedLight2());
        }

        public override string[] Keywords => new string[] { "Energy_Keyword" };

        public static string Desc = "[On Use] Restore 1 Light next Scene and the Scene after";
    }


    #endregion


    #region - ZWEI WESTERN SECTION 3 -

    public class DiceCardSelfAbility_AftermathStackFunnyPage : DiceCardSelfAbilityBase
    {
        public override void OnStartBattleAfterCreateBehaviour()
        {
            List<BattleDiceCardModel> list = owner.allyCardDetail.GetHand().FindAll(x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 50105));
            int count = list.Count;

            if (count > 0)
            {
                DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId(AftermathCollectionInitializer.packageId, 50105), false);
                BattleDiceBehavior funnyDie = new BattleDiceBehavior
                {
                    behaviourInCard = cardItem.DiceBehaviourList[0].Copy()
                };

                foreach (BattleDiceCardModel combatPage in list)
                {
                    owner.cardSlotDetail.keepCard.AddDice(funnyDie);
                    owner.allyCardDetail.ExhaustCardInHand(combatPage);
                }
            }
        }

        public override string[] Keywords => new string[] { "bstart_Keyword" };
        public static string Desc = "[Combat Start] If 'Justice: Duty' is in hand, exhaust all of its copies, then add the dice of the discarded pages to this";
    }

    public class DiceCardSelfAbility_AftermathLiamFlexible : DiceCardSelfAbilityBase
    {
        public override string[] Keywords => new string[] { "Duty_onlypage_liam_Keyword" };
    }

    public class DiceCardSelfAbility_AftermathTamoraDemoralize : DiceCardSelfAbilityBase
    {
        public override string[] Keywords => new string[] { "Demoralize_onlypage_tamora_Keyword" };
    }

    public class DiceCardSelfAbility_AftermathFreshMeat : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            card.ApplyDiceAbility(DiceMatch.AllAttackDice, new DiceCardAbility_recoverHp2atk { });
        }
        public override string[] Keywords => new string[] { "bstart_Keyword" };
        public static string Desc = "[Combat Start] All offensive dice on this page gain '[On Hit] Recover 2 HP'";
    }

    public class DiceCardSelfAbility_AftermathDanielDiscipline : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.card.ignorePower = true;
        }

        public override void OnStartParrying()
        {
            BattleUnitModel target = this.card.target;
            if (target == null || target.currentDiceAction == null)
            {
                return;
            }
            target.currentDiceAction.ignorePower = true;
        }

        public override string[] Keywords => new string[] { "Discipline_onlypage_daniel_Keyword" };
        public static string Desc = "Dice on this page and the one clashing with it are unaffected by Power gain or loss";
    }

    /*
    * The reason the two effects below are even Card Effects in the first place is because the ReduceDmgNatRoll breaks against
    * Mass Summations, leading to the summation result being used as the stacks of resistance, effectively giving the character
    * a "pseudo-Unrelenting" for a couple of scenes. On one hand- it's pretty funny. On the other- it's very broken.
    */
    public class DiceCardSelfAbility_AftermathReduceDmgNatRollApply2Ally1Protection : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            card.ForeachQueue(DiceMatch.AllAttackDice, delegate (BattleDiceBehavior x)
            {
                x.AddAbility(new DiceCardAbility_AftermathReduceDmgNatRoll());
            });
        }

        public override void OnStartBattle()
        {
            base.OnStartBattle();
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList_random(base.owner.faction, 2))
            {
                battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Protection, 1, base.owner);
            }
        }

        public override string[] Keywords => new string[] { "Protection_Keyword" };
        public static string Desc = "[Combat Start] Give 1 Protection to two random allies [On Use] All offensive dice on this page gain '[On Clash Lose] Reduce incoming damage by the natural roll'";

    }

    public class DiceCardSelfAbility_AftermathReduceDmgNatRollApplyAndDraw1 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            card.ForeachQueue(DiceMatch.AllAttackDice, delegate (BattleDiceBehavior x)
            {
                x.AddAbility(new DiceCardAbility_AftermathReduceDmgNatRoll());
            });
            owner.allyCardDetail.DrawCards(1);
        }
        public override string[] Keywords => new string[] { "Protection_Keyword" };
        public static string Desc = "[On Use] Draw 1 page; All offensive dice on this page gain '[On Clash Lose] Reduce incoming damage by the natural roll'";

    }

    #endregion

    #region - TIES & FAMILY -

    // [On Play; Target Ally] Target restores 3 Light
    // If starved, add 'Rise From The Ashes' to hand and exhaust this page instead
    public class DiceCardSelfAbility_Aftermathenergy3draw1 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            base.OnUseInstance(unit, self, targetUnit);
            if (unit.passiveDetail.HasPassive<PassiveAbility_Aftermath_Aftermathstarvation>() || unit.passiveDetail.HasPassive<PassiveAbility_Aftermath_Aftermathstarvation2>())
            {
                unit.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60217));
                self.exhaust = true;
            } else
            {
                targetUnit.cardSlotDetail.RecoverPlayPoint(3);
            }
        }

        public override bool IsOnlyAllyUnit()
        {
            return true;
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "BreadBoys_Only_Keyword",
                    "Energy_Keyword"
                };
            }
        }

        public static string Desc = "[On Play; Target Ally] Target restores 3 Light\nIf starved, add 'Rise From The Ashes' to hand and exhaust this page instead";
    }


    // [On Use] Remove all dice from this page, discard up to 4 Venoms and add their dice to this page
    public class DiceCardSelfAbility_AftermathDiscardAllVenomAddDice : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            card.card.XmlData.Spec.Ranged = CardRange.Far;
            List<BattleDiceCardModel> list = owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword"));
            card.DestroyDice(DiceMatch.AllDice);
            if (list.Count > 0)
            {
                int limit;
                if (list.Count >= 4)
                {
                    limit = 4;
                }
                else
                {
                    limit = list.Count;
                }
                while (limit > 0)
                {
                    BattleDiceCardModel page = list[limit - 1];
                    BattleDiceBehavior die = page.CreateDiceCardBehaviorList()[0];
                    owner.allyCardDetail.DiscardACardByAbility(page);
                    die.behaviourInCard.MotionDetail = MotionDetail.F;
                    card.AddDice(die);
                    limit--;
                }
            }
        }

        public override void OnEnterCardPhase(BattleUnitModel unit, BattleDiceCardModel self)
        {
            base.OnEnterCardPhase(unit, self);
            self.XmlData.Spec.Ranged = CardRange.Near;
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Family_Only_Keyword",
                            "Venom_Use_Keyword"
                };
            }
        }

        public static string Desc = "[On Use] Change this page's range to Ranged, remove all dice from this page, discard up to 4 Venoms and add their dice to this page";
    }

    // Exhausts when used or discarded;
    // [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Paralysis next Scene
    public class DiceCardSelfAbility_AftermathVenomParalysis : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.OnActivate(base.owner, this.card.card);
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            this.OnActivate(unit, self);
        }

        private void OnActivate(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
            if (!unit.bufListDetail.HasBuf<BattleUnitBuf_VenomParalysis>())
            {
                unit.bufListDetail.AddBuf(new BattleUnitBuf_VenomParalysis());
            }
        }

        public class BattleUnitBuf_VenomParalysis : BattleUnitBuf
        {
            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                BattleUnitModel target = behavior.card.target;
                if (target != null && behavior.card.card.GetSpec().Ranged == CardRange.Near)
                {
                    target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 1, this._owner);
                }
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                        "Paralysis_Keyword",
                        "Venom_Keyword"
                };
            }
        }

        public static string Desc = "Exhausts when used or discarded; [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Paralysis next Scene";
    }

    // Exhausts when used or discarded;
    // [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Disarm next Scene
    public class DiceCardSelfAbility_AftermathVenomDisarm : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.OnActivate(base.owner, this.card.card);
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            this.OnActivate(unit, self);
        }

        private void OnActivate(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
            if (!unit.bufListDetail.HasBuf<BattleUnitBuf_VenomDisarm>())
            {
                unit.bufListDetail.AddBuf(new BattleUnitBuf_VenomDisarm());
            }
        }

        public class BattleUnitBuf_VenomDisarm : BattleUnitBuf
        {
            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                BattleUnitModel target = behavior.card.target;
                if (target != null && behavior.card.card.GetSpec().Ranged == CardRange.Near)
                {
                    target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Disarm, 1, this._owner);
                }
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                        "Disarm_Keyword",
                        "Venom_Keyword"
                };
            }
        }

        public static string Desc = "Exhausts when used or discarded; [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Disarm next Scene";
    }

    // Exhausts when used or discarded;
    // [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Bind next Scene
    public class DiceCardSelfAbility_AftermathVenomBind : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.OnActivate(base.owner, this.card.card);
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            this.OnActivate(unit, self);
        }

        private void OnActivate(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
            if (!unit.bufListDetail.HasBuf<BattleUnitBuf_VenomBind>())
            {
                unit.bufListDetail.AddBuf(new BattleUnitBuf_VenomBind());
            }
        }

        public class BattleUnitBuf_VenomBind : BattleUnitBuf
        {
            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                BattleUnitModel target = behavior.card.target;
                if (target != null && behavior.card.card.GetSpec().Ranged == CardRange.Near)
                {
                    target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, this._owner);
                }
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                        "Binding_Keyword",
                        "Venom_Keyword"
                };
            }
        }

        public static string Desc = "Exhausts when used or discarded; [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Bind next Scene";
    }

    // Exhausts when used or discarded;
    // [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Feeble, 1 Disarm and 1 Bind next Scene
    public class DiceCardSelfAbility_AftermathVenomEX : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.OnActivate(base.owner, this.card.card);
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            this.OnActivate(unit, self);
        }

        private void OnActivate(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
            if (!unit.bufListDetail.HasBuf<BattleUnitBuf_VenomEX>())
            {
                unit.bufListDetail.AddBuf(new BattleUnitBuf_VenomEX());
            }
        }

        public override bool BeforeAddToHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (unit.allyCardDetail.GetHand().Find(x => x.GetID() == self.GetID()) != null)
            {
                return false;
            }
            return true;
        }

        public class BattleUnitBuf_VenomEX : BattleUnitBuf
        {
            private int stacks = 0;

            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                BattleUnitModel target = behavior.card.target;
                if (target != null && behavior.card.card.GetSpec().Ranged == CardRange.Near && stacks < 2)
                {
                    target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Weak, 1, this._owner);
                    target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Disarm, 1, this._owner);
                    target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, this._owner);
                    stacks++;
                }
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                        "Weak_Keyword",
                        "Disarm_Keyword",
                        "Binding_Keyword",
                        "Venom_Keyword"
                };
            }
        }

        public static string Desc = "Exhausts when used or discarded, only one can be held at once; [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Feeble, 1 Disarm and 1 Bind next Scene (Max. 2)";
    }

    // [On Use] Discard up to 5 Venoms, draw 1 page for every 2 Venoms discarded,
    // and an extra one if 5 Venoms were discarded
    public class DiceCardSelfAbility_AftermathSoberUp : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            List<BattleDiceCardModel> bruh = owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword"));
            int count = bruh.Count;
            if (count > 5)
            {
                count = 5;
            }

            foreach (BattleDiceCardModel venom in bruh)
            {
                owner.allyCardDetail.DiscardACardByAbility(venom);

                if (count == 5)
                {
                    owner.allyCardDetail.DrawCards(1);
                }

                else if (count % 2 == 0)
                {

                    owner.allyCardDetail.DrawCards(1);
                }

                count--;
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Family_Only_Keyword",
                            "Venom_Use_Keyword",
                            "DrawCard_Keyword"
                };
            }
        }

        public static string Desc = "[On Use] Discard up to 5 Venoms, draw 1 page for every 2 Venoms discarded, and an extra one if 5 Venoms were discarded";
    }

    // [Combat Start] Discard all Venoms from hand,
    // take 1 damage and gain 1 Strength/Endurance/Haste next Scene
    // (depending on type of Venom discarded) for every Venom discarded this way
    public class DiceCardSelfAbility_AftermathOverdose : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            List<BattleDiceCardModel> list = owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword"));
            int count = list.Count;

            if (count > 0)
            {
                owner.TakeDamage(count, DamageType.Card_Ability);
                foreach (BattleDiceCardModel page in list)
                {
                    if (page.CheckForKeyword("Binding_Keyword") && page.CheckForKeyword("Weak_Keyword") && page.CheckForKeyword("Disarm_Keyword"))
                    {
                        owner.allyCardDetail.DiscardACardByAbility(page);
                        owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, base.owner);
                        owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, base.owner);
                        owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
                    }

                    else if (page.CheckForKeyword("Binding_Keyword"))
                    {
                        owner.allyCardDetail.DiscardACardByAbility(page);
                        owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
                    }

                    else if (page.CheckForKeyword("Paralysis_Keyword"))
                    {
                        owner.allyCardDetail.DiscardACardByAbility(page);
                        owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, base.owner);
                    }

                    else if (page.CheckForKeyword("Disarm_Keyword"))
                    {
                        owner.allyCardDetail.DiscardACardByAbility(page);
                        owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, base.owner);
                    }
                }
            }
        }
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Family_Only_Keyword",
                    "Venom_Use_Keyword"
                };
            }
        }

        public static string Desc = "[On Use] Discard all Venoms from hand, take 1 damage and gain 1 Strength/Endurance/Haste next Scene (depending on type of Venom discarded) for every Venom discarded this way";
    }

    // [Combat Start] Discard a random Venom from hand,
    // take 1 damage and gain 1 Strength/Endurance/Haste next Scene
    // (depending on type of Venom discarded)
    public class DiceCardSelfAbility_AftermathInjectOne : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            List<BattleDiceCardModel> list = owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword"));
            int count = list.Count;

            if (count > 0)
            {
                owner.TakeDamage(1, DamageType.Card_Ability);
                BattleDiceCardModel page = RandomUtil.SelectOne<BattleDiceCardModel>(list);
                owner.allyCardDetail.DiscardACardByAbility(page);

                if (page.CheckForKeyword("Binding_Keyword") && page.CheckForKeyword("Weak_Keyword") && page.CheckForKeyword("Disarm_Keyword"))
                {
                    owner.allyCardDetail.DiscardACardByAbility(page);
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, base.owner);
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, base.owner);
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
                }

                else if (page.CheckForKeyword("Binding_Keyword"))
                {
                    owner.allyCardDetail.DiscardACardByAbility(page);
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
                }

                else if (page.CheckForKeyword("Paralysis_Keyword"))
                {
                    owner.allyCardDetail.DiscardACardByAbility(page);
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, base.owner);
                }

                else if (page.CheckForKeyword("Disarm_Keyword"))
                {
                    owner.allyCardDetail.DiscardACardByAbility(page);
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, base.owner);
                }
            }
        }
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Family_Only_Keyword",
                    "Venom_Use_Keyword"
                };
            }
        }

        public static string Desc = "[On Use] Discard a random Venom from hand, take 1 damage and gain 1 Strength/Endurance/Haste next Scene (depending on type of Venom discarded)";
    }

    // [On Use] Restore 1 Light; add 1 random Venom to hand
    public class DiceCardSelfAbility_AftermathExtract : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            VenomCardModel.AddVenomToHand(owner);
        }
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Family_Only_Keyword",
                            "Venom_Keyword",
                            "Energy_Keyword"
                };
            }
        }

        public static string Desc = "[On Use] Restore 1 Light; add 1 random Venom to hand";
    }

    // [On Use] Restore 1 Light; add 2 random Venoms to hand
    public class DiceCardSelfAbility_AftermathSynth : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            VenomCardModel.AddVenomToHand(owner);
            VenomCardModel.AddVenomToHand(owner);
        }
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Family_Only_Keyword",
                            "Venom_Keyword",
                            "Energy_Keyword"
                };
            }
        }

        public static string Desc = "[On Use] Restore 1 Light; add 2 random Venoms to hand";
    }

    // If Blade Thesis is active, all dice on this page roll their maximum value.
    // If not, all dice on this page roll their minimum value.
    public class DiceCardSelfAbility_AftermathBladeThesis : DiceCardSelfAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            if (owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>())
            {
                behavior.behaviourInCard.Min = behavior.GetDiceVanillaMax();

            }
            else
            {
                behavior.behaviourInCard.Dice = behavior.GetDiceVanillaMin();
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Silvio_Only_Keyword"
                };
            }
        }

        public static string Desc = "If Blade Thesis is active, dice on this page roll their maximum value; if not, dice on this page roll their minimum value.";
    }

    // [Combat Start] Give 1 Strength to all allies this Scene
    public class DiceCardSelfAbility_AftermathBenitoCommand : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            foreach (BattleUnitModel bruh in BattleObjectManager.instance.GetAliveList(card.target.faction))
            {
                bruh.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1, owner);
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Benito_Only_Keyword",
                            "Strength_Keyword",
                            "bstart_Keyword"
                };
            }
        }

        public static string Desc = "[Combat Start] Give 1 Strength to target's team this Scene";
    }

    // [Combat Start] Inflict 5 Bind to target next Scene and 3 Bind the Scene after
    public class DiceCardSelfAbility_AftermathSlowAmulet : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            if (card.target != null)
            {
                card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 5, owner);
                card.target.bufListDetail.AddKeywordBufNextNextByCard(KeywordBuf.Binding, 3, owner);
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Benito_Only_Keyword",
                            "Binding_Keyword",
                            "bstart_Keyword"
                };
            }
        }

        public static string Desc = "[Combat Start] Inflict 5 Bind to target next Scene and 3 Bind the Scene after";
    }

    // [Combat Start] Target is not influenced by Power gain/loss for this Scene
    public class DiceCardSelfAbility_AftermathEqualizerAmulet : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            if (card.target != null)
            {
                card.target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.NullifyPower, 1, owner);
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Benito_Only_Keyword",
                            "NullifyPower",
                            "bstart_Keyword"
                };
            }
        }

        public static string Desc = "[Combat Start] Target is not influenced by Power gain/loss for this Scene";
    }

    // [On Play] Target draws 3 pages
    public class DiceCardSelfAbility_Aftermathdraw3pages : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            base.OnUseInstance(unit, self, targetUnit);
            targetUnit.allyCardDetail.DrawCards(3);
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Benito_Only_Keyword",
                    "DrawCard_Keyword"
                };
            }
        }

        public static string Desc = "[On Play] Target draws 3 pages";
    }

    // [Combat Start] Give target a Counter Evade die (4~8)
    public class DiceCardSelfAbility_AftermathOrderRetreat : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            if (card.target != null)
            {
                BattleDiceCardModel cardItem = BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(new LorId(AftermathCollectionInitializer.packageId, 60128)));
                BattleDiceBehavior battleDiceBehavior = cardItem.CreateDiceCardBehaviorList()[0];
                card.target.cardSlotDetail.keepCard.AddBehaviour(cardItem, battleDiceBehavior);
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Benito_Only_Keyword",
                            "bstart_Keyword"
                };
            }
        }

        public static string Desc = "[Combat Start] Give target an Evade die (4~8)";
    }

    // [Combat Start] Inflict 2 Disarm on target's team this Scene
    public class DiceCardSelfAbility_AftermathBenitoIntimidate : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            foreach (BattleUnitModel bruh in BattleObjectManager.instance.GetAliveList(card.target.faction))
            {
                bruh.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Disarm, 2, owner);
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Benito_Only_Keyword",
                            "Disarm_Keyword",
                            "bstart_Keyword"
                };
            }
        }

        public static string Desc = "[Combat Start] Inflict 2 Disarm on target's team this Scene";
    }

    // [On Use] Add 'Sharp-Suited Strikes' to hand
    public class DiceCardSelfAbility_AftermathHiddenWeapon : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60126), true).AddBuf(new BattleDiceCardBuf_HiddenWeapon());
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, 3, owner);
        }

        public class BattleDiceCardBuf_HiddenWeapon : BattleDiceCardBuf
        {
            public override void OnRoundStart()
            {
                base.OnRoundStart();
                _card.temporary = true;
            }
        }

        public override string[] Keywords => new string[] { "HiddenWeapon_Keyword", "DrawCard_Keyword" };


        public static string Desc = "[On Use] Add 'Sharp-Suited Strikes' to hand; gain 3 Damage Up next Scene";
    }

    // Can only be used this Scene (for Sharp-Suited Strikes)
    public class DiceCardSelfAbility_AftermathSharpStrikes : DiceCardSelfAbilityBase
    {
        public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (unit.customBook.ClassInfo.Name == "A Yellow Ties Officer" || unit.customBook.ClassInfo.Name == "A Yellow Ties Officer's Page")
            {
                unit.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
                unit.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
                unit.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
                unit.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S4);
                unit.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S4);
                unit.view.charAppearance.SetAltMotion(ActionDetail.Move, ActionDetail.Special);
                unit.view.charAppearance.SetAltMotion(ActionDetail.Guard, ActionDetail.S5);
                unit.view.charAppearance.SetAltMotion(ActionDetail.Evade, ActionDetail.S6);
                unit.view.charAppearance.SetAltMotion(ActionDetail.Damaged, ActionDetail.S7);
            }
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            base.OnRoundEnd(unit, self);
            if (unit.customBook.ClassInfo.Name == "A Yellow Ties Officer" || unit.customBook.ClassInfo.Name == "A Yellow Ties Officer's Page")
            {
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Hit);
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Slash);
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Penetrate);
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Default);
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Move);
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Guard);
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Evade);
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Damaged);
                unit.view.charAppearance.RemoveAltMotion(ActionDetail.Standing);
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

        public static string Desc = "Can only be used this Scene";
    }

    // [Combat Start] Heal 50 HP and lose 25 Stagger
    public class DiceCardSelfAbility_AftermathTorch : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            owner.RecoverHP(35);
            owner.breakDetail.TakeBreakDamage(35, DamageType.Card_Ability);
            card.card.exhaust = true;
        }

        public static string Desc = "[Combat Start] Restore 35 HP and deal 35 stagger damage to self";
    }

    // [Combat Start] Halve the amount of status ailments on target (Rounded up)
    public class DiceCardSelfAbility_AftermathHelpingHand : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();

            if (card.target != null)
            {
                foreach (BattleUnitBuf battleUnitBuf in card.target.bufListDetail.GetActivatedBufList())
                {
                    if (battleUnitBuf.positiveType == BufPositiveType.Negative && battleUnitBuf.stack >= 2)
                    {
                        battleUnitBuf.stack = (battleUnitBuf.stack + 1) / 2;
                    }
                }
            }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Benito_Only_Keyword",
                            "bstart_Keyword"
                };
            }
        }

        public static string Desc = "[Combat Start] Halve the amount of status ailments on target (Rounded up)";
    }

    // [Combat Start] All of target's allies gain 1 Strength and 1 Endurance this Scene, restore 1 Light and 5 HP, and lose 6 stacks of random status ailments.
    public class DiceCardSelfAbility_AftermathCamaraderie : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            if (card.target != null)
            {
                foreach (BattleUnitModel ally in BattleObjectManager.instance.GetAliveList(card.target.faction))
                {
                    int stacks = 6;
                    int decrement;

                    foreach (BattleUnitBuf bruh in ally.bufListDetail.GetActivatedBufList())
                    {
                        if (bruh.positiveType == BufPositiveType.Negative && stacks > 0 && bruh.stack >= 1)
                        {
                            decrement = bruh.stack;

                            if (stacks >= decrement)
                            {
                                stacks -= decrement;
                                bruh.Destroy();
                            }

                            else if (stacks < decrement)
                            {
                                bruh.stack -= stacks;
                                stacks = 0;
                            }
                        }

                        if (bruh.stack < 0)
                        {
                            bruh.Destroy();
                        }
                    }

                    ally.RecoverHP(5);
                    ally.cardSlotDetail.RecoverPlayPointByCard(1);
                    ally.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1);
                    ally.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Endurance, 1);
                }
            }
        }


        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                            "Benito_Only_Keyword",
                            "Strength_Keyword",
                            "Endurance_Keyword",
                            "Energy_Keyword",
                            "bstart_Keyword"
                };
            }
        }

        public static string Desc = "[Combat Start] All of target's allies gain 1 Strength and 1 Endurance this Scene, restore 1 Light and 5 HP, and lose 6 stacks of random status ailments";
    }

    // [On Play] Target restores 1 Light and draws 1 page
    public class DiceCardSelfAbility_AftermathRestUpBenitoPoggers : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            base.OnUseInstance(unit, self, targetUnit);
            targetUnit.allyCardDetail.DrawCards(1);
            targetUnit.cardSlotDetail.RecoverPlayPoint(1);
        }

        public static string Desc = "[On Play] Target restores 1 Light and draws 1 page";
    }

    #endregion

    #region - LIU REMNANTS -

    // [On Use] Purge all stacks of Burn from self and target; gain stacks of ‘Inner Flame’ equal to Burn purged this way
    public class DiceCardSelfAbility_AftermathBottleUp : DiceCardSelfAbilityBase
    {
        public override bool IsTargetableAllUnit()
        {
            return true;
        }

        public override void OnUseCard()
        {
            int stacks = 0;
            BattleUnitModel target = card.target;
            if (target != null)
            {
                BattleUnitBuf battleUnitBuf = target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Burn);
                if (battleUnitBuf != null)
                {
                    stacks += battleUnitBuf.stack;
                    target.bufListDetail.RemoveBufAll(KeywordBuf.Burn);
                }
            }

            BattleUnitBuf battleUnitBuf2 = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x.bufType == KeywordBuf.Burn);
            if (battleUnitBuf2 != null)
            {
                stacks += battleUnitBuf2.stack;
                owner.bufListDetail.RemoveBufAll(KeywordBuf.Burn);
            }

            if (stacks > 0)
            {
                BattleUnitBuf battleUnitBuf3 = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_InnerFlame);
                if (battleUnitBuf3 != null)
                {
                    battleUnitBuf3.stack += stacks;
                }
                else
                {
                    owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_InnerFlame() { stack = stacks });
                }
            }
        }
        public override string[] Keywords => new string[] { "HunanOnlyPage_Keyword", "Burn_Keyword" };


        public static string Desc = "This page can target allies [On Use] Purge all stacks of Burn from self and target; gain stacks of ‘Inner Flame’ equal to Burn purged this way";
    }

    // Only one copy of this page can be in hand at once; if the first die in this page wins or loses a clash, purge all stacks of Inner Flame after the clash.
    public class DiceCardSelfAbility_AftermathLetItOut : DiceCardSelfAbilityBase
    {
        public override bool BeforeAddToHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (unit.allyCardDetail.GetHand().Contains(self))
            {
                return false;
            }
            return true;
        }

        public override string[] Keywords => new string[] { "Burn_Keyword", "LetItOutWin", "LetItOutLose" };

        public static string Desc = "Only one copy of this page can be in hand at once";
    }

    // [On Use] Gain 1 Haste next Scene; At Emotion Level 2 or less, gain 1 additional Haste
    public class DiceCardSelfAbility_AftermathHaste2EmotionPlus : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (owner.emotionDetail.EmotionLevel <= 2)
            {
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, owner);
            }
            else
            {
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, owner);
            }
        }

        public override string[] Keywords => new string[] { "Quickness_Keyword", "Burn_Keyword" };

        public static string Desc = "[On Use] Gain 1 Haste next Scene; At Emotion Level 2 or less, gain 1 additional Haste";
    }

    // [Combat Start] Gain 1 Protection this Scene
    // [On Use] At the start of the next Scene, draw all copies of 'Holding the Line' from the deck
    public class DiceCardSelfAbility_AftermathHoldingThatLine : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Protection, 1, owner);
        }

        public override void OnUseCard()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_DrawHoldingLineBuf(card.card));
        }

        public class BattleUnitBuf_DrawHoldingLineBuf : BattleUnitBuf
        {
            public BattleDiceCardModel _except;

            public BattleUnitBuf_DrawHoldingLineBuf(BattleDiceCardModel card)
            {
                _except = card;
            }

            public override void OnRoundEndTheLast()
            {
                _owner.allyCardDetail.DrawCardsAllSpecific(new LorId(AftermathCollectionInitializer.packageId, 60207), _except);
                this.Destroy();
            }
        }

        public override string[] Keywords => new string[] { "DrawCard_Keyword", "Protection_Keyword" };

        public static string Desc = "[Combat Start] Gain 1 Protection this Scene [On Use] At the start of the next Scene, draw all copies of 'Holding the Line' from the deck";
    }

    // [On Use] Restore 3 Light
    public class DiceCardSelfAbility_AftermathEnergy3 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPoint(3);
        }

        public override string[] Keywords => new string[] { "Energy_Keyword", "Burn_Keyword" };

        public static string Desc = "[On Use] Restore 3 Light";
    }

    // [On Use] At the end of the Scene, gain 1 Protection next Scene for every Positive Emotion Point gained this Scene;
    // gain 1 Damage Up next Scene for every Negative Emotion Point gained this Scene
    public class DiceCardSelfAbility_AftermathAdrenalineFueledAssault : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_GiveDmgOrProtSceneEnd());
        }


        public class BattleUnitBuf_GiveDmgOrProtSceneEnd : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                foreach (var point in _owner.emotionDetail.AllEmotionCoins)
                {
                    switch (point.CoinType)
                    {
                        case EmotionCoinType.Negative:
                            _owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, 1, _owner);
                            continue;
                        case EmotionCoinType.Positive:
                            _owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Protection, 1, _owner);
                            continue;
                        default:
                            continue;
                    }
                }
                this.Destroy();
            }
        }

        public override string[] Keywords => new string[] { "Protection_Keyword", "Energy_Keyword", "Burn_Keyword", "LiuRemnantsOnlyPage_Keyword" };

        public static string Desc = "[On Use] At the end of the Scene, gain 1 Protection next Scene for every Positive Emotion Point gained this Scene; gain 1 Damage Up next Scene for every Negative Emotion Point gained this Scene";
    }

    // Can only be used at Emotion Level 3 and above [On Use] Reduce Emotion Level by 1; Draw 3 pages
    public class DiceCardSelfAbility_AftermathEmotionCrashItDoGoDown : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(3);
            owner.emotionDetail.SetEmotionLevel(owner.emotionDetail.EmotionLevel - 1);
        }

        public override string[] Keywords => new string[] { "DrawCard_Keyword", "LiuRemnantsOnlyPage_Keyword" };

        public static string Desc = "Can only be used at Emotion Level 3 and above [On Use] Reduce Emotion Level by 1; Draw 3 pages";
    }

    // Only usable at Emotion Level 3 or above;
    // this page's cost is equal to the user's current Light at the start of the Scene
    // [On Use] Set Emotion Level to 0, this page gains Power equal to the amount of Emotion Levels lost;
    // gain bonuses depending on the amount of Emotion Level lost
    public class DiceCardSelfAbility_AftermathLiuOnward : DiceCardSelfAbilityBase
    {
        public override int GetCostLast(BattleUnitModel unit, BattleDiceCardModel self, int oldCost)
        {
            return unit.cardSlotDetail.PlayPoint;
        }

        public override void OnUseCard()
        {
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus() { power = owner.emotionDetail.EmotionLevel });
            switch (owner.emotionDetail.EmotionLevel)
            {
                case 3:
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 2, owner);
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 2, owner);
                    break;

                case 4:
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 3, owner);
                    owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, owner);
                    owner.allyCardDetail.DrawCards(1);
                    break;

                default:
                    if (owner.emotionDetail.EmotionLevel >= 5)
                    {
                        owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 2, owner);
                        owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 2, owner);
                        var passive = owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Aftermath_Aftermathstarvation);
                        if (passive != null)
                        {
                            owner.passiveDetail.DestroyPassive(passive);
                            owner.passiveDetail.AddPassive(new LorId(AftermathCollectionInitializer.packageId, 60209));
                        }
                    }
                    break;
            }
            owner.emotionDetail.SetEmotionLevel(0);
        }


        public override string[] Keywords => new string[] { "LiuRemnantsOnlyPage_Keyword", "OnwardEffect1", "OnwardEffect2", "OnwardEffect3", "Burn_Keyword" };

        public static string Desc = "Only usable at Emotion Level 3 or above; The Cost of this page equals the user's current Light at the start of the Scene [On Use] Set Emotion Level to 0; dice on this page gain Power equal to the amount of Emotion levels lost; Gains additional effects depending on the amount of Emotion Levels lost";
    }

    // [Combat Start] All of this character's dice gain '[On Clash Lose] Inflict 1 Burn to each other' this Scene
    public class DiceCardSelfAbility_AftermathMutuallyAssuredIgnitionHunan : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_OnClashLoseInflictBurn());
        }

        public override string[] Keywords => new string[] { "HunanBruceOnlyPage_Keyword", "Burn_Keyword" };

        public static string Desc = "[Combat Start] All of this character's dice gain '[On Clash Lose] Inflict 1 Burn to each other' this Scene";
    }

    // just keyword correction for yijun exclusives
    public class DiceCardSelfAbility_AftermathYijunOnlyPageKeyword : DiceCardSelfAbilityBase
    {
        public override string[] Keywords => new string[] { "YijunOnlyPage_Keyword", "Burn_Keyword" };
    }

    // just keyword correction for yijun exclusives
    public class DiceCardSelfAbility_AftermathYijunRSHopeKeyword : DiceCardSelfAbilityBase
    {
        public override string[] Keywords => new string[] { "YijunOnlyPage_Keyword", "Burn_Keyword" };

        public static string Desc = "Cannot be redirected. This page is used against all enemies.";
    }

    // just keyword for cheaper at Emotion Level 3 or higher
    public class DiceCardSelfAbility_AftermathKeywordForBrokenFlow : DiceCardSelfAbilityBase
    {
        public override int GetCostAdder(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (unit.emotionDetail.EmotionLevel >= 3)
            {
                return -1;
            }
            return 0;
        }
        public override string[] Keywords => new string[] { "Burn_Keyword" };

        public static string Desc = "Costs 1 less at Emotion Level 3 or higher";
    }

    // [On Use] Restore 4 Light and draw 4 pages; fully recover Stagger Resist. For the remainder of the Act,
    // all offensive dice gain '[On Hit] Inflict 1 Burn', and all defensive dice gain ‘[On Clash Win] Inflict 1 Burn’
    public class DiceCardSelfAbility_AftermathRiseFromTheAshesYijunPassive : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPoint(4);
            owner.allyCardDetail.DrawCards(4);
            owner.breakDetail.ResetBreakDefault();
            BattleUnitBuf buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_MetalGearRisingFromTheAshes);
            if (buf == null)
                owner.bufListDetail.AddBuf(new BattleUnitBuf_MetalGearRisingFromTheAshes());
            else
                buf.stack++;
        }

        public override string[] Keywords => new string[] { "Energy_Keyword", "DrawCard_Keyword", "MetalGearRisingFromTheAshesKeyword" };

        public static string Desc = "[On Use] Restore 4 Light and draw 4 pages; fully recover Stagger Resist. For the remainder of the Act, all offensive dice gain '[On Hit] Inflict 1 Burn', and all defensive dice gain ‘[On Clash Win] Inflict 1 Burn’";
    }

    // This page's Cost is reduced by 1 for every 3 stacks of Burn on self\n[Combat Start] Give Burn Protection to all allies equal to half of user's Burn Protection
    public class DiceCardSelfAbility_AftermathBruceRepentance : DiceCardSelfAbilityBase
    {
        public override int GetCostAdder(BattleUnitModel unit, BattleDiceCardModel self)
        {
            var burn = unit.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
            if (burn != null)
            {
                return -(burn.stack / 3);
            }
            return 0;
        }

        public override void OnStartBattle()
        {
            base.OnStartBattle();
            var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_AftermathBurnProtection);
            if (buf != null)
            {
                foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction).SkipWhile(x => x == owner))
                {
                    var buf3 = unit.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_AftermathBurnProtection);
                    if (buf3 == null) unit.bufListDetail.AddBuf(new BattleUnitBuf_AftermathBurnProtection { stack = buf.stack / 2 });
                    else
                    {
                        buf3.stack += buf.stack / 2;
                        buf3.Init(unit);
                    };
                }
            }
        }



        public override string[] Keywords => new string[] { "BruceOnlyPage_Keyword", "Burn_Keyword", "Aftermath_BurnProtection" };

        public static string Desc = "This page's Cost is reduced by 1 for every 3 stacks of Burn on self\n[Combat Start] Give Burn Protection to all allies equal to half of user's Burn Protection";
    }

    #endregion

    #region - COLOR CHUN -

    // just keyword for cheaper at Emotion Level 3 or higher
    public class DiceCardSelfAbility_ColorChunOnlyPageKeyword : DiceCardSelfAbilityBase
    {
        public override int GetCostAdder(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (unit.emotionDetail.EmotionLevel >= 3)
            {
                return -1;
            }
            return 0;
        }
        public override string[] Keywords => new string[] { "ColorChunOnlyPage_Keyword", "Burn_Keyword" };
    }

    // [On Use] Purge all Burn on target;
    // offensive dice on this page deal bonus stagger damage equivalent to half of the amount of Burn purged this way
    public class DiceCardSelfAbility_ColorChunStaggerBurn : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            var target = card.target;
            if (target != null)
            {
                int cock = target.bufListDetail.GetKewordBufAllStack(KeywordBuf.Burn);
                cock /= 2;
                if (cock > 0)
                {
                    card.ApplyDiceStatBonus(DiceMatch.AllAttackDice, new DiceStatBonus { breakDmg = cock });
                }
            }
        }

        public override string[] Keywords => new string[] { "ColorChunOnlyPage_Keyword", "Burn_Keyword" };

        public static string Desc = "[On Use] Offensive dice on this page deal bonus stagger damage equivalent to half of the amount of Burn on target";
    }

    // [On Use] Add a copy of 'Flaming Blitz' to hand
    public class DiceCardSelfAbility_ColorChunLightTheFlames : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_AftermathAddAnotherBurnStackPls());
            owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 101));
        }

        public class BattleUnitBuf_AftermathAddAnotherBurnStackPls : BattleUnitBuf
        {
            public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
            {
                if (cardBuf is BattleUnitBuf_burn) return 1;
                return base.OnGiveKeywordBufByCard(cardBuf, stack, target);
            }
        }


        public override string[] Keywords => new string[] { "ColorChunOnlyPage_Keyword", "Burn_Keyword", "DrawCard_Keyword" };

        public static string Desc = "[Combat Start] When inflicting Burn using Combat Pages this Scene, inflict 1 additional stacks; add a copy of 'Flaming Blitz' to hand";
    }

    // This page exhausts when discarded or used 
    // This page's Cost is lowered by the number of other 'Flaming Blitz' in hand
    // [Combat Start] Discard up to 2 other copies of ‘Flaming Blitz’ and add their dice to this page
    public class DiceCardSelfAbility_ColorChunFunnyPage : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            card.card.exhaust = true;
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
        }

        public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            base.OnRoundStart_inHand(unit, self);
            self.AddCost(-(unit.allyCardDetail.GetHand().FindAll(x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 101) && x != self).Count));
        }

        public override void OnStartBattleAfterCreateBehaviour()
        {
            List<BattleDiceCardModel> list = owner.allyCardDetail.GetHand().FindAll(x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 101));
            int loop = 0;

            if (list.Count > 0)
            {
                foreach (BattleDiceCardModel combatPage in list)
                {
                    if (combatPage != null)
                    {
                        foreach (var funnyDie in combatPage.CreateDiceCardBehaviorList())
                        {
                            if (funnyDie != null && loop < 2)
                            {
                                card.AddDice(funnyDie);
                                owner.allyCardDetail.DiscardACardByAbility(combatPage);
                                loop++;
                            }
                        }
                    }
                }
            }
        }

        public override string[] Keywords => new string[] { "ColorChunOnlyPage_Keyword", "Burn_Keyword" };

        public static string Desc = "This page exhausts when discarded or used\nThis page's Cost is lowered by the number of other 'Flaming Blitz' in hand\n[Combat Start] Discard up to 2 other copies of ‘Flaming Blitz’ and add their dice to this page";
    }

    // If 3 or more Burn was inflicted with this page, reduce the Cost of all 'Frontal Onslaughts' by 1 [On Use] Draw 1 page
    public class DiceCardSelfAbility_ColorChunOnslaught : DiceCardSelfAbilityBase
    {
        int burn = 0;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            var target = card.target;
            if (target != null)
            {
                var buf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                if (buf != null) burn = buf.stack;
            }
        }

        public override void OnEndBattle()
        {
            var target = card.target;
            if (target != null)
            {
                var buf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                if (buf != null)
                {
                    if (buf.stack >= (burn + 3))
                    {
                        var greg = owner.allyCardDetail.GetAllDeck().FindAll(x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 104));
                        foreach (var card in greg)
                        {
                            card.AddCost(-1);
                        }
                    }
                }
            }
        }

        public override string[] Keywords => new string[]
        {
                "ColorChunOnlyPage_Keyword",
                "Burn_Keyword",
                "DrawCard_Keyword"
        };

        public static string Desc = "If 3 or more Burn was inflicted with this page, reduce the Cost of all 'Frontal Onslaughts' by 1 [On Use] Draw 1 page";
    }

    // Dice on this page deal no physical or stagger damage
    public class DiceCardSelfAbility_ColorChunEnormous : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            card.ApplyDiceStatBonus(DiceMatch.AllAttackDice, new DiceStatBonus { dmg = -999999, breakDmg = -999999 });
        }

        public override string[] Keywords => new string[]
        {
                "ColorChunOnlyPage_Keyword",
                "Burn_Keyword"
        };

        public static string Desc = "Dice on this page deal no physical or stagger damage";
    }

    // 
    public class DiceCardSelfAbility_ColorChunInnerFlames : DiceCardSelfAbilityBase
    {
        int stacks = 0;

        public override void OnStartBattle()
        {
            var target = card.target;
            if (target != null)
            {
                var buf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
                if (buf != null)
                {
                    stacks = buf.stack;
                }
            }
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            stacks /= 6;
            if (stacks >= 1)
            {
                card.ApplyDiceAbility(DiceMatch.NextAttackDice, new DiceCardAbility_AftermathhiddenEconomyOne(stacks));
                card.ApplyDiceAbility(DiceMatch.NextDefenseDice, new DiceCardAbility_AftermathhiddenEconomyTwo(stacks));
            }
        }

        public static string Desc = "";

        public override string[] Keywords => new string[]
        {
                "ColorChunOnlyPage_Keyword",
                "Burn_Keyword",
                "DrawCard_Keyword",
                "Energy_Keyword"
        };

        public class DiceCardAbility_AftermathhiddenEconomyOne : DiceCardAbilityBase
        {
            int stonks;

            public DiceCardAbility_AftermathhiddenEconomyOne(int stacks)
            {
                stonks = stacks;
            }

            public override void OnRollDice()
            {
                if (behavior.IsParrying()) owner.cardSlotDetail.RecoverPlayPoint(stonks);
            }
        }
        public class DiceCardAbility_AftermathhiddenEconomyTwo : DiceCardAbilityBase
        {
            int stonks;

            public DiceCardAbility_AftermathhiddenEconomyTwo(int stacks)
            {
                stonks = stacks;
            }

            public override void OnWinParrying()
            {
                base.OnWinParrying();
                owner.allyCardDetail.DrawCards(stonks);
            }

            public override void OnDrawParrying()
            {
                base.OnDrawParrying();
                owner.allyCardDetail.DrawCards(stonks);
            }

            public override void OnLoseParrying()
            {
                var h = behavior.TargetDice;
                if (h != null)
                {
                    if (!IsAttackDice(h.Detail)) owner.allyCardDetail.DrawCards(stonks);
                }
            }

        }
    }

    #endregion

    #region - RETURN OF THE INDEX -

    [UnusedAbility]
    public class DiceCardSelfAbility_Aftermath_DrawandSingletonHaste : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Draw 1 page. If Singleton, gain 1 Haste next Scene";

        public override string[] Keywords => new string[] { "DrawCard_Keyword", "OnlyOne_Keyword", "Quickness_Keyword" };

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            if (owner.allyCardDetail.IsHighlander())
            {
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, owner);
            }
        }
    }

    [UnusedAbility]
    public class DiceCardSelfAbility_Aftermath_LightandSingletonHaste : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Restore 1 Light. If Singleton, gain 1 Haste next Scene";

        public override void OnUseCard()
        {
            base.owner.cardSlotDetail.RecoverPlayPoint(1);
            if (base.owner.allyCardDetail.IsHighlander())
            {
                base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
            }
        }

        public override string[] Keywords => new string[] { "Energy_Keyword", "OnlyOne_Keyword", "Quickness_Keyword" };
    }

    public class DiceCardSelfAbility_Aftermath_RestoreLightByHaste : DiceCardSelfAbilityBase
    {
        public static string Desc = "[After Use] If Singleton, restore 1 Light for every 2 Haste on self (Max. 2)";

        public override void OnEndBattle()
        {
            base.OnEndBattle();
            if (owner.allyCardDetail.IsHighlander())
            {
                int buf = owner.bufListDetail.GetKewordBufStack(KeywordBuf.Quickness);
                for (int i = 0; i < buf / 2 && i < 2; i++)
                {
                    owner.cardSlotDetail.RecoverPlayPoint(1);
                }  
            }
        }

        public override string[] Keywords => new string[] { "Energy_Keyword", "Quickness_Keyword" };
    }

    public class DiceCardSelfAbility_Aftermath_OvertakeOphelia : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, owner);
            if (card.speedDiceResultValue >= 8)
            {
                owner.cardSlotDetail.RecoverPlayPoint(1);
                owner.allyCardDetail.DrawCards(1);
            }
        }

        public static string Desc = "[On Use] Gain 1 Haste next Scene; If Speed is 8 or higher, restore 1 Light and draw 1 page";

        public override string[] Keywords => new string[] { "Energy_Keyword", "DrawCard_Keyword" , "Quickness_Keyword" };
    }

    public class DiceCardSelfAbility_Aftermath_CircumscriptionOphelia : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Gain 1 Haste next scene; if Singleton, gain 1 additional Haste next scene; if Speed is 8 or higher, all dice on this page gain +5 Power";

        public override string[] Keywords => new string[] { "OnlyOne_Keyword", "Quickness_Keyword" };

        public override void OnUseCard()
        {
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
            if (base.owner.allyCardDetail.IsHighlander())
            {
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
            }
            if (card.speedDiceResultValue >= 8)
            {
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus { power = 5 });
            }
        }
    }

    public class DiceCardSelfAbility_Aftermath_EviscerateOphelia : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            if (card.speedDiceResultValue >= 8)
            {
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus { power = 2 });
            }
        }

        public static string Desc = "[On Use] If Speed is 8 or higher, all dice on this page gain +2 Power";
    }

    public class DiceCardSelfAbility_Aftermath_LevigationOphelia : DiceCardSelfAbilityBase
    {
        public static string Desc = "Only usable in the 'Blade Unlocked' state\nIf Speed is lower than 8, destroy the first die on this page";

        public override string[] Keywords => new string[] { "onlypage_Ophe_Keyword", "Quickness_Keyword" };

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null;
        }

        public override void OnStartBattleAfterCreateBehaviour()
        {
            if (card.speedDiceResultValue < 8) card.DestroyDice(DiceMatch.DiceByIdx(0));
        }
    }

    [UnusedAbility]
    public class DiceCardSelfAbility_Aftermath_SingletonHaste : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] If Singleton, gain 1 Haste next Scene";

        public override string[] Keywords => new string[] { "OnlyOne_Keyword", "Quickness_Keyword" };

        public override void OnUseCard()
        {
            if (base.owner.allyCardDetail.IsHighlander())
            {
                base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
            }
        }
    }

    public class DiceCardSelfAbility_Aftermath_SingletonLightRestoreOnHaste : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] If Singleton, restore 1 Light for every 2 Haste on self (Max. 2)";

        public override string[] Keywords => new string[] { "Energy_Keyword", "OnlyOne_Keyword", "Quickness_Keyword" };


        public override void OnUseCard()
        {
            if (base.owner.allyCardDetail.IsHighlander())
            {
                BattleUnitBuf battleUnitBuf_quickness = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
                if (battleUnitBuf_quickness != null)
                    base.owner.cardSlotDetail.RecoverPlayPoint(Mathf.Min(battleUnitBuf_quickness.stack / 2, 2));
            }
        }
    }

    [UnusedAbility]
    public class DiceCardSelfAbility_Aftermath_opheliaCard : DiceCardSelfAbilityBase
    {
        public static string Desc = "Only usable in the 'Blade Unlocked' state";

        public override string[] Keywords => new string[] { "onlypage_Ophe_Keyword", "Quickness_Keyword" };

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null;
        }
    }

    public class DiceCardSelfAbility_Aftermath_OpheHasteOnPlay : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            base.OnUseInstance(unit, self, targetUnit);
            LorId[] ids = new LorId[] { };
            foreach (var car in unit.allyCardDetail.GetAllDeck())
            {
                if (!ids.Contains(car.GetID())) ids.Append(car.GetID());
            }
            unit.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, Mathf.Min((ids.Count() + 1) / 2, 7), unit);
        }

        public override string[] Keywords => new string[] { "Quickness_Keyword" };

        public static string Desc = "[On Play] Gain 1 Haste next Scene for every 2 unique Combat Pages in deck (Rounded up; up to 7)";
    }

    public class DiceCardSelfAbility_Aftermath_EvanPrescript1 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            if (targetUnit != null && targetUnit.faction == unit.faction)
            {
                List<BattleDiceCardModel> list = targetUnit.allyCardDetail.GetHand().FindAll((BattleDiceCardModel x) => x.GetCost() >= 1);
                if (list.Count > 0)
                {
                    BattleDiceCardModel battleDiceCardModel = RandomUtil.SelectOne<BattleDiceCardModel>(list);
                    battleDiceCardModel.AddBuf(new DiceCardSelfAbility_Aftermath_EvanPrescript1.CostDecreasePermaBuf());
                }
                SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            }
            if (targetUnit.faction != unit.faction)
            {
                List<BattleDiceCardModel> list = unit.allyCardDetail.GetHand().FindAll((BattleDiceCardModel x) => x.GetCost() >= 1);
                if (list.Count > 0)
                {
                    BattleDiceCardModel battleDiceCardModel = RandomUtil.SelectOne<BattleDiceCardModel>(list);
                    battleDiceCardModel.AddBuf(new DiceCardSelfAbility_Aftermath_EvanPrescript1.CostDecreasePermaBuf());
                }
                SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            }
            SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            foreach (int cardId in PassiveAbility_Aftermath_Evan5.targetIds)
            {
                unit.personalEgoDetail.RemoveCard(new LorId(AftermathCollectionInitializer.packageId, cardId));
            }
            //(unit.passiveDetail.PassiveList.Find((PassiveAbilityBase x) => x is PassiveAbility_Evan5) as PassiveAbility_Evan5).GiveSpellCards(unit);
        }

        public override bool IsValidTarget(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            bool result;
            if (targetUnit.faction != unit.faction)
            {
                result = !BattleObjectManager.instance.GetAliveList(unit.faction).Exists((BattleUnitModel x) => x != unit);
            }
            else
            {
                result = (targetUnit != unit);
            }
            return result;
        }

        public override bool IsTargetableAllUnit()
        {
            return true;
        }
        public class CostDecreasePermaBuf : BattleDiceCardBuf
        {
            public override DiceCardBufType bufType
            {
                get
                {
                    return DiceCardBufType.CostDecrease;
                }
            }

            public CostDecreasePermaBuf()
            {
                this._stack = 1;
            }

            public void Add()
            {
                this._stack++;
            }
            public override void OnUseCard(BattleUnitModel owner, BattlePlayingCardDataInUnitModel playingCard)
            {
                base.Destroy();
            }
            public override int GetCost(int oldCost)
            {
                return oldCost - base.Stack;
            }
        }
    }

    public class DiceCardSelfAbility_Aftermath_EvanPrescript2 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            if (targetUnit != null && targetUnit.faction == unit.faction)
            {
                targetUnit.allyCardDetail.DiscardInHand(1);
                targetUnit.allyCardDetail.DrawCards(2);
            }
            if (targetUnit.faction != unit.faction)
            {
                unit.allyCardDetail.DiscardInHand(1);
                unit.allyCardDetail.DrawCards(2);
            }
            SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            foreach (int cardId in PassiveAbility_Aftermath_Evan5.targetIds)
            {
                unit.personalEgoDetail.RemoveCard(new LorId(AftermathCollectionInitializer.packageId, cardId));
            }
            //(unit.passiveDetail.PassiveList.Find((PassiveAbilityBase x) => x is PassiveAbility_Evan5) as PassiveAbility_Evan5).GiveSpellCards(unit);
        }

        public override bool IsValidTarget(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            bool result;
            if (targetUnit.faction != unit.faction)
            {
                result = !BattleObjectManager.instance.GetAliveList(unit.faction).Exists((BattleUnitModel x) => x != unit);
            }
            else
            {
                result = (targetUnit != unit);
            }
            return result;
        }

        public override bool IsTargetableAllUnit()
        {
            return true;
        }

        public override string[] Keywords => new string[] { "DrawCard_Keyword" };
    }

    public class DiceCardSelfAbility_Aftermath_EvanPrescript3 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            if (targetUnit != null && targetUnit.faction == unit.faction)
            {
                int a = (int)((float)targetUnit.MaxHp * 0.10f);
                int b = (int)((float)targetUnit.breakDetail.breakGauge * 0.10f);
                if (a > 8) a = 8;
                if (b > 8) b = 8;
                targetUnit.RecoverHP(a);
                targetUnit.breakDetail.RecoverBreak(b);
                SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            }
            if (targetUnit.faction != unit.faction)
            {
                int a = (int)((float)unit.MaxHp * 0.10f);
                int b = (int)((float)unit.breakDetail.breakGauge * 0.10f);
                if (a > 8) a = 8;
                if (b > 8) b = 8;
                unit.RecoverHP(a);
                unit.breakDetail.RecoverBreak(b);
                SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            }
            SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            foreach (int cardId in PassiveAbility_Aftermath_Evan5.targetIds)
            {
                unit.personalEgoDetail.RemoveCard(new LorId(AftermathCollectionInitializer.packageId, cardId));
            }
            //(unit.passiveDetail.PassiveList.Find((PassiveAbilityBase x) => x is PassiveAbility_Evan5) as PassiveAbility_Evan5).GiveSpellCards(unit);
        }

        public override bool IsValidTarget(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            bool result;
            if (targetUnit.faction != unit.faction)
            {
                result = !BattleObjectManager.instance.GetAliveList(unit.faction).Exists((BattleUnitModel x) => x != unit);
            }
            else
            {
                result = (targetUnit != unit);
            }
            return result;
        }

        public override bool IsTargetableAllUnit()
        {
            return true;
        }
    }

    public class DiceCardSelfAbility_Aftermath_EvanPrescript4 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            if (targetUnit != null && targetUnit.faction == unit.faction)
            {
                targetUnit.bufListDetail.AddBuf(new BattleUnitBuf_EvanPrescriptBuf4());
                SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            }
            if (targetUnit.faction != unit.faction)
            {
                unit.bufListDetail.AddBuf(new BattleUnitBuf_EvanPrescriptBuf4());
                SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            }
            SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            foreach (int cardId in PassiveAbility_Aftermath_Evan5.targetIds)
            {
                unit.personalEgoDetail.RemoveCard(new LorId(AftermathCollectionInitializer.packageId, cardId));
            }
            //(unit.passiveDetail.PassiveList.Find((PassiveAbilityBase x) => x is PassiveAbility_Evan5) as PassiveAbility_Evan5).GiveSpellCards(unit);
        }

        public override bool IsValidTarget(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            bool result;
            if (targetUnit.faction != unit.faction)
            {
                result = !BattleObjectManager.instance.GetAliveList(unit.faction).Exists((BattleUnitModel x) => x != unit);
            }
            else
            {
                result = (targetUnit != unit);
            }
            return result;
        }

        public override bool IsTargetableAllUnit()
        {
            return true;
        }
        public class BattleUnitBuf_EvanPrescriptBuf4 : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                this.Destroy();
            }
            public override void BeforeRollDice(BattleDiceBehavior behavior)
            {
                if (base.IsAttackDice(behavior.Detail))
                {
                    if (behavior.abilityList.Find((DiceCardAbilityBase x) => x is DiceCardAbility_Messenger5BufCard) == null)
                    {
                        behavior.AddAbility(new DiceCardAbility_Messenger5BufCard());
                    }
                }
            }

            public class DiceCardAbility_Messenger5BufCard : DiceCardAbilityBase
            {
                public override void OnRollDice()
                {
                    if (this.behavior.DiceVanillaValue == this.behavior.GetDiceMin() || this.behavior.DiceVanillaValue == this.behavior.GetDiceMax())
                    {
                        this.behavior.ApplyDiceStatBonus(new DiceStatBonus
                        {
                            power = 2,
                            min = 1,
                            max = -1
                        });
                    }
                }
            }
        }
    }

    public class DiceCardSelfAbility_Aftermath_Evan1 : DiceCardSelfAbilityBase
    {
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                "IndexReleaseCard3_Keyword",
                "DrawCard_Keyword",
                "Energy_Keyword"
                };
            }
        }
        public override void OnUseCard()
        {
            this.owner.allyCardDetail.DrawCards(1);
            base.owner.allyCardDetail.AddNewCard(605010, false);
            this.card.card.exhaust = true;
        }
    }

    public class DiceCardSelfAbility_Aftermath_Evan2 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            int sus = (int)((float)this.owner.hp * 0.07f);
            this.owner.TakeDamage(sus);
            this.owner.cardSlotDetail.RecoverPlayPoint(3);
            this.owner.allyCardDetail.DrawCards(1);
        }
    }

    public class DiceCardSelfAbility_Aftermath_Evan3 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            if (base.owner.allyCardDetail.IsHighlander())
            {
                base.owner.bufListDetail.AddBuf(new DiceCardSelfAbility_Aftermath_Evan3.BattleUnitBuf_buf2());
            }
            else
            {
                base.owner.bufListDetail.AddBuf(new DiceCardSelfAbility_Aftermath_Evan3.BattleUnitBuf_buf1());
            }
        }

        public class BattleUnitBuf_buf1 : BattleUnitBuf
        {
            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                this._owner.RecoverHP(2);
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }
        public class BattleUnitBuf_buf2 : BattleUnitBuf
        {
            public override void OnSuccessAttack(BattleDiceBehavior behavior)
            {
                this._owner.RecoverHP(3);
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }

        public override string[] Keywords => new string[] { "OnlyOne_Keyword" };
    }

    public class DiceCardSelfAbility_Aftermath_Evan4 : DiceCardSelfAbilityBase
    {
        public override string[] Keywords => new string[] { "OnlyOne_Keyword" };

        public override void OnUseCard()
        {
            this.owner.allyCardDetail.DrawCards(1);
            if (base.owner.allyCardDetail.IsHighlander())
            {
                List<BattleDiceCardModel> list = base.owner.allyCardDetail.GetHand().FindAll((BattleDiceCardModel x) => x.GetCost() >= 1);
                if (list.Count > 0)
                {
                    BattleDiceCardModel battleDiceCardModel = RandomUtil.SelectOne<BattleDiceCardModel>(list);
                    battleDiceCardModel.AddBuf(new DiceCardSelfAbility_Aftermath_Evan4.CostDecreasePermaBuf());

                }
            }
        }

        public class CostDecreasePermaBuf : BattleDiceCardBuf
        {
            public override DiceCardBufType bufType
            {
                get
                {
                    return DiceCardBufType.CostDecrease;
                }
            }

            public CostDecreasePermaBuf()
            {
                this._stack = 1;
            }

            public void Add()
            {
                this._stack++;
            }
            public override void OnUseCard(BattleUnitModel owner, BattlePlayingCardDataInUnitModel playingCard)
            {
                base.Destroy();
            }
            public override int GetCost(int oldCost)
            {
                return oldCost - base.Stack;
            }
        }
    }

    public class DiceCardSelfAbility_Aftermath_Evan5 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel = this.card.target.cardSlotDetail.cardAry[this.card.targetSlotOrder];
            if (battlePlayingCardDataInUnitModel != null)
                if (battlePlayingCardDataInUnitModel.card.GetSpec().Ranged != CardRange.FarArea && battlePlayingCardDataInUnitModel.card.GetSpec().Ranged != CardRange.FarAreaEach)
                {
                    battlePlayingCardDataInUnitModel.target = base.owner;
                    battlePlayingCardDataInUnitModel.targetSlotOrder = this.card.slotOrder;
                }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Aftermath_EvanEnmity_Keyword"
                };
            }
        }
        public override void OnUseCard()
        {
            this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                power = Mathf.Min(3, Mathf.RoundToInt(((float)base.owner.MaxHp - base.owner.hp) / 7f))
            });
        }
    }

    public class DiceCardSelfAbility_Aftermath_Evan6 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, Mathf.Min(3, Mathf.RoundToInt(((float)base.owner.MaxHp - base.owner.hp) / 15f)), this.owner);
        }

        public override string[] Keywords => new string[] { "Strength_Keyword" };
    }

    public class DiceCardSelfAbility_Aftermath_Evan7 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel = this.card.target.cardSlotDetail.cardAry[this.card.targetSlotOrder];
            if (battlePlayingCardDataInUnitModel != null)
                if (battlePlayingCardDataInUnitModel.card.GetSpec().Ranged != CardRange.FarArea && battlePlayingCardDataInUnitModel.card.GetSpec().Ranged != CardRange.FarAreaEach)
                {
                    battlePlayingCardDataInUnitModel.target = base.owner;
                    battlePlayingCardDataInUnitModel.targetSlotOrder = this.card.slotOrder;
                }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Aftermath_EvanEnmity_Keyword"
                };
            }
        }
        public override void OnUseCard()
        {
            this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                dmgRate = Mathf.Min(50, Mathf.RoundToInt(((float)this.owner.MaxHp - this.owner.hp) / 7f * 10f))
            });
        }
    }

    public class DiceCardSelfAbility_Aftermath_Evan8 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel = this.card.target.cardSlotDetail.cardAry[this.card.targetSlotOrder];
            if (battlePlayingCardDataInUnitModel != null)
                if (battlePlayingCardDataInUnitModel.card.GetSpec().Ranged != CardRange.FarArea && battlePlayingCardDataInUnitModel.card.GetSpec().Ranged != CardRange.FarAreaEach)
                {
                    battlePlayingCardDataInUnitModel.target = base.owner;
                    battlePlayingCardDataInUnitModel.targetSlotOrder = this.card.slotOrder;
                }
        }

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Aftermath_EvanEnmity_Keyword"
                };
            }
        }
        public override void OnUseCard()
        {
            this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                dmgRate = Mathf.Min(50, Mathf.RoundToInt(((float)this.owner.MaxHp - this.owner.hp) / 7f * 10f))
            });
        }
    }

    public class DiceCardSelfAbility_Aftermath_Evan9 : DiceCardSelfAbilityBase
    {
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Aftermath_onlypage_evan_Keyword",
                };
            }
        }
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null;
        }
        public override void OnUseCard()
        {
            this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                power = Mathf.Min(5, Mathf.RoundToInt(((float)base.owner.MaxHp - base.owner.hp) / 10f)),
            });
        }
    }

    public class DiceCardSelfAbility_Aftermath_EvanSpecial : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel = this.card.target.cardSlotDetail.cardAry[this.card.targetSlotOrder];
            if (battlePlayingCardDataInUnitModel != null)
                if (battlePlayingCardDataInUnitModel.card.GetSpec().Ranged != CardRange.FarArea && battlePlayingCardDataInUnitModel.card.GetSpec().Ranged != CardRange.FarAreaEach)
                {
                    battlePlayingCardDataInUnitModel.target = base.owner;
                    battlePlayingCardDataInUnitModel.targetSlotOrder = this.card.slotOrder;
                }
        }
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null;
        }
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Aftermath_onlypage_evan_Keyword",
                    "Aftermath_EvanEnmity_Keyword"
                };
            }
        }
    }

    public class DiceCardSelfAbility_Aftermath_EvanGuard : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return BattleObjectManager.instance.GetAliveList(this.owner.faction).Count > 1;
        }
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            if (targetUnit != null)
            {
                unit.bufListDetail.AddReadyBuf(new DiceCardSelfAbility_Aftermath_EvanGuard.BattleUnitBuf_EvanSelfBuf());
                foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(false))
                {
                    battleUnitModel.bufListDetail.AddReadyBuf(new DiceCardSelfAbility_Aftermath_EvanGuard.BattleUnitBuf_EvanTarget());
                }
                SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
            }
        }

        public class BattleUnitBuf_EvanSelfBuf : BattleUnitBuf
        {
            public override string keywordId
            {
                get
                {
                    return "Aftermath_EvanProvoke";
                }
            }

            public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
            {
                return 0.65f;
            }

            public override float BreakDmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
            {
                return 0.65f;
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }

        public class BattleUnitBuf_EvanTarget : BattleUnitBuf
        {
            public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
            {
                return BattleObjectManager.instance.GetAliveList(false).Find((BattleUnitModel x) => x.bufListDetail.HasBuf<DiceCardSelfAbility_Aftermath_EvanGuard.BattleUnitBuf_EvanSelfBuf>());
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }
    }

    public class DiceCardSelfAbility_Aftermath_ElisThePrescriptCommandsIt : DiceCardSelfAbilityBase
    {
        public static string Desc = "[Combat Start] If Singleton, give 2 Type Dice Power-Up to all Singleton allies (The type is determined by the current Offensive type boosted by Grace of the Prescript)";

        public override string[] Keywords => new string[] { "OnlyOne_Keyword", "onlypage_Elis_Keyword" };

        private KeywordBuf bufType = KeywordBuf.SlashPowerUp;

        public override void OnStartBattle()
        {
            base.OnStartBattle();          
            if (owner.allyCardDetail.IsHighlander())
            {
                var passive = owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_240018) as PassiveAbility_240018;
                if (passive != null)
                {
                    switch (passive.targetBehaviour)
                    {
                        case BehaviourDetail.Slash:
                            bufType = KeywordBuf.SlashPowerUp;
                            break;
                        case BehaviourDetail.Penetrate:
                            bufType = KeywordBuf.PenetratePowerUp;
                            break;
                        case BehaviourDetail.Hit:
                            bufType = KeywordBuf.HitPowerUp;
                            break;
                    }
                    foreach (var ally in BattleObjectManager.instance.GetAliveList(owner.faction).FindAll(x => x != owner && x.allyCardDetail.IsHighlander()))
                    {
                        var buf = ally.bufListDetail.GetActivatedBuf(bufType);
                        if (buf != null) buf.stack += 2;
                        else ally.bufListDetail.AddKeywordBufThisRoundByEtc(bufType, 2, owner);
                    }
                }
            }
        }
    }


    #endregion

    #region - NORINCO WORKSHOP / C.B.L. I -

    public class DiceCardSelfAbility_Aftermath_ExhaustIfNoDice : DiceCardSelfAbilityBase
    {
        public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            base.OnRoundStart_inHand(unit, self);
            if (self.XmlData.DiceBehaviourList.Count <= 0)
            {
                unit.allyCardDetail.ExhaustACard(self);
                self.exhaust = true;
            }
        }

        public static string Desc = "If there are no dice left on this page, exhaust it at the start of the Scene";
    }

    public class DiceCardSelfAbility_Aftermath_NoDmgStaggerSelf : DiceCardSelfAbilityBase
    {
        public override void OnStartBattleAfterCreateBehaviour()
        {
            base.OnStartBattleAfterCreateBehaviour();
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus { dmg = -99999 });
            owner.bufListDetail.AddBuf(new BattleUnitBuf_StaggerSelfSceneEnd());
        }


        private class BattleUnitBuf_StaggerSelfSceneEnd : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                base.OnRoundEnd();
                _owner.breakDetail.TakeBreakDamage(_owner.breakDetail.GetDefaultBreakGauge());
                this.Destroy();
            }
        }

        public static string Desc = "Dice on this page deal no physical damage [On Use] Stagger self at the end of the Scene";
    }

    public class DiceCardSelfAbility_Aftermath_ChemHomeRun : DiceCardSelfAbilityBase
    {
        public override bool BeforeAddToHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (unit.allyCardDetail.GetHand().Find(x => x.GetID() == self.GetID()) != null)
            {
                return false;
            }
            return true;
        }

        public override void OnUseCard()
        {
            this.Activate(owner, card.card);
            base.OnUseCard();
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            this.Activate(unit, self);
            base.OnDiscard(unit, self);
        }

        public void Activate(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
            unit.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 2, unit);
            unit.bufListDetail.AddBuf(new BattleUnitBuf_ChemHomeRunSTAGGERYOURSELF());
        }

        public class BattleUnitBuf_ChemHomeRunSTAGGERYOURSELF : BattleUnitBuf
        {
            public override void OnRoundEnd()
            {
                _owner.breakDetail.TakeBreakDamage(_owner.breakDetail.breakGauge, DamageType.Card_Ability);
                this.Destroy();
            }
        }

        public override string[] Keywords => new string[] { "Strength_Keyword" };

        public static string Desc = "Only one copy of this page can be held at a time\nExhausts when used or discarded; [On Exhaust] Gain 2 Strength; become Staggered at the end of the Scene";
    }

    public class DiceCardSelfAbility_Aftermath_ChemRebound : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.Activate(owner, card.card);
            base.OnUseCard();
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            this.Activate(unit, self);
            base.OnDiscard(unit, self);
        }

        public void Activate(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
            var buf = unit.bufListDetail.GetReadyBufList().Find(x => x is BattleUnitBuf_Aftermath_ChemRebound);
            if (buf == null) unit.bufListDetail.AddReadyBuf(new BattleUnitBuf_Aftermath_ChemRebound() { stack = 1 });
            else buf.stack++;
        }

        public override string[] Keywords => new string[] { "Aftermath_ChemRebound_Keyword" }; 

        public static string Desc = "Exhausts when used or discarded; [On Exhaust] Gain 1 Rebound next Scene";
    }

    public class DiceCardSelfAbility_Aftermath_ChemErazer : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.Activate(owner, card.card);
            base.OnUseCard();
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            this.Activate(unit, self);
            base.OnDiscard(unit, self);
        }

        public void Activate(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
            unit.breakDetail.RecoverBreak(6);
            unit.TakeDamage(2, DamageType.Card_Ability, unit);
        }

        public static string Desc = "Exhausts when used or discarded; [On Exhaust] Recover 6 Stagger Resist; deal 2 damage to self";
    }

    public class DiceCardSelfAbility_Aftermath_ChemBreeze : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            this.Activate(owner, card.card);
            base.OnUseCard();
        }

        public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
        {
            this.Activate(unit, self);
            base.OnDiscard(unit, self);
        }

        public void Activate(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
            unit.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, unit);
            unit.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 1, unit);
        }


        public override string[] Keywords => new string[] { "Quickness_Keyword", "Paralysis_Keyword" };

        public static string Desc = "Exhausts when used or discarded; [On Exhaust] Gain 2 Haste next Scene; inflict 1 Paralysis to self next Scene";
    }

    public class DiceCardSelfAbility_Aftermath_Spend1ChemDraw1 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_StoredChems);
            if (buf != null)
            {
                if (buf.stack > 0)
                {
                    buf.stack--;
                    ChemsCardModel.AddChemToHand(owner);
                }
                if (buf.stack < 1) buf.Destroy();
            }
        }

        public override string[] Keywords => new string[] {"Aftermath_StoredChems"};

        public static string Desc = "[On Use] Spend 1 Stored Chems to add 1 random Chem to hand";
    }

    public class DiceCardSelfAbility_Aftermath_Spend3ChemDraw2 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_StoredChems);
            if (buf != null)
            {
                if (buf.stack > 2)
                {
                    buf.stack -= 3;
                    ChemsCardModel.AddChemToHand(owner);
                    ChemsCardModel.AddChemToHand(owner);
                }
                if (buf.stack < 1) buf.Destroy();
            }
        }

        public override string[] Keywords => new string[] { "Aftermath_StoredChems" };

        public static string Desc = "[On Use] Spend 3 Stored Chems to add 2 random Chem to hand";
    }

    public class DiceCardSelfAbility_Aftermath_DiscardLowestCostChem : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            var chemHand = owner.allyCardDetail.GetHand().FindAll(x => ChemsCardModel.IsChemCard(x));
            if (chemHand.Any())
            {
                chemHand.SortByCost();
                owner.allyCardDetail.DiscardACardByAbility(chemHand.First()) ;
            }
        }

        public static string Desc = "[On Use] Discard a Chem in with the lowest Cost";
    }

    public class DiceCardSelfAbility_Aftermath_DiscardHighestCostChem : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            var chemHand = owner.allyCardDetail.GetHand().FindAll(x => ChemsCardModel.IsChemCard(x));
            if (chemHand.Any())
            {
                chemHand.SortByCost();
                owner.allyCardDetail.DiscardACardByAbility(chemHand.Last());
                if (chemHand.Last().GetID() == new LorId(AftermathCollectionInitializer.packageId, 20101))
                {
                    var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is DiceCardSelfAbility_Aftermath_ChemHomeRun.BattleUnitBuf_ChemHomeRunSTAGGERYOURSELF);
                    if (buf != null) buf.Destroy();
                    card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus { max = 2 });
                }
            }
        }

        public static string Desc = "[On Use] Discard a Chem in with the highest Cost; if ‘Home Run’ was discarded this way, do not become Staggered at the end of the Scene due to its effect and boost the max value of all dice on this page by +2";
    }

    public class DiceCardSelfAbility_Aftermath_CannotUse6Speed : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.GetCurrentSpeed() < 6;
        }

        public static string Desc = "Cannot be used at 6+ Speed";
    }

    public class DiceCardSelfAbility_Aftermath_OnlyUse6Speed : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.GetCurrentSpeed() > 5;
        }

        public static string Desc = "Can only be used at 6+ Speed";
    }

    public class DiceCardSelfAbility_Aftermath_OnlyUse7Speed : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.GetCurrentSpeed() > 6;
        }

        public static string Desc = "Can only be used at 7+ Speed";
    }

    public class DiceCardSelfAbility_Aftermath_OnlyUse8Speed : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.GetCurrentSpeed() > 7;
        }

        public static string Desc = "Can only be used at 8+ Speed";
    }

    public class DiceCardSelfAbility_Aftermath_OnlyUse9Speed : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.GetCurrentSpeed() > 9;
        }

        public static string Desc = "Can only be used at 10+ Speed";
    }


    #endregion

    #region - MOBIUS OFFICE I -

    public class DiceCardSelfAbility_Aftermath_warpCharge4Draw1 : DiceCardSelfAbilityBase
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

        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 4, null);
            base.owner.allyCardDetail.DrawCards(1);
        }
        public static string Desc = "[On Use] Gain 4 Charge and draw 1 page";

    }

    public class DiceCardSelfAbility_Aftermath_DMOenergy1 : DiceCardSelfAbilityBase
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

        public override void OnUseCard()
        {
            base.owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
        public static string Desc = "[On Use] Restore 1 Light";
    }

    public class DiceCardSelfAbility_Aftermath_EnergizedShieldPrep : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (card.target != null)
            {
                if (!owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
                {
                    card.DestroyDice(DiceMatch.LastDice, DiceUITiming.Start);
                }
            }
        }
    }

    public class DiceCardSelfAbility_Aftermath_power1costOvercharge : DiceCardSelfAbilityBase
    {
        public override string[] Keywords => new string[] { "Aftermath_Dem_Overcharge_Keyword" };

        public override void OnUseCard()
        {
            var Overcharge = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Overcharge) as BattleUnitBuf_Aftermath_Overcharge;
            if (Overcharge != null && Overcharge.stack >= 3)
            {
                Overcharge.UseStack(3);
                this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    power = 1
                });
            }
        }
        public static string Desc = "[On Use] Spend 2 Overcharge to boost Power of all dice on this page by +1";
    }

    public class DiceCardSelfAbility_Aftermath_charge2friend : DiceCardSelfAbilityBase
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

        public override void OnStartBattle()
        {
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(this.card.owner.faction))
            {
                battleUnitModel.bufListDetail.AddKeywordBufByCard(KeywordBuf.WarpCharge, 2, null);
            }
        }
        public static string Desc = "[Combat Start] Give 2 allies 4 Charge";
    }

    public class DiceCardSelfAbility_Aftermath_Instant4ChargeEnemy : DiceCardSelfAbilityBase
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

        public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            base.OnRoundStart_inHand(unit, self);
            if (unit.faction <= Faction.Enemy)
            {
                if (unit.RollSpeedDice().FindAll((SpeedDice x) => !x.breaked).Count > 0 && !unit.IsBreakLifeZero() && unit.cardSlotDetail.PlayPoint >= 3)
                {
                    unit.cardSlotDetail.LosePlayPoint(3);
                    foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(unit.faction))
                    {
                        battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 4, null);
                    }
                }
            }
        }

        public static string Desc = "[On Play] Give all allies 4 Charge";
    }

    public class DiceCardSelfAbility_Aftermath_Instant4Charge : DiceCardSelfAbilityBase
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

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            unit.cardSlotDetail.LosePlayPoint(3);
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(unit.faction))
            {
                battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 4, null);
            }

            if (unit.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
            {
                BattleUnitBuf_Aftermath_Overcharge thing = unit.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Overcharge) as BattleUnitBuf_Aftermath_Overcharge;
                if (thing.stack > 0)
                {
                    thing.UseStack(thing.stack);
                    foreach (BattleUnitModel dude in BattleObjectManager.instance.GetAliveList(unit.faction))
                    {
                        if (dude != null)
                        {
                            dude.cardSlotDetail.RecoverPlayPointByCard(1);
                        }
                    }
                }

            }
        }
        public static string Desc = "[On Play] Give all allies 4 Charge; if the user has Overcharge, spend all of it to restore 1 light to all allies";
    }

    public class DiceCardSelfAbility_Aftermath_aceCard : DiceCardSelfAbilityBase
    {
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Aftermath_Dem_Overcharge_Keyword"
                };
            }
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            var activatedBuf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Overcharge) as BattleUnitBuf_Aftermath_Overcharge;
            return activatedBuf != null && activatedBuf.stack >= 10;
        }
        public static string Desc = "Only usable at 10+ Overcharge";
    }

    public class DiceCardSelfAbility_Aftermath_lanceCard : DiceCardSelfAbilityBase
    {
        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Aftermath_Dem_Overcharge_Keyword"
                };
            }
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            var activatedBuf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Overcharge) as BattleUnitBuf_Aftermath_Overcharge;
            return activatedBuf != null && activatedBuf.stack >= 8;
        }
        public static string Desc = "Only usable at 8+ Overcharge";
    }

    public class DiceCardSelfAbility_Aftermath_SpendChargeHP : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            BattleUnitBuf_Aftermath_Overcharge battleUnitBuf_Overcharge = base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_Overcharge) as BattleUnitBuf_Aftermath_Overcharge;
            bool flag = battleUnitBuf_Overcharge.stack > 0;
            if (flag)
            {
                battleUnitBuf_Overcharge.UseStack(battleUnitBuf_Overcharge.stack);
                this.card.owner.RecoverHP(10);
            }
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            BattleUnitBuf_Aftermath_Overcharge battleUnitBuf_Overcharge = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_Aftermath_Overcharge) as BattleUnitBuf_Aftermath_Overcharge;
            return battleUnitBuf_Overcharge != null && battleUnitBuf_Overcharge.stack >= 10;
        }

        public static string Desc = "Only usable at 10+ Overcharge\n[On Use] Spend all charge to recover 10 HP";
    }

    #endregion

    #region - THE LIME LIME -

    public class DiceCardSelfAbility_Aftermath_PrimeTimeOfYourLimePlayer : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            base.OnUseInstance(unit, self, targetUnit);
            unit.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_PrimeTimeOfYourLimeOneScene());
            BattleManagerUI.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(unit, unit.faction, unit.hp, unit.breakDetail.breakGauge, unit.bufListDetail.GetBufUIDataList());
        }

        public static string Desc = "[On Play] This Scene, all dice gain +1 Dice Power; if an attack is one-sided, deal 50% less damage and Stagger damage, gain +1 additional Power and reroll the die once";
    }

    public class DiceCardSelfAbility_Aftermath_Target3Enemies : DiceCardSelfAbilityBase
    {
        public override void OnApplyCard()
        {
            card.subTargets.Clear();
            List<BattleUnitModel> bruh = BattleObjectManager.instance.GetAliveList_opponent(owner.faction);
            int h = 0;
            foreach (var targetVar in bruh.FindAll(x => x != card.target))
            {
                card.subTargets.Add(new BattlePlayingCardDataInUnitModel.SubTarget() { target = targetVar, targetSlotOrder = UnityEngine.Random.Range(0, targetVar.speedDiceCount) });
                h++;
                if (h >= 2)
                {
                    break;
                }
            }
        }

        public static string Desc = "This page is used against 3 enemies";
    }

    public class DiceCardSelfAbility_Aftermath_Gain1PowerAgainstBased : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus() { power = 1 });
        }

        public override string[] Keywords => new string[] { "Aftermath_Basic" };

        public static string Desc = "If target has Based, dice on this page gain +1 Power";
    }

    public class DiceCardSelfAbility_Aftermath_Draw3PagesAnd2Erosion : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.allyCardDetail.DrawCards(3);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Decay, 2, owner);
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword", "DrawCard_Keyword"};

        public static string Desc = "[On Use] Draw 3 pages and inflict 2 Erosion to self";
    }

    public class DiceCardSelfAbility_Aftermath_RigobertoBasedOnWhatPlayer : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Based);
            if (buf != null)
            {
                owner.cardSlotDetail.RecoverPlayPointByCard(2);
                owner.allyCardDetail.DrawCards(1);
            } else
            {
                var buf2 = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
                if (buf2 != null && buf2.stack > 4)
                {
                    buf2.stack -= 4;
                    if (buf2.stack < 1) buf2.Destroy();
                    owner.bufListDetail.AddBuf(new BattleUnitBuf_Aftermath_Based() { stack = 2 });
                }
                
            }
        }

        public override string[] Keywords => new string[] { "Energy_Keyword", "DrawCard_Keyword", "Aftermath_Decay_Keyword", "Aftermath_Basic" } ;

        public static string Desc = "[On Use] If user is Based, restore 2 Light and draw 1 page; otherwise, spend 4 Erosion to become 2 Based";
    }

    public class DiceCardSelfAbility_Aftermath_RigobertoIntellectual : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            if (!owner.allyCardDetail.IsHighlander())
            {
                int stonks = owner.bufListDetail.GetKewordBufAllStack(KeywordBuf.Decay) / 5;
                for (int i = 0; i < Math.Min(stonks, 3); i++)
                {
                    owner.allyCardDetail.GetHand().SelectOneRandom().AddCost(-Math.Min(stonks, 2));
                }
            }
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "[On Use] If not Singleton, reduce the Cost of 1 page in hand for every 5 Erosion on self (Max. 3) by 1 for every 5 Erosion on self (Max. 2)";
    }

    public class DiceCardSelfAbility_Aftermath_RigobertoSublimeStrike : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, owner);
            if (card.target != null && card.target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) != null)
                owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, owner); 
        }

        public override string[] Keywords => new string[] { "Endurance_Keyword", "Strength_Keyword", "Aftermath_Decay_Keyword" };

        public static string Desc = "[On Use] Gain 1 Endurance next Scene; If target has Erosion, gain 1 Strength next Scene";
    }

    public class DiceCardSelfAbility_Aftermath_InflictAdditionalErosion : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            base.OnStartBattle();
            owner.bufListDetail.AddBuf(new BattlUnBuf_Aftermath_InflictAddDecay());
        }

        public class BattlUnBuf_Aftermath_InflictAddDecay : BattleUnitBuf
        {
            public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
            {
                return cardBuf.bufType == KeywordBuf.Decay ? 1 : 0;
            }

            public override void OnRoundEnd()
            {
                this.Destroy();
            }
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "[Combat Start] When inflicting Erosion using Combat Pages this Scene, inflict +1 additional stack";
    }

    public class DiceCardSelfAbility_Aftermath_THEBALLAD : DiceCardSelfAbilityBase
    {
        public override int GetCostAdder(BattleUnitModel unit, BattleDiceCardModel self)
        {
            return -(unit.bufListDetail.GetKewordBufAllStack(KeywordBuf.Decay) / 2);
        }

        public override void OnStartParrying()
        {
            base.OnStartParrying();
            var target = card.target;
            if (target != null)
            {
                var bufEn = target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
                var bufself = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
                if (bufEn != null && bufEn.stack >= 10)
                    card.ApplyDiceAbility(DiceMatch.NextDice, new DiceCardAbility_teddyEgo());
                if (bufself != null && bufself.stack >= 10)
                    card.ApplyDiceAbility(DiceMatch.NextAttackDice, new DiceCardAbility_Aftermath_HiddenEffectBALLAD(bufself.stack + (bufEn != null ? bufEn.stack : 0)));
                bufEn?.Destroy();
                bufself?.Destroy();
            }

        }

        public class DiceCardAbility_Aftermath_HiddenEffectBALLAD : DiceCardAbilityBase
        {
            int stacks;

            public DiceCardAbility_Aftermath_HiddenEffectBALLAD(int stonk) => stacks = stonk;

            public override void OnSucceedAttack(BattleUnitModel target)
            {
                base.OnSucceedAttack(target);
                target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, stacks, owner);
            }
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "This page’s Cost is lowered by half the amount of Erosion on self\n[Start of Clash] Purge all Erosion from self and target; if at least 10 Erosion was purged from target, the first die on this page gains ‘[On Clash Win] Destroy all of target’s dice’; if at least 10 Erosion was purged from self, the first die on this page gains ‘[On Hit] Inflict Erosion equal to all stacks of Erosion purged by this page’";
    }

    public class DiceCardSelfAbility_Aftermath_OnlyUsableAt3Erosion : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) is BattleUnitBuf buf && buf.stack >= 3;
        }

        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay).stack -= 3;
            if (owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) is BattleUnitBuf buf && buf.stack < 1)
                buf.Destroy();
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "Only usable at 3+ Erosion\n[On Use] Purge 3 Erosion from self";
    }

    public class DiceCardSelfAbility_Aftermath_OnlyUsableAt5Erosion : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) is BattleUnitBuf buf && buf.stack >= 5;
        }

        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay).stack -= 5;
            if (owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) is BattleUnitBuf buf && buf.stack < 1)
                buf.Destroy();
        }

        public override string[] Keywords => new string[] { "Aftermath_Decay_Keyword" };

        public static string Desc = "Only usable at 5+ Erosion\n[On Use] Purge 5 Erosion from self";
    }

    #endregion
}