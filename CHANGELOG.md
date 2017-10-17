# Change Log
All notable changes will be tracked in this file. Most likely.

## [Unreleased Changes]
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