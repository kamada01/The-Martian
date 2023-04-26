# The-Martian

improvments
- Inventory slots border + color
- HUD child componenets script linkages
- Health bar size? Font? Style
- 'Minotaur ' AnimationEvent has no function name specified! -> Need fix 
      -> Not sure what happens but the prefab should work, will investigate later (Ed 4/25 16:55)

#Spawn Usage
- spawnU for up, spawnD for down, SpawnR for R, SpawnL for L
- Must attach Enemy and Player
- Enemy_two is for the wolf pack only
- recommend setting:
- SpawnU: Enemy = Alpha; Enemy_two = Beta; Spawn Time = 5
- SpawnD: Emeny = Minotaur; Spawn Time = 6
- SpawnL and SpawnR: Enemy = BrainMole; Spawn Time = 2


# Summery for monsters:
1. Minotaur:
- Low speed
- High attack
- High HP
- Poor vision
- Mid attack range

2. Brainmole:
- High speed
- Low attack
- Low HP
- Strong vision
- Low attack range

3. Alpha(werewolf):
- average speed
- low attack
- mid HP
- Poor Vision
- Low Attack range
- can summon its pack (Beta)

4. Beta:
- average speed
- low attack
- Low HP
- Poor Vision
- Low Attack range

