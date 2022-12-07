package main

import (
	"fmt"
	"os"
	"sort"
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

	sort.Slice(totals, func(i, j int) bool {
		return totals[i] > totals[j]
	})

	top3 := totals[:3]
	var totalOfTop3 int

	for i := 0; i < len(top3); i++ {
		totalOfTop3 = totalOfTop3 + top3[i]
	}

	fmt.Println("The top three calories totals are:", top3)
	fmt.Println("The top three total calories is:", totalOfTop3)
}
