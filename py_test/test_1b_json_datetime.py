from datetime import datetime
import json
#import re

data=datetime.today().strftime("%Y-%m-%d %H:%M:%S")
d={'data':data, 'name':"upyachka"}

# 1
str1=json.dumps(d)
print (str1)

# 2
#def datetimeParser(dct):
#    for k, v in dct.items():
#        if isinstance(v, str) and re.search("\ UTC", v):
#            try:
#                dct[k] = datetime.strptime(v, DATE_FORMAT)
#            except:
#                pass
#    return dct

str2=json.loads(str1)
print(str2)