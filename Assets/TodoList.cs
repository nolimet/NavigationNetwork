/*
 * TODO: WaveManager
 * Finish new wave manager. Rewrite from scratch... Cause it's a mess now
 * 
 * SPECS:
 * - Must be able to hande more than one spawn point
 * - A spawn point a script containing delays and spawn amounts. When script is done it removes it self and says to wave spawner i'm done;
 * - Must keep track of live Enemies
 * - Must send event when no live Enemies remain
 * ----------------------------------------------------------------------------------------
 * TODO: PathManager
 * Add support for more than one route to PathManager.
 * 
 * CHANGE:
 * Change who nodes are connection. Instead of connecting them in order spawned connect them according to a array system.
 * 
 * BUILDUP:
 * - Node Positions
 * - Node Type : Spawner, Endpoint, Regular
 * - Node Connections
 * 
 * WORKORDER:
 * - SpawnNodes and SetTypes
 * - Connect the nodes according the connection Array
 * 
 * CONNECTION ARRAY:
 * - Node A : int (node's location in the position array)
 * - Node B : int
 * A connects to B. Objects will move from A to B in a one way trafic
 * 
 * VISUALS: 
 * Render path connections using line Renders
 * -----------------------------------------------------------------------------------------
 * TODO: PlayField
 * Should the size of the playfield be a fixed size or should game have ablity to zoom in and out?
 * if none fixed size
 * Write way to move camera about and keep it with in a bounded box.
 * add background to show field size. Make game default background color a dark color so it's clear that a empty void.
 * 
 * -----------------------------------------------------------------------------------------
 * TODO: PLACEMENT MANAGER
 * add bounding box to playfield so you can't place tower outside the playfield
 * 
 * -----------------------------------------------------------------------------------------
 * TODO: CONTEXTMENU
 * add right fliped menu so menu is not out of view when near edge of viewfield...
 * ------------------------------------------------------------------------------------------
 * TODO: GAME UI
 * add game UI so player has information and knows who far he has progressed in a level
 * 
 * FUNCTIONS:
 * - Button to start wave
 * - Button to pause game
 * 
 * VISUAL:
 * -
 */