message="Hello python world!"
print(message)

message = "hello python crash course world!"
print(message.title())

class Dog(): 
    def __init__(self,name,age):
         self.name = name
         self.age = age

    def Squat(self):
        print(self.name + ": hello")
my_dog = Dog('lucy',1111) 
print("name:",my_dog.name,"age:",my_dog.age)
my_dog.Squat()

my_dog_2 = Dog('lily',12)
print("name:",my_dog_2.name,"age:",my_dog_2.age)
my_dog_2.Squat()


with open('test.txt') as file_object:
	contents = file_object.read()
	print(contents.strip())
