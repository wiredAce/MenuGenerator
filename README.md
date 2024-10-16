WORK IN PROGRESS

# What is this?
This is a  Plugin for Unity3d that allows you to create easy and navigatable menus from XML configuration Files.
The aim is to remove the repetitive tasks of creating MenuObjects in the hirarchie and configuring them to switch menu
layers.

# Installation

Get the Contents of this repo within your unity project. It needs to be placed such that 'MenuGenerator' is a direct 
child of your "Assets" folder.

So you can either clone the repo 
```
git clone git@github.com:wiredAce/MenuGenerator.git ${pathToYourAssetFolder}
git clone https://github.com/wiredAce/MenuGenerator.git ${pathToYourAssetFolder}
```

Or download the Zip via the projects home and extract it into your Asset Folder:
https://github.com/wiredAce/MenuGenerator

# How to use this?
## Format
Firstly you need to create a valid XML. 
A prefix like: 
```
<?xml version="XXX" encoding="XXX" ?>
```
Is optional. However notice that the program is only tested within in 8 bit ASCII space. You may use Unicode-Chars but 
nothing is guranteed.

The xml needs to caontain a root-tag. the name of which doesn't matter however 'root' or 'menu' is adviced

## Valid Example
```
<menu>
    <firstLayer>
        <secondLayer/>
    </firstLayer>
    <firstLayerSibling/>
</menu>
```

## Building on Top of Skeletton
So you successfully generated the Menu, now what?

The produced Skelleton of the value above should look like this in the hirarchy:

