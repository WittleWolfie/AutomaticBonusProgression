# Automatic Bonus Progression

An item rebalance mod for Pathfinder: Wrath of the Righteous based on Pathfinder's [Automatic Bonus Progression](https://www.d20pfsrd.com/gamemastering/other-rules/unchained-rules/automatic-bonus-progression/) ruleset.

### This mod creates a save dependency. Once you save with this mod enabled it must remain enabled or you will lose access to the save.

## Problems or Suggestions

File a [bug](https://github.com/WittleWolfie/AutomaticBonusProgression/issues/new?template=bug_report.md&title=%5BBUG%5D) or [feature request](https://github.com/WittleWolfie/AutomaticBonusProgression/issues/new?template=feature_request.md&title=%5BFeature%5D). You can also reach me (@WittleWolfie) on [Discord](https://discord.com/invite/owlcat) in the #mod-user-general channel.

## Installation

1. Download and install [Unity Mod Manager](https://github.com/newman55/unity-mod-manager) (min v0.23)
2. Install [ModFinder](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder) and use it to search for Character Options+
3. Click "Install"
    * If you don't have [TabletopTweaks-Core](https://github.com/Vek17/TabletopTweaks-Core) and [ModMenu](https://github.com/WittleWolfie/ModMenu) already, install them using ModFinder

I also recommend installing [TabletopTweaks-Base](https://github.com/Vek17/TabletopTweaks-Base) and [TabletopTweaks-Reworks](https://github.com/Vek17/TabletopTweaks-Reworks). They are not required but I developed the mod using them and there may bugs if you use it without them.

If you do not want to use ModFinder, download [AutomaticBonusProgression.*.zip](https://github.com/WittleWolfie/AutomaticBonusProgression/releases/latest) and install it using Unity Mod Manager. Make sure to install [TabletopTweaks-Core](https://github.com/Vek17/TabletopTweaks-Core) and [ModMenu](https://github.com/WittleWolfie/ModMenu) as well.

### [Settings are NOT in UMM, they use Mod Menu](#mod-options) 

## How it works

Automatic Bonus Progression automatically grants stat bonuses usually provided by items which no longer provide them. As a result you are free to equip items based on their special abilities and effects.

*Note: Items in game still list these bonuses in the UI, but they do not apply.*

* **Armor Attunement**: *+X Enhancement to Armor*
    * +1 Chainmail
* **Shield Attunement**: *+X Enhancement to Shield*
    * Armor attunement is lower when a shield is equipped
* **Weapon Attunement**: *+X Enhancement to Weapon*
    * +1 Greataxe, Amulet of Mighty Fists +1
* **Off-Hand Attunement**: *+X Enhancement to Off-Hand Weapon/Secondary Natural Attacks*
    * Weapon attunement is lower when dual wielding or using secondary natural attacks
* **Resistance**: *+X Resistance to Saving Throws*
    * Cloak of Resistance +1
* **Deflection**: *+X Deflection to AC*
    * Ring of Deflection +1
* **Toughness**: *+X Enhancement to Natural Armor*
    * e.g. Amulet of Natural Armor +1
* **Mental Prowess**: *+X Enhancement to Mental Ability Score*
    * Headband of Wisdom +2
* **Physical Prowess**: *+X Enhancement to Physical Ability Score*
    * Belt of Strength +2

| Level  | Bonus |
| :----: | ------------- |
| 3      | +1 Resistance |
| 4      | +1 Armor<br>+1 Weapon |
| 5      | +1 Deflection |
| 6      | +2 Mental Prowess |
| 7      | +2 Physical Prowess |
| 8      | +2 Resistance<br>+1 Shield<br>+1 Off-Hand<br>+1 Toughness |
| 9      | +2 Armor<br>+2 Weapon |
| 10     | +3 Resistance<br>+2 Deflection |
| 11     | +4 or +2/+2 Mental Prowess |
| 12     | +4 or +2/+2 Physical Prowess |
| 13     | +4 Resistance<br>+2 Toughness<br>+4/+2 Mental Prowess<br>+4/+2 Physical Prowess |
| 14     | +5 Resistance<br>+3 Armor, +2 Shield<br>+3 Weapon, +3 Off-Hand |
| 15     | +4 Armor, +3 Shield<br>+4 Weapon, +3 Off-Hand<br>+6/+2 or +4/+4 Mental Prowess |
| 16     | +3 Deflection<br>+3 Toughness<br>+6/+2 or +4/+4 Physical Prowess  |
| 17     | +4 Deflection<br>+4 Tougness<br>+5 Armor<br>+5 Weapon<br>+6/+2/+2 or +4/+4/+2 Mental Prowess<br>+6/+2/+2 or +4/+4/+2 Physical Prowess |
| 18     | +5 Deflection<br>+5 Tougness<br>+6/+4/+2 or +4/+4/+4 Mental Prowess<br>+6/+4/+2 or +4/+4/+4 Physical Prowess |
| 19     | 3 Legendary Gifts  |
| 20     | 5 Legendary Gifts  |

Mental Prowess and Physical Prowess allow you to select which ability scores to boost. The bonuses you receive are further customizable using Legendary Gifts, detailed below.

Each Legendary Gift allows you to select a different special bonus. In addition to the Legendary Gifts you receive at level 19 and 20, you receive 2 Legendary Gifts at every Mythic Level for a total of 28.

### Legendary Ability

Gain a +1 inherent bonus to any ability score. You can select this legendary gift multiple times, and it stacks up to +5 in any one ability score.

### Legendary Enchantments

Allows you to apply enchantments to your equipment (Armor, Shield, Weapon, Off-Hand). Each enchantment has an Enhancement Cost and you can apply enchantments with a total cost equal to your rank in the corresponding feature (max 5).

#### Armor Enchantments

TODO: Add list

#### Weapon Enchantments

TODO: Add list

### Legendary Prowess

This allows you to increase the bonuses granted by Physical Prowess or Mental Prowess. You cannot increase the bonus to any single ability score above +6.

### Legendary Shieldmaster

Removes the armor enhancement bonus penalty applied when using a shield.

### Legendary Twin Weapons

Removes the weapon enhancement bonus penalty applied when using off-hand or secondary weapons.

## Base Game Changes

* Spell Resistance Enchantment
    * Increased from `13 / 15 / 17 /19` to `13 / 16 / 19 / 22`
* Invulnerability Enchantment
    * Increased from `5/magic` to `10/magic`
* Gold Acquisition Reduced
    * Reduced by 30% since buying magic items is less relevant

## Reporting Problems

### First, go to Settings > Mods > Automatic Bonus Progression and turn on "Enable Detailed Logs"

**Follow this guide: [So you want to report a WotR mod bug?](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/ModBugReports.md)** Ideally include a save file.

1. Use [ModFinder](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder) to install [Mewsifer Console](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole) and Mewsifer Console Menu
2. Reproduce the issue in-game
3. Open Settings > Mods > Mewsifer Console and select "Mod Bug Report"
    * ![Mewsifer Console Menu screenshot](https://github.com/WittleWolfie/CharacterOptionsPlus/blob/main/screenshots/bug_report.png)
4. File an [issue on GitHub](https://github.com/WittleWolfie/CharacterOptionsPlus/issues/new?template=bug_report.md&title=%5BBUG%5D) and attach the generated bug report
5. Include your save file in the bug report
    * Saves are stored in `%UserProfile%\AppData\LocalLow\Owlcat Games\Pathfinder Wrath Of The Righteous\Saved Games`
    
### Mod Options

#### TODO: Add screenshot

This mod uses [Mod Menu](https://github.com/WittleWolfie/ModMenu), settings are in the game settings: Settings > Mods > Automatic Bonus Progression. They are not available in UMM.

## Acknowledgements

* Bubbles (factsubio) for [BubblePrints](https://github.com/factubsio/BubblePrints), saving me from going mad.
* The modding community on [Discord](https://discord.com/invite/owlcat), an invaluable and supportive resource for help modding.
* All the Owlcat modders who came before me, wrote documents, and open sourced their code.

## Interested in modding?

* Check out the [OwlcatModdingWiki](https://github.com/WittleWolfie/OwlcatModdingWiki/wiki).
* Join us on [Discord](https://discord.com/invite/owlcat).