open System

//taken from http://www.lesession.co.uk/abc/abc_notation.htm
//ABC for middle C +- 1 octave: C,D,E,F,G,A,B,CDEFGABc
let baseDuration : int = 1
//let noteList = ["C,"; "D,"; "E,"; "F,"; "G,"; "A,"; "B,"; "C"; "D"; "E"; "F"; "G"; "A"; "B"; "c"]

let numBars : int = 8 //8 bars default


// ______    _  _    ___  ___ _   _  _____  _____  _____ 
// |  ___| _| || |_  |  \/  || | | |/  ___||_   _|/  __ \
// | |_   |_  __  _| | .  . || | | |\ `--.   | |  | /  \/
// |  _|   _| || |_  | |\/| || | | | `--. \  | |  | |    
// | |    |_  __  _| | |  | || |_| |/\__/ / _| |_ | \__/\
// \_|      |_||_|   \_|  |_/ \___/ \____/  \___/  \____/

//logo made using https://patorjk.com/software/taag/#p=display&h=0&v=0&f=Doom&t=F%23%20MUSIC
                                                      
                                                      


//"F" :: song == ["F";] @ song

// let chooseNote =
//     "X"

// let chooseDuration =
//     "2"

// let note chooseNote chooseDuration =
//     [ chooseNote; chooseDuration; ]



let seed = Random()

// let chords =
//     [ for i in 0..numBars do
//             match seed.Next(0, 3) with
//             | 0 -> 'C'
//             | 1 -> 'F'
//             | 2 -> 'G']

// printfn "%c" (chords.Item(0))



// let t: string = 
//     match 2 with
//     |   1 -> "one"
//     |   2 -> "two"
//     |   3 -> "three"

//printfn "%s" t

//Chord selection
let chordProbs = [| 'C', 5;//50%
           'F', 3;
           'G', 2 |]
let chordSums = Seq.scan (+) 0 (dict chordProbs).Values |> Seq.skip 1 |> Seq.toArray 

let chords =
    [
        for i in 0..numBars-1 do
        let pick = seed.Next(1, (chordSums |> Array.max)+1)
        let res = fst chordProbs.[chordSums |> Seq.findIndex ((<=) pick)]
        res
    ]

//notes in C chord selection
let cProbs = [| "C", 2;
           "D", 1;
           "E", 2;
           "F", 1;
           "G", 3;
           "A", 1;
           "B", 1;
           "z.", 1|]
let cSums = Seq.scan (+) 0 (dict cProbs).Values |> Seq.skip 1 |> Seq.toArray 

let fProbs = [| "C", 3;
           "D", 1;
           "E", 1;
           "F", 2;
           "G", 1;
           "A", 2;
           "B", 1;
           "z.", 1|]
let fSums = Seq.scan (+) 0 (dict fProbs).Values |> Seq.skip 1 |> Seq.toArray

let gProbs = [| "C", 1;
           "D", 3;
           "E", 1;
           "F", 1;
           "G", 2;
           "A", 1;
           "B", 2;
           "z.", 1|]
let gSums = Seq.scan (+) 0 (dict gProbs).Values |> Seq.skip 1 |> Seq.toArray

// let durationProbs = [| "", 3;
//            "/2", 2;
//            "2", 2;
//            "/4", 1;
//            "3", 1;
//            "/8", 1;
//            "4", 1; 
//            "/16", 1|]

let durationProbs = [| 1.0, 3;
           0.5, 2;
           2.0, 2;
           0.25, 1;
           3.0, 1;
           0.125, 1;
           4.0, 1; 
           0.0625, 1|]
let durationSums = Seq.scan (+) 0 (dict durationProbs).Values |> Seq.skip 1 |> Seq.toArray 

// let song =
//     [
//         for i in 0..(numBars*4)-1 do
//             let pick = seed.Next(1, (cSums |> Array.max)+1)
//             let chosenNote = fst cProbs.[cSums |> Seq.findIndex ((<=) pick)]
//             chosenNote
//     ]

// let chooseNote chord = 
//     if chord = 'C' then
//         let pick = seed.Next(1, (cSums |> Array.max)+1)
//         let chosenNote = fst cProbs.[cSums |> Seq.findIndex ((<=) pick)]
//         chosenNote
//     elif chord = 'F' then
//         let pick = seed.Next(1, (fSums |> Array.max)+1)
//         let chosenNote = fst fProbs.[fSums |> Seq.findIndex ((<=) pick)]
//         chosenNote
//     else
//         let pick = seed.Next(1, (gSums |> Array.max)+1)
//         let chosenNote = fst gProbs.[gSums |> Seq.findIndex ((<=) pick)]
//         chosenNote

// let song2: char list = 
//     [
//         let mutable currentChord: int = 0

//         //TODO make a map that iterates through elements in chords and runs a recursive function on each element that chooses notes until total duration >=4

//         currentChord <- currentChord+1
//         for i: int in 0..(numBars*4)-1 do
//             match chords.Item(if currentChord % 4 = 0 then currentChord+1 else currentChord) with
//             | 'C' -> chooseNote('C')
//             | 'F' -> chooseNote('F')
//             | 'G' -> chooseNote('G')
//             | _ -> printfn "ERROR"

        
//     ]

// let rec chooseNote (totalDuration:int) (chord:char) (bar:list<string>) : list<string> =
//     if totalDuration < 4 then
//         if chord = 'C' then
//             let pick = seed.Next(1, (cSums |> Array.max)+1)
//             let chosenNote = fst cProbs.[cSums |> Seq.findIndex ((<=) pick)]
//             chooseNote (totalDuration+1) chord (chosenNote.ToString()::bar)
//         elif chord = 'F' then
//             let pick = seed.Next(1, (fSums |> Array.max)+1)
//             let chosenNote = fst fProbs.[fSums |> Seq.findIndex ((<=) pick)]
//             chooseNote (totalDuration+1) chord (chosenNote.ToString()::bar)
//         else
//             let pick = seed.Next(1, (gSums |> Array.max)+1)
//             let chosenNote = fst gProbs.[gSums |> Seq.findIndex ((<=) pick)]
//             chooseNote (totalDuration+1) chord (chosenNote.ToString()::bar)

let rec chooseDuration (totalDuration : float) : float = // use n to change durationProbs
    let pick = seed.Next(1, (durationSums |> Array.max)+1)
    let chosenDuration = fst durationProbs.[durationSums |> Seq.findIndex ((<=) pick)]
    if (chosenDuration + totalDuration) > 4.0 then
        chooseDuration totalDuration
    else
        chosenDuration

let durationToABC (duration:float) :string =
    match duration with
    | 1.0 -> ""
    | 0.5 -> "/2"
    | 2.0 -> "2"
    | 0.25 -> "/4"
    | 3.0 -> "3"
    | 0.125 -> "/8"
    | 4.0 -> "4"
    | 0.0625 -> "/16"

// let rec chooseNote (totalDuration:float) (chord:char) (bar:list<string>) : list<string> =
//     if totalDuration < 4.0 then
//         if chord = 'C' then
//             let pick = seed.Next(1, (cSums |> Array.max)+1)
//             let chosenNote = fst cProbs.[cSums |> Seq.findIndex ((<=) pick)]
//             let duration = chooseDuration totalDuration
//             chooseNote (totalDuration+duration) chord ((chosenNote.ToString()+durationToABC duration)::bar)
//         elif chord = 'F' then
//             let pick = seed.Next(1, (fSums |> Array.max)+1)
//             let chosenNote = fst fProbs.[fSums |> Seq.findIndex ((<=) pick)]
//             let duration = chooseDuration totalDuration
//             chooseNote (totalDuration+duration) chord ((chosenNote.ToString()+durationToABC duration)::bar)
//         else
//             let pick = seed.Next(1, (gSums |> Array.max)+1)
//             let chosenNote = fst gProbs.[gSums |> Seq.findIndex ((<=) pick)]
//             let duration = chooseDuration totalDuration
//             chooseNote (totalDuration+duration) chord ((chosenNote.ToString()+durationToABC duration)::bar)
//     else
//         bar

let rec chooseNote (totalDuration:float) (chord:char) (bar:string) : string =
    if totalDuration < 4.0 then
        if chord = 'C' then
            let pick = seed.Next(1, (cSums |> Array.max)+1)
            let chosenNote = fst cProbs.[cSums |> Seq.findIndex ((<=) pick)]
            let duration = chooseDuration totalDuration
            chooseNote (totalDuration+duration) chord ((chosenNote.ToString()+durationToABC duration) + " " + bar)
        elif chord = 'F' then
            let pick = seed.Next(1, (fSums |> Array.max)+1)
            let chosenNote = fst fProbs.[fSums |> Seq.findIndex ((<=) pick)]
            let duration = chooseDuration totalDuration
            chooseNote (totalDuration+duration) chord ((chosenNote.ToString()+durationToABC duration) + " " + bar)
        else
            let pick = seed.Next(1, (gSums |> Array.max)+1)
            let chosenNote = fst gProbs.[gSums |> Seq.findIndex ((<=) pick)]
            let duration = chooseDuration totalDuration
            chooseNote (totalDuration+duration) chord ((chosenNote.ToString()+durationToABC duration) + " " + bar)
    else
        bar + "|"

let song:string = String.concat " " (List.map (fun (currentChord:char) -> chooseNote 0 currentChord "") chords)

printfn "%s" song

// let song:list<list<string>> = List.map (fun (currentChord:char) -> chooseNote 0 currentChord []) chords

// let rec duration runningTotal =
//     let generatedDuration = chooseDuration 1
//     if (generatedDuration+runningTotal) > 4.0 then
//         duration runningTotal
//     else
//         generatedDuration

//Inspired by: https://stackoverflow.com/a/10164570
// let d = [| 'C', 5
//            'F', 3
//            'G', 2 |]
// let sums = Seq.scan (+) 0 (dict d).Values |> Seq.skip 1 |> Seq.toArray 
// let pick = seed.Next(1, (sums |> Array.max)+1)
// let res = fst d.[sums |> Seq.findIndex ((<=) pick)]

// let formatArray arr =
//     arr
//     |> Array.map (fun subArr -> subArr |> Array.map (fun s -> s.Replace("\"", "") |> Seq.filter (fun c -> not (c = ';' || c = '[' || c = ']')) |> String.concat " "))
//     |> Array.map (fun subArr -> subArr |> String.concat " | ")
//     |> String.concat " "