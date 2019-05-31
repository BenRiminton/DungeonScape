'Made by Ben Munday, Ashley Pile, Ben Riminton
'DungeonScape the Text Adventure
Module Main 'Module Begining and Declared Global Variables
    Dim Alive As Boolean        'Are you alive? Y or N
    Dim Playing As Boolean = True 'Are you playing?
    Dim Input As String         'Player Input
    Dim Riddle As String        'Riddle Input
    Dim CurrentRoom As Integer  'Current room you are standing in
    Dim Moving As Boolean       'Can you move?
    '-------------------------
    Dim Intro As String
    Dim Name As String
    Dim Room(64) As String      'Room Description Array
    Dim Exits(64) As String     'Possible Exits Array
    Dim Visited(64) As Boolean  'Rooms Visited Array
    Dim RoomV(64) As String     'Visited Room Descriptions Array
    Dim words() As String       'Word Inputs
    Dim NoOfWords As Integer    'How many words
    Dim length As Integer
    Dim NameLength As Integer
    Dim Suffix As String
    '---------------------------
    Dim Health As Integer
    Dim ItemName(99) As String
    Dim ItemDescription(99) As String
    Dim ItemLocation(99) As Integer
    Dim ItemDropped(99) As Boolean
    Dim DropSearcher As Integer
    Dim PickupCounter As Integer
    Dim BreakCounter As Integer
    Dim SearchCounter As Integer
    Dim UnlockCounter As Integer
    Dim CombatCounter As Integer
    Dim WriteCounter As Integer
    Dim DisarmCounter As Integer
    Dim OpenCounter As Integer
    Dim DropCounter As Integer
    Dim Random As Integer
    Dim Found = False
    '--------------------------
    Dim ObjectName(99) As String
    Dim ObjectDescription(99) As String
    Dim ObjectLocation(99) As Integer
    '-------------------------
    Dim EnemyName(99) As String
    Dim EnemyDescription(99) As String
    Dim EnemyLocation(99) As Integer
    '-------------------------
    Dim strMessage As String
    Dim strMessageChop() As String

    Dim God As Boolean = False

    Sub Main()  'Main Game Sub

        Console.Title = "DungeonScape - A Game By Team Array"
        Do While Playing = True
            Console.WindowHeight = 40
            Console.WindowWidth = 80
            Console.BufferHeight = 40
            Console.ForegroundColor = 7
            Console.Clear()

            Health = 100

            Alive = True        'Declare yourself Alive(New Game)
            RoomInitialise()      'Call Sub RoomInitialise (Assigns Room Descriptions)
            Console.Out.WriteLine("")
            Console.Out.WriteLine("                                   [        ]                                   ")
            System.Threading.Thread.Sleep(500)
            Console.Clear()
            Console.Out.WriteLine("                               INITIALISING ROOMS                               ")
            Console.Out.WriteLine("                                   [II      ]                                   ")
            InitialiseExits()     'Call Sub InitialiseExits (Assigns Possible Room Exits)
            InitialiseVisited()   'Call Sub InitialiseVisisted  (Assigns Rooms Visited)
            InitialiseRoomV()     'Call Sub InitialiseRoomV (Assigns Visited Room Descriptions)
            InitialiseItems()     'Call Sub InitialiseItems (Assigns Items)
            System.Threading.Thread.Sleep(750)
            Console.Clear()
            Console.Out.WriteLine("                               INITIALISING ITEMS                               ")
            Console.Out.WriteLine("                                   [IIII    ]                                   ")
            InitialiseObjects()   'Call Sub InitialiseObjects (Assigns Objects)
            System.Threading.Thread.Sleep(750)
            Console.Clear()
            Console.Out.WriteLine("                              INITIALISING OBJECTS                              ")
            Console.Out.WriteLine("                                   [IIIIII  ]                                   ")
            InitialiseEnemy()     'Call Sub InitialiseEnemy (Assigns Enemies)
            System.Threading.Thread.Sleep(750)
            Console.Clear()
            Console.Out.WriteLine("                              INITIALISING ENEMIES                              ")
            Console.Out.WriteLine("                                   [IIIIIIII]                                   ")
            Console.ForegroundColor = 15
            System.Threading.Thread.Sleep(750)
            'INTRO
            Console.Clear()
            PrintLogo()
            Console.ForegroundColor = 15
            Console.Clear()
            PrintIntro()
            Do

                Console.Out.WriteLine("")
                Console.Out.WriteLine("Enter a character name:")
                Console.Out.WriteLine("")
                Console.ForegroundColor = 14
                Console.Out.Write(">")
                Name = Console.In.ReadLine()
                Console.ForegroundColor = 15
                Console.Clear()
                NameLength = LEN(Name)
                If NameLength > 8 Then
                    Console.Out.WriteLine("You Hero name may be no longer than 8 characters.")
                End If
            Loop Until NameLength > 0 And NameLength < 9
            Suffix = " The Prisoner"


            Console.Out.Writeline("")
            Console.Out.Writeline("*****BASIC COMMANDS******")
            Console.Out.Writeline("You can move around the world using NORTH, SOUTH, EAST OR WEST.")
            Console.Out.Writeline("You can check your inventory with INVENTORY(I)")
            Console.Out.Writeline("You can interact with objects around you with SEARCH or EXAMINE")
            Console.Out.WriteLine("Typing LOOK or LOOK AROUND gives you a description of your current location.")
            Console.Out.WriteLine("Clear the console screen using CLS or CLEAR.")
            Console.Out.WriteLine("You can use HELP to re-read these basic commands if you forget!")
            Console.Out.Writeline("The rest is up to you to figure out!")
            Console.Out.Writeline("")
            Console.Out.WriteLine("Press ENTER to begin your journey!")
            Console.out.WriteLine("")
            Console.In.Readline()
            Console.Clear()
            'START OF GAME
            Console.ForegroundColor = 10
            Console.Out.WriteLine("Health: " & Health & " HP.")
            Console.ForegroundColor = 15
            Console.Out.WriteLine("")
            FirstRoom()
            Do While Alive = True 'DO Loop to allow for command inputs until you die

                Console.Out.WriteLine("")
                Console.ForegroundColor = 14
                Console.Out.Write(">") 'Input Cursor
                Input = ucase(Console.In.ReadLine())
                Console.ForegroundColor = 15
                Console.Out.WriteLine("")
                Parser() 'Calls the command parser to determine what the input is.
                If Health <= 0 Then
                    Alive = False
                End If
                If EnemyLocation(0) = 2 And Visited(2) = True Then
                    RoomV(2) = "The guard is still sitting in his chair unwary of your presence. You can go back south or leave the office to the east, but I suggest taking out the guard first."
                Else
                    RoomV(2) = "A guard is leaning back in his chair facing the wall ahead of you. He seems oblivious to your presence. A small candle is burning on his desk sending your shadow dancing across the walls around you. There is a door just to the east of the guard. I would suggest taking out the guard with something first. You could also return to the south."
                End If

                If EnemyLocation(1) = 47 And Visited(47) = True Then
                    RoomV(47) = "The giant green mass of Slime still wobbles and blocks the room the the west, you can go north or south, or fight the slime."
                Else
                    RoomV(47) = "There is a large green watery pool of where the slime once was. You can go back north or south or check the room to the west."
                End If
            Loop
            'Death upon leaving the Alive loop
            Console.ForegroundColor = 12
            Console.Out.Writeline("")
            Console.out.Writeline("You died, I suppose you did better than I expected, well done. Oh yes I almost forgot, would you like to try again?")
            Console.Out.Writeline("")
            Console.ForegroundColor = 15
            Console.Out.Write("Y/N: ")
            Input = ucase(Console.In.Readline())
            'Would you like to continue?
            If Input = "N" Or Input = "NO" Then
                Playing = False
            End If
        Loop
    End Sub
    '---PARSER---
    Sub Parser()    'A subroutine which determines the player's input.
        length = LEN(input)
        words = Split(input, " ")
        NoOfWords = ubound(words)
        Select Case NoOfWords
            Case Is = 0
                commandsOneWord()
            Case Is = 1
                commandsTwoWords()
            Case Is = 2
                If instr(words(1), "THE") > 0 Then
                    words(1) = words(2)
                    commandsTwoWords()
                ElseIf instr(words(1), "ON") > 0 Then
                    words(1) = words(2)
                    commandstwoWords()
                ElseIf instr(words(1), "ON") > 0 And instr(words(2), "THE") > 0 Then
                    words(1) = words(3)
                    commandsTwoWords()
                ElseIf instr(words(1), "BOOK") > 0 Then
                    words(1) = words(2)
                    commandsTwoWords()
                ElseIf instr(words(1), "RED") > 0 Or instr(words(1), "BLUE") > 0 Then
                    words(1) = words(1)
                    commandsTwoWords()
                ElseIf instr(words(2), "POTION") > 0 Then
                    commandsTwoWords()
                ElseIf words(0) = "DROP" Then
                    commandsTwoWords()
                ElseIf words(0) = "TAKE" Or words(0) = "PICKUP" Or words(0) = "LOOT" Or words(0) = "TAKE" Or words(0) = "PICKUP" Or words(0) = "GRAB" Then
                    commandsTwoWords()
                Else
                    Console.Out.WriteLine("I don't understand what you want me to do.")
                End If
            Case Else
                Console.Out.WriteLine("I don't understand what you want me to do.")
        End Select
    End Sub
    '---ONE WORD---
    Sub commandsOneWord()
        Select Case words(0)
            Case Is = "NORTH", "EAST", "SOUTH", "WEST", "N", "W", "E", "S" 'ONE WORD MOVEMENTS
                Movement(words(0))
            Case Is = "I", "INVENTORY", "ITEMS"
                Inventory()
            Case Is = "SILENCE"
                Write(words(0))
            Case Is = "HELP"
                Help()
            Case Is = "LOOK"
                Look()
            Case Is = "HI", "HELLO", "GREETINGS", "LOL", "BYE", "GOODBYE"
                Comedy()
            Case Is = "CLS", "CLEAR"
                Clear()
            Case Is = "SUICIDE", "DIE"
                If God = True Then
                    Console.Out.WriteLine("But you are a god!")
                Else
                    Alive = False
                End If
            Case Is = "HP", "HEALTH"
                HealthView()
            Case Is = "GODMODE"
                GodMode()
            Case Is = "DROP"
                Console.Out.WriteLine("What would you like to drop?")
                Console.ForegroundColor = 14
                Console.Out.WriteLine("")
                Console.Out.Write(">")
                Input = ucase(Console.In.ReadLine())
                Console.Out.WriteLine("")
                Console.ForegroundColor = 15
                Drop(Input)
            Case Is = ""
            Case Else
                Console.Out.WriteLine("I don't understand what you want me to do.")
        End Select
    End Sub
    '---TWO WORDS---
    Sub commandsTwoWords()    'TWO WORD COMMANDS
        Select Case words(0)
            Case Is = "GO", "WALK", "RUN", "HEAD", "MOVE", "JOG", "SKIP", "STROLL", "WANDER", "BOWL"    'Movements
                Movement(words(1))
            Case Is = "SEARCH", "EXAMINE", "CHECK", "INSPECT" 'Search
                Search(words(1))
            Case Is = "BREAK", "DESTROY", "HIT", "KICK", "SMASH", "BASH", "RAM", "JAB", "" 'Break
                Break(words(1))
            Case Is = "KILL", "HURT", "ATTACK", "FIGHT", "STAB", "SHANK", "CUT"  'Combat
                Combat(words(1))
            Case Is = "LOOT", "TAKE", "PICKUP", "GRAB", "STEAL", "ACQUIRE", "NINJA"  'Pickup
                words(1) = MID(input, 6, length)
                Pickup(words(1))
            Case Is = "UNLOCK"
                Unlock(words(1))
            Case Is = "WRITE", "DRAW"
                Write(words(1))
            Case Is = "PORT", "TP", "GIVE", "CURRENT"
                Cheat(words(1))
            Case Is = "LOOK"
                Look()
            Case Is = "DISARM"
                Disarm(words(1))
            Case Is = "OPEN"
                Open(words(1))
            Case Is = ""
            Case Is = "DROP"
                words(1) = MID(input, 6, length)
                Drop(words(1))
            Case Is = "READ"
                Read(words(1))
            Case Is = "TOUCH"
                If words(1) = "TORCH" And ItemLocation(0) = -1 Then
                    Console.Out.WriteLine("You foolishly touch the torch. It burns your fingers.")
                    Health = Health - 2
                    Console.ForegroundColor = 12
                    Console.Out.WriteLine("")
                    Console.Out.WriteLine("You lose 2 HP.")
                    Console.ForegroundColor = 15
                ElseIf ItemLocation(0) = CurrentRoom Then
                    Console.Out.WriteLine("You foolishly touch the torch. It burns your fingers.")
                    Health = Health - 2
                    Console.ForegroundColor = 12
                    Console.Out.WriteLine("")
                    Console.Out.WriteLine("You lose 2 HP.")
                    Console.ForegroundColor = 15
                Else
                    Console.Out.WriteLine("I don't see the point in doing that...")
                End If
            Case Is = "DRINK"
                Drink(words(1))
            Case Else
                Console.Out.WriteLine("I don't understand what you want me to do.") 'Invalid Command
        End Select
    End Sub
    '---MOVEMENT---
    Sub Movement(ByVal direction As String) 'Allows for movement across rooms.
        Select Case direction
            Case Is = "N"
                direction = "NORTH"
            Case Is = "E"
                direction = "EAST"
            Case Is = "W"
                direction = "WEST"
            Case Is = "S"
                direction = "SOUTH"
        End Select
        Moving = False
        'Room Collision events
        If CurrentRoom = 0 And ObjectLocation(0) > -1 And direction = "EAST" Then
            Console.Out.WriteLine("The cell door blocks your way.")
        ElseIf CurrentRoom = 0 And ObjectLocation(0) > -1 And direction = "E" Then
            Console.Out.WriteLine("The cell door blocks your way.")
        ElseIf CurrentRoom = 9 And ObjectLocation(3) > -1 And direction = "EAST" Then
            Console.Out.WriteLine("A locked door blocks your way.")
        ElseIf CurrentRoom = 9 And ObjectLocation(3) > -1 And direction = "E" Then
            Console.Out.WriteLine("A locked door blocks your way.")
        ElseIf CurrentRoom = 13 And ItemLocation(0) > -1 And direction = "SOUTH" Then
            Console.Out.WriteLine("You step forward into the darkness. The floor beneath you disappears and you")
            Console.Out.WriteLine("plummet into darkness.")
            Alive = False
        ElseIf CurrentRoom = 13 And ItemLocation(0) > -1 And direction = "S" Then
            Console.Out.WriteLine("You step forward into the darkness. The floor beneath you disappears and you")
            Console.Out.WriteLine("plummet into darkness.")
            Alive = False
        ElseIf CurrentRoom = 2 And EnemyLocation(0) = CurrentRoom And direction = "EAST" Then
            Console.Out.Writeline("You try to sneak past the guard as he rests in his chair, he notices you and swiftly tackles you to the ground and slits your throat, maybe you should try taking him out first?")
            Alive = False
        ElseIf CurrentRoom = 2 And EnemyLocation(0) = CurrentRoom And direction = "E" Then
            Console.Out.Writeline("You try to sneak past the guard as he rests in his chair, he notices you and swiftly tackles you to the ground and slits your throat, maybe you should try taking him out first?")
            Alive = False
        ElseIf CurrentRoom = 6 And ObjectLocation(5) > -1 And direction = "EAST" Then
            Console.Out.WriteLine("A magically sealed door blocks your way.")
        ElseIf CurrentRoom = 6 And ObjectLocation(5) > -1 And direction = "E" Then
            Console.Out.WriteLine("A magically sealed door blocks your way.")
        ElseIf CurrentRoom = 47 And EnemyLocation(1) = 47 And direction = "WEST" Then
            Console.Out.WriteLine("The Slime is blocking that direction.")
        ElseIf CurrentRoom = 47 And EnemyLocation(1) = 47 And direction = "W" Then
            Console.Out.WriteLine("The Slime is blocking that direction.")
        Else
            If Instr(Exits(CurrentRoom), direction) > 0 Then 'Take the first letter of your input for direction
                Moving = True
                Select Case direction
                    Case Is = "NORTH", "N"
                        CurrentRoom = CurrentRoom - 8   'Go North
                        Visited(CurrentRoom + 8) = True
                    Case Is = "EAST", "E"
                        CurrentRoom = CurrentRoom + 1   'Go East
                        Visited(CurrentRoom - 1) = True
                    Case Is = "SOUTH", "S"
                        CurrentRoom = CurrentRoom + 8   'Go South
                        Visited(CurrentRoom - 8) = True
                    Case Is = "WEST", "W"
                        CurrentRoom = CurrentRoom - 1   'Go west
                        Visited(CurrentRoom + 1) = True
                End Select
            Else
                Select Case direction
                    Case Is = "NORTH", "SOUTH", "EAST", "WEST", "N", "S", "E", "W"
                        Console.Out.Writeline("You cannot go that way.") 'Invalid Direction	
                    Case Else
                        Console.Out.WriteLine("I don't understand that...")
                End Select
            End If

            If Moving = True And Visited(CurrentRoom) = False Then  'IF Statement - If a room is visited, change room description
                Buffer()
                DroppedItems()
                Visited(CurrentRoom) = True
            ElseIf Moving = True Then
                BufferV()
                DroppedItems()
                Visited(CurrentRoom) = True
            End If


        End If


    End Sub

    '---CLEAR---
    Sub Clear()
        Console.Clear()
    End Sub
    '---CHEAT---
    Sub Cheat(ByVal objects As String)

        Select Case objects
            Case Is = "ROOM"
                Console.Out.WriteLine("Current Room: " & CurrentRoom & ".")
            Case Is = "ALL"
                Console.ForegroundColor = 10
                Console.Out.WriteLine("ALL ITEMS ARE NOW IN YOUR POSESSION!")
                Console.ForegroundColor = 15
                For Counter = 0 To 14 ' Change when items are added.
                    ItemLocation(Counter) = -1
                Next
            Case Else
                CurrentRoom = val(objects)
                Console.Out.WriteLine(Room(CurrentRoom))
                DroppedItems()
        End Select
    End Sub
    '---COMBAT---
    Sub Combat(ByVal objects As String)
        CombatCounter = 0
        Do Until Found = True Or CombatCounter = 99
            If Instr(EnemyName(CombatCounter), objects) > 0 Then
                Found = True
                If Instr(EnemyLocation(CombatCounter), CurrentRoom) > 0 And ItemLocation(1) = -1 And objects = "GUARD" Then
                    Console.Out.WriteLine("You attack the " & lcase(objects) & " and force the shiv into his neck, he falls to the floor, dead.")
                    EnemyLocation(CombatCounter) = -1
                ElseIf Instr(EnemyLocation(CombatCounter), CurrentRoom) > 0 And ItemLocation(1) > -1 And objects = "GUARD" Then
                    Console.Out.WriteLine("You try to choke the " & lcase(objects) & " with your fists. He breaks loose and slits your throat.")
                    Console.Out.WriteLine("")
                    Console.ForegroundColor = 12
                    Console.Out.WriteLine("You lose 100 HP.")
                    Console.ForegroundColor = 15
                    Health = Health - 100
                ElseIf Instr(EnemyLocation(CombatCounter), CurrentRoom) > 0 And ItemLocation(12) = -1 And objects = "SLIME" Then
                    Console.Out.WriteLine("You attack the " & lcase(objects) & " and throw the white powder at it, it screeches and disolves into a watery puddle.")
                    EnemyLocation(CombatCounter) = -1
                    ObjectLocation(12) = -2
                ElseIf Instr(EnemyLocation(CombatCounter), CurrentRoom) > 0 And ItemLocation(12) > -1 And objects = "SLIME" Then
                    Console.Out.WriteLine("You attack the " & lcase(objects) & " with your weapon. As you slice the Slime, it sucks the weapon from your hand and disolves it. It then continues to envelop you and slowing eat you.")
                    Console.Out.WriteLine("")
                    Console.ForegroundColor = 12
                    Console.Out.WriteLine("You lose 100 HP.")
                    Console.ForegroundColor = 15
                    Health = Health - 100
                ElseIf EnemyLocation(CombatCounter) = -1 Then
                    Console.Out.WriteLine("You have already killed the " & lcase(objects) & ".")
                Else
                    Console.Out.WriteLine("There is no " & lcase(objects) & " in this room.")
                End If
            Else
                CombatCounter = CombatCounter + 1
            End If
        Loop
        If CombatCounter = 99 Then
            Console.Out.WriteLine("You can't " & lcase(words(0)) & " the " & lcase(objects) & ".")
        End If
        Found = False
    End Sub
    '---SEARCH---
    Sub Search(ByVal objects As String)
        SearchCounter = 0
        Do Until Found = True Or SearchCounter = ubound(ObjectName)
            If Instr(ObjectName(SearchCounter), objects) > 0 Then

                Found = True
                If ObjectLocation(SearchCounter) > -1 Then
                    If Instr(ObjectLocation(SearchCounter), CurrentRoom) > 0 Then
                        Console.Out.WriteLine("You " & lcase(words(0)) & " the " & lcase(objects) & ".")
                        Console.Out.WriteLine("")
                        Console.ForegroundColor = 11
                        Console.Out.WriteLine(ObjectDescription(SearchCounter))
                        Console.ForegroundColor = 15
                        If CurrentRoom = 1 Then
                            ObjectLocation(SearchCounter) = -1
                            ItemLocation(1) = -1
                        ElseIf CurrentRoom = 8 Then
                            ObjectLocation(SearchCounter) = -1
                            ItemLocation(2) = -1
                        ElseIf CurrentRoom = 7 And objects = "PEBBLES" Then
                            ObjectLocation(SearchCounter) = -1
                            ItemLocation(5) = -1
                        End If
                    Else
                        Console.Out.WriteLine("There is no " & lcase(objects) & " in this room.")
                    End If
                Else
                    Console.Out.WriteLine("You don't find anything interesting.")
                End If

            Else
                SearchCounter = SearchCounter + 1
            End If
        Loop
        If SearchCounter = ubound(ItemName) Then
            SearchCounter = 0
            Do Until SearchCounter = ubound(EnemyName) Or Found = True
                If Instr(EnemyName(SearchCounter), objects) > 0 Then
                    Console.Out.WriteLine("")
                    Found = True
                    If EnemyLocation(SearchCounter) > -1 Then
                        If Instr(EnemyLocation(SearchCounter), CurrentRoom) > 0 Then
                            Console.Out.WriteLine("You " & lcase(words(0)) & " the " & lcase(objects) & ".")
                            Console.Out.WriteLine(EnemyDescription(SearchCounter))
                        Else
                            Console.Out.WriteLine("There's no " & lcase(objects) & " here.")
                        End If
                    Else
                        Console.Out.WriteLine("You don't find anything interesting.")
                    End If

                Else
                    SearchCounter = SearchCounter + 1
                End If
            Loop
            If SearchCounter = ubound(ItemName) Then
                SearchCounter = 0
                Do Until SearchCounter = ubound(ItemName) Or Found = True
                    If Instr(ItemName(SearchCounter), objects) > 0 Then
                        Console.Out.WriteLine("")
                        Found = True
                        If ItemLocation(SearchCounter) > -1 Then
                            If Instr(ItemLocation(SearchCounter), CurrentRoom) > 0 Then
                                Console.Out.WriteLine("You " & lcase(words(0)) & " the " & lcase(objects) & ".")
                                Console.Out.WriteLine("")
                                Console.Out.WriteLine(ItemDescription(SearchCounter))
                            Else
                                Console.Out.WriteLine("There's no " & lcase(objects) & " here.")
                            End If
                        Else
                            Console.Out.WriteLine("There's no " & lcase(objects) & " here.")
                        End If

                    Else
                        SearchCounter = SearchCounter + 1
                    End If
                Loop
            End If
            If SearchCounter = ubound(ItemName) Then
                Console.Out.WriteLine("I can't do that.")
            End If
        End If
        Found = False
    End Sub
    '---BREAK---
    Sub Break(ByVal objects As String)
        BreakCounter = 0
        Do Until Found = True Or BreakCounter = 99
            If Instr(ObjectName(BreakCounter), objects) > 0 Then
                Found = True
                If Instr(ObjectLocation(BreakCounter), CurrentRoom) > 0 Then
                    Console.Out.WriteLine("You break down the " & lcase(ObjectName(BreakCounter)) & ".")
                    ObjectLocation(BreakCounter) = -1
                Else
                    Console.Out.WriteLine("There is no " & lcase(objects) & " in this room.")
                End If

            Else
                BreakCounter = BreakCounter + 1
            End If
        Loop
        If BreakCounter = 99 Then
            Console.Out.WriteLine("I can't do that.")
        End If
        Found = False
    End Sub
    '---PICKUP---
    Sub Pickup(ByVal item As String)
        PickupCounter = 0
        If item = ItemName(1) And CurrentRoom = 1 And ItemLocation(1) = 1 And ItemDropped(1) = False Then
            PickupCounter = 99
        ElseIf item = ItemName(2) And CurrentRoom = 9 And ItemLocation(2) = 9 And ItemDropped(1) = False Then
            PickupCounter = 99
        End If
        Do Until Found = True Or PickupCounter = 99

            If Instr(ItemName(PickupCounter), item) > 0 Then
                Found = True
                If ItemLocation(PickupCounter) = -1 Then
                    Console.Out.WriteLine("You already have the " & ucase(item) & ".")
                Else
                    If Instr(ItemLocation(PickupCounter), CurrentRoom) > 0 And item = ItemName(PickupCounter) Then
                        Console.ForegroundColor = 15
                        Console.Out.Write("You take the ")
                        Console.ForegroundColor = 11
                        Console.Out.Write(ucase(ItemName(PickupCounter)))
                        Console.ForegroundColor = 15
                        Console.Out.WriteLine(".")
                        ItemLocation(PickupCounter) = -1
                    Else
                        Console.Out.WriteLine("There is no " & lcase(item) & " in this room.")
                    End If
                End If
            ElseIf Instr(Input, item) = 0 Then
                Console.Out.Writeline("You can't pick that up.")
            Else
                PickupCounter = PickupCounter + 1
            End If
        Loop
        If PickupCounter = 99 Then
            Console.Out.WriteLine("I can't do that.")
        End If
        Found = False
    End Sub
    '---UNLOCK---
    Sub Unlock(ByVal objects As String)
        UnlockCounter = 0
        Do Until Found = True Or UnlockCounter = 99
            If Instr(ObjectName(UnlockCounter), objects) > 1 Then
                Found = True
                If ObjectLocation(UnlockCounter) = -1 Then
                    Console.Out.WriteLine("The door isn't locked.")
                Else
                    If CurrentRoom = 0 Then
                        Console.Out.WriteLine("You can't unlock the door.")
                    ElseIf Instr(ObjectLocation(UnlockCounter), CurrentRoom) > 0 And ItemLocation(2) = -1 Then
                        Console.Out.WriteLine("You unlock the " & lcase(ObjectName(UnlockCounter)) & "and then open it.")
                        Console.Out.WriteLine("The key gets stuck in the lock. In your attempt to remove it, the key breaks.")
                        ObjectLocation(UnlockCounter) = -1
                        ItemLocation(2) = -2
                    ElseIf Instr(ObjectLocation(UnlockCounter), CurrentRoom) > 0 And ItemLocation(2) > -1 Then
                        Console.Out.WriteLine("You need a key to unlock the door.")
                    Else
                        Console.Out.WriteLine("There is no " & lcase(objects) & " in this room.")
                    End If
                End If
            Else
                UnlockCounter = UnlockCounter + 1
            End If
        Loop
        If UnlockCounter = 99 Then
            Console.Out.WriteLine("I can't do that.")
        End If
        Found = False
    End Sub
    '---WRITE---
    Sub Write(ByVal objects As String)
        WriteCounter = 0
        Do Until Found = True Or WriteCounter = 99
            If Instr(ObjectName(WriteCounter), objects) > 0 Then
                Found = True
                If ObjectLocation(WriteCounter) = -1 Then
                    Console.Out.WriteLine("The riddle has already been solved.")
                Else
                    If Instr(ObjectLocation(WriteCounter), CurrentRoom) > 0 And ItemLocation(4) = -1 Then
                        Console.Out.Write("What do you want to write?: ")
                        Riddle = ucase(Console.In.ReadLine())
                    End If
                    If Riddle = "SILENCE" And ObjectLocation(5) > -1 Then
                        Console.Out.Writeline("")
                        Console.Out.Writeline("The glowing magic around the sealed door dissapates.")
                        Console.Out.WriteLine("The piece of chalk crumbles to dust.")
                        ObjectLocation(5) = -1
                        ItemLocation(4) = -2

                    ElseIf CurrentRoom = ObjectLocation(4) Then
                        Console.Out.WriteLine("")
                        Console.Out.WriteLine("Nothing happens.")
                    Else
                        Console.Out.WriteLine("There is no " & lcase(objects) & " in here.")
                    End If
                    If Instr(ObjectLocation(WriteCounter), CurrentRoom) > 0 And ItemLocation(4) > -1 Then
                        Console.Out.WriteLine("")
                        Console.Out.WriteLine("You need something to write with.")
                    End If
                End If
            Else
                WriteCounter = WriteCounter + 1
            End If
        Loop
        If WriteCounter = 99 Then
            Console.Out.WriteLine("I can't do that.")
        End If
        Found = False
    End Sub
    '---DISARM---
    Sub Disarm(ByVal objects)
        DisarmCounter = 0
        Do Until Found = True Or DisarmCounter = 99
            If Instr(ObjectName(DisarmCounter), objects) > 0 Then
                Found = True
                If ObjectLocation(7) = -1 Or ObjectLocation(8) = -1 Then
                    Console.Out.WriteLine("There is nothing to disarm.")
                Else
                    If Instr(ObjectLocation(7), CurrentRoom) > 0 Or Instr(ObjectLocation(8), CurrentRoom) > 0 Then
                        Console.Out.WriteLine("You attempt to disarm the " & lcase(ObjectName(DisarmCounter)) & ".")
                        Console.Out.WriteLine("")
                        Randomize()
                        Random = Int(Rnd() * 6)
                        If Random = 0 Then
                            Console.Out.WriteLine("You accidentally trigger the poison dart trap and a thin dart launches through the air and pierces your skin.")
                            Console.Out.WriteLine("")
                            Console.ForegroundColor = 12
                            Console.Out.WriteLine("You lose 30 HP.")
                            Console.ForegroundColor = 15
                            Console.Out.WriteLine("")
                            Console.Out.WriteLine("The chest looks safe now.")
                            Health = Health - 30
                            ObjectLocation(7) = -1
                        Else
                            Console.Out.WriteLine("You succesfully disarm the trap. The chest is now safe to open.")
                            ObjectLocation(7) = -1
                            Suffix = " The Swift"
                            Console.Out.WriteLine("")
                            Console.ForegroundColor = 13
                            Console.Out.WriteLine("You earned a new title: 'The Swift'.")
                            Console.ForegroundColor = 15
                        End If
                    Else
                        Console.Out.WriteLine("There is no " & lcase(objects) & " in this room to disarm.")
                    End If
                End If
            Else
                DisarmCounter = DisarmCounter + 1
            End If
        Loop
        If DisarmCounter = 99 Then
            Console.Out.WriteLine("I can't do that.")
        End If
        Found = False
    End Sub
    '---OPEN---
    Sub Open(ByVal objects)
        OpenCounter = 0
        Do Until Found = True Or OpenCounter = 99
            If Instr(ObjectName(OpenCounter), objects) > 0 Then
                Found = True
                If Instr(ObjectLocation(OpenCounter), CurrentRoom) > 0 And objects = "CHEST" And ObjectLocation(7) = -1 Then
                    ObjectLocation(8) = -1
                    Console.Out.WriteLine("You open the " & lcase(ObjectName(OpenCounter)) & ".")
                    Console.ForegroundColor = 11
                    Console.Out.WriteLine("")
                    Console.Out.WriteLine("You find a short steel dagger.")
                    Console.Out.WriteLine("")
                    Console.ForegroundColor = 15
                    Console.Out.WriteLine("You decide to swap it for your rusty shiv.")
                    Console.Out.WriteLine("")
                    Console.ForegroundColor = 11
                    Console.Out.WriteLine("You also find a vial with a red viscous liquid. A label reads 'Health Potion'.")
                    Console.ForegroundColor = 15
                    Console.Out.WriteLine("")
                    Console.Out.WriteLine("You decide to take it.")
                    ItemLocation(1) = 1
                    ItemLocation(6) = -1
                    ItemLocation(8) = -1
                ElseIf Instr(ObjectLocation(8), CurrentRoom) > 0 And objects = "CHEST" And ObjectLocation(7) > 0 Then
                    Console.Out.WriteLine("You open the " & lcase(ObjectName(OpenCounter)) & ".")
                    Console.Out.WriteLine("")
                    Console.Out.WriteLine("You accidentally trigger the poison dart trap and a thin dart launches through the air and pierces your skin.")
                    Console.Out.WriteLine("")
                    Console.ForegroundColor = 12
                    Console.Out.WriteLine("You lose 30 HP.")
                    Console.ForegroundColor = 15
                    Console.Out.WriteLine("")
                    Console.Out.WriteLine("The chest looks safe now.")
                    Health = Health - 30
                    ObjectLocation(7) = -1
                ElseIf ObjectLocation(7) = -1 And objects = "CHEST" Then
                    Console.Out.WriteLine("The " & lcase(objects) & " is already open.")
                Else
                    Console.Out.WriteLine("You can't open the " & lcase(objects) & ".")
                End If
            Else
                OpenCounter = OpenCounter + 1
            End If
        Loop
        If OpenCounter = 99 Then
            Console.Out.WriteLine("That isn't possible.")
        End If
        Found = False
    End Sub
    '---DROP---
    Sub Drop(ByVal objects As String)
        DropCounter = 0
        Do Until Found = True Or DropCounter = 99
            If Instr(ItemName(DropCounter), objects) > 0 Then
                Found = True
                If ItemLocation(DropCounter) = -1 And objects = ItemName(DropCounter) Then
                    Console.Out.Write("You drop the ")
                    Console.ForegroundColor = 11
                    Console.Out.Write(ucase(objects))
                    Console.ForegroundColor = 15
                    Console.Out.WriteLine(" on the floor.")
                    ItemLocation(DropCounter) = CurrentRoom
                    ItemDropped(DropCounter) = True
                Else
                    Console.Out.WriteLine("You don't have a " & ucase(objects) & ".")
                End If
            Else
                DropCounter = DropCounter + 1
            End If
        Loop
        If DropCounter = 99 And Words(1) = "THE BASS" Then
            Console.Out.WriteLine("WUB WUB WUB")
        ElseIf DropCounter = 99 Then
            Console.Out.WriteLine("You don't have a " & ucase(objects) & ".")
        End If
        Found = False
    End Sub
    '---READ---
    Sub Read(ByVal objects As String)
        If CurrentRoom = 15 Then
            If ObjectLocation(9) = -1 Then
                Console.Out.WriteLine("All 3 books are sealed. There is no way to open them.")
            Else
                If objects = "BOOK" Then
                Else
                    Randomize()
                    objects = Int(Rnd() * 3)
                End If
                Select Case objects
                    Case Is = 0
                        Console.Out.WriteLine("You read the book.")
                        Console.Out.WriteLine("")
                        Console.Out.WriteLine("The book flashes suddenly and you feel replenished.")
                        ObjectLocation(9) = -1
                        ObjectLocation(10) = -1
                        ObjectLocation(11) = -1
                        Console.Out.WriteLine("")
                        Console.Out.WriteLine("All 3 books then seal themselves.")
                        Console.Out.WriteLine("")
                        Health = 100
                        Console.Foregroundcolor = 10
                        Console.Out.WriteLine("Your health is now restored.")
                        Console.Foregroundcolor = 15
                    Case Is = 1
                        Console.Out.WriteLine("You read the book.")
                        Console.Out.WriteLine("")
                        Console.Out.WriteLine("You feel stronger as you are empowered with fortitude. ")
                        ObjectLocation(9) = -1
                        ObjectLocation(10) = -1
                        ObjectLocation(11) = -1
                        Console.Out.WriteLine("")
                        Console.Out.WriteLine("All 3 books then seal themselves")
                        Console.Out.WriteLine("")
                        Health = Health + 25
                        Console.Foregroundcolor = 10
                        Console.Out.WriteLine("You gain 25 HP.")
                        Console.Foregroundcolor = 15
                    Case Is = 2
                        Console.Out.WriteLine("You read the book.")
                        Console.Out.WriteLine("")
                        Console.Out.WriteLine("The book begins to vibrate and a dark cloud swarms your face.")
                        Console.Out.WriteLine("Your muscles tense and you feel an enormous pain.")
                        Console.Out.WriteLine("The pain stops and all 3 books then seal themselves.")
                        ObjectLocation(9) = -1
                        ObjectLocation(10) = -1
                        ObjectLocation(11) = -1
                        Console.Out.WriteLine("")
                        Console.Out.WriteLine("All 3 books then seal themselves")
                        Console.Out.WriteLine("")
                        Health = Health - 40
                        Console.Foregroundcolor = 12
                        Console.Out.WriteLine("You lose 40 health.")
                        Console.Foregroundcolor = 15
                    Case Is = "BOOK"
                        If CurrentRoom = 15 Then
                            Console.Out.WriteLine("Which book would you like to do read?")
                            Console.Out.WriteLine("")
                            Console.ForegroundColor = 14
                            Console.Out.Write(">")
                            Console.ForegroundColor = 15
                            Input = Console.In.ReadLine()
                            Console.Out.WriteLine("")
                            Randomize()
                            Input = Int(Rnd() * 3)
                            Select Case Input
                                Case Is = "0"
                                    Console.Out.WriteLine("You read the book.")
                                    Console.Out.WriteLine("")
                                    Console.Out.WriteLine("The book flashes suddenly and you feel replenished.")
                                    ObjectLocation(9) = -1
                                    ObjectLocation(10) = -1
                                    ObjectLocation(11) = -1
                                    Console.Out.WriteLine("")
                                    Console.Out.WriteLine("All 3 books then seal themselves.")
                                    Console.Out.WriteLine("")
                                    Health = 100
                                    Console.Foregroundcolor = 10
                                    Console.Out.WriteLine("Your health is now restored.")
                                    Console.Foregroundcolor = 15
                                Case Is = "1"
                                    Console.Out.WriteLine("You read the book.")
                                    Console.Out.WriteLine("")
                                    Console.Out.WriteLine("You feel stronger as you are empowered with fortitude. ")
                                    ObjectLocation(9) = -1
                                    ObjectLocation(10) = -1
                                    ObjectLocation(11) = -1
                                    Console.Out.WriteLine("")
                                    Console.Out.WriteLine("All 3 books then seal themselves")
                                    Console.Out.WriteLine("")
                                    Health = Health + 25
                                    Console.Foregroundcolor = 10
                                    Console.Out.WriteLine("You gain 25 HP.")
                                    Console.Foregroundcolor = 15
                                Case Is = "2"
                                    Console.Out.WriteLine("You read the book.")
                                    Console.Out.WriteLine("")
                                    Console.Out.WriteLine("The book begins to vibrate and a dark cloud swarms your face.")
                                    Console.Out.WriteLine("Your muscles tense and you feel an enormous pain.")
                                    Console.Out.WriteLine("The pain stops and all 3 books then seal themselves.")
                                    ObjectLocation(9) = -1
                                    ObjectLocation(10) = -1
                                    ObjectLocation(11) = -1
                                    Console.Out.WriteLine("")
                                    Console.Out.WriteLine("All 3 books then seal themselves")
                                    Console.Out.WriteLine("")
                                    Health = Health - 40
                                    Console.Foregroundcolor = 12
                                    Console.Out.WriteLine("You lose 40 health.")
                                    Console.Foregroundcolor = 15
                                Case Else
                                    Console.Out.WriteLine("I can't do that..")
                            End Select
                        Else
                            Console.Out.WriteLine("There's no " & lcase(objects) & "to read.")
                        End If
                    Case Else
                        Console.Out.WriteLine("I don't understand what you want me to do...")
                End Select
            End If
        ElseIf CurrentRoom = 36 Then
            Select Case objects
                Case Is = "BOOK"
                    Console.Out.Write("Which book would you like to read? ")
                    Console.ForegroundColor = 12
                    Console.Out.Write("RED ")
                    Console.ForegroundColor = 15
                    Console.Out.Write("or ")
                    Console.ForegroundColor = 9
                    Console.Out.Write("BLUE")
                    Console.ForegroundColor = 15
                    Console.Out.WriteLine("?")
                    Console.ForegroundColor = 14
                    Console.Out.Write(">")
                    Input = ucase(Console.In.ReadLine())
                    Console.ForegroundColor = 15
                    Console.Out.WriteLine("")
                    Select Case Input
                        Case Is = "RED"
                            If ObjectLocation(12) > 0 Then
                                Console.Out.WriteLine("You open the red book to find the pages blank.")
                                Console.Out.WriteLine("")
                                Console.ForegroundColor = 11
                                Console.Out.WriteLine("A slight tear reveals a blue gemstone in the binding of the book.")
                                Console.ForegroundColor = 15
                                Console.Out.WriteLine("")
                                Console.Out.WriteLine("You decide to take it.")
                                ItemLocation(7) = -1
                                ObjectLocation(12) = -1
                            Else
                                Console.Out.WriteLine("You read the book.")
                                Console.Out.WriteLine("")
                                Console.Out.WriteLine("You don't find anything interesting.")
                            End If
                        Case Is = "BLUE"
                            If ObjectLocation(13) > 0 Then
                                Console.Out.WriteLine("You open the blue book, a burst of fire is released from the pages and scorches your face, Looks like you just got owned.")
                                Console.Out.WriteLine("")
                                Console.ForegroundColor = 12
                                Health = Health - 35
                                Console.Out.WriteLine("You lose 35 Health")
                                Console.Out.WriteLine("")
                                Suffix = " The Scorched"
                                Console.Out.WriteLine("")
                                Console.ForegroundColor = 13
                                Console.Out.WriteLine("You earned a new title: 'The Scorched'.")
                                Console.ForegroundColor = 15
                                ObjectLocation(13) = -1
                            Else
                                Console.Out.WriteLine("Are you such a simplton that you would try reading this again?")
                            End If
                    End Select
                Case Is = "RED"
                    Console.Out.WriteLine("You open the red book to find the pages blank.")
                    Console.Out.WriteLine("")
                    Console.ForegroundColor = 11
                    Console.Out.WriteLine("A slight tear reveals a blue gemstone in the binding of the book.")
                    Console.ForegroundColor = 15
                    Console.Out.WriteLine("")
                    Console.Out.WriteLine("You decide to take it.")
                    ItemLocation(7) = -1
                    ObjectLocation(12) = -1
                Case Is = "BLUE"
                    Console.Out.WriteLine("You open the blue book, a burst of fire is released from the pages and scorches your face, Looks like you just got owned.")
                    Console.Out.WriteLine("")
                    Console.ForegroundColor = 12
                    Health = Health - 35
                    Console.Out.WriteLine("You lose 35 Health")
                    Console.Out.WriteLine("")
                    Suffix = " The Scorched"
                    Console.Out.WriteLine("")
                    Console.ForegroundColor = 13
                    Console.Out.WriteLine("You earned a new title: 'The Scorched'.")
                    Console.ForegroundColor = 15
                    ObjectLocation(13) = -1
            End Select
        Else
            Console.Out.WriteLine("There's nothing to read..")
        End If
    End Sub
    '---ROOMS---
    Sub RoomInitialise() 'Declare room descriptions
        CurrentRoom = 0 'Start in Room 0
        Intro = "Welcome to the world of DungeonScape! This is a dangerous and confusing world, and it must be explored with care! Can you defeat the evil of Dungeonscape?"
        Room(0) = "You awaken; you open your eyes to find yourself in a dim dungeon prison cell. You can barely see the rusted iron bars surrounding you. Perhaps you should try and find a way out of this cell, or you could just sit here and rot. You make out the door of your cell to the east. It doesn't look very strong."
        Room(1) = "As you step out of the cell you notice a corpse hanging on the wall by some iron chains. You can continue exploring the room to the south or go back into your cell which is to the west."
        Room(2) = "A guard is leaning back in his chair facing the wall ahead of you. He seems oblivious to your presence. A small candle is burning on his desk sending your shadow dancing across the walls around you. There is a door just to the east of the guard. I would suggest taking out the guard with something first. You could also return to the south."
        Room(3) = "You leave the guards office. You find yourself in a long narrow tunnel. The smell of moss makes you cough. The tunnel continues to the east. To the west is the guards office."
        Room(4) = "As you continue down the tunnel you feel the room temperature plummet. You accidentally step into a freezing cold puddle of a muddy solution. You know for sure this place is uninhabited by humans. To the west is a tunnel. To the east is the end of the tunnel."
        Room(5) = "You find yourself outside a large open doorway to the east. A small sign written in scrawly handwriting reads:'KCAB EMOC TNOD'. Perhaps it’s a code? To the west is the tunnel back to the guard's office."
        Room(6) = "As you step through the doorway, it seals itself behind you almost instantly. Then all goes silent. The room is empty apart from a large tablet in the centre of the room. As you get closer you notice a small stick of chalk beside the tablet. After closer examination you notice a riddle on the tablet, written in similar scrawly handwriting. It reads 'Say my name and I disappear. What am I?'. To the east is a locked door."
        Room(7) = "You enter a small square shaped room. As you enter a smell creeps up your nostrils making your eyes water. There is a strong smell of poison in the room. The floor is cluttered with gold coins and a red carpet leads to the centre of the room. At the centre lies a pedestal with a large chest on top. Upon closer inspection you notice the coins that surround you are just pebbles painted yellow. The whole room gives you the chills. As you step closer to the chest the smell gets stronger. Perhaps you should be careful opening this chest. Another door is located to the south."
        Room(8) = "You’ve followed the noise to a dead end but you still can’t locate the source. A small shimmer catches your eye. It’s coming from the base of the wall. You bend down to inspect it closer. It seems to be a small mouse hole. You hear the squeaking again and an agitated rat runs past your face revealing a sparkling glint inside the hole. There is a door to the east."
        Room(9) = "You can still feel the eerie presence of the rotting corpse as you leave it behind. You then realise you’ve arrived at a crossroads of sorts. You feel the darkness begin to encompass you. To the east is a large strong door that is locked. It is too strong to break open. In the small crack underneath the door you see candlelight flickering and shadows moving. A sharp screeching noise grabs your attention which is followed by a quiet rustle. It came from the hallway to your west. You could also return to the north."
        Room(10) = "You find yourself in a dimly lit room. The smell of rum brings a smile onto your face. To the north is a guard leaning back on his chair. You can either head north to the guard or go back to the west."
        Room(11) = "Empty Plane."
        Room(12) = "You ascend the steep moss covered steps, sweat dripping off your worn out body. As you reach the top, the sunlight blinds you."
        Room(13) = "You reach the turning and the hallway continues down south. From here you can only see darkness, watch your step. Perhaps you should bring a light source."
        Room(14) = "As you walk through the door you notice yourself in another dim lit corrider, this being a lot narrower than others. Another torch is placed on the wall. You can see a turning to the west, or you can walk back through the door to the east."
        Room(15) = "You enter a large room. Covering all the walls are shelves upon which are very expensive looking books. All of these books are out of your reach. At the centre of the room is a desk. Upon it are 3 books. They all look similar apart from large gold numerals on the front cover of each book. The first book reads 'I', the second reads 'II' & the third reads 'III'. There is a door to the west and a door to the north from where you came from."
        Room(16) = "Room 16"
        Room(17) = "Room 17"
        Room(18) = "Room 18"
        Room(19) = "Room 19"
        Room(20) = "Room 20"
        Room(21) = "You use your torch to light the way and manage to spot a crevice in the floor. You manage to manouver around it and carry on. The hallway continues to the south and to the north."
        Room(22) = "Empty Plane."
        Room(23) = "Empty Plane."
        Room(24) = "Room 24"
        Room(25) = "Room 25"
        Room(26) = "Room 26"
        Room(27) = "Empty Plane."
        Room(28) = "As you walk in you quickly have to duck out of the way of a book that seems to have been launched at you. You scan the room to try to spot someone that could’ve done this, but to no avail. As you take a closer look at the room, you notice under the eerie purple light that the books are flying from shelf to shelf. Flapping their pages like wings as they glide across the room, seemingly ignoring your presence. Two books however are completely immobile and are placed on a podium to the south."
        Room(29) = "You are at the end of a dark corridor, you can see a green light glowing in the east and a purple light glowing in the west. There is also a path to the south."
        Room(30) = "You follow the green light into a small corridor. The walls are lined with the odd pipe here and there, they seem poorly maintained and have started leaking a glowing green goo from the joints. The goo seems to have formed a puddle in the centre of the corridor.  A trail of the goo leads away to he east and the hallway remains to your west."
        Room(31) = "The trail disappears down what seems to be a manhole into a sewer. It looks badly damaged, as if it's been forced open. The lid is covered in goo and the ladder leading down looks deceitfully unstable. You hear a small gargling sound emanating from the sewers and the same green glow is visible."
        Room(32) = "Room 32"
        Room(33) = "Room 33"
        Room(34) = "Empty Plane."
        Room(35) = "A large skull is hanging from a chain in the ceiling. Whispers surrounding it and as you approach it you feel as if you're being watched. The closer you get to it the stronger this feeling gets. The room continues to the South."
        Room(36) = "You approach the two books decorated in elaborate patterns and symbols, the only difference between the two is that one is decorated in bold red patterns resembling that of a flame, whereas the other is decorated in blue which brings the ocean instantly to mind. You have the urge to open one of the books, are you going to resist and head back north or are you going to give in to curiosity?"
        Room(37) = "You're in a long dark corridoor, a thick dust fills the air and you can hardly make out what's infront of you. The corridoor continues to the South and a hole in a collapsed wall leads to the North."
        Room(38) = "Empty Plane."
        Room(39) = "You've descended into what looks like a sewer. The smell of faeces and decay fills your nostrils. You hear the gargling noise coming from the South. The manhole out is to the North"
        Room(40) = "Room 40"
        Room(41) = "Room 41"
        Room(42) = "How can you stand this smell for so long!? The corpses are still here and there doesnt seem to be anything interesting. The room continues to the East and to the South."
        Room(43) = "Rotting corpses cover the floor in piles and skeletons hang from chains in the walls. The smell is horrific, you should find a way out of here quickly. Two doors lead to the East and to the South. The room continues to the North and to the West."
        Room(44) = "The air in this room feels like heaven to your starved body. However there doesnt seem to be anything useful in here. There are two doors leading to the East or the the West."
        Room(45) = "The dust in the air gets thicker and starts to make it hard to breath you can make out a door to the West and the corridoor continues to the North and to the South."
        Room(46) = "You've come across a dead end. There's a small green gemstone on the floor. It may be of use later. The sewer continues behind you."
        Room(47) = "You've followed the strange noise through the sewer. Before you is a large green Slime, it seems to be moving on it's own. Bubbles rise through the Slime and burst ontop creating the gargling noise you heard previously. It starts to drag it's gooey self towards you. You better do something fast, I don't think it's friendly. The sewer goes back North and carries on South. The wall to the West looks rather peculier. Perhaps I should investigate?"
        Room(48) = "Room 48"
        Room(49) = "Room 49"
        Room(50) = "Rotting corpses cover the floor in piles and skeletons hang from chains in the walls. The smell is horrific, you should find a way out of here quickly. A door leads to the West and the room continues to the North."
        Room(51) = "You're in looks like an abandoned butcher's pantry. Carcasses hang from hooks all over the room and the pungent smell of rotting meat hit's you like a truck. The room continues to the South and a door leads to the North."
        Room(52) = "Empty Plane"
        Room(53) = "You're in a long dark corridoor, a thick dust fills the air and you can hardly make out what's infront of you. The corridoor continues to the north and a door leads to the South."
        Room(54) = "Empty Plane."
        Room(55) = "You continue south down the wretched sewer, mutated rats are crawling across the walls a ceilings. You see a peculiar looking stick in one of the sewer grills. You can continue south or return north."
        Room(56) = "Room 56"
        Room(57) = "Room 57"
        Room(58) = "You're in looks like an abandoned butcher's pantry. Carcasses hang from hooks all over the room and the pungent smell of rotting meat hit's you like a truck. The room continues to the East and a door leads to the West."
        Room(59) = "An abomination of meat and flesh stands infront of you! He's holding a bloodied meat cleaver in one hand and a rusty hook in the other and seems to have taken a liking to a large flour sack that he's fitted over his head. Cold dead eyes are visible through two rough holes in his 'Hat'. He lets out a large groan as he smashes the cleaver on the floor causing the whole room to shake violently."
        Room(60) = "You're in looks like an abandoned butcher's pantry. Carcasses hang from hooks all over the room and the pungent smell of rotting meat hit's you like a truck. The room continues to the west and a door leads to the East."
        Room(61) = "More shelves surround you filled with stale food and roten meat. You can see a small container of white powder on the top shelf. A door leads to the North and to the West. The rest of the storeroom is behind you to the East."
        Room(62) = "You're in part of an abandoned store room. Potions and food line the shelves in abundance, cobwebs line the walls and you wonder if anything here is actually safe to use. The room continues to the West and a large hole in the floor is to the East."
        Room(63) = "The sewer seems to have been blocked with some gunk, you cant go forward anymore. There's an old wooden ladder next to you. The sewer continues back North."
    End Sub

    Sub InitialiseExits() 'Declare Exits
        Exits(0) = "EAST"
        Exits(1) = "SOUTH WEST"
        Exits(2) = "EAST SOUTH"
        Exits(3) = "EAST WEST"
        Exits(4) = "EAST WEST"
        Exits(5) = "EAST WEST"
        Exits(6) = "EAST"
        Exits(7) = "WEST SOUTH"
        Exits(8) = "EAST"
        Exits(9) = "NORTH EAST WEST"
        Exits(10) = "NORTH WEST"
        Exits(11) = ""
        Exits(12) = "SOUTH"
        Exits(13) = "EAST SOUTH"
        Exits(14) = "EAST WEST"
        Exits(15) = "NORTH WEST"
        Exits(16) = "SOUTH"
        Exits(17) = "EAST"
        Exits(18) = "EAST WEST SOUTH"
        Exits(19) = "WEST EAST"
        Exits(20) = "WEST NORTH"
        Exits(21) = "NORTH SOUTH"
        Exits(22) = ""
        Exits(23) = ""
        Exits(24) = "NORTH EAST"
        Exits(25) = "EAST WEST SOUTH"
        Exits(26) = "NORTH WEST"
        Exits(27) = ""
        Exits(28) = "EAST SOUTH"
        Exits(29) = "NORTH EAST WEST SOUTH"
        Exits(30) = "EAST WEST"
        Exits(31) = "WEST SOUTH"
        Exits(32) = "EAST SOUTH"
        Exits(33) = "NORTH WEST SOUTH"
        Exits(34) = ""
        Exits(35) = "SOUTH"
        Exits(36) = "NORTH"
        Exits(37) = "SOUTH"
        Exits(38) = ""
        Exits(39) = "NORTH SOUTH"
        Exits(40) = "NORTH EAST"
        Exits(41) = "SOUTH NORTH WEST"
        Exits(42) = "EAST SOUTH"
        Exits(43) = "NORTH WEST EAST SOUTH"
        Exits(44) = "EAST WEST"
        Exits(45) = "NORTH SOUTH WEST"
        Exits(46) = "WEST"
        Exits(47) = "NORTH SOUTH WEST"
        Exits(48) = "EAST"
        Exits(49) = "SOUTH NORTH WEST EAST"
        Exits(50) = "NORTH WEST"
        Exits(51) = "SOUTH NORTH"
        Exits(52) = ""
        Exits(53) = "NORTH"
        Exits(54) = ""
        Exits(55) = "NORTH SOUTH"
        Exits(56) = ""
        Exits(57) = "NORTH EAST"
        Exits(58) = "WEST EAST"
        Exits(59) = "WEST EAST NORTH"
        Exits(60) = "EAST WEST"
        Exits(61) = "EAST WEST"
        Exits(62) = "EAST WEST"
        Exits(63) = "NORTH WEST"
    End Sub

    Sub InitialiseVisited() 'Declare Visited Rooms
        For x = 0 To 63
            Visited(x) = False
        Next
    End Sub

    Sub InitialiseRoomV() 'Declare RoomV
        RoomV(0) = "You step back into your cell. The broken remains of the door lie on the floor. There doesn't seem to be anything interesting in here. There is an exit to the east."
        RoomV(1) = "You notice the corpse is still hanging on the wall by some iron chains. You can continue exploring the room to the south or enter your cell to the west."
        RoomV(2) = "The dead guard is lying on the floor with the dirty shiv still in his neck. A small candle is burning on the desk sending your shadow dancing across the walls around you. There is a door to the east or you can return to the south."
        RoomV(3) = "You are standing outside the guards office. The tunnel continues to the east. To the west is the guards office."
        RoomV(4) = "As you continue down the tunnel you remind yourself of the puddle and manage to avoid it. The tunnel continues to the west and east. To west is the guard's office. To the east is the end of the tunnel."
        RoomV(5) = "You find yourself outside a large metal door. A small sign written in scrawly handwriting reads 'KCAB EMOC T’NOD'. Perhaps it’s a code? To the west is a tunnel."
        RoomV(6) = "The room is empty. To the east is a once magical door. To the west is the coded door that has no way of entry."
        RoomV(7) = "You enter a small square shaped room. There is a strong smell of poison in the room. The floor is cluttered with yellow pebbles and a red carpet leads to the centre of the room. At the centre lies a pedestal with a large chest on top. Perhaps you should be careful opening this chest. There is a door to the west and one to the south."
        RoomV(8) = "You’ve followed the noise to a dead end but you still can’t locate the source. A small shimmer catches your eye. It’s coming from the base of the wall. You bend down to inspect it closer. It seems to be a small mouse hole. There is a door to the east."
        RoomV(9) = "You’ve arrived at a crossroads of sorts. To the east is a large strong door. It is too strong to break open. In the small crack underneath the door you see candlelight flickering. A sharp screeching noise grabs your attention which is followed by a quiet rustle. It came from the hallway to your west. You could also return to the corpse which is to your north."
        RoomV(10) = "You find yourself in a dimly lit room. The smell of rum brings a smile to your face. The room continues north or you can go back west."
        RoomV(11) = ""
        RoomV(12) = ""
        RoomV(13) = "You reach the turning and the hallway continues down south. From here you can only see darkness, watch your step."
        RoomV(14) = "As you walk through the door you notice yourself in another dim lit corrider, this being a lot narrower than others. You can see a turning to the west, or you can walk back through the door to the east."
        RoomV(15) = "You enter a large room. Covering all the walls are shelves upon which are very expensive looking books. All of these books are out of your reach. At the centre of the room is a desk. Upon it are 3 books titled '1', '2' & '3'. There is a door to the west and a door to the north from where you came from."
        RoomV(16) = ""
        RoomV(17) = ""
        RoomV(18) = ""
        RoomV(19) = ""
        RoomV(20) = ""
        RoomV(21) = "You use your torch to light the way and manage to spot a crevice in the floor. You manage to manouver around it and carry on. The hallway continues to the south and to the north."
        RoomV(22) = ""
        RoomV(23) = ""
        RoomV(24) = ""
        RoomV(25) = ""
        RoomV(26) = ""
        RoomV(27) = ""
        RoomV(28) = "The books are still flapping above you blissfully unaware of your 'interference'. The podium is to the south and a door is to the east."
        RoomV(29) = "You are at the end of a dark corridor, you can see a green light glowing in the east and a purple light glowing in the west."
        RoomV(30) = "The walls are lined with the odd pipe here and there, they seem poorly maintained and have started leaking a glowing green goo from the joints. The goo seems to have formed a puddle in the centre of the corridor.  A trail of the goo leads away to the east and the hallway remains to your west."
        RoomV(31) = "A trail of goo disappears down what seems to be a manhole into a sewer. It looks badly damaged, as if it's been forced open. The lid is covered in the goo and the ladder leading down looks deceitfully unstable. You hear a small gargling sound emanating from the sewers and a green glow is visible."
        RoomV(32) = ""
        RoomV(34) = ""
        RoomV(35) = ""
        RoomV(36) = "You approach the podium holding the two coloured books. you feel a slight unease around them and decide to leave them alone. The only way out is back north."
        RoomV(37) = "You're in a long dark corridoor, a thick dust fills the air and you can hardly make out what's infront of you. The corridoor continues to the South and a hole in a collapsed wall leads to the North."
        RoomV(38) = ""
        RoomV(39) = "You're in what looks like a sewer. The smell of faeces and decay fills your nostrils. You hear the gargling noise coming from the South. The manhole out is to the North"
        RoomV(40) = "A dim red glow is emanating from the corner of the room. A strange whirring sound echoes off the damp stone walls. The room continues to the North and East"
        RoomV(41) = "A dim red glow is emanating from the corner of the room. A strange whirring sound echoes off the damp stone walls. The room continues to the North and West. A door leads to the South."
        RoomV(42) = "How can you stand this smell for so long!? The corpses are still here and there doesnt seem to be anything interesting. The room continues to the East and to the South."
        RoomV(43) = "Rotting corpses cover the floor in piles and skeletons hang from chains in the walls. The smell is horrific, you should find a way out of here quickly. Two doors lead to the East and to the South. The room continues to the North and to the West."
        RoomV(44) = "The air in this room feels like heaven to your starved body. However there doesnt seem to be anything useful in here. There are two doors leading to the East or the the West."
        RoomV(45) = "The dust in the air gets thicker and starts to make it hard to breath you can make out a door to the West and the corridoor continues to the North and to the South."
        RoomV(46) = "You've come across a dead end. There's a small green gem on the floor. It may be of use later. The sewer continues behind you."
        RoomV(47) = "There is a large green watery pool of where the slime once was. You can go back north or south or check the room to the west."
        RoomV(48) = "The smog seems to have faded and you;ve reached a dead end. An old key hangs off of a nail. The room continues to the East."
        RoomV(49) = "The smog settles around your waist, but you hear a small growl behind you and something brush up against your leg. You turn around but are unable to spot the culprit. You hear a faint scuttle and see the smog move in the distance. The room continues to the South and to the West. A door leads to the North."
        RoomV(50) = "Rotting corpses cover the floor in piles and skeletons hang from chains in the walls. The smell is horrific, you should find a way out of here quickly. A door leads to the West and the room continues to the North."
        RoomV(51) = "You're in what looks like an abandoned butcher's pantry. Carcasses hang from hooks all over the room and the pungent smell of rotting meat hit's you like a truck. The room continues to the South and a door leads to the North."
        RoomV(52) = ""
        RoomV(53) = "You're in a long dark corridoor, a thick dust fills the air and you can hardly make out what's infront of you. The corridoor continues to the north and a door leads to the South."
        RoomV(54) = ""
        RoomV(55) = "You're nearing the end of the sewer, mutated rats are crawling across the walls a ceilings. You see a peculiar looking stick in one of the sewer grills. You can continue south or return north."
        RoomV(56) = "Room 56"
        RoomV(57) = "A smog envelopes you as you make your way through the room. Voices echoe through the room but there doesn't seem to be a source. The room continues to the North and a door leads to the East."
        RoomV(58) = "You're in what looks like an abandoned butcher's pantry. Carcasses hang from hooks all over the room and the pungent smell of rotting meat hit's you like a truck. The room continues to the East and a door leads to the West."
        RoomV(59) = "An abomination of meat and flesh stands infront of you! He's holding a bloodied meat cleaver in one hand and a rusty hook in the other and seems to have taken a liking to a large flour sack that he's fitted over his head. Cold dead eyes are visible through two rough holes in his 'Hat'. He lets out a large groan as he smashes the cleaver on the floor causing the whole room to shake violently."
        RoomV(60) = "You're in what looks like an abandoned butcher's pantry. Carcasses hang from hooks all over the room and the pungent smell of rotting meat hit's you like a truck. The room continues to the west and a door leads to the East."
        RoomV(61) = "Shelves surround you filled with stale food and rotten meat. A door leads to the North and to the West. The rest of the storeroom is behind you to the East."
        RoomV(62) = "You're in part of an abandoned store room. Potions and food line the shelves in abundance, cobwebs line the walls and you wonder if anything here is actually safe to use. The room continues to the West and a large hole in the floor is to the East."
        RoomV(63) = "The sewer seems to have been blocked with some gunk, you cant get past it. There's an old wooden ladder to the west. The sewer continues back North."
    End Sub
    '---ITEMS---
    Sub InitialiseItems()
        ItemName(0) = "TORCH"                                                       'TORCH
        ItemDescription(0) = "It's burning brightly. Better not touch it!"
        ItemLocation(0) = 14
        ItemDropped(0) = False

        ItemName(1) = "SHIV"                                                        'SHIV
        ItemDescription(1) = "It's a bit blunt but should still do some damage."
        ItemLocation(1) = 1
        ItemDropped(1) = False

        ItemName(2) = "RUSTY KEY"                                                   'RUSTY KEY
        ItemDescription(2) = "It must open a locked door somewhere."
        ItemLocation(2) = 8
        ItemDropped(2) = False

        ItemName(3) = "STICK"                                                       'SAVAGE STICK
        ItemDescription(3) = "It has the word 'SAVAGE' crudely carved into its side."
        ItemLocation(3) = 55
        ItemDropped(3) = False

        ItemName(4) = "CHALK"                                                       'Chalk
        ItemDescription(4) = "A small crumbly piece of white chalk."
        ItemLocation(4) = 6
        ItemDropped(4) = False

        ItemName(5) = "YELLOW GEMSTONE"                                             'Yellow Gemstone
        ItemDescription(5) = "It's so pretty!"
        ItemLocation(5) = 7
        ItemDropped(5) = False

        ItemName(6) = "STEEL DAGGER"                                                'Steel Dagger
        ItemDescription(6) = "It's small but powerful."
        ItemLocation(6) = 7
        ItemDropped(6) = False

        ItemName(7) = "BLUE GEMSTONE"                                               'Blue Gemstone
        ItemDescription(7) = "It's so pretty!"
        ItemLocation(7) = 36
        ItemDropped(7) = False

        ItemName(8) = "HEALTH POTION"                                               'Health Potion
        ItemDescription(8) = "It will heal some health."
        ItemLocation(8) = 7
        ItemDropped(8) = False

        ItemName(9) = "EMPTY VIAL"                                                  'Empty Vial
        ItemDescription(9) = "An empty container."
        ItemLocation(9) = -2
        ItemDropped(9) = False

        ItemName(10) = "GREEN GEMSTONE"                                             'Green Gemstone
        ItemDescription(10) = "It's so... actually its quite dirty."
        ItemLocation(10) = 46
        ItemDropped(10) = False

        ItemName(11) = "RED GEMSTONE"                                               'Red Gemstone
        ItemDescription(11) = "It's so pretty!"
        ItemLocation(11) = 33
        ItemDropped(11) = False

        ItemName(12) = "POWDER"                                                     'Powder
        ItemDescription(12) = "A curious white powder."
        ItemLocation(12) = 61
        ItemDropped(12) = False

        ItemName(13) = "BLOODY CLEAVER"                                             'Bloody Cleaver
        ItemDescription(13) = "It's covered in blood and rust."
        ItemLocation(13) = 59
        ItemDropped(13) = False

        ItemName(14) = "SKULL"                                                      'Skull
        ItemDescription(14) = "An ominous Skull, you feel unwelcome."
        ItemLocation(14) = 35
        ItemDropped(14) = False

    End Sub
    '---OBJECTS---
    Sub InitialiseObjects()
        ObjectName(0) = "DOOR"                                                      'Door
        ObjectDescription(0) = "It doesn’t look very strong. Perhaps you could break it?"
        ObjectLocation(0) = 0

        ObjectName(1) = "CORPSE"                                                    'Corpse
        ObjectDescription(1) = "You find a blunt shiv attached to the corpse's leather belt."
        ObjectLocation(1) = 1

        ObjectName(2) = "HOLE"                                                      'Hole
        ObjectDescription(2) = "You find a rusty key."
        ObjectLocation(2) = 8

        ObjectName(3) = "LOCKED DOOR"                                               'Locked Door
        ObjectDescription(3) = "The door is locked. Perhaps you should find a key to open it."
        ObjectLocation(3) = 9

        ObjectName(4) = "TABLET"                                                    'Tablet
        ObjectDescription(4) = "A stone tablet dripping with magical properties."
        ObjectLocation(4) = 6

        ObjectName(5) = "MAGIC DOOR"                                                'Magic Door
        ObjectDescription(5) = "This door is magically sealed by a riddle."
        ObjectLocation(5) = 6

        ObjectName(6) = "PEBBLES"                                                   'Pebbles
        ObjectDescription(6) = "Among the yellow pebbles you find a sparkling yellow gemstone."
        ObjectLocation(6) = 7

        ObjectName(7) = "TRAP"                                                      'Trap
        ObjectDescription(7) = "A small wire leads from the chest lid to the keyhole. Perhaps you should disarm it?"
        ObjectLocation(7) = 7

        ObjectName(8) = "CHEST"                                                     'Chest
        ObjectDescription(8) = "It seems to be booby trapped. Perhaps you should disarm it?"
        ObjectLocation(8) = 7

        ObjectName(9) = "BOOK 1"                                                    'Book 1
        ObjectDescription(9) = "It's just a dusty old book. A big 'I' is printed on the front."
        ObjectLocation(9) = 15

        ObjectName(10) = "BOOK 2"                                                   'Book 2
        ObjectDescription(10) = "It's just a dusty old book. A big 'II' is printed on the front."
        ObjectLocation(10) = 15

        ObjectName(11) = "BOOK 3"                                                   'Book 3
        ObjectDescription(11) = "It's just a dusty old book. A big 'III' is printed on the front."
        ObjectLocation(11) = 15

        ObjectName(12) = "RED BOOK"                                                 'Red Book
        ObjectDescription(12) = "A large red book."
        ObjectLocation(12) = 36

        ObjectName(13) = "BLUE BOOK"                                                'Blue Book
        ObjectDescription(13) = "A large blue book."
        ObjectLocation(13) = 36

    End Sub
    '---ENEMIES---
    Sub InitialiseEnemy()
        EnemyName(0) = "GUARD"                                                      'Guard
        EnemyDescription(0) = "He is leaning back on his chair and hasn’t noticed you."
        EnemyLocation(0) = 2

        EnemyName(1) = "SLIME"                                                      'Slime
        EnemyDescription(1) = "I giant green globby mass, no ordinary weapon would work againts it."
        EnemyLocation(1) = 47

        EnemyName(2) = "ABOMINATION"                                                        'Abomination
        EnemyDescription(2) = "I giant amalgamation of corpses crudely sewn together, this is a mighty foe."
        EnemyLocation(2) = 59

        EnemyName(3) = "WARLOCK"                                                        'Warlock
        EnemyDescription(3) = "A master of dark magic and demons. His powers are blocking your way"
        EnemyLocation(3) = 33

    End Sub
    '---INVENTORY---
    Sub Inventory()
        'Inventory UI --------
        Console.ForegroundColor = 14
        Console.Out.WriteLine(ucase(left(Name, 1)) & lcase(mid(Name, 2, NameLength - 1)) & Suffix & "'s Inventory")
        Console.ForegroundColor = 15
        Console.Out.WriteLine("--------------------------------------------------------------------------------")
        Console.Out.WriteLine("----------------")
        Console.Out.Write("|Health: ")
        Console.ForegroundColor = 10
        Console.Out.Write(Health & "HP.")
        Console.ForegroundColor = 15
        Console.Out.WriteLine("|")
        Console.Out.WriteLine("----------------")
        Console.ForegroundColor = 11
        For InvCounter = 0 To ubound(ItemName)
            If ItemLocation(InvCounter) = -1 Then
                Console.Out.WriteLine(">" & ItemName(InvCounter) & " - " & ItemDescription(InvCounter))
            End If                          '-------------------------------------------------------
        Next
        Console.ForegroundColor = 15
        Console.Out.WriteLine("--------------------------------------------------------------------------------")
        If ItemLocation(5) = -1 Then        'Ui for Gem stones--------
            Console.ForegroundColor = 14
        Else
            Console.ForegroundColor = 7
        End If
        Console.Out.Write("            			  * ")
        If ItemLocation(7) = -1 Then
            Console.ForegroundColor = 9
        Else
            Console.ForegroundColor = 7
        End If
        Console.Out.Write("* ")
        If ItemLocation(10) = -1 Then
            Console.ForegroundColor = 10
        Else
            Console.ForegroundColor = 7
        End If
        Console.Out.Write("* ")
        If ItemLocation(11) = -1 Then
            Console.ForegroundColor = 12
        Else
            Console.ForegroundColor = 7
        End If
        Console.Out.WriteLine("* ")
        Console.ForegroundColor = 15
        Console.Out.WriteLine("")       '----------------------------
    End Sub
    '---COMEDY---
    Sub Comedy()
        If words(0) = "LOL" Then
            Console.Out.Writeline("Whats so funny?")
        ElseIf words(0) = "BYE" Or words(0) = "GOODBYE" Then
            Console.Out.Writeline("Where do you think your going?")
        Else
            Console.Out.WriteLine("Im not really the social type, can we get back to work please?")
        End If
    End Sub
    '---HELP---
    Sub Help()
        Console.Out.Writeline("")
        Console.Out.Writeline("*****BASIC COMMANDS******")
        Console.Out.Writeline("You can move around the world using NORTH, SOUTH, EAST OR WEST.")
        Console.Out.Writeline("You can check your inventory with INVENTORY(I)")
        Console.Out.Writeline("You can interact with the world around you with commands such as SEARCH or EXAMINE")
        Console.Out.WriteLine("Typing LOOK or LOOK AROUND gives you a description of your current location.")
        Console.Out.WriteLine("Clear the console screen using CLS or CLEAR.")
        Console.Out.Writeline("The rest is up to you to figure out!")
        Console.Out.Writeline("")
    End Sub
    '---LOOK---
    Sub Look()
        If Visited(CurrentRoom) = False Then   'IF Statement - If a room is visited, change room description
            Visited(CurrentRoom) = True
            Buffer()
            DroppedItems()
        Else
            Visited(CurrentRoom) = True
            BufferV()
            DroppedItems()
        End If
    End Sub
    '---BUFFER LINES---
    Sub Buffer()
        strMessageChop = Split(Room(CurrentRoom), " ")
        strMessage = ""
        Dim intCtr, intLine As Integer

        For intCtr = 0 To strMessageChop.Length - 1
            strMessage &= strMessageChop(intCtr) & " "

            ' add 1 to account for the space between each word
            intLine += (strMessageChop(intCtr).Length + 1)

            If intCtr < strMessageChop.Length - 1 Then
                If intLine + strMessageChop(intCtr + 1).Length >= 79 Then
                    strMessage &= vbCrLf 'Visual Basic Carriage Return Line Feed - Cuts down to the next line 'How did you find that out? :O
                    intLine = 0
                End If
            End If
        Next
        Console.Out.Write(strMessage)
        Console.Out.WriteLine("")
    End Sub

    Sub BufferV()
        strMessageChop = Split(RoomV(CurrentRoom), " ")
        strMessage = ""
        Dim intCtr, intLine As Integer

        For intCtr = 0 To strMessageChop.Length - 1
            strMessage &= strMessageChop(intCtr) & " "

            ' add 1 to account for the space between each word
            intLine += (strMessageChop(intCtr).Length + 1)

            If intCtr < strMessageChop.Length - 1 Then
                If intLine + strMessageChop(intCtr + 1).Length >= 79 Then
                    strMessage &= vbCrLf 'Visual Basic Carriage Return Line Feed - Cuts down to the next line 'How did you find that out? :O
                    intLine = 0
                End If
            End If
        Next
        Console.Out.Write(strMessage)
        Console.Out.WriteLine("")
    End Sub

    Sub PrintIntro()
        strMessageChop = Split(Intro, " ")
        strMessage = ""
        Dim intCtr, intLine As Integer

        For intCtr = 0 To strMessageChop.Length - 1
            strMessage &= strMessageChop(intCtr) & " "

            ' add 1 to account for the space between each word
            intLine += (strMessageChop(intCtr).Length + 1)

            If intCtr < strMessageChop.Length - 1 Then
                If intLine + strMessageChop(intCtr + 1).Length >= 79 Then
                    strMessage &= vbCrLf 'Visual Basic Carriage Return Line Feed - Cuts down to the next line 'How did you find that out? :O
                    intLine = 0
                End If
            End If
        Next
        Console.Out.Write(strMessage)
        Console.Out.WriteLine("")
    End Sub

    Sub FirstRoom()
        strMessageChop = Split(Room(CurrentRoom), " ")
        strMessage = ""
        Dim intCtr, intLine As Integer

        For intCtr = 0 To strMessageChop.Length - 1
            strMessage &= strMessageChop(intCtr) & " "

            ' add 1 to account for the space between each word
            intLine += (strMessageChop(intCtr).Length + 1)

            If intCtr < strMessageChop.Length - 1 Then
                If intLine + strMessageChop(intCtr + 1).Length >= 79 Then
                    strMessage &= vbCrLf 'Visual Basic Carriage Return Line Feed - Cuts down to the next line 'How did you find that out? :O
                    intLine = 0
                End If
            End If
        Next
        Console.Out.Write(strMessage)
        Console.Out.WriteLine("")
        DroppedItems()
    End Sub
    '---DROPPED ITEMS---
    Sub DroppedItems()

        DropSearcher = 0
        Do Until DropSearcher = 99
            If ItemDropped(DropSearcher) = True And ItemLocation(DropSearcher) = CurrentRoom Then
                Console.Out.WriteLine("")
                Console.Out.WriteLine("On the floor lies:")
                Found = True
                Console.Out.Write("-")
                Console.Out.WriteLine("A " & UCASE(ItemName(DropSearcher)) & ".")
            End If
            DropSearcher = DropSearcher + 1
        Loop
        If Found = False Then
            Console.Out.WriteLine("Nothing.")
        End If
        Found = False
    End Sub
    '---HEALTH VIEW---
    Sub HealthView()
        Console.ForegroundColor = 10
        Console.Out.WriteLine("Health: " & Health & " HP.")
        Console.ForegroundColor = 15
    End Sub
    '---GODMODE---
    Sub GodMode()
        If God = False Then
            Suffix = " The God"
            Console.Out.WriteLine("")
            Console.ForegroundColor = 13
            Console.Out.WriteLine("You earned a new title: 'The God'.")
            Console.Out.WriteLine("")
            Console.ForegroundColor = 15
            Console.ForegroundColor = 8
            Console.Out.WriteLine("RETRIEVING ITEMS...")
            Console.ForegroundColor = 15
            For Counter = 0 To 11 ' Change when items are added.
                ItemLocation(Counter) = -1
            Next
            Console.Out.WriteLine("")
            Console.ForegroundColor = 8
            Console.Out.WriteLine("COMPLETING ALL ROOMS...")
            Console.ForegroundColor = 15
            For Counter = 0 To 99 ' Change when items are added.
                ObjectLocation(Counter) = -1
            Next
            Console.Out.WriteLine("")
            Console.ForegroundColor = 8
            Console.Out.WriteLine("KILLING ALL ENEMIES...")
            Console.ForegroundColor = 15
            For Counter = 0 To 99 ' Change when items are added.
                EnemyLocation(Counter) = -1
            Next
            Console.Out.WriteLine("")
            Console.ForegroundColor = 10
            Console.Out.WriteLine("GODMODE ENABLED.")
            Console.ForegroundColor = 15
            Health = 999999

            God = True

        Else
            Suffix = " The Legend"
            Console.Out.WriteLine("")
            Console.ForegroundColor = 13
            Console.Out.WriteLine("You earned a new title: 'The Legend'.")
            Console.Out.WriteLine("")
            Console.ForegroundColor = 15
            Console.ForegroundColor = 8
            Console.Out.WriteLine("RETURNING ITEMS")
            Console.ForegroundColor = 15
            InitialiseItems()

            Console.Out.WriteLine("")
            Console.ForegroundColor = 8
            Console.Out.WriteLine("RETURNING ROOMS TO ORIGINAL STATE")
            Console.ForegroundColor = 15
            InitialiseObjects()

            Console.Out.WriteLine("")
            Console.ForegroundColor = 8
            Console.Out.WriteLine("BRINGING ALL ENEMIES BACK TO LIFE")
            Console.ForegroundColor = 15
            InitialiseEnemy()

            Console.Out.WriteLine("")
            Console.ForegroundColor = 10
            Console.Out.WriteLine("GODMODE DISABLED.")
            Console.ForegroundColor = 15
            Health = 100

            God = False
        End If
    End Sub
    '---DRINK---
    Sub Drink(ByVal objects As String)
        Select Case objects
            Case Is = "POTION", "HEALTH"
                If objects = "POTION" Then
                    Console.Out.WriteLine("Which potion would you like to  drink?")
                    Console.ForegroundColor = 14
                    Console.Out.Write(">")
                    Input = ucase(Console.in.ReadLine())
                    Console.ForegroundColor = 15
                    Console.Out.WriteLine("")
                Else
                    Input = objects
                End If
                Select Case Input
                    Case Is = "HEALTH"
                        If ItemLocation(8) > -1 Then
                            Console.Out.WriteLine("You don't have this potion.")
                        ElseIf ItemLocation(8) = -1 Then
                            Console.Out.Write("You drink the ")
                            Console.ForegroundColor = 11
                            Console.Out.Write("health potion")
                            Console.ForegroundColor = 15
                            Console.Out.WriteLine(".")
                            ItemLocation(8) = -2
                            ItemLocation(9) = -1
                            If Health <= 70 Then
                                Console.Out.WriteLine("")
                                Console.ForegroundColor = 10
                                Console.Out.WriteLine("It heals 30 HP.")
                                Health = Health + 30
                                Console.ForegroundColor = 15
                            ElseIf Health >= 100 Then
                                Console.Out.WriteLine("")
                                Console.Out.WriteLine("It has no effect.")
                            ElseIf Health >= 71 And Health <= 99 Then
                                Console.Out.WriteLine("")
                                Console.ForegroundColor = 10
                                Console.Out.WriteLine("It heals " & 100 - Health & " HP.")
                                Health = 100
                                Console.ForegroundColor = 15
                            End If
                            Console.Out.WriteLine("")
                            Console.Out.Write("You are left with an ")
                            Console.ForegroundColor = 11
                            Console.Out.Write("empty vial")
                            Console.ForegroundColor = 15
                            Console.Out.WriteLine(".")
                        Else
                            Console.Out.WriteLine("You don't have this potion.")
                        End If
                    Case Else
                        Console.Out.WriteLine("That doesn't make any sense...")
                End Select
            Case Else
                Console.Out.WriteLine("You can't do that.")
        End Select
    End Sub

    Sub PrintLogo()
        For counter = 1 To 5
            Console.Clear()
            Console.ForegroundColor = counter + 10
            Console.Out.WriteLine("")
            Console.Out.WriteLine("                                      ...                                      ")
            Console.Out.WriteLine("                            .#@@@@@@@@@@@@@@@@@@@#.                            ")
            Console.Out.WriteLine("                        '@@@@@@@:         @@@@@@@@@@@@+                        ")
            Console.Out.WriteLine("                     @@@@@@@@@@@:         @@@@@@@@@@@@@@@@                     ")
            Console.Out.WriteLine("                  ,@@@@@@@@@@@@@:         @@@@@@@@@@@@@@@@@@:                  ")
            Console.Out.WriteLine("                :@@@@@@@@@@@@@@@:         @@@@@@@@@@@@@@@@@@@@'                ")
            Console.Out.WriteLine("               @@@@@@@@@@+:+@@@@:         @@@@@@@@@@+:+@@@@@@@@@`              ")
            Console.Out.WriteLine("             @@@@@@@@@.    `  '@:         @@@@@@@.        '@@@@@@@             ")
            Console.Out.WriteLine("            @@@@@@@@+      @@@` ;         @@@@@;    +@@@@.  @@@@@@@            ")
            Console.Out.WriteLine("           @@@@@@@@       ;@@@@@          @@@@,      @@@@@@ @@@@@@@@           ")
            Console.Out.WriteLine("          @@@@@@@@:       :@@@@@;         @@@@       @@@@@@@@@@@@@@@@          ")
            Console.Out.WriteLine("         :@@@@@@@@        :@@@@@:         @@@         @@@@@@@@@@@@@@@+         ")
            Console.Out.WriteLine("         @@@@@@@@         :@@@@@:         @@@          @@@@@@@@@@@@@@@         ")
            Console.Out.WriteLine("         @@@@@@@@         :@@@@@:         @@@           @@@@@@@@@@@@@@,        ")
            Console.Out.WriteLine("        +@@@@@@@@         :@@@@@:         @@@,           @@@@@@@@@@@@@@        ")
            Console.Out.WriteLine("        @@@@@@@@@         :@@@@@:         @@@@            @@@@@@@@@@@@@        ")
            Console.Out.WriteLine("        @@@@@@@@+         :@@@@@:         @@@@;            @@@@@@@@@@@@        ")
            Console.Out.WriteLine("        ,@@@@@@@+         :@@@@@:         @@@@@             @@@@@@@@@@+        ")
            Console.Out.WriteLine("         @@@@@@@@         :@@@@@:         @@@@@@             @@@@@@@@@         ")
            Console.Out.WriteLine("         @@@@@@@@         :@@@@@:         @@@@@@@            @@@@@@@@@         ")
            Console.Out.WriteLine("          @@@@@@@         :@@@@@:         @@@@@@@@           @@@@@@@@          ")
            Console.Out.WriteLine("           @@@@@@:        :@@@@@:         @@@@@@@@'          :@@@@@@`          ")
            Console.Out.WriteLine("           `@@@@@@        :@@@@@:         @@@@@@@@@.         +@@@@@,           ")
            Console.Out.WriteLine("             @@@@@+       :@@@@@          @@@@@@@@@@         @@@@@             ")
            Console.Out.WriteLine("              @@@@@       :@@@@           @@@ .@@@@@@       '@@@@              ")
            Console.Out.WriteLine("                @@@@`      @@@ `;         @@@@  +@@@@@     ;@@@                ")
            Console.Out.WriteLine("                  @@@@        #@:         @@@@@@   .:     @@@.                 ")
            Console.Out.WriteLine("                    @@@@+..+@@@@@@@@@@@@@@@@@@@@@@@`   :@@@                    ")
            Console.Out.WriteLine("                       @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                       ")
            Console.Out.WriteLine("                          `#@@@@@@@@@@@@@@@@@@@@@@@@.                          ")
            Console.Out.WriteLine("                                `:+@@@@@@@@@+:`                                ")
            Console.Out.WriteLine("                                                                               ")
            Console.Out.WriteLine("                              DungeonScape © 2012                              ")
            Console.Out.WriteLine("")
            Console.Out.WriteLine("                              A GAME BY TEAM ARRAY                             ")
            System.Threading.Thread.Sleep(1000)

        Next
        Console.Out.WriteLine("")
        Console.ForegroundColor = 14
        Console.Out.WriteLine("                            PRESS ENTER TO CONTINUE                            ")
        Input = Console.In.ReadLine()
    End Sub
End Module