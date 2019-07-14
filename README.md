# UnityUtilities
Static helper utilities for Unity. Includes Rect, Math, Color, and more.

## Rect (RectUtilities)
<b>SplitToRow</b> - Returns a Rect sliced along X axis. <br>
<b>SplitToColumn</b> - Returns a Rect sliced along the Y axis. <br>
<b>SplitToRectIndex</b> - Returns a Rect at a specific index position, after being sliced along the X and Y axis nth amount of times.<br>

## Math (MathUtilities)
<b>Remap (Int and Float)</b> - Converts a value in a range to its equivalent in another range.<br>

## Texture (TextureUtilities)
<b>SinglePixelTexture</b> - Returns a new Texture2D of a single colored texture.<br>
<b>TintedTexture</b> - Using a copy of a texture, returns it tinted with a color.<br>

## Color (ColorUtilities)
<b>GetColorBetween</b> - Gets the average between two colors.<br>
<b>GetColorsBetween</b> - Returns a single color along a gradient.<br>

## Event (EventUtilities)
<b>isDoubleClickedInEditor</b> - Determines if we have double clicked in the Unity Editor. <br>
<b>isDoubleClicked</b> - Determines if we have double clicked during runtime.<br>

## String (StringUtilities)
<b>AddLowerUpperNeighboringSpaces</b> - Adds white spaces anywhere lowercase neighbors to the left of an uppercase letter. (Ex. "HeyThere!" becomes "Hey There!")

# EDITOR

## UtilityGUI - Partial class for utilities.
<b>BorderRect</b> - Draws a rect with a border. Draw Inside, Outside, or Centered.<br>
<b>ImageButton</b> - Draws a button using an image. Contains states for Active, Inactive, Pressed, and Hover.<br>
<b>BorderedRectButton</b> - Draws a rect with a border that can be selected. Contains colored states for Active, Inactive, Pressed, and Hover.<br>

## UtilityGUILayout - Partial class for utilities.
<b>ImageButton</b> - Draws a button using an image. Contains states for Active, Inactive, Pressed, and Hover.<br>
<b>BorderedRectButton</b> - Draws a rect with a border that can be selected using automatic layout. Contains colored states for Active, Inactive, Pressed, and Hover. (Pressed color with layout isn't working currently. Working on a fix soon.)<br>
<b>StaticSelectionList</b> - <br> Creates a list of StaticSelection as BorderedRectButtons. One click returns the object of TType from static selection and the selection remains active. Double clicking sets StaticSelection.isOpening to true. <br>
<b>StaticSelection</b> - A menu item for StaticSelectionLists.<br>
<b>SelectionStyleContent</b> - A Selection GUIContent and GUIStyle mixed, intended for most GUI and GUILayout list types that are in this branch.<br>
