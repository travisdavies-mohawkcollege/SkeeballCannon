# Game Design Document - Simcade Proto - Team 2 
 Blair Haines, Travis Davies, Brendan McNally, Aemon Simon
# Game Title: CannonBallCarnage
**Roles:**

Blair: Research/Documentation, Programming Assistant

Brendan & Travis: Programming Leads

Aemon: Assets 


# Concept

CannonBallCarnage is a simcade style physics game where the player fires a cannonball from a mounted cannon into destructible structures. Each shot requires the player to judge angle, compensate for wind, and make precise timing choices. The goal is simple... break as much as possible within a fixed shot limit and aim for the highest score.
The experience aims to feel quick, punchy, and satisfying, emphasizing destruction, slightly exaggerated physics, and replayable high score chasing.

**Objective Statement:**
To deliver a fast, fun, and destructive simcade experience where players use physics, timing, and fine motor control to demolish structures and maximize their score within a limited number of shots.


**Design Rationale:**
This prototype focuses on arcade immediacy combined with light simulation elements.
Short rounds, intuitive controls, exaggerated destruction, and skill improvement create a tight gameplay loop ideal for rapid prototyping.
The game encourages experimentation through varying wind conditions and destructible setups, inviting players to refine strategy and aim for increasingly optimized runs.


## Core Mechanics:
**Aiming:**
Players rotate the cannon horizontally and vertically to set the launch trajectory.

**Firing:**
Once aimed, the cannon launches a cannonball using a fixed power value (with potential for variable power in future versions).

**Wind Influence:**
Randomly generated wind modifies the cannonball’s flight path, requiring player adaptation and skillful prediction.

**Destruction Interaction:**
Cannonballs collide with destructible bricks and structures, awarding points for each impact and encouraging strategic targeting.

**Match Structure:**

The player is given 7 shots total.

Each shot launches a cannonball that can collide with multiple bricks before stopping.

The round “ends” after all shots are used.

Score is Displayed in the UI.

Players are encouraged to replay repeatedly to improve high scores.

**Scoring:**
Each brick hit by the cannonball awards points.

## The Future of CannonBallCarnage ##
CannonBallCarnage is a fun simple arcade style game that could be involved into level based system with different score requirements for each level. A simple system, akin to a game such as ‘Angry Birds’, would lead to a fun and replayable game loop that rewards replaying levels to get higher scores. This concept could be kept even more fun with the introduction of new cannonballs, enemies or even boss fight levels. Advanced levels could even include an on-rail shooter section, where the player moves around the level.

## Implemented Feedback ##
**Elijah** - "Maybe pull out the camera further or move/rotate it a little so the player can see where they're shooting more accurately.
Have a bit of an indicator to the strength/intensity of wind"

**Brandon C** - "...a toggle for flipping the Y axis of the camera would be greatly appreciated lol"

**Jazz** - "I think increasing the time between each shot would help as the rapid firing, while fun, seems a little much"

**Karndeep** - "Cannons don't normally have an auto fire mode"

**Micheal** - "Limit cannon balls shot at a time."

**Wyatt** - "Maybe there is a way to show how much wind resistence there is on screen?"

## How Our Concept Changed ##
The original concept for CannonBallCarnage was SkeeBall Cannon, with a focus on accurate shots making it into a hole or cup. After placing some rigidbody objects within the scene to show how the cannon ball physics were working, we realized something: Blasting rigidbodys around was far more entertaining than attempting to score into the cups. Some of our feedback recommended this approach as well, commenting on the fun of destroying things with the cannon. This design switch allowed us to still focus on the main concept, the cannon, which gave us room to explore more entertaining game loops without starting from scratch.

## GDD ##

https://www.youtube.com/watch?v=VCLjuHMhCy0 

!(/images/GDDimage.png)

## Metric References/Research ##

Characteristics of a Projectiles Trajectory
https://www.physicsclassroom.com/class/vectors/Lesson-2/Characteristics-of-a-Projectile-s-Trajectory

How to make a simple physics based cannon in Unity/C#
https://medium.com/geekculture/how-to-make-a-simple-physics-based-cannon-in-unity-c-219a6a21d3d6 

Projectile Motion Example - Cannon
https://www.youtube.com/watch?v=YRJKzUAn5MI 

Trig Help: The Human Cannonball 1
https://www.youtube.com/watch?v=2tDAi6gy8DI

## Assets and References ##

Cannon and cannonball were created by Aemon.

Cannon Launch Audio Clip:
https://www.youtube.com/watch?v=2tDAi6gy8DI 

Low Poly Brick Model:
https://sketchfab.com/3d-models/low-poly-clay-brick-5bc1e9e3fe2244b7983e306285f65ec8 