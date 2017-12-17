<!---
 Copyright (c) 2017 Trevor Redfern
 
 This software is released under the MIT License.
 https://opensource.org/licenses/MIT
-->


## Seperation of concerns
* Characters general contain the rules and values need to create the calculations of character sheets
* The "Actions" namespace is for operations that either require making decision on how a character is generated. Or complex meshing of rules that require some extra massaging in order for the pieces to work right.

## Features, Abilities, Statistics

The future design idea is for there to be three levels / forms that components interact on the 
character sheet. Each dealing with a different problem and ability to customize.

*Features* represent the highest level and are the largest choices for characters. 
Race, Class, would be very high level forms, but even options like bloodlines for sorcerers, or what
domains are selected for a cleric would be features. They drive different paths for the character.
As characters level up, it's the features they have selected that usually determine what abilities
come about to a character. Features shouldn't "Do" anything to the character on their own. They
provide the skeleton for all the future options that will become available.

*Abilities* represent those things that the character can do. Feats are abilities. Abilities
might manipulate a stat. Or they might provide some other option. Channel Energy is an ability.
Sneak Attack is an ability. During the level up process, abilities might improve but they
shouldn't usually trigger new options themselves. There are likely some exceptions to that rule though.

*Statistics* represent the values in the end that would drive the game. They are formed
by calculating various pieces of information. These are more driven by other choices as
opposed to representing additional choices. Statistics are reactive to the choices made. 
Many statistics could be tied to a single ability. An attack has many stats that can be manipulated
by abilities like feats individually. Weapon Focus + Weapon Specialization and the attack bonus
and damage statistics for an attack change.

## Current Reality
Current state is not with this design in mind. This design has evolved out of working through
various attempts at implementing the classes. As all the essential classes are implemented,
the diversity of the rules as brought to the surface a way of getting most of the options 
implemented and highlighted the pain points in designing classes. In order to create
diverse characters with interesting stories, being able to meld the *Features* and *Abilities*
together with a backstory will require redesigning the way that options are implemented.

But this process has become more clear. Providing more flexible tools for configuration leads me 
to the following design choices:

* Values should be represented in the DATA files as much as possible
* The "abilities" mechanism built into classes is closing in on the ideal solution
    * But this should be converted into a general action steps. This would allow more control on the sequence
* Stats and Abilities need to be separated out more clearly
* Stats should become components that are easily accessible
* StatModifiers should be more easily defined by DATA files
* The DefenseStats, OffenseStats and AbilityScores are handy accessors for making sense of typical operations but they should not actually manage any of the values anymore. They should be basically filters on top of the component bag framework.
