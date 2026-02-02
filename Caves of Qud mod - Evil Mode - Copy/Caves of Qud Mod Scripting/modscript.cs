
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





//Spear Attack Event


namespace Scripting
{
    public class Script
    {
        


     public bool AddSkill(GameObject GO)
    {
      ActivatedAbilities part = GO.GetPart("ActivatedAbilities") as ActivatedAbilities;
      if (part != null)
      {
        this.ActivatedAbilityID = part.AddAbility("Pierce [&Wattack&y]", "CommandReachAttack", "Skill", -1, false, false, "You attack with your spear from a distance", "-", false, false);
        this.Ability = part.AbilityByGuid[this.ActivatedAbilityID];
      }
      return true;
    }

    public bool RemoveSkill(GameObject GO)
    {
      if (this.ActivatedAbilityID != Guid.Empty)
        (GO.GetPart("ActivatedAbilities") as ActivatedAbilities).RemoveAbility(this.ActivatedAbilityID);
      return true;
    }
            


    }


    public class CommandReachAttack
    {
        
    }
      

}

