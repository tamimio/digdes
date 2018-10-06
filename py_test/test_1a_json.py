import json

str1 = '{"a": 2, "b": 3}'
str2 = '{"a": 2, "b": 4}'

json_data1 = json.loads(str1)
json_data2 = json.loads(str2)

print(json_data1)
print(json_data2)

if json_data1["a"]!=json_data2["a"]:
    print("a:"+json_data1["a"])
    print("a:"+json_data2["a"])

if json_data1["b"]!=json_data2["b"]:
    print("b:"+str(json_data1["b"]))
    print("b:"+str(json_data2["b"]))
#---------------------------------------------
str1 = '{"a": "hello", "b": {"c": 3}}'
str2 = '{"a": "hello", "b": {"c": 11}}'

json_data1 = json.loads(str1)
json_data2 = json.loads(str2)

print(json_data1)
print(json_data2)

if json_data1["a"]!=json_data2["a"]:
    print("a:"+json_data1["a"])
    print("a:"+json_data2["a"])

if json_data1["b"]!=json_data2["b"]:
    print("\"b:\""+str(json_data1["b"]))
    print("\"b:\""+str(json_data2["b"]))