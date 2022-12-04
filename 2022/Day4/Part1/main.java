package Part1;
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

            var assigmentOverlaps = CheckIfTheAssignmentsAreWithEachOther(firstAssignment, secondAssignment);

            if(assigmentOverlaps){
                total += 1;
            }               
        }

        System.out.print(total);
        
    }

    private static boolean CheckIfTheAssignmentsAreWithEachOther(String firstAssignment, String secondAssignment) {        
        var assignmentRange1 = firstAssignment.split("-");        
        int start1 = Integer.parseInt(assignmentRange1[0]);
        int end1 = Integer.parseInt(assignmentRange1[1]);

        var assignmentRange2 = secondAssignment.split("-");
        int start2 =  Integer.parseInt(assignmentRange2[0]);
        int end2 =  Integer.parseInt(assignmentRange2[1]);

        var range1IsWithinRange2 = IsWithinRange(start1, end1, start2, end2);
        var range2IsWithinRange1 = IsWithinRange(start2, end2, start1, end1);

        if (range1IsWithinRange2 || range2IsWithinRange1) {
            return true;
        }

        return false;
    }
    
    public static boolean IsWithinRange(int start1, int end1, int start2, int end2){
        return start1 <= start2 && end1 >= end2;
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