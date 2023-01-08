using System.IO;
using System.Reflection;
using ModSettings;

namespace RespawnTweaker
{
    class RespawnTweakerSettings : JsonModSettings
    {
        [Section("Enable Mod")]
        [Name("Enable Mod")]
        [Description("NO: Mod is disabled. YES: Mod is enabled.")]
        public bool modFunction = false;

        [Name("Respawn Rate: Bear")]
        [Description("MULTIPLIER for Respawn Rate.\n1 = Game Default, 2 = 2x FASTER than Game Default,\n0.5 = 2x SLOWER than Game Default, 0 = Never.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00##}x")]
        public float bearRespawnRate = 1f;

        [Name("Respawn Rate: Deer")]
        [Description("MULTIPLIER for Respawn Rate.\n1 = Game Default, 2 = 2x FASTER than Game Default,\n0.5 = 2x SLOWER than Game Default, 0 = Never.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00##}x")]
        public float deerRespawnRate = 1f;

        [Name("Respawn Rate: Fish")]
        [Description("MULTIPLIER for Respawn Rate.\n1 = Game Default, 2 = 2x FASTER than Game Default,\n0.5 = 2x SLOWER than Game Default, 0 = Never.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00##}x")]
        public float fishRespawnRate = 1f;

        [Name("Respawn Rate: Moose")]
        [Description("MULTIPLIER for Respawn Rate.\n1 = Game Default, 2 = 2x FASTER than Game Default,\n0.5 = 2x SLOWER than Game Default, 0 = Never.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00##}x")]
        public float mooseRespawnRate = 1f;

        [Name("Respawn Rate: Rabbit")]
        [Description("MULTIPLIER for Respawn Rate.\n1 = Game Default, 2 = 2x FASTER than Game Default,\n0.5 = 2x SLOWER than Game Default, 0 = Never.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00##}x")]
        public float rabbitRespawnRate = 1f;

        [Name("Respawn Rate: Wolf")]
        [Description("MULTIPLIER for Respawn Rate.\n1 = Game Default, 2 = 2x FASTER than Game Default,\n0.5 = 2x SLOWER than Game Default, 0 = Never.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00##}x")]
        public float wolfRespawnRate = 1f;

        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            if (field.Name == nameof(modFunction)) RefreshFields();
        }

        internal void RefreshFields()
        {
            SetFieldVisible(nameof(bearRespawnRate), modFunction);
            SetFieldVisible(nameof(deerRespawnRate), modFunction);
            SetFieldVisible(nameof(fishRespawnRate), modFunction);
            SetFieldVisible(nameof(mooseRespawnRate), modFunction);
            SetFieldVisible(nameof(rabbitRespawnRate), modFunction);
            SetFieldVisible(nameof(wolfRespawnRate), modFunction);
        }
    }

    internal static class Settings
    {
        internal static RespawnTweakerSettings settings;
        internal static void OnLoad()
        {
            settings = new RespawnTweakerSettings();
            settings.RefreshFields();
            settings.AddToModSettings("Respawn Tweaker");
        }
    } 
}
