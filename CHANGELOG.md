# Change Log
All notable changes will be tracked in this file. Most likely.

## 0.5.0.0
### Added
- Implement Prerequisites for class features, skill ranks, proficiency, race
- Favored Classes
- Skill Bonus Token for extra skill ranks
- Weapon Proficiencies for races now working

### Changed
- Removed SpecialAbility class and replaced with interfaces. Cleans up behavior and logic
- Eliminated some of the clunky interfaces and focused on the component system
- Classes, Races and Feats now implemented using a more consistent and flexible system of CharacterAttributes. Allows complex definition and keeps things well organized
- Damage and Energy Resistance is more properly implemented


### Known Issues
- Damage Resistance shows up weird in the abilities list view


## 0.4.0.0
### Added
- Bard Class
- Sorcerer Class
- Wizard Class

### Changed
- Fixed issues with spellcasting, specifically divine casters
- Redesigned the spellcasting implementation to be more universal and manageable
- Improved method of adding custom attacks, and in general working towards design of adding items to component bag versus monkeying around in classes that shouldn't be.

### Known Issues
- Draconic bloodline is doing some weird things
- Bonus spells can get selected before the character would receive them for free. Defeats the purpose
- Energy Resistance is all wrong

## 0.3.0.0
### Added
- Druid class is now supported (minus animal companions >^.^< )
- Add Spell Resistance statistic
- Multiple Attacks are awarded for high attack bonuses now
- Monk class is now supported
- Favorite Colors are available for characters and used in description generation

### Changed
- Levels were reusing abilities which caused weird bugs, now each level up will create new instances for each character
- Provides more flexibility in defining attacks
- Improved? Layout of information for character to make it easier to read
- Broke out rendering of character template information to make it easier to change
- Reworked traits to be more standard custom-implementation usage
- Feat Tokens are just components that are added and removed (No longer managed by list)
- Upgraded to .NET CORE 2.0
- Standardizing around language of "Select" over "Choose"
- Updated license headers in source code files
- Fixed bug that was duplicating the spellcasting for some classes


### Known Issues
- Animal Companions are not supported yet which makes the druid generation more limited than it should be

## 0.2.0.0
### Added
- Added 3d6 ability score generator
- Added point buy ability score generator
- Added ability to select generator via UI
- Added rogue talents
- Added rogue class
- Added criminal build
- Added cleric class
- Added domain abilities and spells
- Added masterwork armor and weapons
- Added concept of "shops" they are limited right now but room to grow 
- Added basic magic weapons and armor
- Added wands and potions
- Improved ability to generate descriptions
- Allows for cascading description templates to choose more complex descriptions
- Created common lexicon for story generation
- Introduced background story generation
- Stores selected values to make more robust story telling by allowing repeating words
- Generates description sentences for the characters eyes and hair
- Allows selecting from available main classes


### Changed
- Layout of HTML has changed. It will provide better flexibility in long run but 
maybe less readable right now
- Uses the current LTS 1.0.4 of .NET Core
- Broke out project into subprojects. This will allow better portability to
other libraries in the future
- Supports Visual Studio

### Known Issues
- Rage Powers and Rogue Talents don't modify stats and are incomplete
- Feats aren't fully implemented yet
- Descriptions are limited and look a little funky. Baseline is robust but will take work to get smoothed out
- In production it can get stuck and generates all the same names. Not able to repro yet.