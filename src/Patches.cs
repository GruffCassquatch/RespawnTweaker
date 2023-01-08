using System;
using Il2CppSystem;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace RespawnTweaker
{
    public enum AnimalType
    {
        False, Bear, Deer, Fish, Moose, Rabbit, Wolf
    }

    internal class Patches
    {
        [HarmonyPatch(typeof(SpawnRegion), "Start")]
        internal class UpdateSpawnRegion
        {
            private static void Postfix(SpawnRegion __instance)
            {
                if (Settings.settings.modFunction)
                {
                    if (IsSandbox())
                    {
                        string scene = GameManager.m_ActiveScene;
                        if (scene == null) return;

                        AnimalType animalType = GetAnimalType(__instance);
                        if (animalType != AnimalType.False)
                        {
                            ApplyAnimalSettings(animalType, __instance);
                        }


                    }
                }
            }
        }

        [HarmonyPatch(typeof(IceFishingHole), "Start", null)]
        internal static class UpdateIceFishingHole
        {
            private static void Postfix(IceFishingHole __instance)
            {
                if (Settings.settings.modFunction)
                {
                    string scene = GameManager.m_ActiveScene;
                    if (scene == null) return;

                        if (IsSandbox())
                        {
                            ApplyFishSettings(__instance);
                        }
                    }
                }
            }


        // Check if is Survival Sandbox game
        private static bool IsSandbox()
        {
            ExperienceModeType xpMode = ExperienceModeManager.GetCurrentExperienceModeType();
            switch (xpMode)
            {
                case ExperienceModeType.Pilgrim:
                    return true;
                case ExperienceModeType.Voyageur:
                    return true;
                case ExperienceModeType.Stalker:
                    return true;
                case ExperienceModeType.Interloper:
                    return true;
                case ExperienceModeType.Custom:
                    return true;
                default:
                    return false;
            }
        }

        private static AnimalType GetAnimalType(SpawnRegion spawnRegion)
        {
            AiSubType animalType = spawnRegion.m_AiSubTypeSpawned;

            switch (animalType)
            {
                case AiSubType.Wolf:
                    return AnimalType.Wolf;
                    break;
                case AiSubType.Bear:
                    return AnimalType.Bear;
                    break;
                case AiSubType.Stag:
                    return AnimalType.Deer;
                    break;
                case AiSubType.Rabbit:
                    return AnimalType.Rabbit;
                    break;
                case AiSubType.Moose:
                    return AnimalType.Moose;
                    break;
                default:
                    break;
            }
            return AnimalType.False;
        }

        private static void ApplyAnimalSettings(AnimalType animal, SpawnRegion spawnRegion)
        {
            switch (animal)
            {
                case AnimalType.Bear:
                    spawnRegion.m_NumHoursBetweenRespawns *= Settings.settings.bearRespawnRate;
                    break;
                case AnimalType.Deer:
                    spawnRegion.m_NumHoursBetweenRespawns *= Settings.settings.deerRespawnRate;
                    break;
                case AnimalType.Moose:
                    spawnRegion.m_NumHoursBetweenRespawns *= Settings.settings.mooseRespawnRate;
                    break;
                case AnimalType.Rabbit:
                    spawnRegion.m_NumHoursBetweenRespawns *= Settings.settings.mooseRespawnRate;
                    break;
                case AnimalType.Wolf:
                    spawnRegion.m_NumHoursBetweenRespawns *= Settings.settings.wolfRespawnRate;
                    break;
                default:
                    MelonLogger.Msg("ERROR: Animal Type Settings not found!");
                    return;
            }
        }

        private static void ApplyFishSettings(IceFishingHole iceFishingHole)
        {
            iceFishingHole.m_MinGameMinutesCatchFish /= Settings.settings.fishRespawnRate;
            iceFishingHole.m_MaxGameMinutesCatchFish /= Settings.settings.fishRespawnRate;
        }
    }
}
