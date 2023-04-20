open System

[<EntryPoint>]
let composer args = 
    
    //Takes the first number entered as input and sets it as the desired number of bars in the song
    let numBars:int = args |> Seq.head |> int

    //Default duration of a note in this program
    let baseDuration : int = 1

    let seed = Random()

    //Chord selection
    let chordProbs = [| 'C', 5;//50%
               'F', 3;
               'G', 2 |]
    let chordSums = Seq.scan (+) 0 (dict chordProbs).Values |> Seq.skip 1 |> Seq.toArray 

    //Chord generation
    let chords =
        [
            for i in 0..numBars-1 do
            let pick = seed.Next(1, (chordSums |> Array.max)+1)
            let res = fst chordProbs.[chordSums |> Seq.findIndex ((<=) pick)]
            res
        ]

    //probabilities of the notes in the C chord selection
    let cProbs = [| "C", 2;
               "D", 1;
               "E", 2;
               "F", 1;
               "G", 3;
               "A", 1;
               "B", 1;
               "z", 1|]
    let cSums = Seq.scan (+) 0 (dict cProbs).Values |> Seq.skip 1 |> Seq.toArray 

    //probabilities of the notes in the F chord selection
    let fProbs = [| "C", 3;
               "D", 1;
               "E", 1;
               "F", 2;
               "G", 1;
               "A", 2;
               "B", 1;
               "z", 1|]
    let fSums = Seq.scan (+) 0 (dict fProbs).Values |> Seq.skip 1 |> Seq.toArray

    //probabilities of the notes in the G chord selection
    let gProbs = [| "C", 1;
               "D", 3;
               "E", 1;
               "F", 1;
               "G", 2;
               "A", 1;
               "B", 2;
               "z", 1|]
    let gSums = Seq.scan (+) 0 (dict gProbs).Values |> Seq.skip 1 |> Seq.toArray

    //The weightings of a each note duration
    let durationProbs = [| 1.0, 3;
               0.5, 2;
               2.0, 2;
               0.25, 1;
               3.0, 1;
               0.125, 1;
               4.0, 1; 
               0.0625, 1|]
    let durationSums = Seq.scan (+) 0 (dict durationProbs).Values |> Seq.skip 1 |> Seq.toArray 

    (*
        Chooses the duration of a note whilst taking in the current running duration of the bar,
        This is passed as an argument from the chooseNote function.
        If the current running duration in addition to the generated note duration is greater than 4, the function recurs and 'tries again'
    *)
    let rec chooseDuration (totalDuration : float) : float = // use n to change durationProbs
        let pick = seed.Next(1, (durationSums |> Array.max)+1)
        let chosenDuration = fst durationProbs.[durationSums |> Seq.findIndex ((<=) pick)]
        if (chosenDuration + totalDuration) > 4.0 then
            chooseDuration totalDuration
        else
            chosenDuration
    
    (*
        Converts the duration of each note into ABC format
    *)
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

    (*
        The chooseNote function recursively chooses a note and calls the chooseDuration function to select the duration of the note.
        The note is then added to the generated bar which is passed as a parameter back into the chooseNote function itself.
        This process repeats until the bar has a total duration of 4 beats, then adds the pipe character as a bar seperator at the end.
    *)
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

    (*
        The song declaration maps the recursive function of chooseNote to each chord.
        This means that the chooseNote function is entered then recursively called until the bar is completed.
        The function then moves on to the next bar until every element in the chords list has been mapped to.
        Finally, a space is concatenate on to each bar to seperate them and increase readability.
    *)
    let song:string = String.concat " " (List.map (fun (currentChord:char) -> chooseNote 0 currentChord "") chords)

    //Outputs the completed song to the screen
    printfn "%s" song

    0

    //used as template for the probability weightings: https://stackoverflow.com/a/10164570
    // let d = [| 'C', 5
    //            'F', 3
    //            'G', 2 |]
    // let sums = Seq.scan (+) 0 (dict d).Values |> Seq.skip 1 |> Seq.toArray 
    // let pick = seed.Next(1, (sums |> Array.max)+1)
    // let res = fst d.[sums |> Seq.findIndex ((<=) pick)]