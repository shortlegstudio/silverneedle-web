# Blog Post Ideas

## Refactoring
    1. Perform subclassing by duplicating and merging, not by attempting to predict 
        what the hierarchy will be: Example in SpellCasting, also in Inventory
    1. When a future class hierarchy is identified and needs to be redone, build
        the new model first independent of cross dependency of the item being 
        merged in THEN rebuild the old implementation. Trying to mix on top results
        in errors and lots of breaking code
## Boundaries
    1. Events are useful for passing boundaries but poor for implementing patterns
        within a system.
    1. Examples of boundaries: UI, Data Stores, Game Engines, Libraries, ...
