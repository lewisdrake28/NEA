from fileinput import filename
from urllib import response
import requests
import datetime
from datetime import datetime

now = datetime.now()
print(now)
print("Start")

trueWords = []
falseWords = []
checkWords = []

readName = "/home/pi/Desktop/words_alpha.txt"
writeName = "/home/pi/Desktop/"#needs + letter + ".txt"
f = open(readName, "r")

now = datetime.now()
print(now)
print("Test")

for a in f:
    try: 
        word = a

        URL = "https://api.dictionaryapi.dev/api/v2/entries/en/" + word
        response = requests.get(URL)
        statusCode = response.status_code

        # OK - assume the word is a true word
        if statusCode < 400:
            trueWords.append(a)
            
        # Not Found - assume the word is a false word
        elif statusCode == 404:
            falseWords.append(a)
            
        # all other status codes
        # check manually later/retry
        else:
            checkWords.append(a)
            
    # error when trying to make API call
    # check manually later/retry
    except:
        now = datetime.now()
        print(now)
        print("An error occurred! - ", word)
        checkWords.append(a)
f.close()

now = datetime.now()
print(now)
print("Write")

f = open("/home/pi/Desktop/TrueWords.txt", "w")
for a in trueWords:
    f.write(a)
f.close()

f = open("/home/pi/Desktop/CheckWords.txt", "w")
for a in checkWords:
    f.write(a)
f.close()

f = open("/home/pi/Desktop/FalseWords.txt", "w")
for a in falseWords:
    f.write(a)
f.close()
