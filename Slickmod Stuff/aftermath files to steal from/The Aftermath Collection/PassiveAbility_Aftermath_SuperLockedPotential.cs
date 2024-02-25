// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_SuperLockedPotential
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using LOR_XML;
using Sound;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_SuperLockedPotential : PassiveAbilityBase
  {
    private List<LorId> _cardIdList = new List<LorId>();
    private bool trigger;
    private GameObject _aura;

    public override void OnWaveStart()
    {
      if (this.owner.UnitData.floorBattleData.param1 == 0)
        this._cardIdList = new List<LorId>();
      if (this.owner.UnitData.floorBattleData.param1 != 1)
        return;
      this.IndexWaveStartUnlock();
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
      if (this.owner.UnitData.floorBattleData.param1 != 0)
        return;
      LorId id = curCard.card.GetID();
      if (!this._cardIdList.Contains(id))
        this._cardIdList.Add(id);
      if (this.trigger)
        return;
      if (this._cardIdList.Count >= 6)
      {
        this.owner.bufListDetail.RemoveBufAll(typeof (PassiveAbility_250115.BattleUnitBuf_indexReleaseCount));
        if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null)
          return;
        this.IndexCardUnlock();
      }
      else
      {
        this.owner.bufListDetail.RemoveBufAll(typeof (PassiveAbility_250115.BattleUnitBuf_indexReleaseCount));
        BattleUnitBufListDetail bufListDetail = this.owner.bufListDetail;
        PassiveAbility_250115.BattleUnitBuf_indexReleaseCount buf = new PassiveAbility_250115.BattleUnitBuf_indexReleaseCount();
        buf.stack = this._cardIdList.Count;
        bufListDetail.AddBufWithoutDuplication((BattleUnitBuf) buf);
      }
    }

    public override void OnRoundStart()
    {
      if (this.owner.UnitData.floorBattleData.param1 != 0)
        return;
      foreach (BattleDiceCardModel battleDiceCardModel in this.owner.allyCardDetail.GetAllDeck())
        battleDiceCardModel.RemoveBuf<PassiveAbility_250115.BattleDiceCardBuf_indexReleaseCount>();
      if (this._cardIdList.Count >= 6)
        return;
      foreach (BattleDiceCardModel battleDiceCardModel in this.owner.allyCardDetail.GetAllDeck())
      {
        if (!this._cardIdList.Contains(battleDiceCardModel.GetID()) && battleDiceCardModel.GetSpec().Ranged != CardRange.Instance)
          battleDiceCardModel.AddBuf((BattleDiceCardBuf) new PassiveAbility_250115.BattleDiceCardBuf_indexReleaseCount());
      }
    }

    public override void OnDestroyed()
    {
      if (!((UnityEngine.Object) this._aura != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this._aura);
    }

    private void SetParticle()
    {
      if (!((UnityEngine.Object) this._aura == (UnityEngine.Object) null))
        return;
      UnityEngine.Object original1 = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_Aura");
      if (original1 != (UnityEngine.Object) null)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate(original1) as GameObject;
        gameObject.transform.parent = this.owner.view.charAppearance.transform;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
        IndexReleaseAura component = gameObject.GetComponent<IndexReleaseAura>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.Init(this.owner.view);
        this._aura = gameObject;
      }
      UnityEngine.Object original2 = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_ActivateParticle");
      if (original2 != (UnityEngine.Object) null)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate(original2) as GameObject;
        gameObject.transform.parent = this.owner.view.charAppearance.transform;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
      }
      SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Buf/Effect_Index_Unlock");
    }

    public override void OnBattleEnd_alive()
    {
      this.owner.UnitData.floorBattleData.param1 = 0;
      if (!this.trigger)
        return;
      this.owner.UnitData.floorBattleData.param1 = 1;
    }

    private void IndexCardUnlock()
    {
      this.trigger = true;
      this.owner.bufListDetail.RemoveBufAll(typeof (PassiveAbility_250115.BattleUnitBuf_indexReleaseCount));
      if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) == null)
      {
        this.owner.bufListDetail.AddBuf((BattleUnitBuf) new PassiveAbility_250115.BattleUnitBuf_indexRelease());
        this.owner?.battleCardResultLog?.SetUseCardEvent(new BattleCardBehaviourResult.BehaviourEvent(this.SetParticle));
      }
      if (this.owner.customBook.ContainsCategory(BookCategory.TheIndex))
      {
        this.owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
        this.owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
        this.owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
      }
      if (!(this.owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42060302)) && !(this.owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42060301)))
        return;
      this.owner.view.DisplayDlg(DialogType.SPECIAL_EVENT, "BLADE_UNLOCK_" + Singleton<System.Random>.Instance.Next(2).ToString());
    }

    private void IndexWaveStartUnlock()
    {
      this.trigger = true;
      this.owner.bufListDetail.RemoveBufAll(typeof (PassiveAbility_250115.BattleUnitBuf_indexReleaseCount));
      if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) == null)
      {
        this.owner.bufListDetail.AddBuf((BattleUnitBuf) new PassiveAbility_250115.BattleUnitBuf_indexRelease());
        if (this.owner != null)
          this.SetParticle();
      }
      if (!this.owner.customBook.ContainsCategory(BookCategory.TheIndex))
        return;
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
    }

    public void ClearList() => this._cardIdList.Clear();
  }
}
