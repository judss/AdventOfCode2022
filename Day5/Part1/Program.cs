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

Console.WriteLine(JsonConvert.SerializeObject(answer));

List< KeyValuePair<int, Stack<string>>> MapStacks(string startingStacks)
{
    var result = new List<KeyValuePair<int, Stack<string>>>();

    var rows = startingStacks.Split("\r\n").Reverse().ToArray();

    for(var i = 0; i < rows.Count(); i++)
    {
        var row = rows[i];
        
        if(i == 0)
        {
            var numbers = row.Split("   ");

            foreach(var number in numbers)
            {
                var numberAsInt = int.Parse(number.Trim());
                var stack = new KeyValuePair<int,Stack<string>>(numberAsInt, new Stack<string>());
                result.Add(stack);
            }            
        }
        else
        {
            var crates = row.Chunk(4).Select(x => new string(x)).ToArray();          

            for(var c = 0; c < crates.Length; c++)
            {
                var crate = crates[c];

                if(string.IsNullOrWhiteSpace(crate))
                {
                    continue;
                }

                var crateContents = crate.Trim().Trim(new Char[] { '[', ']' });
                
                result.Single(x => x.Key == c + 1).Value.Push(crateContents);
            }

        }
    }

    return result;
}

List<int[]> MapProcedures(string rearrangementProcedure)
{
    var result = new List<int[]>();

    var procedures = rearrangementProcedure.Split("\r\n");
    
    foreach(var stringProcedure in procedures)
    {
        var procedure = new int[3];

        var matches = System.Text.RegularExpressions.Regex.Matches(stringProcedure, @"\d+");
        
        procedure[0] = int.Parse(matches[0].Value);
        procedure[1] = int.Parse(matches[1].Value);
        procedure[2] = int.Parse(matches[2].Value);

        result.Add(procedure);
    }

    return result;
}

List<KeyValuePair<int, Stack<string>>> ExecuteProcedures(List<KeyValuePair<int, Stack<string>>> stacks, List<int[]> procedures)
{
    foreach(var procedure in procedures)
    {
        var move = procedure[0];
        var from = procedure[1];
        var to = procedure[2];

        var fromStack = stacks.Single(x => x.Key == from);
        var toStack = stacks.Single(x => x.Key == to);

        for(var i = 0; i < move; i++){
            var item = fromStack.Value.Pop();
            toStack.Value.Push(item);
        }            
    }

    return stacks;
}

string GetAnswerFromNewStacks(List< KeyValuePair<int, Stack<string>>> newStacks){
    var answer = "";

    foreach(var stack in newStacks){
        answer += stack.Value.First();
    }

    return answer;
}