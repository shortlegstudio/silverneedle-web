# Character Generator
This is an NPC generator that utilizes various strategies and weightings in 
order to create characters that might make sense. These are currently loosely 
based on 3.5 style classes but whether the specific implementation is 100% 
accurate is not determined. Things like prerequisites could be off or changed 
in the future to make things more interesting.

# How it works
There are many data files that determine various aspects of classes, traits, 
and races. These are form the basis for the rules to generate the classes. 
To make that all work well together and provide reasonable creations there
is a CharacterBuildStrategy that determines the weightings to provide to 
different options. 

For example, the TANK build option has the following:

```yaml
  classes:
    - name: fighter
      weight: 10
    - name: ranger
      weight: 7
    - name: paladin
      weight: 10
    - name: barbarian
      weight: 10
```

This essential gives top weightings to fighter, paladin, and barbarians. Rangers
are possible but they are less likely. Whether this makes sense for a Tank is
completely beside the point for now. In the future as more is implemented
more time will be spent tweaking specific weightings to generate better NPCs.

This weighting/strategy mechanism can be applied to races, feats, ability scores
and basically anywhere to provide more customization.

# What to do?
This is a pretty limited implementation at this point and there are many 
factors not properly tracked. 

* Class level abilities are not properly implemented
* More interesting characteristics: Likes, Dislikes, Quirks, etc...
* Multiclassing possibilities
* Generating multiple NPCs depending on a place. For example, create a busy
tavern of NPCs
