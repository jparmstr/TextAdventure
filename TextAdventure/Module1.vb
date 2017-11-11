Module Module1

    'main variables
    Public continueRunning As Boolean = True
    Public userInput As String
    Public userInput_commandType As commandType
    Public turnCounter As Integer = 1

    'settings
    Public setting_limitPlayerMovement As Boolean = False

    'messages
    Public mainMessage As String
    Public triggerMessage As String

    'player data
    Public player As Person

    'game data
    Public places As New List(Of Place)
    Public people As New List(Of Person)
    Public things As New List(Of Thing)
    Public triggers As New List(Of trigger)
    Public connections As New List(Of connection)
    Public flags As New List(Of flag)

    'triggers
    Public triggerActions_thatHappenAfterTurn As New List(Of triggerAction)

#Region "notes"

    ' final goals (automatic data acquisition)
    ' - using NN, take an input story and automatically turn it into a text adventure
    '   (I'm thinking something similar to the "summarize article" NN could sum up a given story, maybe at multiple layers.
    '    we'd have a sub that distinguishes between information and actions, knowing what to display as text and what to see as actions.
    '    the program would parse the given story and translate it into its own little world; therefore we could "interact" with the story insofar as the program comprehended it.)
    ' - create a chat bot, basically, that can respond in creative ways to user input while not straying from its knowledge of the story
    ' - it would be cool if the program could harvest every bit of information about each character, allowing us to have interactive conversations with them
    '   for example we could talk to Gandalf and ask him where the Palantir came from. the program/NN would respond by summarizing what it had extracted about this subject for this character
    ' - it would be cool if we could extract geographical information and then draw our best guess at a map, from text only
    '   maybe this could also include events; if the program is aware of locale throughout the story, it can find events and tag them with the current locale

    ' intermediate goals (presentation of data)
    ' - I should build a program that displays data in an interactive format
    '   - generally, we have Exposition and Commands. the exposition gives you clues about which commands to give, and the correct sequence of commands advances the story
    '   - in traditional TAs the progression is quite strict. it would be nice to let the flow be freer (although this is probably more of a final goal)
    '   + we also have Places. traveling between Places triggers more Exposition
    '   + there would also be Objects and Characters, both of which can trigger Exposition when interacted with
    ' - I ultimately want to avoid hardcoding data, but I need to do just that for ease of debugging in the early phase
    '   (I need to create by hand anything that I want to be automated in the future)
    '   (I also need to decide if I'm really going to pursue the 'final goals' because they're seeming more farfetched now...
    '    and I'm also starting to think that the less I formalize objects the less predictable the game will feel)
    ' - I need to design and implement the concept of story flow
    '   + is the story just a chain of events? an event being an action that causes changes
    '       + certain actions should cause changes, such as changing the dialogue for characters, the descriptions of places, whether paths are blocked (currently unimplemented), and so on
    ' - maybe the player can move a certain number of nodes per turn. This could be an upgrade during the game
    ' + I need to work on the scripting language to make asset/area creation more intuitive
    '   I'll need to let the program read from / save to an external source eventually
    '   I wonder if creating a little GUI / IDE would be the most straightforward way to create game assets
    '   I just want to get away from: with thisTrigger .condition.property = this, .condition.property = that, because it distracts from content creation a little too much
    '   I feel like I shouldn't focus on the GUI editor just yet. It would be useful but I think it's too much of a distraction this early; it would cement details that I'm still unsure about
    ' + I tidied up the object creation syntax: it's a lot better now
    '   there are helper subs for each type of triggerAction and triggerCondition - this stops you from forgetting any variables
    '   it also lets you declare both conditions and triggerActions in one line which is awesome
    ' - GUI should be live; run at the same time as the game window; let me examine each object without having to set breakpoints

    ' game ideas
    ' - there is an underground cave system you must navigate
    '   I could draw it out on paper, but in the game you're blind without a candle
    ' + a candle / lantern that when lit burns down however many turns (use automatic triggers)
    ' - you need this candle to get through the caves, otherwise it's pitch dark
    '   earlier there's an elephant shaped candle that you can pick up if you want...
    '   when you get to the caves there's a person who will only give you one candle at a time
    '   but if you have the elephant candle you get extra time to navigate the caves
    ' - I could write music for each game area. I like the idea of music with a text-based adventure game
    '   it would be like Jerry Seinfeld was saying about music carrying things along
    '   I think the combination of text and music could be quite good to help you visualize stuff
    ' - add a new property, .sound, which anything can optionally make
    '   somewhere in the game you could hear a curious sound effect and need to ask "what's that sound?" to figure it out
    ' - one area is a dream
    '   you go to bed before having the dream; maybe in a hotel
    '   the dream world doesn't make sense at all; none of your commands work like normal
    '   there should be a lot of random content; so much that it would be unlikely for it to ever repeat
    '   you have to say "wake up" to get out of it
    ' - maybe you're hitchhiking across your country in this game!
    '   but you need a little more motivation though... maybe I can keep the traveling element without hitchhiking
    '   it would be better if the game didn't take place in the modern world since we need limitations; limitations more believable in the past
    '   I need some kind of narrative motivation; either you're moving towards a goal or running away from something
    ' - if the cave section is fun we could create a forest section! 
    '   it wouldn't be dark like the caves but it would be a little bit of a maze
    '   there could be monsters in the forest, you could find trees, find the forest chapel...
    '   maybe there would be a special item like mushrooms which you could collect by solving little puzzles
    '   the cave/forest have in common that you're removed from civilization and that navigation is much more unclear
    '   there needs to be a penalty for being in these places... maybe having each "room" unnamed and moving NESW only would be enough?
    '   in most RPG video games, similar areas beat you down "fighting enemies" but that is not part of this game; at least now throwaway enemies
    '   maybe at some point you do have a fight; but it would be nothing like a stereotypical video game where you're mowing everything down in your path
    ' - I'd like a location that's either a festival or carnival
    '   it would be a unique music opportunity, we could have little games and rides, prizes, and lots of people
    ' - in general I don't want anything to become formulaic or predictable
    '   my favorite part of Pokemon is the very beginning; my least favorite part of Breath of The Wild was figuring out the template with Divine Beasts
    '   since this game can be very minimal in areas that force conventional games to adopt formulas and repetition, I should focus on always keeping things fresh
    '   but glibly speaking, there's always a balance between freshness and user frustration; I need to maintain a language they can rely on for interacting with the game
    '   I think there's something meditative and esoteric about this whole process, which I like: even talking about keeping things non-formulaic makes it feel more formulaic!
    ' - oh yeah, a ghost town sort of like Lavender Town from Pokemon. I was thinking of that last night
    '   this would be another good music opportunity
    '   I'd like to think of a way to make a haunted area suspenseful; have some consequences. Maybe ghosts could ask you riddles and transport you somewhere if you fail to answer
    ' - time: I could translate the turn counter into time
    '   each turn could be about 10 minutes
    '   I'm not sure how much I want this to affect gameplay; I don't want to create The Sims
    '   yeah, I'm not sure how good of an idea this is. I know that I want some locations / scenes to take place at night irregardless of how many turns the player uses
    ' - food: I could let the player eat things
    '   this and the time of day could figure into some secret scores - like you could be evaluated by a doctor towards the end of the game
    ' - seasons and weather that match real-world seasons and weather
    '   for example anything that could be seasonal could be changed; there could be snow on the ground the whole game if played in winter
    '   this could be something with relatively low effort that would be very difficult to do in a game with graphics
    ' - a location that takes place at night and whose soundtrack is a Future-style rap instrumental
    '   a good music opportunity and an opportunity to play with the player's perception of reality
    '   I had the idea earlier that the player could be preoccupied with an argument they had with their girlfriend while trying to navigate a maze area
    '   if the player is drunk I think we could have some fun with an altered reality 
    '   this would be a place the player couldn't go back to; I guess some locations are always there and some like this and the dream only happen once
    ' - since I'm going to the trouble to let objects contain other objects, I should create a puzzle for the game that takes advantage of this
    '   maybe I could have you rebuild an intake manifold system for a car
    '   you start out with a box of car parts and a description of how they're connected; you need to connect them in the right order and put them in the car
    '   I guess it'd be more accurate to say that those parts are connected to each other rather than "in" each other
    '   should I create the concept of connections in the game? I guess picking up or dropping items that were connected would pick them all up
    '   but sometimes connecting things turns them both into something else... like with the car parts you should be able to pick up the assembly without having every little piece in your inventory
    '   maybe putting things together, they should become the "x assembly"?
    '   if so the only difference between a connection and ownership is the syntax
    '   so we could understand "connect" as a variation of "put in", have a boolean that indicates an item is an assembly instead of a container,
    '    display the item as "name assembly" if .isAssembly is true
    '   this could be amusing if the player takes it upon themselves to start assembling / connecting / putting together random things
    '   we could also create more puzzles where the player is creating makeshift contraptions
    '   I wonder if we could create symptoms of misconnected parts for the car example...
    '   this raises another question, could we "connect" both ends of a hose to two different things without creating circular logic? probably not...
    '   I guess we would have to define both ends of a hose as different objects, which could be amusing for the car assembly puzzle
    ' - oh, and I don't think we're preventing putting the same thing in multiple other things yet

    ' immediate goals
    ' + create classes for each of the major Objects I've identified so far
    ' + make it so the player can ask where things are
    ' + make it so the player can take or leave things
    ' + make it so the player can move around
    ' + make it so the player can talk to people
    ' + for each object I'm adding Action, Description, and Dialogue
    ' - to make this more game-like I should limit the player's powers
    '   + can only move to adjacent areas? (would require specifying which areas are connected to each other)
    '       + there should be a global setting, maybe setting_unlimitedMovement that allows moving directly to any location for ease of testing
    '   + can only take objects from the area you're in (I think this is already done actually)
    '   - can only ask where objects are that you've seen (requires storing whether you've seen an object yet)
    '   - should seeing what a person has be required to have "seen" those objects? (different from asking what's in a room)
    '   - should you have to search a room to know what's in it?
    '   - should you have to search an object to know what's in that object?
    '   + maybe you can use objects that are in your current location but not in your inventory...
    '   - however you can't use objects that someone else has, regardless of whether you're in the same room as them
    '   - can only ask about locations that you've been to 
    '   - should there be multiple layers of knowledge for locations?
    '       - such as, by being in a location that's connected to another one, you then know the name of the adjacent location
    '       - but you have to have been in a location to know its description
    '       - you have to have searched a location to know what items are in it
    '         (should you have to currently be in a location to remember all the items that are there?)
    '   - I guess overall I need to decide how good the player's memory is
    ' + it seems like there should be locked doors / blocked paths, so I need a flag indicating whether a path is blocked
    '   + should I assume a path exists between every area? this would require / be directly related to defining how areas are connected
    '     if the connections between areas are their own object, I could simply add a boolean value that indicates whether the path is blocked
    '     then by performing certain actions that path could be unblocked
    ' + I probably need a new class, trigger
    '   + this could be defined as performing a certain actionType on a certain Person, Place, or Thing
    '     every time we do processActions, we also check to see if a trigger has been activated
    '     if a trigger has been activated, we perform the changes specified in the trigger
    '     the trigger needs a list of triggerActions since it can do multiple things
    ' + decide which triggerActions should exist
    '   + change action, description, and dialogue for Person, Place, or Thing (including player)
    '   + add or remove inventory for Person, Place, or Thing (including player)
    '   + lock or unlock a Path
    '   + display text (add text to mainMessage, or...?)
    ' + tidy up the etymology of classes for clarity
    ' + triggers that happen automatically every x number of turns
    '   + I could create a global turn counter and then check for turnCounter Mod x
    '   + triggers with automatic conditions will (for now) have to be checked in a separate loop from other triggers 
    '     since we're only letting one trigger activate per turn to prevent a bug with triggers that change each other 
    '   + triggers that happen automatically should all happen on the same turn, but...
    '     actually I think we'll have the same issue we've already had with normal triggers
    '     if we have a set of automatically happening triggers that move a person back and forth between locations every 5 turns...
    '       then on a 5th turn the first trigger would move the person and set the second trigger as not completed
    '       accidentally causing the second trigger to also fire on that very same turn and move the person back to the first location
    '   + this suggests that we need to come up with a better solution than just stopping checking for triggers once one has happened in a turn
    '       + maybe we can specify whether each triggerAction takes place during or after a turn
    '         (those that modify other triggers should take place after the turn)
    '       + if a TA takes place after a turn, instead of executing it, we could add the TA to a global list
    '         this way we can loop over that global TA list as the last step of processCommands and, since we're no longer checking for activated conditions, not accidentally activate triggers simultaneously
    '         I think I should be able to remove the TA from the global list without destroying it; I'd only be removing the reference to it
    ' + I'm not sure if the "preemptsCommand" variable is relevant anymore. Is there any case where we do not want a trigger to happen before the command?
    '   + I've disabled this distinction since every single trigger I'm creating should happen before the commands
    '     but I've left the after-command checks in place in case there's a trigger that happens as the result of a command (inventory change or something)
    '   + My current solution, which seems good, is to check for trigger activation both before and after the commands
    '     TriggerActions that happen before the commands can write messages as they happen
    '     The command can write a message after it happens
    '     Another check for activated triggerActions happens after the command executes, and each of those can write messages too
    '     Finally, check for triggerActions that have asked happen after the turn is over (such as setCommandNotCompleted) so they don't trigger anything else on the same turn
    ' / just pressing enter with no input doesn't seem to advance the turn counter. I guess this is fine?
    ' - Should there be aliases for commands per Thing? For example the Candle could understand "light" and "extinguish" as "use"
    '   (but in that specific example the issue is raised that "use" is general and applies to both On and Off while more specific language could apply only to either On or Off
    '    so without some new logic we could have a player accidentally lighting the Candle by saying to "extinguish" it when it was already unlit)
    ' + my assuption that every thing has a unique name is backfiring a little bit now
    '   if the TV Remote has two AAA Batteries, we can only hold one of them at a time
    '   I could check the player's inventory by uniqueID but that still doesn't exactly solve the issue if we trace further back:
    '   whichThing_doesInputContain() needs to handle ambiguous cases, I guess
    '   so if there are many AAA Batteries in the world, we need to figure out which one the userInput is referring to
    '   maybe if whichThing_ let items the player doesn't have supercede items the player does have?
    '   but it would also have to be command context sensitive, because we'd want the opposite if the player was trying to use a thing
    '   if I decide to let there be generic items, it also has some implications for any function which refers to a thing by name (which is, everything, including all the triggerActions)
    '   I'm not sure how to solve this yet
    ' + commandType to put one thing in another thing
    '   this would probably require recognizing multiple things in the input ("put the x in the y")
    '   I don't think a list of every item mentioned would necessarily solve anything...
    '   say we get a command, "put the x and the y in the z"; this could accidentally put the x in the y. this could apply to any exponent of the problem
    '   I guess for the specific "put x in y" command, we'd be best served by "whichContainer_doesInputContain" that looks for the first item mentioned after the word "in"
    '   this would be more extensible to multi-item commands like "put x and y in z". it should be able to handle an arbitrary number of items coming before "in z"
    ' + there is a new variable thing.isContainer letting us know which things can contain other things
    ' - should there be a concept of size? 
    '   because right now we could put every item that exists in a pillow and then put that pillow in a flashlight

#End Region

    Sub Main()
        debug_setUp()

        If Not mainMessage = "" Then Console.WriteLine(mainMessage)

        While continueRunning
            Console.Write(">")
            userInput = Console.ReadLine().ToLower.Replace(">", "")

            parseInput()
            processCommands()

            turnCounter += 1
        End While
    End Sub

    Sub parseInput()
        'parse input; look for key words and figure out what they want
        userInput_commandType = commandType.unknown

        With userInput
            If .Contains("program") AndAlso .Contains("exit") Then
                userInput_commandType = commandType.programExit
            End If

            If .Contains("look") Then
                If containsPersonName() Then
                    userInput_commandType = commandType.lookAtPerson
                ElseIf containsPlaceName() Then
                    userInput_commandType = commandType.lookAtPlace
                ElseIf containsThingName() Then
                    userInput_commandType = commandType.lookAtThing
                ElseIf containsPlayerName() Then
                    userInput_commandType = commandType.lookAtPlayer
                Else
                    userInput_commandType = commandType.lookAtPlace
                End If
            End If

            If .Contains("move") Or .Contains("go") Or .Contains("walk") Or .Contains("run") Or .Contains("travel") Then
                userInput_commandType = commandType.moveToPlace
            End If

            If .Contains("take") AndAlso containsPersonName() AndAlso containsPlaceName() Then
                userInput_commandType = commandType.takePersonToPlace
            End If

            If (.Contains("take") AndAlso containsThingName()) Or .Contains("get") Or .Contains("pick up") Then
                userInput_commandType = commandType.takeThing
            End If

            If .Contains("leave") Or .Contains("drop") Then
                userInput_commandType = commandType.leaveThing
            End If

            If .Contains("give") Then
                userInput_commandType = commandType.giveThing
            End If

            If .Contains("check") Or (.Contains("what") AndAlso .Contains("have")) Or .Contains("search") _
                Or (.Contains("look") AndAlso .Contains("inside")) Or (.Contains("look") AndAlso .Contains("in")) _
                Or (.Contains("what") AndAlso .Contains("inside")) Or (.Contains("what") AndAlso .Contains("in")) Then
                If containsPersonName() Then
                    userInput_commandType = commandType.checkPersonIventory
                ElseIf containsPlaceName() Then
                    userInput_commandType = commandType.checkPlaceInventory
                ElseIf containsThingName() Then
                    userInput_commandType = commandType.checkThingInventory
                ElseIf containsPlayerName() Then
                    userInput_commandType = commandType.checkPlayerInventory
                Else
                    userInput_commandType = commandType.checkPlaceInventory
                End If
            End If

            If .Contains("find") Or .Contains("where") Then
                If containsPersonName() Then
                    userInput_commandType = commandType.findPerson
                ElseIf containsPlaceName() Then
                    userInput_commandType = commandType.findPlace
                ElseIf containsThingName() Then
                    userInput_commandType = commandType.findThing
                ElseIf containsPlayerName() Then
                    userInput_commandType = commandType.findPlayer
                End If
            End If

            If .Contains("talk") Then
                If containsPersonName() Then
                    userInput_commandType = commandType.talkToPerson
                ElseIf containsPlaceName() Then
                    userInput_commandType = commandType.talkToPlace
                ElseIf containsThingName() Then
                    userInput_commandType = commandType.talkToThing
                ElseIf containsPlayerName() Then
                    userInput_commandType = commandType.talkToPlayer
                End If
            End If

            If .Contains("who") AndAlso .Contains("here") Then
                userInput_commandType = commandType.whoIsHere
            End If

            If .Contains("what") AndAlso .Contains("here") Then
                userInput_commandType = commandType.whatIsHere
            End If

            If .Contains("what") AndAlso .Contains("doing") Then
                If containsPersonName() Then
                    userInput_commandType = commandType.whatIsPersonDoing
                ElseIf containsPlaceName() Then
                    userInput_commandType = commandType.whatIsPlaceDoing
                ElseIf containsThingName() Then
                    userInput_commandType = commandType.useThing
                ElseIf containsPlayerName() Then
                    userInput_commandType = commandType.whatIsPlayerDoing
                End If
            End If

            If .Contains("use") AndAlso containsThingName() Then
                userInput_commandType = commandType.useThing
            End If

            If .Contains("who has") AndAlso containsThingName() Then
                userInput_commandType = commandType.whoHasThing
            End If

            If .Contains("where can i go") Then
                userInput_commandType = commandType.whereCanIGo
            End If

            If .Contains("put") AndAlso .Contains("in") AndAlso containsThingName() Then
                userInput_commandType = commandType.putThingInOtherThing
            End If
        End With
    End Sub

    Enum commandType
        noCommand
        programExit
        unknown
        lookAtPlace
        lookAtPerson
        lookAtThing
        lookAtPlayer
        moveToPlace
        takeThing
        leaveThing
        giveThing
        putThingInOtherThing
        checkPlayerInventory
        checkPersonIventory
        checkPlaceInventory
        checkThingInventory
        findThing
        findPerson
        findPlace
        findPlayer
        talkToPerson
        talkToThing
        talkToPlace
        talkToPlayer
        whoIsHere
        whatIsHere
        whatIsPersonDoing
        whatIsPlaceDoing
        whatIsPlayerDoing
        useThing
        whoHasThing
        takePersonToPlace
        whereCanIGo
    End Enum

    Sub processCommands()
        'process player actions / input
        Dim thisPerson As Person = whichPerson_doesInputContain()
        Dim thisPlace As Place = whichPlace_doesInputContain()
        Dim thisThing As Thing = whichThing_doesInputContain()
        Dim thisContainer As Thing = whichContainerThing_doesInputContain()

        mainMessage = ""

        'check for activated triggers that preempt commands
        For Each t In triggers
            If Not thisPerson Is Nothing Then t.checkIfActivated(userInput_commandType, thisPerson.name)
            If Not thisPlace Is Nothing Then t.checkIfActivated(userInput_commandType, thisPlace.name)
            If Not thisThing Is Nothing Then t.checkIfActivated(userInput_commandType, thisThing.name)
            If Not triggerMessage = "" Then
                Console.WriteLine(triggerMessage)
                triggerMessage = ""
                'Exit Sub 'kind of a hack to let only 1 trigger happen per input
            End If
        Next

        Select Case userInput_commandType
            Case commandType.programExit
                continueRunning = False
            Case commandType.unknown
                If Not userInput = "" Then mainMessage = "I don't understand what you mean."
            Case commandType.lookAtPlace
                If thisPlace Is Nothing Then thisPlace = player.location
                mainMessage = thisPlace.description
            Case commandType.lookAtPerson
                mainMessage = thisPerson.description
            Case commandType.lookAtThing
                mainMessage = thisThing.description
            Case commandType.lookAtPlayer
                mainMessage = player.description
            Case commandType.moveToPlace
                If player.location Is thisPlace Then
                    mainMessage = "You are already in the " + thisPlace.name + "."
                Else
                    If Not setting_limitPlayerMovement Then
                        'unlimited movement
                        mainMessage = "You moved to the " + thisPlace.name + "." + Environment.NewLine + thisPlace.description
                        player.location = thisPlace
                    ElseIf thisPlace Is Nothing Then
                        'mistyped or nonexistent place name
                        mainMessage = "I don't know where that is."
                    Else
                        'limited movement
                        If placesAreConnected(player.location.name, thisPlace.name) Then
                            Dim thisConnection = getConnection(player.location.name, thisPlace.name)
                            If thisConnection.locked Then
                                mainMessage = "There is a locked " + thisConnection.lockedDoorType + " between the " + player.location.name + " and the " + thisPlace.name + "."
                            Else
                                mainMessage = "You moved to the " + thisPlace.name + "." + Environment.NewLine + thisPlace.description
                                player.location = thisPlace
                            End If
                        Else
                            mainMessage = "The " + thisPlace.name + " is too far away."
                        End If
                    End If
                End If
            Case commandType.takeThing
                'see if player is in the same location as the thing they want to take
                If player.location Is thisThing.getLocation Then
                    If player.inventory.Contains(thisThing) Then
                        mainMessage = "You already have the " + thisThing.name + "."
                    Else
                        mainMessage = "You took the " + thisThing.name + " from " + addArticle(thisThing.getOwner.name, True) + "."
                        thisThing.getOwner.inventory.remove(thisThing)
                        player.inventory.Add(thisThing)
                    End If
                Else
                    mainMessage = "There is no " + thisThing.name + " here."
                End If
            Case commandType.leaveThing
                If Not player.inventory.Contains(thisThing) Then
                    mainMessage = "You do not have the " + thisThing.name + "."
                Else
                    mainMessage = "You left the " + thisThing.name + " in the " + thisPlace.name
                    player.inventory.Remove(thisThing)
                    thisPlace.inventory.Add(thisThing)
                End If
            Case commandType.giveThing
                If Not player.inventory.Contains(thisThing) Then
                    mainMessage = "You do not have the " + thisThing.name + "."
                Else
                    mainMessage = "You gave the " + thisThing.name + " to " + thisPerson.name
                    player.inventory.Remove(thisThing)
                    thisPerson.inventory.Add(thisThing)
                End If
            Case commandType.checkPlayerInventory
                mainMessage = "You have "
                Select Case player.inventory.Count
                    Case 0
                        mainMessage = "You don't have anything."
                    Case 1
                        mainMessage += addArticle(player.inventory(0).name) + "."
                    Case 2
                        mainMessage += addArticle(player.inventory.First.name) + " and " + addArticle(player.inventory(1).name) + "."
                    Case Else
                        For Each t In player.inventory
                            If player.inventory.Last Is t Then
                                mainMessage += "and " + addArticle(t.name) + "."
                            Else
                                mainMessage += addArticle(t.name) + ", "
                            End If
                        Next
                End Select
            Case commandType.checkPersonIventory
                mainMessage = thisPerson.name + " has "
                Select Case thisPerson.inventory.Count
                    Case 0
                        mainMessage = thisPerson.name + " doesn't have anything."
                    Case 1
                        mainMessage += addArticle(thisPerson.inventory(0).name) + "."
                    Case 2
                        mainMessage += addArticle(thisPerson.inventory.First.name) + " and " + addArticle(thisPerson.inventory(1).name) + "."
                    Case Else
                        For Each t In thisPerson.inventory
                            If thisPerson.inventory.Last Is t Then
                                mainMessage += "and " + addArticle(t.name) + "."
                            Else
                                mainMessage += addArticle(t.name) + ", "
                            End If
                        Next
                End Select
            Case commandType.checkPlaceInventory
                mainMessage = "In the " + thisPlace.name + " there is "
                Select Case thisPlace.inventory.Count
                    Case 0
                        mainMessage = "There isn't anything in the " + thisPlace.name + "."
                    Case 1
                        mainMessage += addArticle(thisPlace.inventory(0).name) + "."
                    Case 2
                        mainMessage += addArticle(thisPlace.inventory.First.name) + " and " + addArticle(thisPlace.inventory(1).name) + "."
                    Case Else
                        For Each t In thisPlace.inventory
                            If thisPlace.inventory.Last Is t Then
                                mainMessage += "and " + addArticle(t.name) + "."
                            Else
                                mainMessage += addArticle(t.name) + ", "
                            End If
                        Next
                End Select
            Case commandType.checkThingInventory
                mainMessage = "In the " + thisThing.name + " there is "
                Select Case thisThing.inventory.Count
                    Case 0
                        mainMessage = "There isn't anything in the " + thisThing.name + "."
                    Case 1
                        mainMessage += addArticle(thisThing.inventory(0).name) + "."
                    Case 2
                        mainMessage += addArticle(thisThing.inventory.First.name) + " and " + addArticle(thisThing.inventory(1).name) + "."
                    Case Else
                        For Each t In thisThing.inventory
                            If thisThing.inventory.Last Is t Then
                                mainMessage += "and " + addArticle(t.name) + "."
                            Else
                                mainMessage += addArticle(t.name) + ", "
                            End If
                        Next
                End Select
            Case commandType.findThing
                'if the thing is owned by someone, returns the location of the owner
                mainMessage = "The " + thisThing.name + " is in the " + thisThing.getLocation.name + "."
            Case commandType.findPerson
                mainMessage = thisPerson.name + " is in the " + thisPerson.location.name + "."
            Case commandType.findPlayer
                mainMessage = "You are in the " + player.location.name
            Case commandType.talkToPerson
                mainMessage = thisPerson.dialogue
                If mainMessage = "" Then mainMessage = thisPerson.name + "doesn't have anything to say."
            Case commandType.talkToPlace
                mainMessage = thisPlace.dialogue
                If mainMessage = "" Then mainMessage = thisPlace.name + "doesn't have anything to say."
            Case commandType.talkToThing
                mainMessage = thisThing.dialogue
                If mainMessage = "" Then mainMessage = thisThing.name + "doesn't have anything to say."
            Case commandType.talkToPlayer
                mainMessage = player.dialogue
                If mainMessage = "" Then mainMessage = "You don't have anything to say."
            Case commandType.whoIsHere
                Dim peopleHere = getPeople_byLocation(thisPlace)
                Select Case peopleHere.Count
                    Case 0
                        mainMessage = "No one is in the " + thisPlace.name
                    Case 1
                        mainMessage += peopleHere.First.name + " is in the " + thisPlace.name + "."
                    Case 2
                        mainMessage += peopleHere.First.name + " and " + peopleHere(1).name + " are in the " + thisPlace.name + "."
                    Case Else
                        For Each p In peopleHere
                            If peopleHere.Last Is p Then
                                mainMessage += "and " + p.name
                            Else
                                mainMessage += p.name + ", "
                            End If
                        Next

                        mainMessage += " are in the " + thisPlace.name + "."
                End Select
            Case commandType.whatIsHere
                mainMessage = "In the " + thisPlace.name + " there is "
                Dim thingsHere = getThings_inLocation(thisPlace)

                Select Case thingsHere.Count
                    Case 0
                        mainMessage = "There is nothing in the " + thisPlace.name + "."
                    Case 1
                        mainMessage += addArticle(thingsHere.First.name) + "."
                    Case 2
                        mainMessage += addArticle(thingsHere.First.name) + " and " + addArticle(thingsHere(1).name) + "."
                    Case Else
                        For Each t In thingsHere
                            If thingsHere.Last Is t Then
                                mainMessage += "and " + addArticle(t.name) + "."
                            Else
                                mainMessage += addArticle(t.name) + ", "
                            End If
                        Next
                End Select
            Case commandType.whatIsPersonDoing
                mainMessage = thisPerson.action
                If mainMessage = "" Then mainMessage = thisPerson.name + " isn't doing anything."
            Case commandType.whatIsPlaceDoing
                mainMessage = thisPlace.action
                If mainMessage = "" Then mainMessage = thisPlace.name + " isn't doing anything."
            Case commandType.useThing
                If Not player.inventory.Contains(thisThing) AndAlso (Not thisThing.location Is player.location) Then
                    mainMessage = "You don't have the " + thisThing.name + "."
                Else
                    If Not thisThing.tempAction = "" Then
                        mainMessage = thisThing.tempAction
                    Else
                        If thisThing.isUsedUp Then
                            mainMessage = "The " + thisThing.name + " is all used up."
                        Else
                            mainMessage = thisThing.action
                            If mainMessage = "" Then mainMessage = thisThing.name + " isn't doing anything."
                        End If
                    End If
                End If
            Case commandType.whatIsPlayerDoing
                mainMessage = player.action
                If mainMessage = "" Then mainMessage = "You aren't doing anything."
            Case commandType.whoHasThing
                If Not thisThing.getOwner Is Nothing Then
                    mainMessage = addArticle(thisThing.getOwner.name, True) + " has the " + thisThing.name + "."
                Else
                    mainMessage = "No one has the " + thisThing.name + "."
                End If
            Case commandType.takePersonToPlace
                thisPerson.location = thisPlace
                mainMessage = "You took " + thisPerson.name + " to the " + thisPlace.name + "."
            Case commandType.whereCanIGo
                Dim connectedPlaces = getConnectedPlaces(player.location)
                mainMessage = "From here you can go to "
                Select Case connectedPlaces.Count
                    Case 0
                        mainMessage = "There are no places connected to this one."
                    Case 1
                        mainMessage += "the " + connectedPlaces.First.name + "."
                    Case 2
                        mainMessage += "the " + connectedPlaces.First.name + " or the " + connectedPlaces(1).name + "."
                    Case Else
                        For Each c In connectedPlaces
                            If connectedPlaces.Last Is c Then
                                mainMessage += "or the " + c.name + "."
                            Else
                                mainMessage += "the " + c.name + ", "
                            End If
                        Next
                End Select
            Case commandType.putThingInOtherThing
                If thisThing Is Nothing Or thisContainer Is Nothing Then
                    mainMessage = "I don't know what that is."
                ElseIf Not thisThing.location Is player.location Then
                    mainMessage = "There is no " + thisThing.name + " here."
                ElseIf Not thisContainer.location Is player.location Then
                    mainMessage = "There is no " + thisContainer.name + " here."
                ElseIf thisThing Is thisContainer Then
                    mainMessage = "You can't put the " + thisThing.name + " inside itself!"
                ElseIf thisContainer.getOwner() Is thisThing Then
                    mainMessage = "Are you trying to destroy the universe? You can't do that!"
                    'need to also handle multiple-level circular reasoning somehow
                    'can cause stack overflow by creating a chain of three or more containers inside each other
                ElseIf Not thisContainer.isContainer Then
                    mainMessage = "The " + thisContainer.name + " cannot contain things."
                ElseIf getObjectType_byName(thisThing.getOwner.name) = "thing" Then
                    mainMessage = "The " + thisThing.name + " is already in the " + thisThing.getOwner.name + "."
                Else
                    mainMessage = "You put the " + thisThing.name + " in the " + thisContainer.name + "."
                    thisThing.getOwner.inventory.remove(thisThing)
                    thisContainer.inventory.Add(thisThing)
                End If
        End Select

        If Not mainMessage = "" Then Console.WriteLine(mainMessage)

        updateThings()

        'see if any triggers have been activated as the result of the command
        For Each t In triggers
            If Not thisPerson Is Nothing Then t.checkIfActivated(userInput_commandType, thisPerson.name)
            If Not thisPlace Is Nothing Then t.checkIfActivated(userInput_commandType, thisPlace.name)
            If Not thisThing Is Nothing Then t.checkIfActivated(userInput_commandType, thisThing.name)
            If Not triggerMessage = "" Then
                Console.WriteLine(triggerMessage)
                triggerMessage = ""
                'Exit For 'kind of a hack to let only 1 trigger happen per input
            End If
        Next

        'execute any trigger actions that happen after the turn
        For Each ta In triggerActions_thatHappenAfterTurn
            ta.execute()
        Next
        triggerActions_thatHappenAfterTurn.Clear()
    End Sub

    Sub updateThings()
        For Each t In things
            'clear temp action messages
            t.tempAction = ""

            'update gradual use items
            t.update()
        Next
    End Sub

    Sub debug_setUp()
        'Places
        Dim templatePlace As New Place With {
            .name = "templatePlace",
            .description = "",
            .action = ""}
        Dim bedroom As New Place With {
            .name = "Bedroom",
            .description = "This is the bedroom with Arrowroot colored walls."}
        Dim redBedroom As New Place With {
            .name = "Red Bedroom",
            .description = "This is the red bedroom / office / recording studio."}
        Dim kitchen As New Place With {
            .name = "Kitchen",
            .description = "This kitchen has printed wallpaper and a retro style."}
        Dim livingRoom As New Place With {
            .name = "Living Room",
            .description = "This living room has grey walls and brown carpet."}
        Dim den As New Place With {
            .name = "Den",
            .description = "The den is Alyssa's art studio."}
        Dim hallway As New Place With {
            .name = "Hallway",
            .description = "The hallway has green-ish walls and connects most rooms of the house."}
        Dim blueBathroom As New Place With {
            .name = "Blue Bathroom",
            .description = "The blue bathroom has a toilet, sink, closet, and shower."}
        Dim pinkBathroom As New Place With {
            .name = "Pink Bathroom",
            .description = "The pink bathroom is smaller than the blue bathroom. It has printed wallpaper, a toilet, and a sink."}

        'Connections
        Dim c1 As New connection("red bedroom", "hallway")
        Dim c2 As New connection("bedroom", "hallway")
        Dim c3 As New connection("bedroom", "pink bathroom")
        Dim c4 As New connection("blue bathroom", "hallway")
        Dim c5 As New connection("den", "hallway")
        Dim c6 As New connection("kitchen", "hallway")
        c6.locked = True
        c6.lockedDoorType = "gate"
        Dim c7 As New connection("living room", "kitchen")

        'Things
        Dim templateThing As New Thing With {
            .name = "templateThing",
            .description = "",
            .action = "",
            .dialogue = ""}
        Dim computer As New Thing With {
            .name = "Computer",
            .description = "It has an old Dell screen. Its tower was custom built sometime around 2012.",
            .action = "The computer is running this very program.",
            .dialogue = "I am sad that you have not upgraded to Windows 10. I cannot figure out why because your Kinect is unplugged. Please rekinect your Kinect so I kan spy on you some more.",
            .isContainer = True}
        Dim pillow As New Thing With {
            .name = "Pillow",
            .description = "This is a cheap pillow from Wal-Mart. The pillow case came from Target.",
            .action = "You punch the pillow and relieve some of your anxiety.",
            .isContainer = True}
        Dim pencil As New Thing With {
            .name = "Pencil",
            .description = "This is a grey mechanical pencil with 0.5mm lead.",
            .action = "The pencil is rattling around. It sounds like it has plenty of lead."}
        Dim tvRemote As New Thing With {
            .name = "TV Remote",
            .description = "This is the remote for a Samsung Smart TV.",
            .action = "The remote lights up orange when you press its buttons.",
            .isContainer = True}
        Dim banana As New Thing With {
            .name = "Banana",
            .description = "It is mostly ripe.",
            .action = "You take a bite of the banana and save the rest for later."}
        Dim toothBrush As New Thing With {
            .name = "Toothbrush",
            .description = "This is an Oral-B toothbrush. It is white and green.",
            .action = "You brush your teeth with the dry toothbrush. It tastes vaguely minty and wrong."}
        Dim soap As New Thing With {
            .name = "Soap",
            .description = "This is a bar of pineapple scented soap in a wrapper.",
            .action = "The soap vaguely smells like something but it also seems dusty."}
        Dim flashlight As New Thing With {
            .name = "Flashlight",
            .description = "This is Pete's cool Fenix flashlight. It is turned off.",
            .action = "",
            .isContainer = True}
        Dim phone As New Thing With {
            .name = "Phone",
            .description = "This is Alyssa's smartphone. It's a Nexus 5X in a teal colored case.",
            .action = "You unlock the smartphone. There are several Instagram notifications. You turn it back off.",
            .dialogue = "There is no one on the phone."}
        Dim bone As New Thing With {
            .name = "Bone",
            .description = "This is a white nyla-bone. It has teef marks all in it.",
            .action = "You hold up the bone and Scuttle jumps for it."}
        Dim sock As New Thing With {
            .name = "Sock",
            .description = "This is a plain white sock. Wiggily loves to steal these.",
            .action = "You hold out the sock and Wiggily jumps for it.",
            .isContainer = True}
        Dim collar As New Thing With {
            .name = "Collar",
            .description = "This is Roxy's collar. It has little fish printed on it.",
            .action = "You feel the collar and it feels grimey or something."}
        Dim key As New Thing With {
            .name = "Key",
            .description = "This can unlock the gate between the hallway and the kitchen",
            .action = "You can't use the key here."}
        Dim candle As New Thing With {
            .name = "Candle",
            .description = "It is almost full.",
            .action = ""}
        candle.createGraduallyConsumable(10,
                                         "It has gotten a little bit smaller.",
                                         "There's a little more than half left.",
                                         "It is about half gone.",
                                         "There's only a little bit remaining.",
                                         "It's all used up.")
        Dim battery1 As New Thing With {
            .name = "AAA Battery",
            .description = "This is a AAA battery from the TV Remote."}
        Dim battery2 As New Thing With {
            .name = "AAA Battery",
            .description = "This is a AAA battery from the TV Remote."}
        Dim battery3 As New Thing With {
            .name = "AA Battery",
            .description = "This is a AA battery for the Flashlight."}

        'People
        Dim templatePerson As New Person With {
            .name = "templatePerson",
            .location = Nothing,
            .description = "",
            .action = "",
            .dialogue = ""}
        Dim alyssa As New Person With {
            .name = "Alyssa",
            .location = bedroom,
            .description = "Alyssa is your wife. She has red hair and is wearing her pajamas.",
            .action = "Alyssa is sleeping on the bed.",
            .dialogue = "You say something to Alyssa but she doesn't hear you and snores."}
        Dim roxy As New Person With {
            .name = "Roxy",
            .location = redBedroom,
            .description = "Roxy is almost 10 years old. She's your first dog.",
            .action = "Roxy is intermittently chewing on herself and napping.",
            .dialogue = "You tell Roxy to quit chewing on herself but she completely ignores you."}
        Dim wiggily As New Person With {
            .name = "Wiggily",
            .location = bedroom,
            .description = "Wiggily is Scuttle's brother. He's grey, tan, black, and white.",
            .action = "Wiggily is lightly sleeping.",
            .dialogue = "You say something to Wiggily and he perks right up."}
        Dim scuttle As New Person With {
            .name = "Scuttle",
            .location = bedroom,
            .description = "Scuttle is Wiggily's brother. She's mostly tan and black with a little bit of grey.",
            .action = "Scuttle is sleeping under the bed.",
            .dialogue = "You say something to Scuttle. She looks at you and wags her tail but does not get up."}

        'Player
        player = New Person With {
            .name = "Pete",
            .location = redBedroom,
            .description = "You are Pete. You're 32 years old and unemployed. You feel anxious about the future.",
            .action = "You are writing a text adventure program due to insomnia and stress. You are also playing the same game. Inception.",
            .dialogue = "Your inner voice is singing Climate Controlled Facility."}

        'assign Things to Places and People
        redBedroom.inventory.Add(computer)
        redBedroom.inventory.Add(key)
        bedroom.inventory.Add(pillow)
        den.inventory.Add(pencil)
        livingRoom.inventory.Add(tvRemote)
        kitchen.inventory.Add(banana)
        pinkBathroom.inventory.Add(toothBrush)
        blueBathroom.inventory.Add(soap)
        player.inventory.Add(flashlight)
        player.inventory.Add(candle)
        alyssa.inventory.Add(phone)
        wiggily.inventory.Add(sock)
        scuttle.inventory.Add(bone)
        roxy.inventory.Add(collar)
        tvRemote.inventory.Add(battery1)
        tvRemote.inventory.Add(battery2)
        flashlight.inventory.Add(battery3)

        mainMessage = "Welcome to the adventure. You are in the " + player.location.name + "."

        'triggers
        'a trigger that can unlock the hallway
        Dim hallwayToKitchenKeyTrigger As New trigger
        With hallwayToKitchenKeyTrigger
            .condition.createActionOnObject_andCompareObjects(commandType.useThing, "key", "key", "hallway", logicalOperationType.equals, comparisonPropertyType.location)
            .triggerName = "use key in hallway trigger"
            'lock or unlock the gate in the hallway
            .triggerActions.Add(New triggerAction().createLockOrUnlock("key", "hallway", "kitchen"))
            'set self not completed so that this action can be performed again and again
            .triggerActions.Add(New triggerAction().createSetTriggerNotCompleted("use key in hallway trigger"))
        End With

        '(create two triggers that turn the flashlight on/off and also set each other as not completed)
        Dim firstFlashLightTrigger As New trigger
        With firstFlashLightTrigger
            .condition.createActionOnObject(commandType.useThing, "flashlight")
            .triggerName = "turn on flashlight"
            'change the text on the flashlight item
            .triggerActions.Add(New triggerAction().createSetDescription("flaghlight", "This is Pete's cool Fenix flashlight. It is turned on."))
            'change the action text on the flashlight
            .triggerActions.Add(New triggerAction().createSetAction("flashlight", "You turned on the flashlight."))
            'set the other trigger as not completed
            .triggerActions.Add(New triggerAction().createSetTriggerNotCompleted("turn off flashlight"))
        End With

        Dim secondFlashlightTrigger As New trigger
        With secondFlashlightTrigger
            .condition.createActionOnObject(commandType.useThing, "flashlight")
            .triggerName = "turn off flashlight"
            .completed = True 'completed at first since this is the default state
            'change the text on the flashlight item
            .triggerActions.Add(New triggerAction().createSetDescription("flaghlight", "This is Pete's cool Fenix flashlight. It is turned off."))
            'change the action text on the flashlight
            .triggerActions.Add(New triggerAction().createSetAction("flashlight", "You turned off the flashlight."))
            'set the other trigger as not completed
            .triggerActions.Add(New triggerAction().createSetTriggerNotCompleted("turn on flashlight"))
        End With

        'a set of triggers that moves Roxy back and forth every 5 turns
        Dim automaticMoveRoxyTrigger1 As New trigger
        With automaticMoveRoxyTrigger1
            .condition.createAutomatic(5)
            .triggerName = "move Roxy to bedroom"
            .exposition = "Roxy moved to the Bedroom"
            'TA move Roxy to hallway
            .triggerActions.Add(New triggerAction().createChangeLocation("roxy", "bedroom"))
            'TA set other trigger as not completed
            .triggerActions.Add(New triggerAction().createSetTriggerNotCompleted("move Roxy to red bedroom"))
        End With

        Dim automaticMoveRoxyTrigger2 As New trigger
        With automaticMoveRoxyTrigger2
            .condition.createAutomatic(5)
            .triggerName = "move Roxy to red bedroom"
            .exposition = "Roxy moved to the Red Bedroom"
            .completed = True 'completed since it's the default
            'TA move roxy to red bedroom
            .triggerActions.Add(New triggerAction().createChangeLocation("roxy", "red bedroom"))
            'TA set other trigger as not completed
            .triggerActions.Add(New triggerAction().createSetTriggerNotCompleted("move Roxy to bedroom"))
        End With

        'a trigger that moves Scuttle to a random location if Roxy is in the same room as her
        Dim chaseScuttleTrigger As New trigger
        With chaseScuttleTrigger
            .condition.createCompareObjects("roxy", "scuttle", logicalOperationType.equals, comparisonPropertyType.location)
            .triggerName = "roxy chases scuttle trigger"
            .exposition = "Scuttle ran away from Roxy"
            'move scuttle to random location
            .triggerActions.Add(New triggerAction().createChangeLocationRandom("scuttle"))
            'set self not completed
            .triggerActions.Add(New triggerAction().createSetTriggerNotCompleted("roxy chases scuttle trigger"))
        End With

        'triggers to light or extinguish the candle

        'triggers that burn the candle automatically
        '(probably need to do this to triggerActions:
        ' automatic_BasedOnTurnCounter
        ' automatic_BasedOnItemValue
        'and to Item:
        ' .value
        'let this type of triggerAction decrement item.value every however many global turns...
        'but we also want to be able to turn an item on and off without instantly using it up once it's on
        'maybe we have an Item.turnWhenActivated property to help with this)

        'maybe instead of relying on triggerActions to incrementally use up an item, we can build this right into the item class
        'inUse, turnWhenActivated, 100pctMessage, 80pctMessage, and so on

        'I'm not sure about this graduated messaging thing. It's really imprecise and it feels like it'll be frustrating trying to figure out what these messages mean
        'I've disabled the graduated messaging code but haven't removed it or changed the constructor yet

        Dim candleTrigger1 As New trigger
        With candleTrigger1
            .condition.createActionOnObject(commandType.useThing, "candle")
            .triggerName = "candle trigger 1"
            '.exposition = "You lit the candle."
            .triggerActions.Add(New triggerAction().createActivateItem("candle", "You lit the candle."))
            .triggerActions.Add(New triggerAction().createSetTriggerNotCompleted("candle trigger 2"))
        End With

        Dim candleTrigger2 As New trigger
        With candleTrigger2
            .condition.createActionOnObject(commandType.useThing, "candle")
            .triggerName = "candle trigger 2"
            '.exposition = "You put out the candle."
            .completed = True
            .triggerActions.Add(New triggerAction().createDeactivateItem("candle", "You put out the candle."))
            .triggerActions.Add(New triggerAction().createSetTriggerNotCompleted("candle trigger 1"))
        End With

        'disable the other two triggers if the candle is used up
        Dim candleTrigger3 As New trigger
        With candleTrigger3
            .condition.createCheckIfItemUsedUp("candle")
            .triggerName = "candle trigger 3"
            .exposition = "The candle has been used up and went out."
            .triggerActions.Add(New triggerAction().createSetTriggerCompleted("candle trigger 1"))
            .triggerActions.Add(New triggerAction().createSetTriggerDeactivated("candle trigger 1"))
            .triggerActions.Add(New triggerAction().createSetTriggerCompleted("candle trigger 2"))
            .triggerActions.Add(New triggerAction().createSetTriggerDeactivated("candle trigger 2"))
        End With
    End Sub

#Region "functions"

    Function getFlag(flagName As String) As flag
        Dim result As flag = Nothing
        flagName = flagName.ToLower

        For Each f In flags
            If f.name.ToLower = flagName Then result = f
        Next

        Return result
    End Function

    Function getFlag(uniqueID As Integer) As flag
        Dim result As flag = Nothing

        For Each f In flags
            If f.uniqueID = uniqueID Then result = f
        Next

        Return result
    End Function

    Function getFlagValue(flagName As String) As Boolean
        Return getFlag(flagName).isSet
    End Function

    Function getFlagValue(uniqueID As Integer) As Boolean
        Return getFlag(uniqueID).isSet
    End Function

    Function getObject_byUniqueID(uniqueID As Integer) As Object
        Dim result As Object = Nothing

        If player.uniqueID = uniqueID Then
            result = player
        End If

        For Each p In people
            If p.uniqueID = uniqueID Then result = p
        Next

        For Each p In places
            If p.uniqueID = uniqueID Then result = p
        Next

        For Each t In things
            If t.uniqueID = uniqueID Then result = t
        Next

        For Each t In triggers
            If t.uniqueID = uniqueID Then result = t
            If t.condition.uniqueID = uniqueID Then result = t.condition
            For Each ta In t.triggerActions
                If ta.uniqueID = uniqueID Then result = ta
            Next
        Next

        For Each c In connections
            If c.uniqueID = uniqueID Then result = c
        Next

        For Each f In flags
            If f.uniqueID = uniqueID Then result = f
        Next

        Return result
    End Function

    Function getObject_byName(name As String) As Object
        Dim result As Object = Nothing
        If name = "" Then
            Return result
            Exit Function
        End If
        name = name.ToLower

        If player.name.ToLower = name Then result = player

        For Each p In people
            If p.name.ToLower = name Then result = p
        Next

        For Each p In places
            If p.name.ToLower = name Then result = p
        Next

        For Each t In things
            If t.name.ToLower = name Then result = t
        Next

        For Each t In triggers
            If t.triggerName.ToLower = name Then result = t
        Next

        'connections do not have names right now

        For Each f In flags
            If f.name.ToLower = name Then result = f
        Next

        Return result
    End Function

    Function getUniqueID_byObjectName(name As String) As Integer
        Dim result As Integer = -1
        name = name.ToLower

        If player.name = name Then result = player.uniqueID

        For Each p In people
            If p.name.ToLower = name Then result = p.uniqueID
        Next

        For Each p In places
            If p.name.ToLower = name Then result = p.uniqueID
        Next

        For Each t In things
            If t.name.ToLower = name Then result = t.uniqueID
        Next

        For Each t In triggers
            If t.triggerName.ToLower = name Then result = t.uniqueID
        Next

        Return result
    End Function

    Function getPlace_byUniqueID(uniqueID As Integer) As Place
        Dim result As Place = Nothing

        For Each p In places
            If p.uniqueID = uniqueID Then result = p
        Next

        Return result
    End Function

    Function getPlace_byName(name As String) As Place
        Dim result As Place = Nothing
        name = name.ToLower

        For Each p In places
            If LCase(p.name) = name Then result = p
        Next

        Return result
    End Function

    Function placesAreConnected(place1Name As String, place2Name As String) As Boolean
        Dim result As Boolean = False
        place1Name = place1Name.ToLower
        place2Name = place2Name.ToLower

        For Each c In connections
            If c.place1 = place1Name AndAlso c.place2 = place2Name Then result = True
            If c.place1 = place2Name AndAlso c.place2 = place1Name Then result = True
        Next

        Return result
    End Function

    Function placesAreConnected(place1 As Place, place2 As Place) As Boolean
        Dim result As Boolean = False

        For Each c In connections
            If c.place1 = place1.name AndAlso c.place2 = place2.name Then result = True
            If c.place1 = place2.name AndAlso c.place2 = place1.name Then result = True
        Next

        Return result
    End Function

    Function getConnectedPlaces(startPlace As Place) As List(Of Place)
        Dim result As New List(Of Place)

        For Each c In connections
            If c.place1 = startPlace.name.ToLower Then
                result.Add(getPlace_byName(c.place2))
            End If
            If c.place2 = startPlace.name.ToLower Then
                result.Add(getPlace_byName(c.place1))
            End If
        Next

        Return result
    End Function

    Function getConnection(firstPlaceName As String, secondPlaceName As String) As connection
        Dim result As connection = Nothing
        firstPlaceName = firstPlaceName.ToLower
        secondPlaceName = secondPlaceName.ToLower

        For Each c In connections
            If c.place1 = firstPlaceName AndAlso c.place2 = secondPlaceName Then result = c
            If c.place1 = secondPlaceName AndAlso c.place2 = firstPlaceName Then result = c
        Next

        Return result
    End Function

    Function getRandomPlace_thatIsNot(location As Place) As Place
        Dim result As Place = location
        Dim r As New Random

        While result Is location
            Dim randomIndex As Integer = r.NextDouble() * (places.Count - 1)
            result = places(randomIndex)
        End While

        Return result
    End Function

    Function getPerson_byUniqueID(uniqueID As Integer) As Person
        Dim result As Person = Nothing

        For Each p In people
            If p.uniqueID = uniqueID Then result = p
        Next

        Return result
    End Function

    Function getPerson_byName(name As String) As Person
        Dim result As Person = Nothing
        name = name.ToLower

        For Each p In people
            If LCase(p.name) = name Then result = p
        Next

        Return result
    End Function

    Function getPeople_byLocation(location As Place) As List(Of Person)
        Dim result As New List(Of Person)

        For Each p In people
            If p.location Is location Then result.Add(p)
        Next

        Return result
    End Function

    Function getPeople_byLocation(uniqueID As Integer) As List(Of Person)
        Dim result As New List(Of Person)

        For Each p In people
            If p.location.uniqueID = uniqueID Then result.Add(p)
        Next

        Return result
    End Function

    Function getPeople_byLocation(name As String) As List(Of Person)
        Dim result As New List(Of Person)

        For Each p In people
            If p.location.name = name Then result.Add(p)
        Next

        Return result
    End Function

    Function getThing_byUniqueID(uniqueID As Integer) As Thing
        Dim result As Thing = Nothing

        For Each t In things
            If t.uniqueID = uniqueID Then result = t
        Next

        Return result
    End Function

    Function getThing_byName(name As String) As Thing
        Dim result As Thing = Nothing
        If name = "" Then
            Return result
            Exit Function
        End If
        name = name.ToLower

        For Each t In things
            If LCase(t.name) = name Then result = t
        Next

        Return result
    End Function

    Function getThings_inLocation(location As Place) As List(Of Thing)
        Dim result As New List(Of Thing)

        For Each p In places
            If p Is location Then
                result = p.inventory
            End If
        Next

        Return result
    End Function

    Function getThings_inLocation(uniqueID As Integer) As List(Of Thing)
        Dim result As New List(Of Thing)

        For Each p In places
            If p.uniqueID = uniqueID Then
                result = p.inventory
            End If
        Next

        Return result
    End Function

    Function getThings_inLocation(name As String) As List(Of Thing)
        Dim result As New List(Of Thing)
        name = name.ToLower

        For Each p In places
            If p.name.ToLower = name Then
                result = p.inventory
            End If
        Next

        Return result
    End Function

    Function getThings_notInLocation(placeName As String) As List(Of Thing)
        Return things.Except(getThings_inLocation(placeName)).ToList
    End Function

    Function getThings_withPerson(thisPerson As Person) As List(Of Thing)
        Dim result As New List(Of Thing)

        For Each p In people
            If p Is thisPerson Then
                result = p.inventory
            End If
        Next

        If player Is thisPerson Then
            result = player.inventory
        End If

        Return result
    End Function

    Function getThings_withPerson(uniqueID As Integer) As List(Of Thing)
        Dim result As New List(Of Thing)

        For Each p In people
            If p.uniqueID = uniqueID Then
                result = p.inventory
            End If
        Next

        If player.uniqueID = uniqueID Then
            result = player.inventory
        End If

        Return result
    End Function

    Function getThings_withPerson(name As String) As List(Of Thing)
        Dim result As New List(Of Thing)
        name = name.ToLower

        For Each p In people
            If p.name.ToLower = name Then
                result = p.inventory
            End If
        Next

        If player.name.ToLower = name Then
            result = player.inventory
        End If

        Return result
    End Function

    Function getThings_personDoesntHave(personName As String) As List(Of Thing)
        Return things.Except(getThings_withPerson(personName)).ToList
    End Function

    Function getThings_thatPeopleHave() As List(Of Thing)
        'returns all items from every Person's inventory
        Dim result As New List(Of Thing)

        For Each p In people
            For Each i In p.inventory
                result.Add(i)
            Next
        Next

        Return result
    End Function

    Function getObjectType_byUniqueID(uniqueID As Integer) As String
        Dim result As String = ""

        If player.uniqueID = uniqueID Then result = "person"

        For Each p In people
            If p.uniqueID = uniqueID Then result = "person"
        Next

        For Each p In places
            If p.uniqueID = uniqueID Then result = "place"
        Next

        For Each t In things
            If t.uniqueID = uniqueID Then result = "thing"
        Next

        For Each t In triggers
            If t.uniqueID = uniqueID Then result = "trigger"
            If t.condition.uniqueID = uniqueID Then result = "condition"
            For Each ta In t.triggerActions
                If ta.uniqueID = uniqueID Then result = "triggerAction"
            Next
        Next

        For Each c In connections
            If c.uniqueID = uniqueID Then result = "connection"
        Next

        For Each f In flags
            If f.uniqueID = uniqueID Then result = "flag"
        Next

        Return result
    End Function

    Function getObjectType_byName(name As String) As String
        Dim result As String = ""
        name = name.ToLower

        If player.name.ToLower = name Then result = "player"

        For Each p In people
            If p.name.ToLower = name Then result = "person"
        Next

        For Each p In places
            If p.name.ToLower = name Then result = "place"
        Next

        For Each t In things
            If t.name.ToLower = name Then result = "thing"
        Next

        For Each t In triggers
            If t.triggerName.ToLower = name Then result = "trigger"
            'conditions do not have names
            For Each ta In t.triggerActions
                If ta.name.ToLower = name Then result = "triggerAction"
            Next
        Next

        'connections do not have names

        For Each f In flags
            If f.name.ToLower = name Then result = "flag"
        Next

        Return result
    End Function

    Function getNextUniqueID() As Integer
        Dim thisMax As Integer = -1

        If Not player Is Nothing Then
            If player.uniqueID > thisMax Then thisMax = player.uniqueID
        End If

        For Each p In places
            If p.uniqueID > thisMax Then thisMax = p.uniqueID
        Next

        For Each p In people
            If p.uniqueID > thisMax Then thisMax = p.uniqueID
        Next

        For Each t In things
            If t.uniqueID > thisMax Then thisMax = t.uniqueID
        Next

        For Each t In triggers
            If t.uniqueID > thisMax Then thisMax = t.uniqueID
            If t.condition.uniqueID > thisMax Then thisMax = t.condition.uniqueID
            For Each ta In t.triggerActions
                If ta.uniqueID > thisMax Then thisMax = ta.uniqueID
            Next
        Next

        For Each c In connections
            If c.uniqueID > thisMax Then thisMax = c.uniqueID
        Next

        For Each f In flags
            If f.uniqueID > thisMax Then thisMax = f.uniqueID
        Next

        Return thisMax + 1
    End Function

    Function containsPersonName() As Boolean
        Dim result As Boolean = False

        For Each p In people
            If userInput.Contains(p.name.ToLower) Then result = True
        Next

        Return result
    End Function

    Function containsPlaceName() As Boolean
        Dim result As Boolean = False

        For Each p In places
            If userInput.Contains(p.name.ToLower) Then result = True
        Next

        Return result
    End Function

    Function containsThingName() As Boolean
        Dim result As Boolean = False

        For Each t In things
            If userInput.Contains(t.name.ToLower) Then result = True
        Next

        Return result
    End Function

    Function containsPlayerName() As Boolean
        Dim result As Boolean = False

        If userInput.Contains(player.name.ToLower) Then result = True

        If userInput.Contains(" i ") Or userInput.Contains(" i?") Or userInput.Contains(" i") Then result = True

        Return result
    End Function

    Function whichPerson_doesInputContain() As Person
        Dim result As Person = Nothing

        If userInput.Contains(player.name.ToLower) Or userInput.Contains(" i") Then
            result = player
        End If

        For Each p In people
            If userInput.Contains(p.name.ToLower) Then result = p
        Next

        Return result
    End Function

    Function whichPlace_doesInputContain() As Place
        Dim result As Place = Nothing

        For Each p In places
            If userInput.Contains(p.name.ToLower) Then result = p
        Next

        If userInput.Contains("here") Then result = player.location

        If userInput_commandType = commandType.leaveThing AndAlso result Is Nothing Then result = player.location

        ''default to the player's location if no place was mentioned by name
        'If result Is Nothing Then
        '    result = player.location
        'End If

        Return result
    End Function

    Function whichContainerThing_doesInputContain() As Thing
        Dim result As Thing = Nothing
        userInput = userInput.ToLower

        Dim inLocation = InStr(userInput, " in ")
        Dim userInput_afterIn = userInput.Substring(inLocation)

        '2. container not in same place (equivalent to you don't have)
        For Each t In getThings_notInLocation(player.location.name)
            If userInput_afterIn.Contains(t.name.ToLower) Then result = t
        Next
        '1. container in same place (equivalent to you have)
        For Each t In getThings_inLocation(player.location.name)
            If userInput_afterIn.Contains(t.name.ToLower) Then result = t
        Next

        Return result
    End Function

    Function whichThing_doesInputContain() As Thing
        Dim result As Thing = Nothing
        userInput = userInput.ToLower
        'prioritize which thing is returned based on command type (for handling generic objects)
        'for this purpose we need to have the highest priority item at the bottom, so that it replaces the lower priority ones last

        Select Case userInput_commandType
            Case commandType.putThingInOtherThing
                'do not look for this thing in the container; exclude everything after the word "in"
                Dim inLocation = InStr(userInput, " in ")
                Dim userInput_withoutContainer = userInput.Replace(userInput.Substring(inLocation), "")
                '2. thing not in same place (equivalent to you don't have)
                For Each t In getThings_notInLocation(player.location.name)
                    If userInput_withoutContainer.Contains(t.name.ToLower) Then result = t
                Next
                '1. thing in same place (equivalent to you have)
                For Each t In getThings_inLocation(player.location.name)
                    If userInput_withoutContainer.Contains(t.name.ToLower) Then result = t
                Next
            Case commandType.takeThing
                '3. things not in your location and that you don't have ("there is no ... here")
                For Each t In getThings_notInLocation(player.location.name).Union(getThings_personDoesntHave(player.name)).ToList
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
                '2. things you already have ("you already have the ...")
                For Each t In player.inventory
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
                '1. things you don't have that are in the same place as you
                For Each t In getThings_inLocation(player.location.name).Union(getThings_personDoesntHave(player.name)).ToList
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
            Case commandType.leaveThing
                '2. things you don't have ("you don't have the ...")
                For Each t In getThings_personDoesntHave(player.name)
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
                '1. things you have
                For Each t In player.inventory
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
            Case commandType.giveThing
                '2. things you don't have ("you don't have the ...")
                For Each t In getThings_personDoesntHave(player.name)
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
                '1. things you have
                For Each t In player.inventory
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
            Case commandType.findThing
                '2. thing you have ("you already have the ...")
                For Each t In player.inventory
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
                '1. things you don't have
                For Each t In getThings_personDoesntHave(player.name)
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
            Case commandType.whoHasThing
                '3. no person has it (thing.owner is a place, another thing, or the thing doesn't exist)
                For Each t In things.Except(getThings_thatPeopleHave).ToList
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
                '2. you have it
                For Each t In player.inventory
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
                '1. other person has it
                For Each t In getThings_thatPeopleHave.Except(player.inventory).ToList
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
            Case Else
                For Each t In things
                    If userInput.Contains(t.name.ToLower) Then result = t
                Next
        End Select

        Return result
    End Function

    Function addArticle(toWord As String, Optional definiteArticle As Boolean = False) As String
        Select Case getObjectType_byName(toWord)
            Case "person"
                toWord = toWord
            Case "place"
                toWord = "the " + toWord
            Case "thing"
                If definiteArticle Then
                    toWord = "the " + toWord
                Else
                    If startsWithVowel(toWord) Then
                        toWord = "an " + toWord
                    ElseIf startsWithConsonant(toWord) Then
                        toWord = "a " + toWord
                    End If
                End If
            Case Else
                toWord = "the " + toWord
        End Select

        Return toWord
    End Function

    Function startsWithConsonant(thisWord As String) As Boolean
        Return Not startsWithVowel(thisWord)
    End Function

    Function startsWithVowel(thisWord As String) As Boolean
        Dim result As Boolean = False
        thisWord = thisWord.ToLower

        With thisWord
            If .StartsWith("a") Or .StartsWith("e") Or .StartsWith("i") Or .StartsWith("o") Or .StartsWith("u") Then result = True
        End With

        Return result
    End Function

#End Region

#Region "triggers"

    'flow: triggerCondition > activates trigger > performs triggerActions

    Class trigger
        Public uniqueID As Integer
        Public triggerName As String = "" 'optional
        Public triggerActions As New List(Of triggerAction)
        Public condition As New triggerCondition()
        Public activated As Boolean
        Public completed As Boolean

        Public exposition As String

        Public Sub New()
            uniqueID = getNextUniqueID()
            triggers.Add(Me)
        End Sub

        Public Sub activate()
            'certain triggerActions can set their own triggerMessage (if their action is dynamic)
            'storing exposition -> triggerMessage here gives each triggerAction the opportunity to overwrite it
            triggerMessage = exposition

            For Each t In triggerActions
                If t.happensAfterTurn Then
                    triggerActions_thatHappenAfterTurn.Add(t)
                Else
                    t.execute()
                End If
            Next

            activated = False
            completed = True
        End Sub

        Public Sub checkIfActivated(commandType As commandType, objectUniqueName As String)
            If Not completed Then
                If activated Then
                    'has been activated but not yet executed
                    activate()
                ElseIf condition.matches(commandType, objectUniqueName) Then
                    'matches trigger conditions
                    activated = True
                    activate()
                End If
            End If
        End Sub

    End Class

    Class triggerAction
        Public uniqueID As Integer
        Public type As triggerActionType
        Public targetName As String

        Public action As String
        Public description As String
        Public dialogue As String
        Public name As String = ""
        Public locationName As String
        Public thingName As String 'name of thing
        Public lockUnlock_firstPlaceName As String
        Public lockUnlock_secondPlaceName As String
        Public flagName As String
        Public flagValue As String
        Public activateItemMessage As String
        Public deactivateItemMessage As String

        Public happensAfterTurn As Boolean
        Public completed As Boolean

        Public Sub New()
            uniqueID = getNextUniqueID()
            type = triggerActionType.none
        End Sub

        Public Function createSetAction(targetName_ As String, action_ As String) As triggerAction
            type = triggerActionType.setAction
            targetName = targetName_
            action = action_
            Return Me
        End Function

        Public Function createSetDescription(targetName_ As String, description_ As String) As triggerAction
            type = triggerActionType.setDescription
            targetName = targetName_
            description = description_
            Return Me
        End Function

        Public Function createSetDialogue(targetName_ As String, dialogue_ As String) As triggerAction
            type = triggerActionType.setDialogue
            targetName = targetName_
            dialogue = dialogue_
            Return Me
        End Function

        Public Function createSetName(targetName_ As String, name_ As String) As triggerAction
            type = triggerActionType.setName
            targetName = targetName_
            name = name_
            Return Me
        End Function

        Public Function createChangeLocation(targetName_ As String, locationName_ As String) As triggerAction
            type = triggerActionType.changeLocation
            targetName = targetName_
            locationName = locationName_
            Return Me
        End Function

        Public Function createChangeLocationRandom(targetName_ As String) As triggerAction
            type = triggerActionType.changeLocationRandom
            targetName = targetName_
            Return Me
        End Function

        Public Function createAddInventory(targetName_ As String, thingName_ As String) As triggerAction
            type = triggerActionType.addInventory
            targetName = targetName_
            thingName = thingName_
            Return Me
        End Function

        Public Function createRemoveInventory(targetName_ As String, thingName_ As String) As triggerAction
            type = triggerActionType.removeInventory
            targetName = targetName_
            thingName = thingName_
            Return Me
        End Function

        Public Function createSetTriggerActivated(triggerName_ As String) As triggerAction
            type = triggerActionType.setTriggerActivated
            targetName = triggerName_
            'happensAfterTurn = True
            Return Me
        End Function

        Public Function createSetTriggerDeactivated(triggerName_ As String) As triggerAction
            type = triggerActionType.setTriggerDeactivated
            targetName = triggerName_
            'happensAfterTurn = True
            Return Me
        End Function

        Public Function createSetTriggerCompleted(triggerName_ As String) As triggerAction
            type = triggerActionType.setTriggerCompleted
            targetName = triggerName_
            happensAfterTurn = True
            Return Me
        End Function

        Public Function createSetTriggerNotCompleted(triggerName_ As String) As triggerAction
            type = triggerActionType.setTriggerNotCompleted
            targetName = triggerName_
            happensAfterTurn = True
            Return Me
        End Function

        Public Function createLockOrUnlock(keyObjectName_ As String, firstPlaceName As String, secondPlaceName As String) As triggerAction
            type = triggerActionType.lockOrUnlock
            targetName = keyObjectName_
            lockUnlock_firstPlaceName = firstPlaceName
            lockUnlock_secondPlaceName = secondPlaceName
            Return Me
        End Function

        Public Function createSetFlag(flagName_ As String, flagValue_ As String) As triggerAction
            type = triggerActionType.setFlag
            flagName = flagName_
            flagValue = flagValue_
            Return Me
        End Function

        Public Function createActivateItem(itemName_ As String, activateMessage_ As String) As triggerAction
            type = triggerActionType.activateItem
            targetName = itemName_
            activateItemMessage = activateMessage_
            Return Me
        End Function

        Public Function createDeactivateItem(itemName_ As String, deactivateMessage_ As String) As triggerAction
            type = triggerActionType.deactivateItem
            targetName = itemName_
            deactivateItemMessage = deactivateMessage_
            Return Me
        End Function

        Public Sub execute()
            Dim targetObject = getObject_byName(targetName)
            Dim targetThing As Thing = getThing_byName(thingName)

            Select Case type
                Case triggerActionType.setAction
                    targetObject.action = action
                Case triggerActionType.setDescription
                    targetObject.description = description
                Case triggerActionType.setDialogue
                    targetObject.dialogue = dialogue
                Case triggerActionType.setName
                    targetObject.name = name
                Case triggerActionType.changeLocation
                    targetObject.location = getPlace_byName(locationName)
                Case triggerActionType.changeLocationRandom
                    targetObject.location = getRandomPlace_thatIsNot(targetObject.location)
                    triggerMessage = targetObject.name + " moved to the " + targetObject.location.name
                Case triggerActionType.addInventory
                    targetObject.inventory.add(targetThing)
                Case triggerActionType.removeInventory
                    targetObject.inventory.remove(targetThing)
                Case triggerActionType.setTriggerActivated
                    targetObject.activated = True
                Case triggerActionType.setTriggerDeactivated
                    targetObject.activated = False
                Case triggerActionType.setTriggerCompleted
                    targetObject.completed = True
                Case triggerActionType.setTriggerNotCompleted
                    targetObject.completed = False
                Case triggerActionType.lockOrUnlock
                    Dim thisConnection = getConnection(lockUnlock_firstPlaceName, lockUnlock_secondPlaceName)
                    thisConnection.locked = Not thisConnection.locked
                    If thisConnection.locked Then
                        targetObject.tempAction = "The " + thisConnection.lockedDoorType + " from the " + lockUnlock_firstPlaceName + " to the " + lockUnlock_secondPlaceName + " has been locked."
                    Else
                        targetObject.tempAction = "The " + thisConnection.lockedDoorType + " from the " + lockUnlock_firstPlaceName + " to the " + lockUnlock_secondPlaceName + " has been unlocked."
                    End If
                Case triggerActionType.setFlag
                    getFlag(flagName).isSet = flagValue
                Case triggerActionType.activateItem
                    targetObject.inUse = True
                    targetObject.turnAtLastUpdate = turnCounter
                    If Not activateItemMessage = "" Then targetObject.tempAction = activateItemMessage
                Case triggerActionType.deactivateItem
                    targetObject.inUse = False
                    targetObject.turnAtLastUpdate = turnCounter
                    If Not deactivateItemMessage = "" Then targetObject.tempAction = deactivateItemMessage
            End Select

            completed = True
        End Sub

    End Class

    Class triggerCondition
        Public uniqueID As Integer
        Public type As triggerConditionType = triggerConditionType.none

        'automatic
        Public automaticCounter As Integer = -1

        'action on object
        Public targetCommand As commandType 'command when performed activates this trigger condition
        Public targetObject As String 'unique name of Person, Place, Thing

        'compare objects
        Public firstComparisonObject As String
        Public secondComparisonObject As String
        Public logicalOperation As logicalOperationType = logicalOperationType.none
        Public comparisonProperty As comparisonPropertyType = comparisonPropertyType.none

        'check flag
        Public flagName As String
        Public flagValue As Boolean

        Public Sub New()
            uniqueID = getNextUniqueID()
        End Sub

        Public Sub createAutomatic(automaticCounter_ As Integer)
            type = triggerConditionType.automatic
            automaticCounter = automaticCounter_
        End Sub

        Public Sub createActionOnObject(_targetCommand As commandType, _targetObjectName As String)
            type = triggerConditionType.actionOnObject
            targetCommand = _targetCommand
            targetObject = _targetObjectName
        End Sub

        Public Sub createCompareObjects(firstComparisonObject_ As String, secondComparisonObject_ As String, logicalOperation_ As logicalOperationType, comparisonProperty_ As comparisonPropertyType)
            type = triggerConditionType.compareObjects
            firstComparisonObject = firstComparisonObject_
            secondComparisonObject = secondComparisonObject_
            logicalOperation = logicalOperation_
            comparisonProperty = comparisonProperty_
        End Sub

        Public Sub createActionOnObject_andCompareObjects(targetCommand_ As commandType, targetObjectName_ As String, firstComparisonObject_ As String, secondComparisonObject_ As String, logicalOperation_ As logicalOperationType, comparisonProperty_ As comparisonPropertyType)
            type = triggerConditionType.actionOnObject_andCompareObjects
            targetCommand = targetCommand_
            targetObject = targetObjectName_
            firstComparisonObject = firstComparisonObject_
            secondComparisonObject = secondComparisonObject_
            logicalOperation = logicalOperation_
            comparisonProperty = comparisonProperty_
        End Sub

        Public Sub createCheckFlag(flagName_ As String, flagValue_ As Boolean)
            type = triggerConditionType.checkFlag
            flagName = flagName_
            flagValue = flagValue_
        End Sub

        Public Sub createCheckIfItemUsedUp(itemName_ As String)
            type = triggerConditionType.checkIfItemUsedUp
            targetObject = itemName_
        End Sub

        Public Function matches(commandType As commandType, objectUniqueName As String) As Boolean
            Dim result As Boolean = False

            Select Case type
                Case triggerConditionType.none
                    Console.WriteLine("[Warning] The triggerCondition with uniqueID " + uniqueID.ToString + " has type 'none'.")
                    Console.WriteLine("[Warning] You need to initialize the condition with .createActionOnObject or a sub like that.")
                Case triggerConditionType.automatic
                    If Not automaticCounter = -1 Then
                        'automatic conditions would automatically fire on turn 0 since 0 Mod anything returns 0
                        If Not turnCounter = 0 Then
                            'automatic condition that activates on this turn
                            If turnCounter Mod automaticCounter = 0 Then result = True
                        End If
                    End If
                Case triggerConditionType.actionOnObject
                    If matches_actionOnObject(commandType, objectUniqueName) Then result = True
                Case triggerConditionType.compareObjects
                    If matches_compareObjects() Then result = True
                Case triggerConditionType.actionOnObject_andCompareObjects
                    If matches_actionOnObject(commandType, objectUniqueName) AndAlso matches_compareObjects() Then result = True
                Case triggerConditionType.checkFlag
                    If getFlagValue(flagName) = flagValue Then result = True
                Case triggerConditionType.checkIfItemUsedUp
                    If getThing_byName(targetObject).isUsedUp Then result = True
            End Select

            Return result
        End Function

        Private Function matches_actionOnObject(commandType As commandType, objectUniqueName As String) As Boolean
            Dim result As Boolean = False

            If commandType = targetCommand AndAlso objectUniqueName.ToLower = targetObject.ToLower Then
                result = True
            End If

            Return result
        End Function

        Private Function matches_compareObjects() As Boolean
            Dim result As Boolean = False

            If Not comparisonProperty = comparisonPropertyType.none AndAlso Not logicalOperation = logicalOperationType.none Then
                Dim to1 = getObject_byName(firstComparisonObject)
                Dim to2 = getObject_byName(secondComparisonObject)

                Select Case comparisonProperty
                    Case comparisonPropertyType.location
                        Select Case logicalOperation
                            Case logicalOperationType.equals
                                'location of targetObject and secondTargetObject are equal
                                If to1.location Is to2.location Then
                                    result = True
                                Else
                                    result = False
                                End If
                            Case logicalOperationType.doesNotEqual
                                'location of targetObject and secondTargetObject are not equal
                                If Not to1.location Is to2.location Then
                                    result = True
                                Else
                                    result = False
                                End If
                        End Select
                End Select
            End If

            Return result
        End Function

    End Class


    Enum triggerActionType
        none
        setAction
        setDescription
        setDialogue
        setName
        changeLocation
        changeLocationRandom
        addInventory
        removeInventory
        setTriggerActivated
        setTriggerDeactivated
        setTriggerCompleted
        setTriggerNotCompleted
        lockOrUnlock
        setFlag
        activateItem
        deactivateItem
    End Enum

    Enum triggerConditionType
        none
        automatic
        actionOnObject
        compareObjects
        actionOnObject_andCompareObjects
        checkFlag
        checkIfItemUsedUp
    End Enum

    Enum comparisonPropertyType
        none
        location

    End Enum

    Enum logicalOperationType
        none
        equals
        doesNotEqual

    End Enum

#End Region

    Class Place
        Public uniqueID As Integer
        Public name As String
        Public inventory As New List(Of Thing)

        Public action As String
        Public description As String
        Public dialogue As String

        Public Sub New()
            uniqueID = getNextUniqueID()
            places.Add(Me)
        End Sub

        Public Function location() As Place
            'for functions that perform actions on a generic object
            Return Me
        End Function
    End Class

    Class Person
        Public uniqueID As Integer
        Public name As String
        Public location As Place
        Public inventory As New List(Of Thing)

        Public action As String
        Public description As String
        Public dialogue As String

        Public Sub New()
            uniqueID = getNextUniqueID()
            people.Add(Me)
        End Sub
    End Class

    Class Thing
        Public uniqueID As Integer
        Public name As String
        Public inventory As New List(Of Thing) 'things can be containers (note this is unimplemented and could get tricky)
        Public isContainer As Boolean = False

        Public action As String 'an action that's displayed by default if there's no tempAction
        Public description As String
        Public dialogue As String

        'variables for a gradually consumable item
        Public inUse As Boolean = False
        Private howManyTurnsDoesItLast As Integer
        Public turnAtLastUpdate As Integer
        Public value As Integer = 1 'default so that random items don't report "used up"
        Public description80pct As String
        Public description60pct As String
        Public description40pct As String
        Public description20pct As String
        Public description0pct As String
        'Public activateMessage As String

        Public tempAction As String 'an action that's only displayed once as the result of a triggerAction

        Public Sub New()
            uniqueID = getNextUniqueID()
            things.Add(Me)
        End Sub

        Public Sub createGraduallyConsumable(howManyTurnsDoesItLast_ As Integer, description80pct_ As String, description60pct_ As String,
                                             description40pct_ As String, description20pct_ As String, description0pct_ As String)
            howManyTurnsDoesItLast = howManyTurnsDoesItLast_
            value = howManyTurnsDoesItLast
            description80pct = description80pct_
            description60pct = description60pct_
            description40pct = description40pct_
            description20pct = description20pct_
            description0pct = description0pct_
        End Sub

        Public Sub update()
            If inUse Then
                Dim difference = turnCounter - turnAtLastUpdate
                value -= difference
                turnAtLastUpdate = turnCounter

                description = "It will last about " + Math.Round(value - 1).ToString + " more turns."

                If value <= 1 Then description = "The " + name.ToLower + " is all used up."

                'Select Case value / howManyTurnsDoesItLast
                '    Case 0.61 To 0.8
                '        description = description80pct
                '    Case 0.41 To 0.6
                '        description = description60pct
                '    Case 0.21 To 0.4
                '        description = description40pct
                '    Case 0.01 To 0.2
                '        description = description20pct
                '    Case Is <= 0
                '        description = description0pct
                '        inUse = False
                'End Select
            End If
        End Sub

        Public Function isUsedUp() As Boolean
            If value <= 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function location() As Place
            'for functions that perform actions on a generic object
            Return getLocation()
        End Function

        Public Function getLocation() As Place
            'no location property, it's either with a person or in a place
            Dim result As Place = Nothing

            If player.inventory.Contains(Me) Then result = player.location

            For Each p In places
                If p.inventory.Contains(Me) Then result = p
            Next

            For Each p In people
                If p.inventory.Contains(Me) Then result = p.location
            Next

            For Each t In things
                If t.inventory.Contains(Me) Then result = t.location
            Next

            Return result
        End Function

        Public Function getOwner() As Object
            Dim result As Object = Nothing

            For Each i In player.inventory
                If i Is Me Then result = player
            Next

            For Each p In people
                For Each i In p.inventory
                    If i Is Me Then result = p
                Next
            Next

            For Each p In places
                For Each i In p.inventory
                    If i Is Me Then result = p
                Next
            Next

            'note: this would be better with a recursive call but I haven't figured that out yet
            'with this logic the maximum nested item depth is 5

            'we could always store a reference to the owner in each thing...
            'would have to update it on creation and each time the thing changes owners

            For Each t In things
                For Each i In t.inventory
                    If i Is Me Then result = t
                    For Each i1 In i.inventory
                        If i1 Is Me Then result = i
                        For Each i2 In i1.inventory
                            If i2 Is Me Then result = i1
                            For Each i3 In i2.inventory
                                If i3 Is Me Then result = i2
                                For Each i4 In i3.inventory
                                    If i4 Is Me Then result = i3
                                Next
                            Next
                        Next
                    Next
                Next
            Next

            Return result
        End Function

        Public Function getTopLevelOwner() As Thing
            'drill up until we find a thing with no owner itself

            Dim result As Thing = Me

            While Not result.getOwner Is Nothing
                result = result.getOwner
            End While

            If result Is Me Then result = Nothing

            Return result
        End Function

    End Class

    Class connection
        Public uniqueID As Integer
        Public place1 As String
        Public place2 As String

        Public locked As Boolean = False
        Public lockedDoorType As String

        Public Sub New(place1Name As String, place2Name As String)
            uniqueID = getNextUniqueID()
            place1 = place1Name.ToLower
            place2 = place2Name.ToLower

            connections.Add(Me)
        End Sub
    End Class

    Class flag
        Public uniqueID As Integer
        Public name As String
        Public isSet As Boolean

        Public Sub New(flagName As String, Optional flagIsSet As Boolean = False)
            uniqueID = getNextUniqueID()
            name = flagName
            flags.Add(Me)
        End Sub
    End Class

End Module
