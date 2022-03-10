# ICS167 Group 9

rules:

This game currently supports multiplayer between two players, 1 player against 1 AI. Before a player's turn, they will need to press the "I'm Ready!" button, 
allowing them to move their units and let the units take actions. 
At the end of a player's turn, they can click the "Next Turn" button, allowing the other player to come on the computer and start their turn. 
Between some turns, various AI units will also move. 
 
During a player's turn, they can click on their units to select them, which will both highlight that unit to let you know it is selected, and create a magenta circle 
around them which indicates the distance that unit can move during its turn. 
Right clicking the ground anywhere within that circle will make that unit move around, but only within its movement range. 
To take an action, right click a resource or other unit depending on what type of unit that is taking the action 
(ex. Bowmen can only take action on enemy units to attack them). 
Any target needs to be within the range to let unit actually take action, or the unit will just stand there (the unit can only attack enemy unit if that enemy unit is
in the unit's attacking range).
Only one action can be taken per turn (on any given unit), when a unit has taken its action, it will not be able to move that turn. 
Also, when a unit has taken its action, the attacking/collecting/healing range indicator ring (depending on what type of unit that is) will disappear. 
This mechanism also works to tell whether a certain unit has action available.
An unit's range ring is indicated in the following color:
Red ring represents a unit's attacking range.
Green ring represents a unit's collecting range.
Orange ring represents a unit's healing range.

If you collect enough resources, you can spawn units, consuming the prompted amount of resources. 
(There are in total 5 different resources in the world: Food, Wood, Stone, Gold, Sliver, if you are not sure what resource a certain object is,
you can hover your mouse over that object, and that type of resource will be highlighted in the left bottom corner.)
These newly spawned units can take action on the next turn and they become selectable on the next turn too.

If you are not sure what unit type a certain unit is, you can select that unit, and refer to the information panel which is located in the bottom of the screen.
The information panel also shows a certain unit's specific health amount.

Use the arrow keys or WASD to move the camera. Pressing space will lock the camera on the selected unit and will force the camera to follow that unit. You can press space again to regain free camera control. 
The state of this is represented through the icon in the bottom right of the screen, with the camera icon having a red slash through it when the camera is following a unit.
Left click on a specific unit to select that unit.
With a unit selected, right click on the ground to move the selected unit.
With a unit selected, right click on the enemy to order the selected unit to attack the enemy.
With a unit selected, right click on the resource to gather resources.
Gather resources to build troops.
Left click buttons in the right hand corner to spawn units, must have the required resources.
To swap turns, click the button in the top left corner.
Open settings in the top right corner.  Use the slider to adjust camera move speed, zoom speed and also the volume of the background music.

