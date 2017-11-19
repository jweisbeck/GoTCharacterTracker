## Concept
A browseable compendium that lets users learn about and explore the story arcs of GoT characters. The characters are at the center of the app. Users would start by picking a character. From there they could explore a character's journey through the story, study family relationships, track encounters with other GoT figures, and learn about important belongings, etc. 

This is GoT fan site of sorts. But what would make it different is users could fill in character details, making it a rich compendium of GoT character knowledge. I'd want the focus to be on the HBO series, simply because it's a much simpler, easier amount of work to tackle. The value of the app is that users who love the TV show, but can't keep up with everything happening, can use this as a resouce to enrich their viewing experience.

Notably, this compendium will not have a strong connection to the structure of the plot or TV series. It is intended to be a non-linear experience. I don't want it to track the narrative threads in the series. I want the characters as individuals to be the hub of the experience, not their progression. This approach also helps guard against the compendium becoming a raft of spoiler alerts. It's true that revealing encounters between characters and plotting someone's progress on a map may give away too much, but it wouldn't spoil the story or take away from the enjoyment of waching characters develop.


## Objects/classes/entities

* Characters (people)
* Objects (precious things belonging to a person, i.e. longclaw)
* Non-human characters (creatures)
* Houses - details about houses
* Organizations - various relevant groups that appear, i.e. Faith Militant, Brotherhood without Banners, Knights Watch
* Oganization_type - A fixed list of kinds of organzations: military, religious, political, welfare, commercial
* Familial relationships - record familial relationships between characters of a family (or interfamillial relations?). 
* Places - list of places in Westeros relevant to important scenes in the seriesk
* Journey - track where character has been
* Encounters - Details of a character encounter with another character, i.e. Sandor Clegane and Brienne Tarth epic fight
* Events - marquee events from the TV series only, i.e. Red Wedding, Battle of Blackwater Bay. Low-fidelity, low detail - no spoilers

App features
* Character cards - a card of rich data on a character, joins in their house, organizations, family relationships, places and objects
* Jouneys - a representation of all the places a character has been. Can start off as just a list. Eventually could be plotted on a map
* Encounters - a list of all key encounters a character has had. These can be a sub-list under a character Journey
* Approved users and authenticate and modfiy character data, adding to the compendium
* Auth handled via google/facebook login only
