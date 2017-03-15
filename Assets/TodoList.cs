/*
 * TODO: WaveManager -- Mostly done needs a lil bit more testing to besure
 * Finish new wave manager. Rewrite from scratch... Cause it's a mess now
 * 
 * SPECS:
 * - Must be able to hande more than one spawn point
 * - A spawn point a script containing delays and spawn amounts. When script is done it removes it self and says to wave spawner i'm done;
 * - Must keep track of live Enemies
 * - Must send event when no live Enemies remain
 * ----------------------------------------------------------------------------------------
 * TODO: PathManager --- DONE and expaned added now supports multipul ends and starts
 * 
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
 * TODO: PLACEMENT MANAGER-- Works good can place tower only on play field
 * add bounding box to playfield so you can't place tower outside the playfield
 * 
 * -----------------------------------------------------------------------------------------
 * TODO: CONTEXTMENU -- Able to extend need to add more menuse for stuff. Like building
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
 * - buttons that pop up. 
 * - a slight background
 * 
 * CHANGE:
 * - Change layout of the menu cause it looks like total shit right now. 
 * - Gone release pc only now so need to change UI to that maybe some context building?
 * 
 * 
 * ------------------------------------------------------------------------------------------
 * TODO: LASER TOWER
 * a tower that shoots a beam weapon that hits instantly
 * 
 * FUNCTIONS: 
 * - Line Renderer between tower and target. Should not remake a object every frame but updating instead.
 * - A damage pulse every so many miliseconds
 * 
 * VISUAL:
 * - A Tower that looks like it can shoot a blazor
 * - A lazer sprite that can be safely streched so a solid color would be best.
 * - Laser should slowly fade away after damage tick and pop back to max size after damage tick
 * 
 *----------------------------------------------------------------------------------------------
 * TODO: RESPRITE TOWER & ENEMIES
 * Create new sprites for the towers and enemies
 * 
 * TOWERS:
 * - Tower base should be round if they are gone rotate and squar if they are stationary. And they should have some square base. 
 * - Laser tower should have a laser ontop o.o go figure
 * - Cannon Tower will be dupped rocked tower from now on. AND IT WILL HAVE A ROCKED LAUNCHER AND ROCKETS!!
 * 
 * ENEMIES:
 * Not yet sure how to sprite those. Need to figure em out still
 * 
 * ----------------------------------------------------------------------------------------------
 * 
 * TODO: TOWER BUILDING FACE
 * Make the tower building face
 * 
 * FUNCTIONS: 
 * - Player must be able to place tower onto the field durring the building face
 * - Player must be able to upgrade sayed tower aswell
 */