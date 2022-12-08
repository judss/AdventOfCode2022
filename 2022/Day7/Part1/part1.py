import os
import re
from dataclasses import dataclass

ROOT_DIR = os.path.abspath(os.curdir)

@dataclass
class directory:
  def __init__(self, dirName, parentDirName, subDirectories, files, size):
    self.dirName = dirName
    self.parentDirName = parentDirName
    self.subDirectories = subDirectories
    self.files = files
    self.Size = size
  def __repr__(self):
      return f"<Directory dirName:{self.dirName}, size: {self.Size}>"

root = directory("/", "", [], [], 0)
currentDirectory = root
smallDIRTotal = 0


def readFile():
  with open('./2022/Day7/part1/puzzle-input.txt') as f:
    contents = f.read()
    return contents

def DoCommand(command):
  global currentDirectory
  if command == "$ ls":
    #list the directories
    return
  if command == "$ cd /":
    # go to root
    currentDirectory = root
    return
  elif command == "$ cd ..":
    # go back a directory    
    currentDirectory = FindDirectoryWithinDirectories(currentDirectory.parentDirName, root.subDirectories)
    return
  else:
    # go to specific directory
    dirName = command.split("$ cd ",1)[1]
    currentDirectory = FindDirectoryWithinDirectories(dirName, root.subDirectories)
    return

def ProcessDirectory(dir):
  # store directory
  dirName = dir.split("dir ",1)[1]
  existingDirectory = FindDirectoryWithinDirectories(dirName, root.subDirectories)
  if existingDirectory is None:
    newDirectory = directory(dirName, currentDirectory.dirName, [], [], 0)
    currentDirectory.subDirectories.append(newDirectory)
  
def FindDirectoryWithinDirectories(dirName, directories):
  if dirName == "/":
    return root
  
  for directory in directories:
    if directory.dirName == dirName:
      # Found the directory, return it
      return directory
    else:
      # Recursively search in the sub-directories
      result = FindDirectoryWithinDirectories(dirName, directory.subDirectories)
      if result:
        # Found the directory, return it
        return result
  return None

def AddFileToDirectory(file):
  global root
  global currentDirectory
  
  if file not in currentDirectory.files:
    number = re.findall(r'\d+', file)
    fileSize = int(number[0])    
    AddSizeToParentDirectories(currentDirectory, fileSize)
    currentDirectory.files.append(file)

def AddSizeToParentDirectories(directory, size):
  # Set the size of the current directory
  directory.Size += size

  # if the directory has a parent, recursively add the size to the parent directory
  if directory.parentDirName:
    parent_directory = FindDirectoryWithinDirectories(directory.parentDirName, root.subDirectories)
    AddSizeToParentDirectories(parent_directory, size)

def getTotal(directories):
  total = 0
  for dir in directories:    
    subDirectoryTotal = getTotal(dir.subDirectories)
    total += subDirectoryTotal if subDirectoryTotal <= 100000 else 0
    total += dir.Size if dir.Size <= 100000 else 0

  return total

Sizes = []
def printAll(directories):
  global Sizes
  for dir in directories:
    print(dir.__repr__())
    Sizes.append(dir.Size)
    printAll(dir.subDirectories)

# read the file
fileContents = readFile()
rows = fileContents.splitlines()

for x in rows:
  if x.startswith("$"):
    # is command
    DoCommand(x)
  elif x.startswith("dir"):
    # is a directory
    ProcessDirectory(x)
  else:
    # is a file
    AddFileToDirectory(x)

answer = getTotal(root.subDirectories)


printAll(root.subDirectories)
print(answer)
print("Sizes:", Sizes)

total = 0
for size in Sizes:
  total += size if size <= 100000 else 0
print(total)

