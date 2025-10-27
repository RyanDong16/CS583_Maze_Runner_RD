# CS583_Maze_Runner_RD
Apocalypse Maze Escape

## Overview:
Apocalypse Maze Escape (AME) is a 2D bird-eye view, single player, arcade-style game combining puzzle-solving and endless runner mechanics into one new, unique experience. With influences from Pac-Man, Speed Runner, and maze puzzles, AME allows players to continuously traverse through the maze while evading the fallout of a nuclear explosion. The player needs to navigate through the maze from left to right, collect as many diamond gems as possible, and pick up special power-ups, before the oncoming explosion barrier chases after them from left to right. The maze structure will remain static, but as the player progresses further to the right, more of the maze will be revealed. The game only ends when the player is consumed by the barrier. Special power-up gems include phasing through the maze wall for 5 seconds, doubling gems for 10 seconds, and super speed for 7 seconds. The player can store all three power-ups, but can only store one of each at a time. The impending chase of the explosion adds to the urgency of quick thinking, leading to an exciting gaming experience.

## Controls:
- Keyboard and mouse controls only.
- Up, down, right, and left arrows are used to control the movement of the character.
- ‘1’, ‘2’, and ‘3’ buttons are used to activate power-ups: phasing, doubling gems, and super speed, respectively.
- The mouse right click is used to start the game and restart the game on the game-over screen.

## Art Assets:
- Background: Pixel art-styled city landscape image.
- Gems: Regular circle (diamond/ white), phasing circle (sapphire/  blue), doubling circle (ruby/ red), super speed circle (emerald/ green).
- Character: Pixel art-styled man image.
- Barrier: Long rectangle covered in pixel art-styled flames design (red & yellow). 
- Maze Outline: Black semi-thick lines.

## Audio Assets:
- Background: Semi-chaotic arcade-type music.
- Barrier: Fire noises intensify as the barrier gets closer to the character.
- Movement: Running and breathing noises.
- Game Over: Character’s scream of agony.

## Game Flow:
- As you progress longer through the maze, there will be fewer special power-ups available making it harder.
- The explosion barrier will start after a 5 seconds head start.
- The map design will start more simple and get more tricky as you traverse forward. 

## Challenges:
- Since the maze is static, the player is more likely to remember their path prior to the game restarting. Thus, the game can be repetitive and boring to the player.
- The idea of AME seems small and simple, but I’m still new to using Unity so there will be unforeseen problems along the way.
- Having all the wanted features work together as designed can cause other features to not work properly.

# Changes to One-Pager Plan Implementation

## Overview:
- The game ends only if you reach the rescue helicopter at the end of the maze (victory) or get consumed by the explosion barrier (defeat).
- Instead of doubling ruby gems for 10 seconds, the ruby gems are counted as 50 points each.

## Controls:
- Super speed gems are now gold gems that can be activated by pressing ‘1’ lasting for 8 seconds.
- Pressing ‘1’, ‘2’, ‘3’ keys activates the powerups: super speed, phasing, and shrinking respectively.
- Clicking on the start button in the game menu starts the game. Restarting the game happens when clicking on the retry button which takes you back to the game menu screen.

## Art Assets:
- Diamond, Ruby, Gold, Emerald, Sapphire gems represent regular 10 points, extra 50 points, super speed, phasing/ ghost, and shrinking respectively.
- The maze outlines are colored semi-transparent white and they vary in thickness.
- Detonator added to art assets to start off the chasing barrier.
- Menu screen and victory text added.

## Audio Assets:
- Removed background music, sound effects for character movements.
- Added different sound effects for collecting each different type of gem, explosion sound added once detonator hits the ground, helicopter’s engine turns on with victory.

## Game Flow:
- The explosion barrier starts after 2.5 seconds head start instead of 5 seconds.
- The explosion barrier moves faster as you go further into the maze.

## Challenges:
- 
