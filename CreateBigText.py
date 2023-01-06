import os

def GetData(path, bigText):
    directory = os.fsencode(path)

    for file in os.listdir(directory):
        filename = os.fsdecode(file)
        
        if (filename.endswith(".cs")):
            fullPath = path + "/" + filename
            print(fullPath)
            f = open(fullPath, "r")
            bigText = bigText + f.read()
            f.close()

            bigText = bigText + "\n" + "\n"
            
    return bigText

bigText = "";

bigText = GetData("/Users/lewisdrake/NEA/FINAL/Classes", bigText)
bigText = GetData("/Users/lewisdrake/NEA/FINAL/View designers", bigText)
bigText = GetData("/Users/lewisdrake/NEA/FINAL/Views", bigText)

f = open("/Users/lewisdrake/NEA/BigText.txt", "w")
f.write(bigText)
f.close()

print(bigText)
