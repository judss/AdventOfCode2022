#include <iostream>
#include <fstream>
#include "string"
#include <list>
#include <algorithm>
using namespace std;

class Position {  
    public:
        int x;
        int y;
};

bool checkIfHeadIsOverLappingTail(Position newHeadPosition, Position currentTailPosition){
    return currentTailPosition.x == newHeadPosition.x && currentTailPosition.y == newHeadPosition.y;
}

bool checkIfInSameColumn(Position newHeadPosition, Position currentTailPosition){
    return newHeadPosition.y == currentTailPosition.y;
}

bool checkIfInSameRow(Position newHeadPosition, Position currentTailPosition){
    return newHeadPosition.x == currentTailPosition.x;
}

bool checkIfAboveOrBelow(Position newHeadPosition, Position currentTailPosition){
    return newHeadPosition.y == currentTailPosition.y - 1 || newHeadPosition.y == currentTailPosition.y + 1;
}

bool checkIfLeftOrRight(Position newHeadPosition, Position currentTailPosition){
    return newHeadPosition.x == currentTailPosition.x - 1 || newHeadPosition.x == currentTailPosition.x + 1;
}


int main() 
{
    // position constants
    const string overLapping = "OL";
    const string left = "L";
    const string right = "R";
    const string above = "A";
    const string underneath = "U";

    list<Position> visitedTailPositions;
    Position currentHeadPosition;
    currentHeadPosition.x = 0;
    currentHeadPosition.y = 0;
    Position currentTailPosition;
    currentTailPosition.x = 0;
    currentTailPosition.y = 0;

    visitedTailPositions.emplace_back(currentTailPosition);

    ifstream PuzzleInput("puzzle-input.txt");

    for (string line; std::getline(PuzzleInput, line); )
    {
        // get direction to go in
        char directionOfMove = line[0];

        // convert char to int using ascii code
        int numberOfMoves = line[2] - '0';

        for(int i = 0; i < numberOfMoves; i++)
        {
            Position newHeadPosition = currentHeadPosition;
            Position newTailPosition = currentTailPosition;

            bool isOverLapping = false;
            bool onSameColumn = false;
            bool onSameRow = false;
            bool isAboveOrBelow = false;
            bool isToLeftOrRight = false;

            switch(directionOfMove)
            {
                case 'L':
                    newHeadPosition.x -= 1;

                    isOverLapping = checkIfHeadIsOverLappingTail(newHeadPosition, currentTailPosition);
                    onSameColumn = checkIfInSameColumn(newHeadPosition, currentTailPosition);
                    isAboveOrBelow = checkIfAboveOrBelow(newHeadPosition, currentTailPosition);

                    if(!isOverLapping){
                        if((newHeadPosition.x == currentTailPosition.x - 2 && isAboveOrBelow) || newHeadPosition.y == currentTailPosition.y){
                            newTailPosition = newHeadPosition;
                            newTailPosition.x += 1;
                        }
                    }
                    break;
                case 'R':
                    newHeadPosition.x += 1;

                    isOverLapping = checkIfHeadIsOverLappingTail(newHeadPosition, currentTailPosition);
                    onSameColumn = checkIfInSameColumn(newHeadPosition, currentTailPosition);
                    isAboveOrBelow = checkIfAboveOrBelow(newHeadPosition, currentTailPosition);

                    if(!isOverLapping){
                        if((newHeadPosition.x == currentTailPosition.x + 2 && isAboveOrBelow) || onSameColumn){
                            newTailPosition = newHeadPosition;
                            newTailPosition.x -= 1;
                        }
                    }
                    break;
                case 'U':
                    newHeadPosition.y += 1;

                    isOverLapping = checkIfHeadIsOverLappingTail(newHeadPosition, currentTailPosition);
                    onSameRow = checkIfInSameRow(newHeadPosition, currentTailPosition);
                    isToLeftOrRight = checkIfLeftOrRight(newHeadPosition, currentTailPosition);

                    if(!isOverLapping){
                        if((newHeadPosition.y == currentTailPosition.y + 2 && isToLeftOrRight) || onSameRow){
                            newTailPosition = newHeadPosition;
                            newTailPosition.y -= 1;
                        }                        
                    }                  
                    break;
                case 'D':
                    newHeadPosition.y -= 1;

                    isOverLapping = checkIfHeadIsOverLappingTail(newHeadPosition, currentTailPosition);
                    onSameRow = checkIfInSameRow(newHeadPosition, currentTailPosition);
                    isToLeftOrRight = checkIfLeftOrRight(newHeadPosition, currentTailPosition);

                    if(!isOverLapping){
                        if((newHeadPosition.y == currentTailPosition.y - 2 && isToLeftOrRight) || onSameRow){
                            newTailPosition = newHeadPosition;
                            newTailPosition.y += 1;
                        }                        
                    }  
                    break;
            }

            currentHeadPosition = newHeadPosition;
            currentTailPosition = newTailPosition;
            visitedTailPositions.emplace_back(newTailPosition);
        }
    }

    // Sort the elements in the list by their x and y properties.
    visitedTailPositions.sort([](const Position& a, const Position& b) 
    {
        if (a.x < b.x) {
            return true;
        }

        if (a.x == b.x && a.y < b.y) {
            return true;
        }
        
        return false;
    });

    // erase any duplicates
    visitedTailPositions.erase(std::unique(visitedTailPositions.begin(), visitedTailPositions.end(), 
                            [](const Position& a, const Position& b) {
                                return a.x == b.x && a.y == b.y;
                            }),
                            visitedTailPositions.end());

     

    int answer = visitedTailPositions.size();

    std::cout << "Answer: " << answer;

    PuzzleInput.close();
}