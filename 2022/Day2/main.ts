import * as fs from 'fs';
import { YouPlays, OpponentPlays } from './constants';

// opponent
// a = rock
// b = paper
// c = sissors

// you
// x = rock
// y = paper
// z = sissors

// paper > rock
// rock > sissors
// sissors > paper

// a & x = rock
// b & y = paper
// c & z = sissors

// a x = 3
// a y = 6
// a z = 0

// b y = 3
// b z = 6
// b x = 0

// c z = 3
// c x = 6
// c y = 0

const puzzleInput = fs.readFileSync('puzzle-input.txt', 'utf8');
const games = puzzleInput.split("\r\n");

var scores:number[] = [];

games.forEach(game => {
    
    const players = game.split(" ");
    var opponent = players[0];
    var you = players[1];

    // get the initial score based on what you played
    var score = getInitialScore(you);

    // get the score for the outcome of the match
    score += getOutcomeScore(opponent, you);

    // push the score to the totals
    scores.push(score);
});

const result = scores.reduce((accumulator, current) => {
    return accumulator + current;
  }, 0);

console.log(result);

function getInitialScore(play:string):number{
    switch(play){
        case YouPlays.rock:
            return 1;
        case YouPlays.paper:
            return 2;
        case YouPlays.sissors:
            return 3;
        default:
            throw new Error(`Incorrect You Play: ${play}`);
    }
}

function getOutcomeScore(opponent:string, you:string):number{
    var score = 0;

    switch(opponent){
        case OpponentPlays.rock:
            if(you == YouPlays.rock)
            {
                score = 3;
            }
            else if(you == YouPlays.paper)
            {
                score = 6;
            } 
            else if(you == YouPlays.sissors){
                score = 0;
            }                
        case OpponentPlays.paper:
            if(you == YouPlays.rock)
            {
                score = 0;
            }
            else if(you == YouPlays.paper)
            {
                score = 3;
            } 
            else if(you == YouPlays.sissors){
                score = 6;
            }  
        case OpponentPlays.sissors:
            if(you == YouPlays.rock)
            {
                score = 6;
            }
            else if(you == YouPlays.paper)
            {
                score = 0;
            } 
            else if(you == YouPlays.sissors){
                score = 3;
            }  
        default:
            throw new Error(`Incorrect Opponent Play: ${opponent}`);
    }

    return score;
}