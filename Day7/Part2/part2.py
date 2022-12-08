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

def getRemovableSpace(dirSizes):
    # total disk space
    diskSpace = 70000000

    # disk space required
    requiredSpace = 30000000

    # used disk space - the first one is root which is total used
    used = dirSizes[0]

    # the space that's available currently
    freeSpace = diskSpace - used

    # the space we need to delete to meet the required space
    spaceToDelete = requiredSpace - freeSpace
    
    # set the current removeable space to the diskSpace
    removableSpace = diskSpace;

    # go through each of the sizes
    for size in dirSizes:
        # if the size is enough to delete to reach the required space 
        # and its less than the current removable space
        # set the removable space to this size
        if size >= spaceToDelete and size < removableSpace:
            removableSpace = size
    
    return removableSpace

# read the file
fileContents = readFile()

# build the directory tree
dirTree = getDirTree(fileContents)

# get the sizes of the directories
dirSizes = getDirSizes(dirTree)

# work out the answer
answer = getRemovableSpace(dirSizes)

print(answer)