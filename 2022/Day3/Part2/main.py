import os

ROOT_DIR = os.path.abspath(os.curdir)

def readFile():
  with open('./2022/Day3/part2/puzzle-input.txt') as f:
    contents = f.read()
    return contents

def splitTheRucksacks(fileContents):
    rucksacks = fileContents.split()
    return rucksacks

def getPriority(char):
    correction_number = 96 if char.islower() else 38
    asci_number = ord((char))
    return asci_number - correction_number

def bundleTheRucksacksIntoGroups(rucksacks):
    for i in range(0, len(rucksacks), 3):
        yield rucksacks[i:i + 3]

# read the file
fileContents = readFile()

# split the rucksacks
rucksacks = splitTheRucksacks(fileContents)

# bundle the rucksack into groups of 3
bundles = bundleTheRucksacksIntoGroups(rucksacks)

#find the common letter in each bundle and total the prorities together
total = 0

# go into each bundle and find the common character
for bundle in bundles:
    result = set.intersection(*map(set,bundle))
    common_character = ''.join(sorted(list(result)))

#calculate the priority of that character
    priority = getPriority(common_character)

#total all of the priorities
    total += priority

#print the answer
print(total)



