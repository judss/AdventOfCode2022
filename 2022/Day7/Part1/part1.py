import os
import re

class directory:
  def __init__(self, name, parent):
    self.name = name
    self.parent = parent

class directoryFile:
  def __init__(self, dirName, files):
    self.dirName = dirName
    self.files = files

ROOT_DIR = os.path.abspath(os.curdir)

directories = []
currentDirectory = directory("/", "/") 
directoryFiles = []
answer = 0

def readFile():
  with open('./2022/Day7/part1/puzzle-input.txt') as f:
    contents = f.read()
    return contents

def DoCommand(command):
  global currentDirectory 
  global directories
  if command == "$ ls":
    #list the directories
    return
  if command == "$ cd /":
    # go to root
    currentDirectory = directory("/", "/")
    return
  elif command == "$ cd ..":
    # go back a directory
    parentDirectory = getDirectory(currentDirectory.parent)
    currentDirectory = parentDirectory
    return
  else:
    # go to specific directory
    dirName = command.split("$ cd ",1)[1]      
    currentDirectory = getDirectory(dirName)
    #directory(dirName, currentDirectory.name)

def ProcessDirectory(dir):
  global directories
  # store directory
  dirName = dir.split("dir ",1)[1]
  if dirName not in directories:  
    parentName = currentDirectory.name
    directories.append(directory(dirName, parentName))

def getExistingDirectoryFiles(dirName):
  global directoryFiles
  return next((obj for obj in directoryFiles if obj.dirName == dirName), None)

def getDirectory(dirName):
  global directories
  return next((obj for obj in directories if obj.name == dirName), None)

def AddFileToDirectory(file):
  #global directories
  global directoryFiles
  global currentDirectory
  
  existingDirectory = getExistingDirectoryFiles(currentDirectory.name)
  #print(existingDirectory)
  if existingDirectory is not None:
    if file not in existingDirectory.files :
      existingDirectory.files.append(file)
  else:
    files = [ file ]
    current = directoryFile(currentDirectory.name, files)
    directoryFiles.append(current) 

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
    

for df in directoryFiles:
  directoryTotal = 0
  files = df.files
  print("Name: " + df.dirName)
  for file in files:
    print("-- File: " + file)
    numbers = list(map(int, re.findall(r'\d+', file)))
    directoryTotal += sum(numbers)
  if directoryTotal <= 100000:
      answer += directoryTotal

print("Answer:", answer)
