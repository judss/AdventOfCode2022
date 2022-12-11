#include <iostream>
#include <fstream>
#include "string"
#include <list>
using namespace std;

int main() 
{
    // position constants
    const string overLapping = "OL";
    const string left = "L";
    const string right = "R";
    const string above = "A";
    const string underneath = "U";

    list<position*> visitedTailPositions;
    position *currentHeadPosition = new position(0,0);
    position *currentTailPosition = new position(0,0);

    visitedTailPositions.emplace_back(currentTailPosition);

    ifstream PuzzleInput("puzzle-input.txt");

    for (string line; std::getline(PuzzleInput, line); )
    {
        position *newHeadPosition = new position();
        position *newTailPosition = new position();

        const char *moves = line.c_str();
        // get direction to go in
        char directionOfMove = moves[0];

        // convert char to int using ascii code
        int numberOfMoves = moves[1] - '0';

        switch(directionOfMove)
        {
            case 'L':
            break;
            case 'R':
            break;
            case 'U':
            break;
            case 'D':
            break;
        }

        //result += ">> " + line + "\n";
    }

    PuzzleInput.close();
}

class position {  
    int x;
    int y;
  public:
    position(int, int);
    position();
};

position::position(int x, int y){
    x = x;
    y = y;
};