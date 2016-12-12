/*
 * TODO:
 * Finish new wave manager. Rewrite from scratch... Cause it's a mess now
 * Specifications
 * - Must be able to hande more than one spawn point
 * - A spawn point a script containing delays and spawn amounts. When script is done it removes it self and says to wave spawner i'm done;
 * - Must keep track of live Enemies
 * - Must send event when no live Enemies remain
 * - 
 * TODO:
 * Add support for more than one node to PathManager.
 * 
 * CHANGE:
 * Change who nodes are connection. Instead of connecting them in order spawned connect them according to a array system.
 * BUILDUP:
 * - Node Positions
 * - Node Type : Spawner, Endpoint, Regular
 * - Node Connections
 * WORKORDER
 * - SpawnNodes and SetTypes
 * - Connect the nodes according the connection Array
 * 
 * CONNECTION ARRAY:
 * - Node A : int (node's location in the position array)
 * - Node B : int
 * A connects to B. Objects will move from A to B in a one way trafic
 * 
 */