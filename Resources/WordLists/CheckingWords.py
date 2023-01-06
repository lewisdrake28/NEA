from fileinput import filename
from urllib import response
import requests

f = open("/Users/lewisdrake/NEA/Resources/WordLists/CheckWords.txt", "r")
log = []

for a in f:
    try:
        word = a

        URL = "https://api.dictionaryapi.dev/api/v2/entries/en/" + word
        response = requests.get(URL)
        statusCode = response.status_code

        if statusCode < 400:
            print('yes ' , word)
            text = 'yes ' + word
            log.append(text)
            
        elif statusCode == 404:
            print('no ' , word)
            text = 'no ' + word
            log.append(text)
            
        else:
            print('maybe ' , word)
            text = 'maybe ' + word
            log.append(text)

    except:
        print("An error occurred! - ", word)
        text = 'An error occurred! - ' + word
        log.append(text)


f.close()

f = open("/Users/lewisdrake/NEA/Resources/WordLists/Log2.txt", "w")
for a in log:
    f.write(a)
f.close()
