using TUNING;
using UnityEngine;

namespace SimpleBuilding
{
    public class ExampleLampConfig : IBuildingConfig
    {
        // It's a good idea to prefix your ID-s with a unique word, to avoid conflicts with other mods
        public static string ID = "MyMod_ExampleLamp";

        // Storing some values to easily access later
        public static Vector2 LIGHT_OFFSET = new Vector2(0.05f, 1.5f);
        public static float RANGE = 4f;
        public static int LUX = 400;
        public static LightShape SHAPE = LightShape.Circle;

        // A BuildingDef holds configuration about our building
        public override BuildingDef CreateBuildingDef()
        {
            BuildingDef def = BuildingTemplates.CreateBuildingDef(
               id: ID,
               width: 1,
               height: 2,
               anim: "examplelamp_kanim", // this is the name of the FOLDER in which your animation is. always add _kanim to the end of this
               hitpoints: BUILDINGS.HITPOINTS.TIER2,
               construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER1,
               construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER1,
               construction_materials: MATERIALS.ALL_METALS,
               melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1,
               build_location_rule: BuildLocationRule.OnFloor,
               decor: DECOR.BONUS.TIER1,
               noise: NOISE_POLLUTION.NONE);

            // BuildingDef has a bunch of useful fields you can tinker with, here are a few example settings:
            def.RequiresPowerInput = true;
            def.EnergyConsumptionWhenActive = 7f;
            def.SelfHeatKilowattsWhenActive = 0.5f;
            def.ViewMode = OverlayModes.Light.ID;
            def.AudioCategory = "Metal";

            return def;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            // Tell the game that this building counts as a lightsource for rooms that need one
            go.AddTag(RoomConstraints.ConstraintTags.LightSource);
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            // Configure the PREVIEW for the light source
            LightShapePreview lightShapePreview = go.AddComponent<LightShapePreview>();
            lightShapePreview.lux = LUX;
            lightShapePreview.radius = RANGE;
            lightShapePreview.shape = SHAPE;
            lightShapePreview.offset = new CellOffset(LIGHT_OFFSET);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            // Add component to consume energy
            go.AddOrGet<EnergyConsumer>();

            // Configure the actual light source
            Light2D light2d = go.AddOrGet<Light2D>();
            light2d.Lux = LUX;
            light2d.Range = RANGE;
            light2d.shape = SHAPE;
            light2d.overlayColour = LIGHT2D.FLOORLAMP_OVERLAYCOLOR;
            light2d.Color = new Color(0.35f, 2f, 0.5f);
            light2d.Offset = LIGHT_OFFSET;
            light2d.drawOverlay = true;

            // Add component to toggle being on and off as a lamp
            go.AddOrGetDef<LightController.Def>();
        }
    }
}
