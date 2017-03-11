[![Build Status](https://travis-ci.org/shortlegstudio/silverneedle-web.svg?branch=master)](https://travis-ci.org/shortlegstudio/silverneedle-web)
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

# Building

I do not have official build steps as of yet. I am working on Mac for this project
I would assume with dotnet code on visual studio would be relatively trivial to get
running.

This project is using dotnet: 1.0.0-preview2-1-003177 for builds and deployments.
The NET Core projects are all 1.1. Getting a templated ASP.NET MVC project running
should be enough configuration for this to be restored, built and run on a system.

# Roadmap

The [Trello board](https://trello.com/b/JkSfXB4F/silver-needle-npc-rpg-generator) 
contains the current list of items.
