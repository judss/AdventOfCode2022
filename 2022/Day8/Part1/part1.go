package main

import (
	"fmt"
	"os"
)

func main() {
	inputFile := "puzzle-input.txt"

	file, err := os.ReadFile(inputFile)

	if err != nil {
		fmt.Printf("Could not read the file due to this %s error \n", err)
	}

	fileContent := string(file)

	fmt.Println("File:", fileContent)
}
