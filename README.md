# Unity_Game
 Alpha1.0
---------------------------------------------------------------------------
Changes
---------------------------------------------------------------------------
Added 2 scenes, lobby and dungeon. Added transition from Lobby to Dungeon.
Added camera that follows player.
Added save state and load state. Pending improvements.
Added player logic, movement, +various attributes that will be worked on in the future.
Added enemy logic, enemy follows player for a certain distance and when player leaves threshold enemy goes back to initial position. 
Added partial chest logic, throws debug message when player interacts and changes sprite.
Added primordial gamemanager that keeps track of different values. Additional work needed.
Added sprite animations for
·Protagonist
      Idle
      Walking/Running
·Enemy(skeleton)
      Idle
---------------------------------------------------------------------------
To fix
---------------------------------------------------------------------------
Enemy logic does not work in Dungeon scene for some unknown reason.
Popup messages to indicate game events not working. 
Panel that encapsulates text object for game events has opacity and for an unknown reason applies some sort of gray filter to game. 
Sprite mask to show player when behind wall also appears when player is in front of wall. Needs fix.
---------------------------------------------------------------------------
Improve
---------------------------------------------------------------------------
Skeleton animations too primitive, required further improvements.
Flesh out shop, "Potion seller" entity needs animations and has to support interactions.
Expand on dungeon levels, add more enemies, bosses and traps. More decor. 
Add object drops. 
GameManager needs to be improved upon. 
Add UI.
Polish game mechanics such as exp and combat.
Add procedural generation if possible.




