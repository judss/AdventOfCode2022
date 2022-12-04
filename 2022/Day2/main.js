"use strict";
exports.__esModule = true;
var fs = require("fs");
var constants_1 = require("./constants");
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
var puzzleInput = fs.readFileSync('puzzle-input.txt', 'utf8');
var games = puzzleInput.split("\r\n");
var scores = [];
games.forEach(function (game) {
    var players = game.split(" ");
    var opponent = players[0];
    var you = players[1];
    // get the initial score based on what you played
    var score = getInitialScore(you);
    // get the score for the outcome of the match
    score += getOutcomeScore(opponent, you);
    // push the score to the totals
    scores.push(score);
});
var result = scores.reduce(function (accumulator, current) {
    return accumulator + current;
}, 0);
console.log(result);
function getInitialScore(play) {
    switch (play) {
        case constants_1.YouPlays.rock:
            return 1;
        case constants_1.YouPlays.paper:
            return 2;
        case constants_1.YouPlays.sissors:
            return 3;
        default:
            throw new Error("Incorrect You Play: ".concat(play));
    }
}
function getOutcomeScore(opponent, you) {
    var score = 0;
    switch (opponent) {
        case constants_1.OpponentPlays.rock:
            if (you == constants_1.YouPlays.rock) {
                score = 3;
            }
            else if (you == constants_1.YouPlays.paper) {
                score = 6;
            }
            else if (you == constants_1.YouPlays.sissors) {
                score = 0;
            }
        case constants_1.OpponentPlays.paper:
            if (you == constants_1.YouPlays.rock) {
                score = 0;
            }
            else if (you == constants_1.YouPlays.paper) {
                score = 3;
            }
            else if (you == constants_1.YouPlays.sissors) {
                score = 6;
            }
        case constants_1.OpponentPlays.sissors:
            if (you == constants_1.YouPlays.rock) {
                score = 6;
            }
            else if (you == constants_1.YouPlays.paper) {
                score = 0;
            }
            else if (you == constants_1.YouPlays.sissors) {
                score = 3;
            }
        default:
            throw new Error("Incorrect Opponent Play: ".concat(opponent));
    }
    return score;
}
