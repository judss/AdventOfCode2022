import os

ROOT_DIR = os.path.abspath(os.curdir)

def readFile():
  with open('./2022/Day3/part1/puzzle-input.txt') as f:
    contents = f.read()
    return contents

def splitTheRucksacks(fileContents):
    rucksacks = fileContents.split()
    return rucksacks

def getPriority(char):
    correction_number = 96 if char.islower() else 38
    asci_number = ord((char))
    return asci_number - correction_number

# read the file
fileContents = readFile()

# split the rucksacks
rucksacks = splitTheRucksacks(fileContents)

#split the compartments and work out the total priotiries
total = 0

for x in rucksacks:
    first_compartment  = x[:len(x)//2]
    second_compartment = x[len(x)//2:]

#find the letter that is in both compartments
    common_character = ''.join(set(first_compartment).intersection(second_compartment))

#calculate the priority of that letter
    priority = getPriority(common_character)

#total all of the priorities
    total += priority

#print the answer
print(total)



