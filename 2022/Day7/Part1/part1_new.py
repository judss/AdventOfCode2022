import os
from pathlib import Path

ROOT_DIR = os.path.abspath(os.curdir)

def readFile():
  with open('./2022/Day7/part1/puzzle-input.txt') as f:
    contents = []
    for line in f.readlines():
        contents.append(line.strip())
    return contents

def getDirTree(commands):
    currentPath = Path("")
    tree = {}
    for command in commands:
        if "$ cd" in command:
            if ".." in command:
                currentPath = currentPath.parent
            else:
                name = command[5:]
                currentPath = currentPath / name
                if currentPath not in tree:
                    tree[currentPath] = []
        elif "dir" in command:
            name = command.split(" ")
            tree[currentPath].append(currentPath / name[1])
        elif command[0].isnumeric():
            file = command.split(" ")
            fileSize = file[0]
            fileName = file[1]
            tree[currentPath].append((fileName, int(fileSize)))
    return tree

def getDirSizes(tree):
    sizes = []
    for dir in tree:
        sizeOfDir = totalDirSize(dir, tree)
        sizes.append(sizeOfDir)
    return sizes

def totalDirSize(path, tree):
    total = 0
    for item in tree[path]:
        if isinstance(item, Path):
            total += totalDirSize(item, tree)
        else:
            total += item[1]
    return total

def getTotal(dirSizes):
    total = 0
    for size in dirSizes:
        if size <= 100000:
            total += size
    return total

# read the file
fileContents = readFile()

# build the directory tree
dirTree = getDirTree(fileContents)

# get the sizes of the directories
dirSizes = getDirSizes(dirTree)

# work out the answer
answer = getTotal(dirSizes)

print(answer)