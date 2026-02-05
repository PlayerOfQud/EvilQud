
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime;
using System.Security;
using Qud;
using Qud.API;
using Qud.UI;
using System.IO;
using System;
using XRL.World;
using XRL.World.Parts;
using XRL;





//Spear Attack Event


namespace XRL.World.Parts.Skill
{

  [Serializable]


  
    public class ReachAttack
  {

    public override void Register(GameObject Object)
    {
      Object.RegisterPartEvent((IPart) this, "CommandSpearReachAttack");
      Object.RegisterPartEvent((IPart) this, "AIGetOffensiveMutationList");
      base.Register(Object);
    }

    public override bool AddSkill(GameObject GO)
    {
      ActivatedAbilities part = GO.GetPart("ActivatedAbilities") as ActivatedAbilities;
      if (part != null)
      {
        this.ActivatedAbilityID = part.AddAbility("Reach Attack [&Wattack&y]", "CommandSpearReachAttack", "Skill", -1, false, false, "You strike with your spear from a distance.", "-", false, false);
        this.Ability = part.AbilityByGuid[this.ActivatedAbilityID];
      }
      return true;
    }

    public override bool RemoveSkill(GameObject GO)
    {
      if (this.ActivatedAbilityID != Guid.Empty)
        (GO.GetPart("ActivatedAbilities") as ActivatedAbilities).RemoveAbility(this.ActivatedAbilityID);
      return true;
    }

    public override bool FireEvent(Event E)
    {
      if (E.ID == "AIGetOffensiveMutationList")
      {
        int intParameter = E.GetIntParameter("Distance");
        if (E.GetGameObjectParameter("Target") == null || !this.IsPrimarySpearEquipped() || this.ParentObject.pPhysics != null && this.ParentObject.pPhysics.IsFrozen())
          return true;
        List<AICommandList> parameter = (List<AICommandList>) E.GetParameter("List");
        if (this.Ability != null && this.Ability.Cooldown <= 0 && intParameter <= 2)
          parameter.Add(new AICommandList("CommandSpearReachAttack", 1));
        return true;
      }
      if (E.ID == "CommandSpearReachAttack")
      {
        if (!this.IsPrimarySpearEquipped())
        {
          if (this.ParentObject.IsPlayer())
            Popup.Show("You must have a spear equipped in order to perform a reach attack.", true);
          return true;
        }
        if (this.ParentObject.pPhysics != null && this.ParentObject.pPhysics.IsFrozen())
        {
          if (this.ParentObject.IsPlayer())
            Popup.Show("You are frozen solid!", true);
          return true;
        }
        string str = this.PickDirectionS();
        Cell cellFromDirection = this.ParentObject.GetCurrentCell().GetCellFromDirection(str, true);
        if (cellFromDirection == null)
          return true;

          this.ParentObject.UseEnergy(1000, "Skill Reach Attack");
          if (!this.ParentObject.HasEffect("Spear_Reach"))
            (this.ParentObject.GetPart("ActivatedAbilities") as ActivatedAbilities).AbilityByGuid[this.ActivatedAbilityID].Cooldown = 510;

      }
    }

  }

  public bool isPrimarySpearEquipped()
    {
      return true;
    }


  public Event CommandSpearReachAttack()
    {
      return new Event E;
    }

}

