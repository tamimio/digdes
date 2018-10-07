# I still don't get your grade method:
# Like, we calc. sum of 1-3 tests and get 50 points which is B;
# But then we calc. sum of this and 4th test (50+50=100 points), whis is still B
# in the output array with letter-representation of grades.
# So I solve this task as I got it from the 1st e-mail.

grade=[]
for i in range(1,5):
    print("Input grade for the ", i, " task -> ")
    k=int(input())
    grade.append(k)
total_score=sum(grade)
print("Total score is: ", total_score)
letter_grade=[]
for i in range(0,4):
    if grade[i]<=10 :
        letter_grade.append("D")
    elif grade[i]>10 and grade[i]<30:
        letter_grade.append("C")
    elif grade[i]>=30 and grade[i]<=50:
        letter_grade.append("B")        
print("Letter grades for entered grades are: ", letter_grade)
print("Letter grade for all tests: ", end='')
if total_score>=90:
    print("A")
elif total_score>=80:
    print("B")
elif total_score>=70:
    print("C")
elif total_score>=60:
    print("D")
else:
    print("=(")
