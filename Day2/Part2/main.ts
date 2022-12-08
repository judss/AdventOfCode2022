import * as fs from 'fs';
import { OpponentPlays, Outcomes, YouPlays } from './constants';

const puzzleInput = fs.readFileSync('puzzle-input.txt', 'utf8');
const games = puzzleInput.split("\r\n");

var scores:number[] = [];

games.forEach(game => {
    
    const players = game.split(" ");
    var opponent = players[0];
    var result = players[1]

    var youPlay = getYouPlay(result, opponent);    

    // get the initial score based on what you played
    var score = getPayScore(youPlay);

    // get the score for the outcome of the match
    score += getResultScore(result);

    // push the score to the totals
    scores.push(score);
});

const result = scores.reduce((accumulator, current) => {
    return accumulator + current;
  }, 0);

console.log(result);

function getPayScore(play:string):number{
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

function getYouPlay(result:string, opponent:string):string{
    switch(result){
        case Outcomes.win:
           return getWinPlay(opponent); 
        case Outcomes.draw:
            return getDrawPlay(opponent);
        case Outcomes.loss:
           return getLossPlay(opponent);
    }
}

function getWinPlay(opponent:string):string{
    switch(opponent){
        case OpponentPlays.rock:
            return YouPlays.paper;
        case OpponentPlays.paper:
            return YouPlays.sissors;
        case OpponentPlays.sissors:
            return YouPlays.rock;
    }
}

function getLossPlay(opponent:string):string{
    switch(opponent){
        case OpponentPlays.rock:
            return YouPlays.sissors;
        case OpponentPlays.paper:
            return YouPlays.rock;
        case OpponentPlays.sissors:
            return YouPlays.paper;
    }
}

function getDrawPlay(opponent:string):string{
    switch(opponent){
        case OpponentPlays.rock:
            return YouPlays.rock;
        case OpponentPlays.paper:
            return YouPlays.paper;
        case OpponentPlays.sissors:
            return YouPlays.sissors;
    }
}

function getResultScore(result:string):number{
    switch(result){
        case Outcomes.win:
            return 6;
        case Outcomes.draw:
            return 3;
        case Outcomes.loss:
            return 0;
    }
}