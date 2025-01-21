using ExileCore2.Shared.Interfaces;
using ExileCore2.Shared.Nodes;
using ExileCore2.Shared.Attributes;
using System.Drawing;

namespace Tablets;

public class TabletsSettings : ISettings
{
    public ToggleNode DrawNormalTablet { get; set; } = new ToggleNode(true);
    public ToggleNode DrawTwoModTablets { get; set; } = new ToggleNode(true);
    public ToggleNode HideBadTablets { get; set; } = new ToggleNode(true);
    public ToggleNode DrawLowMapsInRangeTablets { get; set; } = new ToggleNode(true);
    public RangeNode<int> MapsInRange { get; set; } = new (1, 3, 10);
    public CommonPrefixModsSettings CommonPrefixModsSettings { get; set; } = new CommonPrefixModsSettings();
    public BreachTabletSettings BreachTabletSettings { get; set; } = new BreachTabletSettings();
    public RitualTabletSettings RitualTabletSettings { get; set; } = new RitualTabletSettings();
    public OverseerTabletSettings OverseerTabletSettings { get; set; } = new OverseerTabletSettings();
    public ExpeditionTabletSettings ExpeditionTabletSettings { get; set; } = new ExpeditionTabletSettings();
    public DeliriumTabletSettings DeliriumTabletSettings { get; set; } = new DeliriumTabletSettings();
    public PrecursorTabletSettings PrecursorTabletSettings { get; set; } = new PrecursorTabletSettings();
    public BorderRenderSettings BorderRenderSettings { get; set; } = new BorderRenderSettings();
    public ToggleNode Enable { get; set; } = new ToggleNode(false);
}

[Submenu(CollapsedByDefault = false)]
public class BorderRenderSettings
{
    public ColorNode NormalBorderColor { get; set; } = new ColorNode(Color.White);
    public ColorNode TwoModBorderColor { get; set; } = new ColorNode(Color.Blue);
    public ColorNode BadTabletBorderColor { get; set; } = new ColorNode(Color.Red);
    public ColorNode ReadyToGoBorderColor { get; set; } = new ColorNode(Color.Green);
    public ColorNode LowMapsInRangeBorderColor { get; set; } = new ColorNode(Color.Gray);
}

[Submenu(CollapsedByDefault = true)]
public class CommonPrefixModsSettings
{
    public EmptyNode CommonPrefixSpacer { get; set; } = new EmptyNode();
    [Menu("(10-20)% increased Quantity of Items found in your Maps")]
    public ToggleNode MapDroppedItemQuantityIncrease { get; set; } = new ToggleNode(false);
    [Menu("(10-20)% increased Rarity of Items found in your Maps")]
    public ToggleNode MapDroppedItemRarityIncrease { get; set; } = new ToggleNode(false);
    [Menu("(3-8)% increased Pack Size in your Maps")]
    public ToggleNode MapPackSizeIncrease { get; set; } = new ToggleNode(false);
    [Menu("(15-25)% increased Magic Monsters in your Maps")]
    public ToggleNode MapMagicPackIncrease { get; set; } = new ToggleNode(false);
    [Menu("(10-15)% increased Rare Monsters in your Maps")]
    public ToggleNode MapRarePackIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Gold found in your Maps")]
    public ToggleNode MapDroppedGoldIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Experience gain in your Maps")]
    public ToggleNode MapExperienceGainIncrease { get; set; } = new ToggleNode(false);
}

public class EmptyNode { }

[Submenu(CollapsedByDefault = true)]
public class BreachTabletSettings
{
    public ToggleNode EnableBreachTablet { get; set; } = new ToggleNode(false); 
    [Menu("Breaches spawn (15-25)% increased Magic Monsters")]
    public ToggleNode BreachMagicMonsterIncrease { get; set; } = new ToggleNode(false);
    [Menu("Breaches spawn an additional Rare Monster")]
    public ToggleNode BreachRareMonsterIncrease { get; set; } = new ToggleNode(false);
    [Menu("Breaches have (5-10)% increased Monster density")]
    public ToggleNode BreachDensityIncrease { get; set; } = new ToggleNode(false);
    [Menu("Breaches open and close (5-10)% faster")]
    public ToggleNode BreachSpeedIncrease { get; set; } = new ToggleNode(false);
    [Menu("Breaches contain 1 additional Clasped Hand")]
    public ToggleNode BreachChestAdditional { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Quantity of Breach Splinters")]
    public ToggleNode BreachMonsterSplinterIncrease { get; set; } = new ToggleNode(false);
    [Menu("(2-4)% chance to contain three additional Breaches")]
    public ToggleNode Breach3AdditionalChance { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% chance to contain an additional Breach")]
    public ToggleNode BreachAdditionalChance { get; set; } = new ToggleNode(false);
}

[Submenu(CollapsedByDefault = true)]
public class RitualTabletSettings
{
    public ToggleNode EnableRitualTablet { get; set; } = new ToggleNode(false);
    [Menu("Monsters grant (5-10)% increased Tribute")]
    public ToggleNode RitualTributeIncrease { get; set; } = new ToggleNode(false);
    [Menu("Rerolling costs (10-15)% reduced Tribute")]
    public ToggleNode RitualRerollCostDecrease { get; set; } = new ToggleNode(false);
    [Menu("Deferring costs (10-15)% reduced Tribute")]
    public ToggleNode RitualDeferCostDecrease { get; set; } = new ToggleNode(false);
    [Menu("Deferred Favours reappear (10-15)% sooner")]
    public ToggleNode RitualDeferSpeedIncrease { get; set; } = new ToggleNode(false);
    [Menu("Allow rerolling Favours an additional time")]
    public ToggleNode RitualExtraReroll { get; set; } = new ToggleNode(false);
    [Menu("(1-3)% chance to reroll for no Tribute")]
    public ToggleNode RitualFreeRerollChance { get; set; } = new ToggleNode(false);
    [Menu("(10-15)% increased chance for Rare Monsters")]
    public ToggleNode RitualRareMonstersIncrease { get; set; } = new ToggleNode(false);
    [Menu("(15-25)% increased chance for Magic Monsters")]
    public ToggleNode RitualMagicMonstersIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased chance to contain Omens")]
    public ToggleNode RitualOmensIncrease { get; set; } = new ToggleNode(false);
}

[Submenu(CollapsedByDefault = true)]
public class OverseerTabletSettings
{
    public ToggleNode EnableOverseerTablet { get; set; } = new ToggleNode(false);
    [Menu("Areas with Map Bosses contain an additional Strongbox")]
    public ToggleNode MapBossStrongboxAdditional { get; set; } = new ToggleNode(false);
    [Menu("Areas with Map Bosses contain an additional Shrine")]
    public ToggleNode MapBossShrineAdditional { get; set; } = new ToggleNode(false);
    [Menu("Areas with Map Bosses contain an additional Essence")]
    public ToggleNode MapBossEssenceAdditional { get; set; } = new ToggleNode(false);
    [Menu("Map Boss has +(10-20)% chance to drop a Waystone")]
    public ToggleNode MapBossWaystoneChance { get; set; } = new ToggleNode(false);
    [Menu("Map Bosses grant (20-40)% increased Experience")]
    public ToggleNode MapBossExperienceIncrease { get; set; } = new ToggleNode(false);
    [Menu("(15-25)% increased Rarity from Map Bosses")]
    public ToggleNode MapBossRarityIncrease { get; set; } = new ToggleNode(false);
    [Menu("(10-15)% increased Quantity from Map Bosses")]
    public ToggleNode MapBossQuantityIncrease { get; set; } = new ToggleNode(false);
}

[Submenu(CollapsedByDefault = true)]
public class ExpeditionTabletSettings
{
    public ToggleNode EnableExpeditionTablet { get; set; } = new ToggleNode(false);
    [Menu("")] 
    public EmptyNode ExpeditionSpacer { get; set; } = new EmptyNode();
    [Menu("(5-10)% increased quantity of Artifacts")]
    public ToggleNode ExpeditionArtifactsIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Explosive Placement Range")]
    public ToggleNode ExpeditionPlacementRangeIncrease { get; set; } = new ToggleNode(false);
    [Menu("Expeditions have +1 Remnant")]
    public ToggleNode ExpeditionRemnantAdditional { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Explosive Radius")]
    public ToggleNode ExpeditionRadiusIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Logbook Quantity")]
    public ToggleNode ExpeditionLogbookQuantityIncrease { get; set; } = new ToggleNode(false);
    [Menu("(10-15)% increased Rare Expedition Monsters")]
    public ToggleNode ExpeditionRaresIncrease { get; set; } = new ToggleNode(false);
    [Menu("(3-6)% increased Effect of Remnants")]
    public ToggleNode ExpeditionRemnantEffectIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Runic Monster Markers")]
    public ToggleNode ExpeditionRunicMonstersIncrease { get; set; } = new ToggleNode(false);
}

[Submenu(CollapsedByDefault = true)]
public class DeliriumTabletSettings
{
    public ToggleNode EnableDeliriumTablet { get; set; } = new ToggleNode(false);
    [Menu("")] 
    public EmptyNode DeliriumSpacer { get; set; } = new EmptyNode();
    [Menu("(5-10)% increased Simulacrum Splinter stack size")]
    public ToggleNode DeliriumSplintersIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Reward Progress")]
    public ToggleNode DeliriumProgressIncrease { get; set; } = new ToggleNode(false);
    [Menu("Fog lasts (3-6) additional seconds")]
    public ToggleNode DeliriumDurationIncrease { get; set; } = new ToggleNode(false);
    [Menu("Fog dissipates (10-15)% slower")]
    public ToggleNode DeliriumDissipationDecrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% faster difficulty increase")]
    public ToggleNode DeliriumDifficultyIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Pack Size")]
    public ToggleNode DeliriumPackSizeIncrease { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% increased Fracturing Mirrors")]
    public ToggleNode DeliriumMirrorsIncrease { get; set; } = new ToggleNode(false);
    [Menu("Timer pauses for 2 seconds on Rare kills")]
    public ToggleNode DeliriumPauseOnRareKills { get; set; } = new ToggleNode(false);
    [Menu("(5-10)% more likely for Unique Bosses")]
    public ToggleNode DeliriumBossChanceIncrease { get; set; } = new ToggleNode(false);
    [Menu("(3-8)% chance for additional Reward type")]
    public ToggleNode DeliriumRewardTypeAdditionalChance { get; set; } = new ToggleNode(false);
}

[Submenu(CollapsedByDefault = true)]
public class PrecursorTabletSettings
{
    public ToggleNode EnablePrecursorTablet { get; set; } = new ToggleNode(false);
    [Menu("")] 
    public EmptyNode PrecursorSpacer { get; set; } = new EmptyNode();
    [Menu("(10-20)% increased Quantity of Waystones")]
    public ToggleNode PrecursorWaystonesIncrease { get; set; } = new ToggleNode(false);
    [Menu("(20-30)% chance for additional Rare Modifier")]
    public ToggleNode PrecursorRareModifierChance { get; set; } = new ToggleNode(false);
    [Menu("+(10-20)% chance to contain a Shrine")]
    public ToggleNode PrecursorShrineChance { get; set; } = new ToggleNode(false);
    [Menu("+(10-20)% chance to contain a Strongbox")]
    public ToggleNode PrecursorStrongboxChance { get; set; } = new ToggleNode(false);
    [Menu("+(10-20)% chance to contain an Essence")]
    public ToggleNode PrecursorEssenceChance { get; set; } = new ToggleNode(false);
    [Menu("1 additional random Modifier")]
    public ToggleNode PrecursorModifierAdditional { get; set; } = new ToggleNode(false);
}
