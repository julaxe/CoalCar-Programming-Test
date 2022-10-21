# Coalcar Programming Test
Julian Escobar Echeverri

## Locomotion.
It is all handled by the **Left controller**.
### Turn
Primary 2D axis. 
Left and Right (45 degree rotation).
Down (180 degree rotation).

### Teleport
Primary 2D axis
Up Hold (Shows a ray that indicates where are you going to teleport)
Up Released (Teleport to the selected location if it’s valid)

## Objects editor.
It’s all handled by the **Right Controller**.

### Spawn Objects
Primary Button.
##### On Hold 
* Spawns a ray from your controller if you’re pointing at a valid platform.
* Also spawns a preview of the object that is going to be spawned.
##### On Released
* Spawns an object in the position of the preview object.

### Grab Objects
Grip
##### On Hold
* Objects are going to move the same way of your controller.
* You can rotate and translate objects.
* You can also move them and rotate them with the Primary 2d axis.
##### On Release
* Place your object on the last location.
    
### Delete Objects
The object has to be grabbed.
Secondary button.
##### On Pressed
* Deletes the object that is currently grabbed.

## Main Menu.
You need to use **Both controllers**.

_**Open Menu:**_ Left controller menu button (toggle).
You’ll be presented with 3 options (Editor, Save and Load).

### Editor
* Here you can change the object that you want to spawn.
* Just click/Select the object with your right controller ray.
    
### Save
##### New level
* Saves current level with current objects on the level.
* Creates a new slot with the next number in the level list.
##### Old slots
* If an old slot is selected it will override the old slot with the current level.

### Load
##### Select any Level
* It will load all the spawnable objects that were saved (type of spawnable, rotation, and position).

**Note:** To delete all the saved data you can go to SaveAndLoadManager on the Unity editor and on the inspector press the button “Delete Data”.
