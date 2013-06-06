SheetYourself
=============

_Introduction_

SheetYourself is a simple sprite sheet generator for Windows. Given a source folder, it will compile all images found in the source folder into a single compact sprite sheet image file and an XML file defining the size and position of each image in the sprite sheet. Transparent pixels can be optionally cropped out of source images. At this time, SheetYourself does not rotate images to make a more compact sprite sheet, nor does it attempt to constrain sprite sheet output to power of 2 sizes used by other tools.

_Code Versions_

The Visual Studio 2010 solution is provided for users without Visual Studio 2012. Active development is done in the Visual Studio 2012 version, which targets .NET Framework 4.5. Updates to the 2012 version will be backported to the 2010 version when possible.