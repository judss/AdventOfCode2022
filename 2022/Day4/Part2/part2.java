package Part2;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

class Main {
    public static void main(String[] args) {
        var listOfAssignmentPairs = GetAssignmentPairsFromFile();   
        
        var total = 0;

        for (String assignmentPair : listOfAssignmentPairs) {
            var assignments = assignmentPair.split(",");

            var firstAssignment = assignments[0];
            var secondAssignment = assignments[1];

            var assigmentOverlaps = CheckIfTheAssignmentsAreWithinEachOther(firstAssignment, secondAssignment);

            if(assigmentOverlaps){
                total += 1;
            }               
        }

        System.out.print(total);
        
    }

    private static boolean CheckIfTheAssignmentsAreWithinEachOther(String firstAssignment, String secondAssignment) {        
        var assignmentRange1 = firstAssignment.split("-");        
        int start1 = Integer.parseInt(assignmentRange1[0]);
        int end1 = Integer.parseInt(assignmentRange1[1]);

        var assignmentRange2 = secondAssignment.split("-");
        int start2 =  Integer.parseInt(assignmentRange2[0]);
        int end2 =  Integer.parseInt(assignmentRange2[1]);

        var rangeOverlapsRange = RangeOverlapsRange(start1, end1, start2, end2);

        if (rangeOverlapsRange) {
            return true;
        }

        return false;
    }
    
    public static boolean RangeIsWithinRange(int start1, int end1, int start2, int end2){
        return (start1 <= start2 && end1 >= end2) || (start2 <= start1 && end2 >= end1);
    }

    public static boolean RangeOverlapsRange(int start1, int end1, int start2, int end2){
        return (start1 <= end2 && end1 >= start2) || (start2 <= end1 && end2 >= start1);
    }

    public static boolean ValueIsWithinRange(int value, int rangeFrom, int RangeTo){
        return rangeFrom <= value && RangeTo >= value;
    }

    public static List<String> GetAssignmentPairsFromFile(){
        var assignmentPairs = new ArrayList<String>();

        try {
            File myObj = new File("puzzle-input.txt");
            Scanner myReader = new Scanner(myObj);
            while (myReader.hasNextLine()) {
              assignmentPairs.add(myReader.nextLine());
            }
            myReader.close();
          } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
          }

          return assignmentPairs;
    }
}