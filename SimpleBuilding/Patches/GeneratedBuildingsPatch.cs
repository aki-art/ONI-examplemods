using HarmonyLib;
using STRINGS;

namespace SimpleBuilding.Patches
{
    class GeneratedBuildingsPatch
    {
        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            // This code will be inserted at the top of GeneratedBuildings.LoadGeneratedBuildings
            public static void Prefix()
            {
                // Add out lamp to the Furniture menu
                ModUtil.AddBuildingToPlanScreen("Furniture", ExampleLampConfig.ID);

                // Make it researchable 
                Db.Get().Techs.Get("InteriorDecor").unlockedItemIDs.Add(ExampleLampConfig.ID);

                // Give it a name and a description
                Strings.Add("STRINGS.BUILDINGS.PREFABS.MYMOD_EXAMPLELAMP.NAME", "Example Lamp");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.MYMOD_EXAMPLELAMP.DESC", "My flavor text for this lamp.");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.MYMOD_EXAMPLELAMP.EFFECT", $"Provides {UI.FormatAsLink("Light", "LIGHT")}");
            }
        }
    }
}
