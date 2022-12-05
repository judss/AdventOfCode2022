using System.Text.RegularExpressions;
using Newtonsoft.Json;

// read the puzzle input into a string
var fileContents = System.IO.File.ReadAllText(@"./puzzle-input.txt");

// split the starting stack diagram from the rearrangement procedure
var fileSections = fileContents.Split("\r\n\r\n");
var startingStacks = fileSections[0];
var rearrangementProcedures = fileSections[1];

// map the stacks into a list of keyvalue pairs
var stacks = MapStacks(startingStacks);

// map the procedures into
var procedures = MapProcedures(rearrangementProcedures);

// execute the procedures
var newStacks = ExecuteProcedures(stacks, procedures);

// get the answer
var answer = GetAnswerFromNewStacks(newStacks);

// return answer
Console.WriteLine(JsonConvert.SerializeObject(answer));

List< KeyValuePair<int, Stack<string>>> MapStacks(string startingStacks)
{
    var result = new List<KeyValuePair<int, Stack<string>>>();

    // split the stacks into rows and then reverse it so we can use the numbers first and work backwards on the stacks
    var rows = startingStacks.Split("\r\n").Reverse().ToArray();

    for(var i = 0; i < rows.Count(); i++)
    {
        var row = rows[i];
        
        // if its the first row we know this this the numbers row
        if(i == 0)
        {
            var numbers = row.Split("   ");

            // go through each number and create a new stack to store the crates
            foreach(var number in numbers)
            {
                var numberAsInt = int.Parse(number.Trim());
                var stack = new KeyValuePair<int,Stack<string>>(numberAsInt, new Stack<string>());
                result.Add(stack);
            }            
        }
        else
        {
            //if its any other row we know it contains crates.

            // splite the row into chunks of 4 as this will contain 9 lots of either a crate or whitespace
            var crates = row.Chunk(4).Select(x => new string(x)).ToArray();          

            for(var c = 0; c < crates.Length; c++)
            {
                var crate = crates[c];
                
                // if its an empty space we can skip it
                if(string.IsNullOrWhiteSpace(crate))
                {
                    continue;
                }

                // remove any white space or [] from the crate to reveal the contents (letter)
                var crateContents = crate.Trim().Trim(new Char[] { '[', ']' });
                
                // get the stack and add the letter to it
                result.Single(x => x.Key == c + 1).Value.Push(crateContents);
            }

        }
    }

    return result;
}

List<int[]> MapProcedures(string rearrangementProcedure)
{
    var result = new List<int[]>();

    // each procedure is on a new line so split it out
    var procedures = rearrangementProcedure.Split("\r\n");
    
    foreach(var stringProcedure in procedures)
    {
        // we know a procedure has 3 values (move, from, to) so create an array to store these values
        var procedure = new int[3];

        // use regex to extract the numbers out for the string
        var matches = System.Text.RegularExpressions.Regex.Matches(stringProcedure, @"\d+");
        
        // set the posisions in the array (move, from, to)
        procedure[0] = int.Parse(matches[0].Value);
        procedure[1] = int.Parse(matches[1].Value);
        procedure[2] = int.Parse(matches[2].Value);

        // add to results
        result.Add(procedure);
    }

    return result;
}

List<KeyValuePair<int, Stack<string>>> ExecuteProcedures(List<KeyValuePair<int, Stack<string>>> stacks, List<int[]> procedures)
{
    // got through each procedure and execute it
    foreach(var procedure in procedures)
    {
        // we know the position in the array so extract these out into variables
        var move = procedure[0];
        var from = procedure[1];
        var to = procedure[2];

        // get the stack we want to remove from and the stack we want add those items to
        var fromStack = stacks.Single(x => x.Key == from);
        var toStack = stacks.Single(x => x.Key == to);

        var items = new List<string>();

        // for each move we want to remove the item from the stack and then add it to a list
        for(var i = 0; i < move; i++){
            var item = fromStack.Value.Pop();
            items.Add(item);
        }
        
        // reverse the list to retain its order
        items.Reverse();

        // add the items to the to stack
        foreach(var item in items){
            toStack.Value.Push(item);
        }      
    }

    return stacks;
}

string GetAnswerFromNewStacks(List< KeyValuePair<int, Stack<string>>> newStacks){
    
    var answer = "";

    // get the first item on the stack which is the items at the top of the stack.
    foreach(var stack in newStacks){
        answer += stack.Value.First();
    }

    return answer;
}