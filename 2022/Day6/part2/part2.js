const {readFileSync} = require('fs');

const puzzleInput = readFileSync('puzzle-input.txt', 'utf8');

const characters = puzzleInput.split("");

var postion = 0;

for(let i = 0; i < characters.length; i++)
{
    var group = characters.slice(i, i + 14);
    let findDuplicates = arr => arr.filter((item, index) => arr.indexOf(item) != index)

    var duplicates = findDuplicates(group);

    if(duplicates.length == 0){
        postion = i;
        console.log(group);
        break;
    }
}

console.log(postion + 14);