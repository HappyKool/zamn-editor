# General information #

Level pointers start at SNES $9F:8002 or PC $0F:8202.  All pointers are 2 bytes each and are relative to the 9F bank.<br />
All data is stored in little endian format.<br />
All pointers are in SNES format.

# Level format #

## Header ##

This is the format of the level header

```
  |00 01 02 03  04 05 06 07  08 09 0a 0b  0c 0d 0e 0f
--+--------------------------------------------------
00|TILESETPTR-  LVLDATAPTR-  TLSTCOLPTR-  TLSTGFXPTR-
10|PALETTEPTR-  SPRTPLTPTR-  PLTANIMPTR-  MNSTR VCTIM
20|ITEMS WIDTH  HEIGT UNKNW  UNKN3 P1XPS  P1YPS P2XPS
30|P2YPS MUSIC  UNKN2 TITL1  TITL2 BONUS
```

TILESETPTR - a pointer to the tilesets Map16 data.  It stores the arrangements of tiles that make up each background tile<br />
LVLDATAPTR - a pointer to the arrangement of tiles in the background<br />
TLSTCOLPTR - a pointer to the collision data for the tileset<br />
TLSTGFXPTR - a pointer to the graphics data for the tileset<br />
PALETTEPTR - a pointer to the palette for the tileset<br />
SPRTPLTPTR - a pointer to the sprite palette for the level<br />
MNSTR - a pointer to the levels respawning monster data<br />
VCTIM - a pointer to the levels victim and non-respawning monster data<br />
ITEMS - a pointer to the levels item data<br />
WIDTH - the width in tiles of the level background<br />
HEIGT - the height in tiles of the level background<br />
UNKNW - unknown<br />
UNKN3 - unknown<br />
P1XPS - the starting x position of player 1<br />
P1YPS - the starting y position of player 1<br />
P2XPS - the starting x position of player 2<br />
P2YPS - the starting y position of player 2<br />
MUSIC - which song should be played on the level<br />
UNKN2 - unknown<br />
TITL1 - a pointer to the first page of the level title<br />
TITL2 - a pointer to the second page of the level title<br />
BONUS - a pointer to the bonus data<br />

## Boss monsters ##

The boss monster data is listed directly after the header<br />
Each boss monster has this format:

```
  |00 01 02 03  04 05 06 07  08 09 0a 0b  0c 0d 0e 0f
--+--------------------------------------------------
00|CODEPOINTER  EXTRADATA--
```

CODEPOINTER - a pointer to the monsters code<br />
> $82:873C = UFO<br />
> $82:9569 = Giant Baby<br />
> $82:A8BB = Desert Snakeoid<br />
> $82:A8C3 = Grass Snakeoid<br />
> $82:AB95 = Palette fade<br />
> $82:D7CF = Unknown<br />
> $83:AD33 = Giant Spider<br />
EXTRADATA - 4 bytes of extra data that can be used by the monster.  For all standard monsters this is 2 bytes for the x position followed by 2 bytes for the y position, but for the palette fade sprite, these 4 bytes specify a pointer to somewhere inside of the level file to this data:

```
  |00 01 02 03  04 05 06 07  08 09 0a 0b  0c 0d 0e 0f
--+--------------------------------------------------
00|NEWBGPALETT  NEWSPRITEPL
```

NEWBGPALETT - A pointer to the background palette to fade to<br />
NEWSPRITEPL - A pointer to the sprite palette to fade to

After all of the boss monster, 00 00 00 00 is put.

## Tileset Map16 ##

This data is compressed using a compression method similar to Yaz0. Once decompressed, this is the format:<br />
Each tile gets 2 bytes.  The first byte tells which graphics tile to use.  The second bytes individual bits set properties for the tile.

XY-PPP-G

X - Flip the tile horizontally<br />
Y - Flip the tile vertically<br />
P - The palette to use for the tile<br />
G - Use the second graphics page (add 0x100 to the original graphics tile number)

Each full background tile is made of 64 of these.  The tileset has 256 background tiles.

Addresses:<br />
> $9A:C000 = Castle<br />
> $9B:8000 = Grass<br />
> $9B:BCB5 = Pyramid/Beach<br />
> $9C:8000 = Office<br />
> $9C:B6EF = Mall

## Background data ##

This is just the numbers of the background tiles listed from left to right, top to bottom.  Each tile is 2 bytes.

## Collision data ##

There are 2 bytes for each graphics tile.  The bits set properties of the tile.  What each bit does is currently unknown.

Addresses:<br />
> $9B:F4D1 = Grass<br />
> $9B:F8D1 = Pyramid/Beach<br />
> $9C:EAAB = Castle<br />
> $9C:EEAB = Mall<br />
> $9C:F2AB = Office

## Graphics data ##

The graphics are stored in 4bpp planar format.  There are 512 tiles per tileset.

Addresses:<br />
> $98:8000 = Grass<br />
> $98:C000 = Pyramid/Beach<br />
> $99:8000 = Castle<br />
> $99:C000 = Mall<br />
> $9A:8000 = Office

## Palette data ##

Standard 0BBBBBGG GGGRRRRR color format. 128 colors are in the palette.

Addresses: $9E:xx76<br />
> 8E = Grass<br />
> 90 = Fall grass<br />
> 91 = Snowy grass<br />
> 92 = Night grass

> 94 = Pyramid<br />
> 95 = Beach/Desert<br />
> 96 = Dark beach<br />
> 97 = Cave

> 98 = Castle<br />
> 99 = Night castle<br />
> 9A = Bright castle<br />
> 9B = Dark castle

> 9C = Mall<br />
> 9D = Dark mall

> 9E = Office<br />
> 9F = Dark fire cave<br />
> A0 = Light office<br />
> A1 = Dark office<br />
> A2 = Fire cave

## Sprite palette ##

Standard palette format.  Is always $9E:8F76

## Palette animation ##

This appears to point to code.  I haven't yet tried to figure out how the code works.

Addresses:<br />
> $80:A0AD = Castle<br />
> $80:A0EF = Grass<br />
> $80:A137 = Beach<br />
> $80:A222 = Pyramid<br />
> $80:A264 = Fire cave

## Respawning monster data ##

Each monster has this format:

```
  |00 01 02 03  04 05 06 07  08 09 0a 0b  0c 0d 0e 0f
--+--------------------------------------------------
00|RR XPOS- YPOS-- DD CODEPOINTER-
```

RR - The radius of the respawn location<br />
XPOS - The x position of the respawn location<br />
YPOS - The y position of the respawn location<br />
DD - The delay between spawns<br />
CODEPOINTER - A pointer to the monsters code<br />
> $81:87F8 = Normal Zombie<br />
> $81:88CA = Fast Zombie<br />
> $81:8C17 = Mummy<br />
> $81:8E89 = Clone<br />
> $81:99DF = Martian<br />
> $81:ABF5 = Werewolf<br />
> $81:B2B0 = Evil doll<br />
> $81:B664 = Flame guy<br />
> $81:C1FB = Ant from hole<br />
> $81:C321 = Ant from hiding<br />
> $81:C3B6 = Red ant from hole<br />
> $81:C87B = Football player<br />
> $81:CCCF = Blob<br />
> $81:D704 = Mushroom men<br />
> $82:990F = Tentacle

After all monsters, 00 00 is put.

## Victim data ##

Each victim has this format:

```
  |00 01 02 03  04 05 06 07  08 09 0a 0b  0c 0d 0e 0f
--+--------------------------------------------------
00|XPOS- YPOS-  00 00 VN 00  CODEPOINTER
```

XPOS - The x position of the victim<br />
YPOS - The y position of the victim<br />
VN - The victim number.  This specifies which victims will be available when some are lost.  The victims are eliminated from highest to lowest.  For the 10th victim the number should be 0x10 not 0x0A.<br />
CODEPOINTER - A pointer to the victims code.<br />
> $83:9699 = Barbecue guy<br />
> $83:9776 = Baby<br />
> $83:9843 = Trampoline girl<br />
> $83:993D = Army guy<br />
> $83:9A89 = Dog<br />
> $83:9BAD = Dr. Bug<br />
> $83:9C6D = Teacher<br />
> $83:9E15 = Archaeologist<br />
> $83:9D00 = Pool Guy<br />
> $83:9EBE = Cheerleader<br />
> $83:9FE2 = Tourists

After the 10 victims, non-respawning monsters are listed with the same format, except the victim number is always 0.

Code pointers:<br />
> $81:983A = Chainsaw guy<br />
> $81:A455 = Frankenstein<br />
> $81:D2F1 = Plant<br />
> $81:D2F9 = Plant 2<br />
> $82:EF36 = Dracula<br />
> $82:F25D = Fire<br />
> $83:B55E = Dr. Tongue (disappears)<br />
> $83:B7F6 = Giant head (credit level)

After all the monsters, 00 00 is put.

## Item data ##

Each item has this format:

```
  |00 01 02 03  04 05 06 07  08 09 0a 0b  0c 0d 0e 0f
--+--------------------------------------------------
00|XPOS- YPOS-  NN
```

XPOS - The x position of the item<br />
YPOS - The y position of the item<br />
NN - The item numer<br />
> 0000 = Squirtgun<br />
> 0002 = Fire Extinguisher<br />
> 0004 = Bubble Gun<br />
> 0006 = Weed Whacker<br />
> 0008 = Key<br />
> 000A = Running Shoes<br />
> 000C = Monster Potion<br />
> 000E = Blue Potion<br />
> 0010 = Mystery Potion<br />
> 0012 = Gray Potion (unused)<br />
> 0014 = Hamburger<br />
> 0016 = Pop Cans<br />
> 0018 = Tomatoes<br />
> 001A = Popsicles<br />
> 001C = Bananas (unused)<br />
> 001E = First Aid Kit<br />
> 0020 = Plates<br />
> 0022 = Silverware<br />
> 0024 = Cross<br />
> 0026 = Bazooka<br />
> 0028 = Footballs<br />
> 002A = Flamethrower<br />
> 002C = Pandora's Box<br />
> 002E = Skull Key<br />
> 0030 = Clowns<br />
> 0032 = Pile of Keys (only used when a player dies)<br />
> 0034 = Secret Level<br />
> 0036 = 1up<br />
> 0038 = Coins<br />
> 003A = Dollar Bills

After all of the items, 00 00 is put.

## Unknown ##

This value always corresponds to the level's tileset:<br />
> 0057 = Office<br />
> 0059 = Pyramid<br />
> 0069 = Mall<br />
> 0070 = Grass/Castle

## Unknown 2 ##

I have no clue what this does.  It doesn't appear to affect the level in any way.

## Unknown 3 ##

This is always 01FF

## Music ##

0002 = Evening of the Undead<br />
0003 = Title Screen<br />
0004 = Pyramid of Fear<br />
0005 = Terror in Aisle Five<br />
0006 = Zombie Panic<br />
0007 = Chainsaw Mayhem<br />
0008 = Mars Needs Cheerleaders<br />
0009 = Dr. Tongue's Castle<br />
000A = Unused Song<br />
000B = Titanic Toddler

## Level Titles ##

Each word has this format:

```
  |00 01 02 03  04 05 06 07  08 09 0a 0b  0c 0d 0e 0f
--+--------------------------------------------------
00|XP YP PL PG
```

XP - The x position of the word in tiles<br />
YP - The y position of the word in tiles<br />
PL - The palette of the word<br />
> 00 = Blue dripping letters<br />
> 02 = Green Block letters<br />
> 04 = Red/Orange block letters<br />
> 06 = Red dripping letters<br />
PG - The page it should go on<br />
> 01 = First page<br />
> 00 = Second page

Then the characters are listed.  Each is 1 byte.<br />
After each word is 0xFF, or 0x00 if it is the end of the page.

## Bonus data ##

All of the bonuses available in the level are listed.  They are 2 bytes each.<br />
> 0004 = All Victims Saved<br />
> 0006 = Ten Cheerleader<br />
> 0008 = Massive Destruction<br />
> 000A = Pass Completion<br />
> 000C = Weed Cutting<br />
> 000E = No Bazooka Fired<br />
> 0010 = Monster Frozen<br />
> 0012 = Extermination<br />
> 0014 = Chainsaw Begone<br />
> 0016 = Fish Fry Bonus<br />
> 0018 = Frankenstein Destroyed<br />
> 001A = Martian Bubbled<br />
> 001C = Alien Invasion Repulsed<br />
> 001E = Vampire Crossed Out<br />
> 0020 = Secret Bonus

After all of the bonuses 00 00 is put.