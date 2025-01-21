using System.Drawing;
using System.Collections.Generic;
using ExileCore2;
using ExileCore2.PoEMemory.MemoryObjects;
using ExileCore2.PoEMemory.Components;

namespace Tablets;

// Generated: Main plugin class for handling tablet item highlighting in Path of Exile
// Generated: Provides visual indicators for valuable tablet combinations based on user settings
public class Tablets : BaseSettingsPlugin<TabletsSettings>
{
    // Generated: Stores information about tablet frames to be rendered
    private class TabletInfo
    {
        public ExileCore2.Shared.RectangleF Rectangle { get; set; }
        public Color Color { get; set; }
    }
    private List<TabletInfo> _currentFrames = new();
    private System.Diagnostics.Stopwatch _updateTimer = new();
    private const int UPDATE_INTERVAL_MS = 100;

    public override bool Initialise()
    {
        _updateTimer.Start();
        return true;
    }
    private bool IsStashOpen() => 
        GameController?.Game?.IngameState?.IngameUi?.StashElement?.IsVisible == true;
    private bool IsTabletItem(Entity item) => item?.Path.Contains("Metadata/Items/TowerAugment") == true;

    // ==========================================================
    // ===================== Tick Update ========================
    // ==========================================================
    // Generated: Core update logic that processes visible tablets in stash
    public override void Tick()
    {
        if (_updateTimer.ElapsedMilliseconds < UPDATE_INTERVAL_MS)
            return;

        _updateTimer.Restart();
        _currentFrames.Clear();

        if (!IsStashOpen())
            return;

        var stash = GameController?.Game?.IngameState?.IngameUi?.StashElement;
            if (stash == null || stash.VisibleStash == null)
                return;
        var items = stash.VisibleStash.VisibleInventoryItems;
            if (items == null)
                return;
        foreach (var item in items)
        {
            if (item?.Item == null || !IsTabletItem(item.Item))
                continue;

            if (!ShouldRenderTablet(item.Item))
                continue;

            var color = GetFrameColorByMods(item.Item);

            if (Settings.HideBadTablets && color == Settings.BorderRenderSettings.BadTabletBorderColor)
                continue;

            var rect = item.GetClientRect();
            var insetRect = new ExileCore2.Shared.RectangleF(
                rect.X + 3,
                rect.Y + 3,
                rect.Width - 6,
                rect.Height - 6
            );

            _currentFrames.Add(new TabletInfo
            {
                Rectangle = insetRect,
                Color = color
            });
        }
    }

    // ==========================================================
    // ================ Checking Tablet Type ====================
    // ==========================================================
    // Generated: Determines if a tablet should be highlighted based on its mods and settings
    private bool ShouldRenderTablet(Entity item)
    {
        var mods = item?.GetComponent<Mods>();
        if (mods == null) return false;

        var rarity = mods.ItemRarity.ToString();
        var modsCount = mods.ItemMods.Count;

        bool hasLowMapsInRange = false;
        if (Settings.DrawLowMapsInRangeTablets)
        {
            foreach (var mod in mods.ItemMods)
            {
                if (mod.Group.StartsWith("TowerAddContent"))
                {
                    int towerValue = mod.Values.Count > 0 ? mod.Values[0] : 0;
                    if (towerValue < Settings.MapsInRange.Value)
                    {
                        hasLowMapsInRange = true;
                        break;
                    }
                }
            }
        }

        if (!((rarity == "Normal" && Settings.DrawNormalTablet) ||
              (modsCount == 2 && Settings.DrawTwoModTablets) ||
              modsCount == 3 ||
              hasLowMapsInRange))
        {
            return false;
        }

        if (hasLowMapsInRange)
            return true;

        foreach (var mod in mods.ItemMods)
        {
            if (!mod.Group.StartsWith("TowerAddContent"))
                continue;

            int towerValue = mod.Values.Count > 0 ? mod.Values[0] : 0;

            if (towerValue < Settings.MapsInRange.Value)
                continue;

            switch (mod.Name)
            {
                case "TowerAddDeliriumToMapsImplicit":
                    if (Settings.DeliriumTabletSettings.EnableDeliriumTablet)
                        return true;
                    break;
                case "TowerAddBreachToMapsImplicit":
                    if (Settings.BreachTabletSettings.EnableBreachTablet)
                        return true;
                    break;
                case "TowerAddMapBossesToMapsImplicit":
                    if (Settings.OverseerTabletSettings.EnableOverseerTablet)
                        return true;
                    break;
                case "TowerAddRitualToMapsImplicit":
                    if (Settings.RitualTabletSettings.EnableRitualTablet)
                        return true;
                    break;
                case "TowerAddExpeditionToMapsImplicit":
                    if (Settings.ExpeditionTabletSettings.EnableExpeditionTablet)
                        return true;
                    break;
                case "TowerAddIrradiatedToMapsImplicit":
                    if (Settings.PrecursorTabletSettings.EnablePrecursorTablet)
                        return true;
                    break;
            }
        }

        return false;
    }

    // ==========================================================
    // =================== Checking mods ========================
    // ==========================================================
    // Generated: Determines the highlight color based on tablet quality and settings colors
    private Color GetFrameColorByMods(Entity item)
    {
        var mods = item?.GetComponent<Mods>();
        if (mods == null) return Settings.BorderRenderSettings.BadTabletBorderColor;

        if (Settings.DrawLowMapsInRangeTablets)
        {
            foreach (var mod in mods.ItemMods)
            {
                if (mod.Group.StartsWith("TowerAddContent"))
                {
                    int towerValue = mod.Values.Count > 0 ? mod.Values[0] : 0;
                    if (towerValue < Settings.MapsInRange.Value)
                    {
                        return Settings.BorderRenderSettings.LowMapsInRangeBorderColor;
                    }
                }
            }
        }

        var modsCount = mods.ItemMods.Count;

        if (modsCount == 1)
            return Settings.BorderRenderSettings.NormalBorderColor;

        if (modsCount == 2)
            return Settings.BorderRenderSettings.TwoModBorderColor;

        if (modsCount == 3)
        {
            bool hasValidMapInRange = false;
            int enabledModsCount = 0;

            foreach (var mod in mods.ItemMods)
            {
                if (IsMapInRangeMod(mod))
                {
                    hasValidMapInRange = true;
                    continue;
                }

                if (IsEnabledCommonPrefixMod(mod))
                {
                    enabledModsCount++;
                    continue;
                }

                if (IsEnabledTypeSpecificMod(mod))
                {
                    enabledModsCount++;
                    continue;
                }
            }

            if (hasValidMapInRange && enabledModsCount == 2)
            {
                return Settings.BorderRenderSettings.ReadyToGoBorderColor;
            }

            return Settings.BorderRenderSettings.BadTabletBorderColor;
        }

        return Settings.BorderRenderSettings.BadTabletBorderColor;
    }

    private bool IsMapInRangeMod(ItemMod mod)
    {
        if (!mod.Group.StartsWith("TowerAddContent")) return false;
        int towerValue = mod.Values.Count > 0 ? mod.Values[0] : 0;
        return towerValue >= Settings.MapsInRange.Value;
    }

    private bool IsEnabledTypeSpecificMod(ItemMod mod)
    {
        if (Settings.BreachTabletSettings.EnableBreachTablet)
        {
            if (IsEnabledBreachMod(mod)) return true;
        }
        if (Settings.RitualTabletSettings.EnableRitualTablet)
        {
            if (IsEnabledRitualMod(mod)) return true;
        }
        if (Settings.OverseerTabletSettings.EnableOverseerTablet)
        {
            if (IsEnabledOverseerMod(mod)) return true;
        }
        if (Settings.ExpeditionTabletSettings.EnableExpeditionTablet)
        {
            if (IsEnabledExpeditionMod(mod)) return true;
        }
        if (Settings.DeliriumTabletSettings.EnableDeliriumTablet)
        {
            if (IsEnabledDeliriumMod(mod)) return true;
        }
        if (Settings.PrecursorTabletSettings.EnablePrecursorTablet)
        {
            if (IsEnabledPrecursorMod(mod)) return true;
        }
        return false;
    }

    private bool IsEnabledCommonPrefixMod(ItemMod mod)
    {
        var settings = Settings.CommonPrefixModsSettings;
        return mod.Group switch
        {
            "MapDroppedItemQuantityIncrease" => settings.MapDroppedItemQuantityIncrease,
            "MapDroppedItemRarityIncrease" => settings.MapDroppedItemRarityIncrease,
            "MapPackSizeIncrease" => settings.MapPackSizeIncrease,
            "MapMagicPackIncrease" => settings.MapMagicPackIncrease,
            "MapRarePackIncrease" => settings.MapRarePackIncrease,
            "MapDroppedGoldIncrease" => settings.MapDroppedGoldIncrease,
            "MapExperienceGainIncrease" => settings.MapExperienceGainIncrease,
            _ => false
        };
    }

    private bool IsEnabledBreachMod(ItemMod mod)
    {
        var settings = Settings.BreachTabletSettings;
        return mod.Group switch
        {
            "BreachMagicMonsterIncrease" => settings.BreachMagicMonsterIncrease,
            "BreachRareMonsterIncrease" => settings.BreachRareMonsterIncrease,
            "BreachDensityIncrease" => settings.BreachDensityIncrease,
            "BreachSpeedIncrease" => settings.BreachSpeedIncrease,
            "BreachChestAdditional" => settings.BreachChestAdditional,
            "BreachMonsterSplinterIncrease" => settings.BreachMonsterSplinterIncrease,
            "Breach3AdditionalChance" => settings.Breach3AdditionalChance,
            "BreachAdditionalChance" => settings.BreachAdditionalChance,
            _ => false
        };
    }

    private bool IsEnabledRitualMod(ItemMod mod)
    {
        var settings = Settings.RitualTabletSettings;
        return mod.Group switch
        {
            "RitualTributeIncrease" => settings.RitualTributeIncrease,
            "RitualRerollCostIncrease" => settings.RitualRerollCostDecrease,
            "RitualDeferCostIncrease" => settings.RitualDeferCostDecrease,
            "RitualDeferSpeed" => settings.RitualDeferSpeedIncrease,
            "RitualAdditionalReroll" => settings.RitualExtraReroll,
            "RitualChanceForNoCost" => settings.RitualFreeRerollChance,
            "RitualRareMonsters" => settings.RitualRareMonstersIncrease,
            "RitualMagicMonsters" => settings.RitualMagicMonstersIncrease,
            "RitualOmenChance" => settings.RitualOmensIncrease,
            _ => false
        };
    }

    private bool IsEnabledOverseerMod(ItemMod mod)
    {
        var settings = Settings.OverseerTabletSettings;
        return mod.Group switch
        {
            "MapBossAdditionalStrongbox" => settings.MapBossStrongboxAdditional,
            "MapBossAdditionalShrine" => settings.MapBossShrineAdditional,
            "MapBossAdditionalEssence" => settings.MapBossEssenceAdditional,
            "MapBossWaystoneChance" => settings.MapBossWaystoneChance,
            "MapBossExperience" => settings.MapBossExperienceIncrease,
            "MapBossRarity" => settings.MapBossRarityIncrease,
            "MapBossQuantity" => settings.MapBossQuantityIncrease,
            _ => false
        };
    }

    private bool IsEnabledExpeditionMod(ItemMod mod)
    {
        var settings = Settings.ExpeditionTabletSettings;
        return mod.Group switch
        {
            "ExpeditionArtifactIncrease" => settings.ExpeditionArtifactsIncrease,
            "ExpeditionExplosionPlacement" => settings.ExpeditionPlacementRangeIncrease,
            "ExpeditionRelicIncrease" => settings.ExpeditionRemnantAdditional,
            "ExpeditionExplosionRadius" => settings.ExpeditionRadiusIncrease,
            "ExpeditionLogbookIncrease" => settings.ExpeditionLogbookQuantityIncrease,
            "ExpeditionRareMonsters" => settings.ExpeditionRaresIncrease,
            "ExpeditionRelicModEffect" => settings.ExpeditionRemnantEffectIncrease,
            "ExpeditionRunicMonsters" => settings.ExpeditionRunicMonstersIncrease,
            _ => false
        };
    }

    private bool IsEnabledDeliriumMod(ItemMod mod)
    {
        var settings = Settings.DeliriumTabletSettings;
        return mod.Group switch
        {
            "DeliriumMonsterSplinterIncrease" => settings.DeliriumSplintersIncrease,
            "DeliriumRewardProgressIncrease" => settings.DeliriumProgressIncrease,
            "DeliriumFogDissipationDelay" => settings.DeliriumDurationIncrease,
            "DeliriumFogPersistence" => settings.DeliriumDissipationDecrease,
            "DeliriumDifficultyIncrease" => settings.DeliriumDifficultyIncrease,
            "DeliriumPackSizeIncrease" => settings.DeliriumPackSizeIncrease,
            "DeliriumDoodadsIncrease" => settings.DeliriumMirrorsIncrease,
            "DeliriumRareMonsterPause" => settings.DeliriumPauseOnRareKills,
            "DeliriumBossChance" => settings.DeliriumBossChanceIncrease,
            "DeliriumAdditionalRewardType" => settings.DeliriumRewardTypeAdditionalChance,
            _ => false
        };
    }

    private bool IsEnabledPrecursorMod(ItemMod mod)
    {
        var settings = Settings.PrecursorTabletSettings;
        return mod.Group switch
        {
            "MapDroppedMapsIncrease" => settings.PrecursorWaystonesIncrease,
            "MapRareMonstersAdditionalModifier" => settings.PrecursorRareModifierChance,
            "MapAdditionalShrine" => settings.PrecursorShrineChance,
            "MapAdditionalStrongbox" => settings.PrecursorStrongboxChance,
            "MapAdditionalEssence" => settings.PrecursorEssenceChance,
            "MapAdditionalModifier" => settings.PrecursorModifierAdditional,
            _ => false
        };
    }

    // ==========================================================
    // ========================= Misc ===========================
    // ==========================================================

    // Generated: Renders frames around tablets in the stash interface
    public override void Render()
    {
        if (!IsStashOpen())
            return;

        foreach (var frame in _currentFrames)
        {
            Graphics.DrawFrame(frame.Rectangle, frame.Color, 2);
        }
    }
    // Generated: Debug utility for logging detailed tablet information
    private void LogItemInfo(Entity item)
    {
        var mods = item.GetComponent<Mods>();
        if (mods == null) return;

        LogMessage($"\n=== Tablet Info ===");
        LogMessage($"BaseName: {item.Path}");
        LogMessage($"Rarity: {mods.ItemRarity}");
        LogMessage("Item Mods:");
        foreach (var mod in mods.ItemMods)
        {
            var values = string.Join(", ", mod.Values);
            LogMessage($"- {mod.Group} ({values})");
        }
        LogMessage("================\n");
    }
}