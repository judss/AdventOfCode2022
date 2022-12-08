package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	inputFile := "puzzle-input.txt"

	file, err := os.ReadFile(inputFile)

	if err != nil {
		fmt.Printf("Could not read the file due to this %s error \n", err)
	}

	fileContent := string(file)

	elves := strings.Split(fileContent, "\r\n\r\n")

	var totals []int

	for _, elf := range elves {
		var total int = 0

		calories := strings.Split(elf, "\r\n")

		for _, calorie := range calories {
			value, err := strconv.Atoi(calorie)

			if err != nil {
				fmt.Println("Error during conversion")
				return
			}

			total += value
		}

		totals = append(totals, total)
	}

	mostCalories := totals[0]

	for i := 1; i < len(totals); i++ {

		if mostCalories < totals[i] {

			mostCalories = totals[i]
		}
	}

	fmt.Println("Total Calories of the elf carrying the most:", mostCalories)
}
