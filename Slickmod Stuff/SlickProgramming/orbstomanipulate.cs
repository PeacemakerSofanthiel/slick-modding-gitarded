using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickRuinaMod;
using UnityEngine;

namespace SlickRuinaMod
{
    public class passivedefectorbbase : PassiveAbilityBase
    {
        public virtual bool halvefocusgain => false;
        public virtual int orblimit => 1;
        public virtual void onevoke()
        {

        }
    }
    class PassiveAbilityBase_SlickMod_DefectiveCapacitor2 : passivedefectorbbase
    {
        public override bool halvefocusgain => true;
        public override int orblimit => 2;
    }
    public class PassiveAbilityBase_SlickMod_Capacitor3 : passivedefectorbbase
    {
        public override int orblimit => 3;
    }
    public class PassiveAbilityBase_SlickMod_Capacitor4 : passivedefectorbbase
    {
        public override int orblimit => 4;
    }
    public class PassiveAbilityBase_SlickMod_Capacitor5 : passivedefectorbbase
    {
        public override int orblimit => 5;
    }

    public class BattleUnitBuf_SlickMod_Orb_Lockon : BattleUnitBuf
    {
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            stack = 0;
            bool based = false;
            foreach (BattleUnitModel gamer in BattleObjectManager.instance.GetAliveList())
            {
                if (gamer != null && gamer != owner)
                {
                    if (gamer.bufListDetail.HasBuf<BattleUnitBuf_SlickMod_Orb_Lockon>())
                    {
                        based = true;
                    }
                }
            }
            if (based)
            {
                owner.bufListDetail.RemoveBuf(this);
            }
        }
        public override string keywordId => "SlickMod_Orb_Lockon";
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            Destroy();
        }
    }

    public class BattleUnitBuf_SlickMod_Orb_Focus : BattleUnitBuf
    {
        public override void OnAddBuf(int addedStack)
        {
            base.OnAddBuf(addedStack);
            bool based = false;
            bool cringe = false;
            foreach (PassiveAbilityBase thingy in this._owner.passiveDetail.PassiveList)
            {
                if (thingy != null && thingy is passivedefectorbbase)
                {
                    cringe = true;
                    passivedefectorbbase bim = thingy as passivedefectorbbase;
                    if (bim.halvefocusgain)
                    {
                        based = true;
                    }
                }
            }
            if (based)
            {
                stack /= 2;
            }
            if (!cringe)
            {
                Destroy();
            }
        }
        public override KeywordBuf bufType => MyKeywordBufs.SlickMod_Orb_Focus;
        public override BufPositiveType positiveType => BufPositiveType.Positive;
        public override string keywordId => "SlickMod_Orb_Focus";
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            Destroy();
        }
    }

    public class bufforbbase : BattleUnitBuf
    {
        public virtual void onevoke()
        {
            foreach (PassiveAbilityBase thingy in _owner.passiveDetail.PassiveList)
            {
                if (thingy != null && thingy is passivedefectorbbase)
                {
                    try
                    {
                        passivedefectorbbase bimmer = thingy as passivedefectorbbase;
                        bimmer.onevoke();
                    }
                    catch (Exception ex)
                    {
                        Debug.Log(ex);
                    }
                }
            }
            Destroy();
        }
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            stack = 0;
            int b = 1;
            foreach (PassiveAbilityBase thingy in owner.passiveDetail.PassiveList)
            {
                if (thingy != null && thingy is passivedefectorbbase)
                {
                    try
                    {
                        passivedefectorbbase bimmer = thingy as passivedefectorbbase;
                        if (bimmer.orblimit > b)
                        {
                            b = bimmer.orblimit;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Log(ex);
                    }
                }
            }
            if (owner.bufListDetail.GetActivatedBufList().Count(x => x is bufforbbase && !x.IsDestroyed()) >= b)
            {
                bufforbbase thingy = owner.bufListDetail.GetActivatedBufList().Find(x => x is bufforbbase && !x.IsDestroyed()) as bufforbbase;
                if (thingy != null)
                {
                    thingy.onevoke();
                }
            }
        }
    }
    public class SlickMod_Orb_Lightning : bufforbbase
    {
        public override string keywordId => "SlickMod_Orb_Lightning_Orb";
        public override void onevoke()
        {
            base.onevoke();
            List<BattleUnitModel> dudes = BattleObjectManager.instance.GetAliveList_opponent(_owner.faction);
            if (dudes.Count(x => x.bufListDetail.HasBuf<BattleUnitBuf_SlickMod_Orb_Lockon>()) > 0)
            {
                BattleUnitModel dude = RandomUtil.SelectOne(dudes.FindAll(x => x.bufListDetail.HasBuf<BattleUnitBuf_SlickMod_Orb_Lockon>()));
                if (dude != null)
                {
                    dude.TakeDamage(8 + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Focus), DamageType.Buf, _owner);
                }
            }
            else
            {
                BattleUnitModel dude = RandomUtil.SelectOne(dudes);
                if (dude != null)
                {
                    dude.TakeDamage(8 + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Focus), DamageType.Buf, _owner);
                }
            }
        }
        public override void OnRoundStartAfter()
        {
            base.OnRoundStartAfter();
            List<BattleUnitModel> dudes = BattleObjectManager.instance.GetAliveList_opponent(_owner.faction);
            if (dudes.Count(x => x.bufListDetail.HasBuf<BattleUnitBuf_SlickMod_Orb_Lockon>()) > 0)
            {
                BattleUnitModel dude = RandomUtil.SelectOne(dudes.FindAll(x => x.bufListDetail.HasBuf<BattleUnitBuf_SlickMod_Orb_Lockon>()));
                if (dude != null)
                {
                    dude.TakeDamage(3 + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Focus), DamageType.Buf, _owner);
                }
            }
            else
            {
                BattleUnitModel dude = RandomUtil.SelectOne(dudes);
                if (dude != null)
                {
                    dude.TakeDamage(3 + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Focus), DamageType.Buf, _owner);
                }
            }
        }
    }
    public class SlickMod_Orb_Frost : bufforbbase
    {

        public override string keywordId => "SlickMod_Orb_Frost_Orb";
        public override void onevoke()
        {
            base.onevoke();
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(generic_buffs.keywordsing.shield, 7 + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Focus), _owner);
        }
        public override void OnRoundStartAfter()
        {
            base.OnRoundStartAfter();
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(generic_buffs.keywordsing.shield, 3 + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Focus), _owner);
        }
    }
    public class SlickMod_Orb_Darkness : bufforbbase
    {

        public override string keywordId => "SlickMod_Orb_Darkness_Orb";
        public override void onevoke()
        {
            base.onevoke();
            int b = 999999999;
            foreach (BattleUnitModel gamer in BattleObjectManager.instance.GetAliveList_opponent(_owner.faction))
            {
                if (gamer != null)
                {
                    if (gamer.hp < b)
                    {
                        b = (int)gamer.hp;
                    }
                }
            }
            if (BattleObjectManager.instance.GetAliveList_opponent(_owner.faction).Count(x => x.hp == b) > 0)
            {
                BattleUnitModel dude = RandomUtil.SelectOne(BattleObjectManager.instance.GetAliveList_opponent(_owner.faction).FindAll(x => x.hp == b));
                if (dude != null)
                {
                    dude.TakeDamage(6 + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Focus) + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Malice), DamageType.Buf, _owner);
                }
            }
            this._owner.bufListDetail.RemoveBufAll(MyKeywordBufs.SlickMod_Orb_Malice);
        }
        public override void OnRoundStartAfter()
        {
            base.OnRoundStartAfter();
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(MyKeywordBufs.SlickMod_Orb_Malice, 3 + _owner.bufListDetail.GetKewordBufStack(MyKeywordBufs.SlickMod_Orb_Focus), _owner);
        }
    }
    public class SlickMod_Orb_Plasma : bufforbbase
    {

        public override string keywordId => "SlickMod_Orb_Plasma_Orb";
        public override void onevoke()
        {
            base.onevoke();
            this._owner.bufListDetail.AddBuf(new library());
        }
        public override int MaxPlayPointAdder()
        {
            return 1;
        }
        public override void OnRoundStartAfter()
        {
            base.OnRoundStartAfter();
            _owner.cardSlotDetail.RecoverPlayPoint(1);
        }
        public class library : BattleUnitBuf
        {
            public override void OnRoundStartAfter()
            {
                base.OnRoundStartAfter();
                this._owner.cardSlotDetail.RecoverPlayPoint(2);
                this._owner.bufListDetail.RemoveBuf(this);
            }
        }
    }
    public class BattleUnitBuf_SlickMod_Orb_Malice : BattleUnitBuf
    {
        public override KeywordBuf bufType => MyKeywordBufs.SlickMod_Orb_Malice;
        public override string keywordId => "SlickMod_Orb_Malice";
    }

    public class DiceCardSelfAbility_SlickMod_ExampleDefect : DiceCardSelfAbilityBase
    {
        public static string Desc = "[On Use] Gain 1 Lightning Orb, Frost Orb, Darkness Orb, and Plasma Orb";
        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.bufListDetail.AddBuf(new SlickMod_Orb_Lightning());
            owner.bufListDetail.AddBuf(new SlickMod_Orb_Frost());
            owner.bufListDetail.AddBuf(new SlickMod_Orb_Darkness());
            owner.bufListDetail.AddBuf(new SlickMod_Orb_Plasma());
        }
    }
}
